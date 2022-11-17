using System.Threading.Tasks;
using User.WebApi.User.WebApi.DataTransferObjects.Account;

namespace User.WebApi.User.WebApi.BusinessLogicInterface
{
    public interface IAccountService
    {
        Task CreateAccountAsync(AccountCreateRequest accountCreateRequest);
    }
}
