using FitnessCenterMVC.Data;
using FitnessCenterMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using FitnessCenterMVC.ModelMapper;

namespace FitnessCenterMVC.Controllers
{
    public class FitnessCenterController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FitnessCenterController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult AddFitnessCenter()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddFitnessCenter(FitnessCenterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // if we got here, something failed
                return View("Error");
            }

            var fitnessCenter = FitnessCenterConversions.ConvertToFintessCenter(model);
            await _context.FitnessCenter.AddAsync(fitnessCenter);
            await _context.SaveChangesAsync();
            return RedirectToAction("GetAllFitnessCenters", "FitnessCenter");
        }

        [HttpGet]
        public async Task<IActionResult> EditFitnessCenter(int id)
        {
            var fitnessCenter = await _context.FitnessCenter.FirstOrDefaultAsync(x => x.Id == id);
            var halls = await _context.Hall.Where(x => x.FitnessCenterId == id).ToListAsync();
            if (fitnessCenter == null || halls == null)
            {
                // if we got here, something failed
                return View("Error");
            }

            EditFitnessCenterViewModel model = new EditFitnessCenterViewModel();
            List<HallViewModel> hallViewModels = new List<HallViewModel>();
            var fitnessCenterViewModel = FitnessCenterConversions.ConvertToFintessCenterViewModel(fitnessCenter);

            foreach (var hall in halls)
            {
            hallViewModels.Add(HallConversions.ConvertToHallViewModel(hall));
            }

            model.fitnessCenter = fitnessCenterViewModel;
            model.halls = hallViewModels;

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditFitnessCenter(EditFitnessCenterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // if we got here, something failed
                return View("Error");
            }

            var fitnessCenter = await _context.FitnessCenter.FirstOrDefaultAsync(x => x.Id == model.fitnessCenter.Id);

            fitnessCenter.Title = model.fitnessCenter.Title;
            fitnessCenter.PhoneNumber = model.fitnessCenter.PhoneNumber;
            fitnessCenter.EmailAddress = model.fitnessCenter.EmailAddress;
            fitnessCenter.Street = model.fitnessCenter.Street;
            fitnessCenter.Number = model.fitnessCenter.Number;
            fitnessCenter.City = model.fitnessCenter.City;
            fitnessCenter.ZipCode = model.fitnessCenter.ZipCode;

            await _context.SaveChangesAsync();
            return RedirectToAction("EditFitnessCenter", "FitnessCenter", new { id = model.fitnessCenter.Id });
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFitnessCenters()
        {
            var fitnessCenterViewModels = new List<FitnessCenterViewModel>();

            /*fitnessCenter variable is going to have all fitness centers except "Default" fitness center
             which is used as a default fitness center when we create coaches, and it cannot be deleted, updated etc. */
            var fitnessCenters = await _context.FitnessCenter.Where(x => x.Title != "Default").ToListAsync();

            foreach (var fitnessCenter in fitnessCenters)
            {
                fitnessCenterViewModels.Add(FitnessCenterConversions.ConvertToFintessCenterViewModel(fitnessCenter));
            }
            return View(fitnessCenterViewModels);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteFitnessCenter(int id)
        {
            
            var fitnessCenter = await _context.FitnessCenter.FirstOrDefaultAsync(x => x.Id == id);
            if (fitnessCenter == null)
            {
                // if we got here, something failed
                return View("Error");
            }

            // We must delete rows from FitnessCenterHall and Hall tables as well
            var fitnessCenterHalls = await _context.FitnessCenterHall.Where(x => x.FitnessCenterId == id).ToListAsync();
            var halls = await _context.Hall.Where(x => x.FitnessCenterId == id).ToListAsync();

            _context.FitnessCenterHall.RemoveRange(fitnessCenterHalls);
            _context.Hall.RemoveRange(halls);
            _context.FitnessCenter.Remove(fitnessCenter);
            await _context.SaveChangesAsync();
            return RedirectToAction("GetAllFitnessCenters", "FitnessCenter");
        }
    }
}
