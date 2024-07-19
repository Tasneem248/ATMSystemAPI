/*using Microsoft.AspNetCore.Mvc;
using ATMSystemAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ATMSystemAPI.Data;

namespace ATMSystemAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {

        [HttpPost("login")]
        public IActionResult Login([FromBody] User login)
        {
            var user = Common.Users.SingleOrDefault(u => u.Username == login.Username && u.Password == login.Password);

            if (user == null)
            {
                return Unauthorized("Kullanıcı adı veya şifre hatalı");
            }

            // Özel token oluşturma
            string token = TokenGenerator.GenerateCustomToken(64);
            user.Token = token; // Kullanıcının token'ını güncelle

            return Ok(new { Token = token });
        }

      
    }
}*/

using Microsoft.AspNetCore.Mvc;
using ATMSystemAPI.Models;
using ATMSystemAPI.Data;
using System.Linq;
using System.Text;
using System.IO;
using ATMSystemAPI.Helpers;

namespace ATMSystemAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        // Login metodu

        [HttpPost("register")]
        public IActionResult Register([FromBody] User newUser)
        {
            if (Common.Users.Any(u => u.Username == newUser.Username))
            {
                return BadRequest("Bu kullanıcı adı zaten kullanılıyor.");
            }

            newUser.Id = Common.Users.Count > 0 ? Common.Users.Max(u => u.Id) + 1 : 1;
            Common.Users.Add(newUser);
            UserFileHelper.WriteUsersToFile(Common.Users);

            return Ok("Kullanıcı başarıyla kaydedildi.");
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] User login)
        {
            var user = Common.Users.SingleOrDefault(u => u.Username == login.Username && u.Password == login.Password);

            if (user == null)
            {
                return Unauthorized("Kullanıcı adı veya şifre hatalı");
            }

            // Özel token oluşturma
            string token = TokenGenerator.GenerateCustomToken(64);
            user.Token = token; // Kullanıcının token'ını güncelle
            UserFileHelper.WriteUsersToFile(Common.Users);

            return Ok(new { Token = token });
        }

        [HttpGet("read-users")]
        public IActionResult ReadUsers()
        {
            var users = Common.Users;
            return Ok(users);

        }
    }
}
