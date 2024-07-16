using Microsoft.AspNetCore.Mvc;
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

        /*[HttpGet("[action]")]
        public IActionResult GetLoggedUser()
        {
            string token = HttpContext.Request.Headers["Authorization"].ToString() ?? "";
            var user = Common.Users.FirstOrDefault(x => x.Token.Equals(token));

            return Ok(new { Name = user.Username });
        }*/
    }
}
