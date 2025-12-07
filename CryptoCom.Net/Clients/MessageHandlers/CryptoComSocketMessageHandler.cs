using CryptoCom.Net.Objects.Internal;
using CryptoExchange.Net.Converters.MessageParsing.DynamicConverters;
using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Text.Json;

namespace CryptoCom.Net.Clients.MessageHandlers
{
    internal class CryptoComSocketMessageHandler : JsonSocketMessageHandler
    {
        public override JsonSerializerOptions Options { get; } = SerializerOptions.WithConverters(CryptoComExchange._serializerContext);

        public CryptoComSocketMessageHandler()
        {
            AddTopicMapping<CryptoComResponse>(x => {
                if (x.Id > 0)
                    return x.Id.ToString();

                var result = x.GetResult();
                if (result is CryptoComSubscriptionEvent evnt)
                {
                    if (evnt.Depth != null)
                        return $"{evnt.Symbol}.{evnt.Depth}";

                    if (evnt.Interval != null)
                        return $"{evnt.Interval}.{evnt.Symbol}";

                    return evnt.Symbol;
                }

                return null;
            });

        }

        protected override MessageTypeDefinition[] TypeEvaluators { get; } = [

            new MessageTypeDefinition {
                ForceIfFound = true,
                Fields = [
                    new PropertyFieldReference("method").WithEqualContstraint("public/heartbeat"),
                ],
                StaticIdentifier = "public/heartbeat"
            },

            new MessageTypeDefinition {
                Fields = [
                    new PropertyFieldReference("id").WithCustomContstraint(x => long.Parse(x!) > 0),
                ],
                TypeIdentifierCallback = x => x.FieldValue("id")!
            },

            new MessageTypeDefinition {
                Fields = [
                    new PropertyFieldReference("channel") { Depth = 2 },
                ],
                TypeIdentifierCallback = x => x.FieldValue("channel")!
            },
        ];
    }
}
