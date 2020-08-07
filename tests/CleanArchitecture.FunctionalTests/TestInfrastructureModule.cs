using Autofac;
using CleanArchitecture.Infrastructure.Data;
using CleanArchitecture.SharedKernel.Interfaces;
using MediatR;
using Module = Autofac.Module;

namespace CleanArchitecture.FunctionalTests
{
    public class TestInfrastructureModule : Module
    {
        protected override void Load(ContainerBuilder builder) =>
            RegisterCommonDependencies(builder);

        private void RegisterCommonDependencies(ContainerBuilder builder)
        {
            builder.RegisterType<EfRepository>().As<IRepository>()
                .InstancePerLifetimeScope();

            builder.Register<ServiceFactory>(context =>
            {
                var c = context.Resolve<IComponentContext>();
                return t => c.Resolve(t);
            });
        }
    }
}