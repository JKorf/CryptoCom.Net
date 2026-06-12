using CryptoExchange.Net.Objects;
using Microsoft.Extensions.Logging;
using CryptoCom.Net.Interfaces.Clients.ExchangeApi;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
using CryptoCom.Net.Objects.Models;
using System;
using CryptoCom.Net.Enums;
using CryptoCom.Net.Objects;
using CryptoExchange.Net.Converters.SystemTextJson;
using System.Linq;
using CryptoExchange.Net;
using CryptoExchange.Net.Objects.Errors;

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
        public async Task<HttpResult<CryptoComPosition[]>> GetPositionsAsync(string? symbol = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(CryptoComExchange._parameterSerializationSettings);
            parameters.Add("instrument_name", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "private/get-positions", CryptoComExchange.RateLimiter.RestPrivate, 1, true);
            var result = await _baseClient.SendAsync<CryptoComPositionWrapper>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<CryptoComPosition[]>(result);

            return HttpResult.Ok(result, result.Data.Data);
        }

        #endregion

        #region Place Order

        /// <inheritdoc />
        public async Task<HttpResult<CryptoComOrderId>> PlaceOrderAsync(
            string symbol,
            OrderSide side, 
            OrderType type,
            decimal? quantity = null, 
            decimal? quoteQuantity = null, 
            decimal? price = null, 
            string? clientOrderId = null, 
            bool? postOnly = null, 
            TimeInForce? timeInForce = null,
            decimal? triggerPrice = null,
            PriceType? triggerPriceType = null,
            bool? margin = null,
            SelfTradePreventionScope? selfTradePreventionScope = null, 
            SelfTradePreventionMode? selfTradePreventionMode = null,
            string? selfTradePreventionId = null,
            bool? smartPostOnly = null,
            bool? isolatedMargin = null,
            string? isolationId = null,
            int? leverage = null,
            decimal? isolatedMarginQuantity = null,
            bool? reduceOnly = null,
            CancellationToken ct = default)
        {
            if (postOnly == true && smartPostOnly == true)
                throw new ArgumentException("Only one of [postOnly, smartPostOnly] can be set to true");

            var execInsts = new List<string>();
            if (postOnly == true) execInsts.Add("POST_ONLY");
            if (smartPostOnly == true) execInsts.Add("SMART_POST_ONLY");
            if (isolatedMargin == true) execInsts.Add("ISOLATED_MARGIN");
            if (reduceOnly == true) execInsts.Add("REDUCE_ONLY");

            var parameters = new Parameters(CryptoComExchange._parameterSerializationSettings);
            parameters.Add("instrument_name", symbol);
            parameters.Add("side", side);
            parameters.Add("type", type);
            parameters.Add("quantity", quantity);
            parameters.Add("notional", quoteQuantity);
            parameters.Add("price", price);
            parameters.Add("client_oid", clientOrderId ?? ExchangeHelpers.RandomString(32));
            parameters.AddArray("exec_inst", execInsts.Any() ? execInsts.ToArray() : null);
            parameters.Add("time_in_force", timeInForce);
            parameters.Add("ref_price", triggerPrice);
            parameters.Add("ref_price_type", triggerPriceType);
            parameters.Add("spot_margin", margin == true ? "MARGIN" : null);
            parameters.Add("stp_scope", selfTradePreventionScope);
            parameters.Add("stp_inst", selfTradePreventionMode);
            parameters.Add("stp_id", selfTradePreventionId);
            parameters.Add("isolation_id", isolationId);
            parameters.Add("leverage", leverage);
            parameters.Add("isolated_margin_amount", isolatedMarginQuantity);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "private/create-order", CryptoComExchange.RateLimiter.RestPrivateSpecific, 1, true);
            var result = await _baseClient.SendAsync<CryptoComOrderId>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Edit Order

        /// <inheritdoc />
        public async Task<HttpResult<CryptoComOrderId>> EditOrderAsync(decimal newQuantity, decimal newPrice, string? orderId = null, string? clientOrderId = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(CryptoComExchange._parameterSerializationSettings);
            parameters.Add("new_price", newPrice);
            parameters.Add("new_quantity", newQuantity);
            parameters.Add("order_id", orderId);
            parameters.Add("orig_client_oid", clientOrderId);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "private/amend-order", CryptoComExchange.RateLimiter.RestPrivateSpecific, 1, true);
            var result = await _baseClient.SendAsync<CryptoComOrderId>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Cancel Order

        /// <inheritdoc />
        public async Task<HttpResult<CryptoComOrderId>> CancelOrderAsync(string? orderId = null, string? clientOrderId = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(CryptoComExchange._parameterSerializationSettings);
            parameters.Add("order_id", orderId);
            parameters.Add("client_oid", clientOrderId);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "private/cancel-order", CryptoComExchange.RateLimiter.RestPrivateSpecific, 1, true);
            var result = await _baseClient.SendAsync<CryptoComOrderId>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Cancel All Orders

        /// <inheritdoc />
        public async Task<HttpResult> CancelAllOrdersAsync(string? symbol = null, OrderTypeFilter? type = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(CryptoComExchange._parameterSerializationSettings);
            parameters.Add("instrument_name", symbol);
            parameters.Add("type", type);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "private/cancel-all-orders", CryptoComExchange.RateLimiter.RestPrivateSpecific, 1, true);
            var result = await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Close Position

        /// <inheritdoc />
        public async Task<HttpResult<CryptoComOrderId>> ClosePositionAsync(string symbol, OrderType orderType, decimal? price = null, string? isolationId = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(CryptoComExchange._parameterSerializationSettings);
            parameters.Add("instrument_name", symbol);
            parameters.Add("type", orderType);
            parameters.Add("price", price);
            parameters.Add("isolation_id", isolationId);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "private/close-position", CryptoComExchange.RateLimiter.RestPrivate, 1, true);
            var result = await _baseClient.SendAsync<CryptoComOrderId>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Open Orders

        /// <inheritdoc />
        public async Task<HttpResult<CryptoComOrder[]>> GetOpenOrdersAsync(string? symbol = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(CryptoComExchange._parameterSerializationSettings);
            parameters.Add("instrument_name", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "private/get-open-orders", CryptoComExchange.RateLimiter.RestPrivate, 1, true);
            var result = await _baseClient.SendAsync<CryptoComOrderWrapper>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<CryptoComOrder[]>(result);

            return HttpResult.Ok(result, result.Data.Data);
        }

        #endregion

        #region Get Order

        /// <inheritdoc />
        public async Task<HttpResult<CryptoComOrder>> GetOrderAsync(string? orderId = null, string? clientOrderId = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(CryptoComExchange._parameterSerializationSettings);
            parameters.Add("order_id", orderId);
            parameters.Add("client_oid", clientOrderId);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "private/get-order-detail", CryptoComExchange.RateLimiter.RestPrivateSpecific, 1, true);
            var result = await _baseClient.SendAsync<CryptoComOrder>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Closed Orders

        /// <inheritdoc />
        public async Task<HttpResult<CryptoComOrder[]>> GetClosedOrdersAsync(
            string? symbol = null,
            string? isolationId = null,
            DateTime? startTime = null, 
            DateTime? endTime = null, 
            int? limit = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(CryptoComExchange._parameterSerializationSettings);
            parameters.Add("instrument_name", symbol);
            parameters.Add("isolation_id", isolationId);
            parameters.Add("start_time", DateTimeConverter.ConvertToNanoseconds(startTime));
            parameters.Add("end_time", DateTimeConverter.ConvertToNanoseconds(endTime));
            parameters.Add("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "private/get-order-history", CryptoComExchange.RateLimiter.RestPrivateSpecific, 1, true);
            var result = await _baseClient.SendAsync<CryptoComOrderWrapper>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<CryptoComOrder[]>(result);

            return HttpResult.Ok(result, result.Data.Data);
        }

        #endregion

        #region Get User Trades

        /// <inheritdoc />
        public async Task<HttpResult<CryptoComUserTrade[]>> GetUserTradesAsync(
            string? symbol = null,
            string? isolationId = null,
            DateTime? startTime = null,
            DateTime? endTime = null,
            int? limit = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(CryptoComExchange._parameterSerializationSettings);
            parameters.Add("instrument_name", symbol);
            parameters.Add("isolation_id", isolationId);
            parameters.Add("start_time", DateTimeConverter.ConvertToNanoseconds(startTime));
            parameters.Add("end_time", DateTimeConverter.ConvertToNanoseconds(endTime));
            parameters.Add("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "private/get-trades", CryptoComExchange.RateLimiter.RestPrivateSpecific, 1, true);
            var result = await _baseClient.SendAsync<CryptoComUserTradeWrapper>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<CryptoComUserTrade[]>(result);

            return HttpResult.Ok(result, result.Data.Data);
        }

        #endregion

        #region Place Multiple Orders

        /// <inheritdoc />
        public async Task<HttpResult<CallResult<CryptoComOrderResult>[]>> PlaceMultipleOrdersAsync(IEnumerable<CryptoComOrderRequest> orders, CancellationToken ct = default)
        {
            var parameters = new Parameters(CryptoComExchange._parameterSerializationSettings);

            foreach (var order in orders)
                order.ClientOrderId ??= ExchangeHelpers.RandomString(32);

            parameters.Add("contingency_type", "LIST");
            parameters.Add("order_list", orders.ToArray());
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "private/create-order-list", CryptoComExchange.RateLimiter.RestPrivate, 1, true);
            var resultData = await _baseClient.SendAsync<CryptoComOrderResult[]>(request, parameters, ct).ConfigureAwait(false);
            if (!resultData.Success)
                return HttpResult.Fail<CallResult<CryptoComOrderResult>[]>(resultData);

            var result = new List<CallResult<CryptoComOrderResult>>();
            foreach (var item in resultData.Data!)
            {
                if (item.Code != 0)
                    result.Add(CallResult<CryptoComOrderResult>.Fail(new ServerError(item.Code, _baseClient.GetErrorInfo(item.Code, item.Message!))));
                else
                    result.Add(CallResult<CryptoComOrderResult>.Ok(item));
            }

            if (result.All(x => !x.Success))
                return HttpResult.Fail(resultData, new ServerError(new ErrorInfo(ErrorType.AllOrdersFailed, "All orders failed")), result.ToArray());

            return HttpResult.Ok(resultData, result.ToArray());
        }

        #endregion

        #region Cancel Orders

        /// <inheritdoc />
        public async Task<HttpResult<CryptoComCancelOrderResult[]>> CancelOrdersAsync(IEnumerable<CryptoComCancelOrderRequest> orders, CancellationToken ct = default)
        {
            var parameters = new Parameters(CryptoComExchange._parameterSerializationSettings);
            parameters.Add("contingency_type", "LIST");
            parameters.Add("order_list", orders.ToArray());
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "private/cancel-order-list", CryptoComExchange.RateLimiter.RestPrivate, 1, true);
            return await _baseClient.SendAsync<CryptoComCancelOrderResult[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Place OCO Order

        /// <inheritdoc />
        public async Task<HttpResult<CryptoComOcoResult>> PlaceOcoOrderAsync(CryptoComOrderRequest order1, CryptoComOrderRequest order2, CancellationToken ct = default)
        {
            var parameters = new Parameters(CryptoComExchange._parameterSerializationSettings);

            order1.ClientOrderId ??= ExchangeHelpers.RandomString(32);
            order2.ClientOrderId ??= ExchangeHelpers.RandomString(32);

            parameters.Add("contingency_type", "OCO");
            parameters.Add("order_list", new[] { order1, order2 });
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "private/create-order-list", CryptoComExchange.RateLimiter.RestPrivate, 1, true);
            return await _baseClient.SendAsync<CryptoComOcoResult>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Cancel OCO Order

        /// <inheritdoc />
        public async Task<HttpResult> CancelOcoOrderAsync(string symbol, string listId, CancellationToken ct = default)
        {
            var parameters = new Parameters(CryptoComExchange._parameterSerializationSettings);
            parameters.Add("contingency_type", "OCO");
            parameters.Add("list_id", listId);
            parameters.Add("instrument_name", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "private/cancel-order-list", CryptoComExchange.RateLimiter.RestPrivate, 1, true);
            return await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get OCO Order

        /// <inheritdoc />
        public async Task<HttpResult<CryptoComOrder[]>> GetOcoOrderAsync(string symbol, string listId, CancellationToken ct = default)
        {
            var parameters = new Parameters(CryptoComExchange._parameterSerializationSettings);
            parameters.Add("contingency_type", "OCO");
            parameters.Add("list_id", listId);
            parameters.Add("instrument_name", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "private/get-order-list", CryptoComExchange.RateLimiter.RestPrivate, 1, true);
            var result = await _baseClient.SendAsync<CryptoComOrderWrapper>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<CryptoComOrder[]>(result);

            return HttpResult.Ok(result, result.Data.Data);
        }

        #endregion
    }
}
