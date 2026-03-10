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
        /// ["<c>0</c>"] Success (no error or rejection)
        /// </summary>
        [Map("0")]
        Success,

        /// <summary>
        /// ["<c>201</c>"] No position available.
        /// </summary>
        [Map("201")]
        NoPosition,

        /// <summary>
        /// ["<c>202</c>"] Account is suspended.
        /// </summary>
        [Map("202")]
        AccountIsSuspended,

        /// <summary>
        /// ["<c>203</c>"] Accounts do not match.
        /// </summary>
        [Map("203")]
        AccountsDoNotMatch,

        /// <summary>
        /// ["<c>204</c>"] Duplicate client order ID.
        /// </summary>
        [Map("204")]
        DuplicateClientOrderId,

        /// <summary>
        /// ["<c>205</c>"] Duplicate order ID.
        /// </summary>
        [Map("205")]
        DuplicateOrderId,

        /// <summary>
        /// ["<c>206</c>"] Instrument has expired.
        /// </summary>
        [Map("206")]
        InstrumentExpired,

        /// <summary>
        /// ["<c>207</c>"] No mark price available.
        /// </summary>
        [Map("207")]
        NoMarkPrice,

        /// <summary>
        /// ["<c>208</c>"] Instrument is not tradable.
        /// </summary>
        [Map("208")]
        InstrumentNotTradable,

        /// <summary>
        /// ["<c>209</c>"] Instrument is invalid.
        /// </summary>
        [Map("209")]
        InvalidInstrument,

        /// <summary>
        /// ["<c>210</c>"] Account is invalid.
        /// </summary>
        [Map("210")]
        InvalidAccount,

        /// <summary>
        /// ["<c>211</c>"] Currency is invalid.
        /// </summary>
        [Map("211")]
        InvalidCurrency,

        /// <summary>
        /// ["<c>212</c>"] Invalid order ID.
        /// </summary>
        [Map("212")]
        InvalidOrderId,

        /// <summary>
        /// ["<c>213</c>"] Invalid order quantity.
        /// </summary>
        [Map("213")]
        InvalidOrderQuantity,

        /// <summary>
        /// ["<c>214</c>"] Invalid settlement currency.
        /// </summary>
        [Map("214")]
        InvalidSettleCurrency,

        /// <summary>
        /// ["<c>215</c>"] Invalid fee currency.
        /// </summary>
        [Map("215")]
        InvalidFeeCurrency,

        /// <summary>
        /// ["<c>216</c>"] Invalid position quantity.
        /// </summary>
        [Map("216")]
        InvalidPositionQuantity,

        /// <summary>
        /// ["<c>217</c>"] Invalid open quantity.
        /// </summary>
        [Map("217")]
        InvalidOpenQuantity,

        /// <summary>
        /// ["<c>218</c>"] Invalid order type.
        /// </summary>
        [Map("218")]
        InvalidOrderType,

        /// <summary>
        /// ["<c>219</c>"] Invalid execution instruction.
        /// </summary>
        [Map("219")]
        InvalidExecutionInstruction,

        /// <summary>
        /// ["<c>220</c>"] Invalid side.
        /// </summary>
        [Map("220")]
        InvalidSide,

        /// <summary>
        /// ["<c>221</c>"] Invalid time in force.
        /// </summary>
        [Map("221")]
        InvalidTimeInForce,

        /// <summary>
        /// ["<c>222</c>"] Stale mark price.
        /// </summary>
        [Map("222")]
        StaleMarkPrice,

        /// <summary>
        /// ["<c>223</c>"] No client order ID provided.
        /// </summary>
        [Map("223")]
        NoClientOrderId,

        /// <summary>
        /// ["<c>224</c>"] Rejected by matching engine.
        /// </summary>
        [Map("224")]
        RejectedByMatchingEngine,

        /// <summary>
        /// ["<c>225</c>"] Exceeds maximum entry leverage.
        /// </summary>
        [Map("225")]
        ExceedMaximumEntryLeverage,

        /// <summary>
        /// ["<c>226</c>"] Invalid leverage.
        /// </summary>
        [Map("226")]
        InvalidLeverage,

        /// <summary>
        /// ["<c>227</c>"] Invalid slippage.
        /// </summary>
        [Map("227")]
        InvalidSlippage,

        /// <summary>
        /// ["<c>228</c>"] Invalid floor price.
        /// </summary>
        [Map("228")]
        InvalidFloorPrice,

        /// <summary>
        /// ["<c>229</c>"] Invalid reference price.
        /// </summary>
        [Map("229")]
        InvalidReferencePrice,

        /// <summary>
        /// ["<c>230</c>"] Invalid trigger type.
        /// </summary>
        [Map("230")]
        InvalidTriggerType,

        /// <summary>
        /// ["<c>301</c>"] Account is in margin call.
        /// </summary>
        [Map("301")]
        AccountIsInMarginCall,

        /// <summary>
        /// ["<c>302</c>"] Exceeds account risk limit.
        /// </summary>
        [Map("302")]
        ExceedsAccountRiskLimit,

        /// <summary>
        /// ["<c>303</c>"] Exceeds position risk limit.
        /// </summary>
        [Map("303")]
        ExceedsPositionRiskLimit,

        /// <summary>
        /// ["<c>304</c>"] Order will lead to immediate liquidation.
        /// </summary>
        [Map("304")]
        OrderWillLeadToImmediateLiquidation,

        /// <summary>
        /// ["<c>305</c>"] Order will trigger margin call.
        /// </summary>
        [Map("305")]
        OrderWillTriggerMarginCall,

        /// <summary>
        /// ["<c>306</c>"] Insufficient available balance.
        /// </summary>
        [Map("306")]
        InsufficientAvailableBalance,

        /// <summary>
        /// ["<c>307</c>"] Invalid order status.
        /// </summary>
        [Map("307")]
        InvalidOrderStatus,

        /// <summary>
        /// ["<c>308</c>"] Invalid price.
        /// </summary>
        [Map("308")]
        InvalidPrice,

        /// <summary>
        /// ["<c>309</c>"] Market is not open.
        /// </summary>
        [Map("309")]
        MarketIsNotOpen,

        /// <summary>
        /// ["<c>310</c>"] Order price beyond liquidation price.
        /// </summary>
        [Map("310")]
        OrderPriceBeyondLiquidationPrice,

        /// <summary>
        /// ["<c>311</c>"] Position is in liquidation.
        /// </summary>
        [Map("311")]
        PositionIsInLiquidation,

        /// <summary>
        /// ["<c>312</c>"] Order price is greater than the limit up price.
        /// </summary>
        [Map("312")]
        OrderPriceGreaterThanLimitUpPrice,

        /// <summary>
        /// ["<c>313</c>"] Order price is less than the limit down price.
        /// </summary>
        [Map("313")]
        OrderPriceLessThanLimitDownPrice,

        /// <summary>
        /// ["<c>314</c>"] Exceeds maximum order size.
        /// </summary>
        [Map("314")]
        ExceedsMaxOrderSize,

        /// <summary>
        /// ["<c>315</c>"] Far away limit price.
        /// </summary>
        [Map("315")]
        FarAwayLimitPrice,

        /// <summary>
        /// ["<c>316</c>"] No active order.
        /// </summary>
        [Map("316")]
        NoActiveOrder,

        /// <summary>
        /// ["<c>317</c>"] Position does not exist.
        /// </summary>
        [Map("317")]
        PositionDoesNotExist,

        /// <summary>
        /// ["<c>318</c>"] Exceeds maximum allowed orders.
        /// </summary>
        [Map("318")]
        ExceedsMaxAllowedOrders,

        /// <summary>
        /// ["<c>319</c>"] Exceeds maximum position size.
        /// </summary>
        [Map("319")]
        ExceedsMaxPositionSize,

        /// <summary>
        /// ["<c>320</c>"] Exceeds initial margin.
        /// </summary>
        [Map("320")]
        ExceedsInitialMargin,

        /// <summary>
        /// ["<c>321</c>"] Exceeds maximum available balance.
        /// </summary>
        [Map("321")]
        ExceedsMaxAvailableBalance,

        /// <summary>
        /// ["<c>401</c>"] Account does not exist.
        /// </summary>
        [Map("401")]
        AccountDoesNotExist,

        /// <summary>
        /// ["<c>406</c>"] Account is not active.
        /// </summary>
        [Map("406")]
        AccountIsNotActive,

        /// <summary>
        /// ["<c>407</c>"] Margin unit does not exist.
        /// </summary>
        [Map("407")]
        MarginUnitDoesNotExist,

        /// <summary>
        /// ["<c>408</c>"] Margin unit is suspended.
        /// </summary>
        [Map("408")]
        MarginUnitIsSuspended,

        /// <summary>
        /// ["<c>409</c>"] Invalid user.
        /// </summary>
        [Map("409")]
        InvalidUser,

        /// <summary>
        /// ["<c>410</c>"] User is not active.
        /// </summary>
        [Map("410")]
        UserIsNotActive,

        /// <summary>
        /// ["<c>411</c>"] User does not have derivative access.
        /// </summary>
        [Map("411")]
        UserNoDerivativeAccess,

        /// <summary>
        /// ["<c>412</c>"] Account does not have derivative access.
        /// </summary>
        [Map("412")]
        AccountNoDerivativeAccess,

        /// <summary>
        /// ["<c>415</c>"] Below minimum order size.
        /// </summary>
        [Map("415")]
        BelowMinimumOrderSize,

        /// <summary>
        /// ["<c>501</c>"] Exceeds maximum effective leverage.
        /// </summary>
        [Map("501")]
        ExceedMaximumEffectiveLeverage,

        /// <summary>
        /// ["<c>604</c>"] Invalid collateral price.
        /// </summary>
        [Map("604")]
        InvalidCollateralPrice,

        /// <summary>
        /// ["<c>605</c>"] Invalid margin calculation.
        /// </summary>
        [Map("605")]
        InvalidMarginCalculation,

        /// <summary>
        /// ["<c>606</c>"] Exceeds allowed slippage.
        /// </summary>
        [Map("606")]
        ExceedAllowedSlippage,

        /// <summary>
        /// ["<c>30024</c>"] Maximum amount violated.
        /// </summary>
        [Map("30024")]
        MaxAmountViolated,

        /// <summary>
        /// ["<c>40001</c>"] Bad request.
        /// </summary>
        [Map("40001")]
        BadRequest,

        /// <summary>
        /// ["<c>40002</c>"] Method not found.
        /// </summary>
        [Map("40002")]
        MethodNotFound,

        /// <summary>
        /// ["<c>40003</c>"] Invalid request.
        /// </summary>
        [Map("40003")]
        InvalidRequest,

        /// <summary>
        /// ["<c>40004</c>"] Missing or invalid argument.
        /// </summary>
        [Map("40004")]
        MissingOrInvalidArgument,

        /// <summary>
        /// ["<c>40005</c>"] Invalid date.
        /// </summary>
        [Map("40005")]
        InvalidDate,

        /// <summary>
        /// ["<c>40006</c>"] Duplicate request received.
        /// </summary>
        [Map("40006")]
        DuplicateRequest,

        /// <summary>
        /// ["<c>40101</c>"] Unauthorized access.
        /// </summary>
        [Map("40101")]
        Unauthorized,

        /// <summary>
        /// ["<c>40102</c>"] Invalid nonce value.
        /// </summary>
        [Map("40102")]
        InvalidNonce,

        /// <summary>
        /// ["<c>40103</c>"] IP address not whitelisted.
        /// </summary>
        [Map("40103")]
        IpIllegal,

        /// <summary>
        /// ["<c>40104</c>"] User tier is invalid.
        /// </summary>
        [Map("40104")]
        UserTierInvalid,

        /// <summary>
        /// ["<c>40107</c>"] Exceeds maximum subscriptions.
        /// </summary>
        [Map("40107")]
        ExceedMaxSubscriptions,

        /// <summary>
        /// ["<c>40401</c>"] Not found.
        /// </summary>
        [Map("40401")]
        NotFound,

        /// <summary>
        /// ["<c>40801</c>"] Request has timed out.
        /// </summary>
        [Map("40801")]
        RequestTimeout,

        /// <summary>
        /// ["<c>42901</c>"] Too many requests.
        /// </summary>
        [Map("42901")]
        TooManyRequests,

        /// <summary>
        /// ["<c>43003</c>"] FOK order not filled and canceled.
        /// </summary>
        [Map("43003")]
        FillOrKill,

        /// <summary>
        /// ["<c>43004</c>"] IOC order not filled and canceled.
        /// </summary>
        [Map("43004")]
        ImmediateOrCancel,

        /// <summary>
        /// ["<c>43005</c>"] Rejected POST_ONLY create-order request.
        /// </summary>
        [Map("43005")]
        PostOnlyRejected,

        /// <summary>
        /// ["<c>43012</c>"] Canceled due to self-trade prevention.
        /// </summary>
        [Map("43012")]
        SelfTradePrevention,

        /// <summary>
        /// ["<c>50001</c>"] Credit line not maintained.
        /// </summary>
        [Map("50001")]
        CreditLineNotMaintained,

        /// <summary>
        /// ["<c>50002</c>"] Internal error occurred.
        /// </summary>
        [Map("50002")]
        InternalError,
    }
}