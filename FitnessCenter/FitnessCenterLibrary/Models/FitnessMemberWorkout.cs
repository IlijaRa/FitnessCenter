using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessCenterLibrary.Models
{
    public class FitnessMemberWorkout
    {
        [Required]
        public Enums.WorkoutState State { get; set; }

        [Range(1,10, ErrorMessage = "You need to provide the rate between 1-10.")]
        public double WorkoutRate { get; set; }

        [Range(1, 10, ErrorMessage = "You need to provide the rate between 1-10.")]
        public double CoachRate { get; set; }

        //navigation properties
        public string FitnessCenterMemberId { get; set; } // string because IdentityUser id is string type
        public FitnessCenterMember FitnessCenterMember { get; set; }
        public int WorkoutId { get; set; }
        public Workout Workout { get; set; }
    }
}
