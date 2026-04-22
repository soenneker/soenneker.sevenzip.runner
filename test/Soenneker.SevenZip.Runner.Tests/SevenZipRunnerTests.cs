using Soenneker.Tests.HostedUnit;

namespace Soenneker.SevenZip.Runner.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public sealed class SevenZipRunnerTests : HostedUnitTest
{
    public SevenZipRunnerTests(Host host) : base(host)
    {
    }

    [Test]
    public void Default()
    {

    }
}
