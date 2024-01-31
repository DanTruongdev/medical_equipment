using System.ComponentModel.DataAnnotations;

namespace MedicalEquipmentWeb.Models
{
    public class Notification
    {
        [Key]
        public int NotificationId { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = " Notification title must be greater than 2 and less than 50 characters")]
        public string Title { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Notification content name content must be greater than 2 and less than 100 characters")]
        public string Content { get; set; }
        [Required]
        public DateTime CreationDate { get; set; }
        //
        public virtual User User { get; set; }
    }
}
