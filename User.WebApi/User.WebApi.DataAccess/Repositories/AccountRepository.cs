using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using User.WebApi.User.WebApi.DataAccess.Interface.Repositories;
using User.WebApi.User.WebApi.Entities;

namespace User.WebApi.User.WebApi.DataAccess.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserWebApiContext userWebApiContext;
        public AccountRepository(UserWebApiContext userWebApiContext)
        {
            this.userWebApiContext = userWebApiContext;
        }

        public void Dispose()
        {
            userWebApiContext.Dispose();
        }
        public async Task CreateAsync(Account account)
        {
            await userWebApiContext.Accounts.AddAsync(account);
            await userWebApiContext.SaveChangesAsync();
        }
        public async Task<bool> EmailExistAsync(string login)
        {
            return await userWebApiContext.Accounts
                .FirstOrDefaultAsync(x => x.Email.ToLower() == login.ToLower()) != null;
        }
    }
}
