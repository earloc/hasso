using System.Collections.Generic;
using System.Linq;
using YamlDotNet.Serialization;

namespace Hasso.Cli
{
    internal class Fragment
    {
        protected readonly ISerializer serializer;
        public Fragment()
        {
            serializer = new SerializerBuilder().Build();
        }

        public string? Name { get; set; } = null;

        public object Content { get; set; } = new object();

        public override string ToString() => serializer.Serialize(Content);
    }
}
