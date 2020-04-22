namespace FamilyKitchen.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class FoodResourceRecipe
    {
        [Required]
        public int FoodResourceId { get; set; }

        public virtual FoodResource FoodResource { get; set; }

        [Required]
        public int RecipeId { get; set; }

        public virtual Recipe Recipe { get; set; }

        [Required]
        public decimal Quantity { get; set; }
    }
}
