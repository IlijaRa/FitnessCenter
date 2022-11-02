using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace FitnessCenterMVC.Models
{
    public class FitnessCenterViewModel
    {
        public int Id { get; set; }


        [DataType(DataType.Text)]
        [Required(ErrorMessage = "You need to provide a title.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "You need to provide a title between 3-50 characters.")]
        public string Title { get; set; }


        [Display(Name = "Phone number")]
        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "You need to provide a phone number.")]
        [MaxLength(11, ErrorMessage = "Phone number cannot be longer than 11 numbers.")]
        public string PhoneNumber { get; set; }


        [Display(Name = "Email address")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "You need to provide an email address.")]
        public string EmailAddress { get; set; }


        [DataType(DataType.Text)]
        [Required(ErrorMessage = "You need to provide a street name.")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "You need to provide a street name between 1-50 characters.")]
        public string Street { get; set; }


        [DataType(DataType.Text)]
        [Required(ErrorMessage = "You need to provide a street number.")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "You need to provide a street number between 1-50 characters.")]
        public string Number { get; set; }


        [DataType(DataType.Text)]
        [Required(ErrorMessage = "You need to provide a city.")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "You need to provide a city between 1-50 characters.")]
        public string City { get; set; }


        [DataType(DataType.PostalCode)]
        [Required(ErrorMessage = "You need to provide postal code.")]
        public string ZipCode { get; set; }
    }
}
