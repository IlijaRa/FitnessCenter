using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessCenterLibrary.Models
{
    public class FitnessCenterMember : User
    {
        //public List<Workout> completed_workouts { get; set; } = new List<Workout>();
        public List<Workout> all_workouts { get; set; } = new List<Workout>();
        public List<Rate> workout_rates { get; set; } = new List<Rate>();

        public List<Workout> GetCompletedWorkouts()
        {
            List<Workout> completed_workouts = new List<Workout>();
            foreach (var workout in all_workouts)
            {
                if(workout.IsCompleted == true)
                {
                    completed_workouts.Add(workout);
                }
            }
            return completed_workouts;
        }

        public List<Workout> GetUnCompletedWorkouts()
        {
            List<Workout> uncompleted_workouts = new List<Workout>();
            foreach (var workout in all_workouts)
            {
                if (workout.IsCompleted == false)
                {
                    uncompleted_workouts.Add(workout);
                }
            }
            return uncompleted_workouts;
        }
    }
}
