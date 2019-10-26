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
          "Endpoint=sb://iothub-ns-hj-azure-i-2337796-0babef8699.servicebus.windows.net/;SharedAccessKeyName=iothubowner;SharedAccessKey=6hjDeq0qk1ViwtTqW6VnrqScdNWn7Hc0YIS5+xlx+Us=;EntityPath=hj-azure-iot-hub");
      _service = ServiceClient.CreateFromConnectionString(
          "HostName=hj-azure-iot-hub.azure-devices.net;SharedAccessKeyName=service;SharedAccessKey=QudOiHoCF90/iTVqdZLejZYMoLHEHdNfRUsEaQWAYKg=");
      var receivers = await eventHub
          .GetRuntimeInformationAsync()
          .ContinueWith(task => task.Result.PartitionIds, cancelletation.Token)
          .ContinueWith(task => task.Result.Select(
              partitionId => eventHub.CreateReceiver("$Default", partitionId, EventPosition.FromEnd())
          ), cancelletation.Token);

      receivers.ToList().ForEach(receiver => receiver.SetReceiveHandler(new Program()));

      
      
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
      events = events as EventData[] ?? events.ToArray();

      events
          .ToList()
          .ForEach(eventData =>
          {
            var jObject = JObject.FromObject(eventData);
            jObject["Body"] = Encoding.UTF8.GetString(eventData.Body.Array);
            Console.WriteLine($"Event Data : {jObject}");

            var isSwitchOn = JsonConvert.DeserializeObject<bool>(Encoding.UTF8.GetString(eventData.Body.Array));

            var turnOnMethod = new CloudToDeviceMethod("TurnOn");
            turnOnMethod.SetPayloadJson("{ message: 'Allume toi!'}");

            var turnOffMethod = new CloudToDeviceMethod("TurnOff");
            turnOffMethod.SetPayloadJson("{message:'Éteint toi!'}");

            _service.InvokeDeviceMethodAsync("lamp", isSwitchOn ? turnOnMethod : turnOffMethod);
          });

      Console.WriteLine();
      Console.WriteLine("Batch Completed !");
      Console.WriteLine();

      return Task.CompletedTask;
    }

    public Task ProcessErrorAsync(Exception error)
    {
      Console.WriteLine(error);
      return Task.CompletedTask;
    }

    public int MaxBatchSize { get; set; } = 10;
  }
}
