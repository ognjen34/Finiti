using System.ComponentModel.DataAnnotations;

namespace Finiti.WEB.DTO.Requests
{
    public class UpdateTermRequest
    {
        [Required(ErrorMessage = "Id is required.")]
        public int Id { get; set; }
        public string Term { get; set; }
        public string Definition { get; set; }

    }
}
