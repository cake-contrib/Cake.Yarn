using Shouldly;
using Xunit;

namespace Cake.Yarn.Tests
{
    public class YarnInstallTests
    {
        private readonly YarnInstallFixture _fixture;

        public YarnInstallTests()
        {
            _fixture = new YarnInstallFixture();
        }

        [Fact]
        public void No_Install_Settings_Should_Use_Correct_Argument_Provided_In_YarnInstallSettings()
        {
            _fixture.InstallSettings = null;

            var result = _fixture.Run();

            result.Args.ShouldBe("install");
        }

        [Fact]
        public void Packages_And_ForProduction_Settings_Specified_Should_Use_Throw_ArgumentException()
        {
            _fixture.InstallSettings = s => s.ForProduction();

            var result = _fixture.Run();

            result.Args.ShouldBe("install --production");
        }

        [Fact]
        public void Install_Settings_IgnorePlatformWarnings_Should_Use_Correct_Argument_Provided_In_YarnInstallSettings()
        {
            _fixture.InstallSettings = s => s.IgnorePlatformWarnings();

            var result = _fixture.Run();

            result.Args.ShouldBe("install --ignore-platform");
        }

        [Fact]
        public void Several_Install_Settings_Should_Use_Correct_Argument_Provided_In_YarnInstallSettings()
        {
            _fixture.InstallSettings = s =>
                s.IgnorePlatformWarnings()
                .IgnoreOptionalWarnings();

            var result = _fixture.Run();

            result.Args.ShouldBe("install --ignore-platform --ignore-optional");
        }
    }
}
