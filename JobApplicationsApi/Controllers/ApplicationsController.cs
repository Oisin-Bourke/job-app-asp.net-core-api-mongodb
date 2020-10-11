using System.Collections.Generic;
using System.Linq;
using JobApplicationsApi.Models;
using JobApplicationsApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace JobApplicationsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationsController : ControllerBase
    {
        private readonly ApplicationService _applicationsService;
        private readonly ILogger<ApplicationsController> _logger;

        public ApplicationsController(ApplicationService applicationService, ILogger<ApplicationsController> logger)
        {
            _applicationsService = applicationService;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<List<Application>> GetAll() =>
            _applicationsService.GetAll();

        [HttpGet("{userId:length(3)}")]
        public ActionResult<List<Application>> GetAllByUser(string userId)
        {
            var applications = _applicationsService.GetAllByUser(userId);

            if (applications == null || !applications.Any())
            {
                return NotFound();
            }

            return applications;
        }

        [HttpGet("{id:length(24)}", Name = "GetApplicaton")]
        public ActionResult<Application> GetById(string id)
        {
            var application = _applicationsService.GetById(id);

            if (application == null)
            {
                return NotFound();
            }

            return application;
        }

        [HttpPost]
        public ActionResult<Application> Create(Application application)
        {
            _applicationsService.Create(application);

            return CreatedAtRoute("GetApplication", new { id = application.Id.ToString() }, application);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Application application)
        {
            var appliction = _applicationsService.GetById(id);

            if (application == null)
            {
                return NotFound();
            }

            _applicationsService.Update(id, application);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var application = _applicationsService.GetById(id);

            if (application == null)
            {
                return NotFound();
            }

            _applicationsService.Remove(application.Id);

            return NoContent();
        }

    }
}
