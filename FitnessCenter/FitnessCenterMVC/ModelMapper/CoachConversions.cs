using FitnessCenterLibrary.Models;
using FitnessCenterMVC.Models;

namespace FitnessCenterMVC.ModelMapper
{
    public class CoachConversions
    {
        public static Coach ConvertToCoach(CoachViewModel model)
        {
            var coach = new Coach();

            //coach.Id = model.Id; <-- Id is generated in a database
            coach.UserName = model.UserName;
            coach.Name = model.Name;
            coach.Surname = model.Surname;
            coach.PhoneNumber = model.PhoneNumber;
            coach.Email = model.Email;
            coach.DateOfBirth = model.DateOfBirth;
            coach.IsActive = model.IsActive;
            coach.Rating = model.Rating;

            return coach;
        }

        public static CoachViewModel ConvertToCoachViewModel(Coach model)
        {
            var coachViewModel = new CoachViewModel();

            coachViewModel.Id = model.Id;
            coachViewModel.UserName = model.UserName;
            coachViewModel.Name = model.Name;
            coachViewModel.Surname = model.Surname;
            coachViewModel.PhoneNumber = model.PhoneNumber;
            coachViewModel.Email = model.Email;
            coachViewModel.DateOfBirth = model.DateOfBirth;
            coachViewModel.IsActive = model.IsActive;
            coachViewModel.Rating = model.Rating;
            coachViewModel.FitnessCenterId = model.FitnessCenterId;

            return coachViewModel;
        }
    }
}
