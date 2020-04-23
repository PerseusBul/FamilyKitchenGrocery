namespace FamilyKitchen.Web.ViewModels.Subscribers
{
    using System.ComponentModel.DataAnnotations;

    public class SubscribersInputModel
    {
        [Required]
        [EmailAddress]
        public string Subscriber { get; set; }
    }
}
