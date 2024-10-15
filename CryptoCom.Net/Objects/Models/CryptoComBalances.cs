using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace CryptoCom.Net.Objects.Models
{
    internal record CryptoComBalancesWrapper
    {
        /// <summary>
        /// Data
        /// </summary>
        [JsonPropertyName("data")]
        public IEnumerable<CryptoComBalances> Data { get; set; } = Array.Empty<CryptoComBalances>();
    }

    /// <summary>
    /// 
    /// </summary>
    public record CryptoComBalances
    {
        /// <summary>
        /// Total available balance
        /// </summary>
        [JsonPropertyName("total_available_balance")]
        public decimal TotalAvailableBalance { get; set; }
        /// <summary>
        /// Total margin balance
        /// </summary>
        [JsonPropertyName("total_margin_balance")]
        public decimal TotalMarginBalance { get; set; }
        /// <summary>
        /// Total initial margin
        /// </summary>
        [JsonPropertyName("total_initial_margin")]
        public decimal TotalInitialMargin { get; set; }
        /// <summary>
        /// Total position initial margin
        /// </summary>
        [JsonPropertyName("total_position_im")]
        public decimal TotalPositionIm { get; set; }
        /// <summary>
        /// Total haircut
        /// </summary>
        [JsonPropertyName("total_haircut")]
        public decimal TotalHaircut { get; set; }
        /// <summary>
        /// Total maintenance margin
        /// </summary>
        [JsonPropertyName("total_maintenance_margin")]
        public decimal TotalMaintenanceMargin { get; set; }
        /// <summary>
        /// Total position cost
        /// </summary>
        [JsonPropertyName("total_position_cost")]
        public decimal TotalPositionCost { get; set; }
        /// <summary>
        /// Total cash balance
        /// </summary>
        [JsonPropertyName("total_cash_balance")]
        public decimal TotalCashBalance { get; set; }
        /// <summary>
        /// Total collateral value
        /// </summary>
        [JsonPropertyName("total_collateral_value")]
        public decimal TotalCollateralValue { get; set; }
        /// <summary>
        /// Total session unrealized pnl
        /// </summary>
        [JsonPropertyName("total_session_unrealized_pnl")]
        public decimal TotalSessionUnrealizedPnl { get; set; }
        /// <summary>
        /// Asset name
        /// </summary>
        [JsonPropertyName("instrument_name")]
        public string AssetName { get; set; } = string.Empty;
        /// <summary>
        /// Total session realized pnl
        /// </summary>
        [JsonPropertyName("total_session_realized_pnl")]
        public decimal TotalSessionRealizedPnl { get; set; }
        /// <summary>
        /// Is liquidating
        /// </summary>
        [JsonPropertyName("is_liquidating")]
        public bool IsLiquidating { get; set; }
        /// <summary>
        /// Total effective leverage
        /// </summary>
        [JsonPropertyName("total_effective_leverage")]
        public decimal TotalEffectiveLeverage { get; set; }
        /// <summary>
        /// Position limit
        /// </summary>
        [JsonPropertyName("position_limit")]
        public decimal PositionLimit { get; set; }
        /// <summary>
        /// Used position limit
        /// </summary>
        [JsonPropertyName("used_position_limit")]
        public decimal UsedPositionLimit { get; set; }
        /// <summary>
        /// Position balances
        /// </summary>
        [JsonPropertyName("position_balances")]
        public IEnumerable<CryptoComBalance> PositionBalances { get; set; } = Array.Empty<CryptoComBalance>();
    }

    /// <summary>
    /// Asset balance
    /// </summary>
    public record CryptoComBalance
    {
        /// <summary>
        /// Asset name
        /// </summary>
        [JsonPropertyName("instrument_name")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonPropertyName("quantity")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Market value
        /// </summary>
        [JsonPropertyName("market_value")]
        public decimal MarketValue { get; set; }
        /// <summary>
        /// Collateral eligible
        /// </summary>
        [JsonPropertyName("collateral_eligible")]
        public bool CollateralEligible { get; set; }
        /// <summary>
        /// Haircut
        /// </summary>
        [JsonPropertyName("haircut")]
        public decimal Haircut { get; set; }
        /// <summary>
        /// Collateral quantity
        /// </summary>
        [JsonPropertyName("collateral_amount")]
        public decimal CollateralQuantity { get; set; }
        /// <summary>
        /// Max withdrawal balance
        /// </summary>
        [JsonPropertyName("max_withdrawal_balance")]
        public decimal MaxWithdrawalBalance { get; set; }
        /// <summary>
        /// Reserved quantity
        /// </summary>
        [JsonPropertyName("reserved_qty")]
        public decimal ReservedQuantity { get; set; }
    }


}
