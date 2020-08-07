using Autofac;
using CleanArchitecture.Web;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace CleanArchitecture.FunctionalTests
{
    public class TestStartup : Startup
    {
        public TestStartup(IConfiguration config, IWebHostEnvironment env) : base(config, env)
        {
        }

        public override void ConfigureContainer(ContainerBuilder builder) =>
            builder.RegisterModule<TestInfrastructureModule>();
    }
}