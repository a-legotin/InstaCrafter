using Autofac;
using InstaCrafter.Identity.Core.Interfaces.Gateways.Repositories;
using InstaCrafter.Identity.Core.Interfaces.Services;
using InstaCrafter.Identity.Infrastructure.Auth;
using InstaCrafter.Identity.Infrastructure.Data.Repositories;
using InstaCrafter.Identity.Infrastructure.Interfaces;

namespace InstaCrafter.Identity.Infrastructure.DI
{
    public class InfrastructureModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UserRepository>().As<IUserRepository>().InstancePerLifetimeScope();
            builder.RegisterType<JwtFactory>().As<IJwtFactory>();
            builder.RegisterType<JwtTokenHandler>().As<IJwtTokenHandler>();
            builder.RegisterType<TokenFactory>().As<ITokenFactory>();
            builder.RegisterType<JwtTokenValidator>().As<IJwtTokenValidator>();
        }
    }
}
