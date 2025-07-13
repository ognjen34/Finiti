using AutoMapper;
using Finiti.DOMAIN.Model;
using Finiti.DOMAIN.Services;
using Finiti.WEB.DTO.Requests;
using Finiti.WEB.DTO.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Finiti.WEB.Controllers
{
    [Route("terms")]
    [ApiController]
    public class GlossaryTermController : BaseController
    {
        private IGlossaryTermService _glossaryTermService;
        private IForbiddenWordService _forbiddenWordService;
        public GlossaryTermController(IMapper mapper, IGlossaryTermService service, IForbiddenWordService forbiddenWordService) : base(mapper)
        {
            _glossaryTermService = service;
            _forbiddenWordService = forbiddenWordService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddTerm([FromBody] CreateTermRequest request)
        {

            GlossaryTerm glossaryTerm = _mapper.Map<GlossaryTerm>(request);
            glossaryTerm.CreatedAt = DateTime.UtcNow;
            glossaryTerm.Author = new Author { Id = LoggedAuthor.Id };
            var createdTerm = await _glossaryTermService.Add(glossaryTerm);
            return Ok(_mapper.Map<TermResponse>(createdTerm));

        }
        [HttpGet("")]
        public async Task<ActionResult> GetAllProjects([FromQuery] PaginationFilter filter)
        {
            PaginationReturnObject<GlossaryTerm> response = await _glossaryTermService.Search(filter);
            PaginationReturnObject<TermResponse> mappedResponse = new PaginationReturnObject<TermResponse>(_mapper.Map<List<TermResponse>>(response.Items), response.Page, response.PageSize, response.TotalItems);


            return Ok(mappedResponse);
        }
        [HttpGet("publish/{id}")]
        public async Task<IActionResult> PublishTerm(int id)
        {
            GlossaryTerm publishedTerm = await _glossaryTermService.Publish(id);
            return Ok(_mapper.Map<TermResponse>(publishedTerm));
        }
        [HttpGet("archive/{id}")]
        public async Task<IActionResult> ArchiveTerm(int id)
        {
            GlossaryTerm archivedTerm = await _glossaryTermService.Archive(id);
            return Ok(_mapper.Map<TermResponse>(archivedTerm));
        }
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteTerm(int id)
        {
            GlossaryTerm deletedTerm = await _glossaryTermService.Delete(id);
            return Ok(_mapper.Map<TermResponse>(deletedTerm));
        }
        [HttpPost("forbiddenWord")]
        public async Task<IActionResult> AddForbiddenWord([FromBody] ForbiddenWordRequest request)
        {

            await _forbiddenWordService.Add(_mapper.Map<ForbiddenWord>(request));
            return Ok("Forbidden word added successfully.");

        }
        [HttpDelete("forbiddenWord/{word}")]
        public async Task<IActionResult> DeleteForbiddenWord(string word)
        {
             await _forbiddenWordService.DeleteByWord(word);
             return Ok("Forbidden word deleted successfully.");
            
            
        }
    }
}
