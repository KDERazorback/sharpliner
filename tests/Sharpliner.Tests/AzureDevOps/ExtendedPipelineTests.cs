using FluentAssertions;
using Sharpliner.AzureDevOps;
using Xunit;

namespace Sharpliner.Tests.AzureDevOps;

[Collection("Serial:1")]
public class ExtendedPipelineTests
{
    private class Extended_Pipeline : ExtendsPipelineDefinition<PipelineWithExtends>
    {
        public override string TargetFile => "file.yml";

        public override PipelineWithExtends Pipeline => new()
        {
            Extends = new("templates/pipeline-template.yml")
            {
                Parameters = new()
                {
                    { "param1", "value1" },
                    { "param2", false },
                }
            },

            Trigger = Trigger.None,
            Pool = new HostedPool(vmImage: "ubuntu-latest"),
        };
    }

    [Fact]
    public void TemplateList_Serialization_Test()
    {
        var yaml = new Extended_Pipeline().Serialize();

        yaml.Trim().Should().Be(
            """
            extends:
              template: templates/pipeline-template.yml
              parameters:
                param1: value1
                param2: false

            trigger: none

            pool:
              vmImage: ubuntu-latest
            """);
    }
}
