using FluentAssertions;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Hasso.Cli.Tests.Units
{

    public class AutomationSplitterTests : IClassFixture<AutomationSplitterTestsFixture>
    {
        private readonly AutomationSplitterTestsFixture fixture;

        public AutomationSplitterTests(AutomationSplitterTestsFixture fixture)
        {
            this.fixture = fixture;
        }

        [Theory]
        [InlineData("automations.yaml", 2)]
        public async Task Scripts_Are_Splitted_At_Root_Level(string inputFileName, int expectedSplittedItemCount)
        {
            var sut = this.fixture.SystemUnderTest;

            var fragments = await sut.SplitAsync(new FileInfo(inputFileName));

            var actual = fragments.Count();

            actual.Should()
                .Be(expectedSplittedItemCount, $"the given input file '{inputFileName}' is expected to contain a total of '{expectedSplittedItemCount}' automations");
        }

        [Theory]
        [InlineData("automations.yaml", 0, "some_automation_name_1")]
        [InlineData("automations.yaml", 1, "some_automation_name_2")]
        public async Task Scripts_Are_Parsed_With_Their_Correct_Name(string inputFileName, int index, string expectedName)
        {
            var sut = this.fixture.SystemUnderTest;

            var fragments = await sut.SplitAsync(new FileInfo(inputFileName));

            var actual = fragments.ToArray()[index].Name;

            actual.Should()
                .Be(expectedName, "that is what the test-data says");
        }

        [Theory]
        [InlineData("automations.yaml")]
        public async Task ScriptFragmentContent_Only_Contains_Single_Entry(string inputFileName)
        {
            var sut = this.fixture.SystemUnderTest;

            var fragments = await sut.SplitAsync(new FileInfo(inputFileName));


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
