using Soenneker.Tests.FixturedUnit;
using Xunit;

namespace Soenneker.SevenZip.Runner.Tests;

[Collection("Collection")]
public sealed class SevenZipRunnerTests : FixturedUnitTest
{
    public SevenZipRunnerTests(Fixture fixture, ITestOutputHelper output) : base(fixture, output)
    {
    }

    [Fact]
    public void Default()
    {

    }
}
