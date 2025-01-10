using DecBatchCodeFirstApproachImpl.Data;
using DecBatchCodeFirstApproachImpl.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Net.Mail;
using System.Net;
using System.Text;

namespace DecBatchCodeFirstApproachImpl.Controllers
{
    public class AuthController : Controller
    {
        private readonly ApplicationDbContext db;
        public AuthController(ApplicationDbContext db)
        {
            this.db = db;
        }

        public IActionResult Users()
        {
            var data = db.users.ToList();
            return Json(data);
        }

        public static string EncryptPassword(string password)
        {
            if (password.IsNullOrEmpty())
            {
                return null;
            }
            else
            {
                byte[] data = ASCIIEncoding.UTF8.GetBytes(password);
                string encpass = Convert.ToBase64String(data);
                return encpass;
            }
        }

        public static string DecryptPassword(string password)
        {
            if (password.IsNullOrEmpty())
            {
                return null;
            }
            else
            {
                byte[] data = Convert.FromBase64String(password);
                string decpass = ASCIIEncoding.UTF8.GetString(data);
                return decpass;
            }
        }

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignUp(User us)
        {
            var data = db.users.Where(x => x.Email.Equals(us.Email)).SingleOrDefault();
            if (data != null)
            {
                TempData["existUser"] = "Already User is Exist";
                return View();
            }
            else
            {
                us.Role = "User";
                us.Status = "Active";
                sendSignUpDetails(us.Email, us.Password);
                us.Password = EncryptPassword(us.Password);
                db.users.Add(us);
                db.SaveChanges();

                return RedirectToAction("SignIn");
            }


        }

        public void sendSignUpDetails(string email, string pass)
        {
            //var username = db.users.Where(x => x.Email.Equals(email)).Select(x => x.Username);
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("onkarsalunkhe1519@gmail.com");
            mail.To.Add(email);
            mail.Subject = "Regarding User Credentials";
            mail.Body = $"Hello {email},\n\nYour Credentials is given below:\n\nEmail ID: {email} & Password is {pass}";

            SmtpClient smtp = new SmtpClient("smtp.gmail.com");
            smtp.Credentials = new NetworkCredential("onkarsalunkhe1519@gmail.com", "hxwwwrzpuaxpzwpb");
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.Send(mail);
        }

        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignIn(User us)
        {
            var emaildata = db.users.Where(x => x.Email.Equals(us.Email)).SingleOrDefault();
            if (emaildata != null)
            {
                string pass = DecryptPassword(emaildata.Password);
                var npass = pass.Equals(us.Password);
                if (npass)
                {
                    if (emaildata.Status.Equals("Deactive"))
                    {
                        TempData["status"] = "Your Account is Deactivate by Admin";
                        return View();
                    }
                    else
                    {
                        bool admin = emaildata.Role.Equals("Admin");
                        bool user = emaildata.Role.Equals("User");
                        HttpContext.Session.SetString("Role", emaildata.Role);
                        HttpContext.Session.SetString("Email", emaildata.Email);
                        if (admin)
                        {
                            HttpContext.Session.SetString("User", emaildata.Username);
                            return RedirectToAction("AdminDashboard", "Dashboard");

                        }
                        if (user)
                        {
                            HttpContext.Session.SetString("User", emaildata.Username);
                            return RedirectToAction("UserDashboard", "Dashboard");
                        }
                    }

                }
                else
                {
                    TempData["errpass"] = "Invalid Password";
                    return View();
                }
            }
            else
            {
                TempData["erremail"] = "Email is Invalid";
                return View();
            }
            return View();
        }



        public IActionResult Logout()
        {
            HttpContext.Session.Remove("User");
            HttpContext.Session.Clear();
            return RedirectToAction("SignIn");
        }

        public IActionResult ResetPassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ResetPassword(string newPassword)
        {
            var suser = HttpContext.Session.GetString("Email");
            var data = db.users.Where(x => x.Email.Equals(suser)).SingleOrDefault();
            data.Password = EncryptPassword(newPassword);
            db.users.Update(data);
            db.SaveChanges();
            return View();
        }
    }
}
