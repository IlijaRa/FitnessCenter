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
                // if we get here, something is wrong
                return View("Error");
            }

            var fitnessCenter = FitnessCenterConversions.ConvertToFintessCenter(model);
            await _context.FitnessCenter.AddAsync(fitnessCenter);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> EditFitnessCenter(int id)
        {
            var fitnessCenter = await _context.FitnessCenter.FirstOrDefaultAsync(x => x.Id == id);
            if (fitnessCenter == null)
            {
                // if we get here, something is wrong
                return View("Error");
            }

            var fitnessCenterViewModel = FitnessCenterConversions.ConvertToFintessCenterViewModel(fitnessCenter);
            return View(fitnessCenterViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditFitnessCenter(FitnessCenterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // if we get here, something is wrong
                return View("Error");
            }

            var fitnessCenter = await _context.FitnessCenter.FirstOrDefaultAsync(x => x.Id == model.Id);

            fitnessCenter.Title = model.Title;
            fitnessCenter.PhoneNumber = model.PhoneNumber;
            fitnessCenter.EmailAddress = model.EmailAddress;
            fitnessCenter.Street = model.Street;
            fitnessCenter.Number = model.Number;
            fitnessCenter.City = model.City;
            fitnessCenter.ZipCode = model.ZipCode;

            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFitnessCenters()
        {
            var fitnessCenterViewModels = new List<FitnessCenterViewModel>();
            var fitnessCenters = await _context.FitnessCenter.ToListAsync();

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
            if(fitnessCenter == null)
            {
                // if we get here, something is wrong
                return View("Error");
            }

            _context.FitnessCenter.Remove(fitnessCenter);
            await _context.SaveChangesAsync();
            return RedirectToAction("GetAllFitnessCenters", "FitnessCenter");
        }
    }
}
