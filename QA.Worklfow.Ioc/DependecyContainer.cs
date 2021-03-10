using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using QA.Framework.DataEntities;
using QA.Framework.DataEntities.Entities;
using QA.Workflow.Business.Admin;
using QA.Workflow.Business.Interfaces.Admin;
using QR.Workflow.Infrastructure.Dapper;
using QR.Workflow.Infrastructure.DataContext;
using QR.Workflow.Infrastructure.Repository;
using QR.Workflow.Infrastructure.UnitOfWork;

namespace QA.Framework.Ioc
{
    public static class DependecyContainer
    {
        public static void RegisterServices(this IServiceCollection services, string connection)
        {
            Configure(services, connection);
        }

        public static void Configure(IServiceCollection services, string connection)
        {
            services.AddDbContext<WorkflowDataContext>(options => options.UseSqlServer(connection));
            services.AddScoped<IDataContext, WorkflowDataContext>();
            services.AddScoped<IDapperExtension>(d => new DapperExtension(connection));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            SeviceDiConfigure(services);
            RepositoryDiConfigure(services);
        }

        private static void SeviceDiConfigure(IServiceCollection services)
        {
            services.AddScoped<IMenuService, MenuService>();
        }

        private static void RepositoryDiConfigure(IServiceCollection services)
        {
            services.AddScoped<IRepository<MenuMaster>, Repository<MenuMaster>>();
        }
    }
}