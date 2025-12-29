using CryptoExchange.Net.Objects;
using CryptoCom.Net.Interfaces.Clients.ExchangeApi;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoCom.Net.Objects.Models;
using System;
using CryptoCom.Net.Enums;
using System.Globalization;

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
        public async Task<WebCallResult<CryptoComBalances[]>> GetBalancesAsync(CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            var request = _definitions.GetOrCreate(HttpMethod.Post, "private/user-balance", CryptoComExchange.RateLimiter.RestPrivate, 1, true);
            var result = await _baseClient.SendAsync<CryptoComBalancesWrapper>(request, parameters, ct).ConfigureAwait(false);
            return result.As<CryptoComBalances[]>(result.Data?.Data);
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

        #region Set Account Leverage

        /// <inheritdoc />
        public async Task<WebCallResult> SetAccountLeverageAsync(string accountId, int leverage, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("account_id", accountId);
            parameters.Add("leverage", leverage);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "private/change-account-leverage", CryptoComExchange.RateLimiter.RestPrivate, 1, true);
            var result = await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Set Account Settings

        /// <inheritdoc />
        public async Task<WebCallResult> SetAccountSettingsAsync(SelfTradePreventionScope? stpScope = null, SelfTradePreventionMode? stpMode = null, long? stpId = null, decimal? leverage = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalEnum("stp_scope", stpScope);
            parameters.AddOptionalEnum("stp_inst", stpMode);
            parameters.AddOptional("stp_id", stpId);
            parameters.AddOptional("leverage", leverage);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "private/change-account-settings", CryptoComExchange.RateLimiter.RestPrivate, 1, true);
            var result = await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Account Settings

        /// <inheritdoc />
        public async Task<WebCallResult<CryptoComAccountSettings[]>> GetAccountSettingsAsync(CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            var request = _definitions.GetOrCreate(HttpMethod.Post, "private/get-account-settings", CryptoComExchange.RateLimiter.RestPrivate, 1, true);
            var result = await _baseClient.SendAsync<CryptoComAccountSettings[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Transaction History

        /// <inheritdoc />
        public async Task<WebCallResult<CryptoComTransaction[]>> GetTransactionHistoryAsync(string? symbol = null, TransactionType? transactionType = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("instrument_name", symbol);
            parameters.AddOptionalEnum("journal_type", transactionType);
            parameters.AddOptional("start_time", DateTimeConverter.ConvertToNanoseconds(startTime)?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptional("end_time", DateTimeConverter.ConvertToNanoseconds(endTime)?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptional("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "private/get-transactions", CryptoComExchange.RateLimiter.RestPrivate, 1, true);
            var result = await _baseClient.SendAsync<CryptoComTransactionWrapper>(request, parameters, ct).ConfigureAwait(false);
            return result.As<CryptoComTransaction[]>(result.Data?.Data);
        }

        #endregion

        #region Get Fee Rate

        /// <inheritdoc />
        public async Task<WebCallResult<CryptoComUserFee>> GetFeeRatesAsync(CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            var request = _definitions.GetOrCreate(HttpMethod.Post, "private/get-fee-rate", CryptoComExchange.RateLimiter.RestPrivate, 1, true);
            var result = await _baseClient.SendAsync<CryptoComUserFee>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Symbol Fee Rate

        /// <inheritdoc />
        public async Task<WebCallResult<CryptoComSymbolFeeRate>> GetSymbolFeeRateAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("instrument_name", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "private/get-instrument-fee-rate", CryptoComExchange.RateLimiter.RestPrivate, 1, true);
            var result = await _baseClient.SendAsync<CryptoComSymbolFeeRate>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Withdraw

        /// <inheritdoc />
        public async Task<WebCallResult<CryptoComWithdrawalResult>> WithdrawAsync(string asset, decimal quantity, string address, string? addressTag = null, string? network = null, string? clientWithdrawId = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("currency", asset);
            parameters.Add("amount", quantity);
            parameters.Add("address", address);
            parameters.AddOptional("address_tag", addressTag);
            parameters.AddOptional("network_id", network);
            parameters.AddOptional("client_wid", clientWithdrawId);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "private/create-withdrawal", CryptoComExchange.RateLimiter.RestPrivate, 1, true);
            var result = await _baseClient.SendAsync<CryptoComWithdrawalResult>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Assets

        /// <inheritdoc />
        public async Task<WebCallResult<Dictionary<string, CryptoComAsset>>> GetAssetsAsync(CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            var request = _definitions.GetOrCreate(HttpMethod.Post, "private/get-currency-networks", CryptoComExchange.RateLimiter.RestPrivate, 1, true);
            var result = await _baseClient.SendAsync<CryptoComAssetWrapper>(request, parameters, ct).ConfigureAwait(false);
            return result.As<Dictionary<string, CryptoComAsset>>(result.Data?.AssetMap);
        }

        #endregion

        #region Get Deposit Addresses

        /// <inheritdoc />
        public async Task<WebCallResult<CryptoComDepositAddress[]>> GetDepositAddressesAsync(string asset, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("currency", asset);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "private/get-deposit-address", CryptoComExchange.RateLimiter.RestPrivate, 1, true);
            var result = await _baseClient.SendAsync<CryptoComDepositAddressWrapper>(request, parameters, ct).ConfigureAwait(false);
            return result.As<CryptoComDepositAddress[]>(result.Data?.DepositAddressList);
        }

        #endregion

        #region Get Deposit History

        /// <inheritdoc />
        public async Task<WebCallResult<CryptoComDeposit[]>> GetDepositHistoryAsync(string? asset = null, DateTime? startTime = null, DateTime? endTime = null, DepositStatus? status = null, int? page = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("currency", asset);
            parameters.AddOptionalMillisecondsString("start_ts", startTime);
            parameters.AddOptionalMillisecondsString("end_ts", endTime);
            parameters.AddOptionalEnum("status", status);
            parameters.AddOptional("page_size", pageSize);
            parameters.AddOptional("page", page);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "private/get-deposit-history", CryptoComExchange.RateLimiter.RestPrivate, 1, true);
            var result = await _baseClient.SendAsync<CryptoComDepositWrapper>(request, parameters, ct).ConfigureAwait(false);
            return result.As<CryptoComDeposit[]>(result.Data?.DepositList);
        }

        #endregion

        #region Get Withdrawal History

        /// <inheritdoc />
        public async Task<WebCallResult<CryptoComWithdrawal[]>> GetWithdrawalHistoryAsync(string? asset = null, DateTime? startTime = null, DateTime? endTime = null, WithdrawalStatus? status = null, int? page = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("currency", asset);
            parameters.AddOptionalMilliseconds("start_ts", startTime);
            parameters.AddOptionalMilliseconds("end_ts", endTime);
            parameters.AddOptionalEnum("status", status);
            parameters.AddOptional("page", page);
            parameters.AddOptional("page_size", pageSize);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "private/get-withdrawal-history", CryptoComExchange.RateLimiter.RestPrivate, 1, true);
            var result = await _baseClient.SendAsync<CryptoComWithdrawalWrapper>(request, parameters, ct).ConfigureAwait(false);
            return result.As<CryptoComWithdrawal[]>(result.Data?.WithdrawalList);
        }

        #endregion

    }
}
