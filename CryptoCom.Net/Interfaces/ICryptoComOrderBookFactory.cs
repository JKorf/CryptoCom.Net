using CryptoExchange.Net.Interfaces;
using System;
using CryptoCom.Net.Objects.Options;
using CryptoExchange.Net.SharedApis;

namespace CryptoCom.Net.Interfaces
{
    /// <summary>
    /// CryptoCom local order book factory
    /// </summary>
    public interface ICryptoComOrderBookFactory
    {
        
        /// <summary>
        /// Exchange order book factory methods
        /// </summary>
        IOrderBookFactory<CryptoComOrderBookOptions> Exchange { get; }

        /// <summary>
        /// Create a SymbolOrderBook for the symbol
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="options">Book options</param>
        /// <returns></returns>
        ISymbolOrderBook Create(SharedSymbol symbol, Action<CryptoComOrderBookOptions>? options = null);

        /// <summary>
        /// Create a new local order book instance
        /// </summary>
        ISymbolOrderBook Create(string symbol, Action<CryptoComOrderBookOptions>? options = null);

    }
}