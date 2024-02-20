using Dapper;
using Partners.DataAccess.Data;
using Partners.DataAccess.DbAccess;
using Partners.DataAccess.Utility;
using Serilog;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace Partners
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            container.RegisterType<ISqlDataAccess, SqlDataAccess>();
            container.RegisterType<IPartnerRepository, PartnerRepository>();
            container.RegisterType<IPolicyRepository, PolicyRepository>();
            SqlMapper.AddTypeHandler(new PartnerTypeHandler());
            Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console()
            .CreateLogger();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}