using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessCenterLibrary.Models
{
    public class Workout
    {
        public Guid Id { get; set; }
        public int CoachId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Enums.WorkoutType WorkoutType { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public List<FitnessCenterMember> workout_members { get; set; }
        public bool IsCompleted { get; set; }
        public double Price { get; set; }


        public int GetNumberOfMembers()
        {
            return workout_members.Count();
        }
    }
}