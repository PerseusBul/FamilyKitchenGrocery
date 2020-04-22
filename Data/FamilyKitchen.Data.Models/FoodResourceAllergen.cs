namespace FamilyKitchen.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class FoodResourceAllergen
    {
        [Required]
        public int FoodResourceId { get; set; }

        public virtual FoodResource FoodResource { get; set; }

        [Required]
        public int AllergenId { get; set; }

        public virtual Allergen Allergen { get; set; }
    }
}
