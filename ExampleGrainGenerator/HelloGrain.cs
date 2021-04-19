using System;
using System.Threading.Tasks;
using ExampleGrainGenerator;
using Proto;
using static System.Threading.Tasks.Task;

#pragma warning disable 1998

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
        return CompletedTask;
    }

    
    public override Task SayHello(HelloRequest request, Action<IContext, HelloResponse> respond,
        Action<IContext, string> onError)
    {
        if (new Random().NextDouble() > .5)
        {
            Context.ReenterAfter(Task.Delay(100), () => respond(Context, new HelloResponse
            {
                Message = $"Reentrant hello {request.Name}!"
            }));
            return CompletedTask;
        }
        else return base.SayHello(request, respond, onError);
    }

    public override async Task<HelloResponse> SayHello(HelloRequest request) =>
        new()
        {
            Message = $"Hello {request.Name}, pretty cool, right?"
        };

    public override async Task<GetCurrentStateResponse> GetCurrentState(GetCurrentStateRequest request) =>
        new()
        {
            State = _state
        };
}