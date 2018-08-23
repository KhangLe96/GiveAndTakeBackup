using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Giveaway.API
{
    public class Program
    {
        public static int Main(string[] args)
        {
            try
            {
                BuildWebHost(args).Run();
                return 0;
            }
            catch (Exception ex)
            {
                return 1;
            }
            finally
            {
            }
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseKestrel()
                .UseUrls("http://*:8089")
                .Build();
    }
}
