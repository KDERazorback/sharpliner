using Sharpliner.AzureDevOps;

namespace Sharpliner.Tests.AzureDevOps;

[Collection("Serial:1")]
public class NoopJobTests
{
    private class NoopJobPipeline : SimpleTestPipeline
    {
        public override SingleStagePipeline Pipeline => new()
        {
            Jobs =
            {
                new NoopJob
                {
                    Pool = new HostedPool(vmImage: "windows-2019")
                }
            }
        };
    }

    [Fact]
    public Task Serialize_Pipeline_Test()
    {
        NoopJobPipeline pipeline = new();

        return Verify(pipeline.Serialize());
    }
}
