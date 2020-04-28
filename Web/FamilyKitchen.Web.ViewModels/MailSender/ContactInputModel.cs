namespace FamilyKitchen.Web.ViewModels.MailSender
{
    using System.ComponentModel.DataAnnotations;

    public class ContactInputModel
    {
        [Required]
        [StringLength(25, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 4)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string Subject { get; set; }

        [Required]
        public string Message { get; set; }
    }
}
