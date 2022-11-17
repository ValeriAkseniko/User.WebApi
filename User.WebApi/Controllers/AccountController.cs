using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using User.WebApi.User.WebApi.BusinessLogicServices;
using User.WebApi.User.WebApi.DataTransferObjects.Account;

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
    }
}
