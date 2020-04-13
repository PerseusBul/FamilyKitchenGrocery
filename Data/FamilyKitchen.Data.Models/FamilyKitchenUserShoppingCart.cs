using FamilyKitchen.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FamilyKitchen.Data.Models
{
    public class FamilyKitchenUserShoppingCart
    {
        [Required]
        public string UserId { get; set; }

        public FamilyKitchenUser User { get; set; }

        [Required]
        public int ShoppingCartId { get; set; }

        public ShoppingCart ShoppingCart { get; set; }
    }
}
