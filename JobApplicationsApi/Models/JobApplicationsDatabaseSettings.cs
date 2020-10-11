
namespace JobApplicationsApi.Models
{
    public class JobApplicationsDatabaseSettings : IJobApplicationsDatabaseSettings
    {
        public string ApplicationsCollectionName { get; set; }

        public string ConnectionString { get; set; }

        public string DatabaseName { get; set; }

    }

    public interface IJobApplicationsDatabaseSettings
    {
        string ApplicationsCollectionName { get; set; }

        string ConnectionString { get; set; }

        string DatabaseName { get; set; }

    }
}
