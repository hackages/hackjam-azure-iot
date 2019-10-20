using System;
using System.Threading.Tasks;
using Lamp.Model;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Azure.Devices.Client;
using Microsoft.Azure.Devices.Shared;

namespace Lamp.Implementations
{
  public class LampImplementation
  {
    private readonly DeviceClient _client;
    private readonly LampState _state;

    public LampImplementation(DeviceClient client, LampState state)
    {
      _client = client;
      _state = state;
    }

    public Task Init()
    {
      throw new NotImplementedException();
    }

    private Task Callback(TwinCollection desiredproperties, object usercontext)
    {
      throw new NotImplementedException();
    }

    private Task<MethodResponse> TurnOn(MethodRequest _, object context)
    {
      throw new NotImplementedException();
    }

    private Task<MethodResponse> TurnOff(MethodRequest _, object context)
    {
      throw new NotImplementedException();
    }
  }
}