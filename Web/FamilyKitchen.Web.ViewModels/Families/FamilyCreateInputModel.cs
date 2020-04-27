namespace FamilyKitchen.Web.ViewModels.Families
{
    using System.ComponentModel.DataAnnotations;

    public class FamilyCreateInputModel
    {
        [Required]
        [Display(Name = "Family Nickname")]
        [StringLength(12, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 4)]
        public string FamilyNickname { get; set; }

        [EmailAddress]
        [Display(Name = "Family Member Username")]
        public string FamilyMemberSecond { get; set; }

        [EmailAddress]
        [Display(Name = "Family Member Username")]
        public string FamilyMemberThird { get; set; }

        [EmailAddress]
        [Display(Name = "Family Member Username")]
        public string FamilyMemberFourth { get; set; }
    }
}
