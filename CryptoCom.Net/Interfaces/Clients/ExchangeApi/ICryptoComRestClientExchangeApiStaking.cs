using System.Threading.Tasks;
using System.Threading;
using CryptoExchange.Net.Objects;
using CryptoCom.Net.Objects.Models;
using System;

namespace CryptoCom.Net.Interfaces.Clients.ExchangeApi
{
    /// <summary>
    /// CryptoCom Exchange staking endpoints
    /// </summary>
    public interface ICryptoComRestClientExchangeApiStaking
    {
        /// <summary>
        /// Create a new stake request
        /// <para>
        /// Docs:<br />
        /// <a href="https://exchange-docs.crypto.com/exchange/v1/rest-ws/index.html#private-staking-stake" /><br />
        /// Endpoint:<br />
        /// POST /private/staking/stake
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>instrument_name</c>"] The symbol, for example SOL.staked</param>
        /// <param name="quantity">["<c>quantity</c>"] Quantity to stake</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<CryptoComStakeResult>> StakeAsync(string symbol, decimal quantity, CancellationToken ct = default);
        
        /// <summary>
        /// Create a new unstake request
        /// <para>
        /// Docs:<br />
        /// <a href="https://exchange-docs.crypto.com/exchange/v1/rest-ws/index.html#private-staking-unstake" /><br />
        /// Endpoint:<br />
        /// POST /private/staking/unstake
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>instrument_name</c>"] The symbol, for example `SOL.staked`</param>
        /// <param name="quantity">["<c>quantity</c>"] Quantity to unstake</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<CryptoComUnstakeResult>> UnstakeAsync(string symbol, decimal quantity, CancellationToken ct = default);
        
        /// <summary>
        /// Get current staking positions
        /// <para>
        /// Docs:<br />
        /// <a href="https://exchange-docs.crypto.com/exchange/v1/rest-ws/index.html#private-staking-get-staking-position" /><br />
        /// Endpoint:<br />
        /// POST /private/staking/get-staking-position
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>instrument_name</c>"] Filter by symbol, for example `SOL.staked`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<CryptoComStakePosition[]>> GetStakingPositionsAsync(string symbol, CancellationToken ct = default);
        
        /// <summary>
        /// Get staking symbols
        /// <para>
        /// Docs:<br />
        /// <a href="https://exchange-docs.crypto.com/exchange/v1/rest-ws/index.html#private-staking-get-staking-instruments" /><br />
        /// Endpoint:<br />
        /// POST /private/staking/get-staking-instruments
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<CryptoComStakingSymbol[]>> GetStakingSymbolsAsync(CancellationToken ct = default);
        
        /// <summary>
        /// Get open stake/unstake requests
        /// <para>
        /// Docs:<br />
        /// <a href="https://exchange-docs.crypto.com/exchange/v1/rest-ws/index.html#private-staking-get-open-stake" /><br />
        /// Endpoint:<br />
        /// POST /private/staking/get-open-stake
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>instrument_name</c>"] Filter by staking symbol, for example `SOL.staked`</param>
        /// <param name="startTime">["<c>start_time</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>end_time</c>"] Filter by end time</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<CryptoComStakingRequest[]>> GetOpenStakingRequestsAsync(string? symbol = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get staking request history
        /// <para>
        /// Docs:<br />
        /// <a href="https://exchange-docs.crypto.com/exchange/v1/rest-ws/index.html#private-staking-get-stake-history" /><br />
        /// Endpoint:<br />
        /// POST /private/staking/get-stake-history
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>instrument_name</c>"] Filter by staking symbol</param>
        /// <param name="startTime">["<c>start_time</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>end_time</c>"] Filter by end time</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<CryptoComStakingRequest[]>> GetStakingHistoryAsync(string? symbol = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get staking reward history
        /// <para>
        /// Docs:<br />
        /// <a href="https://exchange-docs.crypto.com/exchange/v1/rest-ws/index.html#private-staking-get-reward-history" /><br />
        /// Endpoint:<br />
        /// POST /private/staking/get-reward-history
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>instrument_name</c>"] Filter by staking symbol, for example 'SOL.staked'</param>
        /// <param name="startTime">["<c>start_time</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>end_time</c>"] Filter by end time</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<CryptoComStakingReward[]>> GetStakingRewardHistoryAsync(string? symbol = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Create a new convert request
        /// <para>
        /// Docs:<br />
        /// <a href="https://exchange-docs.crypto.com/exchange/v1/rest-ws/index.html#private-staking-convert" /><br />
        /// Endpoint:<br />
        /// POST /private/staking/convert
        /// </para>
        /// </summary>
        /// <param name="fromSymbol">["<c>from_instrument_name</c>"] Symbol to convert from, for example `ETH.staked` or `CDCETH`</param>
        /// <param name="toSymbol">["<c>to_instrument_name</c>"] Symbol to convert to, for example `ETH.staked` or `CDCETH`</param>
        /// <param name="expectedRate">["<c>expected_rate</c>"] Expected conversion rate</param>
        /// <param name="quantity">["<c>from_quantity</c>"] Quantity to convert</param>
        /// <param name="slippageToleranceBps">["<c>slippage_tolerance_bps</c>"] Maximum slippage allowed in basis point</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<CryptoComConvertResult>> ConvertAsync(string fromSymbol, string toSymbol, decimal expectedRate, decimal quantity, decimal slippageToleranceBps, CancellationToken ct = default);

        /// <summary>
        /// Get open convert requests
        /// <para>
        /// Docs:<br />
        /// <a href="https://exchange-docs.crypto.com/exchange/v1/rest-ws/index.html#private-staking-get-open-convert" /><br />
        /// Endpoint:<br />
        /// POST /private/staking/get-open-convert
        /// </para>
        /// </summary>
        /// <param name="startTime">["<c>start_time</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>end_time</c>"] Filter by end time</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<CryptoComConvertRequest[]>> GetOpenConvertRequestsAsync(DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get conversion rate
        /// <para>
        /// Docs:<br />
        /// <a href="https://exchange-docs.crypto.com/exchange/v1/rest-ws/index.html#public-staking-get-conversion-rate" /><br />
        /// Endpoint:<br />
        /// POST /public/staking/get-conversion-rate
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>instrument_name</c>"] Symbol name, for example 'CDCETH'</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<CryptoComConversionRate>> GetConvertRateAsync(string symbol, CancellationToken ct = default);

    }
}
