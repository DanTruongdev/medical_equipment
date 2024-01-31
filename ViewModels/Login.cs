using System.ComponentModel.DataAnnotations;

namespace MedicalEquipmentWeb.ViewModels
{
    public class Login
    {
        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [StringLength(40, MinimumLength = 6)]
        public string? Password { get; set; }
    }
}
