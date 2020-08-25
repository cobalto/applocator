using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppLocator.Application.Repositories
{
    public interface IApplicationRepository
    {
        Task<List<Domain.Application>> GetApplicationList();

        Task<Domain.Application> GetApplication(int applicationId);

        Task<Domain.Application> SaveApplication(Domain.Application application);

        Task<Domain.Application> UpdateApplication(int applicationId, Domain.Application application);

        Task DeleteApplication(int applicationId);
    }
}
