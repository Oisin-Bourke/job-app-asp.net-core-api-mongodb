using JobApplicationsApi.Models;
using JobApplicationsApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace JobApplicationsApi.Controllers
{
    [Route("api/{applicationId:length(24)}/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly IApplicationService _applicationsService;
        private readonly ILogger<ApplicationsController> _logger;

        public NotesController(IApplicationService applicationService, ILogger<ApplicationsController> logger)
        {
            _applicationsService = applicationService;
            _logger = logger;
        }

        [HttpGet("{noteId:length(24)}")]
        public ActionResult<Note> GetNote(string applicationId, string noteId)
        {
            var note = _applicationsService.GetNoteById(applicationId, noteId);
            
            if (note == null)
            {
                return NotFound();
            }

            return note;
        }

        [HttpPut]
        public IActionResult AddNote(string applicationId, Note note)
        {
            var application = _applicationsService.GetApplicationById(applicationId);

            if (application == null)
            {
                return NotFound();
            }

            _applicationsService.AddNote(applicationId, note);

            return NoContent();
        }

        [HttpPut("{noteId:length(24)}")]
        public IActionResult UpdateNote(string applicationId, string noteId, Note noteIn)
        {
            var note = _applicationsService.GetNoteById(applicationId, noteId);

            if (note == null)
            {
                return NotFound();
            }

            _applicationsService.UpdateNote(applicationId, noteId, noteIn);

            return NoContent();
        }

        [HttpDelete("{noteId:length(24)}")]
        public IActionResult Delete(string applicationId, string noteId)
        {
            var note = _applicationsService.GetNoteById(applicationId, noteId);

            if (note == null)
            {
                return NotFound();
            }

            _applicationsService.RemoveNote(applicationId, noteId);

            return NoContent();
        }

    }
}
