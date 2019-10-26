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

        private static async Task ToggleLamp(RegistryManager registry)
        {
            var twin = await registry.GetTwinAsync("hackages-box");
            twin.Properties.Desired["state"]["isOn"] = !bool.Parse(twin.Properties.Desired["state"]["isOn"].ToString());
            await registry.UpdateTwinAsync("hackages-box", twin, twin.ETag);
        }

        private static async Task PlaySound(ServiceClient service, CloudToDeviceMethod method)
        {
            await service.InvokeDeviceMethodAsync("hackages-box", method);
        }

        private static async Task DisplaySoundsMenu(RegistryManager registry, ServiceClient client)
        {
            var list = new List<string> {"Offset"};
            var twin = await registry.GetTwinAsync("hackages-box");
            Console.Clear();
            var count = 1;
            foreach (var method in twin.Properties.Desired["methods"])
            {
                list.Add(method.Key);
                Console.WriteLine($"{count++}) {method.Key}");
            }

            Console.Write("Choose an available option");
            var option = Console.ReadLine();
            if (int.TryParse(option, out var index))
            {
                await PlaySound(client, new CloudToDeviceMethod(list[index]));
            }
        }
    }
}
