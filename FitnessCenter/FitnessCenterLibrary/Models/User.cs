using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessCenterLibrary.Models
{
    public class User : IdentityUser
    {
        //public int Id { get; set; }

        //public string UserName { get; set; }

        //public string Password { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "You need to provide a name.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "You need to provide a name between 2-50 characters.")]
        public string Name { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "You need to provide a surname.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "You need to provide a surname between 2-50 characters.")]
        public string Surname { get; set; }

        //public string PhoneNumber { get; set; }

        //public string EmailAddress { get; set; }

        [Display(Name = "Birthday")]
        [DataType(DataType.DateTime)]
        [Required(ErrorMessage = "You need to provide a date of birth.")]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public bool IsActive { get; set; }
    }
}
