namespace FamilyKitchen.Data.Models
{
    using System;
    using System.Collections.Generic;

    using FamilyKitchen.Data.Common.Models;

    public class Family : BaseDeletableModel<string>
    {
        public Family()
        {
            this.Id = Guid.NewGuid().ToString();
            this.FamilyMembers = new HashSet<FamilyKitchenUser>();
            this.CreatedOn = DateTime.UtcNow;
        }

        public string Name { get; set; }

        public string Moderator { get; set; }

        public virtual IEnumerable<FamilyKitchenUser> FamilyMembers { get; set; }
    }
}
