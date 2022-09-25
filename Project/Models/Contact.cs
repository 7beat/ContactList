using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Project.Models
{
    [Index(nameof(Email), IsUnique = true)]
    //[Index(nameof(Phone), IsUnique = true)]
    public class Contact
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name field is required!")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Surname field is required!")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email field is required!")]
        [StringLength(maximumLength: 100, MinimumLength = 2)]
        [EmailAddress]//ErrorMessage?
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone Number field is required!")]
        [StringLength(maximumLength: 15, MinimumLength = 9)]
        public string Phone { get; set; }

        [Required(ErrorMessage = "BirthDay is required!")]
        [DataType(DataType.Date)] //Specifying it as Date
        public DateTime BirthDay { get; set; }

        //public enum Category
        //{
        //    służbowy,
        //    prywatny,
        //    inny
        //}
    }
}
