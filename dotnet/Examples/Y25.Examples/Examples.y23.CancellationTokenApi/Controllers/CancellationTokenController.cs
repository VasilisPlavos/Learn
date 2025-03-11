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
        public bool GetWitCancellationTokenSource()
        {
            var cancellationTokenSource = new CancellationTokenSource();
            var cancellationToken = cancellationTokenSource.Token;

            var task = new Task(() =>
            {
                cancellationToken.Register(() => Console.WriteLine("Operation is canceled"));

                var i = 0;
                while (!cancellationToken.IsCancellationRequested)
                {
                    Console.WriteLine(i++);
                    Thread.Sleep(1);
                }
            }, cancellationToken);

            task.Start();

            Thread.Sleep(50);

            cancellationTokenSource.Cancel();

            task.Wait();
            cancellationTokenSource.Dispose();
            return true;
        }


        [HttpGet("WithTaskThatNeverStops")]
        public string GetWithTaskThatNeverStops()
        {
            new Task(() =>
            {
                var i = 0;
                while (true)
                {
                    Console.WriteLine(i++);
                    Thread.Sleep(100);
                }
            }).Start();

            Thread.Sleep(100);
            return "See the Visual Studio debugger";
        }

        private static bool _token = true;
        [HttpGet("WithTaskThatStopsExternal")]
        public string GetWithTaskThatStopsExternal(bool stop)
        {
            _token = stop != true;

            if (stop) return "Task stop";

            var task = new Task(() =>
            {
                var i = 0;
                while (_token)
                {
                    Console.WriteLine(i++);
                    Thread.Sleep(100);
                }
            });

            task.Start();
            task.Wait(100);
            return "See the Visual Studio debugger";
        }

    }
}
