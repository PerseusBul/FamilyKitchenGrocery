namespace FamilyKitchen.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using FamilyKitchen.Data.Common.Models;
    using FamilyKitchen.Data.Models.Enums;

    public class BaseProduct : BaseDeletableModel<int>
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        public int Availability { get; set; }

        public bool Available => this.Availability > 0;

        //[RegularExpression("[0-9]{8,13}")]
        public ulong? EANCode { get; set; }

        public DateTime? ExpireDate { get; set; }

        public bool IsExpired => DateTime.UtcNow > this.ExpireDate;

        public MetricSystemUnit MetricSystemUnit { get; set; }

        public string ImageUrl => $"/images/product_{this.Id}.jpg";

        public string Producer { get; set; }
    }
}
