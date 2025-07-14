using System.ComponentModel.DataAnnotations;

namespace Finiti.WEB.DTO.Requests
{
    public class AuthorLoginRequest
    {

        [Required]

        public string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
    }
}
