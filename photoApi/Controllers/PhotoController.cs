using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace photoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotoController : ControllerBase
    {

        // POST api/<PhotoController>
        [HttpPost]
        public IActionResult Post([FromBody] string[] value)
        {
            return Ok(value[0]);
        }
    }
}
