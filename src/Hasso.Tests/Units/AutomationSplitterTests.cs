using FluentAssertions;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xunit;
namespace Hasso.Tests.Units
{

    public class AutomationSplitterTests : IClassFixture<AutomationSplitterTestsFixture>
    {
        private readonly AutomationSplitterTestsFixture fixture;

        public AutomationSplitterTests(AutomationSplitterTestsFixture fixture)
        {
            this.fixture = fixture;
        }

        [Theory]
        [InlineData("assets/automations.yaml", 3)]
        public async Task Automations_Are_Splitted_At_Root_Level(string inputFileName, int expectedSplittedItemCount)
        {
            var sut = this.fixture.SystemUnderTest;

            var fragments = await sut.SplitAsync(new FileInfo(inputFileName));

            var actual = fragments.Count();

            actual.Should()
                .Be(expectedSplittedItemCount, $"the given input file '{inputFileName}' is expected to contain a total of '{expectedSplittedItemCount}' automations");
        }

        [Theory]
        [InlineData("assets/automations.yaml", 0, "some_automation_name_1")]
        [InlineData("assets/automations.yaml", 1, "some_automation_name_2")]
        public async Task Automations_Are_Parsed_With_Their_Correct_Name(string inputFileName, int index, string expectedName)
        {
            var sut = this.fixture.SystemUnderTest;

            var fragments = await sut.SplitAsync(new FileInfo(inputFileName));

            var actual = fragments.ToArray()[index].Name;

            actual.Should()
                .Be(expectedName, "that is what the test-data says");
        }

        [Theory]
        [InlineData("assets/automations.yaml")]
        public async Task AutomationsFragmentContent_Only_Contains_Single_Entry(string inputFileName)
        {
            var sut = this.fixture.SystemUnderTest;

            var fragments = await sut.SplitAsync(new FileInfo(inputFileName));

            foreach (var fragment in fragments)
            {
                var actual = Regex.Matches(fragment.Content, "- id: ").Count;
                actual.Should()
                    .Be(1, "when a script has been splitted, a single fragment should only contain a single script");
            }
        }

        [Theory]
        [InlineData("assets/automations.yaml")]
        public async Task AutomationsFragmentContent_Is_Serialized_As_List_Element(string inputFileName)
        {
            var sut = this.fixture.SystemUnderTest;

            var fragments = await sut.SplitAsync(new FileInfo(inputFileName));

            foreach (var fragment in fragments)
            {
                var actual = fragment.Content;
                actual.Should().StartWith("- id: '", "this is how list-style elements are represented in yaml");
            }
        }

        [Fact]
        public async Task Deserializes_AdditionalProperties_Of_Automation()
        {
            var yaml = @"---
                - id: '1234'
                  alias: alias1234
                  some_unknown_field: 42
                - id: '4569'
                  alias: alias4569
                  some_unknown_field: 43
";

            var automation = await fixture.SystemUnderTest.SplitAsync(yaml);
            var fragment = automation.First();

            var actual = fragment.Content
                .AsOneLiner();

            var expected = @"
                - id: '1234'
                  alias: alias1234
                  some_unknown_field: 42"
                .AsOneLiner();

            actual.Should().Be(expected, "splitting up yamls should not modify a fragments content");
        }
    }
}
