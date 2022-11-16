using FitnessCenterLibrary.Models;
using FitnessCenterMVC.Models;

namespace FitnessCenterMVC.ModelMapper
{
    public class FitnessCenterMemberMapper
    {
        public static FitnessCenterMember ConvertToFitnessCenterMember(FitnessCenterMemberViewModel model)
        {
            var fitnessCenterMember = new FitnessCenterMember();

            //fitnessCenterMember.Id = model.Id; <-- Id is generated in a database
            fitnessCenterMember.UserName = model.UserName;
            fitnessCenterMember.Name = model.Name;
            fitnessCenterMember.Surname = model.Surname;
            fitnessCenterMember.PhoneNumber = model.PhoneNumber;
            fitnessCenterMember.Email = model.Email;
            fitnessCenterMember.DateOfBirth = model.DateOfBirth;
            fitnessCenterMember.IsActive = model.IsActive;
            fitnessCenterMember.FirstMembership = model.FirstMembership;

            return fitnessCenterMember;
        }

        public static FitnessCenterMemberViewModel ConvertToCoachViewModel(FitnessCenterMember model)
        {
            var fitnessCenterMemberViewModel = new FitnessCenterMemberViewModel();

            fitnessCenterMemberViewModel.Id = model.Id;
            fitnessCenterMemberViewModel.UserName = model.UserName;
            fitnessCenterMemberViewModel.Name = model.Name;
            fitnessCenterMemberViewModel.Surname = model.Surname;
            fitnessCenterMemberViewModel.PhoneNumber = model.PhoneNumber;
            fitnessCenterMemberViewModel.Email = model.Email;
            fitnessCenterMemberViewModel.DateOfBirth = model.DateOfBirth;
            fitnessCenterMemberViewModel.IsActive = model.IsActive;
            fitnessCenterMemberViewModel.FirstMembership = model.FirstMembership;

            return fitnessCenterMemberViewModel;
        }
    }
}
