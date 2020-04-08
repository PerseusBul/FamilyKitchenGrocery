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
        }

        public string Name { get; set; }

        public virtual IEnumerable<ApplicationUser> FamilyMembers { get; set; }
    }
}
