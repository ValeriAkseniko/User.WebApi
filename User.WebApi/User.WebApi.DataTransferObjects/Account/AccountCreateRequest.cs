using System.ComponentModel.DataAnnotations;

namespace User.WebApi.User.WebApi.DataTransferObjects.Account
{
    public class AccountCreateRequest
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }
    }
}
