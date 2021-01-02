using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatavAPI.Models;

namespace DatavAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Backend.Instance.Init("CoT_Datav", new TimeSpan(0, 0, 0, 5));
            Backend.Instance.Simulator().Start();
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => {
                    webBuilder.UseUrls("http://localhost:5004").
                    UseStartup<Startup>();
                });
    }
}
