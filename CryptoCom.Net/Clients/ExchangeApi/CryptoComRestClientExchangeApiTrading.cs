using CryptoExchange.Net.Objects;
using Microsoft.Extensions.Logging;
using CryptoCom.Net.Interfaces.Clients.ExchangeApi;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
using CryptoCom.Net.Objects.Models;
using System.Drawing;
using System;
using CryptoCom.Net.Enums;
using CryptoCom.Net.Objects;

namespace CryptoCom.Net.Clients.ExchangeApi
{
    /// <inheritdoc />
    internal class CryptoComRestClientExchangeApiTrading : ICryptoComRestClientExchangeApiTrading
    {
        private static readonly RequestDefinitionCache _definitions = new RequestDefinitionCache();
        private readonly CryptoComRestClientExchangeApi _baseClient;
        private readonly ILogger _logger;

        internal CryptoComRestClientExchangeApiTrading(ILogger logger, CryptoComRestClientExchangeApi baseClient)
        {
            _baseClient = baseClient;
            _logger = logger;
        }

        #region Get Positions

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<CryptoComPosition>>> GetPositionsAsync(string? symbol = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("instrument_name", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "private/get-positions", CryptoComExchange.RateLimiter.RestPrivate, 1, true);
            var result = await _baseClient.SendAsync<CryptoComPositionWrapper>(request, parameters, ct).ConfigureAwait(false);
            return result.As<IEnumerable<CryptoComPosition>>(result.Data?.Data);
        }

        #endregion

        #region Place Order

        /// <inheritdoc />
        public async Task<WebCallResult<CryptoComOrderId>> PlaceOrderAsync(string symbol, OrderSide side, OrderType type, decimal? quantity = null, decimal? quoteQuantity = null, decimal? price = null, string? clientOrderId = null, bool? postOnly = null, TimeInForce? timeInForce = null, decimal? triggerPrice = null, PriceType? triggerPriceType = null, bool? margin = null, SelfTradePreventionScope? selfTradePreventionScope = null, SelfTradePreventionMode? selfTradePreventionMode = null, string? selfTradePreventionId = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("instrument_name", symbol);
            parameters.AddEnum("side", side);
            parameters.AddEnum("type", type);
            parameters.AddOptionalString("quantity", quantity);
            parameters.AddOptionalString("notional", quoteQuantity);
            parameters.AddOptionalString("price", price);
            parameters.AddOptional("client_oid", clientOrderId);
            parameters.AddOptional("exec_inst", postOnly == true ? "POST_ONLY" : null);
            parameters.AddOptionalEnum("time_in_force", timeInForce);
            parameters.AddOptionalString("ref_price", triggerPrice);
            parameters.AddOptionalEnum("ref_price_type", triggerPriceType);
            parameters.AddOptional("spot_margin", margin == true ? "MARGIN" : null);
            parameters.AddOptionalEnum("stp_scope", selfTradePreventionScope);
            parameters.AddOptionalEnum("stp_inst", selfTradePreventionMode);
            parameters.AddOptional("stp_id", selfTradePreventionId);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "private/create-order", CryptoComExchange.RateLimiter.RestPrivate, 1, true);
            var result = await _baseClient.SendAsync<CryptoComOrderId>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Cancel Order

        /// <inheritdoc />
        public async Task<WebCallResult<CryptoComOrderId>> CancelOrderAsync(string? orderId = null, string? clientOrderId = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("order_id", orderId);
            parameters.AddOptional("client_oid", clientOrderId);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "private/cancel-order", CryptoComExchange.RateLimiter.RestPrivate, 1, true);
            var result = await _baseClient.SendAsync<CryptoComOrderId>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Cancel All Orders

        /// <inheritdoc />
        public async Task<WebCallResult> CancelAllOrdersAsync(string? symbol = null, OrderTypeFilter? type = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("instrument_name", symbol);
            parameters.AddOptionalEnum("type", type);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "private/cancel-all-orders", CryptoComExchange.RateLimiter.RestPrivate, 1, true);
            var result = await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Close Position

        /// <inheritdoc />
        public async Task<WebCallResult<CryptoComOrderId>> ClosePositionAsync(string symbol, OrderType orderType, decimal? price = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("instrument_name", symbol);
            parameters.AddEnum("type", orderType);
            parameters.AddOptionalString("price", price);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "private/close-position", CryptoComExchange.RateLimiter.RestPrivate, 1, true);
            var result = await _baseClient.SendAsync<CryptoComOrderId>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

    }
}
