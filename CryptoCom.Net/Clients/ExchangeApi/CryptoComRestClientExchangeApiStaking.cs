using CryptoExchange.Net.Objects;
using CryptoCom.Net.Clients.ExchangeApi;
using CryptoCom.Net.Interfaces.Clients.ExchangeApi;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
using CryptoCom.Net.Objects.Models;
using System;
using CryptoCom.Net.Enums;

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
        public async Task<WebCallResult<CryptoComStakeResult>> StakeAsync(string symbol, decimal quantity, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("instrument_name", symbol);
            parameters.AddString("quantity", quantity);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "private/staking/stake", CryptoComExchange.RateLimiter.RestStaking, 1, true);
            var result = await _baseClient.SendAsync<CryptoComStakeResult>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Unstake

        /// <inheritdoc />
        public async Task<WebCallResult<CryptoComUnstakeResult>> UnstakeAsync(string symbol, decimal quantity, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("instrument_name", symbol);
            parameters.AddString("quantity", quantity);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "private/staking/unstake", CryptoComExchange.RateLimiter.RestStaking, 1, true);
            var result = await _baseClient.SendAsync<CryptoComUnstakeResult>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Staking Positions

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<CryptoComStakePosition>>> GetStakingPositionsAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("instrument_name", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "private/staking/get-staking-position", CryptoComExchange.RateLimiter.RestStaking, 1, true);
            var result = await _baseClient.SendAsync<CryptoComStakePositionWrapper>(request, parameters, ct).ConfigureAwait(false);
            return result.As<IEnumerable<CryptoComStakePosition>>(result.Data?.Data);
        }

        #endregion

        #region Get Staking Symbols

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<CryptoComStakingSymbol>>> GetStakingSymbolsAsync(CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            var request = _definitions.GetOrCreate(HttpMethod.Post, "private/staking/get-staking-instruments", CryptoComExchange.RateLimiter.RestStaking, 1, true);
            var result = await _baseClient.SendAsync<CryptoComStakingSymbolWrapper>(request, parameters, ct).ConfigureAwait(false);
            return result.As<IEnumerable<CryptoComStakingSymbol>>(result.Data?.Data);
        }

        #endregion

        #region Get Open Staking Requests

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<CryptoComStakingRequest>>> GetOpenStakingRequestsAsync(string? symbol = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("instrument_name", symbol);
            parameters.AddOptionalMillisecondsString("start_time", startTime);
            parameters.AddOptionalMillisecondsString("end_time", endTime);
            parameters.AddOptional("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "private/staking/get-open-stake", CryptoComExchange.RateLimiter.RestStaking, 1, true);
            var result = await _baseClient.SendAsync<CryptoComStakingRequestWrapper>(request, parameters, ct).ConfigureAwait(false);
            return result.As<IEnumerable<CryptoComStakingRequest>>(result.Data?.Data);
        }

        #endregion

        #region Get Staking History

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<CryptoComStakingRequest>>> GetStakingHistoryAsync(string? symbol = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("instrument_name", symbol);
            parameters.AddOptionalMillisecondsString("start_time", startTime);
            parameters.AddOptionalMillisecondsString("end_time", endTime);
            parameters.AddOptional("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "private/staking/get-stake-history", CryptoComExchange.RateLimiter.RestStaking, 1, true);
            var result = await _baseClient.SendAsync<CryptoComStakingRequestWrapper>(request, parameters, ct).ConfigureAwait(false);
            return result.As<IEnumerable<CryptoComStakingRequest>>(result.Data?.Data);
        }

        #endregion

        #region Get Staking Reward History

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<CryptoComStakingReward>>> GetStakingRewardHistoryAsync(string? symbol = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("instrument_name", symbol);
            parameters.AddOptionalMillisecondsString("start_time", startTime);
            parameters.AddOptionalMillisecondsString("end_time", endTime);
            parameters.AddOptional("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "private/staking/get-reward-history", CryptoComExchange.RateLimiter.RestStaking, 1, false);
            var result = await _baseClient.SendAsync<CryptoComStakingRewardWrapper>(request, parameters, ct).ConfigureAwait(false);
            return result.As<IEnumerable<CryptoComStakingReward>>(result.Data?.Data);
        }

        #endregion

        #region Convert

        /// <inheritdoc />
        public async Task<WebCallResult<CryptoComConvertResult>> ConvertAsync(string fromSymbol, string toSymbol, decimal expectedRate, decimal quantity, decimal slippageToleranceBps, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("from_instrument_name", fromSymbol);
            parameters.Add("to_instrument_name", toSymbol);
            parameters.AddString("expected_rate", expectedRate);
            parameters.AddString("from_quantity", quantity);
            parameters.AddString("slippage_tolerance_bps", slippageToleranceBps);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "private/staking/convert", CryptoComExchange.RateLimiter.RestStaking, 1, true);
            var result = await _baseClient.SendAsync<CryptoComConvertResult>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Open Convert Requests

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<CryptoComConvertRequest>>> GetOpenConvertRequestsAsync(DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalMillisecondsString("start_time", startTime);
            parameters.AddOptionalMillisecondsString("end_time", endTime);
            parameters.AddOptional("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "private/staking/get-open-convert", CryptoComExchange.RateLimiter.RestStaking, 1, true);
            var result = await _baseClient.SendAsync<CryptoComConvertRequestWrapper>(request, parameters, ct).ConfigureAwait(false);
            return result.As<IEnumerable<CryptoComConvertRequest>>(result.Data?.Data);
        }

        #endregion

        #region Get Convert Rate

        /// <inheritdoc />
        public async Task<WebCallResult<CryptoComConversionRate>> GetConvertRateAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("instrument_name", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "public/staking/get-conversion-rate", CryptoComExchange.RateLimiter.RestStaking, 1, false);
            var result = await _baseClient.SendAsync<CryptoComConversionRate>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

    }
}
