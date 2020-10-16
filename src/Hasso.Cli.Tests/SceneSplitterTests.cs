using FluentAssertions;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Hasso.Cli.Tests.Scripts
{

    public class SceneSplitterTests : IClassFixture<SceneSplitterTestsFixture>
    {
        private readonly SceneSplitterTestsFixture fixture;

        public SceneSplitterTests(SceneSplitterTestsFixture fixture)
        {
            this.fixture = fixture;
        }

        [Theory]
        [InlineData("scripts.yaml", 2)]
        public async Task Scripts_Are_Splitted_At_Root_Level(string inputFileName, int expectedSplittedFileCount)
        {
            var sut = this.fixture.SystemUnderTest;

            var fragments = await sut.SplitAsync(inputFileName);

            var actual = fragments.Count();

            actual.Should()
                .Be(expectedSplittedFileCount, $"the given input file '{inputFileName}' is expected to contain a total of '{expectedSplittedFileCount}' scripts");
        }

        [Theory]
        [InlineData("scripts.yaml", 0, "some_script_name_1")]
        [InlineData("scripts.yaml", 1, "some_script_name_2")]
        public async Task Scripts_Are_Parsed_With_Their_Correct_Name(string inputFileName, int index, string expectedName)
        {
            var sut = this.fixture.SystemUnderTest;

            var fragments = await sut.SplitAsync(inputFileName);

            var actual = fragments.ToArray()[index].Name;

            actual.Should()
                .Be(expectedName, "that is what the test-data says");
        }

        [Theory]
        [InlineData("scripts.yaml")]
        public async Task FragmentContent_Only_Contains_Single_Entry(string inputFileName)
        {
            var sut = this.fixture.SystemUnderTest;

            var fragments = await sut.SplitAsync(inputFileName);


            foreach (var fragment in fragments)
            {
                var actual = fragment.Content;
                actual.Count
                    .Should()
                    .Be(1, "when a script has been splitted, a single fragment should only contain a single script");
            }

        }
    }
}
