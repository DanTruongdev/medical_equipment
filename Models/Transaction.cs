using System.ComponentModel.DataAnnotations;

namespace MedicalEquipmentWeb.Models
{
    public class Transaction
    {
        [Key]
        public int TransactionId { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Transaction content must contain between 2 and 100 characters")]
        public string Content { get; set; }
        [Required]
        public DateTime CreationDate { get; set; }
        //
        public virtual User User { get; set; }
    }
}
