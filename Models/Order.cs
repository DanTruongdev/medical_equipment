using System.ComponentModel.DataAnnotations;

namespace MedicalEquipmentWeb.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public int PaymentMethodId { get; set; }
        [Required]
        [StringLength(500, MinimumLength = 2, ErrorMessage = "Delivery address must contain between 2 and 500 characters")]
        public string DeliveryAddress { get; set; }
        [Required]
        [StringLength(12, MinimumLength = 9, ErrorMessage = "Delivery phone number must contain between 9 and 12 characters")]
        public string DeliveryPhoneNumber { get; set; }
        [Required]
        public DateTime OrderDate { get; set; }
        [Required]
        public string Status { get; set; }
        public double? Total { get; set; }
        //
        public virtual User User { get; set; }
        public virtual PaymentMethod PaymentMethod { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }

    }
}
