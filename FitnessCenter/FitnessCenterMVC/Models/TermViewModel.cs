using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace FitnessCenterMVC.Models
{
    public class TermViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int WorkoutId { get; set; }
        
        [Required]
        public string CoachId { get; set; }

        [Display(Name = "Free space")]
        public int FreeSpace { get; set; }
    }
}
