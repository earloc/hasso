using FluentAssertions;
using Hasso.Cli;
using Hasso.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using static Hasso.Models.Automation;

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
            var fragment = new Fragment
            {
                Name = "some_automation_name_1",
                Content = new List<object> {
                    new Automation {
                        Id = "12345",
                        Trigger = new List<TriggerType>()
                        {
                            new TriggerType
                            {
                                Platform = "state",
                                From = "42",
                                To = "43"
                            }
                        }
                    }
                }
            };

            var content = await fixture.SystemUnderTest.WriteAsync(fragment);

            var actual = content.Replace(" ", "").Replace(Environment.NewLine, "");

            var expected = @"
- id: '12345'
  trigger:
  - from: '42'
    to: '43'
    platform: state
  condition: []
  action: []
mode: Single

".Trim().Replace(" ", "").Replace(Environment.NewLine, "");


            actual.Should().Be(expected);

        }


    }
}
