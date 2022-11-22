using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using User.WebApi.User.WebApi.BusinessLogicInterface;
using User.WebApi.User.WebApi.DataTransferObjects.Account;
using User.WebApi.User.WebApi.Entities;

namespace User.WebApi.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService accountService;

        public AccountController(IAccountService accountService)
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

        [HttpPost]
        [Route("Authenticate")]
        public async Task<IActionResult> Authenticate(AuthenticateRequest model)
        {
            var response = await accountService.AuthenticateAsync(model);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }
    }
}
