
using DemoDataAccess.Models.IdentityModel;
using DemoMVCPresentation.Utilities;
using DemoMVCPresentation.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace DemoMVCPresentation.Controllers
{
    public class AccountController(UserManager<ApplicationUser> _userManager , SignInManager<ApplicationUser> _signInManager) : Controller
    {
        #region Register

        //Register:
        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        public IActionResult Register(RegisterViewModel ViewModel)
        {
            if(!ModelState.IsValid) return View(ViewModel);
           
            var User = new ApplicationUser()
            {
                FirstName = ViewModel.FirstName,
                LastName = ViewModel.LastName,
                UserName = ViewModel.UserName,
                Email = ViewModel.Email
            };
            //CreateAsync => that work of hashing the password.
            var Result = _userManager.CreateAsync(User,ViewModel.Password).Result;
            if (Result.Succeeded)
                return RedirectToAction("Login");
            else
            {
                foreach (var error in Result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(ViewModel);
            }
        }
        #endregion

        #region Login
        //Login:
        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public IActionResult Login(LoginViewModel ViewModel)
        {
            if(!ModelState.IsValid) return View(ViewModel);
            var User = _userManager.FindByEmailAsync(ViewModel.Email).Result;
            if(User is not null)
            {
                //Check the password:
             bool Flag = _userManager.CheckPasswordAsync(User, ViewModel.Password).Result;
                if(Flag)
                {
                    var Result = _signInManager.PasswordSignInAsync(User, ViewModel.Password, ViewModel.RememberMe, false).Result;
                    if (Result.IsNotAllowed)
                        ModelState.AddModelError(string.Empty, "Your Account is Not Allowed");
                    if(Result.IsLockedOut)
                        ModelState.AddModelError(string.Empty, "Your Account is Locked Out");
                    if (Result.Succeeded)
                        return RedirectToAction(nameof(HomeController.Index), "Home");
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid Login");
            }
            return View(ViewModel);

        }
        #endregion

        #region Forget Password
        //Forget Password:
        [HttpGet]
        public IActionResult ForgetPassword() => View();

        [HttpPost]
        public IActionResult SendResetPasswordLink(ForgetPasswordViewModel viewModel)
        {
            if(ModelState.IsValid)
            {
                var User = _userManager.FindByEmailAsync(viewModel.Email).Result;
                if (User is not null)
                {
                    var Token = _userManager.GeneratePasswordResetTokenAsync(User).Result;
                    //Baseurl/Account/ResetPassword?email=User.Email&token=Token
                    var ResetPasswordLink = Url.Action("ResetPassword","Account",new {email = viewModel.Email,Token},Request.Scheme);
                    var email = new Utilities.Email()

                    {
                        //intialize the email object:
                        To = viewModel.Email,
                        Subject = "Reset Password",
                        Body = "Reset Password Link" //TODO

                    };
                    //Send the email:
                    EmailSettings.SendEmail(email);
                    return View(nameof(CkeckYourInbox));
                }
            }
            ModelState.AddModelError(string.Empty, "Invalid Operation");
            return View(nameof(ForgetPassword), viewModel);



        }


        [HttpGet]
        public IActionResult CkeckYourInbox() => View();


        [HttpGet]
        public IActionResult ResetPassword(string email , string Token)
        {
            TempData["email"] = email ;
            TempData["Token"] = Token;
            return View();
        }
        [HttpPost]
        public IActionResult ResetPassword(ResetPasswordViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);
            string email = TempData["email"] as string ?? "";
            string Token = TempData["Token"] as string ?? "";

            var USer = _userManager.FindByEmailAsync(email).Result;
            if (USer is not null)
            {
                var Result = _userManager.ResetPasswordAsync(USer, Token, viewModel.Password).Result;

                if (Result.Succeeded)
                    return RedirectToAction(nameof(Login));

                else
                {
                    foreach(var error in Result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View (nameof(ResetPassword),viewModel);

        }

        #endregion




    }
}
