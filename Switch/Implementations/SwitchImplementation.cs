using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;

namespace Switch.Implementations
{
    public class SwitchImplementation
    {
        private readonly DeviceClient _client;

        public SwitchImplementation(DeviceClient client)
        {
            _client = client;
        }

        public async Task HandleToggle(bool switchPosition)
        {
            var message =
                new Message(
                    Encoding.UTF8.GetBytes(
                        JsonConvert.SerializeObject(switchPosition)
                    ));

            await _client.SendEventAsync(message);
        }
    }
}