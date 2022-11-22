using System;
using System.Threading.Tasks;
using User.WebApi.User.WebApi.DataTransferObjects.Account;
using User.WebApi.User.WebApi.Entities;

namespace User.WebApi.User.WebApi.BusinessLogicInterface
{
    public interface IAccountService
    {
        Task CreateAccountAsync(AccountCreateRequest accountCreateRequest);

        Task<AccountView> GetAsync();

        Task UpdateAccountAsync(AccountUpdateRequest accountUpdateRequest);

        Task DeleteAsync();

        Task<Account> GetByIdAsync(Guid id);

        Task<AuthenticateResponse> AuthenticateAsync(AuthenticateRequest model);
    }
}
