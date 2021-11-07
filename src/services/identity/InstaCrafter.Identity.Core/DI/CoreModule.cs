using Autofac;
using InstaCrafter.Identity.Core.Interfaces.UseCases;
using InstaCrafter.Identity.Core.UseCases;

namespace InstaCrafter.Identity.Core.DI
{
    public class CoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<RegisterUserUseCase>().As<IRegisterUserUseCase>();
            builder.RegisterType<LoginUseCase>().As<ILoginUseCase>();
            builder.RegisterType<ExchangeRefreshTokenUseCase>().As<IExchangeRefreshTokenUseCase>();
        }
    }
}
