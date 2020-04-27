namespace FamilyKitchen.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using FamilyKitchen.Data.Common.Models;

    public class ClientCard : BaseDeletableModel<string>
    {
        public ClientCard()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CreatedOn = DateTime.UtcNow;
        }

        [Range(0, 40)]
        public decimal Discount { get; set; }

        public int Points { get; set; }

        public string FamilyKitchenUserId { get; set; }

        public virtual FamilyKitchenUser FamilyKitchenUser { get; set; }
    }
}
