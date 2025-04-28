using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace CryptoCom.Net.Enums
{
	/// <summary>
	/// Enum representing Crypto.com order rejection reasons.
	/// </summary>
	[JsonConverter(typeof(EnumConverter<OrderRejectedReason>))]
	public enum OrderRejectedReason
    {
        /// <summary>
        /// Success (no error or rejection)
        /// </summary>
        [Map("0")]
        Success,

        /// <summary>
        /// No position available.
        /// </summary>
        [Map("201")]
        NoPosition,

        /// <summary>
        /// Account is suspended.
        /// </summary>
        [Map("202")]
        AccountIsSuspended,

        /// <summary>
        /// Accounts do not match.
        /// </summary>
        [Map("203")]
        AccountsDoNotMatch,

        /// <summary>
        /// Duplicate client order ID.
        /// </summary>
        [Map("204")]
        DuplicateClientOrderId,

        /// <summary>
        /// Duplicate order ID.
        /// </summary>
        [Map("205")]
        DuplicateOrderId,

        /// <summary>
        /// Instrument has expired.
        /// </summary>
        [Map("206")]
        InstrumentExpired,

        /// <summary>
        /// No mark price available.
        /// </summary>
        [Map("207")]
        NoMarkPrice,

        /// <summary>
        /// Instrument is not tradable.
        /// </summary>
        [Map("208")]
        InstrumentNotTradable,

        /// <summary>
        /// Instrument is invalid.
        /// </summary>
        [Map("209")]
        InvalidInstrument,

        /// <summary>
        /// Account is invalid.
        /// </summary>
        [Map("210")]
        InvalidAccount,

        /// <summary>
        /// Currency is invalid.
        /// </summary>
        [Map("211")]
        InvalidCurrency,

        /// <summary>
        /// Invalid order ID.
        /// </summary>
        [Map("212")]
        InvalidOrderId,

        /// <summary>
        /// Invalid order quantity.
        /// </summary>
        [Map("213")]
        InvalidOrderQuantity,

        /// <summary>
        /// Invalid settlement currency.
        /// </summary>
        [Map("214")]
        InvalidSettleCurrency,

        /// <summary>
        /// Invalid fee currency.
        /// </summary>
        [Map("215")]
        InvalidFeeCurrency,

        /// <summary>
        /// Invalid position quantity.
        /// </summary>
        [Map("216")]
        InvalidPositionQuantity,

        /// <summary>
        /// Invalid open quantity.
        /// </summary>
        [Map("217")]
        InvalidOpenQuantity,

        /// <summary>
        /// Invalid order type.
        /// </summary>
        [Map("218")]
        InvalidOrderType,

        /// <summary>
        /// Invalid execution instruction.
        /// </summary>
        [Map("219")]
        InvalidExecutionInstruction,

        /// <summary>
        /// Invalid side.
        /// </summary>
        [Map("220")]
        InvalidSide,

        /// <summary>
        /// Invalid time in force.
        /// </summary>
        [Map("221")]
        InvalidTimeInForce,

        /// <summary>
        /// Stale mark price.
        /// </summary>
        [Map("222")]
        StaleMarkPrice,

        /// <summary>
        /// No client order ID provided.
        /// </summary>
        [Map("223")]
        NoClientOrderId,

        /// <summary>
        /// Rejected by matching engine.
        /// </summary>
        [Map("224")]
        RejectedByMatchingEngine,

        /// <summary>
        /// Exceeds maximum entry leverage.
        /// </summary>
        [Map("225")]
        ExceedMaximumEntryLeverage,

        /// <summary>
        /// Invalid leverage.
        /// </summary>
        [Map("226")]
        InvalidLeverage,

        /// <summary>
        /// Invalid slippage.
        /// </summary>
        [Map("227")]
        InvalidSlippage,

        /// <summary>
        /// Invalid floor price.
        /// </summary>
        [Map("228")]
        InvalidFloorPrice,

        /// <summary>
        /// Invalid reference price.
        /// </summary>
        [Map("229")]
        InvalidReferencePrice,

        /// <summary>
        /// Invalid trigger type.
        /// </summary>
        [Map("230")]
        InvalidTriggerType,

        /// <summary>
        /// Account is in margin call.
        /// </summary>
        [Map("301")]
        AccountIsInMarginCall,

        /// <summary>
        /// Exceeds account risk limit.
        /// </summary>
        [Map("302")]
        ExceedsAccountRiskLimit,

        /// <summary>
        /// Exceeds position risk limit.
        /// </summary>
        [Map("303")]
        ExceedsPositionRiskLimit,

        /// <summary>
        /// Order will lead to immediate liquidation.
        /// </summary>
        [Map("304")]
        OrderWillLeadToImmediateLiquidation,

        /// <summary>
        /// Order will trigger margin call.
        /// </summary>
        [Map("305")]
        OrderWillTriggerMarginCall,

        /// <summary>
        /// Insufficient available balance.
        /// </summary>
        [Map("306")]
        InsufficientAvailableBalance,

        /// <summary>
        /// Invalid order status.
        /// </summary>
        [Map("307")]
        InvalidOrderStatus,

        /// <summary>
        /// Invalid price.
        /// </summary>
        [Map("308")]
        InvalidPrice,

        /// <summary>
        /// Market is not open.
        /// </summary>
        [Map("309")]
        MarketIsNotOpen,

        /// <summary>
        /// Order price beyond liquidation price.
        /// </summary>
        [Map("310")]
        OrderPriceBeyondLiquidationPrice,

        /// <summary>
        /// Position is in liquidation.
        /// </summary>
        [Map("311")]
        PositionIsInLiquidation,

        /// <summary>
        /// Order price is greater than the limit up price.
        /// </summary>
        [Map("312")]
        OrderPriceGreaterThanLimitUpPrice,

        /// <summary>
        /// Order price is less than the limit down price.
        /// </summary>
        [Map("313")]
        OrderPriceLessThanLimitDownPrice,

        /// <summary>
        /// Exceeds maximum order size.
        /// </summary>
        [Map("314")]
        ExceedsMaxOrderSize,

        /// <summary>
        /// Far away limit price.
        /// </summary>
        [Map("315")]
        FarAwayLimitPrice,

        /// <summary>
        /// No active order.
        /// </summary>
        [Map("316")]
        NoActiveOrder,

        /// <summary>
        /// Position does not exist.
        /// </summary>
        [Map("317")]
        PositionDoesNotExist,

        /// <summary>
        /// Exceeds maximum allowed orders.
        /// </summary>
        [Map("318")]
        ExceedsMaxAllowedOrders,

        /// <summary>
        /// Exceeds maximum position size.
        /// </summary>
        [Map("319")]
        ExceedsMaxPositionSize,

        /// <summary>
        /// Exceeds initial margin.
        /// </summary>
        [Map("320")]
        ExceedsInitialMargin,

        /// <summary>
        /// Exceeds maximum available balance.
        /// </summary>
        [Map("321")]
        ExceedsMaxAvailableBalance,

        /// <summary>
        /// Account does not exist.
        /// </summary>
        [Map("401")]
        AccountDoesNotExist,

        /// <summary>
        /// Account is not active.
        /// </summary>
        [Map("406")]
        AccountIsNotActive,

        /// <summary>
        /// Margin unit does not exist.
        /// </summary>
        [Map("407")]
        MarginUnitDoesNotExist,

        /// <summary>
        /// Margin unit is suspended.
        /// </summary>
        [Map("408")]
        MarginUnitIsSuspended,

        /// <summary>
        /// Invalid user.
        /// </summary>
        [Map("409")]
        InvalidUser,

        /// <summary>
        /// User is not active.
        /// </summary>
        [Map("410")]
        UserIsNotActive,

        /// <summary>
        /// User does not have derivative access.
        /// </summary>
        [Map("411")]
        UserNoDerivativeAccess,

        /// <summary>
        /// Account does not have derivative access.
        /// </summary>
        [Map("412")]
        AccountNoDerivativeAccess,

        /// <summary>
        /// Below minimum order size.
        /// </summary>
        [Map("415")]
        BelowMinimumOrderSize,

        /// <summary>
        /// Exceeds maximum effective leverage.
        /// </summary>
        [Map("501")]
        ExceedMaximumEffectiveLeverage,

        /// <summary>
        /// Invalid collateral price.
        /// </summary>
        [Map("604")]
        InvalidCollateralPrice,

        /// <summary>
        /// Invalid margin calculation.
        /// </summary>
        [Map("605")]
        InvalidMarginCalculation,

        /// <summary>
        /// Exceeds allowed slippage.
        /// </summary>
        [Map("606")]
        ExceedAllowedSlippage,

        /// <summary>
        /// Maximum amount violated.
        /// </summary>
        [Map("30024")]
        MaxAmountViolated,

        /// <summary>
        /// Bad request.
        /// </summary>
        [Map("40001")]
        BadRequest,

        /// <summary>
        /// Method not found.
        /// </summary>
        [Map("40002")]
        MethodNotFound,

        /// <summary>
        /// Invalid request.
        /// </summary>
        [Map("40003")]
        InvalidRequest,

        /// <summary>
        /// Missing or invalid argument.
        /// </summary>
        [Map("40004")]
        MissingOrInvalidArgument,

        /// <summary>
        /// Invalid date.
        /// </summary>
        [Map("40005")]
        InvalidDate,

        /// <summary>
        /// Duplicate request received.
        /// </summary>
        [Map("40006")]
        DuplicateRequest,

        /// <summary>
        /// Unauthorized access.
        /// </summary>
        [Map("40101")]
        Unauthorized,

        /// <summary>
        /// Invalid nonce value.
        /// </summary>
        [Map("40102")]
        InvalidNonce,

        /// <summary>
        /// IP address not whitelisted.
        /// </summary>
        [Map("40103")]
        IpIllegal,

        /// <summary>
        /// User tier is invalid.
        /// </summary>
        [Map("40104")]
        UserTierInvalid,

        /// <summary>
        /// Exceeds maximum subscriptions.
        /// </summary>
        [Map("40107")]
        ExceedMaxSubscriptions,

        /// <summary>
        /// Not found.
        /// </summary>
        [Map("40401")]
        NotFound,

        /// <summary>
        /// Request has timed out.
        /// </summary>
        [Map("40801")]
        RequestTimeout,

        /// <summary>
        /// Too many requests.
        /// </summary>
        [Map("42901")]
        TooManyRequests,

        /// <summary>
        /// FOK order not filled and canceled.
        /// </summary>
        [Map("43003")]
        FillOrKill,

        /// <summary>
        /// IOC order not filled and canceled.
        /// </summary>
        [Map("43004")]
        ImmediateOrCancel,

        /// <summary>
        /// Rejected POST_ONLY create-order request.
        /// </summary>
        [Map("43005")]
        PostOnlyRejected,

        /// <summary>
        /// Canceled due to self-trade prevention.
        /// </summary>
        [Map("43012")]
        SelfTradePrevention,

        /// <summary>
        /// Credit line not maintained.
        /// </summary>
        [Map("50001")]
        CreditLineNotMaintained,

        /// <summary>
        /// Internal error occurred.
        /// </summary>
        [Map("50002")]
        InternalError,
    }
}