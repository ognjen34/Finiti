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
        public GlossaryTermController(IMapper mapper,IGlossaryTermService service) : base(mapper)
        {
            _glossaryTermService = service;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddTerm([FromBody] CreateTermRequest request)
        {
           
            GlossaryTerm glossaryTerm = _mapper.Map<GlossaryTerm>(request);
            glossaryTerm.CreatedAt = DateTime.UtcNow;
            glossaryTerm.Author = new Author {Id = LoggedAuthor.Id };
            var createdTerm = await _glossaryTermService.Add(glossaryTerm);
            return Ok(_mapper.Map<TermResponse>(createdTerm));

        }
        [HttpGet("")]
        public async Task<ActionResult> GetAllProjects([FromQuery] PaginationFilter filter)
        {
            PaginationReturnObject<GlossaryTerm> response = await _glossaryTermService.Search(filter);

            return Ok(response);
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

    }
}
