using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using User.WebApi.User.WebApi.BusinessLogicServices;
using User.WebApi.User.WebApi.DataTransferObjects.Account;
using User.WebApi.User.WebApi.Entities;

namespace User.WebApi.Controllers
{
    public class AccountController : Controller
    {
        private readonly AccountService accountService;

        public AccountController(AccountService accountService)
        {
            this.accountService = accountService;
        }

        [HttpPost]
        [Route("Create")]
        [AllowAnonymous]
        public async Task Create([FromBody] AccountCreateRequest accountCreateRequest)
        {
            await accountService.CreateAccountAsync(accountCreateRequest);
        }

        [HttpPost]
        [Route("Update")]
        [Authorize]
        public async Task Update([FromBody] AccountUpdateRequest accountUpdateRequest)
        {
            await accountService.UpdateAccountAsync(accountUpdateRequest);
        }

        [HttpGet]
        [Route("Get")]
        [Authorize]
        public async Task<AccountView> Get()
        {
            return await accountService.GetAsync();
        }

        [HttpDelete]
        [Route("Delete")]
        [Authorize]
        public async Task Delete()
        {
            await accountService.DeleteAsync();
        }

        [HttpPost("/token")]
        public async Task<IActionResult> Token(string email)
        {
            ClaimsIdentity identity = await accountService.GetIdentity(email);
            if (identity == null)
            {
                return BadRequest();
            }

            var now = DateTime.UtcNow;
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                username = identity.ToString()
            };

            return Json(response);
        }

        
    }
}
