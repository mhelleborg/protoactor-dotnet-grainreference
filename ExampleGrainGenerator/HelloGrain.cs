using System.Threading.Tasks;
using Cluster.HelloWorld.Messages;
using Proto;
using Proto.Cluster;
using static System.Threading.Tasks.Task;
namespace ConsoleApp13
{
    class HelloGrain : IHelloGrain
    {
        private readonly IContext _ctx;
        private readonly string _id;
        private HelloGrainState _state;

        public HelloGrain(IContext ctx)
        {
            _ctx = ctx;
            _id = ctx.ClusterIdentity()!.Identity;
            _state = new HelloGrainState();
        } 
        public Task<HelloResponse> SayHello(HelloRequest request) =>
            FromResult(new HelloResponse()
            {
                Message = "Pretty cool!"
            });

        public Task<GetCurrentStateResponse> GetCurrentState(GetCurrentStateRequest request) =>
            FromResult(new GetCurrentStateResponse()
            {
                State = _state
            });
    }
}