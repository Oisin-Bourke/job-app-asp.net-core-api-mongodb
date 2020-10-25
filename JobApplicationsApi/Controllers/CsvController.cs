using System;
using System.Collections.Generic;
using JobApplicationsApi.Models;
using JobApplicationsApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace JobApplicationsApi.Controllers
{
    [Route("api/applications/[controller]")]
    [ApiController]
    public class CsvController
    {
        private readonly IApplicationService _applicationsService;
        private readonly ILogger<ApplicationsController> _logger;

        public CsvController(IApplicationService applicationService, ILogger<ApplicationsController> logger)
        {
            _applicationsService = applicationService;
            _logger = logger;
        }

        [HttpPost]
        public ActionResult<List<Application>> GetApplicationsForUserByCreatedDateRangeAsCsv(DateRange dateRange)
        {
            var userId = "123";// TODO replace with user id from JWT

            var result = _applicationsService.GetApplicationsForUserByCreatedDate(userId, dateRange.StartDate, dateRange.EndDate);

            return result;
        }
    }

    public class DateRange
    {
        private DateTime _startDate;
        private DateTime _endDate;

        public DateTime StartDate
        {
            get => _startDate;
            set => _startDate = value.AddHours(0).AddMinutes(0).AddSeconds(0);
        }

        public DateTime EndDate
        {
            get => _endDate;
            set => _endDate = value.AddHours(23).AddMinutes(59).AddSeconds(59);
        }
    }
}
