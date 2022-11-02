using System.ComponentModel.DataAnnotations;

namespace FitnessCenterMVC.Models
{
    public class HallViewModel
    {
        public int Id { get; set; }

        public int FitnessCenterId { get; set; }

        [Required(ErrorMessage = "You need to provide a capacity.")]
        public int Capacity { get; set; }


        [DataType(DataType.Text)]
        [Required(ErrorMessage = "You need to provide a hall mark.")]
        [MaxLength(10, ErrorMessage = "Hall mark cannot be longer than 10 characters.")]
        public string HallMark { get; set; }
    }
}
