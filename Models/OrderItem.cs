using System.ComponentModel.DataAnnotations;

namespace MedicalEquipmentWeb.Models
{
    public class OrderItem
    {
        [Key]
        public int OrderItemId { get; set; }
        [Required]
        public int OrderId { get; set; }
        [Required]
        public int MedicalEquipmentId { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public double TotalCost { get; set; }
        //
        public virtual Order Order { get; set; }
        public virtual MedicalEquipment MedicalEquipment { get; set; }
       
    }
}
