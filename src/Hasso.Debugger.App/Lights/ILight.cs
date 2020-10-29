using System.ComponentModel;

namespace Hasso.Debugger.App.Lights
{
    /// <summary>
    /// a light, which can be turned on, off or be toggled
    /// </summary>
    public interface ILight : INotifyPropertyChanged
    {
        /// <summary>
        /// flips the switch and toggles <see cref="IsEnabled"/>
        /// </summary>
        /// <returns></returns>
        bool Toggle();

        /// <summary>
        /// turns the light on or off
        /// </summary>
        bool IsEnabled { get; set; }

        /// <summary>
        /// light.office_1
        /// </summary>
        string Id { get; }
    }
}
