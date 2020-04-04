namespace FamilyKitchen.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using FamilyKitchen.Data.Common.Models;

    public class Category : BaseDeletableModel<int>
    {
        public Category()
        {
            this.IsDeleted = false;
            this.CreatedOn = DateTime.UtcNow;
            this.SubCategories = new HashSet<SubCategory>();
        }

        [Required]
        public string Name { get; set; }

        public IEnumerable<SubCategory> SubCategories { get; set; }
    }
}