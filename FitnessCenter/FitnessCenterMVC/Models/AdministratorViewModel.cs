using System.ComponentModel.DataAnnotations;

namespace FitnessCenterMVC.Models
{
    public class AdministratorViewModel : UserViewModel
    {
        [DataType(DataType.DateTime)]
        [Required(ErrorMessage = "You need to provide an employment day.")]
        public DateTime EmploymentDay { get; set; }
    }
}
