using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace User.WebApi.User.WebApi.Entities
{
    public class AuthenticateResponse
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }


        public AuthenticateResponse(Account account, string token)
        {
            Id = account.Id;
            FirstName = account.Name;
            LastName = account.Surname;
            Username = account.PhoneNumber;
            Username = account.Email;
            Token = token;
        }
    }
}
