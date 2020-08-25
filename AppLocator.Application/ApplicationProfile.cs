using AppLocator.Application.Models;
using AppLocator.Domain;
using AutoMapper;
using System;

namespace AppLocator.Application
{
    public class ApplicationProfile : Profile
    {
        public ApplicationProfile()
        {
            CreateMap<Domain.Application, ApplicationModel>()
               .ForMember(app => app.Application, opt => opt.MapFrom(i => i.ApplicationId.Value))
               .ForMember(app => app.Url, opt => opt.MapFrom(u => u.Url.ToString()))
               .ForMember(app => app.PathLocal, opt => opt.MapFrom(p => p.Path.ToString()))
               .ForMember(app => app.DebuggingMode, opt => opt.MapFrom(d => d.Mode.IsDebugging));

            CreateMap<ApplicationModel, Domain.Application>()
               .ForMember(up => up.ApplicationId, opt => opt.MapFrom(i => new AppId(i.Application)))
               .ForMember(up => up.Url, opt => opt.MapFrom(u => new AppUrl(new Uri(u.Url))))
               .ForMember(up => up.Path, opt => opt.MapFrom(p => new AppPath(p.PathLocal)))
               .ForMember(up => up.Mode, opt => opt.MapFrom(m => new AppMode(m.DebuggingMode ? AvailableMode.Debug : AvailableMode.Release)));
        }
    }
}
