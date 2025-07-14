namespace Finiti.WEB.DTO.Responses
{
    public class TermResponse
    {
        public int Id { get; set; }
        public string Term { get; set; }
        public string Definition { get; set; }
        public DateTime CreatedAt { get; set; }
        public AuthorResponse Author { get; set; }
        public string Status { get; set; } 
    }
}
