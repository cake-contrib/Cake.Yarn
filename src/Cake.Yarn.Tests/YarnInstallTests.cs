﻿using Shouldly;
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
        public void Install_And_ForProduction_Settings_Specified_Should_Use_Correct_Argument_Provided_In_YarnInstallSettings()
        {
            _fixture.InstallSettings = s => s.ForProduction();

            var result = _fixture.Run();

            result.Args.ShouldBe("install --production=true");
        }

        [Fact]
        public void Install_And_ForProduction_False_Settings_Specified_Should_Use_Correct_Argument_Provided_In_YarnInstallSettings()
        {
            _fixture.InstallSettings = s => s.ForProduction(false);

            var result = _fixture.Run();

            result.Args.ShouldBe("install --production=false");
        }

        [Fact]
        public void Install_Settings_IgnorePlatformWarnings_Should_Use_Correct_Argument_Provided_In_YarnInstallSettings()
        {
            _fixture.InstallSettings = s => s.IgnorePlatformWarnings();

            var result = _fixture.Run();

            result.Args.ShouldBe("install --ignore-platform");
        }

        [Fact]
        public void Install_Settings_IgnoreEnginesWarnings_Should_Use_Correct_Argument_Provided_In_YarnInstallSettings()
        {
            _fixture.InstallSettings = s => s.IgnoreEnginesWarnings();

            var result = _fixture.Run();

            result.Args.ShouldBe("install --ignore-engines");
        }

        [Fact]
        public void Install_Settings_FrozenLockfile_Should_Use_Correct_Argument_Provided_In_YarnInstallSettings()
        {
            _fixture.InstallSettings = s => s.WithFrozenLockfile();

            var result = _fixture.Run();

            result.Args.ShouldBe("install --frozen-lockfile");
        }
        
        [Fact]
        public void Install_Settings_CheckFiles_Should_Use_Correct_Argument_Provided_In_YarnInstallSettings()
        {
            _fixture.InstallSettings = s => s.WithCheckFiles();

            var result = _fixture.Run();

            result.Args.ShouldBe("install --check-files");
        }

        [Fact]
        public void Install_Settings_OfflineInstall_Should_Use_Correct_Argument_Provided_In_YarnInstallSettings()
        {
            _fixture.InstallSettings = s => s.Offline();

            var result = _fixture.Run();

            result.Args.ShouldBe("install --offline");
        }

        [Fact]
        public void Install_Settings_WithArgument_Should_Use_Arguments_Provided_In_YarnInstallSettings()
        {
            _fixture.InstallSettings = s => s.WithArgument("--verbose").WithArgument("--force");

            var result = _fixture.Run();

            result.Args.ShouldBe("install --verbose --force");
        }

        [Fact]
        public void Several_Install_Settings_Should_Use_Correct_Argument_Provided_In_YarnInstallSettings()
        {
            _fixture.InstallSettings = s =>
                s.IgnorePlatformWarnings()
                .IgnoreEnginesWarnings()
                .IgnoreOptionalWarnings()
                .Offline();

            var result = _fixture.Run();

            result.Args.ShouldBe("install --ignore-platform --ignore-optional --ignore-engines --offline");
        }
    }
}
