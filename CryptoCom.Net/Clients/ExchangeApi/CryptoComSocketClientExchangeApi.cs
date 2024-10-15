using System;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Converters.MessageParsing;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net.Sockets;
using Microsoft.Extensions.Logging;
using CryptoCom.Net.Interfaces.Clients.ExchangeApi;
using CryptoCom.Net.Objects.Models;
using CryptoCom.Net.Objects.Options;
using CryptoCom.Net.Objects.Sockets.Subscriptions;

namespace CryptoCom.Net.Clients.ExchangeApi
{
    /// <summary>
    /// Client providing access to the CryptoCom Exchange websocket Api
    /// </summary>
    internal partial class CryptoComSocketClientExchangeApi : SocketApiClient, ICryptoComSocketClientExchangeApi
    {
        #region fields
        private static readonly MessagePath _idPath = MessagePath.Get().Property("id");
        #endregion

        #region constructor/destructor

        /// <summary>
        /// ctor
        /// </summary>
        internal CryptoComSocketClientExchangeApi(ILogger logger, CryptoComSocketOptions options) :
            base(logger, options.Environment.SocketClientAddress!, options, options.ExchangeOptions)
        {
        }
        #endregion

        /// <inheritdoc />
        protected override IByteMessageAccessor CreateAccessor() => new SystemTextJsonByteMessageAccessor();
        /// <inheritdoc />
        protected override IMessageSerializer CreateSerializer() => new SystemTextJsonMessageSerializer();

        /// <inheritdoc />
        protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
            => new CryptoComAuthenticationProvider(credentials);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToXXXUpdatesAsync(Action<DataEvent<CryptoComModel>> onMessage, CancellationToken ct = default)
        {
            var subscription = new CryptoComSubscription<CryptoComModel>(_logger, new [] { "XXX" }, onMessage, false);
            return await SubscribeAsync(subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public override string? GetListenerIdentifier(IMessageAccessor message)
        {
            return message.GetValue<string>(_idPath);
        }

        /// <inheritdoc />
        protected override Query? GetAuthenticationRequest(SocketConnection connection) => null;

        /// <inheritdoc />
        public ICryptoComSocketClientExchangeApiShared SharedClient => this;

        /// <inheritdoc />
        public override string FormatSymbol(string baseAsset, string quoteAsset, TradingMode tradeMode, DateTime? deliverDate) => throw new NotImplementedException();
    }
}
