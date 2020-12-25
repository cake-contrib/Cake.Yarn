using Shouldly;
using Xunit;

namespace Cake.Yarn.Tests
{
    public class YarnPublishTests
    {
        private readonly YarnPublishFixture _fixture;

        public YarnPublishTests()
        {
            _fixture = new YarnPublishFixture();
        }

        [Fact]
        public void No_Publish_Settings_Should_Use_No_Arguments()
        {
            _fixture.PublishSettings = null;

            var result = _fixture.Run();

            result.Args.ShouldBe("publish");
        }

        [Fact]
        public void New_Version_Option_Should_Format_Correctly()
        {
            const int major = 1;
            const int minor = 2;
            const int patch = 3;

            _fixture.PublishSettings = s => s.NewVersion(major, minor, patch);

            var result = _fixture.Run();

            result.Args.ShouldBe($"publish --new-version {major}.{minor}.{patch}");
        }

        [Fact]
        public void Tag_Option_Should_Use_Tag_Argument()
        {
            const string tag = "beta";

            _fixture.PublishSettings = s => s.Tag(tag);

            var result = _fixture.Run();

            result.Args.ShouldBe($"publish --tag {tag}");
        }

        [Fact]
        public void New_Version_And_Tag_Should_Work_Together()
        {
            const int major = 1;
            const int minor = 2;
            const int patch = 3;
            const string tag = "beta";

            _fixture.PublishSettings = s => s
                .NewVersion(major, minor, patch)
                .Tag(tag);

            var result = _fixture.Run();

            result.Args.ShouldBe($"publish --new-version {major}.{minor}.{patch} --tag {tag}");
        }
    }
}