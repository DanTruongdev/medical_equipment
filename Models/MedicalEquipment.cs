using System.ComponentModel.DataAnnotations;

namespace MedicalEquipmentWeb.Models
{
    public class MedicalEquipment
    {
        [Key]
        public int MedicalEquipmentId { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Medical equipment name  must be greater than 2 and less than 50 characters")]
        public string MedicalEquipmentName { get; set; }
        [Required]
        public string CategoryId { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public string Status { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Sold must be greater than  or equal to 0")]
        public int? Sold { get; set; }
        [Required]
        public double UnlockFee { get; set; }
        [Required]
        [StringLength(400, MinimumLength = 2, ErrorMessage = "Medical equipment description  must be greater than 2 and less than 400 characters")]
        public string? Description { get; set; }
        [Required]
        [StringLength(200, MinimumLength = 2, ErrorMessage = "Sale address must be greater than 2 and less than 200 characters")]
        public string SaleAddress { get; set; }
        [Required]
        public DateTime CreationDate { get; set; }
        [Required]
        public DateTime LastModifiedDate { get; set;}
        //
        public virtual User User { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<Attachment> Attachments { get; set; }
        public virtual ICollection<Feedback> Feedbacks { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
        public virtual ICollection<UnlockedMedicalEquipment> UnlockedMedicalEquipments { get; set; }
        public virtual ICollection<Cart> Carts { get; set; }
        




    }
}
