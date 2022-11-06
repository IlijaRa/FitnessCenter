using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessCenterLibrary.Models
{
    public class Workout
    {
        public int Id { get; set; }


        [DataType(DataType.Text)]
        [Required(ErrorMessage = "You need to provide a title.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "You need to provide a title between 3-50 characters.")]
        public string Title { get; set; }


        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "You need to provide a description.")]
        [StringLength(4000, MinimumLength = 1, ErrorMessage = "You need to provide a description between 1-4000 characters.")]
        public string Description { get; set; }


        [Required(ErrorMessage = "You need to provide a type of training.")]
        public Enums.WorkoutType Type { get; set; }


        [Display(Name = "Start time")]
        [DataType(DataType.DateTime)]
        [Required(ErrorMessage = "You need to provide start time")]
        public DateTime StartTime { get; set; }


        [Display(Name = "End time")]
        [DataType(DataType.DateTime)]
        [Required(ErrorMessage = "You need to provide end time")]
        public DateTime EndTime { get; set; }


        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "You need to provide a price.")]
        public double Price { get; set; }


        [Required(ErrorMessage = "You need to provide a capacity.")]
        public int Capacity { get; set; }

        //navigation properties
        public ICollection<FitnessMemberWorkout> FitnessMemberWorkouts { get; set; }
        public string CoachId { get; set; } //string because IdentityUser id is string type
        public Coach Coach { get; set; }
        public Term Term { get; set; }
    }
}