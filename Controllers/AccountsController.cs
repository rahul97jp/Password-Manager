using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PasswordManager.Data;
using PasswordManager.Models;
using System.Text;

namespace PasswordManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : Controller
    {
        private readonly AccountsAPIDbContext dbContext;

        //injecting dbContext in the constructor
        public AccountsController(AccountsAPIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPasswords()
        {
            return Ok(await dbContext.Accounts.ToListAsync());
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetSinglePassword([FromRoute] Guid id) 
        {
            var account = await dbContext.Accounts.FindAsync(id);

            if (account == null)
            {
                return NotFound();
            }

            account.EncryptedPassword = DecryptPassword(account.EncryptedPassword);
            return Ok(account);

        }

        [HttpPost]
        public async Task<IActionResult> AddPassword(AddPasswordRequest addPasswordRequest) 
        {
            var encryptedPassword = EncryptPassword(addPasswordRequest.EncryptedPassword);

            var account = new Accounts()
            {
                Id = Guid.NewGuid(),
                Category = addPasswordRequest.Category,
                App = addPasswordRequest.App,
                UserName = addPasswordRequest.UserName,
                EncryptedPassword = encryptedPassword
            };

            await dbContext.Accounts.AddAsync(account);
            await dbContext.SaveChangesAsync();

            return Ok(account);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdatePassword([FromRoute] Guid id, UpdatePasswordRequest updatePasswordRequest)
        {
            var account = await dbContext.Accounts.FindAsync(id);

            if (account != null)
            {
                account.Category = updatePasswordRequest.Category;
                account.App = updatePasswordRequest.App;
                account.UserName = updatePasswordRequest.UserName;
                account.EncryptedPassword = EncryptPassword(updatePasswordRequest.EncryptedPassword);

                await dbContext.SaveChangesAsync();
                return Ok(account);
            }
            return NotFound();
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeletePassword([FromRoute] Guid id)
        {
            var account = await dbContext.Accounts.FindAsync(id);

            if(account != null)
            {
                dbContext.Remove(account);
                await dbContext.SaveChangesAsync();
                return Ok(account);
            }

            return NotFound(nameof(account));
        }

        private string EncryptPassword(string plaintext)
        {
            return Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(plaintext));
        }

        private string DecryptPassword(string ciphertext)
        {
            return ASCIIEncoding.ASCII.GetString(Convert.FromBase64String(ciphertext));
        }

    }
}
