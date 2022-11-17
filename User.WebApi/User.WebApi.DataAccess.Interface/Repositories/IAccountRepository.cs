using System;
using System.Threading.Tasks;
using User.WebApi.User.WebApi.Entities;

namespace User.WebApi.User.WebApi.DataAccess.Interface.Repositories
{
    public interface IAccountRepository : IDisposable
    {
        Task CreateAsync(Account account);

        Task<bool> EmailExistAsync(string login);
    }
}
