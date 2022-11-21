using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace User.WebApi.User.WebApi.DataTransferObjects.Account
{
    public class AccountUpdateRequest
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }
    }
}
