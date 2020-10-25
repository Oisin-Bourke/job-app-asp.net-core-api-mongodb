using System;
using System.Collections.Generic;
using JobApplicationsApi.Models;

namespace JobApplicationsApi.Services
{
    public interface IApplicationService
    {
        // Applications
        List<Application> GetApplications();

        List<Application> GetApplicationsForUser(string userId);

        Application GetApplicationById(string applicationid);

        Application CreateApplication(Application application);

        List<Application> GetApplicationsForUserByCreatedDate(string userId, DateTime startDate, DateTime endDate);

        void UpdateApplication(string applicationId, Application applicationIn);

        void RemoveApplication(string applicationId);

        void RemoveAllApplications(string userId);

        // Notes (embedded) 
        Note GetNoteById(string applicationId, string noteId);

        void AddNote(string applicationId, Note note);

        void UpdateNote(string applicationId, string noteId, Note noteIn);

        void RemoveNote(string applicationId, string noteId);
    }
}
