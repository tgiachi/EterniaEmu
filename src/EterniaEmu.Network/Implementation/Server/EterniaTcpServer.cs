using System.Net;
using NetCoreServer;
using Serilog;

namespace EterniaEmu.Network.Implementation.Server;

public class EterniaTcpServer : TcpServer
{
    private readonly ILogger _logger = Log.ForContext<EterniaTcpServer>();

    public EterniaTcpServer(IPAddress address, int port) : base(address, port)
    {

    }

    protected override TcpSession CreateSession() => new EterniaTcpSession(this);
}
