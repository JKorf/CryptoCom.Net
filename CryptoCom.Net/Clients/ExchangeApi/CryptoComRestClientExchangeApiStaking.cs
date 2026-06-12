using CryptoExchange.Net.Objects;
using CryptoCom.Net.Interfaces.Clients.ExchangeApi;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
using CryptoCom.Net.Objects.Models;
using System;

namespace CryptoCom.Net.Clients.ExchangeApi
{
    /// <inheritdoc />
    internal class CryptoComRestClientExchangeApiStaking : ICryptoComRestClientExchangeApiStaking
    {
        private static readonly RequestDefinitionCache _definitions = new RequestDefinitionCache();
        private readonly CryptoComRestClientExchangeApi _baseClient;

        internal CryptoComRestClientExchangeApiStaking(CryptoComRestClientExchangeApi baseClient)
        {
            _baseClient = baseClient;
        }

        #region Stake

        /// <inheritdoc />
        public async Task<HttpResult<CryptoComStakeResult>> StakeAsync(string symbol, decimal quantity, CancellationToken ct = default)
        {
            var parameters = new Parameters(CryptoComExchange._parameterSerializationSettings);
            parameters.Add("instrument_name", symbol);
            parameters.Add("quantity", quantity);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "private/staking/stake", CryptoComExchange.RateLimiter.RestStaking, 1, true);
            var result = await _baseClient.SendAsync<CryptoComStakeResult>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Unstake

        /// <inheritdoc />
        public async Task<HttpResult<CryptoComUnstakeResult>> UnstakeAsync(string symbol, decimal quantity, CancellationToken ct = default)
        {
            var parameters = new Parameters(CryptoComExchange._parameterSerializationSettings);
            parameters.Add("instrument_name", symbol);
            parameters.Add("quantity", quantity);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "private/staking/unstake", CryptoComExchange.RateLimiter.RestStaking, 1, true);
            var result = await _baseClient.SendAsync<CryptoComUnstakeResult>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Staking Positions

        /// <inheritdoc />
        public async Task<HttpResult<CryptoComStakePosition[]>> GetStakingPositionsAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new Parameters(CryptoComExchange._parameterSerializationSettings);
            parameters.Add("instrument_name", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "private/staking/get-staking-position", CryptoComExchange.RateLimiter.RestStaking, 1, true);
            var result = await _baseClient.SendAsync<CryptoComStakePositionWrapper>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<CryptoComStakePosition[]>(result);

            return HttpResult.Ok(result, result.Data.Data);
        }

        #endregion

        #region Get Staking Symbols

        /// <inheritdoc />
        public async Task<HttpResult<CryptoComStakingSymbol[]>> GetStakingSymbolsAsync(CancellationToken ct = default)
        {
            var parameters = new Parameters(CryptoComExchange._parameterSerializationSettings);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "private/staking/get-staking-instruments", CryptoComExchange.RateLimiter.RestStaking, 1, true);
            var result = await _baseClient.SendAsync<CryptoComStakingSymbolWrapper>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<CryptoComStakingSymbol[]>(result);

            return HttpResult.Ok(result, result.Data.Data);
        }

        #endregion

        #region Get Open Staking Requests

        /// <inheritdoc />
        public async Task<HttpResult<CryptoComStakingRequest[]>> GetOpenStakingRequestsAsync(string? symbol = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(CryptoComExchange._parameterSerializationSettings);
            parameters.Add("instrument_name", symbol);
            parameters.Add("start_time", startTime);
            parameters.Add("end_time", endTime);
            parameters.Add("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "private/staking/get-open-stake", CryptoComExchange.RateLimiter.RestStaking, 1, true);
            var result = await _baseClient.SendAsync<CryptoComStakingRequestWrapper>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<CryptoComStakingRequest[]>(result);

            return HttpResult.Ok(result, result.Data.Data);
        }

        #endregion

        #region Get Staking History

        /// <inheritdoc />
        public async Task<HttpResult<CryptoComStakingRequest[]>> GetStakingHistoryAsync(string? symbol = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(CryptoComExchange._parameterSerializationSettings);
            parameters.Add("instrument_name", symbol);
            parameters.Add("start_time", startTime);
            parameters.Add("end_time", endTime);
            parameters.Add("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "private/staking/get-stake-history", CryptoComExchange.RateLimiter.RestStaking, 1, true);
            var result = await _baseClient.SendAsync<CryptoComStakingRequestWrapper>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<CryptoComStakingRequest[]>(result);

            return HttpResult.Ok(result, result.Data.Data);
        }

        #endregion

        #region Get Staking Reward History

        /// <inheritdoc />
        public async Task<HttpResult<CryptoComStakingReward[]>> GetStakingRewardHistoryAsync(string? symbol = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(CryptoComExchange._parameterSerializationSettings);
            parameters.Add("instrument_name", symbol);
            parameters.Add("start_time", startTime);
            parameters.Add("end_time", endTime);
            parameters.Add("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "private/staking/get-reward-history", CryptoComExchange.RateLimiter.RestStaking, 1, false);
            var result = await _baseClient.SendAsync<CryptoComStakingRewardWrapper>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<CryptoComStakingReward[]>(result);

            return HttpResult.Ok(result, result.Data.Data);
        }

        #endregion

        #region Convert

        /// <inheritdoc />
        public async Task<HttpResult<CryptoComConvertResult>> ConvertAsync(string fromSymbol, string toSymbol, decimal expectedRate, decimal quantity, decimal slippageToleranceBps, CancellationToken ct = default)
        {
            var parameters = new Parameters(CryptoComExchange._parameterSerializationSettings);
            parameters.Add("from_instrument_name", fromSymbol);
            parameters.Add("to_instrument_name", toSymbol);
            parameters.Add("expected_rate", expectedRate);
            parameters.Add("from_quantity", quantity);
            parameters.Add("slippage_tolerance_bps", slippageToleranceBps);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "private/staking/convert", CryptoComExchange.RateLimiter.RestStaking, 1, true);
            var result = await _baseClient.SendAsync<CryptoComConvertResult>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Open Convert Requests

        /// <inheritdoc />
        public async Task<HttpResult<CryptoComConvertRequest[]>> GetOpenConvertRequestsAsync(DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(CryptoComExchange._parameterSerializationSettings);
            parameters.Add("start_time", startTime);
            parameters.Add("end_time", endTime);
            parameters.Add("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "private/staking/get-open-convert", CryptoComExchange.RateLimiter.RestStaking, 1, true);
            var result = await _baseClient.SendAsync<CryptoComConvertRequestWrapper>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<CryptoComConvertRequest[]>(result);

            return HttpResult.Ok(result, result.Data.Data);
        }

        #endregion

        #region Get Convert Rate

        /// <inheritdoc />
        public async Task<HttpResult<CryptoComConversionRate>> GetConvertRateAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new Parameters(CryptoComExchange._parameterSerializationSettings);
            parameters.Add("instrument_name", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "public/staking/get-conversion-rate", CryptoComExchange.RateLimiter.RestStaking, 1, false);
            var result = await _baseClient.SendAsync<CryptoComConversionRate>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

    }
}
