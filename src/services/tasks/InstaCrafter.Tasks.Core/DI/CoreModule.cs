using Autofac;
using InstaCrafter.Tasks.Core.Interfaces.UseCases;
using InstaCrafter.Tasks.Core.UseCases;

namespace InstaCrafter.Tasks.Core.DI
{
    public class CoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AddIgAccountUseCase>().As<IAddIgAccountUseCase>();
        }
    }
}
