using System;

namespace JobApplicationsApi.Models
{
    public class Note
    {
        public string Description { get; set; }

        public string Body { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
