AZURE IOT:

Microsoft Azure:
Add your voucher to your account https://www.microsoftazurepass.com/
create your account on https://azure.microsoft.com/en-us/

EX1: Create a switch
doc: https://github.com/Azure-Samples/azure-iot-samples-csharp/blob/master/iot-hub/Samples/device/MessageSample/MessageSample.cs

- Send Message from device
- Check with telemetry.
- Check with VSCode.
  - Set-Up connection.

EX2: Turn on/off the lamp 
- Direct Method Call.
- send a method call to trigger the lamp.

EX3: Turn on/off the lamp 
doc: https://docs.microsoft.com/en-us/azure/iot-hub/iot-hub-devguide-device-twins#device-twins
- Use device Twin.
- get required and desired properties.
- Two ways of getting the desired and required properties .
  - From DeviceClient.
  - From RegistryManager.

EX4: Orchestrator
- use endpoint.
- listen to event-hub messages.
- trigger lamp when switch changed.

BONUS: Hackages BOX
CONNECTION_STRING: HostName=hj-azure-iot-hub.azure-devices.net;DeviceId=hackages-box;SharedAccessKey=sh3wEU2qDa4Oo8VVNLt0rRWfK3M2ozuYLTGuVdGDgvY=
- try to connect with the connection strings.
- retrieve informations about the device.
- with the informations try to do run actions (method).
