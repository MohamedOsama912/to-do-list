using Microsoft.AspNetCore.Mvc;
using System.Text;
using to_do_list_project.Data;
using to_do_list_project.Models;
using System.Security.Cryptography;
using to_do_list_project.Dtos;

namespace to_do_list_project.Controllers
{
    public class AccountController : Controller
    {
        private readonly ToDoContext _context;
        private readonly IWebHostEnvironment _environment;
        public AccountController(ToDoContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UserDto user, IFormFile img_file)
        {
            var UU = new User();
            UU.UserName = user.UserName;
            UU.Email = user.Email;


            string path = Path.Combine(_environment.WebRootPath, "img");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            if (img_file != null)
            {
                path = Path.Combine(path, img_file.FileName);
                using (var strem = new FileStream(path, FileMode.Create))
                {
                    await img_file.CopyToAsync(strem);
                    UU.ProfilePicturePath = img_file.FileName;
                }
            }
            else
            {
                UU.ProfilePicturePath = "default.jpeg";
            }
            if (!ModelState.IsValid)
            {
                return View(user);
            }

            var isEmailExist = _context.Users.Any(x => x.Email == user.Email);
            if (isEmailExist)
            {
                ModelState.AddModelError("Email", "Email has already been taken");
                return View();
            }


            if (user.Password == user.RepeatPassword)
            {

                UU.Password = HashPassword(user.Password);
                UU.RepeatPassword = HashPassword(user.RepeatPassword);

                _context.Users.Add(UU);
                _context.SaveChanges();
                return RedirectToAction("Login");
            }
            else
            {
                ViewBag.Message = "Password and confirm password do not match.";
                return View(user);
            }

        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(UserDto model)
        {
            var user = _context.Users.FirstOrDefault(x => x.Email == model.Email);
            if (user != null && user.Password == HashPassword(model.Password))
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                HttpContext.Session.SetString("Id", user.Id.ToString());
                return RedirectToAction("Index", "Home");
            }
            else
            {
                if (user == null)
                {
                    ModelState.AddModelError("Email", "Invalid Email or Password");
                }
                else
                {
                    ModelState.AddModelError("Password", "Invalid Email or Password");
                }
            }
            return View(model);
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Account");
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {

                byte[] data = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                StringBuilder stringBuilder = new StringBuilder();
                for (int i = 0; i < data.Length; i++)
                {
                    stringBuilder.Append(data[i].ToString("x2"));
                }

                return stringBuilder.ToString();
            }
        }


    }
}
