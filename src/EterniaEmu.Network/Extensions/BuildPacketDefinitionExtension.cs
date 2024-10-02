using System.Reflection;
using EterniaEmu.Network.Attributes;
using EterniaEmu.Network.Data;

namespace EterniaEmu.Network.Extensions;

public static class BuildPacketDefinitionExtension
{
    public static NetworkPacketDefinition BuildPacketDefinition(this Type type)
    {
        var packetAttribute = type.GetCustomAttribute<NetworkPacketAttribute>();
        if (packetAttribute == null)
        {
            throw new Exception($"Type {type.Name} does not have a NetworkPacketAttribute");
        }

        var packetDefinition = new NetworkPacketDefinition
        {
            PacketId = packetAttribute.PacketId,
            PacketType = type
        };

        var fields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        foreach (var field in fields)
        {
            var fieldAttribute = field.GetCustomAttribute<PacketFieldAttribute>();
            if (fieldAttribute == null)
            {
                continue;
            }

            packetDefinition.Size += fieldAttribute.Size;
        }

        return packetDefinition;
    }
}
