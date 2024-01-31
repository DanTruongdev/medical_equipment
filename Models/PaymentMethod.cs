using System.ComponentModel.DataAnnotations;

namespace MedicalEquipmentWeb.Models
{
    public class PaymentMethod
    {
        [Key]
        public int PaymentMethodId { get; set; }
        public string PaymentMethodName { get; set; }
        //
        public virtual ICollection<Order>? Orders { get; set; }
    }
}
