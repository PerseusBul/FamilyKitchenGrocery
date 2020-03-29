namespace AspNetCoreTemplate.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Allergen
    {
        //public Allergen()
        //{
        //    this.FoodResourcesAllergens = new HashSet<FoodResourceAllergen>();
        //}

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        //public ICollection<FoodResourceAllergen> FoodResourcesAllergens { get; set; }
    }
}
