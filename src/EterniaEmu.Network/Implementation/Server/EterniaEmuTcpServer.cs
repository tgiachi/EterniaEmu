using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using System.Threading.Channels;
using EterniaEmu.Network.Interfaces.Server;
using Serilog;

namespace EterniaEmu.Network.Implementation.Server;

public class EterniaEmuTcpServer : IEterniaEmuTcpServer
{
    private readonly ILogger _logger = Log.ForContext<EterniaEmuTcpServer>();
    private readonly TcpListener _listener;
    private readonly Channel<(TcpClient, byte[])> _channel;

    private readonly ConcurrentDictionary<string, TcpClient> _clients = new();

    public EterniaEmuTcpServer(string ip, int port)
    {
        _listener = new TcpListener(IPAddress.Parse(ip), port);
        _channel = Channel.CreateUnbounded<(TcpClient, byte[])>();
    }

    public Task StartAsync()
    {
        _logger.Information(
            "Starting server to IP: {IP} and Port: {Port}",
            _listener.LocalEndpoint.ToString(),
            ((IPEndPoint)_listener.LocalEndpoint).Port
        );

        _listener.Start();


        _ = Task.Run(AcceptClientsAsync);


        return Task.CompletedTask;
    }

    public Task StopAsync()
    {
        _logger.Information("Stopping server");

        _listener.Stop();

        return Task.CompletedTask;
    }

    private async Task AcceptClientsAsync()
    {
        while (true)
        {
            var client = await _listener.AcceptTcpClientAsync();

            _clients.TryAdd(((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString(), client);

            _logger.Information("Client connected from {IP}", ((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString());


            _ = Task.Run(() => HandleClientAsync(client));
        }
    }

    private async Task HandleClientAsync(TcpClient client)
    {
        
    }
}
