

using Microsoft.AspNetCore.Mvc;

namespace Hasso.Debugger.App.Scenes
{
    /// <summary>
    /// api-controller for messing around with scenes
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class SceneController
    {
        private readonly IScenesHub scenes;
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="scenes"></param>
        public SceneController(IScenesHub scenes)
        {
            this.scenes = scenes;
        }

        /// <summary>
        /// activates a scene
        /// </summary>
        /// <param name="id">scene.some_scene_name_1</param>
        [HttpPut("{id}")]
        [HttpGet("{id}/activate")]
        public void Activate(string id) => scenes.Current = id;
    }
}
