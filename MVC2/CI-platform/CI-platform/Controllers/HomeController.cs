//using CI_platform.Entities.DataModels;
using CI_platform.Models;
using CI_platform.Service;
using CI_Platform.Entity.DataModels;
using CI_Platform.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MimeKit;
using MailKit.Net.Smtp;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
//using System.Net.Mail;
using System.Security.Claims;

using System.Text;
using MailKit.Security;
using Microsoft.EntityFrameworkCore;

namespace CI_platform.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;
        private readonly IConfiguration _config;
        //private readonly IEmailService _emailService;
        private readonly SMTPConfigModel _smtpConfigModel;  
        //private readonly ilogger<homecontroller> _logger;

        //public homecontroller(ILogger<homecontroller> logger)
        //{
        //    _logger = logger;
        //}
        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork,IOptions<SMTPConfigModel> smtpConfigModel, IConfiguration config)
        {
            _unitOfWork = unitOfWork;
            _config = config;
            this._logger = logger;
            //_emailService = emailService;
            this._smtpConfigModel = smtpConfigModel.Value;
        }

        public IActionResult Index()
        {
            var sessionValue = HttpContext.Session.GetString("UserEmail");
            if (!String.IsNullOrEmpty(sessionValue))
            {
                Console.WriteLine(sessionValue);
                return RedirectToAction(sessionValue);
            }
            return View();
        }
        //public async Task<ViewResult> Index()
        //{
        //    UserEmailOptions options = new UserEmailOptions
        //    {

        //        Body = "Abcd",
        //        Subject = "Ci-platform",
        //        ToEmails = new List<string>() { "test@gmail.com" }
        //    };
        //    await _emailService.SendTestEmail(options);
        //    return View();
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult LoggedIn(string email,string password)
        {
            Console.WriteLine(email);
            if (ModelState.IsValid)
            {
                var user = _unitOfWork.User.GetFirstOrDefault(u => u.Email == email);

                if (user == null || (user.Password != password))
                {
                    TempData["error"] = "Invalid Username or password!!";
                    return RedirectToAction("Index");
                }
                else
                {
                    if (user.Password == password)
                    {
                        TempData["success"] = "Logged In Successfully";
                        HttpContext.Session.SetString("UserEmail", user.Email);
                        return RedirectToAction("HomePage");
                    }
                }
            }
            return RedirectToAction("Index");
        }
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ForgotPassword(string email)
        {
            
                var user = _unitOfWork.User.GetFirstOrDefault(u => u.Email == email);

                if (user == null)
                {
                    TempData["error"] = "User not registered";
                    return View();
                }
                var token = GenerateToken(user);
                Console.WriteLine(token);
       
            _unitOfWork.PasswordReset.Add(new PasswordReset
                    {
                        Email = email,
                        Token = token
                    });
                 _unitOfWork.Save();
            var resetPasswordLink = Url.Action("ResetPassword", "Home", new { token = token }, Request.Scheme);
                UserEmailOptions userEmailOptions = new UserEmailOptions()
                {
                    Subject = "Reset Password Link",
                    Body = "<a href='" + resetPasswordLink + "' >" + resetPasswordLink + "</a>"
                };
                SendEmail(email, userEmailOptions);
                TempData["Success"] = "Mail Sent sucessfully";
               return RedirectToAction("Index");
            }
        private string GenerateToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.Email,user.Email)
            };
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);


            //return new JwtSecurityTokenHandler().WriteToken(token);
            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        // To Send Email
        public void SendEmail(string email, UserEmailOptions userEmailOptions)
        {
            var message = new MimeMessage();

            message.From.Add(new MailboxAddress(_smtpConfigModel.SenderDisplayName, _smtpConfigModel.SenderAddress));
            message.To.Add(new MailboxAddress("User", email));

            message.Subject = userEmailOptions.Subject;
            var bodyBuilder = new BodyBuilder();

            bodyBuilder.HtmlBody = userEmailOptions.Body;
            message.Body = bodyBuilder.ToMessageBody();
            using (var smtp = new SmtpClient())
            {
                smtp.Connect(_smtpConfigModel.host, _smtpConfigModel.Port, _smtpConfigModel.EnableSSL);
                smtp.Authenticate(_smtpConfigModel.UserName, _smtpConfigModel.Password);
                smtp.Send(message);
                smtp.Disconnect(true);
            }
        }

       
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(User userData)
        {
            var useremail = _unitOfWork.User.GetFirstOrDefault(u => u.Email == userData.Email);

            try
            {
                //Console.WriteLine(userData.confirmPassword)
                if (ModelState.IsValid)
                {
                    if (useremail == null)
                    {
                        _unitOfWork.User.Add(userData);
                        _unitOfWork.Save();
                        TempData["success"] = "Employee added successfully";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["error"] = "This Email User is already registered.";
                        return View();
                    }

                }
                else
                {
                    TempData["error"] = "Model Data is not valid";
                    return View();
                }
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return View();

            }

        }

        public IActionResult ResetPassword(string token)
        {
            var userToken = _unitOfWork.PasswordReset.GetFirstOrDefault(u => u.Token == token);
            if (userToken == null)
            {
                return BadRequest("Invalid Session");
            }
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            var tokenObj = handler.ReadJwtToken(token);
            var email = tokenObj.Payload.Claims.ToList()[0].Value;
            if (tokenObj.Payload.Exp < DateTimeOffset.UtcNow.ToUnixTimeSeconds())
            {
                _unitOfWork.PasswordReset.Remove(userToken);
                _unitOfWork.Save();
                return BadRequest("Reset Password Link Expired!");
            }
            ViewBag.Email = new { email = email, token = token };
            return View();
        }

        // POST
        [HttpPost]
        public IActionResult ResetPasswordPost(ResetPassword obj)
        {
            if (obj.Password != obj.ConfirmPassword)
            {
                var parameters = new { token = obj.Token };
                TempData["error"] = "Password and Confirm paswword is not same";
                return RedirectToAction("ResetPassword", parameters);
            }
            var userToken = _unitOfWork.PasswordReset.GetFirstOrDefault(u => u.Email == obj.Email && u.Token == obj.Token);
            if (userToken == null)
            {
                TempData["error"] = "Invalid Reset Password Link";
                return RedirectToAction("ForgotPassword");
            }
            var userObj = _unitOfWork.User.GetFirstOrDefault(u => u.Email == obj.Email);
            var encodedPass = obj.Password;
            _unitOfWork.User.UpdatePassword(userObj, encodedPass);
            _unitOfWork.PasswordReset.Remove(userToken);
            _unitOfWork.Save();
            TempData["success"] = "Password Changed Successfully, Please Login Again!!";
            return RedirectToAction("Index");
        }



        public IActionResult HomePage(long id=0)
        {
            var sessionValue = HttpContext.Session.GetString("UserEmail");
            if (String.IsNullOrEmpty(sessionValue))
            {
                TempData["error"] = "Session Expired!\nPlease Login Again!";
                return RedirectToAction("Index");
            }
            var user = _unitOfWork.User.GetFirstOrDefault(u => u.Email == sessionValue);
            ViewBag.User = user;
            List<Country> countryList = _unitOfWork.Country.GetAll().ToList();
            ViewBag.Countries = countryList;
            if (id != 0)
            {
                List<City> cityList = _unitOfWork.City.GetAll().Where(c => c.CountryId == id).ToList();
                ViewBag.Cities = cityList;
            }
            else
            {
                List<City> cityList = _unitOfWork.City.GetAll().Where(c => c.Name != "Undefined").ToList();
                ViewBag.Cities = cityList;
            }
            List<MissionTheme> themeList = _unitOfWork.MissionTheme.GetAll().ToList();
            ViewBag.Themes = themeList;
            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.Session.SetString("UserEmail", "");
            return RedirectToAction("Index");
        }



        public IActionResult StoryListing()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}