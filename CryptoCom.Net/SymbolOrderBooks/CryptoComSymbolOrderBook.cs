using System;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.OrderBook;
using Microsoft.Extensions.Logging;
using CryptoCom.Net.Clients;
using CryptoCom.Net.Interfaces.Clients;
using CryptoCom.Net.Objects.Options;
using CryptoCom.Net.Objects.Models;

namespace CryptoCom.Net.SymbolOrderBooks
{
    /// <summary>
    /// Implementation for a synchronized order book. After calling Start the order book will sync itself and keep up to date with new data. It will automatically try to reconnect and resync in case of a lost/interrupted connection.
    /// Make sure to check the State property to see if the order book is synced.
    /// </summary>
    public class CryptoComSymbolOrderBook : SymbolOrderBook
    {
        private readonly bool _clientOwner;
        private readonly ICryptoComSocketClient _socketClient;
        private readonly TimeSpan _initialDataTimeout;

        /// <summary>
        /// Create a new order book instance
        /// </summary>
        /// <param name="symbol">The symbol the order book is for</param>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        public CryptoComSymbolOrderBook(string symbol, Action<CryptoComOrderBookOptions>? optionsDelegate = null)
            : this(symbol, optionsDelegate, null, null, null)
        {
            _clientOwner = true;
        }

        /// <summary>
        /// Create a new order book instance
        /// </summary>
        /// <param name="symbol">The symbol the order book is for</param>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        /// <param name="logger">Logger</param>
        /// <param name="restClient">Rest client instance</param>
        /// <param name="socketClient">Socket client instance</param>
        public CryptoComSymbolOrderBook(
            string symbol,
            Action<CryptoComOrderBookOptions>? optionsDelegate,
            ILoggerFactory? logger,
            ICryptoComRestClient? restClient,
            ICryptoComSocketClient? socketClient) : base(logger, "CryptoCom", "Exchange", symbol)
        {
            var options = CryptoComOrderBookOptions.Default.Copy();
            if (optionsDelegate != null)
                optionsDelegate(options);
            Initialize(options);

            _strictLevels = false;
            _sequencesAreConsecutive = options?.Limit == null;

            Levels = options?.Limit;
            _initialDataTimeout = options?.InitialDataTimeout ?? TimeSpan.FromSeconds(30);
            _clientOwner = socketClient == null;
            _socketClient = socketClient ?? new CryptoComSocketClient();
        }

        /// <inheritdoc />
        protected override async Task<CallResult<UpdateSubscription>> DoStartAsync(CancellationToken ct)
        {
            var subResult = await _socketClient.ExchangeApi.SubscribeToOrderBookUpdatesAsync(Symbol, Levels ?? 10, HandleUpdate).ConfigureAwait(false);
            if (!subResult)
                return subResult;

            if (ct.IsCancellationRequested)
            {
                await subResult.Data.CloseAsync().ConfigureAwait(false);
                return subResult.AsError<UpdateSubscription>(new CancellationRequestedError());
            }

            Status = OrderBookStatus.Syncing;
            var setResult = await WaitForSetOrderBookAsync(_initialDataTimeout, ct).ConfigureAwait(false);
            if (!setResult)
            {

                await subResult.Data.CloseAsync().ConfigureAwait(false);
                return setResult.As(subResult.Data);
            }

            return subResult;
        }

        private void HandleUpdate(DataEvent<CryptoComOrderBookUpdate> data)
        {
            if (data.UpdateType == SocketUpdateType.Snapshot)
                SetInitialOrderBook(DateTime.UtcNow.Ticks, data.Data.Bids, data.Data.Asks);
            else
                UpdateOrderBook(DateTime.UtcNow.Ticks, data.Data.Bids, data.Data.Asks);
        }

        /// <inheritdoc />
        protected override async Task<CallResult<bool>> DoResyncAsync(CancellationToken ct)
        {
            return await WaitForSetOrderBookAsync(_initialDataTimeout, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        protected override void Dispose(bool disposing)
        {
            if (_clientOwner)
                _socketClient?.Dispose();

            base.Dispose(disposing);
        }
    }
}
