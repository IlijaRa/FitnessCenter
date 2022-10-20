using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessCenterLibrary
{
    internal class FitnessCenterMember : User
    {
        public List<Workout> completed_workouts { get; set; }
        public List<Workout> requested_workouts { get; set; }
        public List<Rate> workout_rates { get; set; }
    }
}
