using Giveaway.API.Shared.Requests;
using Giveaway.API.Shared.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Giveaway.API.Controllers
{
    [Route("api/v1/test")]
    public class TestController : Controller
    {

        public TestController()
        {

        }

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("list")]
        public AuthResponse Get([FromQuery] FacebookConnectRequest request)
        {

            return new AuthResponse()
            {
                Token = "sdfsdf",
                TokenType = "sdfsdf"
            };
        }

        [HttpPost("create")]
        public AuthResponse Post([FromBody] FacebookConnectRequest request)
        {

            return new AuthResponse()
            {
                Token = "sdfsdf",
                TokenType = "sdfsdf"
            };
        }

        [HttpPut("put")]
        public AuthResponse Put([FromBody] FacebookConnectRequest request)
        {

            return new AuthResponse()
            {
                Token = "sdfsdf",
                TokenType = "sdfsdf"
            };
        }

        [HttpDelete("create/:id")]
        public AuthResponse Delete()
        {
            return new AuthResponse()
            {
                Token = "sdfsdf",
                TokenType = "sdfsdf"
            };
        }
    }
}