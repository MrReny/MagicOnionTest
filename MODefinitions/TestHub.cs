using System.Collections.Generic;
using System.Threading.Tasks;
using MagicOnion;
using MagicOnion.Server.Hubs;
using MessagePack;

namespace MOShared
{
    public interface ITestHubReceiver
    {

    }

    public interface ITestHub:IStreamingHub<ITestHub, ITestHubReceiver>
    {
        public Task<TestObject[]> GetTestObjects(Dictionary<int, string> dict);
    }

    [MessagePackObject]
    public class TestObject
    {
        [Key(0)]
        public int Id { get; set; }

        [Key(1)]
        public string Value { get; set; }

        public TestObject(int id, string value)
        {
            Id = id;
            Value = value;
        }
    }
}