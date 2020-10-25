using System;
using System.Collections.Generic;
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
        private readonly IApplicationService _applicationsService;
        private readonly ILogger<ApplicationsController> _logger;

        public ApplicationsController(IApplicationService applicationService, ILogger<ApplicationsController> logger)
        {
            _applicationsService = applicationService;
            _logger = logger;
        }

        [HttpGet("all_users_applications")]
        public ActionResult<List<Application>> GetAll()
        {
            return _applicationsService.GetApplications();
        }
            
        [HttpGet]
        public ActionResult<List<Application>> GetAllByUser()
        {
            var userId = "123";// TODO replace with user id from JWT

            var applications = _applicationsService.GetApplicationsForUser(userId);

            if (applications == null)
            {
                return NotFound();
            }

            return applications;
        }

        [HttpGet("{applicationId:length(24)}", Name = "GetApplication")]
        [ActionName(nameof(GetApplication))]
        public ActionResult<Application> GetApplication(string applicationId)
        {
            var application = _applicationsService.GetApplicationById(applicationId);

            if (application == null)
            {
                return NotFound();
            }

            return application;
        }

        [HttpPost]
        public ActionResult<Application> Create(Application application)
        {
            _applicationsService.CreateApplication(application);

            return CreatedAtAction("GetApplication", new { applicationId = application.Id.ToString() }, application);
        }

        [HttpPut("{applicationId:length(24)}")]
        public IActionResult Update(string applicationId, Application applicationIn)
        {
            var application = _applicationsService.GetApplicationById(applicationId);

            if (application == null)
            {
                return NotFound();
            }

            _applicationsService.UpdateApplication(applicationId, applicationIn);

            return NoContent();
        }

        [HttpDelete("{applicationId:length(24)}")]
        public IActionResult Delete(string applicationId)
        {
            var application = _applicationsService.GetApplicationById(applicationId);

            if (application == null)
            {
                return NotFound();
            }

            _applicationsService.RemoveApplication(application.Id);

            return NoContent();
        }

        [HttpDelete]
        public IActionResult DeleteAllByUser()
        {
            var userId = "123";// TODO replace with user id from JWT

            _applicationsService.RemoveAllApplications(userId);

            return NoContent();
        }
    }
}
