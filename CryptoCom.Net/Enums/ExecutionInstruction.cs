using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoCom.Net.Enums
{
    /// <summary>
    /// Execution mode
    /// </summary>
    public enum ExecutionInstruction
    {
        /// <summary>
        /// Post only order
        /// </summary>
        [Map("POST_ONLY")]
        PostOnly,
        /// <summary>
        /// Liquidation order
        /// </summary>
        [Map("LIQUIDATION")]
        Liquidation
    }
}
