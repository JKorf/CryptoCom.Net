using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.OrderBook;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using CryptoCom.Net.Interfaces;
using CryptoCom.Net.Interfaces.Clients;
using CryptoCom.Net.Objects.Options;

namespace CryptoCom.Net.SymbolOrderBooks
{
    /// <summary>
    /// CryptoCom order book factory
    /// </summary>
    public class CryptoComOrderBookFactory : ICryptoComOrderBookFactory
    {
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="serviceProvider">Service provider for resolving logging and clients</param>
        public CryptoComOrderBookFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            
            
            Exchange = new OrderBookFactory<CryptoComOrderBookOptions>((symbol, options) => CreateExchange(symbol, options), (baseAsset, quoteAsset, options) => CreateExchange(baseAsset + "_" + quoteAsset, options));

        }

        
         /// <inheritdoc />
        public IOrderBookFactory<CryptoComOrderBookOptions> Exchange { get; }


        
         /// <inheritdoc />
        public ISymbolOrderBook CreateExchange(string symbol, Action<CryptoComOrderBookOptions>? options = null)
            => new CryptoComExchangeSymbolOrderBook(symbol, options, 
                                                          _serviceProvider.GetRequiredService<ILoggerFactory>(),
                                                          _serviceProvider.GetRequiredService<ICryptoComRestClient>(),
                                                          _serviceProvider.GetRequiredService<ICryptoComSocketClient>());


    }
}
