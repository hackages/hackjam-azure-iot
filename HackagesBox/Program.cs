using System.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.Amqp.Serialization;
using Microsoft.Azure.Devices;

namespace HackagesBox
{
  public class Program
  {
    public static async Task Main(string[] args)
    {
      using var service = ServiceClient.CreateFromConnectionString("HostName=hj-azure-iot-hub.azure-devices.net;SharedAccessKeyName=service;SharedAccessKey=QudOiHoCF90/iTVqdZLejZYMoLHEHdNfRUsEaQWAYKg=");
      using var registry = RegistryManager.CreateFromConnectionString("HostName=hj-azure-iot-hub.azure-devices.net;SharedAccessKeyName=service;SharedAccessKey=QudOiHoCF90/iTVqdZLejZYMoLHEHdNfRUsEaQWAYKg=");

      while (true)
      {
        Console.WriteLine("Welcome to the Hackages Box App !");

        Console.WriteLine("1) Play Sound");
        Console.WriteLine("2) Toggle Lamp");
        Console.Write("Choose an option : ");

        var option = Console.ReadLine();
        option = option.Trim();

        switch (option)
        {
          case "1":
            await DisplaySoundsMenu(registry, service);
            break;
          case "2":
            await ToggleLamp(registry);
            break;
          default:
            Console.WriteLine("Choose an available option");
            break;
        }

        await Task.Delay(1500);
        Console.Clear();
      }
    }

    private static Task ToggleLamp(RegistryManager registry)
    {
      throw new NotImplementedException();
    }

    private static Task PlaySound(ServiceClient service, CloudToDeviceMethod method)
    {
      throw new NotImplementedException();
    }

    private static Task DisplaySoundsMenu(RegistryManager registry, ServiceClient client)
    {
      throw new NotImplementedException();
    }
  }
}
