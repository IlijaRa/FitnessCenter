using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessCenterLibrary.Models
{
    public class FitnessCenterMember : User
    {
        [DataType(DataType.DateTime)]
        [Required]
        public DateTime FirstMembership { get; set; }

        //navigation properties
        public ICollection<FitnessMemberWorkout> FitnessMemberWorkouts { get; set; }
    }
}