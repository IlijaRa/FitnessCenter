using AutoMapper.Execution;
using FitnessCenterLibrary.Models;
using FitnessCenterMVC.Data;
using FitnessCenterMVC.ModelMapper;
using FitnessCenterMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Reflection.Metadata;
using System.Transactions;

namespace FitnessCenterMVC.Controllers
{
    public class FitnessCenterMemberController : Controller
    {

        private readonly ApplicationDbContext _context;

        public FitnessCenterMemberController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> DeleteFitnessCenterMember(string id)
        {
            var member = await _context.FitnessCenterMember.FirstOrDefaultAsync(x => x.Id == id);
            if (member == null)
            {
                // if we got here, something failed
                return View("Error");
            }

            var fitnessMemberWorkouts = await _context.FitnessMemberWorkout.ToListAsync();
            var member_workout_to_delete = new List<FitnessMemberWorkout>();


            foreach (var fmw in fitnessMemberWorkouts)
            {
                if (fmw.FitnessCenterMemberId == id)
                {
                    member_workout_to_delete.Add(fmw);
                }
            }

            _context.FitnessMemberWorkout.RemoveRange(member_workout_to_delete);
            _context.FitnessCenterMember.Remove(member);
            await _context.SaveChangesAsync();
            return RedirectToAction("GetAllInactiveCoaches", "Coach");
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
            var workoutViewModel = WorkoutMapper.ConvertToWorkoutViewModel(workout);

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
                    WorkoutRate = 0,
                    CoachRate = 0
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
        public async Task<IActionResult> GetAllEnrolledWorkouts()
        {
            var workoutViewModels = new List<WorkoutViewModel>();
            var member = await _context.FitnessCenterMember.FirstOrDefaultAsync(x => x.UserName == User.Identity.Name);

            // Joined tables "FitnessCenterMember", "Workout" and "FitnessMemberWorkout", syntax-based query
            var workouts = from memberWorkout in _context.FitnessMemberWorkout
                           join mem in _context.FitnessCenterMember on memberWorkout.FitnessCenterMemberId equals mem.Id
                           join workout in _context.Workout on memberWorkout.WorkoutId equals workout.Id
                           where mem.Id == member.Id && memberWorkout.State == Enums.WorkoutState.NonCompleted
                           select new Workout
                           {
                               Id = workout.Id,
                               Title = workout.Title,
                               Description = workout.Description,
                               Type = workout.Type,
                               StartTime = workout.StartTime,
                               EndTime = workout.EndTime,
                               Price = workout.Price,
                               Capacity = workout.Capacity
                           };

            foreach (var workout in workouts)
            {
                workoutViewModels.Add(WorkoutMapper.ConvertToWorkoutViewModel(workout));
            }

            return View(workoutViewModels);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCompletedWorkouts()
        {
            var completedWorkoutViewModel = new CompletedWorkoutViewModel();
            var member = await _context.FitnessCenterMember.FirstOrDefaultAsync(x => x.UserName == User.Identity.Name);

            // Joined tables "FitnessCenterMember", "Workout" and "FitnessMemberWorkout", syntax-based query
            var workouts = from memberWorkout in _context.FitnessMemberWorkout
                           join mem in _context.FitnessCenterMember on memberWorkout.FitnessCenterMemberId equals mem.Id
                           join workout in _context.Workout on memberWorkout.WorkoutId equals workout.Id
                           where mem.Id == member.Id && memberWorkout.State == Enums.WorkoutState.Completed
                           select new{
                               Id = workout.Id,
                               Title = workout.Title,
                               Description = workout.Description,
                               Type = workout.Type,
                               StartTime = workout.StartTime,
                               EndTime = workout.EndTime,
                               Price = workout.Price,
                               Capacity = workout.Capacity,
                               WorkoutRate = memberWorkout.WorkoutRate,
                               CoachRate = memberWorkout.CoachRate
                           };

            foreach (var workout in workouts)
            {
                var workoutViewModel = new WorkoutViewModel();
                workoutViewModel.Id = workout.Id;
                workoutViewModel.Title = workout.Title;
                workoutViewModel.Description = workout.Description;
                workoutViewModel.Type = workout.Type.ToString();
                workoutViewModel.StartTime = workout.StartTime;
                workoutViewModel.EndTime = workout.EndTime;
                workoutViewModel.Price = workout.Price;
                workoutViewModel.Capacity = workout.Capacity;

                if (workout.WorkoutRate == 0 && workout.CoachRate == 0) completedWorkoutViewModel.unrated_workouts.Add(workoutViewModel);
                else completedWorkoutViewModel.rated_workouts.Add(workoutViewModel);

            }

            return View(completedWorkoutViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> CancelWorkout(int id)
        {
            var memberWorkout = await _context.FitnessMemberWorkout.FirstOrDefaultAsync(x => x.WorkoutId == id);
            if (memberWorkout == null)
            {
                // if we got here, something failed
                return View("Error");
            }

            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    _context.FitnessMemberWorkout.Remove(memberWorkout);

                    // We must to increase "FreeSpace" in Term table
                    var term = await _context.Term.Where(x => x.WorkoutId == id).FirstOrDefaultAsync();
                    term.FreeSpace++;
               
                    await _context.SaveChangesAsync();
                    
                    transaction.Complete();
                }
                catch (TransactionException e)
                {
                    ViewData["ErrorMessage"] = e.Message;
                    return View("Error");
                }
            }

            return RedirectToAction("GetAllEnrolledWorkouts", "FitnessCenterMember");
        }

        [HttpGet]
        public async Task<IActionResult> RateWorkout(int id)
        {
            var member = await _context.FitnessCenterMember.FirstOrDefaultAsync(x => x.UserName == User.Identity.Name);
            var workout = await _context.Workout.FirstOrDefaultAsync(x => x.Id == id);

            if (workout == null)
            {
                // if we got here, something failed
                return View("Error");
            }

            var rateWorkoutViewModel = new RateWorkoutViewModel();

            rateWorkoutViewModel.Id = workout.Id;
            rateWorkoutViewModel.CoachId = workout.CoachId;
            rateWorkoutViewModel.Title = workout.Title;
            rateWorkoutViewModel.Description = workout.Description;
            rateWorkoutViewModel.Type = workout.Type.ToString();
            rateWorkoutViewModel.StartTime = workout.StartTime;
            rateWorkoutViewModel.EndTime = workout.EndTime;
            rateWorkoutViewModel.WorkoutRate = 0;
            rateWorkoutViewModel.CoachRate = 0;

            return View(rateWorkoutViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> RateWorkout(RateWorkoutViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            try
            {

                var user = await _context.FitnessCenterMember.Where(x => x.UserName == User.Identity.Name).FirstOrDefaultAsync();
                var memberWorkout = await _context.FitnessMemberWorkout.Where(x => x.WorkoutId == model.Id).FirstOrDefaultAsync();
                
                if (memberWorkout.FitnessCenterMemberId == user.Id  && 
                    memberWorkout.WorkoutId == model.Id             && 
                    memberWorkout.WorkoutRate != 0                  && 
                    memberWorkout.CoachRate != 0)
                {
                    ViewData["ErrorMessage"] = "You already rated this workout!";
                    return View("Error");
                }

                memberWorkout.WorkoutRate = model.WorkoutRate;
                memberWorkout.CoachRate = model.CoachRate;

                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                ViewData["ErrorMessage"] = e.Message;
                return View("Error");
            }

            return RedirectToAction("GetAllCompletedWorkouts", "FitnessCenterMember");
        }
    }
}