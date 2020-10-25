using System;
using System.Collections.Generic;
using System.Linq;
using JobApplicationsApi.Models;
using MongoDB.Driver;

namespace JobApplicationsApi.Services
{
    // Mongodb service methods for applications and embedded notes
    public class ApplicationService : IApplicationService
    {
        private readonly IMongoCollection<Application> _applications;

        public ApplicationService(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _applications = database.GetCollection<Application>(settings.ApplicationsCollectionName);
        }

        // Applications
        public List<Application> GetApplications()
        {
           return _applications.Find(application => true).ToList();
        }
            
        public List<Application> GetApplicationsForUser(string userId)
        {
            return _applications.Find(application => application.UserId == userId).ToList();
        }
            
        public Application GetApplicationById(string applicationId)
        {
            return _applications.Find(application => application.Id == applicationId).FirstOrDefault();
        }

        public List<Application> GetApplicationsForUserByCreatedDate(string userId, DateTime startDate, DateTime endDate)
        {
            var filter =
                 Builders<Application>.Filter.Eq(application => application.UserId, userId)
                 & Builders<Application>.Filter.Gt(application => application.CreatedDate, startDate)
                 & Builders<Application>.Filter.Lt(application => application.CreatedDate, endDate);

            return _applications.Find(filter).ToList();
        }
            
        public Application CreateApplication(Application application)
        {
            _applications.InsertOne(application);
            return application;
        }

        public void UpdateApplication(string applicationId, Application applicationIn)
        {
            var update = Builders<Application>.Update
                .Set(application => application.Title, applicationIn.Title)
                .Set(application => application.Name, applicationIn.Name)
                .Set(application => application.Location, applicationIn.Location)
                .Set(application => application.Email, applicationIn.Email)
                .Set(application => application.Telephone, applicationIn.Telephone)
                .Set(application => application.Status, applicationIn.Status)
                .Set(application => application.ModifiedDate, DateTime.UtcNow);

            _applications.UpdateOne(application => application.Id == applicationId, update);
        }

        public void RemoveApplication(string applicationId)
        {
            _applications.DeleteOne(application => application.Id == applicationId);
        }

        public void RemoveAllApplications(string userId)
        {
            _applications.DeleteMany(application => application.UserId == userId);

        }

        // Notes (embedded)
        public Note GetNoteById(string applicationId, string noteId)
        { 
            var application = _applications.Find(application => application.Id == applicationId).FirstOrDefault();

            return application?.Notes.FirstOrDefault(note => note.Id == noteId);
        }

        public void AddNote(string applicationId, Note note)
        {
            var update = Builders<Application>.Update
                .Push(application => application.Notes, note)
                .Set(application => application.ModifiedDate, DateTime.UtcNow);

            _applications.UpdateOne(application => application.Id == applicationId, update);
        }

        public void UpdateNote(string applicationId, string noteId, Note noteIn)
        {
            var filter =
                Builders<Application>.Filter.Eq(application => application.Id, applicationId) &
                Builders<Application>.Filter.ElemMatch(
                    application => application.Notes, Builders<Note>.Filter.Eq(note => note.Id, noteId)
                    );

            // '-1' gets converted to mongodb positional operator '$'
            var update = Builders<Application>.Update
                .Set(application => application.Notes[-1].Description, noteIn.Description)
                .Set(application => application.Notes[-1].Body, noteIn.Body)
                .Set(application => application.ModifiedDate, DateTime.UtcNow);

            _applications.UpdateOne(filter, update);
        }

        public void RemoveNote(string applicationId, string noteId)
        {
            var update = Builders<Application>.Update
                .PullFilter(application => application.Notes, Builders<Note>.Filter.Eq(note => note.Id, noteId))
                .Set(application => application.ModifiedDate, DateTime.UtcNow); 

            _applications.UpdateOne(application => application.Id == applicationId, update);
        }
    }
}
