using MedicalEquipmentWeb.Models;
using MedicalEquipmentWeb.Services.Model;
using MedicalEquipmentWeb.ViewModels;

namespace MedicalEquipmentWeb.Services
{
    public interface IAuthenticationService
    {
        public Task<string> Login(Login model);
        public Task<ServiceResponse<User>> SignUp(SignUp model);
        public Task<ServiceResponse<string>> VerifyEmail(string token, string email);
        public Task<ServiceResponse<string>> HandleGoogleCallback();
        public Task<ServiceResponse<string>> ResetPassword(ResetPassword model);
    
        
    }
}
