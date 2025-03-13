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
        /// Trading
        /// </summary>
        [Map("TRADING")]
        Trading,
        /// <summary>
        /// Trade fee
        /// </summary>
        [Map("TRADE_FEE")]
        TradeFee,
        /// <summary>
        /// Withdraw fee
        /// </summary>
        [Map("WITHDRAW_FEE")]
        WithdrawFee,
        /// <summary>
        /// Withdraw
        /// </summary>
        [Map("WITHDRAW")]
        Withdraw,
        /// <summary>
        /// Deposit
        /// </summary>
        [Map("DEPOSIT")]
        Deposit,
        /// <summary>
        /// Rollback deposit
        /// </summary>
        [Map("ROLLBACK_DEPOSIT")]
        RollbackDeposit,
        /// <summary>
        /// Rollback withdraw
        /// </summary>
        [Map("ROLLBACK_WITHDRAW")]
        RollbackWithdraw,
        /// <summary>
        /// Funding
        /// </summary>
        [Map("FUNDING")]
        Funding,
        /// <summary>
        /// Realized pnl
        /// </summary>
        [Map("REALIZED_PNL")]
        RealizedPnl,
        /// <summary>
        /// Insurance fund
        /// </summary>
        [Map("INSURANCE_FUND")]
        InsuranceFund,
        /// <summary>
        /// Socialized loss
        /// </summary>
        [Map("SOCIALIZED_LOSS")]
        SocializedLoss,
        /// <summary>
        /// Liquidation fee
        /// </summary>
        [Map("LIQUIDATION_FEE")]
        LiquidationFee,
        /// <summary>
        /// Session reset
        /// </summary>
        [Map("SESSION_RESET")]
        SessionReset,
        /// <summary>
        /// Adjustment
        /// </summary>
        [Map("ADJUSTMENT")]
        Adjustment,
        /// <summary>
        /// Session settle
        /// </summary>
        [Map("SESSION_SETTLE")]
        SessionSettle,
        /// <summary>
        /// Uncovered loss
        /// </summary>
        [Map("UNCOVERED_LOSS")]
        UncoveredLoss,
        /// <summary>
        /// Admin adjustment
        /// </summary>
        [Map("ADMIN_ADJUSTMENT")]
        AdminAdjustment,
        /// <summary>
        /// Delist
        /// </summary>
        [Map("DELIST")]
        Delist,
        /// <summary>
        /// Settlement fee
        /// </summary>
        [Map("SETTLEMENT_FEE")]
        SettlementFee,
        /// <summary>
        /// Auto conversion
        /// </summary>
        [Map("AUTO_CONVERSION")]
        AutoConversion,
        /// <summary>
        /// Manual conversion
        /// </summary>
        [Map("MANUAL_CONVERSION")]
        ManualConversion,
        /// <summary>
        /// Subaccount transaction
        /// </summary>
        [Map("SUBACCOUNT_TX")]
        SubaccountTx,
        /// <summary>
        /// Fiat withdrawal cancel
        /// </summary>
        [Map("FIAT_WITHDRAWAL_CANCEL")]
        FiatWithdrawalCancel,
    }

}
