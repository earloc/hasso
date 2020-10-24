using FluentAssertions;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Hasso.Tests.Units
{

    public class ScriptSplitterTests : IClassFixture<ScriptSplitterTestsFixture>
    {
        private readonly ScriptSplitterTestsFixture fixture;

        public ScriptSplitterTests(ScriptSplitterTestsFixture fixture)
        {
            this.fixture = fixture;
        }

        [Theory]
        [InlineData("assets/scripts.yaml", 2)]
        public async Task Scripts_Are_Splitted_At_Root_Level(string inputFileName, int expectedSplittedItemCount)
        {
            var sut = this.fixture.SystemUnderTest;

            var fragments = await sut.SplitAsync(new FileInfo(inputFileName));

            var actual = fragments.Count();

            actual.Should()
                .Be(expectedSplittedItemCount, $"the given input file '{inputFileName}' is expected to contain a total of '{expectedSplittedItemCount}' scripts");
        }

        [Theory]
        [InlineData("assets/scripts.yaml", 0, "some_script_name_1")]
        [InlineData("assets/scripts.yaml", 1, "some_script_name_2")]
        public async Task Scripts_Are_Parsed_With_Their_Correct_Name(string inputFileName, int index, string expectedName)
        {
            var sut = this.fixture.SystemUnderTest;

            var fragments = await sut.SplitAsync(new FileInfo(inputFileName));

            var actual = fragments.ToArray()[index].Name;

            actual.Should()
                .Be(expectedName, "that is what the test-data says");
        }

        [Theory]
        [InlineData("assets/scripts.yaml")]
        public async Task ScriptFragmentContent_Only_Contains_Single_Entry(string inputFileName)
        {
            var sut = this.fixture.SystemUnderTest;

            var fragments = await sut.SplitAsync(new FileInfo(inputFileName));

            foreach (var fragment in fragments)
            {
                var actual = fragment.Content;
                actual.Count()
                    .Should()
                    .Be(1, "when a script has been splitted, a single fragment should only contain a single script");
            }
        }

        [Theory]
        [InlineData("assets/scripts.yaml")]
        public async Task ScriptFragmentContent_Is_Not_Serialized_As_List_Element(string inputFileName)
        {
            var sut = this.fixture.SystemUnderTest;

            var fragments = await sut.SplitAsync(new FileInfo(inputFileName));

            foreach (var fragment in fragments)
            {
                var actual = fragment.ToString();
                actual.Should().NotStartWith("- id:", "this is not the way scripts are organized");
            }
        }
    }
}
