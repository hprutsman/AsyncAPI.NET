using LEGO.AsyncAPI.Models;
using LEGO.AsyncAPI.Models.Bindings.ServerBindings;
using LEGO.AsyncAPI.Tests;
using Xunit;

namespace LEGO.AsyncAPI.E2E.Tests.readers.samples.AsyncApi
{
    public class ShouldConsumeAsyncApiDocument: ShouldConsumeProduceBase<AsyncApiDocument>
    {
        public ShouldConsumeAsyncApiDocument(): base(typeof(ShouldConsumeAsyncApiDocument))
        {
        }

        [Fact]
        public async void JsonPropertyMinimalSpec()
        {
            var output = _asyncApiAsyncApiReader.Read(GetStream("Minimal.json"));
        
            Assert.Equal("2.3.0", output.Asyncapi);
            Assert.Equal("foo", output.Info.Title);
            Assert.Equal("bar", output.Info.Version);
        }
        
        [Fact]
        public async void JsonPropertyCompleteSpec()
        {
            var output = _asyncApiAsyncApiReader.Read(GetStreamWithMockedExtensions("Complete.json"));
        
            Assert.Equal("2.3.0", output.Asyncapi);
            Assert.Equal("foo", output.Info.Title);
            Assert.Equal("bar", output.Info.Version);
        }
        
        [Fact]
        public async void JsonPropertyCompleteUsingComponentReferencesSpec()
        {
            var output = _asyncApiAsyncApiReader.Read(GetStreamWithMockedExtensions("CompleteUsingComponentReferences.json"));
        
            Assert.Equal("2.3.0", output.Asyncapi);
            Assert.Equal("foo", output.Info.Title);
            Assert.Equal("bar", output.Info.Version);
            Assert.IsType<KafkaServerBinding>(output.Servers["production"].Bindings["kafka"]);
        }
    }
}