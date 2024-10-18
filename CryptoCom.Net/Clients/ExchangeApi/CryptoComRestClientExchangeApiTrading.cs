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
using CryptoExchange.Net.Converters.SystemTextJson;
using System.Linq;

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
            parameters.AddOptional("exec_inst", postOnly == true ? new[] { "POST_ONLY" } : null);
            parameters.AddOptionalEnum("time_in_force", timeInForce);
            parameters.AddOptionalString("ref_price", triggerPrice);
            parameters.AddOptionalEnum("ref_price_type", triggerPriceType);
            parameters.AddOptional("spot_margin", margin == true ? "MARGIN" : null);
            parameters.AddOptionalEnum("stp_scope", selfTradePreventionScope);
            parameters.AddOptionalEnum("stp_inst", selfTradePreventionMode);
            parameters.AddOptional("stp_id", selfTradePreventionId);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "private/create-order", CryptoComExchange.RateLimiter.RestPrivateSpecific, 1, true);
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
            var request = _definitions.GetOrCreate(HttpMethod.Post, "private/cancel-order", CryptoComExchange.RateLimiter.RestPrivateSpecific, 1, true);
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
            var request = _definitions.GetOrCreate(HttpMethod.Post, "private/cancel-all-orders", CryptoComExchange.RateLimiter.RestPrivateSpecific, 1, true);
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

        #region Get Open Orders

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<CryptoComOrder>>> GetOpenOrdersAsync(string? symbol = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("instrument_name", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "private/get-open-orders", CryptoComExchange.RateLimiter.RestPrivate, 1, true);
            var result = await _baseClient.SendAsync<CryptoComOrderWrapper>(request, parameters, ct).ConfigureAwait(false);
            return result.As<IEnumerable<CryptoComOrder>>(result.Data?.Data);
        }

        #endregion

        #region Get Order

        /// <inheritdoc />
        public async Task<WebCallResult<CryptoComOrder>> GetOrderAsync(string? orderId = null, string? clientOrderId = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("order_id", orderId);
            parameters.AddOptional("client_oid", clientOrderId);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "private/get-order-detail", CryptoComExchange.RateLimiter.RestPrivateSpecific, 1, true);
            var result = await _baseClient.SendAsync<CryptoComOrder>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Closed Orders

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<CryptoComOrder>>> GetClosedOrdersAsync(string? symbol = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("instrument_name", symbol);
            parameters.AddOptional("start_time", DateTimeConverter.ConvertToNanoseconds(startTime));
            parameters.AddOptional("end_time", DateTimeConverter.ConvertToNanoseconds(endTime));
            parameters.AddOptional("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "private/get-order-history", CryptoComExchange.RateLimiter.RestPrivateSpecific, 1, true);
            var result = await _baseClient.SendAsync<CryptoComOrderWrapper>(request, parameters, ct).ConfigureAwait(false);
            return result.As<IEnumerable<CryptoComOrder>>(result.Data?.Data);
        }

        #endregion

        #region Get User Trades

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<CryptoComUserTrade>>> GetUserTradesAsync(string? symbol = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("instrument_name", symbol);
            parameters.AddOptional("start_time", DateTimeConverter.ConvertToNanoseconds(startTime));
            parameters.AddOptional("end_time", DateTimeConverter.ConvertToNanoseconds(endTime));
            parameters.AddOptional("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "private/get-trades", CryptoComExchange.RateLimiter.RestPrivateSpecific, 1, true);
            var result = await _baseClient.SendAsync<CryptoComUserTradeWrapper>(request, parameters, ct).ConfigureAwait(false);
            return result.As<IEnumerable<CryptoComUserTrade>>(result.Data?.Data);
        }

        #endregion

        #region Place Multiple Orders

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<CryptoComOrderResult>>> PlaceMultipleOrdersAsync(IEnumerable<CryptoComOrderRequest> orders, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("contingency_type", "LIST");
            parameters.Add("order_list", orders.ToArray());
            var request = _definitions.GetOrCreate(HttpMethod.Post, "private/create-order-list", CryptoComExchange.RateLimiter.RestPrivate, 1, true);
            return await _baseClient.SendAsync<IEnumerable<CryptoComOrderResult>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Cancel Orders

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<CryptoComCancelOrderResult>>> CancelOrdersAsync(IEnumerable<CryptoComCancelOrderRequest> orders, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("contingency_type", "LIST");
            parameters.Add("order_list", orders.ToArray());
            var request = _definitions.GetOrCreate(HttpMethod.Post, "private/cancel-order-list", CryptoComExchange.RateLimiter.RestPrivate, 1, true);
            return await _baseClient.SendAsync<IEnumerable<CryptoComCancelOrderResult>>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Place OCO Order

        /// <inheritdoc />
        public async Task<WebCallResult<CryptoComOcoResult>> PlaceOcoOrderAsync(CryptoComOrderRequest order1, CryptoComOrderRequest order2, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("contingency_type", "OCO");
            parameters.Add("order_list", new[] { order1, order2 });
            var request = _definitions.GetOrCreate(HttpMethod.Post, "private/create-order-list", CryptoComExchange.RateLimiter.RestPrivate, 1, true);
            return await _baseClient.SendAsync<CryptoComOcoResult>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Cancel OCO Order

        /// <inheritdoc />
        public async Task<WebCallResult> CancelOcoOrderAsync(string symbol, string listId, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("contingency_type", "OCO");
            parameters.Add("list_id", listId);
            parameters.Add("instrument_name", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "private/cancel-order-list", CryptoComExchange.RateLimiter.RestPrivate, 1, true);
            return await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get OCO Order

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<CryptoComOrder>>> GetOcoOrderAsync(string symbol, string listId, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("contingency_type", "OCO");
            parameters.Add("list_id", listId);
            parameters.Add("instrument_name", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "private/get-order-list", CryptoComExchange.RateLimiter.RestPrivate, 1, true);
            var result = await _baseClient.SendAsync<CryptoComOrderWrapper>(request, parameters, ct).ConfigureAwait(false);
            return result.As<IEnumerable<CryptoComOrder>>(result.Data?.Data);
        }

        #endregion
    }
}
