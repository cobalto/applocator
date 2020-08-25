using AppLocator.Domain;
using AppLocator.Infrastructure.Entities;
using AutoMapper;
using System;

namespace AppLocator.Infrastructure
{
    public class InfrastructureProfile : Profile
    {
        public InfrastructureProfile()
        {
            CreateMap<Domain.Application, ApplicationDocument>()
               .ForMember(app => app.Application, opt => opt.MapFrom(i => i.ApplicationId.Value))
               .ForMember(app => app.Url, opt => opt.MapFrom(u => u.Url.ToString()))
               .ForMember(app => app.PathLocal, opt => opt.MapFrom(p => p.Path.ToString()))
               .ForMember(app => app.DebuggingMode, opt => opt.MapFrom(d => d.Mode.IsDebugging));

            CreateMap<ApplicationDocument, Domain.Application>()
               .ForMember(up => up.ApplicationId, opt => opt.MapFrom(u => new AppId(u.Application)))
               .ForMember(up => up.Url, opt => opt.MapFrom(u => new AppUrl(new Uri(u.Url))))
               .ForMember(up => up.Path, opt => opt.MapFrom(p => new AppPath(p.PathLocal)))
               .ForMember(up => up.Mode, opt => opt.MapFrom(m => new AppMode(m.DebuggingMode ? AvailableMode.Debug : AvailableMode.Release)));
        }
    }
}
