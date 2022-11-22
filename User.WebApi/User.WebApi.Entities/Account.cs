using System;

namespace User.WebApi.User.WebApi.Entities
{
    public class Account
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }
    }
}
