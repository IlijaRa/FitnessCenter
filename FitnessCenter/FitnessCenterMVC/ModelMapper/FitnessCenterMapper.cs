using FitnessCenterLibrary.Models;
using FitnessCenterMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessCenterMVC.ModelMapper
{
    public class FitnessCenterMapper
    {
        public static FitnessCenter ConvertToFintessCenter(FitnessCenterViewModel model)
        {
            var fitnessCenter = new FitnessCenter();

            //fitnessCenter.Id = model.Id; <-- Id is generated in a database
            fitnessCenter.Title = model.Title;
            fitnessCenter.PhoneNumber = model.PhoneNumber;
            fitnessCenter.EmailAddress = model.EmailAddress;
            fitnessCenter.Street = model.Street;
            fitnessCenter.Number = model.Number;
            fitnessCenter.City = model.City;
            fitnessCenter.ZipCode = model.ZipCode;

            return fitnessCenter;
        }

        public static FitnessCenterViewModel ConvertToFintessCenterViewModel(FitnessCenter model)
        {
            var fitnessCenterViewModel = new FitnessCenterViewModel();

            fitnessCenterViewModel.Id = model.Id;
            fitnessCenterViewModel.Title = model.Title;
            fitnessCenterViewModel.PhoneNumber = model.PhoneNumber;
            fitnessCenterViewModel.EmailAddress = model.EmailAddress;
            fitnessCenterViewModel.Street = model.Street;
            fitnessCenterViewModel.Number = model.Number;
            fitnessCenterViewModel.City = model.City;
            fitnessCenterViewModel.ZipCode = model.ZipCode;

            return fitnessCenterViewModel;
        }
    }
}