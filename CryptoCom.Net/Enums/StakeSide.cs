using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoCom.Net.Enums
{
    /// <summary>
    /// Staking request side
    /// </summary>
    public enum StakeSide
    {
        /// <summary>
        /// Stake
        /// </summary>
        [Map("STAKE")]
        Stake,
        /// <summary>
        /// Unstake
        /// </summary>
        [Map("UNSTAKE")]
        Unstake
    }

}
