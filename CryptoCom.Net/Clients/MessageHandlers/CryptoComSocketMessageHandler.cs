using CryptoExchange.Net.Converters.MessageParsing.DynamicConverters;
using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Text.Json;

namespace CryptoCom.Net.Clients.MessageHandlers
{
    internal class CryptoComSocketMessageHandler : JsonSocketMessageHandler
    {
        public override JsonSerializerOptions Options { get; } = SerializerOptions.WithConverters(CryptoComExchange._serializerContext);

        protected override MessageEvaluator[] TypeEvaluators { get; } = [

            new MessageEvaluator {
                Priority = 1,
                ForceIfFound = true,
                Fields = [
                    new PropertyFieldReference("method") { Constraint = x => x!.Equals("public/heartbeat", StringComparison.Ordinal) },
                ],
                StaticIdentifier = "public/heartbeat"
            },

            new MessageEvaluator {
                Priority = 2,
                Fields = [
                    new PropertyFieldReference("id") { Constraint = x => long.Parse(x!) > 0 },
                ],
                IdentifyMessageCallback = x => x.FieldValue("id")!
            },

            new MessageEvaluator {
                Priority = 3,
                ForceIfFound = true,
                Fields = [
                    new PropertyFieldReference("channel")
                    { 
                        Depth = 2,
                        Constraint = x => x.Equals("user.order", StringComparison.Ordinal) || x.Equals("user.trade", StringComparison.Ordinal)
                    },
                ],
                IdentifyMessageCallback = x => x.FieldValue("channel")!
            },

            new MessageEvaluator {
                Priority = 4,
                Fields = [
                    new PropertyFieldReference("subscription") { Depth = 2 },
                ],
                IdentifyMessageCallback = x => x.FieldValue("subscription")!
            },
        ];
    }
}
