using System.Collections.Generic;
using System.Threading.Tasks;
using MagicOnion.Server.Hubs;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using MOShared;

namespace MOServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseUrls("https://+:16753", "http://+:16752");
                    webBuilder.UseStartup<Startup>();

                });
    }

    public class TestHub : StreamingHubBase<ITestHub, ITestHubReceiver>, ITestHub
    {
        public async Task<TestObject[]> GetTestObjects(Dictionary<int, string> dict)
        {
            var arr = new List<TestObject>();

            foreach (var variable in dict)
            {
                arr.Add(new TestObject(variable.Key, variable.Value));
            }

            return arr.ToArray();
        }
    }
}