using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessCenterLibrary.Models
{
    public class Administrator : User
    {
        [DataType(DataType.DateTime)]
        [Required(ErrorMessage = "You need to provide an employment day.")]
        public DateTime EmploymentDay { get; set; }

    }
}
