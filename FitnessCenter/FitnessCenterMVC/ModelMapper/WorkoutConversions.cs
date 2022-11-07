using FitnessCenterLibrary.Models;
using FitnessCenterMVC.Models;

namespace FitnessCenterMVC.ModelMapper
{
    public class WorkoutConversions
    {
        public static Workout ConvertToWorkout(WorkoutViewModel model)
        {
            var workout = new Workout();

            //workout.Id = model.Id; <-- Id is generated in a database
            workout.Title = model.Title;
            workout.Description = model.Description;
            workout.StartTime = model.StartTime;
            workout.EndTime = model.EndTime;
            workout.Price = model.Price;
            workout.Capacity = model.Capacity;
            workout.CoachId = model.CoachId;

            if (model.Type.Equals("Conditional"))
            {
                workout.Type = Enums.WorkoutType.Conditional;
            }
            else if (model.Type.Equals("PowerLifting"))
            {
                workout.Type = Enums.WorkoutType.PowerLifting;
            }
            else
            {
                workout.Type = Enums.WorkoutType.Bodybuilding;
            }

            return workout;
        }

        public static WorkoutViewModel ConvertToWorkoutViewModel(Workout model)
        {
            var workoutViewModel = new WorkoutViewModel();

            workoutViewModel.Id = model.Id;
            workoutViewModel.Title = model.Title;
            workoutViewModel.Description = model.Description;
            workoutViewModel.StartTime = model.StartTime;
            workoutViewModel.EndTime = model.EndTime;
            workoutViewModel.Price = model.Price;
            workoutViewModel.Capacity = model.Capacity;
            workoutViewModel.CoachId = model.CoachId;

            if (model.Type == Enums.WorkoutType.Conditional)
            {
                workoutViewModel.Type = Enums.WorkoutType.Conditional.ToString();
            }
            else if (model.Type == Enums.WorkoutType.PowerLifting)
            {
                workoutViewModel.Type = Enums.WorkoutType.PowerLifting.ToString();
            }
            else if (model.Type == Enums.WorkoutType.Bodybuilding)
            {
                workoutViewModel.Type = Enums.WorkoutType.Bodybuilding.ToString();
            }

            return workoutViewModel;
        }

        public static WorkoutIndexViewModel ConvertToWorkoutIndexViewModel(Workout model)
        {
            var workoutViewModel = new WorkoutIndexViewModel();

            workoutViewModel.Id = model.Id;
            workoutViewModel.Title = model.Title;
            workoutViewModel.Description = model.Description;
            workoutViewModel.StartTime = model.StartTime;
            workoutViewModel.EndTime = model.EndTime;
            workoutViewModel.Price = model.Price;
            workoutViewModel.Capacity = model.Capacity;
            workoutViewModel.CoachId = model.CoachId;
            workoutViewModel.SearchCriteria = "";
            if (model.Type == Enums.WorkoutType.Conditional)
            {
                workoutViewModel.Type = Enums.WorkoutType.Conditional.ToString();
            }
            else if (model.Type == Enums.WorkoutType.PowerLifting)
            {
                workoutViewModel.Type = Enums.WorkoutType.PowerLifting.ToString();
            }
            else if (model.Type == Enums.WorkoutType.Bodybuilding)
            {
                workoutViewModel.Type = Enums.WorkoutType.Bodybuilding.ToString();
            }

            return workoutViewModel;
        }
    }
}
