namespace FamilyKitchen.Web.ViewModels.ClientCards
{
    using System.ComponentModel.DataAnnotations;

    public class VoucherInputModel
    {
        [Required]
        [RegularExpression("[0-9]{8}")]
        public string NumberCode { get; set; }
    }
}
