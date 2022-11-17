using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User.WebApi.User.WebApi.Entities;

namespace User.WebApi.User.WebApi.DataAccess
{
    public class UserWebApiContext : DbContext
    {
        public UserWebApiContext(DbContextOptions<UserWebApiContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Account> Accounts { get; set; }
    }
}
