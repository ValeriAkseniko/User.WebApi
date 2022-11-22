using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
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
        private readonly AppSettings appSettings;

        public AccountService(IAccountRepository accountRepository, IHttpContextAccessor httpContextAccessor, IOptions<AppSettings> appSettings)
        {
            this.accountRepository = accountRepository;
            this.httpContextAccessor = httpContextAccessor;
            this.appSettings = appSettings.Value;
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

        private string generateJwtToken(Account account)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", account.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<AuthenticateResponse> AuthenticateAsync(AuthenticateRequest model)
        {
            var users = await accountRepository.GetListAsync();
            var user = users.FirstOrDefault(x => x.Email == model.Email);

            if (user == null) return null;

            var token = GenerateJwtToken(user);

            return new AuthenticateResponse(user, token);
        }

        public async Task<Account> GetByIdAsync(Guid id)
        {
            return await accountRepository.GetByIdAsync(id);
        }

        private string GenerateJwtToken(Account account)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", account.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
