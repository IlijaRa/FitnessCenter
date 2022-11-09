using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessCenterLibrary.Models
{
    public class Hall
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "You need to provide a capacity.")]
        public int Capacity { get; set; }


        [DataType(DataType.Text)]
        [Required(ErrorMessage = "You need to provide a hall mark.")]
        [MaxLength(10, ErrorMessage = "Hall mark cannot be longer than 10 characters.")]
        public string HallMark { get; set; }

        //navigation properties
        public int FitnessCenterId { get; set; }
        public FitnessCenter FitnessCenter { get; set; }
        public FitnessCenterHall FitnessCenterHall { get; set; }
    }
}