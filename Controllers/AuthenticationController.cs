using MedicalEquipmentWeb.Data;
using MedicalEquipmentWeb.Models;
using MedicalEquipmentWeb.Services;
using MedicalEquipmentWeb.Services.Model;
using MedicalEquipmentWeb.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace MedicalEquipmentWeb.Controllers
{
    
    public class AuthenticationController : Controller
    {
        private readonly IAuthenticationService _authService;
        private readonly UserManager<User> _userManager;
        private readonly IEmailService _emailService;
        private readonly ApplicationDbContext _dbContext;
        private readonly SignInManager<User> _signInManager;

        public AuthenticationController(IAuthenticationService authService, UserManager<User> userManager, IEmailService emailService, ApplicationDbContext dbContext, SignInManager<User> signInManager)
        {

            _authService = authService;
            _userManager = userManager;
            _emailService = emailService;
            _dbContext = dbContext;
            _signInManager = signInManager;
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Login model)
        {
            if (!ModelState.IsValid) return View();
            string result = await _authService.Login(model);
            if (!result.Equals("Success"))
            {
                ViewBag.Error = result;
                return View();
            }
            return RedirectToAction("Index", "Home");  
        }

        [HttpGet]
        public async Task<IActionResult> SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUp model)
        {
            if (!ModelState.IsValid) return View();
            var result = await _authService.SignUp(model);
            User newUser = null;
            if (!result.Succeeded)
            {
                ViewBag.Error = result.Message;
                return View();
            }
            newUser = result.Data;
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
            var confirmationLink = Url.Action("ConfirmEmail",
               "Authentication", new { token, email = newUser.Email }, Request.Scheme);
            var message = new Message(new string[] { model.Email! },
               "Confirmation email link", $"Please click to the following Url to verify your email: \n {confirmationLink!}");
            _emailService.SendEmail(message);
            return RedirectToAction("WaitConfirmation", new { email = model.Email });
        }
        [HttpGet]
        public async Task<IActionResult> WaitConfirmation(string email)
        {
            return View("WaitConfirmation", email);
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            var result = await _authService.VerifyEmail(token, email);
            if (!result.Succeeded) return View("EmailConfirmation", result.Message);
            return RedirectToAction("Login");
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        //[HttpGet("signin-google")]
        public async Task<IActionResult> SignInGoogle()
        {
            var redirectUri = Url.Action(nameof(HandleGoogleCallback), "Authentication");
            var properties = _signInManager.ConfigureExternalAuthenticationProperties("Google", redirectUri);
            return new ChallengeResult("Google", properties);
        }

        [HttpGet]
        public async Task<IActionResult> HandleGoogleCallback()
        {
            var result = await _authService.HandleGoogleCallback();
            if (!result.Succeeded)
            {
                ViewBag.Error = result.Message;
                return View("Login");
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            User userExist = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email.ToUpper().Equals(email.ToUpper()));
            if (userExist == null) return View("ForgotPassword", "The email does not match any account");
            var token = await _userManager.GeneratePasswordResetTokenAsync(userExist);
            var resetUrl = Url.Action(nameof(ResetPassword), "Authentication", new { token, email }, Request.Scheme);
            var message = new Message(new string[] { email! },
              "Reset password link", $"Please click to the following Url to reset your password: \n {resetUrl!}");
            _emailService.SendEmail(message);
            return RedirectToAction("WaitConfirmation", new { email });
        }

        [HttpGet]
        public async Task<IActionResult> ResetPassword(string token, string email)
        {
            ViewBag.Token = token;
            ViewBag.Email = email;
            return View();            
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPassword model)
        {
            var result = await _authService.ResetPassword(model);
            if (!result.Succeeded)
            {
                ViewBag.Error = result.Message;
                return View();
            }
        
            return RedirectToAction(nameof(Login));
            
        }


    }
}
