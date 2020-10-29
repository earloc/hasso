using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace Hasso.Debugger.App.Lights
{
    public class InMemoryLightsHub : ILightsHub
    {

        private readonly IDictionary<string, ILight> lights = new Dictionary<string, ILight>();

        public InMemoryLightsHub()
        {
        }

        private void Light_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            LightChanged?.Invoke(this, EventArgs.Empty);
        }

        public ILight this[string id] => GetOrAdd(id);

        private ILight GetOrAdd(string id)
        {
            if (!lights.ContainsKey(id))
            {
                var light = new InMemoryLight(id);
                light.PropertyChanged += Light_PropertyChanged;
                lights.Add(id, light);
            }

            return lights[id];
        }

        public event EventHandler? LightChanged;

        public IEnumerator<ILight> GetEnumerator() => lights.Values.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }
}
