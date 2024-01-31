using System.ComponentModel.DataAnnotations;

namespace MedicalEquipmentWeb.Models
{
    public class Attachment
    {
        [Key]
        public int AttachmentId { get; set; }
        [Required]
        public int MedicalEquipmentId { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public string Path { get; set; }
        //
        public virtual  MedicalEquipment MedicalEquipment { get; set; }
    }
}
