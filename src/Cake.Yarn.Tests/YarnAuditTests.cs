using Shouldly;
using Xunit;

namespace Cake.Yarn.Tests
{
    public class YarnAuditTests
    {
        private readonly YarnAuditFixture _fixture;

        public YarnAuditTests()
        {
            _fixture = new YarnAuditFixture();
        }

        [Fact]
        public void No_Audit_Settings_Should_Use_No_Arguments()
        {
            _fixture.AuditSettings = null;

            var result = _fixture.Run();

            result.Args.ShouldBe("audit");
        }

        [Fact]
        public void SetVerbose_Option_Specified_Should_Use_Verbose_Argument()
        {
            _fixture.AuditSettings = s => s.SetVerbose();

            var result = _fixture.Run();

            result.Args.ShouldBe("audit --verbose");
        }

        [Fact]
        public void SetJson_Option_Specified_Should_Use_Json_Argument()
        {
            _fixture.AuditSettings = s => s.SetJson();

            var result = _fixture.Run();

            result.Args.ShouldBe("audit --json");
        }
    }
}