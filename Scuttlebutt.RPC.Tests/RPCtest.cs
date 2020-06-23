using System;
using Xunit;
using Scuttlebutt.RPC;

namespace Scuttlebutt.RPC.Tests
{
    public class ScRPC
    {
        [Fact]
        public void ItSerializes()
        {
            var rpc = RPC.CreateHistoryStream("1");
            Console.WriteLine(rpc.GetArg());
            throw new Exception(rpc.Serialize());
        }
    }
}
