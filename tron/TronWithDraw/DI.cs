using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Withdraw.Cash.Application.Abstractions;
using Withdraw.Cash.Application.Implementations;

namespace Withdraw.Cash.Application;
public static class DI
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddSingleton(typeof(IRabbitMQPublisher<>), typeof(RabbitMQPublisher<>));

        services.AddMediatR(x => x.RegisterServicesFromAssemblies(typeof(DI).Assembly, Assembly.GetExecutingAssembly()));

        services.AddValidatorsFromAssembly(typeof(DI).Assembly);

        return services;
    }
}
