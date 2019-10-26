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

        public async Task Init()
        {
            await _client.SetMethodHandlerAsync(nameof(TurnOn), TurnOn, null);
            await _client.SetMethodHandlerAsync(nameof(TurnOff), TurnOff, null);
            await _client.SetDesiredPropertyUpdateCallbackAsync(Callback, null);
        }

        private Task Callback(TwinCollection desiredproperties, object usercontext)
        {
            var property = desiredproperties["isOn"].ToString();
            var isOn = bool.Parse(property);
            if (isOn) TurnOn(null, null);
            else TurnOff(null, null);
            return Task.CompletedTask;
        }

        private Task<MethodResponse> TurnOn(MethodRequest _, object context)
        {
            _state.Position = Position.On;
            var twinCollection = new TwinCollection {["isOn"] = "true"};
            _client.UpdateReportedPropertiesAsync(twinCollection);
            return Task.FromResult(new MethodResponse(200));
        }

        private Task<MethodResponse> TurnOff(MethodRequest _, object context)
        {
            _state.Position = Position.Off;
            var twinCollection = new TwinCollection { ["isOn"] = "false" };
            _client.UpdateReportedPropertiesAsync(twinCollection);
            return Task.FromResult(new MethodResponse(200));
        }
    }
}