using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace Examples.CancellationToken.Api.WithControllers.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class CancellationTokenController : ControllerBase
	{
		private readonly ILogger<CancellationTokenController> _logger;
		public CancellationTokenController(ILogger<CancellationTokenController> logger)
		{
			_logger = logger;
		}

		[HttpGet]
		public OkObjectResult Get()
		{
			return Ok("Cancellation token is up and running");
		}


		[HttpGet("WithClientToken")]
		public bool GetWithClientToken(System.Threading.CancellationToken cancellationToken)
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
			while (HttpContext.RequestAborted.IsCancellationRequested)
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
				}
			}, cancellationToken);

			task.Start();

			Thread.Sleep(100);

			cancellationTokenSource.Cancel();

			task.Wait();
			cancellationTokenSource.Dispose();
			return true;
		}

		[HttpGet("WithTaskThatNeverStops")]
		public string GetWithTaskThatNeverStops()
		{
			var cancellationTokenSource = new CancellationTokenSource();
			var cancellationToken = cancellationTokenSource.Token;

			var task = new Task(() =>
			{
				cancellationToken.Register(() => Console.WriteLine("Operation is canceled"));

				var i = 0;
				while (true)
				{
					Console.WriteLine(i++);
					Thread.Sleep(100);
				}
			}, cancellationToken);

			task.Start();

			Thread.Sleep(100);

			cancellationTokenSource.Cancel();

			//task.Wait();
			cancellationTokenSource.Dispose();
			return "See the Visual Studio debugger";
		}
	}
}