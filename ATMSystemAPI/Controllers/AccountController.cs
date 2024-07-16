using Microsoft.AspNetCore.Mvc;
using ATMSystemAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System;
using ATMSystemAPI.Data;
//using ATMSystemAPI.Models; // AmountModel sınıfının tanımlandığı namespace

namespace ATMSystemAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
       
        private string GetTokenFromHeader()
        {
            string token = HttpContext.Request.Headers["Authorization"].ToString() ?? "";
            return token;
        }

        private User? GetUser()
        {
            string token = GetTokenFromHeader();
            var user = Common.Users.FirstOrDefault(x => x.Token.Equals(token));

            return user;
        }

        [HttpGet("balance")]
        public IActionResult GetBalance()
        {
            var user = GetUser();
            if (user == null)
            {
                return Unauthorized("Geçersiz token");
            }

            var account =Common.Accounts.SingleOrDefault(a => a.UserId == user.Id);
            if (account == null)
            {
                return NotFound("Hesap bulunamadı");
            }

            return Ok(account.Balance);
        }

        [HttpPost("deposit")]
        public IActionResult Deposit([FromBody] AmountModel model)
        {
            var user = GetUser();
            if (user == null)
            {
                return Unauthorized("Geçersiz token");
            }

            var account = Common.Accounts.SingleOrDefault(a => a.UserId == user.Id);
            if (account == null)
            {
                return NotFound("Hesap bulunamadı");
            }

            account.Balance += model.Amount;
            Common.Transactions.Add(new Transaction { Id = Common.Transactions.Count + 1, AccountId = account.Id, Amount = model.Amount, Date = DateTime.Now, Type = "Deposit" });

            return Ok(account.Balance);
        }

        [HttpPost("withdraw")]
        public IActionResult Withdraw([FromBody] AmountModel model)
        {
            var user = GetUser();
            if (user == null)
            {
                return Unauthorized("Geçersiz token");
            }

            var account = Common.Accounts.SingleOrDefault(a => a.UserId == user.Id);
            if (account == null)
            {
                return NotFound("Hesap bulunamadı");
            }

            if (account.Balance < model.Amount)
            {
                return BadRequest("Yetersiz bakiye");
            }

            account.Balance -= model.Amount;
            Common.Transactions.Add(new Transaction { Id = Common.Transactions.Count + 1, AccountId = account.Id, Amount = model.Amount, Date = DateTime.Now, Type = "Withdrawal" });

            return Ok(account.Balance);
        }

        [HttpGet("transactions")]
        public IActionResult GetTransactions()
        {
            var user = GetUser();
            if (user == null)
            {
                return Unauthorized("Geçersiz token");
            }

            var userTransactions = Common.Transactions.Where(t => Common.Accounts.Any(a => a.UserId == user.Id && a.Id == t.AccountId)).ToList();

            return Ok(userTransactions);
        }
    }
}
