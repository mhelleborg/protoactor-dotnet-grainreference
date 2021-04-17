using System;
using System.Threading.Tasks;
using Hello.Messages;
using Proto;
using Proto.Cluster;
using static System.Threading.Tasks.Task;

namespace ConsoleApp13
{
    class HelloGrain : HelloGrainBase
    {
        private HelloGrainState _state;

        public HelloGrain(IContext ctx) : base(ctx)
        {
        }

        public override Task OnStarted()
        {
            _state = new HelloGrainState();
            Console.WriteLine("Started");
            return base.OnStarted();
        }

        public override Task<HelloResponse> SayHello(HelloRequest request) =>
            FromResult(new HelloResponse()
            {
                Message = "Pretty cool!"
            });

        public override Task<GetCurrentStateResponse> GetCurrentState(GetCurrentStateRequest request) =>
            FromResult(new GetCurrentStateResponse()
            {
                State = _state
            });
    }
}