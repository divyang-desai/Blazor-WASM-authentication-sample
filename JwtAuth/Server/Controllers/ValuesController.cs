using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace JwtAuth.Server.Controllers
{
    [Route("api/[controller]")]    
    public class ValuesController : ControllerBase
    {
        // GET: api/<ValuesController>
        [Authorize]
        [HttpGet]
        public IActionResult Get()
        {
            //return BadRequest("This is not working");

            var response = new string[] { "value1", "value2" };
            return Ok(response);
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ValuesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
