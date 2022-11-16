using System.ComponentModel.DataAnnotations;

namespace FitnessCenterMVC.Models
{
    public class FitnessCenterMemberViewModel : UserViewModel
    {
        [DataType(DataType.DateTime)]
        [Required]
        public DateTime FirstMembership { get; set; }
    }
}
