using System;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;


namespace Example.Cloudon.Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;

        public LoginController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(string username, string password)
        {
            var userObj = JsonConvert.SerializeObject(new { username, password });
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://localhost:44369/api/Users/authenticate"),
                Content = new StringContent(userObj, Encoding.UTF8, "application/json")
            };

            var client = _clientFactory.CreateClient();
            var response = await client.SendAsync(request);
            if (!response.IsSuccessStatusCode) return View();

            var jwt = await response.Content.ReadAsStringAsync();

            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(jwt);
            if (token.Payload.Exp == null) return View();

            var dateExpires = DateTimeOffset.FromUnixTimeSeconds((long)token.Payload.Exp).DateTime;

            ViewBag.Jwt = $"Bearer {jwt}";
            Response.Cookies.Append("UserAuth", "true", new CookieOptions { Expires = dateExpires, SameSite = SameSiteMode.None, Secure = true});
            return View();
        }
    }
}
