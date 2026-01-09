using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlocks.Messaging.MassTransit;

public static class MessageBrokerSettings
{
    public static IServiceCollection AddMessageBroker(this IServiceCollection services, Assembly? assembly = null)
    {
        // impl rabbitmq masstransit config
        return services;
    }
}
