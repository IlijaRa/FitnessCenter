using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessCenterLibrary.Models
{
    public class Hall
    {
        public Guid Id { get; set; }
        public int Capacity { get; set; }
        public string HallMark { get; set; }
        public List<Workout> workout_schedule { get; set; }
    }
}
