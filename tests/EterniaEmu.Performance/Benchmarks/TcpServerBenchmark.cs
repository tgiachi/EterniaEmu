using System.Net;
using System.Net.Sockets;
using BenchmarkDotNet.Attributes;
using EterniaEmu.Core.Config;
using EterniaEmu.Network.Consts;
using EterniaEmu.Network.Implementation.Server;
using EterniaEmu.Network.Internal;
using EterniaEmu.Network.Packets;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Serilog;

namespace EterniaEmu.Performance.Benchmarks;

public class TcpServerBenchmark
{
    private EterniaEmuTcpServer _eterniaEmuTcpServer;


    public TcpServerBenchmark()
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .CreateLogger();
    }

    [GlobalSetup]
    public void Setup()
    {
        var loggerFactory = (ILoggerFactory)new LoggerFactory();
        loggerFactory.AddSerilog(Log.Logger);
        var logger = loggerFactory.CreateLogger<EterniaEmuTcpServer>();
        _eterniaEmuTcpServer = new EterniaEmuTcpServer(
            logger,
            new OptionsWrapper<ServerConfig>(
                new ServerConfig()
                {
                }
            ),
            new List<NetworkPacketDefinition>()
            {
                new NetworkPacketDefinition(PacketTypeEnum.LoginSeed, typeof(LoginSeedPacket))
            }
        );

        _eterniaEmuTcpServer.Start();
    }

    [Benchmark]
    public async Task<int> TcpConnection()
    {
        using var client = new TcpClient();
        await client.ConnectAsync(IPAddress.Loopback, 2593);
        await client.Client.SendAsync(GenerateLoginSeedPacket());
        client.Close();

        return 0;
    }

    private static byte[] GenerateLoginSeedPacket()
    {
        var packet = new byte[22];
        packet[0] = (byte)PacketTypeEnum.LoginSeed;
        for (var i = 1; i < 21; i++)
        {
            packet[i] = (byte)i;
        }

        return packet;
    }
}
