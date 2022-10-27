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
        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "You need to provide a price.")]
        public double Price { get; set; }


        [Display(Name = "Number of members")]
        [Required(ErrorMessage = "You need to provide a number of members.")]
        public int NumberOfMembers { get; set; }

        //navigation properties
        //composite key
        public string CoachId { get; set; } //string because IdentityUser id is string type
        public Coach Coach { get; set; }
        public int WorkoutId { get; set; }
        public Workout Workout { get; set; }
        public ICollection<Schedule> Schedules { get; set; }

    }
}
