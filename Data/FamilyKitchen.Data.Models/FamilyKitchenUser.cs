// ReSharper disable VirtualMemberCallInConstructor
namespace FamilyKitchen.Data.Models
{
    using System;
    using System.Collections.Generic;

    using FamilyKitchen.Data.Common.Models;

    using Microsoft.AspNetCore.Identity;

    public class FamilyKitchenUser : IdentityUser, IAuditInfo, IDeletableEntity
    {
        public FamilyKitchenUser()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Roles = new HashSet<IdentityUserRole<string>>();
            this.Claims = new HashSet<IdentityUserClaim<string>>();
            this.Logins = new HashSet<IdentityUserLogin<string>>();
            this.FamilyKitchenUsersFavoriteProducts = new HashSet<FamilyKitchenUserFavoriteProduct>();
            this.Orders = new HashSet<Order>();
        }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public string FamilyId { get; set; }

        public Family Family { get; set; }

        public int ShoppingCartId { get; set; }

        public ShoppingCart ShoppingCart { get; set; }

        public string ClientCardId { get; set; }

        public virtual ClientCard ClientCard { get; set; }

        public virtual IEnumerable<FamilyKitchenUserFavoriteProduct> FamilyKitchenUsersFavoriteProducts { get; set; }

        public virtual IEnumerable<Order> Orders { get; set; }

        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }

        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }
    }
}
