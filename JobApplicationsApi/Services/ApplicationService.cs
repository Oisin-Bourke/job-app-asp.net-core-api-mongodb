using System.Collections.Generic;
using JobApplicationsApi.Models;
using MongoDB.Driver;

namespace JobApplicationsApi.Services
{
    public class ApplicationService
    {
        private readonly IMongoCollection<Application> _applications;

        public ApplicationService(IJobApplicationsDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _applications = database.GetCollection<Application>(settings.ApplicationsCollectionName);
        }

        public List<Application> GetAll() =>
            _applications.Find(application => true).ToList();

        public List<Application> GetAllByUser(string userId) =>
            _applications.Find(application => application.UserId == userId).ToList();

        public Application GetById(string id) =>
            _applications.Find(application => application.Id == id).FirstOrDefault();

        public Application Create(Application application)
        {
            _applications.InsertOne(application);
            return application;
        }

        public void Update(string id, Application application)
        {
            _applications.ReplaceOne(application => application.Id == id, application);
        }

        public void Remove(Application application)
        {
            _applications.DeleteOne(application => application.Id == application.Id);
        }

        public void Remove(string id)
        {
            _applications.DeleteOne(application => application.Id == id);
        }

    }
}
