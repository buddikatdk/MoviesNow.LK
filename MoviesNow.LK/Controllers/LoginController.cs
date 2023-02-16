using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MoviesNow.LK.Data;
using MoviesNow.LK.Data.Entity;
using MoviesNow.LK.Data.ModalViews;
using MoviesNow.LK.Services;
using System.Text.RegularExpressions;
using System.Web;

namespace MoviesNow.LK.Controllers
{
    public class LoginController:BaseController
    {
        private readonly DataContext _db;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public LoginController(DataContext db, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _db = db;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult> Register(Register user)
        {
            if(user == null)
            {
                return NotFound();
            }
            if(ModelState.IsValid)
            {
                if (CheckExistEmail(user.Email))
                {
                    return BadRequest("Email alrealy registered in system..!");
                }
                if(!IsEmailValidation(user.Email))
                {
                    return BadRequest("Email not in a correct format..!");
                }
                if (CheckExistUsername(user.UserName))
                {
                    return BadRequest("Username alrealy be taken..");
                }

                var nuser = new User
                {
                    Email = user.Email,
                    UserName = user.UserName,
                };

                var result = await _userManager.CreateAsync(nuser,user.Password);
                if (result.Succeeded)
                {
                    //https://localhost:7082/Login/RegisterConfirm?ID=545338&Token=545338&ddbfrrrg
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(nuser);
                    var confirmationLink = Url.Action("RegisterConfirm", "Login", new
                    {
                        ID = nuser.Id, Token = HttpUtility.UrlEncode(token)
                    }, Request.Scheme);

                    var txtBody = "Please confirm your registration..";
                    var link = "<h6>Dear \"" + nuser.UserName + "\",</h6></br><a href=\"" + confirmationLink + "\">Click Here to Confirm</a>";
                    var subject = "MoviesNow.LK | Please confirm your email";
                    if (await SendGridAPI.Execute(nuser.Email,nuser.UserName, subject, txtBody, link))
                    {
                        return Ok("Registration successfully completed.. !");
                    }
                }
                else
                {
                    return BadRequest(result.Errors);
                }
            }
            return StatusCode(StatusCodes.Status400BadRequest);
        }

        private bool CheckExistUsername(string userName)
        {
            return _db.Users.Any(x => x.UserName == userName);
        }

        private bool CheckExistEmail(string email)
        {
           return _db.Users.Any(x =>x.Email == email);
        }

        private bool IsEmailValidation(string email)
        {
            Regex eq = new Regex(@"\w+\@\w+.com|\w+\@\w+.net");
            if(eq.IsMatch(email))
            {
                return true;
            }
            return false;
        }

        [HttpGet]
        [Route("ConfirmRegistration")]
        public async Task<IActionResult> RegisterConfirm(string Id, string token)
        {
            if(string.IsNullOrEmpty(Id) || string.IsNullOrEmpty(token))
            {
                return NotFound();
            }
            var user = await _userManager.FindByIdAsync(Id);
            if(user == null)
            {
                return NotFound();
            }
            if(!user.EmailConfirmed)
            {
                return Unauthorized("You have not confirmed email yet..!");
            }

            var result = await _userManager.ConfirmEmailAsync(user, HttpUtility.UrlDecode(token));
            if(result.Succeeded)
            {
                return Ok("Registration Success..!");
            }
            else 
            {
                return BadRequest(result.Errors);
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LogIn logIn)
        {
            if (logIn == null)
            {
                return NotFound();
            }
            var user = await _userManager.FindByEmailAsync(logIn.Email);
            if (user == null)
            {
                return NotFound();
            }

            var result = await _signInManager.PasswordSignInAsync(user, logIn.Password,logIn.RememberMe, false);
            if (result.Succeeded)
            {
                return Ok("Login Success..!");
            }
            else
            {
                return BadRequest(result.IsNotAllowed);
            }
        }
    }
}
