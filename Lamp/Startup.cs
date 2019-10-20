using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Lamp.Implementations;
using Lamp.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Devices.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Lamp
{
  public class Startup
  {

    // This method gets called by the runtime. Use this method to add services to the container.
    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddSingleton(DeviceClient.CreateFromConnectionString(
        "{INSERT YOUR CONNECTION STRING HERE}"
        ));
      services.AddSingleton<LampImplementation>();
      services.AddSingleton(_ => new LampState
      {
        Position = Position.Off
      });

      services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime applicationLifetime)
    {

      applicationLifetime.ApplicationStarted.Register(async () =>
      {
        var lampImplementation = app.ApplicationServices.GetService<LampImplementation>();
        await lampImplementation.Init();
      });
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseCors(cors =>
      {
        cors.AllowAnyHeader();
        cors.AllowAnyMethod();
        cors.AllowAnyOrigin();
      });
      app.UseRouting();
      app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
  }
}
