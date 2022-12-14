using AutoMapper.Execution;
using FitnessCenterLibrary.Models;
using FitnessCenterMVC.Data;
using FitnessCenterMVC.ModelMapper;
using FitnessCenterMVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace FitnessCenterMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index(string sortCriteria = "")
        {
            var workouts = await _context.Workout.ToListAsync();
            var workoutViewModels = new List<WorkoutViewModel>();

            ChangeWorkoutStatusToCompleted(workouts);

            foreach (var workout in workouts)
            {
                workoutViewModels.Add(WorkoutMapper.ConvertToWorkoutViewModel(workout));
            }
            if (sortCriteria.Equals("price"))
            {
                workoutViewModels.Sort();
            }

            return View(workoutViewModels);
        }

        public IActionResult Privacy()
        {

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private async Task ChangeWorkoutStatusToCompleted(List<Workout> workouts)
        {
            var member = await _context.FitnessCenterMember.Where(x => x.UserName == User.Identity.Name).FirstOrDefaultAsync();

            foreach (var workout in workouts)
            {
                if (workout.EndTime < DateTime.Now)
                {
                    var fitnessMemberWorkout = await _context.FitnessMemberWorkout.Where(x => x.FitnessCenterMemberId == member.Id && x.WorkoutId == workout.Id).FirstOrDefaultAsync();
                    if (fitnessMemberWorkout != null)
                    {
                        fitnessMemberWorkout.State = Enums.WorkoutState.Completed;
                    }
                }
            }
            _context.SaveChangesAsync();
        }
    }
}