using FluentAssertions;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Hasso.Cli.Tests.Scripts
{

    public class SplitTests : IClassFixture<SplitFixture>
    {
        private readonly SplitFixture fixture;

        public SplitTests(SplitFixture fixture)
        {
            this.fixture = fixture;
        }

        [Theory]
        [InlineData("scripts.yaml", 2)]
        public async Task Scripts_Are_Splitted_At_Root_Level(string inputFileName, int expectedSplittedFileCount)
        {
            var sut = this.fixture.ScriptSplitter;

            var fragments = await sut.SplitAsync(inputFileName);

            fragments.Count()
                .Should()
                .Be(expectedSplittedFileCount, $"the given input file '{inputFileName}' is expected to contain a total of '{expectedSplittedFileCount}' scripts");
        }

    }
}
