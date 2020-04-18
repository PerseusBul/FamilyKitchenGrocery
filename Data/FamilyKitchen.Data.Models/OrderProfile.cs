namespace FamilyKitchen.Data.Models
{
    using System;
    using System.Collections.Generic;

    using FamilyKitchen.Data.Common.Models;

    public class OrderProfile : BaseDeletableModel<string>
    {
        public OrderProfile()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string Description { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public string Postcode { get; set; }

        public string Street { get; set; }

        public string BuildingNumber { get; set; }

        public string HomeUnit { get; set; }

        public string Email { get; set; }

        public virtual IEnumerable<Order> Orders { get; set; }
    }
}
