using MedicalEquipmentWeb.Helper.Validation;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace MedicalEquipmentWeb.Models
{
    public class User : IdentityUser
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "First name must be contain between 2 and 50 characters")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Last name must be contain between 2 and 50 characters")]
        public string LastName { get; set; }

        [DobValidation]
        public DateTime? Dob { get; set; }

        public string? Avatar { get; set; }

        public string? Gender { get; set; }
        public double? TotalCoin { get; set; } 

        public string? Address { get; set; }

        public double? UnlockFee { get; set; }

        [Required]
        public bool IsActivated { get; set; }

        public override string ToString()
        {
            return FirstName + " " + LastName;
        }
        //
        // (users who unlock this user)
        public virtual ICollection<User> UnlockedUsers { get; set; }

        // users that this user unlocks
        public virtual ICollection<User> Unlocking { get; set; }

        public virtual ICollection<Transaction>? Transactions { get; set; }
        public virtual ICollection<Notification>? Notifications { get; set; }
        public virtual ICollection<UnlockedMedicalEquipment>? UnlockedMedicalEquipments { get; set; }
        public virtual ICollection<MedicalEquipment>? MedicalEquipments { get; set; }      
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Cart> Carts { get; set; }
        public virtual ICollection<Feedback> Feedbacks { get;}



    }
}
