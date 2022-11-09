using FitnessCenterLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace FitnessCenterMVC.Models
{
    public class WorkoutViewModel : IComparable<WorkoutViewModel>
    {
        public int Id { get; set; }

        [Required]
        public string CoachId { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "You need to provide a title.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "You need to provide a title between 3-50 characters.")]
        public string Title { get; set; }


        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "You need to provide a description.")]
        [StringLength(4000, MinimumLength = 1, ErrorMessage = "You need to provide a description between 1-4000 characters.")]
        public string Description { get; set; }


        [Required(ErrorMessage = "You need to provide a type of training.")]
        [BindProperty]
        public string Type { get; set; }
        public string[] Types = new[] { "Conditional", "PowerLifting", "Bodybuilding" };

        [Display(Name = "Start time")]
        [DataType(DataType.DateTime)]
        [Required(ErrorMessage = "You need to provide start time")]
        public DateTime StartTime { get; set; }


        [Display(Name = "End time")]
        [DataType(DataType.DateTime)]
        [Required(ErrorMessage = "You need to provide end time")]
        public DateTime EndTime { get; set; }


        [Display(Name = "Price ($)")]
        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "You need to provide a price.")]
        public double Price { get; set; }


        [Required(ErrorMessage = "You need to provide a capacity.")]
        public int Capacity { get; set; }

        // property used for sorting workout by price
        public int CompareTo(WorkoutViewModel other)
        {
            return this.Price.CompareTo(other.Price);
        }

    }
}