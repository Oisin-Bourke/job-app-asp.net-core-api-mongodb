
namespace JobApplicationsApi.Models
{
    public class DatabaseSettings : IDatabaseSettings
    {
        public string ApplicationsCollectionName { get; set; }

        public string ConnectionString { get; set; }

        public string DatabaseName { get; set; }

    }

    public interface IDatabaseSettings
    {
        string ApplicationsCollectionName { get; set; }

        string ConnectionString { get; set; }

        string DatabaseName { get; set; }

    }
}
