using AppLocator.Application.Models;
using AppLocator.Application.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppLocator.Application.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly IApplicationRepository _applicationRepository;
        private readonly IMapper _mapper;

        public ApplicationService(IApplicationRepository applicationRepository, IMapper mapper)
        {
            _applicationRepository = applicationRepository;
            _mapper = mapper;
        }

        public async Task<List<ApplicationModel>> GetApplicationList()
        {
            var applicationList = (await _applicationRepository.GetApplicationList()).Select(app => _mapper.Map<ApplicationModel>(app));

            return applicationList.ToList();
        }

        public async Task<ApplicationModel> GetApplication(int applicationId)
        {
            var application = _mapper.Map<ApplicationModel>(await _applicationRepository.GetApplication(applicationId));

            return application;
        }

        public async Task<ApplicationModel> SaveApplication(ApplicationModel application)
        {
            var newApplication = _mapper.Map<Domain.Application>(application);

            var applicationResult = _mapper.Map<ApplicationModel>(await _applicationRepository.SaveApplication(newApplication));

            return applicationResult;
        }

        public async Task<ApplicationModel> UpdateApplication(int applicationId, JsonPatchDocument<ApplicationModel> patchs)
        {
            var applicationOriginal = await _applicationRepository.GetApplication(applicationId);

            var applicationToUpdate = _mapper.Map<ApplicationModel>(applicationOriginal);

            patchs.ApplyTo(applicationToUpdate);

            var applicationUpdated = _mapper.Map<Domain.Application>(applicationToUpdate);

            var applicationResult = _mapper.Map<ApplicationModel>(await _applicationRepository.UpdateApplication(applicationId, applicationUpdated));

            return applicationResult;
        }

        public async Task DeleteApplication(int applicationId)
        {
            await _applicationRepository.DeleteApplication(applicationId);
        }
    }
}
