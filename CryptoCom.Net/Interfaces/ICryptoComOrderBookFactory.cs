using CryptoExchange.Net.Interfaces;
using System;
using CryptoCom.Net.Objects.Options;

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
        /// Create a new Exchange local order book instance
        /// </summary>
        ISymbolOrderBook CreateExchange(string symbol, Action<CryptoComOrderBookOptions>? options);

    }
}