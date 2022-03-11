namespace LEGO.AsyncAPI.E2E.Tests.Readers.Samples.AsyncApi.AsyncApiMessageTraitObject
{
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using Models;
    using Models.Bindings.MessageBindings;
    using Models.Interfaces;
    using Xunit;

    public class ShouldProduceMessageTrait: ShouldConsumeProduceBase<MessageTrait>
    {
        public ShouldProduceMessageTrait(): base(typeof(ShouldProduceMessageTrait))
        {
        }

        [Fact]
        public void ShouldProduceMinimalSpec()
        {
            Assert.Equal(GetString("Minimal.json"), AsyncApiWriter.Write(new MessageTrait()));
        }
        
        [Fact]
        public void ShouldProduceCompleteSpec()
        {
            Assert.Equal(GetString("Complete.json"), AsyncApiWriter.Write(new MessageTrait()
            {
                Name = "UserSignup",
                Title = "User signup",
                Summary = "Action to sign a user up.",
                Description = "A longer description",
                ContentType = "application/json",
                Headers = new Schema(),
                SchemaFormat = "application/vnd.aai.asyncapi;version=2.3.0",
                CorrelationId = new CorrelationId("foo"),
                ExternalDocs = new ExternalDocumentation(),
                Tags = ImmutableArray<Tag>.Empty,
                Bindings = new Dictionary<string, IMessageBinding>(){{"http", new HttpMessageBinding
                    {
                        Headers = new Schema(),
                        BindingVersion = "foo"
                    }
                }},
                Examples = ImmutableList<MessageExample>.Empty
            }));
        }
    }
}