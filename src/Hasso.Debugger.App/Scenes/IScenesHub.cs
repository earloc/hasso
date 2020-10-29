
using System;

namespace Hasso.Debugger.App.Scenes
{
    public interface IScenesHub
    {
        string Current { get; set; }

        event EventHandler? CurrentChanged;
    }
}