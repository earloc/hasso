﻿using Serilog;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using YamlDotNet.Serialization;

namespace Hasso.Cli
{
    internal class YamlFragmentWriter : IFragmentWriter
    {
        private readonly ILogger logger;

        public YamlFragmentWriter(ILogger logger)
        {
            this.logger = logger;
        }
        public Task<IEnumerable<FileInfo>> WriteAsync(DirectoryInfo baseDirectory, IEnumerable<Fragment> fragments)
        {
            if (!baseDirectory.Exists) baseDirectory.Create();

            var serializer = new SerializerBuilder().Build();

            var tasks = fragments.Select(_ =>
            {
                var targetFileName = Path.Combine(baseDirectory.FullName, $"{_.Name}.partial.yaml");
                var targetFile = new FileInfo(targetFileName);
                using var writer = File.CreateText(targetFile.FullName);
                serializer.Serialize(writer, _.Content);

                logger.Information("created '{targetFileName}'", targetFileName);

                return targetFile;
            }).ToArray().AsEnumerable();

            return Task.FromResult(tasks);



        }
    }
}