using System;
using Shouldly;
using Xunit;

namespace Cake.Yarn.Tests
{
    public class YarnAddTests
    {
        private readonly YarnAddFixture _fixture;

        public YarnAddTests()
        {
            _fixture = new YarnAddFixture();
        }

        [Theory]
        [InlineData("any package")]
        [InlineData("https://www.npmjs.com/package/mylib")]
        public void Package_Settings_Specified_Should_Use_Correct_Arguments(string package)
        {
            _fixture.AddSettings = s => s.Package(package);

            var result = _fixture.Run();

            result.Args.ShouldBe("add " + package);
        }

        [Fact]
        public void Package_With_Tag_Settings_Specified_Should_Use_Correct_Arguments()
        {
            _fixture.AddSettings = s => s.Package("any package", ">1.0 && <1.5");

            var result = _fixture.Run();

            result.Args.ShouldBe("add any package@\">1.0 && <1.5\"");
        }

        [Fact]
        public void Package_With_Tag_And_Scope_Settings_Specified_Should_Use_Correct_Arguments()
        {
            _fixture.AddSettings = s => s.Package("any package", ">1.0 && <1.5", "@scope");

            var result = _fixture.Run();

            result.Args.ShouldBe("add @scope/any package@\">1.0 && <1.5\"");
        }

        [Fact]
        public void Package_With_Tag_And_Invalid_Scope_Settings_Specified_Should_Throw_ArgumentException()
        {
            _fixture.AddSettings = s => s.Package("any package", ">1.0 && <1.5", "scope");

            Action run = () => _fixture.Run();

            run.ShouldThrow<ArgumentException>();
        }

        [Theory]
        [InlineData("any package")]
        [InlineData("https://www.npmjs.com/package/mylib")]
        public void Package_And_Globally_Settings_Specified_Should_Use_Correct_Arguments(string package)
        {
            _fixture.AddSettings = s => s.Package(package).Globally();

            var result = _fixture.Run();

            result.Args.ShouldBe("global add " + package);
        }
    }
}
