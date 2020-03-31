namespace FamilyKitchen.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class FoodResourceAllergen
    {
        [Required]
        public int FoodResourceId { get; set; }

        public FoodResource FoodResource { get; set; }

        [Required]
        public int AllergenId { get; set; }

        public Allergen Allergen { get; set; }
    }
}
