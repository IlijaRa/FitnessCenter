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
        [Required]
        public DateTime EmploymentDay { get; set; }


        public int YearsOfExperience { 
            get { return DateTime.Now.Year - EmploymentDay.Year; }
            set { }
        }

        //navigation properties
        //[Key]
        //public int UserId { get; set; }
        //public User User { get; set; }
    }
}
