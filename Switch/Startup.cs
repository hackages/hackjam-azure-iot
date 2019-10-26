using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Devices.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Switch.Implementations;

namespace Switch
{
  public class Startup
  {
    // This method gets called by the runtime. Use this method to add services to the container.
    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
    public void ConfigureServices(IServiceCollection services)
    {

      services.AddSingleton(
          DeviceClient.CreateFromConnectionString(
              "HostName=hj-azure-iot-hub.azure-devices.net;DeviceId=switch;SharedAccessKey=Uf8M7BJvOnYotA/Okn0Tan9mg7kaoh+udkR6q+t4bpA="
              ));

      services.AddScoped<SwitchImplementation>();
      services.AddControllers();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseCors(policies =>
      {
        policies.AllowAnyHeader();
        policies.AllowAnyMethod();
        policies.AllowAnyOrigin();
      });
      app.UseRouting();
      app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
  }
}
