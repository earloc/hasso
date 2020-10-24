using YamlDotNet.RepresentationModel;
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

        public string Content { get; set; } = "";
    }
}
