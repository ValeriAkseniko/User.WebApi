using System;
using System.Threading.Tasks;
using User.WebApi.User.WebApi.BusinessLogicInterface;
using User.WebApi.User.WebApi.DataAccess.Interface.Repositories;
using User.WebApi.User.WebApi.DataTransferObjects.Account;
using User.WebApi.User.WebApi.Entities;
using User.WebApi.UserWebApi.Infrastructure.Exceptions;

namespace User.WebApi.User.WebApi.BusinessLogicServices
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            this.accountRepository = accountRepository;
        }
        public async Task CreateAccountAsync(AccountCreateRequest accountCreateRequest)
        {
            if (await accountRepository.EmailExistAsync(accountCreateRequest.Email))
            {
                var exception = new EmailExistException(accountCreateRequest.Name);
                throw exception;
            }
            var account = new Account()
            {
                Email = accountCreateRequest.Email.ToLower(),
                Id = Guid.NewGuid(),
                Name =accountCreateRequest.Name,
                Surname = accountCreateRequest.Surname,
                PhoneNumber = accountCreateRequest.PhoneNumber
            };
            await accountRepository.CreateAsync(account);
        }
    }
}
