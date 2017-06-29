using Shouldly;
using Xunit;

namespace Cake.Yarn.Tests
{
    public class YarnPackTests
    {
        private readonly YarnPackFixture _fixture;

        public YarnPackTests()
        {
            _fixture = new YarnPackFixture();
        }

        [Fact]
        public void No_Pack_Settings_Should_Use_No_Argumemnts()
        {
            _fixture.PackSettings = null;

            var result = _fixture.Run();

            result.Args.ShouldBe("pack");
        }

        [Fact]
        public void Filename_Option_Specified_Should_Use_Filename_Argument()
        {
            const string generatedfilename = "GeneratedFilename";

            _fixture.PackSettings = s => s.Named(generatedfilename);

            var result = _fixture.Run();

            result.Args.ShouldBe($"pack --filename \"{generatedfilename}\"");
        }
    }
}
