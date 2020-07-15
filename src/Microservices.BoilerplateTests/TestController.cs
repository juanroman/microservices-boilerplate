using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Net.Mime;

namespace Microservices.BoilerplateTests
{
    [ApiController]
    [Route("v1/[controller]")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    public class TestController : ControllerBase
    {
        public IActionResult Post([FromBody] TestModel model)
        {
            if (null == model || string.IsNullOrWhiteSpace(model.RequiredField))
            {
                throw new InvalidOperationException("Invalid Model");
            }

            return Ok();
        }

        [HttpGet]
        public IActionResult Get() => throw new FileNotFoundException("Forced FileNotFound Exception", "somefile.abc");
    }
}
