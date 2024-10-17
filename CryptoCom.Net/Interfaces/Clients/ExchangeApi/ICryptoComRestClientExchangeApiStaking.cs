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
    /// CryptoCom Exchange staking endpoints
    /// </summary>
    public interface ICryptoComRestClientExchangeApiStaking
    {
        /// <summary>
        /// Create a new stake request
        /// <para><a href="https://exchange-docs.crypto.com/exchange/v1/rest-ws/index.html#private-staking-stake" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example SOL.staked</param>
        /// <param name="quantity">Quantity to stake</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<CryptoComStakeResult>> StakeAsync(string symbol, decimal quantity, CancellationToken ct = default);
        
        /// <summary>
        /// Create a new unstake request
        /// <para><a href="https://exchange-docs.crypto.com/exchange/v1/rest-ws/index.html#private-staking-unstake" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `SOL.staked`</param>
        /// <param name="quantity">Quantity to unstake</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<CryptoComUnstakeResult>> UnstakeAsync(string symbol, decimal quantity, CancellationToken ct = default);
        
        /// <summary>
        /// Get current staking positions
        /// <para><a href="https://exchange-docs.crypto.com/exchange/v1/rest-ws/index.html#private-staking-get-staking-position" /></para>
        /// </summary>
        /// <param name="symbol">Filter by symbol, for example `SOL.staked`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<IEnumerable<CryptoComStakePosition>>> GetStakingPositionsAsync(string symbol, CancellationToken ct = default);
        
        /// <summary>
        /// Get staking symbols
        /// <para><a href="https://exchange-docs.crypto.com/exchange/v1/rest-ws/index.html#private-staking-get-staking-instruments" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<IEnumerable<CryptoComStakingSymbol>>> GetStakingSymbolsAsync(CancellationToken ct = default);
        
        /// <summary>
        /// Get open stake/unstake requests
        /// <para><a href="https://exchange-docs.crypto.com/exchange/v1/rest-ws/index.html#private-staking-get-open-stake" /></para>
        /// </summary>
        /// <param name="symbol">Filter by staking symbol, for example `SOL.staked`</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<IEnumerable<CryptoComStakingRequest>>> GetOpenStakingRequestsAsync(string? symbol = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get staking request history
        /// <para><a href="https://exchange-docs.crypto.com/exchange/v1/rest-ws/index.html#private-staking-get-stake-history" /></para>
        /// </summary>
        /// <param name="symbol">Filter by staking symbol</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<IEnumerable<CryptoComStakingRequest>>> GetStakingHistoryAsync(string? symbol = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get staking reward history
        /// <para><a href="https://exchange-docs.crypto.com/exchange/v1/rest-ws/index.html#private-staking-get-reward-history" /></para>
        /// </summary>
        /// <param name="symbol">Filter by staking symbol, for example 'SOL.staked'</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<IEnumerable<CryptoComStakingReward>>> GetStakingRewardHistoryAsync(string? symbol = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Create a new convert request
        /// <para><a href="https://exchange-docs.crypto.com/exchange/v1/rest-ws/index.html#private-staking-convert" /></para>
        /// </summary>
        /// <param name="fromSymbol">Symbol to convert from, for example `ETH.staked` or `CDCETH`</param>
        /// <param name="toSymbol">Symbol to convert to, for example `ETH.staked` or `CDCETH`</param>
        /// <param name="expectedRate">Expected conversion rate</param>
        /// <param name="quantity">Quantity to convert</param>
        /// <param name="slippageToleranceBps">Maximum slippage allowed in basis point</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<CryptoComConvertResult>> ConvertAsync(string fromSymbol, string toSymbol, decimal expectedRate, decimal quantity, decimal slippageToleranceBps, CancellationToken ct = default);

        /// <summary>
        /// Get open convert requests
        /// <para><a href="https://exchange-docs.crypto.com/exchange/v1/rest-ws/index.html#private-staking-get-open-convert" /></para>
        /// </summary>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<IEnumerable<CryptoComConvertRequest>>> GetOpenConvertRequestsAsync(DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get conversion rate
        /// <para><a href="https://exchange-docs.crypto.com/exchange/v1/rest-ws/index.html#public-staking-get-conversion-rate" /></para>
        /// </summary>
        /// <param name="symbol">Symbol name, for example 'CDCETH'</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<CryptoComConversionRate>> GetConvertRateAsync(string symbol, CancellationToken ct = default);

    }
}
