using Microsoft.AspNetCore.Mvc;

namespace Hasso.Debugger.App.Lights
{
    [ApiController]
    [Route("api/[controller]")]
    public class LightController : Controller
    {
        [HttpGet("{id}/toggle")]
        [HttpPatch("{id}")]
        public bool Toggle(string id, [FromServices] ILightsHub lights) => lights[id].Toggle();

        [HttpGet("{id}/off")]
        [HttpDelete("{id}")]
        public bool Off(string id, [FromServices] ILightsHub lights) => lights[id].IsEnabled = false;

        [HttpGet("{id}/on")]
        [HttpPut("{id}/on")]
        public bool On(string id, [FromServices] ILightsHub lights) => lights[id].IsEnabled = true;

    }
}
