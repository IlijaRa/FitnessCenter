using FitnessCenterLibrary.Models;
using FitnessCenterMVC.Data;
using FitnessCenterMVC.ModelMapper;
using FitnessCenterMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public async Task<IActionResult> GetAllEnrolledWorkouts()
        {
            var workoutViewModels = new List<WorkoutViewModel>();
            var member = await _context.FitnessCenterMember.FirstOrDefaultAsync(x => x.UserName == User.Identity.Name);

            // Joined tables "FitnessCenterMember", "Workout" and "FitnessMemberWorkout", syntax-based query
            var workouts = from memberWorkout in _context.FitnessMemberWorkout
                           join mem in _context.FitnessCenterMember on memberWorkout.FitnessCenterMemberId equals mem.Id
                           join workout in _context.Workout on memberWorkout.WorkoutId equals workout.Id
                           where mem.Id == member.Id
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
                workoutViewModels.Add(WorkoutConversions.ConvertToWorkoutViewModel(workout));
            }

            return View(workoutViewModels);
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
    }
}