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


        public double Rate { get; set; }

        //navigation properties
        //composite key
        public string FitnessCenterMemberId { get; set; } //string because IdentityUser id is string type
        public FitnessCenterMember FitnessCenterMember { get; set; }
       
        public int WorkoutId { get; set; }
        public Workout Workout { get; set; }

        
    }
}
