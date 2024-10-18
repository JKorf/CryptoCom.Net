using CryptoExchange.Net.Attributes;

namespace CryptoCom.Net.Enums
{
    /// <summary>
    /// Order side
    /// </summary>
    public enum OrderSide
    {
        /// <summary>
        /// Unknown
        /// </summary>
        [Map("NULL_VAL")]
        Unknown,
        /// <summary>
        /// Buy
        /// </summary>
        [Map("BUY")]
        Buy,
        /// <summary>
        /// Sell
        /// </summary>
        [Map("SELL")]
        Sell
    }
}
