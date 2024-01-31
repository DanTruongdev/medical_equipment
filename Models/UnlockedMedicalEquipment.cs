using System.ComponentModel.DataAnnotations;

namespace MedicalEquipmentWeb.Models
{
    public class UnlockedMedicalEquipment
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public int MedicalEquipmentId { get; set; }
        //
        public virtual User User { get; set; }
        public virtual MedicalEquipment MedicalEquipment { get; set; }
    }
}
