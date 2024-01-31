using System.ComponentModel.DataAnnotations;

namespace MedicalEquipmentWeb.ViewModels
{
    public class ResetPassword
    {
        [Required(ErrorMessage = "Password is required")]
        [StringLength(40, MinimumLength = 6, ErrorMessage = "Password must be contain between 6 and 40 characters")]
        public string NewPassword { get; set; }
        [Compare(nameof(NewPassword), ErrorMessage = "Confirm password does not match password")]
        public string ConfirmPassword { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Token { get; set; }
    }
}
