using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace InstaCrafter.API.Extensions
{
    internal static class MediatrServiceExtensions
    {
        public static void ConfigureMediatr(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
        }
    }
}