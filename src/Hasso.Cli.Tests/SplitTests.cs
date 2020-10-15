using System.Threading.Tasks;
using Xunit;

namespace Hasso.Cli.Tests.Scripts
{

    public class SplitTests : IClassFixture<SplitFixture>
    {

        [Theory]
        [InlineData("scripts.yaml", 2)]
        public Task Scripts_Are_Splitted_At_Root_Level(string inputFileName, int expectedSplittedFileCount)
        {
            var sut = this.fixture.ScriptSplitter;

            var fragments = sut.Split(inputFileName);


        }

    }
}
