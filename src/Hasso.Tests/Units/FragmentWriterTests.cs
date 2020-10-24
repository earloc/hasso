using FluentAssertions;
using Hasso.Cli;
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
                    Content = "some_script_name_1: some value"
                },
                new Fragment {
                    Name = "some_script_name_2",
                    Content = "some_script_name_2: some other value"
                }
            };

            var writer = fixture.SystemUnderTest;

            var files = await writer.WriteAsync(new DirectoryInfo("testOutput"), fragments);

            files.Count()
                .Should()
                .Be(2, "that´s how many fragments we threw into the writer");
        }

        //        [Fact]
        //        [Trait("issue", "#22")]
        //        [Trait("bug", "missing SingleQoutes on 'from' and 'to' of trigger")]
        //        public async Task Serializes_TriggerFields_With_Enclosing_SingleQuotes()
        //        {
        //            var fragment = new Fragment
        //            {
        //                Name = "some_automation_name_1",
        //                Content = new List<object> {
        //                    new Automation {
        //                        Id = "12345",
        //                        Trigger = new List<Trigger>()
        //                        {
        //                            new Trigger
        //                            {
        //                                Platform = "state",
        //                                From = "42",
        //                                To = "43"
        //                            }
        //                        }
        //                    }
        //                }
        //            };

        //            var content = await fixture.SystemUnderTest.WriteAsync(fragment);

        //            var actual = content.Replace(" ", "").Replace(Environment.NewLine, "");

        //            var expected = @"
        //- id: '12345'
        //  trigger:
        //  - from: '42'
        //    to: '43'
        //    platform: state
        //  condition: []
        //  action: []
        //mode: Single

        //".Trim().Replace(" ", "").Replace(Environment.NewLine, "");


        //            actual.Should().Be(expected);

        //        }

        //        [Fact]
        //        [Trait("issue", "#23")]
        //        [Trait("bug", "missing property 'at', 'tag_id'")]
        //        public async Task Can_Serialize_Fields_On_Trigger()
        //        {
        //            var fragment = new Fragment
        //            {
        //                Name = "some_automation_name_1",
        //                Content = new List<object> {
        //                    new Automation {
        //                        Id = "12345",
        //                        Trigger = new List<Trigger>()
        //                        {
        //                            new Trigger
        //                            {
        //                                At = "01:23:45",
        //                                TagId = "tagid123"
        //                            }
        //                        }
        //                    }
        //                }
        //            };

        //            var content = await fixture.SystemUnderTest.WriteAsync(fragment);

        //            var actual = content.Replace(" ", "").Replace(Environment.NewLine, "");

        //            var expected = @"
        //- id: '12345'
        //  trigger:
        //  - at: 01:23:45
        //    tag_id: tagid123
        //  condition: []
        //  action: []
        //mode: Single

        //".Trim().Replace(" ", "").Replace(Environment.NewLine, "");


        //            actual.Should().Be(expected);

        //        }
    }
}