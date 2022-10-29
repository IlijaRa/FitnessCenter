using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessCenterLibrary.Models
{
    public class User : IdentityUser
    {
        //public int Id { get; set; }               <---Uses Id from IdentityUser class

        //public string UserName { get; set; }      <---Uses UserName from IdentityUser class

        //public string Password { get; set; }      <---InputModel class from file "Register.cshtml.cs" already has Password property 

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "You need to provide a name.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "You need to provide a name between 2-50 characters.")]
        public string Name { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "You need to provide a surname.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "You need to provide a surname between 2-50 characters.")]
        public string Surname { get; set; }

        //public string PhoneNumber { get; set; }   <---Uses PhoneNumber from IdentityUser class

        //public string EmailAddress { get; set; }  <---Uses PhoneNumber from IdentityUser class

        [Display(Name = "Birthday")]
        [DataType(DataType.DateTime)]
        [Required(ErrorMessage = "You need to provide a date of birth.")]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public bool IsActive { get; set; }
    }
}
