using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using QA.Framework.DataEntities.Entities;
using QA.Workflow.Business.Transfers.Admin;
using System;

namespace QA.Framework.Ioc
{
    public static class AutoMapperConfig
    {
        public static void AddAutoMapperConfiguration(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddAutoMapper(typeof(DomainToViewModelMappingProfile));
        }

        public class DomainToViewModelMappingProfile : Profile
        {
            public DomainToViewModelMappingProfile()
            {
                CreateMap<MenuMaster, MenuModel>().ReverseMap();
            }
        }
    }
}