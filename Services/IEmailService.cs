using MedicalEquipmentWeb.Services.Model;

namespace MedicalEquipmentWeb.Services
{
    public interface IEmailService
    {
        public bool SendEmail(Message message);
    }
}
