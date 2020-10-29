
using System;

namespace Hasso.Debugger.App.Scenes
{
    internal interface IScenesHub
    {
        string Current { get; set; }

        event EventHandler? CurrentChanged;
    }
}