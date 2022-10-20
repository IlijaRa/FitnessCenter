using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessCenterLibrary
{
    internal class Coach : User
    {
        public List<Workout> workout_appointments { get; set; }
        public double Rating { get; set; }
    }
}
