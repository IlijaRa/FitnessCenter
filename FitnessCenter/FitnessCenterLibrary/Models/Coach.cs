using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessCenterLibrary.Models
{
    public class Coach : User
    {
        public List<Workout> workout_appointments { get; set; } = new List<Workout>();
        public double Rating { get; set; }
    }
}
