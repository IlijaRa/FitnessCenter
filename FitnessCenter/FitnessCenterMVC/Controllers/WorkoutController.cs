using FitnessCenterLibrary.Models;
using FitnessCenterMVC.Data;
using FitnessCenterMVC.ModelMapper;
using FitnessCenterMVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
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

                    if (model.Type.Equals("Conditional"))
                    {
                        workout.Type = Enums.WorkoutType.Conditional;
                    }
                    else if (model.Type.Equals("PowerLifting"))
                    {
                        workout.Type = Enums.WorkoutType.PowerLifting;
                    }
                    else
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
                ViewData["ErrorMessage"] = "You are not logged in!";
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
    }
}