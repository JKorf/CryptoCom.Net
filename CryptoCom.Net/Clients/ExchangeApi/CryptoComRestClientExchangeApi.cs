using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Objects;
using Microsoft.Extensions.Logging;
using System;
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
using CryptoExchange.Net.Converters.MessageParsing.DynamicConverters;
using CryptoCom.Net.Clients.MessageHandlers;
using System.Net.Http.Headers;

namespace CryptoCom.Net.Clients.ExchangeApi
{
    /// <inheritdoc cref="ICryptoComRestClientExchangeApi" />
    internal partial class CryptoComRestClientExchangeApi : RestApiClient<CryptoComEnvironment, CryptoComAuthenticationProvider, CryptoComCredentials>, ICryptoComRestClientExchangeApi
    {
        #region fields 
        internal new CryptoComRestOptions ClientOptions => (CryptoComRestOptions)base.ClientOptions;

        protected override ErrorMapping ErrorMapping => CryptoComErrors.Errors;
        protected override IRestMessageHandler MessageHandler { get; } = new CryptoComRestMessageHandler(CryptoComErrors.Errors);
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
        internal CryptoComRestClientExchangeApi(ILoggerFactory? loggerFactory, HttpClient? httpClient, CryptoComRestOptions options)
            : base(loggerFactory, CryptoComExchange.Metadata.Id, httpClient, options.Environment.RestClientAddress.AppendPath("/exchange/v1/"), options, options.ExchangeOptions)
        {
            Account = new CryptoComRestClientExchangeApiAccount(this);
            ExchangeData = new CryptoComRestClientExchangeApiExchangeData(_logger, this);
            Staking = new CryptoComRestClientExchangeApiStaking(this);
            Trading = new CryptoComRestClientExchangeApiTrading(_logger, this);
        }
        #endregion

        /// <inheritdoc />
        protected override IMessageSerializer CreateSerializer() => new SystemTextJsonMessageSerializer(SerializerOptions.WithConverters(CryptoComExchange._serializerContext));

        /// <inheritdoc />
        protected override CryptoComAuthenticationProvider CreateAuthenticationProvider(CryptoComCredentials credentials)
            => new CryptoComAuthenticationProvider(credentials);

        internal async Task<HttpResult> SendAsync(RequestDefinition definition, Parameters? parameters, CancellationToken cancellationToken, int? weight = null)
        {
            Parameters? wrapperParameters = parameters;
            if (definition.Method == HttpMethod.Post)
            {
                wrapperParameters = new Parameters(new CryptoComRequest
                {
                    Id = ExchangeHelpers.NextId(),
                    Method = definition.Path.TrimStart('/'),
                    Parameters = parameters ?? new Parameters(CryptoComExchange._parameterSerializationSettings)
                }, CryptoComExchange._parameterSerializationSettings);
            }

            var result = await base.SendAsync<CryptoComResponse>(definition, wrapperParameters, cancellationToken, null, weight).ConfigureAwait(false);
            if (!result.Success)
                return result;

            if (result.Data.Code != 0)
                return HttpResult.Fail(result, new ServerError(result.Data.Code, GetErrorInfo(result.Data.Code, result.Data.Message!)));

            return result;
        }

        internal async Task<HttpResult<T>> SendAsync<T>(RequestDefinition definition, Parameters? parameters, CancellationToken cancellationToken, int? weight = null) where T : class
        {
            Parameters? wrapperParameters = parameters;
            if (definition.Method == HttpMethod.Post)
            {
                wrapperParameters = new Parameters(new CryptoComRequest
                {
                    Id = ExchangeHelpers.NextId(),
                    Method = definition.Path.TrimStart('/'),
                    Parameters = parameters ?? new Parameters(CryptoComExchange._parameterSerializationSettings)
                }, CryptoComExchange._parameterSerializationSettings);
            }

            var result = await base.SendAsync<CryptoComResponse<T>>(definition, wrapperParameters, cancellationToken, null, weight).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<T>(result);

            if (result.Data.Code != 0)
                return HttpResult.Fail<T>(result, new ServerError(result.Data.Code, GetErrorInfo(result.Data.Code, result.Data.Message!)));

            return HttpResult.Ok(result, result.Data.Result);
        }

        /// <inheritdoc />
        protected override Task<HttpResult<DateTime>> GetServerTimestampAsync()
            => ExchangeData.GetServerTimeAsync();

        /// <inheritdoc />
        public override string FormatSymbol(string baseAsset, string quoteAsset, TradingMode tradingMode, DateTime? deliverTime = null)
                => CryptoComExchange.FormatSymbol(baseAsset, quoteAsset, tradingMode, deliverTime);

        /// <inheritdoc />
        public ICryptoComRestClientExchangeApiShared SharedClient => this;

    }
}
