using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CryptoCom.Net.Enums
{
    /// <summary>
    /// Direction
    /// </summary>
    [JsonConverter(typeof(EnumConverter<TransferDirection>))]
    public enum TransferDirection
    {
        /// <summary>
        /// Add to the position
        /// </summary>
        [Map("CREDIT")]
        Credit,
        /// <summary>
        /// Remove from the position
        /// </summary>
        [Map("DEBIT")]
        Debit
    }
}
