using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Objects;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using CryptoCom.Net.Interfaces.Clients.ExchangeApi;
using CryptoCom.Net.Objects.Options;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.SharedApis;
using CryptoCom.Net.Objects.Internal;
using CryptoExchange.Net.Converters.MessageParsing;

namespace CryptoCom.Net.Clients.ExchangeApi
{
    /// <inheritdoc cref="ICryptoComRestClientExchangeApi" />
    internal partial class CryptoComRestClientExchangeApi : RestApiClient, ICryptoComRestClientExchangeApi
    {
        #region fields 
        internal static TimeSyncState _timeSyncState = new TimeSyncState("Exchange Api");

        internal new CryptoComRestOptions ClientOptions => (CryptoComRestOptions)base.ClientOptions;
        #endregion

        #region Api clients
        /// <inheritdoc />
        public ICryptoComRestClientExchangeApiAccount Account { get; }
        /// <inheritdoc />
        public ICryptoComRestClientExchangeApiExchangeData ExchangeData { get; }
        /// <inheritdoc />
        public ICryptoComRestClientExchangeApiStaking Staking { get; }
        /// <inheritdoc />
        public ICryptoComRestClientExchangeApiTrading Trading { get; }
        /// <inheritdoc />
        public string ExchangeName => "CryptoCom";
        #endregion

        #region constructor/destructor
        internal CryptoComRestClientExchangeApi(ILogger logger, HttpClient? httpClient, CryptoComRestOptions options)
            : base(logger, httpClient, options.Environment.RestClientAddress.AppendPath("/exchange/v1/"), options, options.ExchangeOptions)
        {
            Account = new CryptoComRestClientExchangeApiAccount(this);
            ExchangeData = new CryptoComRestClientExchangeApiExchangeData(logger, this);
            Staking = new CryptoComRestClientExchangeApiStaking(this);
            Trading = new CryptoComRestClientExchangeApiTrading(logger, this);
        }
        #endregion

        /// <inheritdoc />
        protected override IStreamMessageAccessor CreateAccessor() => new SystemTextJsonStreamMessageAccessor(SerializerOptions.WithConverters(CryptoComExchange._serializerContext));
        /// <inheritdoc />
        protected override IMessageSerializer CreateSerializer() => new SystemTextJsonMessageSerializer(SerializerOptions.WithConverters(CryptoComExchange._serializerContext));

        /// <inheritdoc />
        protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
            => new CryptoComAuthenticationProvider(credentials);

        internal Task<WebCallResult> SendAsync(RequestDefinition definition, ParameterCollection? parameters, CancellationToken cancellationToken, int? weight = null)
            => SendToAddressAsync(BaseAddress, definition, parameters, cancellationToken, weight);

        internal async Task<WebCallResult> SendToAddressAsync(string baseAddress, RequestDefinition definition, ParameterCollection? parameters, CancellationToken cancellationToken, int? weight = null)
        {
            ParameterCollection? wrapperParameters = parameters;
            if (definition.Method == HttpMethod.Post)
            {
                wrapperParameters = new ParameterCollection();
                wrapperParameters.SetBody(new CryptoComRequest
                {
                    Id = ExchangeHelpers.NextId(),
                    Method = definition.Path,
                    Parameters = parameters ?? new ParameterCollection()
                });
            }

            var result = await base.SendAsync<CryptoComResponse>(baseAddress, definition, wrapperParameters, cancellationToken, null, weight).ConfigureAwait(false);
            if (!result)
                return result.AsDataless();

            if (result.Data.Code != 0)
                return result.AsDatalessError(new ServerError(result.Data.Code, result.Data.Message!));

            return result.AsDataless();
        }

        internal Task<WebCallResult<T>> SendAsync<T>(RequestDefinition definition, ParameterCollection? parameters, CancellationToken cancellationToken, int? weight = null) where T : class
            => SendToAddressAsync<T>(BaseAddress, definition, parameters, cancellationToken, weight);

        internal async Task<WebCallResult<T>> SendToAddressAsync<T>(string baseAddress, RequestDefinition definition, ParameterCollection? parameters, CancellationToken cancellationToken, int? weight = null) where T : class
        {
            ParameterCollection? wrapperParameters = parameters;
            if (definition.Method == HttpMethod.Post)
            {
                wrapperParameters = new ParameterCollection();
                wrapperParameters.SetBody(new CryptoComRequest
                {
                    Id = ExchangeHelpers.NextId(),
                    Method = definition.Path,
                    Parameters = parameters ?? new ParameterCollection()
                });
            }

            var result = await base.SendAsync<CryptoComResponse<T>>(baseAddress, definition, wrapperParameters, cancellationToken, null, weight).ConfigureAwait(false);
            if (!result)
                return result.As<T>(default);

            if (result.Data.Code != 0)
                return result.AsError<T>(new ServerError(result.Data.Code, result.Data.Message!));

            return result.As(result.Data.Result);
        }

        protected override Error ParseErrorResponse(int httpStatusCode, KeyValuePair<string, string[]>[] responseHeaders, IMessageAccessor accessor, Exception? exception)
        {
            if (!accessor.IsValid)
                return new ServerError(null, "Unknown request error", exception: exception);

            var code = accessor.GetValue<int?>(MessagePath.Get().Property("code"));
            var msg = accessor.GetValue<string>(MessagePath.Get().Property("message"));
            if (msg == null)
                return new ServerError(null, "Unknown request error", exception: exception);

            if (code == null)
                return new ServerError(null, msg, exception);

            return new ServerError(code.Value, msg, exception);
        }

        /// <inheritdoc />
        protected override Task<WebCallResult<DateTime>> GetServerTimestampAsync()
            => ExchangeData.GetServerTimeAsync();

        /// <inheritdoc />
        public override TimeSyncInfo? GetTimeSyncInfo()
            => new TimeSyncInfo(_logger, ApiOptions.AutoTimestamp ?? ClientOptions.AutoTimestamp, ApiOptions.TimestampRecalculationInterval ?? ClientOptions.TimestampRecalculationInterval, _timeSyncState);

        /// <inheritdoc />
        public override TimeSpan? GetTimeOffset()
            => _timeSyncState.TimeOffset;

        /// <inheritdoc />
        public override string FormatSymbol(string baseAsset, string quoteAsset, TradingMode tradingMode, DateTime? deliverTime = null)
                => CryptoComExchange.FormatSymbol(baseAsset, quoteAsset, tradingMode, deliverTime);

        /// <inheritdoc />
        public ICryptoComRestClientExchangeApiShared SharedClient => this;

    }
}
