﻿using System.Threading.Tasks;
using Xunit;

namespace Hasso.Cli.Tests
{
    public class FragmentWriterTests : IClassFixture<FragmentWriterFixture>
    {
        private readonly FragmentWriterFixture fixture;

        public FragmentWriterTests(FragmentWriterFixture fixture)
        {
            this.fixture = fixture;
        }

        [Theory]
        public async Task Writes_Script_Fragments_To_Target_Files()
        {
            var fragments = new[] {
                new Fragment {
                    Name = "some_script_name_1",
                    Content = "some_script_name_1: some value"
                },
                new Fragment {
                    Name = "some_script_name_2",
                    Content = "some_script_name_2: some other value"
                }
            };

            var writer = fixture.FragmentWriter;
            
            var files = await writer.WriteAsync(fragments);

            files.Count()
                .Should()
                .Be(2, "that´s how many fragments we threw into the writer");

        }

    }
}
