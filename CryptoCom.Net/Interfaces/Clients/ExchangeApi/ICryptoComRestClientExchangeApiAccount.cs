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
        Task<WebCallResult<IEnumerable<CryptoComBalances>>> GetBalancesAsync(CancellationToken ct = default);

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

    }
}
