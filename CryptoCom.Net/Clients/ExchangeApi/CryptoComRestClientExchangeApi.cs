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
using CryptoExchange.Net.Objects.Errors;

namespace CryptoCom.Net.Clients.ExchangeApi
{
    /// <inheritdoc cref="ICryptoComRestClientExchangeApi" />
    internal partial class CryptoComRestClientExchangeApi : RestApiClient, ICryptoComRestClientExchangeApi
    {
        #region fields 
        internal static TimeSyncState _timeSyncState = new TimeSyncState("Exchange Api");

        internal new CryptoComRestOptions ClientOptions => (CryptoComRestOptions)base.ClientOptions;

        protected override ErrorCollection ErrorMapping { get; } = new ErrorCollection([
            new ErrorInfo(ErrorType.Unauthorized, false, "Account suspended", "202"),
            new ErrorInfo(ErrorType.Unauthorized, false, "User does not have derivatives access", "411", "412"),
            new ErrorInfo(ErrorType.Unauthorized, false, "Unauthorized", "40101"),
            new ErrorInfo(ErrorType.Unauthorized, false, "IP not allowed", "40103"),
            new ErrorInfo(ErrorType.Unauthorized, false, "Not allowed based on user tier", "40104"),

            new ErrorInfo(ErrorType.TimestampInvalid, false, "Invalid timestamp", "40102"),

            new ErrorInfo(ErrorType.Timeout, false, "Request timeout", "40801"),

            new ErrorInfo(ErrorType.SystemError, false, "Internal error", "50001"),

            new ErrorInfo(ErrorType.NoPosition, false, "No position", "201", "317"),

            new ErrorInfo(ErrorType.DuplicateClientOrderId, false, "Duplicate client order id", "204"),

            new ErrorInfo(ErrorType.InvalidParameter, false, "Duplicate order id", "205"),
            new ErrorInfo(ErrorType.InvalidParameter, false, "Invalid settle asset", "214"),
            new ErrorInfo(ErrorType.InvalidParameter, false, "Invalid fee asset", "215"),
            new ErrorInfo(ErrorType.InvalidParameter, false, "Maximum entry leverage exceeded", "225"),
            new ErrorInfo(ErrorType.InvalidParameter, false, "Invalid leverage", "226"),
            new ErrorInfo(ErrorType.InvalidParameter, false, "Invalid slippage", "227"),
            new ErrorInfo(ErrorType.InvalidParameter, false, "Invalid trigger type", "230"),
            new ErrorInfo(ErrorType.InvalidParameter, false, "Max effective leverage exceeded", "501"),
            new ErrorInfo(ErrorType.InvalidParameter, false, "Invalid collateral price", "604"),
            new ErrorInfo(ErrorType.InvalidParameter, false, "Max allowed slippage exceeded", "606"),
            new ErrorInfo(ErrorType.InvalidParameter, false, "Bad/invalid request", "40001", "40003"),
            new ErrorInfo(ErrorType.InvalidParameter, false, "Invalid timestamp", "40005"),

            new ErrorInfo(ErrorType.PriceInvalid, false, "Invalid floor price", "228"),
            new ErrorInfo(ErrorType.PriceInvalid, false, "Invalid reference price", "229"),
            new ErrorInfo(ErrorType.PriceInvalid, false, "Invalid price", "308"),
            new ErrorInfo(ErrorType.PriceInvalid, false, "Order price is beyond liquidation price", "310"),
            new ErrorInfo(ErrorType.PriceInvalid, false, "Order price greater than limit up price", "312"),
            new ErrorInfo(ErrorType.PriceInvalid, false, "Order price less than limit down price", "313"),
            new ErrorInfo(ErrorType.PriceInvalid, false, "Limit price too far from current price", "315"),

            new ErrorInfo(ErrorType.QuantityInvalid, false, "Order quantity invalid", "213"),
            new ErrorInfo(ErrorType.QuantityInvalid, false, "Position quantity invalid", "216"),
            new ErrorInfo(ErrorType.QuantityInvalid, false, "Open quantity invalid", "217"),
            new ErrorInfo(ErrorType.QuantityInvalid, false, "Max order quantity exceeded", "314"),
            new ErrorInfo(ErrorType.QuantityInvalid, false, "Less than min order quantity", "415"),

            new ErrorInfo(ErrorType.OrderConfigurationRejected, false, "Invalid order type", "218"),
            new ErrorInfo(ErrorType.OrderConfigurationRejected, false, "Invalid execution instruction", "219"),
            new ErrorInfo(ErrorType.OrderConfigurationRejected, false, "Invalid side", "220"),
            new ErrorInfo(ErrorType.OrderConfigurationRejected, false, "Invalid timeInForce", "221"),
            new ErrorInfo(ErrorType.OrderConfigurationRejected, false, "Rejected by matching engine", "224"),
            new ErrorInfo(ErrorType.OrderConfigurationRejected, false, "FillOrKill order could not be filled immediately", "43003"),
            new ErrorInfo(ErrorType.OrderConfigurationRejected, false, "ImmediateOrCancel order could not be filled immediately", "43004"),
            new ErrorInfo(ErrorType.OrderConfigurationRejected, false, "PostOnly order could not be posted as maker", "43005"),
            new ErrorInfo(ErrorType.OrderConfigurationRejected, false, "Canceled because of Self Trade Prevention", "43012"),

            new ErrorInfo(ErrorType.RiskError, false, "Exceeds account risk limit", "302"),
            new ErrorInfo(ErrorType.RiskError, false, "Exceeds position risk limit", "303"),
            new ErrorInfo(ErrorType.RiskError, false, "Order would lead to immediate liquidation", "304"),
            new ErrorInfo(ErrorType.RiskError, false, "Order would trigger margin call", "305"),

            new ErrorInfo(ErrorType.TargetIncorrectState, false, "Invalid order status", "307"),
            new ErrorInfo(ErrorType.TargetIncorrectState, false, "Position in liquidation", "311"),

            new ErrorInfo(ErrorType.BalanceInsufficient, false, "Insufficient balance", "306"),
            new ErrorInfo(ErrorType.BalanceInsufficient, false, "Exceeds maximum available balance", "321"),
            new ErrorInfo(ErrorType.BalanceInsufficient, false, "Insufficient balance available for withdrawal", "30024"),

            new ErrorInfo(ErrorType.UnknownSymbol, false, "Symbol expired", "206"),
            new ErrorInfo(ErrorType.UnknownSymbol, false, "Invalid symbol", "209"),

            new ErrorInfo(ErrorType.UnknownAsset, false, "Invalid asset", "211"),

            new ErrorInfo(ErrorType.UnknownOrder, false, "Invalid order id", "212"),
            new ErrorInfo(ErrorType.UnknownOrder, false, "No active order", "316"),

            new ErrorInfo(ErrorType.SymbolNotTrading, false, "Symbol not currently trading", "208", "309"),

            new ErrorInfo(ErrorType.InvalidOperation, false, "Account is in margin call", "301"),

            new ErrorInfo(ErrorType.OrderRateLimited, false, "Max amount of orders reached", "319"),

            new ErrorInfo(ErrorType.RequestRateLimited, false, "Too many requests", "42901"),

            ],
            [
                new ErrorEvaluator("40004", (code, msg) =>
                {
                    if (msg?.Equals("Invalid instrument_name") == true)
                        return new ErrorInfo(ErrorType.UnknownSymbol, false, "Invalid symbol name", code);

                    return new ErrorInfo(ErrorType.InvalidParameter, false, "Missing or empty parameter", code);
                })
                ]);
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
                return result.AsDatalessError(new ServerError(result.Data.Code, GetErrorInfo(result.Data.Code, result.Data.Message!)));

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
                return result.AsError<T>(new ServerError(result.Data.Code, GetErrorInfo(result.Data.Code, result.Data.Message!)));

            return result.As(result.Data.Result);
        }

        protected override Error ParseErrorResponse(int httpStatusCode, KeyValuePair<string, string[]>[] responseHeaders, IMessageAccessor accessor, Exception? exception)
        {
            if (!accessor.IsValid)
                return new ServerError(ErrorInfo.Unknown, exception: exception);

            var code = accessor.GetValue<int?>(MessagePath.Get().Property("code"));
            var msg = accessor.GetValue<string>(MessagePath.Get().Property("message"));
            if (msg == null)
                return new ServerError(ErrorInfo.Unknown, exception: exception);

            if (code == null)
                return new ServerError(ErrorInfo.Unknown with { Message = msg }, exception);

            return new ServerError(code.Value, GetErrorInfo(code.Value, msg), exception);
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
