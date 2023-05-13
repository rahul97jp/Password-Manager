using Microsoft.EntityFrameworkCore;
using PasswordManager.Models;

namespace PasswordManager.Data
{
    public class AccountsAPIDbContext : DbContext
    {
        //constructor
        public AccountsAPIDbContext(DbContextOptions options) : base(options)
        {
        }

        //Property which will act as table for entity framework core
        public DbSet<Accounts> Accounts { get; set; }
    }
}
