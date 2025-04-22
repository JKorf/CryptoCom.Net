using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace CryptoCom.Net.Enums
{
    /// <summary>
    /// Enum representing Crypto.com order rejection reasons.
    /// </summary>
    [JsonConverter(typeof(EnumConverter<OrderRejectedReason>))]
    public enum OrderRejectedReason : int
    {
        /// <summary>
        /// No position available.
        /// </summary>
        [Map("NO_POSITION")]
        NoPosition = 201,

        /// <summary>
        /// Account is suspended.
        /// </summary>
        [Map("ACCOUNT_IS_SUSPENDED")]
        AccountIsSuspended = 202,

        /// <summary>
        /// Accounts do not match.
        /// </summary>
        [Map("ACCOUNTS_DO_NOT_MATCH")]
        AccountsDoNotMatch = 203,

        /// <summary>
        /// Duplicate client order ID.
        /// </summary>
        [Map("DUPLICATE_CLORDID")]
        DuplicateClientOrderId = 204,

        /// <summary>
        /// Duplicate order ID.
        /// </summary>
        [Map("DUPLICATE_ORDERID")]
        DuplicateOrderId = 205,

        /// <summary>
        /// Instrument has expired.
        /// </summary>
        [Map("INSTRUMENT_EXPIRED")]
        InstrumentExpired = 206,

        /// <summary>
        /// No mark price available.
        /// </summary>
        [Map("NO_MARK_PRICE")]
        NoMarkPrice = 207,

        /// <summary>
        /// Instrument is not tradable.
        /// </summary>
        [Map("INSTRUMENT_NOT_TRADABLE")]
        InstrumentNotTradable = 208,

        /// <summary>
        /// Instrument is invalid.
        /// </summary>
        [Map("INVALID_INSTRUMENT")]
        InvalidInstrument = 209,

        /// <summary>
        /// Account is invalid.
        /// </summary>
        [Map("INVALID_ACCOUNT")]
        InvalidAccount = 210,

        /// <summary>
        /// Currency is invalid.
        /// </summary>
        [Map("INVALID_CURRENCY")]
        InvalidCurrency = 211,

        /// <summary>
        /// Invalid order ID.
        /// </summary>
        [Map("INVALID_ORDERID")]
        InvalidOrderId = 212,

        /// <summary>
        /// Invalid order quantity.
        /// </summary>
        [Map("INVALID_ORDERQTY")]
        InvalidOrderQuantity = 213,

        /// <summary>
        /// Invalid settlement currency.
        /// </summary>
        [Map("INVALID_SETTLE_CURRENCY")]
        InvalidSettleCurrency = 214,

        /// <summary>
        /// Invalid fee currency.
        /// </summary>
        [Map("INVALID_FEE_CURRENCY")]
        InvalidFeeCurrency = 215,

        /// <summary>
        /// Invalid position quantity.
        /// </summary>
        [Map("INVALID_POSITION_QTY")]
        InvalidPositionQuantity = 216,

        /// <summary>
        /// Invalid open quantity.
        /// </summary>
        [Map("INVALID_OPEN_QTY")]
        InvalidOpenQuantity = 217,

        /// <summary>
        /// Invalid order type.
        /// </summary>
        [Map("INVALID_ORDTYPE")]
        InvalidOrderType = 218,

        /// <summary>
        /// Invalid execution instruction.
        /// </summary>
        [Map("INVALID_EXECINST")]
        InvalidExecutionInstruction = 219,

        /// <summary>
        /// Invalid side.
        /// </summary>
        [Map("INVALID_SIDE")]
        InvalidSide = 220,

        /// <summary>
        /// Invalid time in force.
        /// </summary>
        [Map("INVALID_TIF")]
        InvalidTimeInForce = 221,

        /// <summary>
        /// Stale mark price.
        /// </summary>
        [Map("STALE_MARK_PRICE")]
        StaleMarkPrice = 222,

        /// <summary>
        /// No client order ID provided.
        /// </summary>
        [Map("NO_CLORDID")]
        NoClientOrderId = 223,

        /// <summary>
        /// Rejected by matching engine.
        /// </summary>
        [Map("REJ_BY_MATCHING_ENGINE")]
        RejectedByMatchingEngine = 224,

        /// <summary>
        /// Exceeds maximum entry leverage.
        /// </summary>
        [Map("EXCEED_MAXIMUM_ENTRY_LEVERAGE")]
        ExceedMaximumEntryLeverage = 225,

        /// <summary>
        /// Invalid leverage.
        /// </summary>
        [Map("INVALID_LEVERAGE")]
        InvalidLeverage = 226,

        /// <summary>
        /// Invalid slippage.
        /// </summary>
        [Map("INVALID_SLIPPAGE")]
        InvalidSlippage = 227,

        /// <summary>
        /// Invalid floor price.
        /// </summary>
        [Map("INVALID_FLOOR_PRICE")]
        InvalidFloorPrice = 228,

        /// <summary>
        /// Invalid reference price.
        /// </summary>
        [Map("INVALID_REF_PRICE")]
        InvalidReferencePrice = 229,

        /// <summary>
        /// Invalid trigger type.
        /// </summary>
        [Map("INVALID_TRIGGER_TYPE")]
        InvalidTriggerType = 230,

        /// <summary>
        /// Account is in margin call.
        /// </summary>
        [Map("ACCOUNT_IS_IN_MARGIN_CALL")]
        AccountIsInMarginCall = 301,

        /// <summary>
        /// Exceeds account risk limit.
        /// </summary>
        [Map("EXCEEDS_ACCOUNT_RISK_LIMIT")]
        ExceedsAccountRiskLimit = 302,

        /// <summary>
        /// Exceeds position risk limit.
        /// </summary>
        [Map("EXCEEDS_POSITION_RISK_LIMIT")]
        ExceedsPositionRiskLimit = 303,

        /// <summary>
        /// Order will lead to immediate liquidation.
        /// </summary>
        [Map("ORDER_WILL_LEAD_TO_IMMEDIATE_LIQUIDATION")]
        OrderWillLeadToImmediateLiquidation = 304,

        /// <summary>
        /// Order will trigger margin call.
        /// </summary>
        [Map("ORDER_WILL_TRIGGER_MARGIN_CALL")]
        OrderWillTriggerMarginCall = 305,

        /// <summary>
        /// Insufficient available balance.
        /// </summary>
        [Map("INSUFFICIENT_AVAILABLE_BALANCE")]
        InsufficientAvailableBalance = 306,

        /// <summary>
        /// Invalid order status.
        /// </summary>
        [Map("INVALID_ORDSTATUS")]
        InvalidOrderStatus = 307,

        /// <summary>
        /// Invalid price.
        /// </summary>
        [Map("INVALID_PRICE")]
        InvalidPrice = 308,

        /// <summary>
        /// Market is not open.
        /// </summary>
        [Map("MARKET_IS_NOT_OPEN")]
        MarketIsNotOpen = 309,

        /// <summary>
        /// Order price beyond liquidation price.
        /// </summary>
        [Map("ORDER_PRICE_BEYOND_LIQUIDATION_PRICE")]
        OrderPriceBeyondLiquidationPrice = 310,

        /// <summary>
        /// Position is in liquidation.
        /// </summary>
        [Map("POSITION_IS_IN_LIQUIDATION")]
        PositionIsInLiquidation = 311,

        /// <summary>
        /// Order price is greater than the limit up price.
        /// </summary>
        [Map("ORDER_PRICE_GREATER_THAN_LIMITUPPRICE")]
        OrderPriceGreaterThanLimitUpPrice = 312,

        /// <summary>
        /// Order price is less than the limit down price.
        /// </summary>
        [Map("ORDER_PRICE_LESS_THAN_LIMITDOWNPRICE")]
        OrderPriceLessThanLimitDownPrice = 313,

        /// <summary>
        /// Exceeds maximum order size.
        /// </summary>
        [Map("EXCEEDS_MAX_ORDER_SIZE")]
        ExceedsMaxOrderSize = 314,

        /// <summary>
        /// Far away limit price.
        /// </summary>
        [Map("FAR_AWAY_LIMIT_PRICE")]
        FarAwayLimitPrice = 315,

        /// <summary>
        /// No active order.
        /// </summary>
        [Map("NO_ACTIVE_ORDER")]
        NoActiveOrder = 316,

        /// <summary>
        /// Position does not exist.
        /// </summary>
        [Map("POSITION_NO_EXIST")]
        PositionDoesNotExist = 317,

        /// <summary>
        /// Exceeds maximum allowed orders.
        /// </summary>
        [Map("EXCEEDS_MAX_ALLOWED_ORDERS")]
        ExceedsMaxAllowedOrders = 318,

        /// <summary>
        /// Exceeds maximum position size.
        /// </summary>
        [Map("EXCEEDS_MAX_POSITION_SIZE")]
        ExceedsMaxPositionSize = 319,

        /// <summary>
        /// Exceeds initial margin.
        /// </summary>
        [Map("EXCEEDS_INITIAL_MARGIN")]
        ExceedsInitialMargin = 320,

        /// <summary>
        /// Exceeds maximum available balance.
        /// </summary>
        [Map("EXCEEDS_MAX_AVAILABLE_BALANCE")]
        ExceedsMaxAvailableBalance = 321,

        /// <summary>
        /// Account does not exist.
        /// </summary>
        [Map("ACCOUNT_DOES_NOT_EXIST")]
        AccountDoesNotExist = 401,

        /// <summary>
        /// Account is not active.
        /// </summary>
        [Map("ACCOUNT_IS_NOT_ACTIVE")]
        AccountIsNotActive = 406,

        /// <summary>
        /// Margin unit does not exist.
        /// </summary>
        [Map("MARGIN_UNIT_DOES_NOT_EXIST")]
        MarginUnitDoesNotExist = 407,

        /// <summary>
        /// Margin unit is suspended.
        /// </summary>
        [Map("MARGIN_UNIT_IS_SUSPENDED")]
        MarginUnitIsSuspended = 408,

        /// <summary>
        /// Invalid user.
        /// </summary>
        [Map("INVALID_USER")]
        InvalidUser = 409,

        /// <summary>
        /// User is not active.
        /// </summary>
        [Map("USER_IS_NOT_ACTIVE")]
        UserIsNotActive = 410,

        /// <summary>
        /// User does not have derivative access.
        /// </summary>
        [Map("USER_NO_DERIV_ACCESS")]
        UserNoDerivativeAccess = 411,

        /// <summary>
        /// Account does not have derivative access.
        /// </summary>
        [Map("ACCOUNT_NO_DERIV_ACCESS")]
        AccountNoDerivativeAccess = 412,

        /// <summary>
        /// Below minimum order size.
        /// </summary>
        [Map("BELOW_MIN_ORDER_SIZE")]
        BelowMinimumOrderSize = 415,

        /// <summary>
        /// Exceeds maximum effective leverage.
        /// </summary>
        [Map("EXCEED_MAXIMUM_EFFECTIVE_LEVERAGE")]
        ExceedMaximumEffectiveLeverage = 501,

        /// <summary>
        /// Invalid collateral price.
        /// </summary>
        [Map("INVALID_COLLATERAL_PRICE")]
        InvalidCollateralPrice = 604,

        /// <summary>
        /// Invalid margin calculation.
        /// </summary>
        [Map("INVALID_MARGIN_CALC")]
        InvalidMarginCalculation = 605,

        /// <summary>
        /// Exceeds allowed slippage.
        /// </summary>
        [Map("EXCEED_ALLOWED_SLIPPAGE")]
        ExceedAllowedSlippage = 606,

        /// <summary>
        /// Maximum amount violated.
        /// </summary>
        [Map("MAX_AMOUNT_VIOLATED")]
        MaxAmountViolated = 30024,

        /// <summary>
        /// Bad request.
        /// </summary>
        [Map("BAD_REQUEST")]
        BadRequest = 40001,

        /// <summary>
        /// Method not found.
        /// </summary>
        [Map("METHOD_NOT_FOUND")]
        MethodNotFound = 40002,

        /// <summary>
        /// Invalid request.
        /// </summary>
        [Map("INVALID_REQUEST")]
        InvalidRequest = 40003,

        /// <summary>
        /// Missing or invalid argument.
        /// </summary>
        [Map("MISSING_OR_INVALID_ARGUMENT")]
        MissingOrInvalidArgument = 40004,

        /// <summary>
        /// Invalid date.
        /// </summary>
        [Map("INVALID_DATE")]
        InvalidDate = 40005,

        /// <summary>
        /// Duplicate request received.
        /// </summary>
        [Map("DUPLICATE_REQUEST")]
        DuplicateRequest = 40006,

        /// <summary>
        /// Unauthorized access.
        /// </summary>
        [Map("UNAUTHORIZED")]
        Unauthorized = 40101,

        /// <summary>
        /// Invalid nonce value.
        /// </summary>
        [Map("INVALID_NONCE")]
        InvalidNonce = 40102,

        /// <summary>
        /// IP address not whitelisted.
        /// </summary>
        [Map("IP_ILLEGAL")]
        IpIllegal = 40103,

        /// <summary>
        /// User tier is invalid.
        /// </summary>
        [Map("USER_TIER_INVALID")]
        UserTierInvalid = 40104,

        /// <summary>
        /// Exceeds maximum subscriptions.
        /// </summary>
        [Map("EXCEED_MAX_SUBSCRIPTIONS")]
        ExceedMaxSubscriptions = 40107,

        /// <summary>
        /// Not found.
        /// </summary>
        [Map("NOT_FOUND")]
        NotFound = 40401,

        /// <summary>
        /// Request has timed out.
        /// </summary>
        [Map("REQUEST_TIMEOUT")]
        RequestTimeout = 40801,

        /// <summary>
        /// Too many requests.
        /// </summary>
        [Map("TOO_MANY_REQUESTS")]
        TooManyRequests = 42901,

        /// <summary>
        /// FOK order not filled and canceled.
        /// </summary>
        [Map("FILL_OR_KILL")]
        FillOrKill = 43003,

        /// <summary>
        /// IOC order not filled and canceled.
        /// </summary>
        [Map("IMMEDIATE_OR_CANCEL")]
        ImmediateOrCancel = 43004,

        /// <summary>
        /// Rejected POST_ONLY create-order request.
        /// </summary>
        [Map("POST_ONLY_REJ")]
        PostOnlyRejected = 43005,

        /// <summary>
        /// Canceled due to self-trade prevention.
        /// </summary>
        [Map("SELF_TRADE_PREVENTION")]
        SelfTradePrevention = 43012,

        /// <summary>
        /// Credit line not maintained.
        /// </summary>
        [Map("DW_CREDIT_LINE_NOT_MAINTAINED")]
        CreditLineNotMaintained = 50001,

        /// <summary>
        /// Internal error occurred.
        /// </summary>
        [Map("ERR_INTERNAL")]
        InternalError = 50002
    }
}