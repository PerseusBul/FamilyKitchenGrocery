namespace FamilyKitchen.Web.ViewModels.Families
{
    using System.ComponentModel.DataAnnotations;

    public class FamilyBaseInputModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Family Member Username")]
        public string FamilyHead { get; set; }

        [Required]
        [Display(Name = "Family Nickname")]
        public string FamilyNickname { get; set; }
    }
}
