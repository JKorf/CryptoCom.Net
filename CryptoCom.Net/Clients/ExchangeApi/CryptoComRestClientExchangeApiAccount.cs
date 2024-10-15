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
    internal class CryptoComRestClientExchangeApiAccount : ICryptoComRestClientExchangeApiAccount
    {
        private static readonly RequestDefinitionCache _definitions = new RequestDefinitionCache();
        private readonly CryptoComRestClientExchangeApi _baseClient;

        internal CryptoComRestClientExchangeApiAccount(CryptoComRestClientExchangeApi baseClient)
        {
            _baseClient = baseClient;
        }

        #region Get Balances

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<CryptoComBalances>>> GetBalancesAsync(CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            var request = _definitions.GetOrCreate(HttpMethod.Post, "private/user-balance", CryptoComExchange.RateLimiter.RestPrivate, 1, true);
            var result = await _baseClient.SendAsync<CryptoComBalancesWrapper>(request, parameters, ct).ConfigureAwait(false);
            return result.As<IEnumerable<CryptoComBalances>>(result.Data?.Data);
        }

        #endregion

        #region Get Balance History

        /// <inheritdoc />
        public async Task<WebCallResult<CryptoComBalanceHistory>> GetBalanceHistoryAsync(Timeframe interval, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddEnum("timeframe", interval);
            parameters.AddOptionalMilliseconds("end_time", endTime);
            parameters.AddOptional("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "private/user-balance-history", CryptoComExchange.RateLimiter.RestPrivate, 1, true);
            var result = await _baseClient.SendAsync<CryptoComBalanceHistory>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Account Info

        /// <inheritdoc />
        public async Task<WebCallResult<CryptoComAccountInfo>> GetAccountInfoAsync(int? page = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("page", page);
            parameters.AddOptional("page_size", pageSize);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "private/get-accounts", CryptoComExchange.RateLimiter.RestPrivate, 1, true);
            var result = await _baseClient.SendAsync<CryptoComAccountInfo>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

    }
}
