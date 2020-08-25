using AppLocator.Application.Repositories;
using AppLocator.Infrastructure.Entities;
using AutoMapper;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppLocator.Infrastructure.Repositories
{
    public class ApplicationRepository : IApplicationRepository
    {
        private readonly Context _context;
        private readonly IMapper _mapper;

        public ApplicationRepository(Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<Domain.Application>> GetApplicationList()
        {
            var applications = await _context.Application.FindAsync(new BsonDocument());

            var applicationList = (await _context.Application.FindAsync(new BsonDocument())).ToList();

            return applicationList.Select(app => _mapper.Map<Domain.Application>(app)).ToList();
        }

        public async Task<Domain.Application> GetApplication(int applicationId)
        {
            var applicationResult = await _context.Application.Find(e => e.Application == applicationId).SingleOrDefaultAsync();

            return _mapper.Map<Domain.Application>(applicationResult);
        }

        public async Task<Domain.Application> SaveApplication(Domain.Application application)
        {
            var newApplication = _mapper.Map<ApplicationDocument>(application);

            await _context.Application.InsertOneAsync(newApplication);

            return _mapper.Map<Domain.Application>(newApplication);
        }

        public async Task<Domain.Application> UpdateApplication(int applicationId, Domain.Application application)
        {
            var idToBeUpdated = await _context.Application.Find(e => e.Application == applicationId).Project(app => app.Id).SingleOrDefaultAsync();

            var applicationEdited = _mapper.Map<ApplicationDocument>(application);
            applicationEdited.Id = idToBeUpdated;

            var result = await _context.Application.ReplaceOneAsync(app => app.Application == applicationId, applicationEdited);

            return _mapper.Map<Domain.Application>(applicationEdited);
        }

        public async Task DeleteApplication(int applicationId)
        {
            await _context.Application.DeleteOneAsync(app => app.Application == applicationId);
        }
    }
}
