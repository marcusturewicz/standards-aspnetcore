using System;
using System.Globalization;
using Standards.AspNetCore;
using Microsoft.AspNetCore.Mvc;

namespace Standards.AspNetCore.Tests
{
    [ApiController]
    [Route("[controller]")]
    public class Iso8601DateController : ControllerBase
    {
        [HttpGet("attribute")]
        public IActionResult GetAttribute([ModelBinder(BinderType = typeof(Iso8601Date))] DateTime date)
        {
            return Ok();
        }

        [HttpGet("provider")]
        public IActionResult GetProvider(DateTime date)
        {
            return Ok();
        }        
    }
}