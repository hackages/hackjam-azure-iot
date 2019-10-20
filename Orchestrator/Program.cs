using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Colorful;
using Microsoft.Azure.Devices;
using Microsoft.Azure.EventHubs;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Console = Colorful.Console;

namespace Orchestrator
{
  public class Program : IPartitionReceiveHandler
  {
    private static ServiceClient _service;
    static async Task Main(string[] args)
    {

      var cancelletation = new CancellationTokenSource();
      var eventHub = EventHubClient.CreateFromConnectionString(
          "{INSERT CONNECTION STRING HERE}");
      _service = ServiceClient.CreateFromConnectionString(
          "{INSERT CONNECTION STRING HERE}");


      Console.WriteLine(new Figlet(FigletFont.Load("doh.flf")).ToAscii("ORCHESTRATOR"));
      Console.WriteLine("Press Enter to finish...");

      if (Console.ReadKey(true).Key == ConsoleKey.Enter)
      {
        Console.WriteLine("Finishing...");
        cancelletation.Cancel();
        cancelletation.Dispose();
        await Task.Delay(1000, CancellationToken.None);
        Console.WriteLine("Finished...");
      }

    }

    public Task ProcessEventsAsync(IEnumerable<EventData> events)
    {
      throw new NotImplementedException();
    }

    public Task ProcessErrorAsync(Exception error)
    {
      return Task.CompletedTask;
    }

    public int MaxBatchSize { get; set; } = 10;
  }
}
