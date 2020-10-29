using System;

namespace Hasso.Debugger.App.Scenes
{
    public class InMemoryScenesHub : IScenesHub
    {
        private string current = "None";

        public string Current { 
            get => current;
            set {
                current = value;
                CurrentChanged?.Invoke(this, EventArgs.Empty);
            } 
        }

        public event EventHandler? CurrentChanged;
    }
}