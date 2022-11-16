using FitnessCenterLibrary.Models;
using FitnessCenterMVC.Data;
using FitnessCenterMVC.ModelMapper;
using FitnessCenterMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FitnessCenterMVC.Controllers
{
    public class HallController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HallController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult AddHall(int id)
        {
            var hall = new HallViewModel();
            hall.FitnessCenterId = id;

            return View(hall);
        }

        [HttpPost]
        public async Task<IActionResult> AddHall(HallViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // if we got here, something failed
                return View("Error");
            }

            var hall = HallMApper.ConvertToHall(model);

            await _context.Hall.AddAsync(hall);
            await _context.SaveChangesAsync();

            // We can easily populate table FitnessCenterHall with available HallId and FitnessCenterId
            // We haven't known halls id until now, because id is generated in the database.
            var hall_with_id = _context.Hall.FirstOrDefault(x => (x.HallMark == model.HallMark) && (x.FitnessCenterId == model.FitnessCenterId));

            if (hall_with_id == null)
            {
                // if we got here, something failed
                return View("Error");
            }

            var fitnessCenterHall = new FitnessCenterHall();
            fitnessCenterHall.HallId = hall_with_id.Id;
            fitnessCenterHall.FitnessCenterId = model.FitnessCenterId;

            await _context.FitnessCenterHall.AddAsync(fitnessCenterHall);
            await _context.SaveChangesAsync();

            return RedirectToAction("EditFitnessCenter", "FitnessCenter", new { id = model.FitnessCenterId });
        }

        [HttpGet]
        public IActionResult EditHall(int id)
        {
            var hall = _context.Hall.FirstOrDefault(x => x.Id == id);
            var hallViewModel = HallMApper.ConvertToHallViewModel(hall);

            return View(hallViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditHall(HallViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // if we got here, something failed
                return View("Error");
            }

            var hall = await _context.Hall.FirstOrDefaultAsync(x => x.Id == model.Id);

            hall.Capacity = model.Capacity;
            hall.HallMark = model.HallMark;

            await _context.SaveChangesAsync();
            return RedirectToAction("EditFitnessCenter", "FitnessCenter", new { id = model.FitnessCenterId });
        }

        [HttpGet]
        public async Task<IActionResult> DeleteHall(int id)
        {
            var hall = await _context.Hall.FirstOrDefaultAsync(x => x.Id == id);
            if (hall == null)
            {
                // if we got here, something failed
                return View("Error");
            }

            // At the same time, we are deleting connected tuples in FitnessCenterHall table
            var fitnessCenterHall = new FitnessCenterHall();
            fitnessCenterHall.HallId = hall.Id;
            fitnessCenterHall.FitnessCenterId = hall.FitnessCenterId;

            _context.FitnessCenterHall.Remove(fitnessCenterHall);
            _context.Hall.Remove(hall);
            await _context.SaveChangesAsync();
            return RedirectToAction("EditFitnessCenter", "FitnessCenter", new { id = hall.FitnessCenterId });
        }
    }
}