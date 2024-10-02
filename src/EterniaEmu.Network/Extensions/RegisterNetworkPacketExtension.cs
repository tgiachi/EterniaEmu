using System.Reflection;
using EterniaEmu.Core.Extensions;
using EterniaEmu.Network.Attributes;
using EterniaEmu.Network.Consts;
using EterniaEmu.Network.Internal;
using Microsoft.Extensions.DependencyInjection;

namespace EterniaEmu.Network.Extensions;

public static class RegisterNetworkPacketExtension
{
    public static IServiceCollection RegisterNetworkPacket(this IServiceCollection services, Type type)
    {
        var packetAttribute = type.GetCustomAttribute<NetworkPacketAttribute>();
        if (packetAttribute == null)
        {
            throw new Exception($"Packet {type.Name} does not have a NetworkPacketAttribute");
        }

        services.AddToRegisterTypedList(new NetworkPacketDefinition((PacketTypeEnum)packetAttribute.PacketId, type));
        return services;
    }

    public static IServiceCollection RegisterNetworkPacket<TPacket>(
        this IServiceCollection services
    )
    {
        var packetType = typeof(TPacket);
        var packetAttribute = packetType.GetCustomAttribute<NetworkPacketAttribute>();
        if (packetAttribute == null)
        {
            throw new Exception($"Packet {packetType.Name} does not have a NetworkPacketAttribute");
        }

        services.AddToRegisterTypedList(new NetworkPacketDefinition((PacketTypeEnum)packetAttribute.PacketId, packetType));
        return services;
    }
}
