namespace FamilyKitchen.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Allergen
    {
        public Allergen()
        {
            this.FoodResourcesAllergens = new HashSet<FoodResourceAllergen>();
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public IEnumerable<FoodResourceAllergen> FoodResourcesAllergens { get; set; }
    }
}
