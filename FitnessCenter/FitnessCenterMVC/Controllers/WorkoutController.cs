using FitnessCenterLibrary.Models;
using FitnessCenterMVC.Data;
using FitnessCenterMVC.ModelMapper;
using FitnessCenterMVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System;
using System.Data;
using System.Linq;
using System.Transactions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace FitnessCenterMVC.Controllers
{
    public class WorkoutController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WorkoutController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> AddWorkout()
        {
            var workoutViewModel = new WorkoutViewModel();
            var coach = await _context.Coach.FirstOrDefaultAsync(x => x.UserName == User.Identity.Name);
            workoutViewModel.CoachId = coach.Id;

            return View(workoutViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddWorkout(WorkoutViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // if we got here, something failed
                var workoutViewModel = new WorkoutViewModel();
                var coach = await _context.Coach.FirstOrDefaultAsync(x => x.UserName == User.Identity.Name);
                workoutViewModel.CoachId = coach.Id;
                return View(workoutViewModel);
            }

            try
            {
                var workout = WorkoutConversions.ConvertToWorkout(model);
                await _context.Workout.AddAsync(workout);
                await _context.SaveChangesAsync();

                var located_workout = await _context.Workout.FirstOrDefaultAsync(x => x.Title.Equals(model.Title));
                var term = new Term(){ CoachId = model.CoachId, WorkoutId = located_workout.Id, FreeSpace = model.Capacity };
                await _context.Term.AddAsync(term);

                await _context.SaveChangesAsync();

                return RedirectToAction("GetAllWorkouts", "Workout");
            }
            catch (Exception e)
            {
                // if we got here, something failed
                ViewData["ErrorMessage"] = e.Message;
                return View("Error");
            }

        }

        [HttpGet]
        public async Task<IActionResult> EditWorkout(int id)
        {
            var workout = await _context.Workout.FirstOrDefaultAsync(x => x.Id == id);
            if (workout == null)
            {
                // if we got here, something failed
                return View("Error");
            }

            var workoutViewModel = WorkoutConversions.ConvertToWorkoutViewModel(workout);

            return View(workoutViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditWorkout(WorkoutViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // if we got here, something failed
                return View();
            }

            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var workout = _context.Workout.FirstOrDefault(x => x.Id == model.Id);
                    workout.Title = model.Title;
                    workout.Description = model.Description;
                    workout.StartTime = model.StartTime;
                    workout.EndTime = model.EndTime;
                    workout.Price = model.Price;
                    workout.Capacity = model.Capacity;
                    workout.CoachId = model.CoachId;

                    if (model.Type == Enums.WorkoutType.Conditional.ToString())
                    {
                        workout.Type = Enums.WorkoutType.Conditional;
                    }
                    else if (model.Type == Enums.WorkoutType.PowerLifting.ToString())
                    {
                        workout.Type = Enums.WorkoutType.PowerLifting;
                    }
                    else if (model.Type == Enums.WorkoutType.Bodybuilding.ToString())
                    {
                        workout.Type = Enums.WorkoutType.Bodybuilding;
                    }
                    await _context.SaveChangesAsync();

                    var term = _context.Term.FirstOrDefault(x => x.CoachId == model.CoachId && x.WorkoutId == model.Id);
                    term.FreeSpace = model.Capacity; //TODO: Make calculation by suptracting freespace with someone who already reserved this term. 
                    await _context.SaveChangesAsync();
                    transaction.Complete();
                }
                catch (TransactionException e)
                {
                    ViewData["ErrorMessage"] = e.Message;
                    return View("Error");
                }
            } 

            return RedirectToAction("GetAllWorkouts", "Workout");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWorkouts()
        {
            var workoutViewModels = new List<WorkoutViewModel>();
            var coach = await _context.Coach.FirstOrDefaultAsync(x => x.UserName == User.Identity.Name);

            if(coach == null)
            {
                // if we got here, something failed
                ViewData["ErrorMessage"] = "You are not logged in as coach!";
                return View("Error");
            }

            var workouts = await _context.Workout.Where(x => x.CoachId == coach.Id).ToListAsync();

            foreach (var workout in workouts)
            {
                workoutViewModels.Add(WorkoutConversions.ConvertToWorkoutViewModel(workout));
            }
            return View(workoutViewModels);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteWorkout(int id)
        {
            var workout = await _context.Workout.FirstOrDefaultAsync(x => x.Id == id);
            if (workout == null)
            {
                // if we got here, something failed
                return View("Error");
            }

            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    // We must delete rows from Term table as well
                    var term = await _context.Term.Where(x => x.WorkoutId == id).ToListAsync();

                    _context.Term.RemoveRange(term);
                    _context.Workout.Remove(workout);
                    await _context.SaveChangesAsync();
                    transaction.Complete();
                }
                catch (TransactionException e)
                {
                    ViewData["ErrorMessage"] = e.Message;
                    return View("Error");
                }
            }

            return RedirectToAction("GetAllWorkouts", "Workout");
        }

        [HttpGet]
        public async Task<IActionResult> EnrollInWorkout(int id)
        {
            var term = _context.Term.FirstOrDefault(x => x.WorkoutId == id);
            var workout = _context.Workout.FirstOrDefault(x => x.Id == id);

            if (term == null || workout == null)
            {
                ViewData["ErrorMessage"] = "Request couldn't be executed!";
                return View("Error");
            }
            var workoutViewModel = WorkoutConversions.ConvertToWorkoutViewModel(workout);

            // We are putting freespace value into capacity, because maybe someone have enrolled in workout already
            workoutViewModel.Capacity = term.FreeSpace; 

            return View(workoutViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EnrollInWorkout(WorkoutViewModel model)
        {

            if (model.Capacity < 1)
            {
                ViewData["ErrorMessage"] = "There is no space for more enrollments!";
                return View("Error");
            }

            try
            {
                var member = await _context.FitnessCenterMember.FirstOrDefaultAsync(x => x.UserName == User.Identity.Name);
                
                var checkfitnessMemberWorkout = await _context.FitnessMemberWorkout.Where(x => x.FitnessCenterMemberId == member.Id && x.WorkoutId == model.Id).FirstOrDefaultAsync();

                if (checkfitnessMemberWorkout != null)
                {
                    ViewData["ErrorMessage"] = "You already reserved this term!";
                    return View("Error");
                }

                var fitnessMemberWorkout = new FitnessMemberWorkout()
                {
                    FitnessCenterMemberId = member.Id,
                    WorkoutId = model.Id,
                    State = Enums.WorkoutState.NonCompleted,
                    Rate = 0
                };
                await _context.FitnessMemberWorkout.AddAsync(fitnessMemberWorkout);
                await _context.SaveChangesAsync();

                var term = _context.Term.FirstOrDefault(x => x.CoachId == model.CoachId && x.WorkoutId == model.Id);
                term.FreeSpace--;
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            catch (Exception e)
            {
                ViewData["ErrorMessage"] = e.Message;
                return View("Error");
            }
            
        }

        [HttpGet]
        public async Task<IActionResult> SearchWorkout(string searching)
        {
            if (searching.Equals("") || String.IsNullOrEmpty(searching))
            {
                return View();
            }
            var workouts = await _context.Workout.ToListAsync();
            var found_workouts = new List<WorkoutViewModel>();
            searching = searching.ToLower();
            
            foreach (var workout in workouts)
            {
                if (HasSimilarities(workout, searching))
                {
                    found_workouts.Add(WorkoutConversions.ConvertToWorkoutViewModel(workout));
                }
            }

            return View(found_workouts);
        }

        //Function shows if workout has similar values which user searched
        private bool HasSimilarities(Workout workout, string searching)
        {
            double value;
            bool hasSimilarity = false;

            if (workout.Title.ToLower().Contains(searching))  hasSimilarity = true;

            else if (workout.Type.ToString().ToLower().Contains(searching)) hasSimilarity = true;
            
            else if (workout.Description.ToLower().Contains(searching)) hasSimilarity = true;
            
            else if (double.TryParse(searching, out value))
            {
                if (workout.Price == value || (workout.EndTime - workout.StartTime).TotalHours == value || workout.Capacity == value) hasSimilarity = true;
            }

            else if(SearchCombined(workout, searching)) hasSimilarity = true;

            return hasSimilarity;
        }

        private bool SearchCombined(Workout workout, string searching)
        {
            bool hasSimilarity = false;
            string[] searching_elements = SplitString(searching);
            string combined_string = CombinedString(workout);

            foreach (var element in searching_elements)
            {
                if (combined_string.Contains(element))
                {
                    hasSimilarity = true;
                    break;
                }
            }
            return hasSimilarity;
        }

        private string[] SplitString(string searching)
        {
            string[] searching_elements = searching.Split(' '); // splits an array on places where " " is found
            searching_elements = searching_elements.Where(w => w != "").ToArray(); // excludes from array all elements that are " "

            return searching_elements;
        }

        private string CombinedString(Workout workout)
        {
            // Group all values to one string
            string combined_string = workout.Title +
                                            workout.Type.ToString() +
                                            workout.Description +
                                            (workout.EndTime - workout.StartTime).TotalHours.ToString() +
                                            workout.Price.ToString() +
                                            workout.Capacity.ToString();

            // deletes all whitespaces from a string
            combined_string = new string(combined_string.ToLower()
                                                        .ToCharArray()
                                                        .Where(c => !Char.IsWhiteSpace(c))
                                                        .ToArray());

            return combined_string;
        }
    }
}