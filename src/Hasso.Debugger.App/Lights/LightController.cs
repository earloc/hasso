using Microsoft.AspNetCore.Mvc;

namespace Hasso.Debugger.App.Lights
{
    /// <summary>
    /// api-controller for messing around with lights
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class LightController : Controller
    {
        private readonly ILightsHub lights;

        internal LightController(ILightsHub lights)
        {
            this.lights = lights;
        }

        /// <summary>
        /// Toggles a light.
        /// </summary>
        /// <param name="id">light.office_1</param>
        /// <returns>the current state of the light (true, false)</returns>
        [HttpGet("{id}/toggle")]
        [HttpPatch("{id}")]
        public bool Toggle(string id) => lights[id].Toggle();

        /// <summary>
        /// Turns off a light.
        /// </summary>
        /// <param name="id">>light.office_1</param>
        /// <returns>the current state of the light (true, false)</returns>
        [HttpGet("{id}/off")]
        [HttpDelete("{id}")]
        public bool Off(string id) => lights[id].IsEnabled = false;

        /// <summary>
        /// Turns on a light.
        /// </summary>
        /// <param name="id">>light.office_1</param>
        /// <returns>the current state of the light (true, false)</returns>
        [HttpGet("{id}/on")]
        [HttpPut("{id}")]
        public bool On(string id) => lights[id].IsEnabled = true;

    }
}
