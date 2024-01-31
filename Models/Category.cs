using System.ComponentModel.DataAnnotations;

namespace MedicalEquipmentWeb.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        [Required]
        public string CategoryName { get; set; }
        //
        public virtual ICollection<MedicalEquipment> MedicalEquipments { get; set; }
    }
}
