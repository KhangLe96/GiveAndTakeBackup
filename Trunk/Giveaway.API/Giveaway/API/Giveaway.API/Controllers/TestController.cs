using Giveaway.API.Shared.Requests;
using Giveaway.API.Shared.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Giveaway.API.Controllers
{
    [Route("api/v1/asdf")]
    public class TestController : Controller
    {

        public TestController()
        {

        }

        [HttpGet("qwe")]
        [Produces("application/json")]
        public AuthResponse ABC([FromBody] FacebookConnectRequest request)
        {

            return new AuthResponse()
            {
                Token = "sdfsdf",
                TokenType = "sdfsdf"
            };
        }
    }
}