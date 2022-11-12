using Microsoft.Build.Framework;

namespace FitnessCenterMVC.Models
{
    public class CoachViewModel : UserViewModel
    {
        [Required]
        public double Rating { get; set; }

        public int FitnessCenterId { get; set; }
    }
}
