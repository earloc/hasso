
using System;

namespace Hasso.Debugger.App.Scenes
{
    /// <summary>
    /// a hub for all the scenes
    /// </summary>
    public interface IScenesHub
    {
        /// <summary>
        /// retuns the currently active scene
        /// </summary>
        string Current { get; set; }

        /// <summary>
        /// indicates that <see cref="Current"/> has changed
        /// </summary>
        event EventHandler? CurrentChanged;
    }
}