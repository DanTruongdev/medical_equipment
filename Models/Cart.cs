using System.ComponentModel.DataAnnotations;

namespace MedicalEquipmentWeb.Models
{
    public class Cart
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public int MedicalEquipmentId { get; set; }
        [Required]
        [Range(0, 1000, ErrorMessage = "Quantity must be greater than or equal to 1 and less than 1000")]
        public int Quantity { get; set; }
        //
        public virtual MedicalEquipment MedicalEquipment { get; set; }
        public virtual User User { get; set; }
    }
}
