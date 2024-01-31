using MedicalEquipmentWeb.Data;
using MedicalEquipmentWeb.Models;
using MedicalEquipmentWeb.Services.Model;
using MedicalEquipmentWeb.ViewModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Security.Policy;

namespace MedicalEquipmentWeb.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<User> _signInManager;

        public AuthenticationService(ApplicationDbContext dbContext, UserManager<User> userManager, RoleManager<IdentityRole> roleManager, SignInManager<User> signInManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }    

        public async Task<string> Login(Login model)
        {
            User userExist = _dbContext.Users.FirstOrDefault(u => u.Email.ToUpper().Equals(model.Email.ToUpper()));
            if (userExist == null)
            {
                return "Email does not match any account";
                
            }
            if (!userExist.IsActivated) return "Account has been disabled";
            var passwordChecker = await _userManager.CheckPasswordAsync(userExist, model.Password);
            if (!passwordChecker)
            {
                return "Invalid password";
                
            }

            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, true, true);
            if (!result.Succeeded)
            {
                return result.ToString();
            }
            //var role = await _userManager.GetRolesAsync(userExist);
            //await _userManager.AddClaimAsync(userExist, new Claim(ClaimTypes.Role, role.First()));
            //await _userManager.AddClaimAsync(userExist, new Claim(ClaimTypes.Email, userExist.Email));
            return "Success";
        }

        public async Task<ServiceResponse<User>> SignUp(SignUp model)
        {
            User userExist = await _userManager.FindByEmailAsync(model.Email);
            if (userExist != null) return new ServiceResponse<User>(false, "The email already exist");
            
            User newUser = new User()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.Email,
                Email = model.Email,
                IsActivated = true
            };

            var addUserResult = await _userManager.CreateAsync(newUser, model.Password);
            if (!addUserResult.Succeeded) return new ServiceResponse<User>(false, addUserResult.ToString());
            var addRoleResult = _userManager.AddToRolesAsync(newUser, new[] { "USER" });
            if (!addUserResult.Succeeded) return new ServiceResponse<User>(true, "An error occurs when adding role to new account");
            return new ServiceResponse<User>(true,"Create new account successfull", newUser);
        }

        
        public async Task<ServiceResponse<string>> VerifyEmail(string token, string email)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Email.ToUpper().Equals(email.ToUpper()));
            if (user == null) return new ServiceResponse<string>(false,$"No account matchs with {email}");
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (!result.Succeeded) return new ServiceResponse<string>(false, "Invalid token");
            return  new ServiceResponse<string>(true);
        }

        public Task<bool> VerifyPasswordPhoneNumber()
        {
            throw new NotImplementedException();
        }

        public async  Task<ServiceResponse<string>> HandleGoogleCallback()
        {
            ExternalLoginInfo externalUserInfor = null;
            var externalEmail = "";
            User userWithExternalMail = null;
            try
            {
                externalUserInfor = await _signInManager.GetExternalLoginInfoAsync();
                externalEmail = externalUserInfor.Principal.FindFirstValue(ClaimTypes.Email);
                userWithExternalMail = _dbContext.Users.FirstOrDefault(u => u.Email.ToUpper().Equals(externalEmail.ToUpper()));
            }
            catch 
            {
                return new ServiceResponse<string>(false, "An error occurs during login");
            }

            try
            {
                //truong hop da co tai khoan trong  db
                if (userWithExternalMail != null)
                {
                    //confirm luon email
                    if (!userWithExternalMail.EmailConfirmed)
                    {
                        var token = await _userManager.GenerateEmailConfirmationTokenAsync(userWithExternalMail);
                        await _userManager.ConfirmEmailAsync(userWithExternalMail, token);
                    }
                    // Thực hiện liên kết info và user

                    var addResult = await _userManager.AddLoginAsync(userWithExternalMail, externalUserInfor);
                    if (addResult.Succeeded || addResult.ToString().Equals("Failed : LoginAlreadyAssociated"))
                    {
                        // Thực hiện login    
                        if (!userWithExternalMail.IsActivated) new ServiceResponse<string>(false, "Account has been disabled");

                        await _signInManager.SignInAsync(userWithExternalMail, isPersistent: false);
                        
                        //return the token
                        return new ServiceResponse<string>(true);
                    }
                    else
                    {
                        return new ServiceResponse<string>(false, addResult.ToString());
                    }
                }
                //truong hop chua co tai khoan trong db
                var newUser = new User()
                {
                    UserName = externalEmail,
                    FirstName = externalUserInfor.Principal.FindFirstValue(ClaimTypes.GivenName),
                    LastName = externalUserInfor.Principal.FindFirstValue(ClaimTypes.Surname),              
                    Email = externalEmail,
                    EmailConfirmed = true,                         
                };
                var result = await _userManager.CreateAsync(newUser);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(newUser, "USER");
                    result = await _userManager.AddLoginAsync(newUser, externalUserInfor);
                    await _signInManager.SignInAsync(newUser, isPersistent: false, externalUserInfor.LoginProvider);
                }
                return new ServiceResponse<string>(true);
            }
            catch 
            {
                return new ServiceResponse<string>(false, "Can not add login with external account");
            }
            return new ServiceResponse<string>(true);
        }

        public async Task<ServiceResponse<string>> ResetPassword(ResetPassword model)
        {
            User userExist = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email.ToUpper().Equals(model.Email.ToUpper()));
            if (userExist == null) return new ServiceResponse<string>(false, "The email does not match any account");
            var result = await _userManager.ResetPasswordAsync(userExist, model.Token, model.NewPassword);
            if (!result.Succeeded)
            {
                return new ServiceResponse<string>(false, "Invalid token");
            }
            return new ServiceResponse<string>(true);
        }
    }
}
