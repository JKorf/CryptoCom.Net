using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace CryptoCom.Net.Enums
{
    /// <summary>
    /// Type of transaction
    /// </summary>
    [JsonConverter(typeof(EnumConverter<TransactionType>))]
    public enum TransactionType
    {
        /// <summary>
        /// ["<c>TRADING</c>"] Trading
        /// </summary>
        [Map("TRADING")]
        Trading,
        /// <summary>
        /// ["<c>TRADE_FEE</c>"] Trade fee
        /// </summary>
        [Map("TRADE_FEE")]
        TradeFee,
        /// <summary>
        /// ["<c>WITHDRAW_FEE</c>"] Withdraw fee
        /// </summary>
        [Map("WITHDRAW_FEE")]
        WithdrawFee,
        /// <summary>
        /// ["<c>WITHDRAW</c>"] Withdraw
        /// </summary>
        [Map("WITHDRAW")]
        Withdraw,
        /// <summary>
        /// ["<c>DEPOSIT</c>"] Deposit
        /// </summary>
        [Map("DEPOSIT")]
        Deposit,
        /// <summary>
        /// ["<c>ROLLBACK_DEPOSIT</c>"] Rollback deposit
        /// </summary>
        [Map("ROLLBACK_DEPOSIT")]
        RollbackDeposit,
        /// <summary>
        /// ["<c>ROLLBACK_WITHDRAW</c>"] Rollback withdraw
        /// </summary>
        [Map("ROLLBACK_WITHDRAW")]
        RollbackWithdraw,
        /// <summary>
        /// ["<c>FUNDING</c>"] Funding
        /// </summary>
        [Map("FUNDING")]
        Funding,
        /// <summary>
        /// ["<c>REALIZED_PNL</c>"] Realized pnl
        /// </summary>
        [Map("REALIZED_PNL")]
        RealizedPnl,
        /// <summary>
        /// ["<c>INSURANCE_FUND</c>"] Insurance fund
        /// </summary>
        [Map("INSURANCE_FUND")]
        InsuranceFund,
        /// <summary>
        /// ["<c>SOCIALIZED_LOSS</c>"] Socialized loss
        /// </summary>
        [Map("SOCIALIZED_LOSS")]
        SocializedLoss,
        /// <summary>
        /// ["<c>LIQUIDATION_FEE</c>"] Liquidation fee
        /// </summary>
        [Map("LIQUIDATION_FEE")]
        LiquidationFee,
        /// <summary>
        /// ["<c>SESSION_RESET</c>"] Session reset
        /// </summary>
        [Map("SESSION_RESET")]
        SessionReset,
        /// <summary>
        /// ["<c>ADJUSTMENT</c>"] Adjustment
        /// </summary>
        [Map("ADJUSTMENT")]
        Adjustment,
        /// <summary>
        /// ["<c>SESSION_SETTLE</c>"] Session settle
        /// </summary>
        [Map("SESSION_SETTLE")]
        SessionSettle,
        /// <summary>
        /// ["<c>UNCOVERED_LOSS</c>"] Uncovered loss
        /// </summary>
        [Map("UNCOVERED_LOSS")]
        UncoveredLoss,
        /// <summary>
        /// ["<c>ADMIN_ADJUSTMENT</c>"] Admin adjustment
        /// </summary>
        [Map("ADMIN_ADJUSTMENT")]
        AdminAdjustment,
        /// <summary>
        /// ["<c>DELIST</c>"] Delist
        /// </summary>
        [Map("DELIST")]
        Delist,
        /// <summary>
        /// ["<c>SETTLEMENT_FEE</c>"] Settlement fee
        /// </summary>
        [Map("SETTLEMENT_FEE")]
        SettlementFee,
        /// <summary>
        /// ["<c>AUTO_CONVERSION</c>"] Auto conversion
        /// </summary>
        [Map("AUTO_CONVERSION")]
        AutoConversion,
        /// <summary>
        /// ["<c>MANUAL_CONVERSION</c>"] Manual conversion
        /// </summary>
        [Map("MANUAL_CONVERSION")]
        ManualConversion,
        /// <summary>
        /// ["<c>SUBACCOUNT_TX</c>"] Subaccount transaction
        /// </summary>
        [Map("SUBACCOUNT_TX")]
        SubaccountTx,
        /// <summary>
        /// ["<c>FIAT_WITHDRAWAL_CANCEL</c>"] Fiat withdrawal cancel
        /// </summary>
        [Map("FIAT_WITHDRAWAL_CANCEL")]
        FiatWithdrawalCancel,
        /// <summary>
        /// ["<c>SOFT_STAKE_REWARD</c>"] Soft stake reward
        /// </summary>
        [Map("SOFT_STAKE_REWARD")]
        SoftStakeReward
    }

}
