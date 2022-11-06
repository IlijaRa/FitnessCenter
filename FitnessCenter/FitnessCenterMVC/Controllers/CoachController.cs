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
            var coachViewModel = CoachConversions.ConvertToCoachViewModel(coach);

            return View(coachViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmCoach(CoachViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // if we got here, something failed
                return View("Error");
            }

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
                coachViewModels.Add(CoachConversions.ConvertToCoachViewModel(coach));
            }

            return View(coachViewModels);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteCoach(string id)
        {
            var coach = await _context.Coach.FirstOrDefaultAsync(x => x.Id == id);

            if(coach == null)
            {
                // if we got here, something failed
                return View("Error");
            }

            //TODO: Delete tuples from other tables where this coach is mentioned
            _context.Coach.Remove(coach);
            await _context.SaveChangesAsync();
            return RedirectToAction("GetAllInactiveCoaches", "Coach");
        }
    }
}