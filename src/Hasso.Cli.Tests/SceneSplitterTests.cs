﻿using FluentAssertions;
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
        [InlineData("scenes.yaml", 2)]
        public async Task Scenes_Are_Splitted_At_Root_Level(string inputFileName, int expectedSplittedItemCount)
        {
            var sut = this.fixture.SystemUnderTest;

            var fragments = await sut.SplitAsync(inputFileName);

            var actual = fragments.Count();

            actual.Should()
                .Be(expectedSplittedItemCount, $"the given input file '{inputFileName}' is expected to contain a total of '{expectedSplittedItemCount}' scenes");
        }

        [Theory]
        [InlineData("scenes.yaml", 0, "some_scene_name_1")]
        [InlineData("scenes.yaml", 1, "some_scene_name_2")]
        public async Task Scenes_Are_Parsed_With_Their_Correct_Name(string inputFileName, int index, string expectedName)
        {
            var sut = this.fixture.SystemUnderTest;

            var fragments = await sut.SplitAsync(inputFileName);

            var actual = fragments.ToArray()[index].Name;

            actual.Should()
                .Be(expectedName, "that is what the test-data says");
        }

        [Theory]
        [InlineData("scenes.yaml")]
        public async Task SceneFragmentContent_Only_Contains_Single_Entry(string inputFileName)
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
