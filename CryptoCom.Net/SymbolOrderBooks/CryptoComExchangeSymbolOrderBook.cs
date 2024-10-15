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

namespace CryptoCom.Net.SymbolOrderBooks
{
    /// <summary>
    /// Implementation for a synchronized order book. After calling Start the order book will sync itself and keep up to date with new data. It will automatically try to reconnect and resync in case of a lost/interrupted connection.
    /// Make sure to check the State property to see if the order book is synced.
    /// </summary>
    public class CryptoComExchangeSymbolOrderBook : SymbolOrderBook
    {
        private readonly bool _clientOwner;
        private readonly ICryptoComRestClient _restClient;
        private readonly ICryptoComSocketClient _socketClient;
        private readonly TimeSpan _initialDataTimeout;

        /// <summary>
        /// Create a new order book instance
        /// </summary>
        /// <param name="symbol">The symbol the order book is for</param>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        public CryptoComExchangeSymbolOrderBook(string symbol, Action<CryptoComOrderBookOptions>? optionsDelegate = null)
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
        public CryptoComExchangeSymbolOrderBook(
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
            _restClient = restClient ?? new CryptoComRestClient();
        }

        /// <inheritdoc />
        protected override async Task<CallResult<UpdateSubscription>> DoStartAsync(CancellationToken ct)
        {
            // XXX
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        protected override void DoReset()
        {
        }

        /// <inheritdoc />
        protected override async Task<CallResult<bool>> DoResyncAsync(CancellationToken ct)
        {
            // XXX
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        protected override void Dispose(bool disposing)
        {
            if (_clientOwner)
            {
                _restClient?.Dispose();
                _socketClient?.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
