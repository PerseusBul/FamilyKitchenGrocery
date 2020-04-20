using System.ComponentModel.DataAnnotations;

namespace FamilyKitchen.Web.ViewModels.Orders
{
    public class OrderProfileInputModel
    {
        [Required]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        [RegularExpression("^[A-Z]+[a-zA-Z]*", ErrorMessage = "The {0} must be only from latin letters.")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        [RegularExpression("^[A-Z]+[a-zA-Z]*", ErrorMessage = "The {0} must be only from latin letters.")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Phone]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        [RegularExpression("^[A-Z]+[a-zA-Z]*", ErrorMessage = "The {0} must be only from latin letters.")]
        public string Country { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        [RegularExpression("^[A-Z]+[a-zA-Z]*", ErrorMessage = "The {0} must be only from latin letters.")]
        public string City { get; set; }

        [Display(Name = "Postcode / ZIP*")]
        public string Postcode { get; set; }

        [Required]
        [Display(Name = "Street...N...")]
        public string Street { get; set; }

        [Display(Name = "Apartment, suite, unit...")]
        public string HomeUnit { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name ="Email Adrress")]
        public string Email { get; set; }
    }
}
