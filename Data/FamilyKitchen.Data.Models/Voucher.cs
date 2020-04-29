namespace FamilyKitchen.Data.Models
{
    using System;

    using FamilyKitchen.Data.Common.Models;

    public class Voucher : BaseDeletableModel<string>
    {
        public Voucher()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string NumberCode { get; set; }

        public int Amount { get; set; }
    }
}
