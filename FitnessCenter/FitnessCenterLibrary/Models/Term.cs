using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessCenterLibrary.Models
{
    public class Term
    {
        [Display(Name = "Free space")]
        public int FreeSpace { get; set; }

        //navigation properties
        public string CoachId { get; set; } //string because IdentityUser id is string type
        public Coach Coach { get; set; }
        public int WorkoutId { get; set; }
        public Workout Workout { get; set; }
        public ICollection<Schedule> Schedules { get; set; }

    }
}
