using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using CryptoExchange.Net.Objects;
using CryptoCom.Net.Objects.Models;
using System;
using CryptoCom.Net.Enums;

namespace CryptoCom.Net.Interfaces.Clients.ExchangeApi
{
    /// <summary>
    /// CryptoCom Exchange account endpoints. Account endpoints include balance info, withdraw/deposit info and requesting and account settings
    /// </summary>
    public interface ICryptoComRestClientExchangeApiAccount
    {
        /// <summary>
        /// Get user account balances
        /// <para><a href="https://exchange-docs.crypto.com/exchange/v1/rest-ws/index.html#private-user-balance" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<CryptoComBalances[]>> GetBalancesAsync(CancellationToken ct = default);

        /// <summary>
        /// Get account cash balance history 
        /// <para><a href="https://exchange-docs.crypto.com/exchange/v1/rest-ws/index.html#private-user-balance-history" /></para>
        /// </summary>
        /// <param name="interval">Interval of the data, either OneHour or OneDay</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<CryptoComBalanceHistory>> GetBalanceHistoryAsync(Timeframe interval, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get master and sub account info
        /// <para><a href="https://exchange-docs.crypto.com/exchange/v1/rest-ws/index.html#private-get-accounts" /></para>
        /// </summary>
        /// <param name="page">Page</param>
        /// <param name="pageSize">Max number of results per page</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<CryptoComAccountInfo>> GetAccountInfoAsync(int? page = null, int? pageSize = null, CancellationToken ct = default);

        /// <summary>
        /// Set account leverage. Not that each symbol has it's own max leverage, the lower of the two will be used
        /// <para><a href="https://exchange-docs.crypto.com/exchange/v1/rest-ws/index.html#private-change-account-leverage" /></para>
        /// </summary>
        /// <param name="accountId">Id of the current account</param>
        /// <param name="leverage">New leverage setting</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult> SetAccountLeverageAsync(string accountId, int leverage, CancellationToken ct = default);

        /// <summary>
        /// Update account settings
        /// <para><a href="https://exchange-docs.crypto.com/exchange/v1/rest-ws/index.html#private-change-account-settings" /></para>
        /// </summary>
        /// <param name="stpScope">Self trade prevention scope</param>
        /// <param name="stpMode">Self trade prevention mode</param>
        /// <param name="stpId">Self trade prevention id</param>
        /// <param name="leverage">Leverage</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult> SetAccountSettingsAsync(SelfTradePreventionScope? stpScope = null, SelfTradePreventionMode? stpMode = null, long? stpId = null, decimal? leverage = null, CancellationToken ct = default);

        /// <summary>
        /// Get current account settings
        /// <para><a href="https://exchange-docs.crypto.com/exchange/v1/rest-ws/index.html#private-get-account-settings" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<CryptoComAccountSettings[]>> GetAccountSettingsAsync(CancellationToken ct = default);

        /// <summary>
        /// Get account transaction history
        /// <para><a href="https://exchange-docs.crypto.com/exchange/v1/rest-ws/index.html#private-get-transactions" /></para>
        /// </summary>
        /// <param name="symbol">Filter by symbol</param>
        /// <param name="transactionType">Filter by transaction type</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<CryptoComTransaction[]>> GetTransactionHistoryAsync(string? symbol = null, TransactionType? transactionType = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get user fee rates
        /// <para><a href="https://exchange-docs.crypto.com/exchange/v1/rest-ws/index.html#private-get-fee-rate" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<CryptoComUserFee>> GetFeeRatesAsync(CancellationToken ct = default);

        /// <summary>
        /// Get symbol fee rates
        /// <para><a href="https://exchange-docs.crypto.com/exchange/v1/rest-ws/index.html#private-get-instrument-fee-rate" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETHUSDT`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<CryptoComSymbolFeeRate>> GetSymbolFeeRateAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Withdraw funds
        /// <para><a href="https://exchange-docs.crypto.com/exchange/v1/rest-ws/index.html#private-create-withdrawal" /></para>
        /// </summary>
        /// <param name="asset">Asset to withdraw</param>
        /// <param name="quantity">Quantity</param>
        /// <param name="address">Target address</param>
        /// <param name="addressTag">Address tag</param>
        /// <param name="network">Network to use</param>
        /// <param name="clientWithdrawId">Client id</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<CryptoComWithdrawalResult>> WithdrawAsync(string asset, decimal quantity, string address, string? addressTag = null, string? network = null, string? clientWithdrawId = null, CancellationToken ct = default);

        /// <summary>
        /// Get asset network info
        /// <para><a href="https://exchange-docs.crypto.com/exchange/v1/rest-ws/index.html#private-get-currency-networks" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<Dictionary<string, CryptoComAsset>>> GetAssetsAsync(CancellationToken ct = default);

        /// <summary>
        /// Get deposit addresses for an asset
        /// <para><a href="https://exchange-docs.crypto.com/exchange/v1/rest-ws/index.html#private-get-deposit-address" /></para>
        /// </summary>
        /// <param name="asset">Asset name</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<CryptoComDepositAddress[]>> GetDepositAddressesAsync(string asset, CancellationToken ct = default);

        /// <summary>
        /// Get deposit history
        /// <para><a href="https://exchange-docs.crypto.com/exchange/v1/rest-ws/index.html#private-get-deposit-history" /></para>
        /// </summary>
        /// <param name="asset">Filter by asset</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="status">Filter by status</param>
        /// <param name="page">Page</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<CryptoComDeposit[]>> GetDepositHistoryAsync(string? asset = null, DateTime? startTime = null, DateTime? endTime = null, DepositStatus? status = null, int? page = null, int? pageSize = null, CancellationToken ct = default);

        /// <summary>
        /// Get withdrawal history
        /// <para><a href="https://exchange-docs.crypto.com/exchange/v1/rest-ws/index.html#private-get-withdrawal-history" /></para>
        /// </summary>
        /// <param name="asset">Filter by asset</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="status">Filter by status</param>
        /// <param name="page">Page number</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<CryptoComWithdrawal[]>> GetWithdrawalHistoryAsync(string? asset = null, DateTime? startTime = null, DateTime? endTime = null, WithdrawalStatus? status = null, int? page = null, int? pageSize = null, CancellationToken ct = default);

    }
}
