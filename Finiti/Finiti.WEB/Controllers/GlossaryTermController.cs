using AutoMapper;
using Finiti.DOMAIN.Model;
using Finiti.DOMAIN.Services;
using Finiti.WEB.DTO.Requests;
using Finiti.WEB.DTO.Responses;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
        [HttpPost("add")]
        public async Task<IActionResult> AddTerm([FromBody] CreateTermRequest request)
        {

            GlossaryTerm glossaryTerm = _mapper.Map<GlossaryTerm>(request);
            glossaryTerm.CreatedAt = DateTime.UtcNow;
            glossaryTerm.Author = new Author { Id = LoggedAuthor.Id };
            var createdTerm = await _glossaryTermService.Add(glossaryTerm);
            return Ok(_mapper.Map<TermResponse>(createdTerm));

        }
        [Authorize]
        [HttpGet("")]
        public async Task<ActionResult> GetAllProjects([FromQuery] PaginationFilter filter)
        {
            PaginationReturnObject<GlossaryTerm> response = await _glossaryTermService.Search(filter);
            PaginationReturnObject<TermResponse> mappedResponse = new PaginationReturnObject<TermResponse>(_mapper.Map<List<TermResponse>>(response.Items), response.Page, response.PageSize, response.TotalItems);


            return Ok(mappedResponse);
        }
        [Authorize]
        [HttpGet("author")]
        public async Task<ActionResult> GetAllAuthorProjects([FromQuery] PaginationFilter filter)
        {
            PaginationReturnObject<GlossaryTerm> response = await _glossaryTermService.GetAuthorsTerms(filter,LoggedAuthor.Id);
            PaginationReturnObject<TermResponse> mappedResponse = new PaginationReturnObject<TermResponse>(_mapper.Map<List<TermResponse>>(response.Items), response.Page, response.PageSize, response.TotalItems);


            return Ok(mappedResponse);
        }
        [Authorize]
        [HttpPut("publish/{id}")]
        public async Task<IActionResult> PublishTerm(int id)
        {
            GlossaryTerm publishedTerm = await _glossaryTermService.Publish(id);
            return Ok(_mapper.Map<TermResponse>(publishedTerm));
        }
        [Authorize]
        [HttpPut("archive/{id}")]
        public async Task<IActionResult> ArchiveTerm(int id)
        {
            GlossaryTerm archivedTerm = await _glossaryTermService.Archive(id);
            return Ok(_mapper.Map<TermResponse>(archivedTerm));
        }
        [Authorize]
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteTerm(int id)
        {
            GlossaryTerm deletedTerm = await _glossaryTermService.Delete(id);
            return Ok(_mapper.Map<TermResponse>(deletedTerm));
        }
        [Authorize]
        [HttpPost("forbiddenWord")]
        public async Task<IActionResult> AddForbiddenWord([FromBody] ForbiddenWordRequest request)
        {

            await _forbiddenWordService.Add(_mapper.Map<ForbiddenWord>(request));
            return Ok("Forbidden word added successfully.");

        }
        [Authorize]
        [HttpDelete("forbiddenWord/{word}")]
        public async Task<IActionResult> DeleteForbiddenWord(string word)
        {
             await _forbiddenWordService.DeleteByWord(word);
             return Ok("Forbidden word deleted successfully.");
            
            
        }
        [Authorize]
        [HttpPut("")]
        public async Task<IActionResult> UpdateTerm([FromBody] UpdateTermRequest request)
        {
            GlossaryTerm glossaryTerm = _mapper.Map<GlossaryTerm>(request);
            GlossaryTerm updatedTerm = await _glossaryTermService.Update(glossaryTerm);
            return Ok(_mapper.Map<TermResponse>(updatedTerm));
        }
    }
}
