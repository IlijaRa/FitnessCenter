using FitnessCenterLibrary.Models;
using FitnessCenterMVC.Data;
using FitnessCenterMVC.ModelMapper;
using FitnessCenterMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Contracts;

namespace FitnessCenterMVC.Controllers
{
    public class CoachController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CoachController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmCoach(string id)
        {
            var coach = await _context.Coach.FirstOrDefaultAsync(x => x.Id == id);
            if (coach == null)
            {
                // if we got here, something failed
                return View("Error");
            }
            var coachViewModel = CoachMapper.ConvertToCoachViewModel(coach);

            return View(coachViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmCoach(CoachViewModel model)
        {
            //We are not going to check if model is correct because that proccess was done when user registred himself.
            //Now we know that credentials are legit. Despite that, admin cannot change users info at this stage so it stays legit.

            var coach = await _context.Coach.FirstOrDefaultAsync(x => x.Id == model.Id);

            coach.IsActive = model.IsActive;
            coach.FitnessCenterId = model.FitnessCenterId;

            await _context.SaveChangesAsync();
            return RedirectToAction("GetAllInactiveCoaches", "Coach");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllInactiveCoaches()
        {
            var coachViewModels = new List<CoachViewModel>();
            var coaches = await _context.Coach.Where(x => x.IsActive == false).ToListAsync();

            foreach (var coach in coaches)
            {
                coachViewModels.Add(CoachMapper.ConvertToCoachViewModel(coach));
            }

            return View(coachViewModels);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteCoach(string id)
        {
            var coach = await _context.Coach.FirstOrDefaultAsync(x => x.Id == id);
            if (coach == null)
            {
                // if we got here, something failed
                return View("Error");
            }

            var workouts = await _context.Workout.Where(x => x.CoachId == coach.Id).ToListAsync();
            var terms = await _context.Term.Where(x => x.CoachId == coach.Id).ToListAsync();
            var fitnessMemberWorkouts = await _context.FitnessMemberWorkout.ToListAsync();
            var member_workout_to_delete = new List<FitnessMemberWorkout>();


            foreach (var fmw in fitnessMemberWorkouts)
            {
                foreach (var workout in workouts)
                {
                    if (fmw.WorkoutId == workout.Id)
                    {
                        member_workout_to_delete.Add(fmw);
                    }
                }
            }

            //TODO:check if delete fitnesscentermember and term columns!
            _context.Term.RemoveRange(terms);
            _context.FitnessMemberWorkout.RemoveRange(member_workout_to_delete);
            _context.Workout.RemoveRange(workouts);
            _context.Coach.Remove(coach);
            await _context.SaveChangesAsync();
            return RedirectToAction("GetAllInactiveCoaches", "Coach");
        }
    }
}