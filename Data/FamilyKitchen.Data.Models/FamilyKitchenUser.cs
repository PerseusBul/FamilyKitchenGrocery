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
            this.FamilyKitchenUsersShoppingCarts = new HashSet<FamilyKitchenUserShoppingCart>();
            this.FamilyKitchenUsersFavoriteProducts = new HashSet<FamilyKitchenUserFavoriteProduct>();
        }

        // Audit info
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public string FamilyId { get; set; }

        public Family Family { get; set; }

        public virtual IEnumerable<FamilyKitchenUserShoppingCart> FamilyKitchenUsersShoppingCarts { get; set; }

        public virtual IEnumerable<FamilyKitchenUserFavoriteProduct> FamilyKitchenUsersFavoriteProducts { get; set; }

        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }

        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }
    }
}
