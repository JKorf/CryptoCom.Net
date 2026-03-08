using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Text.Json.Serialization;

namespace CryptoCom.Net.Objects.Models
{
    [SerializationModel]
    internal record CryptoComBalancesWrapper
    {
        /// <summary>
        /// ["<c>data</c>"] Data
        /// </summary>
        [JsonPropertyName("data")]
        public CryptoComBalances[] Data { get; set; } = Array.Empty<CryptoComBalances>();
    }

    /// <summary>
    /// 
    /// </summary>
    [SerializationModel]
    public record CryptoComBalances
    {
        /// <summary>
        /// ["<c>total_available_balance</c>"] Total available balance
        /// </summary>
        [JsonPropertyName("total_available_balance")]
        public decimal TotalAvailableBalance { get; set; }
        /// <summary>
        /// ["<c>total_margin_balance</c>"] Total margin balance
        /// </summary>
        [JsonPropertyName("total_margin_balance")]
        public decimal TotalMarginBalance { get; set; }
        /// <summary>
        /// ["<c>total_initial_margin</c>"] Total initial margin
        /// </summary>
        [JsonPropertyName("total_initial_margin")]
        public decimal TotalInitialMargin { get; set; }
        /// <summary>
        /// ["<c>total_position_im</c>"] Total position initial margin
        /// </summary>
        [JsonPropertyName("total_position_im")]
        public decimal TotalPositionIm { get; set; }
        /// <summary>
        /// ["<c>total_haircut</c>"] Total haircut
        /// </summary>
        [JsonPropertyName("total_haircut")]
        public decimal TotalHaircut { get; set; }
        /// <summary>
        /// ["<c>total_maintenance_margin</c>"] Total maintenance margin
        /// </summary>
        [JsonPropertyName("total_maintenance_margin")]
        public decimal TotalMaintenanceMargin { get; set; }
        /// <summary>
        /// ["<c>total_position_cost</c>"] Total position cost
        /// </summary>
        [JsonPropertyName("total_position_cost")]
        public decimal TotalPositionCost { get; set; }
        /// <summary>
        /// ["<c>total_cash_balance</c>"] Total cash balance
        /// </summary>
        [JsonPropertyName("total_cash_balance")]
        public decimal TotalCashBalance { get; set; }
        /// <summary>
        /// ["<c>total_collateral_value</c>"] Total collateral value
        /// </summary>
        [JsonPropertyName("total_collateral_value")]
        public decimal TotalCollateralValue { get; set; }
        /// <summary>
        /// ["<c>total_session_unrealized_pnl</c>"] Total session unrealized pnl
        /// </summary>
        [JsonPropertyName("total_session_unrealized_pnl")]
        public decimal TotalSessionUnrealizedPnl { get; set; }
        /// <summary>
        /// ["<c>instrument_name</c>"] Asset name
        /// </summary>
        [JsonPropertyName("instrument_name")]
        public string AssetName { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>total_session_realized_pnl</c>"] Total session realized pnl
        /// </summary>
        [JsonPropertyName("total_session_realized_pnl")]
        public decimal TotalSessionRealizedPnl { get; set; }
        /// <summary>
        /// ["<c>is_liquidating</c>"] Is liquidating
        /// </summary>
        [JsonPropertyName("is_liquidating")]
        public bool IsLiquidating { get; set; }
        /// <summary>
        /// ["<c>total_effective_leverage</c>"] Total effective leverage
        /// </summary>
        [JsonPropertyName("total_effective_leverage")]
        public decimal TotalEffectiveLeverage { get; set; }
        /// <summary>
        /// ["<c>position_limit</c>"] Position limit
        /// </summary>
        [JsonPropertyName("position_limit")]
        public decimal PositionLimit { get; set; }
        /// <summary>
        /// ["<c>used_position_limit</c>"] Used position limit
        /// </summary>
        [JsonPropertyName("used_position_limit")]
        public decimal UsedPositionLimit { get; set; }
        /// <summary>
        /// ["<c>total_isolated_cash_balance </c>"] Total isolated cash balance
        /// </summary>
        [JsonPropertyName("total_isolated_cash_balance ")]
        public decimal TotalIsolatedCashBalance { get; set; }
        /// <summary>
        /// ["<c>position_balances</c>"] Position balances
        /// </summary>
        [JsonPropertyName("position_balances")]
        public CryptoComBalance[] PositionBalances { get; set; } = Array.Empty<CryptoComBalance>();
    }

    /// <summary>
    /// Asset balance
    /// </summary>
    [SerializationModel]
    public record CryptoComBalance
    {
        /// <summary>
        /// ["<c>instrument_name</c>"] Asset name
        /// </summary>
        [JsonPropertyName("instrument_name")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>quantity</c>"] Quantity
        /// </summary>
        [JsonPropertyName("quantity")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>market_value</c>"] Market value
        /// </summary>
        [JsonPropertyName("market_value")]
        public decimal MarketValue { get; set; }
        /// <summary>
        /// ["<c>collateral_eligible</c>"] Collateral eligible
        /// </summary>
        [JsonPropertyName("collateral_eligible")]
        public bool CollateralEligible { get; set; }
        /// <summary>
        /// ["<c>haircut</c>"] Haircut
        /// </summary>
        [JsonPropertyName("haircut")]
        public decimal Haircut { get; set; }
        /// <summary>
        /// ["<c>collateral_amount</c>"] Collateral quantity
        /// </summary>
        [JsonPropertyName("collateral_amount")]
        public decimal CollateralQuantity { get; set; }
        /// <summary>
        /// ["<c>max_withdrawal_balance</c>"] Max withdrawal balance
        /// </summary>
        [JsonPropertyName("max_withdrawal_balance")]
        public decimal MaxWithdrawalBalance { get; set; }
        /// <summary>
        /// ["<c>reserved_qty</c>"] Reserved quantity
        /// </summary>
        [JsonPropertyName("reserved_qty")]
        public decimal ReservedQuantity { get; set; }
    }


}
