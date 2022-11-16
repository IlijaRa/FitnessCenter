using FitnessCenterLibrary.Models;
using FitnessCenterMVC.Models;

namespace FitnessCenterMVC.ModelMapper
{
    public class AdministratorMapper
    {
        public static Administrator ConvertToAdministrator(AdministratorViewModel model)
        {
            var administrator = new Administrator();

            //administrator.Id = model.Id; <-- Id is generated in a database
            administrator.UserName = model.UserName;
            administrator.Name = model.Name;
            administrator.Surname = model.Surname;
            administrator.PhoneNumber = model.PhoneNumber;
            administrator.Email = model.Email;
            administrator.DateOfBirth = model.DateOfBirth;
            administrator.IsActive = model.IsActive;
            administrator.EmploymentDay = model.EmploymentDay;

            return administrator;
        }

        public static AdministratorViewModel ConvertToAdministratorViewModel(Administrator model)
        {
            var administratorViewModel = new AdministratorViewModel();

            administratorViewModel.Id = model.Id;
            administratorViewModel.UserName = model.UserName;
            administratorViewModel.Name = model.Name;
            administratorViewModel.Surname = model.Surname;
            administratorViewModel.PhoneNumber = model.PhoneNumber;
            administratorViewModel.Email = model.Email;
            administratorViewModel.DateOfBirth = model.DateOfBirth;
            administratorViewModel.IsActive = model.IsActive;
            administratorViewModel.EmploymentDay = model.EmploymentDay;

            return administratorViewModel;
        }
    }
}
