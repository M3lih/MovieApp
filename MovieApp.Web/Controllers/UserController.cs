using Microsoft.AspNetCore.Mvc;
using MovieApp.Web.Models;
using System.Collections.Generic;
using System.Linq;

namespace MovieApp.Web.Controllers
{
    public class UserController : Controller
    {
        public IActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateUser(UserModel model)
        {
            return View();
        }

        [AcceptVerbs("GET","POST")]
        public IActionResult VerifyUserName(string UserName)
        {
            var users = new List<string> { "sunture", "aksekili" };

            if (users.Any(i => i == UserName))
            {
                return Json ($"{UserName} kullanıcı adı daha önce alınmış!");
            }
            return Json(true);
        }

    }
}
