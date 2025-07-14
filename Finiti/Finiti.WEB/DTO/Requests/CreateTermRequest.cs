using System.ComponentModel.DataAnnotations;

namespace Finiti.WEB.DTO.Requests
{
    public class CreateTermRequest
    {
        [Required(ErrorMessage = "Term is required.")]
        public string Term { get; set; }
        [Required(ErrorMessage = "Definition is required.")]
        public string Definition { get; set; }
    }
}
