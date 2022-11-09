using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessCenterLibrary.Models
{
    public class FitnessCenterHall
    {
        //navigation properties
        public int HallId { get; set; }
        public Hall Hall { get; set; }
        public int FitnessCenterId { get; set; }
        public FitnessCenter FitnessCenter { get; set; }
        public ICollection<Schedule> Schedules { get; set; }
    }
}