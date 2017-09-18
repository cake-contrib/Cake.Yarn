using Shouldly;
using Xunit;

namespace Cake.Yarn.Tests
{
    public class YarnVersionTests
    {
        private readonly YarnVersionFixture _fixture;

        public YarnVersionTests()
        {
            _fixture = new YarnVersionFixture();
        }

        [Fact]
        public void No_Version_Settings_Should_Use_No_Argumemnts()
        {
            _fixture.VersionSettings = null;

            var result = _fixture.Run();

            result.Args.ShouldBe("version");
        }

        [Fact]
        public void NewVersion_Option_Specified_Should_Use_NewVersion_Argument()
        {
            const string generatedVersion = "0.1.0-generated";

            _fixture.VersionSettings = s => s.SetVersion(generatedVersion);

            var result = _fixture.Run();

            result.Args.ShouldBe($"version --new-version \"{generatedVersion}\"");
        }

        [Fact]
        public void DisableGitTag_Option_Specified_Should_Use_NoGitTagVersion_Argument()
        {
            _fixture.VersionSettings = s => s.DisableGitTagCreation();

            var result = _fixture.Run();

            result.Args.ShouldBe($"version --no-git-tag-version");
        }
    }
}