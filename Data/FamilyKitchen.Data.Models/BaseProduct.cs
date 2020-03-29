namespace AspNetCoreTemplate.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using AspNetCoreTemplate.Data.Models.Enums;
    using FamilyKitchen.Data.Common.Models;

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
