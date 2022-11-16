using FitnessCenterLibrary.Models;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace FitnessCenterMVC.Models
{
    public class UserViewModel
    {
        public string Id { get; set; }


        [Display(Name = "User name")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "You need to provide an user name.")]
        [StringLength(256, MinimumLength = 2, ErrorMessage = "You need to provide an user name between 2-256 characters.")]
        public string UserName { get; set; }


        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "You need to provide password.")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        public string Password { get; set; }


        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }


        [DataType(DataType.Text)]
        [Required(ErrorMessage = "You need to provide a name.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "You need to provide a name between 2-50 characters.")]
        public string Name { get; set; }


        [DataType(DataType.Text)]
        [Required(ErrorMessage = "You need to provide a surname.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "You need to provide a surname between 2-50 characters.")]
        public string Surname { get; set; }


        [Display(Name = "Phone number")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }


        [Display(Name = "Email address")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "You need to provide an email address.")]
        [StringLength(256, MinimumLength = 2, ErrorMessage = "You need to provide an email address between 2-256 characters.")]
        public string Email { get; set; }


        [Display(Name = "Birthday")]
        [DataType(DataType.DateTime)]
        [Required(ErrorMessage = "You need to provide a date of birth.")]
        public DateTime DateOfBirth { get; set; }


        [Display(Name = "Is account active?")]
        [Required]
        public bool IsActive { get; set; }
    }

    public class AllUsersViewModel
    {
        public List<CoachViewModel> coaches { get; set; } = new List<CoachViewModel>();
        public List<FitnessCenterMemberViewModel> members { get; set; } = new List<FitnessCenterMemberViewModel>();
        public List<AdministratorViewModel> administrators { get; set; } = new List<AdministratorViewModel>();
    }
}
