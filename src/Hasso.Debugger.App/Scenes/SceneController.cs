

using Microsoft.AspNetCore.Mvc;

namespace Hasso.Debugger.App.Scenes
{
    [ApiController]
    [Route("api/[controller]")]
    public class SceneController
    {
        [HttpGet("{id}/activate")]

        public void Activate(string id, [FromServices] IScenesHub scenes) => scenes.Current = id;
    }
}
