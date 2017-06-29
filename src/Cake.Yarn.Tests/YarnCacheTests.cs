using System;
using Shouldly;
using Xunit;

namespace Cake.Yarn.Tests
{
    public class YarnCacheTests
    {
        private readonly YarnCacheFixture _fixture;

        public YarnCacheTests()
        {
            _fixture = new YarnCacheFixture();
        }


        [Fact]
        public void Not_Setting_ScriptName_Should_Fail()
        {
            _fixture.SubCommand = "";
            Assert.Throws<ArgumentNullException>(() => _fixture.Run());
        }


        [Fact]
        public void No_Install_Settings_Should_Use_Correct_Argument_Provided_In_YarnInstallSettings()
        {
            _fixture.SubCommand = "clean";
            var result = _fixture.Run();
            result.Args.ShouldBe("cache clean");
        }
    }
}
