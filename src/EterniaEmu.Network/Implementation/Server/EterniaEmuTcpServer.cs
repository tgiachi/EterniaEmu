using System.Net;
using EterniaEmu.Core.Config;
using EterniaEmu.Network.Interfaces.Server;
using EterniaEmu.Network.Internal;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;


namespace EterniaEmu.Network.Implementation.Server;

public class EterniaEmuTcpServer : IEterniaEmuTcpServer
{
    private readonly ILogger _logger;

    private readonly EterniaTcpServer _eterniaTcpServer;

    private readonly ServerConfig _serverConfig;

    private readonly List<NetworkPacketDefinition> _networkPacketDefinitions;

    public EterniaEmuTcpServer(
        ILogger<EterniaEmuTcpServer> logger, IOptions<ServerConfig> serverConfig,
        List<NetworkPacketDefinition> networkPacketDefinitions
    )
    {
        _logger = logger;
        _networkPacketDefinitions = networkPacketDefinitions;
        _serverConfig = serverConfig.Value;
        _eterniaTcpServer = new EterniaTcpServer(IPAddress.Parse(_serverConfig.Host), _serverConfig.Port);
        SetupPackets();
    }

    private void SetupPackets()
    {
        _logger.LogDebug("Setting up network packets");

        foreach (var networkPacketDefinition in _networkPacketDefinitions)
        {
            _eterniaTcpServer.AddPacketType(
                (byte)networkPacketDefinition.PacketType,
                networkPacketDefinition.PacketTypeClass
            );
        }
    }

    public void Start()
    {
        _logger.LogInformation("Starting Eternia on {Host}:{Port}", _serverConfig.Host, _serverConfig.Port);
        _eterniaTcpServer.Start();
    }
}
