using NetCoreServer;

namespace EterniaEmu.Network.Implementation.Server;

public class EterniaTcpSession : TcpSession
{
    protected EterniaTcpServer EterniaTcpServer => (EterniaTcpServer)Server;

    public EterniaTcpSession(EterniaTcpServer server) : base(server)
    {
    }
}
