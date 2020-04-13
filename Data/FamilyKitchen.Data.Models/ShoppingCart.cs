namespace FamilyKitchen.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using FamilyKitchen.Data.Common.Models;

    public class ShoppingCart : BaseDeletableModel<int>
    {
        public ShoppingCart()
        {
            this.IsDeleted = false;
            this.CreatedOn = DateTime.UtcNow;
            this.ShoppingCartsShopProducts = new HashSet<ShoppingCartShopProduct>();
            this.FamilyKitchenUsersShoppingCarts = new HashSet<FamilyKitchenUserShoppingCart>();
        }

        [Required]
        public decimal Total { get; set; }

        public virtual IEnumerable<ShoppingCartShopProduct> ShoppingCartsShopProducts { get; set; }

        public virtual IEnumerable<FamilyKitchenUserShoppingCart> FamilyKitchenUsersShoppingCarts { get; set; }
    }
}
