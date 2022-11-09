using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessCenterLibrary.Models
{
    public class Coach : User
    {
        [Required]
        public double Rating { get; set; }

        //navigation properties
        public int FitnessCenterId { get; set; }
        public FitnessCenter FitnessCenter { get; set; }
        public ICollection<Workout>? Workouts { get; set; } //TODO: Check whether ? makes property nullable
        public ICollection<Term>? Terms { get; set; } //TODO: Check whether ? makes property nullable
    }
}
