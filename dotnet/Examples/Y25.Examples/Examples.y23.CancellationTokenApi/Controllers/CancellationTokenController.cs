using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Examples.y23.CancellationTokenApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CancellationTokenController : ControllerBase
    {
        [HttpGet]
        public OkObjectResult Get()
        {
            return Ok("Api is up and running...");
        }

        [HttpGet("WithClientToken")]
        public bool GetWithClientToken(CancellationToken cancellationToken)
        {
            // Abort the request on browser and see the Visual Studio debugger
            var i = 0;
            while (!cancellationToken.IsCancellationRequested)
            {
                Console.WriteLine(i++);
            }
            return true;
        }

        [HttpGet("WithoutClientToken")]
        public bool GetWithoutClientToken()
        {
            // Abort the request on browser and see the Visual Studio debugger
            var i = 0;
            while (!HttpContext.RequestAborted.IsCancellationRequested)
            {
                Console.WriteLine(i++);
            }
            return true;
        }


        [HttpGet("WithTaskCancellationTokenSource")]
        public async Task<bool> GetWitCancellationTokenSource()
        {
            var cancellationTokenSource = new CancellationTokenSource();
            var cancellationToken = cancellationTokenSource.Token;

            var task = Task.Run(async () =>
            {
                cancellationToken.Register(() => Console.WriteLine("Operation is canceled"));

                var i = 0;
                while (!cancellationToken.IsCancellationRequested)
                {
                    Console.WriteLine(i++);
                    await Task.Delay(100, cancellationToken);
                }
            }, cancellationToken);


            await Task.Delay(5000, cancellationToken);
            await cancellationTokenSource.CancelAsync();

            try
            {
                await task;
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Task cancelled");
            }

            cancellationTokenSource.Dispose();
            return true;
        }


        [HttpGet("WithTaskThatNeverStops")]
        public string GetWithTaskThatNeverStops()
        {
            Task.Run(async () =>
            {
                var i = 0;
                while (true)
                {
                    Console.WriteLine(i++);
                    await Task.Delay(100);
                }
            });

            Thread.Sleep(100); // Thread.Sleep is not recommended, but for this example it is ok since the method is not async
            return "See the Visual Studio debugger";
        }

        private static bool _token = true;
        [HttpGet("WithTaskThatStopsExternal")]
        public async Task<string> GetWithTaskThatStopsExternal(bool stop)
        {
            _token = stop != true;
            if (stop) return "Task stop";

            Task.Run(async () =>
            {
                var i = 0;
                while (_token)
                {
                    Console.WriteLine(i++);
                    await Task.Delay(100);
                }
            });
            
            await Task.Delay(1000);
            return "See the Visual Studio debugger";
        }

    }
}
