// See https://aka.ms/new-console-template for more information


using EterniaEmu.Network.Consts;
using EterniaEmu.Network.Implementation.Server;
using EterniaEmu.Network.Packets;
using Serilog;


Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .CreateLogger();


var server = new EterniaTcpServer(System.Net.IPAddress.Any, 2593);


server.AddPacketType(PacketTypeEnum.LoginSeed, typeof(LoginSeedPacket));

server.Start();


while (true)
{
    System.Threading.Thread.Sleep(1000);
}
