using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using MagicOnion.Client;
using MOShared;

namespace MOClient
{
    internal class Program
    {
        public static async Task Main(string[] args)
        {
            try
            {
                Environment.SetEnvironmentVariable("GRPC_VERBOSITY", "DEBUG");
                Environment.SetEnvironmentVariable("GRPC_TRACE", "cares_resolver,cares_address_sorting");

                var client = new TestHubClient();

                await client.ConnectAsync(new Channel("127.0.0.1", 16752, ChannelCredentials.Insecure));

                var en = Enumerable.Range(0, 1000).ToDictionary(n => n, n => n.ToString());

                var res = await client.GetTestObjects(en);

                Console.WriteLine(res);
                foreach (var to in res)
                {
                    Console.WriteLine(to.Id + " " + to.Value);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }
    }

    public class TestHubClient : ITestHubReceiver
    {
        public ITestHub TestHub;

        public async Task ConnectAsync(Channel grpcChannel)
        {
            TestHub = await StreamingHubClient.ConnectAsync<ITestHub, ITestHubReceiver>(grpcChannel, this);
        }

        public async Task<TestObject[]> GetTestObjects(Dictionary<int, string> dict)
        {
            return await TestHub.GetTestObjects(dict);
        }
    }
}