using System;
using System.Collections.Generic;

namespace Hasso.Debugger.App.Lights
{
    internal interface ILightsHub : IEnumerable<ILight>
    {
        ILight this[string id] { get; }

        event EventHandler? LightChanged;
    }
}
