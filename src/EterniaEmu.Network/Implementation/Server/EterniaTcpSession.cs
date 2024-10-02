using NetCoreServer;
using Serilog;
using Serilog.Core;

namespace EterniaEmu.Network.Implementation.Server;

public class EterniaTcpSession : TcpSession
{

    private readonly ILogger _logger = Log.ForContext<EterniaTcpSession>();
    protected EterniaTcpServer EterniaTcpServer => (EterniaTcpServer)Server;

    public EterniaTcpSession(EterniaTcpServer server) : base(server)
    {

    }

    protected override void OnReceived(byte[] buffer, long offset, long size)
    {
        var opCode = buffer[0];
        // format byte to hex
        _logger.Information("Received opCode: {OpCode}", opCode.ToString("X2"));


        base.OnReceived(buffer, offset, size);
    }
}
