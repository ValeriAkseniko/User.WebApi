using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
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
        public async Task<AccountView> Get([FromBody] Guid accountId)
        {
            return await accountService.GetAsync(accountId);
        }

        [HttpDelete]
        [Route("Delete")]
        [Authorize]
        public async Task Delete()
        {
            await accountService.DeleteAsync();
        }
    }
}
