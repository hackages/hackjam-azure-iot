using System;
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

    public Task HandleToggle(bool switchPosition)
    {
      throw new NotImplementedException();
    }
  }
}