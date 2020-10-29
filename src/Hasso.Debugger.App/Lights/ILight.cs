using System.ComponentModel;

namespace Hasso.Debugger.App.Lights
{
    public interface ILight : INotifyPropertyChanged
    {
        bool Toggle();

        bool IsEnabled { get; set; }
        string Id { get; }
    }
}
