using EterniaEmu.Core.Data;
using EterniaEmu.Network.Interfaces.Packets;
using EterniaEmu.Network.Interfaces.Server;
using NetCoreServer;
using Serilog;


namespace EterniaEmu.Network.Implementation.Server;

public class EterniaTcpSession : TcpSession
{
    private readonly ILogger _logger = Log.ForContext<EterniaTcpSession>();

    private readonly IEterniaEmuTcpServer _eterniaEmuTcpServer;


    public int Seed { get; set; }
    public bool IsSeeded => Seed != 0;

    public ClientVersionData ClientVersionData { get; set; }

    protected EterniaTcpServer EterniaTcpServer => (EterniaTcpServer)Server;


    public EterniaTcpSession(EterniaTcpServer server, IEterniaEmuTcpServer eterniaEmuTcpServer) : base(server)
    {
        _eterniaEmuTcpServer = eterniaEmuTcpServer;
    }

    public void SendPackets(params INetworkPacket[] packets)
    {
        foreach (var packet in packets)
        {
            SendPacket(packet);
        }
    }

    public void SendPacket(INetworkPacket packet)
    {
        var buffer = new byte[packet.Size + 1];

        if (packet.OpCode == -1)
        {
            _logger.Warning("[{Id}] OpCode is not set for packet: {PacketType}", Id, packet.GetType().Name);
            return;
        }

        buffer[0] = (byte)packet.OpCode;


        packet.Write().CopyTo(buffer, 1);


        var result = SendAsync(buffer);

        _logger.Debug(
            "[{Id}] >> Sent ({Result}) opCode: 0x{OpCode} total bytes: {Total}",
            Id,
            result,
            packet.OpCode.ToString("X2"),
            buffer.Length
        );
    }

    protected override void OnReceived(byte[] buffer, long offset, long size)
    {
        long bufferPosition = 0;

        while (bufferPosition < size)
        {
            var opCode = buffer[bufferPosition];

            _logger.Debug("[{Id}] << Received opCode: 0x{OpCode}", Id, opCode.ToString("X2"));

            var packet = EterniaTcpServer.CreatePacket(opCode);

            if (packet == null)
            {
                _logger.Warning("[{Id}] !<< Unknown packet type: 0x{OpCode}", Id, opCode.ToString("X2"));
                return;
            }

            int packetSize = packet.Size;

            if (bufferPosition + packetSize > size)
            {
                _logger.Warning("[{Id}] !<< Incomplete packet received, waiting for more data...", Id);
                break;
            }

            byte[] packetData = buffer.Skip((int)bufferPosition).Take(packetSize).ToArray();

            _logger.Debug("[{Id}] // Found packet type: {PacketType}", Id, packet.GetType().Name);


            packet.Read(packetData);


            _eterniaEmuTcpServer.DispatchPacketAsync(this, packet);

            bufferPosition += packetSize;
        }


        base.OnReceived(buffer, offset, size);
    }
}
