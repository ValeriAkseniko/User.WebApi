using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using User.WebApi.User.WebApi.Entities;

namespace User.WebApi.User.WebApi.DataAccess.Interface.Repositories
{
    public interface IAccountRepository : IDisposable
    {
        Task CreateAsync(Account account);

        Task<bool> EmailExistAsync(string login);

        Task<Account> GetAsync(Guid accountId);

        Task<Account> GetAsync(string email);

        Task UpdateAsync(Account account);

        Task DeleteAsync(Guid accountId);

        Task<List<Account>> GetListAsync();

        Task<Account> GetByIdAsync(Guid id);
    }
}
