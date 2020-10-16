using System.Collections.Generic;

namespace Hasso.Cli
{
    public class Fragment
    {
        public string? Name { get; set; } = null;
        public Dictionary<object, object> Content { get; set; } = new Dictionary<object, object>();
    }
}
