using System.ComponentModel.DataAnnotations;

namespace FamilyKitchen.Web.ViewModels.ClientCards
{
    public class ShippingTaxInputModel
    {
        [Required]
        public string City { get; set; }

        [Required]
        [RegularExpression("[a-zA-Z]+", ErrorMessage = "The {0} must be only from latin letters. First letter has to be UpperCase!")]
        public string Block { get; set; }
    }
}
