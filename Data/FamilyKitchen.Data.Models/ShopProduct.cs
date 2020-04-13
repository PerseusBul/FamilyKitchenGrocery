namespace FamilyKitchen.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class ShopProduct : BaseProduct
    {
        public ShopProduct()
        {
            this.IsDeleted = false;
            this.CreatedOn = DateTime.UtcNow;
           this.ShoppingCartsShopProducts = new HashSet<ShoppingCartShopProduct>();

            //this.ExpireDate = this.CreatedOn.AddMonths(2);

           this.ShopProductsSubCategories = new HashSet<ShopProductSubCategory>();
        }

        public string TradeMark { get; set; }

        [Range(0, 60)]
        public decimal Discount { get; set; }

        public int? RecipeId { get; set; }

        // TODO change name to content via attribute
        public Recipe Recipe { get; set; }

        public int? NutritionDeclarationId { get; set; }

        public NutritionDeclaration NutritionDeclaration { get; set; }

        public virtual IEnumerable<ShoppingCartShopProduct> ShoppingCartsShopProducts { get; set; }

       public virtual IEnumerable<ShopProductSubCategory> ShopProductsSubCategories { get; set; }
    }
}
