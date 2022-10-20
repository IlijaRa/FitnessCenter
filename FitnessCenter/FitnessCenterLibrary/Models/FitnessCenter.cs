using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessCenterLibrary.Models
{
    public class FitnessCenter
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public Address Address { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public List<Coach> coaches { get; set; }
        public List<Hall> halls { get; set; }
        public List<Workout> workout_schedule { get; set; }
    }
}
