using System;
using System.ComponentModel;

namespace Hasso.Debugger.App.Lights
{
    public class InMemoryLight : ILight
    {
        public InMemoryLight(string id)
        {
            Id = id;
        }
        public string Id { get; }

        private bool isEnabled = false;
        public bool IsEnabled {
            get => isEnabled;
            set => Set(() => isEnabled = value);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public bool Toggle() => IsEnabled = !IsEnabled;

        private T Set<T>(Func<T> func, string? propertyName = "")
        {
            var result = func();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            return result;
        }
    }
}
