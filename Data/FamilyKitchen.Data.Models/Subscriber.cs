namespace FamilyKitchen.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using FamilyKitchen.Data.Common.Models;

    public class Subscriber : BaseDeletableModel<string>
    {
        public Subscriber()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Email { get; set; }

        public string Username { get; set; }

        public DateTime? SubscriptionTime { get; set; }
    }
}
