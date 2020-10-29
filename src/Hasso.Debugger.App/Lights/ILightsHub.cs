using System;
using System.Collections.Generic;

namespace Hasso.Debugger.App.Lights
{
    /// <summary>
    /// A hub for all the lights
    /// </summary>
    public interface ILightsHub : IEnumerable<ILight>
    {
        /// <summary>
        /// grants access to a light, specified by it´s id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ILight this[string id] { get; }

        /// <summary>
        /// indicates that any of the lights within the hub has changed
        /// </summary>
        event EventHandler? LightChanged;
    }
}
