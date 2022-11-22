using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
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
        private readonly IHttpContextAccessor httpContextAccessor;

        public AccountService(IAccountRepository accountRepository, IHttpContextAccessor httpContextAccessor)
        {
            this.accountRepository = accountRepository;
            this.httpContextAccessor = httpContextAccessor;
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
                Name = accountCreateRequest.Name,
                Surname = accountCreateRequest.Surname,
                PhoneNumber = accountCreateRequest.PhoneNumber
            };
            await accountRepository.CreateAsync(account);
        }
        public async Task UpdateAccountAsync(AccountUpdateRequest accountUpdateRequest)
        {
            var user = httpContextAccessor.HttpContext.User.Identity.Name;
            //var userId = new Guid(httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            var entity = await accountRepository.GetAsync(user);
            entity.Name = accountUpdateRequest.Name;
            entity.Surname = accountUpdateRequest.Surname;
            entity.PhoneNumber = accountUpdateRequest.PhoneNumber;
            entity.Email = accountUpdateRequest.Email;
            await accountRepository.UpdateAsync(entity);
        }

        public async Task<AccountView> GetAsync()
        {
            var user = httpContextAccessor.HttpContext.User.Identity.Name;
            //var userId = new Guid(httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            var entity = await accountRepository.GetAsync(user);
            var result = new AccountView()
            {
                Name = entity.Name,
                Surname = entity.Surname,
                PhoneNumber = entity.PhoneNumber,
                Email = entity.Email
            };
            return result;
        }

        public async Task DeleteAsync()
        {
            var accountId = new Guid(httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            await accountRepository.DeleteAsync(accountId);
        }

        public async Task<ClaimsIdentity> GetIdentity(string email)
        {
            var accounts = await accountRepository.GetListAsync();
            var account = accounts.Find(x => x.Email.ToLower() == email.ToLower());
            if (account != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, account.Email)
                };
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims.ToString(), "Token", ClaimsIdentity.DefaultNameClaimType);
                return claimsIdentity;
            }

            return null;
        }
    }
}
