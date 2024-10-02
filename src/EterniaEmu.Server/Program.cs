// See https://aka.ms/new-console-template for more information


using EterniaEmu.Network.Implementation.Server;
using Serilog;


Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .CreateLogger();


var server = new EterniaTcpServer( System.Net.IPAddress.Any, 2593 );


server.Start();


while (true)
{
    System.Threading.Thread.Sleep(1000);
}
