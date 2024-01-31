using System.ComponentModel.DataAnnotations;

namespace MedicalEquipmentWeb.Models
{
    public class Feedback
    {
        [Key]
        public int FeedbackId { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public string OrderId { get; set; }
        [Required]
        public int MedicalEquipmentId { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 10, ErrorMessage = "Feedback content must be greater than 10 and less than 50 characters")]
        public string Content { get; set; }
        [Required]
        [Range(1,5, ErrorMessage  = "Vote star must be greater than 0 and less than 6")]
        public int VoteStar { get; set; }
        //
        public virtual User User { get; set; }
        public virtual Order Order { get; set; }
        public virtual MedicalEquipment MedicalEquipment { get; set; }
    }
}
