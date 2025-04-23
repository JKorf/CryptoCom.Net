using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace CryptoCom.Net.Enums
{
    /// <summary>
    /// Enum representing Crypto.com order rejection reasons.
    /// </summary>
    public enum OrderRejectedReason : int
    {
        /// <summary>
        /// Success (no error or rejection)
        /// </summary>
        Success = 0,

        /// <summary>
        /// No position available.
        /// </summary>
        NoPosition = 201,

        /// <summary>
        /// Account is suspended.
        /// </summary>
        AccountIsSuspended = 202,

        /// <summary>
        /// Accounts do not match.
        /// </summary>
        AccountsDoNotMatch = 203,

        /// <summary>
        /// Duplicate client order ID.
        /// </summary>
        DuplicateClientOrderId = 204,

        /// <summary>
        /// Duplicate order ID.
        /// </summary>
        DuplicateOrderId = 205,

        /// <summary>
        /// Instrument has expired.
        /// </summary>
        InstrumentExpired = 206,

        /// <summary>
        /// No mark price available.
        /// </summary>
        NoMarkPrice = 207,

        /// <summary>
        /// Instrument is not tradable.
        /// </summary>
        InstrumentNotTradable = 208,

        /// <summary>
        /// Instrument is invalid.
        /// </summary>
        InvalidInstrument = 209,

        /// <summary>
        /// Account is invalid.
        /// </summary>
        InvalidAccount = 210,

        /// <summary>
        /// Currency is invalid.
        /// </summary>
        InvalidCurrency = 211,

        /// <summary>
        /// Invalid order ID.
        /// </summary>
        InvalidOrderId = 212,

        /// <summary>
        /// Invalid order quantity.
        /// </summary>
        InvalidOrderQuantity = 213,

        /// <summary>
        /// Invalid settlement currency.
        /// </summary>
        InvalidSettleCurrency = 214,

        /// <summary>
        /// Invalid fee currency.
        /// </summary>
        InvalidFeeCurrency = 215,

        /// <summary>
        /// Invalid position quantity.
        /// </summary>
        InvalidPositionQuantity = 216,

        /// <summary>
        /// Invalid open quantity.
        /// </summary>
        InvalidOpenQuantity = 217,

        /// <summary>
        /// Invalid order type.
        /// </summary>
        InvalidOrderType = 218,

        /// <summary>
        /// Invalid execution instruction.
        /// </summary>
        InvalidExecutionInstruction = 219,

        /// <summary>
        /// Invalid side.
        /// </summary>
        InvalidSide = 220,

        /// <summary>
        /// Invalid time in force.
        /// </summary>
        InvalidTimeInForce = 221,

        /// <summary>
        /// Stale mark price.
        /// </summary>
        StaleMarkPrice = 222,

        /// <summary>
        /// No client order ID provided.
        /// </summary>
        NoClientOrderId = 223,

        /// <summary>
        /// Rejected by matching engine.
        /// </summary>
        RejectedByMatchingEngine = 224,

        /// <summary>
        /// Exceeds maximum entry leverage.
        /// </summary>
        ExceedMaximumEntryLeverage = 225,

        /// <summary>
        /// Invalid leverage.
        /// </summary>
        InvalidLeverage = 226,

        /// <summary>
        /// Invalid slippage.
        /// </summary>
        InvalidSlippage = 227,

        /// <summary>
        /// Invalid floor price.
        /// </summary>
        InvalidFloorPrice = 228,

        /// <summary>
        /// Invalid reference price.
        /// </summary>
        InvalidReferencePrice = 229,

        /// <summary>
        /// Invalid trigger type.
        /// </summary>
        InvalidTriggerType = 230,

        /// <summary>
        /// Account is in margin call.
        /// </summary>
        AccountIsInMarginCall = 301,

        /// <summary>
        /// Exceeds account risk limit.
        /// </summary>
        ExceedsAccountRiskLimit = 302,

        /// <summary>
        /// Exceeds position risk limit.
        /// </summary>
        ExceedsPositionRiskLimit = 303,

        /// <summary>
        /// Order will lead to immediate liquidation.
        /// </summary>
        OrderWillLeadToImmediateLiquidation = 304,

        /// <summary>
        /// Order will trigger margin call.
        /// </summary>
        OrderWillTriggerMarginCall = 305,

        /// <summary>
        /// Insufficient available balance.
        /// </summary>
        InsufficientAvailableBalance = 306,

        /// <summary>
        /// Invalid order status.
        /// </summary>
        InvalidOrderStatus = 307,

        /// <summary>
        /// Invalid price.
        /// </summary>
        InvalidPrice = 308,

        /// <summary>
        /// Market is not open.
        /// </summary>
        MarketIsNotOpen = 309,

        /// <summary>
        /// Order price beyond liquidation price.
        /// </summary>
        OrderPriceBeyondLiquidationPrice = 310,

        /// <summary>
        /// Position is in liquidation.
        /// </summary>
        PositionIsInLiquidation = 311,

        /// <summary>
        /// Order price is greater than the limit up price.
        /// </summary>
        OrderPriceGreaterThanLimitUpPrice = 312,

        /// <summary>
        /// Order price is less than the limit down price.
        /// </summary>
        OrderPriceLessThanLimitDownPrice = 313,

        /// <summary>
        /// Exceeds maximum order size.
        /// </summary>
        ExceedsMaxOrderSize = 314,

        /// <summary>
        /// Far away limit price.
        /// </summary>
        FarAwayLimitPrice = 315,

        /// <summary>
        /// No active order.
        /// </summary>
        NoActiveOrder = 316,

        /// <summary>
        /// Position does not exist.
        /// </summary>
        PositionDoesNotExist = 317,

        /// <summary>
        /// Exceeds maximum allowed orders.
        /// </summary>
        ExceedsMaxAllowedOrders = 318,

        /// <summary>
        /// Exceeds maximum position size.
        /// </summary>
        ExceedsMaxPositionSize = 319,

        /// <summary>
        /// Exceeds initial margin.
        /// </summary>
        ExceedsInitialMargin = 320,

        /// <summary>
        /// Exceeds maximum available balance.
        /// </summary>
        ExceedsMaxAvailableBalance = 321,

        /// <summary>
        /// Account does not exist.
        /// </summary>
        AccountDoesNotExist = 401,

        /// <summary>
        /// Account is not active.
        /// </summary>
        AccountIsNotActive = 406,

        /// <summary>
        /// Margin unit does not exist.
        /// </summary>
        MarginUnitDoesNotExist = 407,

        /// <summary>
        /// Margin unit is suspended.
        /// </summary>
        MarginUnitIsSuspended = 408,

        /// <summary>
        /// Invalid user.
        /// </summary>
        InvalidUser = 409,

        /// <summary>
        /// User is not active.
        /// </summary>
        UserIsNotActive = 410,

        /// <summary>
        /// User does not have derivative access.
        /// </summary>
        UserNoDerivativeAccess = 411,

        /// <summary>
        /// Account does not have derivative access.
        /// </summary>
        AccountNoDerivativeAccess = 412,

        /// <summary>
        /// Below minimum order size.
        /// </summary>
        BelowMinimumOrderSize = 415,

        /// <summary>
        /// Exceeds maximum effective leverage.
        /// </summary>
        ExceedMaximumEffectiveLeverage = 501,

        /// <summary>
        /// Invalid collateral price.
        /// </summary>
        InvalidCollateralPrice = 604,

        /// <summary>
        /// Invalid margin calculation.
        /// </summary>
        InvalidMarginCalculation = 605,

        /// <summary>
        /// Exceeds allowed slippage.
        /// </summary>
        ExceedAllowedSlippage = 606,

        /// <summary>
        /// Maximum amount violated.
        /// </summary>
        MaxAmountViolated = 30024,

        /// <summary>
        /// Bad request.
        /// </summary>
        BadRequest = 40001,

        /// <summary>
        /// Method not found.
        /// </summary>
        MethodNotFound = 40002,

        /// <summary>
        /// Invalid request.
        /// </summary>
        InvalidRequest = 40003,

        /// <summary>
        /// Missing or invalid argument.
        /// </summary>
        MissingOrInvalidArgument = 40004,

        /// <summary>
        /// Invalid date.
        /// </summary>
        InvalidDate = 40005,

        /// <summary>
        /// Duplicate request received.
        /// </summary>
        DuplicateRequest = 40006,

        /// <summary>
        /// Unauthorized access.
        /// </summary>
        Unauthorized = 40101,

        /// <summary>
        /// Invalid nonce value.
        /// </summary>
        InvalidNonce = 40102,

        /// <summary>
        /// IP address not whitelisted.
        /// </summary>
        IpIllegal = 40103,

        /// <summary>
        /// User tier is invalid.
        /// </summary>
        UserTierInvalid = 40104,

        /// <summary>
        /// Exceeds maximum subscriptions.
        /// </summary>
        ExceedMaxSubscriptions = 40107,

        /// <summary>
        /// Not found.
        /// </summary>
        NotFound = 40401,

        /// <summary>
        /// Request has timed out.
        /// </summary>
        RequestTimeout = 40801,

        /// <summary>
        /// Too many requests.
        /// </summary>
        TooManyRequests = 42901,

        /// <summary>
        /// FOK order not filled and canceled.
        /// </summary>
        FillOrKill = 43003,

        /// <summary>
        /// IOC order not filled and canceled.
        /// </summary>
        ImmediateOrCancel = 43004,

        /// <summary>
        /// Rejected POST_ONLY create-order request.
        /// </summary>
        PostOnlyRejected = 43005,

        /// <summary>
        /// Canceled due to self-trade prevention.
        /// </summary>
        SelfTradePrevention = 43012,

        /// <summary>
        /// Credit line not maintained.
        /// </summary>
        CreditLineNotMaintained = 50001,

        /// <summary>
        /// Internal error occurred.
        /// </summary>
        InternalError = 50002
    }
}