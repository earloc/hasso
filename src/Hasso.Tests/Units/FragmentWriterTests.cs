using FluentAssertions;
using Hasso.Cli;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Hasso.Tests.Units
{
    public class FragmentWriterTests : IClassFixture<FragmentWriterFixture>
    {
        private readonly FragmentWriterFixture fixture;

        public FragmentWriterTests(FragmentWriterFixture fixture)
        {
            this.fixture = fixture;
        }

        [Fact]
        public async Task Writes_Script_Fragments_To_Target_Files()
        {
            var fragments = new[] {
                new Fragment {
                    Name = "some_script_name_1",
                    Content = new Dictionary<object, object> { {"some_script_name_1", "some value" } }
                },
                new Fragment {
                    Name = "some_script_name_2",
                    Content = new Dictionary<object, object> { {"some_script_name_2", "some other value" } }
                }
            };

            var writer = fixture.SystemUnderTest;

            var files = await writer.WriteAsync(new DirectoryInfo("testOutput"), fragments);

            files.Count()
                .Should()
                .Be(2, "that´s how many fragments we threw into the writer");

        }

        [Fact]
        public async Task Serializes_TriggerFields_With_Enclosing_SingleQuotes()
        {
            var fragment = new Fragment {
                Name = "some_automation_name_1",
                Content = new List<object> { 
                    new { 
                        id = "12345",
                        trigger = new List<object>()
                        {
                            new
                            {
                                platform = "state",
                                from = "42",
                                to = "43"
                            }
                        }
                    }
                }
            };

            var actual = await fixture.SystemUnderTest.WriteAsync(fragment);

            var expected = @"
- id = '12345'
  trigger:
  - platform: state
    from: '42'
    to: '43'
".Trim();


            actual.Should().Be(expected);

        }


    }
}
