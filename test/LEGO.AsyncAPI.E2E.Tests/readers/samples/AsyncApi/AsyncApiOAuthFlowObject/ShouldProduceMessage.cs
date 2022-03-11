namespace LEGO.AsyncAPI.E2E.Tests.Readers.Samples.AsyncApi.AsyncApiOAuthFlowObject
{
    using System;
    using System.Collections.Immutable;
    using AsyncAPI.Tests;
    using Models;
    using Xunit;

    public class ShouldProduceOAuthFlow: ShouldConsumeProduceBase<OAuthFlow>
    {
        public ShouldProduceOAuthFlow(): base(typeof(ShouldProduceOAuthFlow))
        {
        }

        [Fact]
        public void ShouldProduceMinimalSpec()
        {
            Assert.Equal(GetString("Minimal.json"), AsyncApiWriter.Write(new OAuthFlow()));
        }
        
        [Fact]
        public void ShouldProduceCompleteSpec()
        {
            Assert.Equal(GetStringWithMockedExtensions("Complete.json"), AsyncApiWriter.Write(new OAuthFlow
            {
                AuthorizationUrl = new Uri("https://lego.com/auth"),
                TokenUrl = new Uri("https://lego.com/token"),
                RefreshUrl = new Uri("https://lego.com/refresh"),
                Scopes = ImmutableDictionary<string, string>.Empty,
                Extensions = MockData.Extensions()
            }));
        }
    }
}