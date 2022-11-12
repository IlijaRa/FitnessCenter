using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessCenterLibrary.Models
{
    public class Rate
    {
        public int Id { get; set; }

        [Range(1, 10, ErrorMessage = "You need to provide the rate between 1-10.")]
        public double Rating { get; set; }
    }
}