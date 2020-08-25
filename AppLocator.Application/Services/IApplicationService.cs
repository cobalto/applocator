using AppLocator.Application.Models;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppLocator.Application.Services
{
    public interface IApplicationService
    {
        Task<List<ApplicationModel>> GetApplicationList();
        Task<ApplicationModel> GetApplication(int applicationId);
        Task<ApplicationModel> SaveApplication(ApplicationModel request);
        Task<ApplicationModel> UpdateApplication(int applicationId, JsonPatchDocument<ApplicationModel> patch);
        Task DeleteApplication(int applicationId);
    }
}
