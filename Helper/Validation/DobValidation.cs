using System.ComponentModel.DataAnnotations;

namespace MedicalEquipmentWeb.Helper.Validation
{
    public class DobValidation : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null) return ValidationResult.Success;
            try
            {
                DateTime dob = Convert.ToDateTime(value);
                if (dob >= DateTime.Now)
                {
                    return new ValidationResult("Dob must be less than current date");
                }
                return ValidationResult.Success;

            }
            catch 
            {
                return new ValidationResult("Invalid input");
            }
        
        }
    }
}
