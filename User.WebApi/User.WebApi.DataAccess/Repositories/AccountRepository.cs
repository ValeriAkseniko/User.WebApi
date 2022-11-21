using Microsoft.EntityFrameworkCore;
using System;
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

        public async Task<Account> GetAsync(Guid accountId)
        {
            return await userWebApiContext.Accounts
                .FirstOrDefaultAsync(x => x.Id == accountId);
        }
        public async Task<bool> EmailExistAsync(string login)
        {
            return await userWebApiContext.Accounts
                .FirstOrDefaultAsync(x => x.Email.ToLower() == login.ToLower()) != null;
        }

        public async Task UpdateAsync(Account account)
        {
            var entity = await GetAsync(account.Id);
            entity.Name = account.Name;
            entity.Surname = account.Surname;
            entity.PhoneNumber = account.PhoneNumber;
            entity.Email = account.Email;
            userWebApiContext.Entry(entity).State = EntityState.Modified;
            await userWebApiContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid accountId)
        {
            var entity = await GetAsync(accountId);
            userWebApiContext.Entry(entity).State = EntityState.Deleted;
            userWebApiContext.Remove(entity);
            await userWebApiContext.SaveChangesAsync();
        }
    }
}
