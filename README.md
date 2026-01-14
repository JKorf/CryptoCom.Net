# ![CryptoCom.Net](https://raw.githubusercontent.com/JKorf/CryptoCom.Net/main/CryptoCom.Net/Icon/icon.png) CryptoCom.Net  

[![.NET](https://img.shields.io/github/actions/workflow/status/JKorf/CryptoCom.Net/dotnet.yml?style=for-the-badge)](https://github.com/JKorf/CryptoCom.Net/actions/workflows/dotnet.yml) ![License](https://img.shields.io/github/license/JKorf/CryptoCom.Net?style=for-the-badge)

CryptoCom.Net is a client library for accessing the [Crypto.com REST and Websocket API](https://exchange-docs.crypto.com/exchange/v1/rest-ws/index.html#introduction). 

## Features
* Response data is mapped to descriptive models
* Input parameters and response values are mapped to discriptive enum values where possible
* High performance
* Automatic websocket (re)connection management 
* Client side rate limiting 
* Client side order book implementation
* Support for managing different accounts
* Extensive logging
* Support for different environments
* Easy integration with other exchange client based on the CryptoExchange.Net base library
* Native AOT support

## Supported Frameworks
The library is targeting both `.NET Standard 2.0` and `.NET Standard 2.1` for optimal compatibility, as well as the latest dotnet versions to use the latest framework features.

|.NET implementation|Version Support|
|--|--|
|.NET Core|`2.0` and higher|
|.NET Framework|`4.6.1` and higher|
|Mono|`5.4` and higher|
|Xamarin.iOS|`10.14` and higher|
|Xamarin.Android|`8.0` and higher|
|UWP|`10.0.16299` and higher|
|Unity|`2018.1` and higher|

## Install the library

### NuGet 
[![NuGet version](https://img.shields.io/nuget/v/CryptoCom.net.svg?style=for-the-badge)](https://www.nuget.org/packages/CryptoCom.Net)  [![Nuget downloads](https://img.shields.io/nuget/dt/CryptoCom.Net.svg?style=for-the-badge)](https://www.nuget.org/packages/CryptoCom.Net)

	dotnet add package CryptoCom.Net
	
### GitHub packages
CryptoCom.Net is available on [GitHub packages](https://github.com/JKorf/CryptoCom.Net/pkgs/nuget/CryptoCom.Net). You'll need to add `https://nuget.pkg.github.com/JKorf/index.json` as a NuGet package source.

### Download release
[![GitHub Release](https://img.shields.io/github/v/release/JKorf/CryptoCom.Net?style=for-the-badge&label=GitHub)](https://github.com/JKorf/CryptoCom.Net/releases)

The NuGet package files are added along side the source with the latest GitHub release which can found [here](https://github.com/JKorf/CryptoCom.Net/releases).

## How to use
* REST Endpoints
	```csharp
	// Get the ETH/USD ticker via rest request
	var restClient = new CryptoComRestClient();
	var tickerResult = await restClient.ExchangeApi.ExchangeData.GetTickerAsync("ETH_USD");
	var lastPrice = tickerResult.Data.LastPrice;
	```
* Websocket streams
	```csharp
	// Subscribe to ETH/USD ticker updates via the websocket API
	var socketClient = new CryptoComSocketClient();
	var tickerSubscriptionResult = socketClient.ExchangeApi.SubscribeToTickerUpdatesAsync("ETH_USD", (update) => 
	{
	  var lastPrice = update.Data.LastPrice;
	});
	```

For information on the clients, dependency injection, response processing and more see the [documentation](https://cryptoexchange.jkorf.dev?library=CryptoCom.Net), or have a look at the examples [here](https://github.com/JKorf/CryptoCom.Net/tree/main/Examples) or [here](https://github.com/JKorf/CryptoExchange.Net/tree/master/Examples).

## CryptoExchange.Net
CryptoCom.Net is based on the [CryptoExchange.Net](https://github.com/JKorf/CryptoExchange.Net) base library. Other exchange API implementations based on the CryptoExchange.Net base library are available and follow the same logic.

CryptoExchange.Net also allows for [easy access to different exchange API's](https://cryptoexchange.jkorf.dev/client-libs/shared).

|Exchange|Repository|Nuget|
|--|--|--|
|Aster|[JKorf/Aster.Net](https://github.com/JKorf/Aster.Net)|[![Nuget version](https://img.shields.io/nuget/v/JKorf.Aster.net.svg?style=flat-square)](https://www.nuget.org/packages/JKorf.Aster.Net)|
|Binance|[JKorf/Binance.Net](https://github.com/JKorf/Binance.Net)|[![Nuget version](https://img.shields.io/nuget/v/Binance.net.svg?style=flat-square)](https://www.nuget.org/packages/Binance.Net)|
|BingX|[JKorf/BingX.Net](https://github.com/JKorf/BingX.Net)|[![Nuget version](https://img.shields.io/nuget/v/JK.BingX.net.svg?style=flat-square)](https://www.nuget.org/packages/JK.BingX.Net)|
|Bitfinex|[JKorf/Bitfinex.Net](https://github.com/JKorf/Bitfinex.Net)|[![Nuget version](https://img.shields.io/nuget/v/Bitfinex.net.svg?style=flat-square)](https://www.nuget.org/packages/Bitfinex.Net)|
|Bitget|[JKorf/Bitget.Net](https://github.com/JKorf/Bitget.Net)|[![Nuget version](https://img.shields.io/nuget/v/JK.Bitget.net.svg?style=flat-square)](https://www.nuget.org/packages/JK.Bitget.Net)|
|BitMart|[JKorf/BitMart.Net](https://github.com/JKorf/BitMart.Net)|[![Nuget version](https://img.shields.io/nuget/v/BitMart.net.svg?style=flat-square)](https://www.nuget.org/packages/BitMart.Net)|
|BitMEX|[JKorf/BitMEX.Net](https://github.com/JKorf/BitMEX.Net)|[![Nuget version](https://img.shields.io/nuget/v/JKorf.BitMEX.net.svg?style=flat-square)](https://www.nuget.org/packages/JKorf.BitMEX.Net)|
|BloFin|[JKorf/BloFin.Net](https://github.com/JKorf/BloFin.Net)|[![Nuget version](https://img.shields.io/nuget/v/BloFin.net.svg?style=flat-square)](https://www.nuget.org/packages/BloFin.Net)|
|Bybit|[JKorf/Bybit.Net](https://github.com/JKorf/Bybit.Net)|[![Nuget version](https://img.shields.io/nuget/v/Bybit.net.svg?style=flat-square)](https://www.nuget.org/packages/Bybit.Net)|
|Coinbase|[JKorf/Coinbase.Net](https://github.com/JKorf/Coinbase.Net)|[![Nuget version](https://img.shields.io/nuget/v/JKorf.Coinbase.net.svg?style=flat-square)](https://www.nuget.org/packages/JKorf.Coinbase.Net)|
|CoinEx|[JKorf/CoinEx.Net](https://github.com/JKorf/CoinEx.Net)|[![Nuget version](https://img.shields.io/nuget/v/CoinEx.net.svg?style=flat-square)](https://www.nuget.org/packages/CoinEx.Net)|
|CoinGecko|[JKorf/CoinGecko.Net](https://github.com/JKorf/CoinGecko.Net)|[![Nuget version](https://img.shields.io/nuget/v/CoinGecko.net.svg?style=flat-square)](https://www.nuget.org/packages/CoinGecko.Net)|
|CoinW|[JKorf/CoinW.Net](https://github.com/JKorf/CoinW.Net)|[![Nuget version](https://img.shields.io/nuget/v/CoinW.net.svg?style=flat-square)](https://www.nuget.org/packages/CoinW.Net)|
|DeepCoin|[JKorf/DeepCoin.Net](https://github.com/JKorf/DeepCoin.Net)|[![Nuget version](https://img.shields.io/nuget/v/DeepCoin.net.svg?style=flat-square)](https://www.nuget.org/packages/DeepCoin.Net)|
|Gate.io|[JKorf/GateIo.Net](https://github.com/JKorf/GateIo.Net)|[![Nuget version](https://img.shields.io/nuget/v/GateIo.net.svg?style=flat-square)](https://www.nuget.org/packages/GateIo.Net)|
|HTX|[JKorf/HTX.Net](https://github.com/JKorf/HTX.Net)|[![Nuget version](https://img.shields.io/nuget/v/JKorf.HTX.net.svg?style=flat-square)](https://www.nuget.org/packages/Jkorf.HTX.Net)|
|HyperLiquid|[JKorf/HyperLiquid.Net](https://github.com/JKorf/HyperLiquid.Net)|[![Nuget version](https://img.shields.io/nuget/v/HyperLiquid.Net.svg?style=flat-square)](https://www.nuget.org/packages/HyperLiquid.Net)|
|Gate.io|[JKorf/GateIo.Net](https://github.com/JKorf/GateIo.Net)|[![Nuget version](https://img.shields.io/nuget/v/GateIo.net.svg?style=flat-square)](https://www.nuget.org/packages/GateIo.Net)|
|Kraken|[JKorf/Kraken.Net](https://github.com/JKorf/Kraken.Net)|[![Nuget version](https://img.shields.io/nuget/v/KrakenExchange.net.svg?style=flat-square)](https://www.nuget.org/packages/KrakenExchange.Net)|
|Kucoin|[JKorf/Kucoin.Net](https://github.com/JKorf/Kucoin.Net)|[![Nuget version](https://img.shields.io/nuget/v/Kucoin.net.svg?style=flat-square)](https://www.nuget.org/packages/Kucoin.Net)|
|Mexc|[JKorf/Mexc.Net](https://github.com/JKorf/Mexc.Net)|[![Nuget version](https://img.shields.io/nuget/v/JK.Mexc.net.svg?style=flat-square)](https://www.nuget.org/packages/JK.Mexc.Net)|
|OKX|[JKorf/OKX.Net](https://github.com/JKorf/OKX.Net)|[![Nuget version](https://img.shields.io/nuget/v/JK.OKX.net.svg?style=flat-square)](https://www.nuget.org/packages/JK.OKX.Net)|
|Upbit|[JKorf/Upbit.Net](https://github.com/JKorf/Upbit.Net)|[![Nuget version](https://img.shields.io/nuget/v/JKorf.Upbit.net.svg?style=flat-square)](https://www.nuget.org/packages/JKorf.Upbit.Net)|
|Toobit|[JKorf/Toobit.Net](https://github.com/JKorf/Toobit.Net)|[![Nuget version](https://img.shields.io/nuget/v/Toobit.net.svg?style=flat-square)](https://www.nuget.org/packages/Toobit.Net)|
|WhiteBit|[JKorf/WhiteBit.Net](https://github.com/JKorf/WhiteBit.Net)|[![Nuget version](https://img.shields.io/nuget/v/WhiteBit.net.svg?style=flat-square)](https://www.nuget.org/packages/WhiteBit.Net)|
|XT|[JKorf/XT.Net](https://github.com/JKorf/XT.Net)|[![Nuget version](https://img.shields.io/nuget/v/XT.net.svg?style=flat-square)](https://www.nuget.org/packages/XT.Net)|

When using multiple of these API's the [CryptoClients.Net](https://github.com/JKorf/CryptoClients.Net) package can be used which combines this and the other packages and allows easy access to all exchange API's.

## Discord
[![Nuget version](https://img.shields.io/discord/847020490588422145?style=for-the-badge)](https://discord.gg/MSpeEtSY8t)  
A Discord server is available [here](https://discord.gg/MSpeEtSY8t). For discussion and/or questions around the CryptoExchange.Net and implementation libraries, feel free to join.

## Supported functionality

### Exchange REST API
|API|Supported|Location|
|--|--:|--|
|Reference and Market Data|✓|`restClient.ExchangeApi.ExchangeData`|
|Account Balance and Position|✓|`restClient.ExchangeApi.Account` / `restClient.ExchangeApi.Trading`|
|Trading|✓|`restClient.ExchangeApi.Account` / `restClient.ExchangeApi.Trading`|
|Advanced Order Management|✓|`restClient.ExchangeApi.Trading`|
|Order, Trade, Transaction History|✓|`restClient.ExchangeApi.Account` / `restClient.ExchangeApi.Trading`|
|Wallet|✓|`restClient.ExchangeApi.Account`|
|Staking|✓|`restClient.ExchangeApi.Staking`|

### Exchange Websocket API
|API|Supported|Location|
|--|--:|--|
|User subscription|✓|`socketClient.ExchangeApi`|
|Order management|✓|`socketClient.ExchangeApi`|
|Market subscription|✓|`socketClient.ExchangeApi`|

## Support the project
Any support is greatly appreciated.

### Referal
If you do not yet have an account please consider using this referal link to sign up:  
[Link](https://crypto.com/exch/26ge92xbkn)

### Donate
Make a one time donation in a crypto currency of your choice. If you prefer to donate a currency not listed here please contact me.

**Btc**:  bc1q277a5n54s2l2mzlu778ef7lpkwhjhyvghuv8qf  
**Eth**:  0xcb1b63aCF9fef2755eBf4a0506250074496Ad5b7   
**USDT (TRX)**  TKigKeJPXZYyMVDgMyXxMf17MWYia92Rjd 

### Sponsor
Alternatively, sponsor me on Github using [Github Sponsors](https://github.com/sponsors/JKorf). 

## Release notes
* Version 3.2.1 - 14 Jan 2026
    * Updated CryptoExchange.Net to version 10.2.3, see https://github.com/JKorf/CryptoExchange.Net/releases/ for full release notes

* Version 3.2.0 - 13 Jan 2026
    * Updated CryptoExchange.Net to version 10.2.0, see https://github.com/JKorf/CryptoExchange.Net/releases/ for full release notes
    * Added SequenceNumber to order book websocket updates
    * Isolated Margin Position support: Added restClient.ExchangeApi.Account.CreateIsolatedMarginTransferAsync endpoint
    * Isolated Margin Position support: Added restClient.ExchangeApi.Account.SetIsolatedMarginLeverageAsync endpoint
    * Isolated Margin Position support: Added various isolationId parameter to various endpoints
    * Isolated Margin Position support: Added various IsolationId property to various response models

* Version 3.1.0 - 07 Jan 2026
    * Updated CryptoExchange.Net version to 10.1.0, see https://github.com/JKorf/CryptoExchange.Net/releases/ for full release notes
    * Added DataTimeLocal and DataAge properties to DataEvent object
    * Added UpdateServerTime, UpdateLocalTime and DataAge properties to (I)SymbolOrderBook
    * Updated GetTransactionHistory to send time as nanoseconds

* Version 3.0.3 - 19 Dec 2025
    * Added missing value TransactionType enum
    * Fixed first first trade update not getting marked as snapshot
    * Updated CryptoExchange.Net to 10.0.2 to fix parameter serialization error

* Version 3.0.2 - 18 Dec 2025
    * Fixed Shared API Spot Ticker updates not calling callback

* Version 3.0.1 - 16 Dec 2025
    * Fixed socketClient.ExchangeApi.SubscribeToOrderUpdatesAsync subscription not recognizing messages

* Version 3.0.0 - 16 Dec 2025
    * Added Net10.0 target framework
    * Updated CryptoExchange.Net version to 10.0.0, see https://github.com/JKorf/CryptoExchange.Net/releases/ for full release notes
    * Improved performance across the board, biggest gains in websocket message processing
    * Updated REST message response handling
    * Updated WebSocket message handling
    * Added UseUpdatedDeserialization socket client options to toggle by new and old message handling
    * Added SocketIndividualSubscriptionCombineTarget socket client option
    * Updated Shared API's subscription update types from ExchangeEvent to DataEvent

* Version 2.12.0 - 11 Nov 2025
    * Updated CryptoExchange.Net to version 9.13.0

* Version 2.11.0 - 03 Nov 2025
    * Updated CryptoExchange.Net to version 9.12.0
    * Added support for using SharedSymbol.UsdOrStable in Shared APIs
    * Fixed pagination for the GetDepositHistoryAsync method
    * Fixed exception when initial trade snapshot has no items in TradeTracker
    * Removed some unhelpful verbose logs

* Version 2.10.0 - 16 Oct 2025
    * Updated CryptoExchange.Net version to 9.10.0, see https://github.com/JKorf/CryptoExchange.Net/releases/
    * Added ClientOrderId mapping on SharedUserTrade models

* Version 2.9.0 - 30 Sep 2025
    * Updated CryptoExchange.Net version to 9.8.0, see https://github.com/JKorf/CryptoExchange.Net/releases/
    * Added ITrackerFactory to TrackerFactory implementation

* Version 2.8.0 - 01 Sep 2025
    * Updated CryptoExchange.Net version to 9.7.0, see https://github.com/JKorf/CryptoExchange.Net/releases/
    * HTTP REST requests will now use HTTP version 2.0 by default

* Version 2.7.0 - 25 Aug 2025
    * Updated CryptoExchange.Net version to 9.6.0, see https://github.com/JKorf/CryptoExchange.Net/releases/
    * Added ClearUserClients method to user client provider

* Version 2.6.1 - 21 Aug 2025
    * Added websocket unknown symbol error mapping

* Version 2.6.0 - 20 Aug 2025
    * Updated CryptoExchange.Net to version 9.5.0, see https://github.com/JKorf/CryptoExchange.Net/releases/
    * Added improved error parsing
    * Fixed serialization of restClient.ExchangeApi.Trading.PlaceMultipleOrdersAsync when no execution mode is provided

* Version 2.5.0 - 04 Aug 2025
    * Updated CryptoExchange.Net to version 9.4.0, see https://github.com/JKorf/CryptoExchange.Net/releases/

* Version 2.4.0 - 23 Jul 2025
    * Updated CryptoExchange.Net to version 9.3.0, see https://github.com/JKorf/CryptoExchange.Net/releases/
    * Updated websocket message matching

* Version 2.3.0 - 15 Jul 2025
    * Updated CryptoExchange.Net to version 9.2.0, see https://github.com/JKorf/CryptoExchange.Net/releases/
    * Added SmartPostOnly parameters and enum value

* Version 2.2.0 - 20 Jun 2025
    * Added restClient.ExchangeApi.ExchangeData.GetAnnouncementsAsync endpoint
    * Added restClient.ExchangeApi.Trading.EditOrderAsync endpoint

* Version 2.1.0 - 02 Jun 2025
    * Updated CryptoExchange.Net to version 9.1.0, see https://github.com/JKorf/CryptoExchange.Net/releases/
    * Added (I)CryptoComUserClientProvider allowing for easy client management when handling multiple users

* Version 2.0.0 - 13 May 2025
    * Updated CryptoExchange.Net to version 9.0.0, see https://github.com/JKorf/CryptoExchange.Net/releases/
    * Added support for Native AOT compilation
    * Added RateLimitUpdated event
    * Added SharedSymbol response property to all Shared interfaces response models returning a symbol name
    * Added GenerateClientOrderId method to ExchangeApi Shared client
    * Added IBookTickerRestClient implementation to ExchangeApi Shared client
    * Added ISpotOrderClientIdClient implementation to ExchangeApi Shared client
    * Added IFuturesOrderClientIdClient implementation to ExchangeApi Shared client
    * Added ISpotTriggerOrderRestClient implementation to ExchangeApi Shared client
    * Added IFuturesTriggerOrderRestClient implementation to ExchangeApi Shared client
    * Added IFuturesTpSlRestClient implementation to ExchangeApi Shared client
    * Added MaxLongLeverage, MaxShortLeverage to SharedFuturesSymbol model
    * Added TriggerPrice, IsTriggerOrder properties to SharedSpotOrder model
    * Added TriggerPrice, IsTriggerOrder properties to SharedFuturesOrder model
    * Added OptionalExchangeParameters and Supported properties to EndpointOptions
    * Added TransactionTime to CryptoComUserTrade model
    * Added All property to retrieve all available environment on CryptoComEnvironment
    * Refactored Shared clients quantity parameters and responses to use SharedQuantity
    * Updated Reason property on CryptoComOrder to return a OrderRejectedReason Enum value
    * Updated all IEnumerable response and model types to array response types
    * Updated PlaceMultipleOrdersAsync endpoints to return a list of CallResult models and an error if all orders fail to place
    * Renamed CryptoComExchangeSymbolOrderBook to CryptoComSymbolOrderBook
    * Removed Newtonsoft.Json dependency
    * Removed legacy AddCryptoCom(restOptions, socketOptions) DI overload
    * Improved socket client order placement response processing
    * Fixed incorrect DataTradeMode on certain Shared interface responses
    * Fixed InvalidOperationException in user data snapshot updates when there is no data
    * Fixed Shared spot order socket updates having a larger fill quantity than order quantity
    * Fixed some typos

* Version 2.0.0-beta4 - 01 May 2025
    * Updated CryptoExchange.Net version to 9.0.0-beta5
    * Added property to retrieve all available API environments
    * Added mapping of CryptoComOrder Reason property to enum

* Version 2.0.0-beta3 - 25 Apr 2025
    * Fixed InvalidOperationException in user data snapshot updates if the snapshot is empty

* Version 2.0.0-beta2 - 23 Apr 2025
    * Updated CryptoExchange.Net to version 9.0.0-beta2
    * Fixed incorrect DataTradeMode on responses

* Version 2.0.0-beta1 - 22 Apr 2025
    * Updated CryptoExchange.Net to version 9.0.0-beta1, see https://github.com/JKorf/CryptoExchange.Net/releases/
    * Added support for Native AOT compilation
    * Added RateLimitUpdated event
    * Added SharedSymbol response property to all Shared interfaces response models returning a symbol name
    * Added GenerateClientOrderId method to ExchangeApi Shared client
    * Added IBookTickerRestClient implementation to ExchangeApi Shared client
    * Added ISpotOrderClientIdClient implementation to ExchangeApi Shared client
    * Added IFuturesOrderClientIdClient implementation to ExchangeApi Shared client
    * Added ISpotTriggerOrderRestClient implementation to ExchangeApi Shared client
    * Added IFuturesTriggerOrderRestClient implementation to ExchangeApi Shared client
    * Added IFuturesTpSlRestClient implementation to ExchangeApi Shared client
    * Added MaxLongLeverage, MaxShortLeverage to SharedFuturesSymbol model
    * Added TriggerPrice, IsTriggerOrder properties to SharedSpotOrder model
    * Added TriggerPrice, IsTriggerOrder properties to SharedFuturesOrder model
    * Added OptionalExchangeParameters and Supported properties to EndpointOptions
    * Refactored Shared clients quantity parameters and responses to use SharedQuantity
    * Updated all IEnumerable response and model types to array response types
    * Updated PlaceMultipleOrdersAsync endpoints to return a list of CallResult models and an error if all orders fail to place
    * Renamed CryptoComExchangeSymbolOrderBook to CryptoComSymbolOrderBook
    * Removed Newtonsoft.Json dependency
    * Removed legacy AddCryptoCom(restOptions, socketOptions) DI overload
    * Improved socket client order placement response processing
    * Fixed some typos

* Version 1.6.0 - 11 Feb 2025
    * Updated CryptoExchange.Net to version 8.8.0, see https://github.com/JKorf/CryptoExchange.Net/releases/
    * Added support for more SharedKlineInterval values
    * Added setting of DataTime value on websocket DataEvent updates
    * Fixed incorrect API docs refernces for subscription methods
    * Fix Mono runtime exception on rest client construction using DI

* Version 1.5.1 - 07 Jan 2025
    * Updated CryptoExchange.Net version
    * Added Type property to CryptoComExchange class

* Version 1.5.0 - 23 Dec 2024
    * Updated CryptoExchange.Net to version 8.5.0, see https://github.com/JKorf/CryptoExchange.Net/releases/
    * Added SetOptions methods on Rest and Socket clients
    * Added setting of DefaultProxyCredentials to CredentialCache.DefaultCredentials on the DI http client
    * Improved websocket disconnect detection

* Version 1.4.1 - 03 Dec 2024
    * Updated CryptoExchange.Net to version 8.4.3, see https://github.com/JKorf/CryptoExchange.Net/releases/
    * Fixed orderbook creation via CryptoComBookFactory

* Version 1.4.0 - 28 Nov 2024
    * Updated CryptoExchange.Net to version 8.4.0, see https://github.com/JKorf/CryptoExchange.Net/releases/tag/8.4.0
    * Added GetFeesAsync Shared REST client implementations
    * Updated CryptoComOptions to LibraryOptions implementation
    * Updated test and analyzer package versions

* Version 1.3.0 - 19 Nov 2024
    * Updated CryptoExchange.Net to version 8.3.0, see https://github.com/JKorf/CryptoExchange.Net/releases/tag/8.3.0
    * Added support for loading client settings from IConfiguration
    * Added DI registration method for configuring Rest and Socket options at the same time
    * Added DisplayName and ImageUrl properties to CryptoComExchange class
    * Updated client constructors to accept IOptions from DI
    * Removed redundant CryptoComSocketClient constructor

* Version 1.2.1 - 18 Nov 2024
    * Fixed deserialization issue in restClient.ExchangeApi.ExchangeData.GetTickerAsync

* Version 1.2.0 - 06 Nov 2024
    * Updated CryptoExchange.Net to version 8.2.0, see https://github.com/JKorf/CryptoExchange.Net/releases/tag/8.2.0

* Version 1.1.0 - 28 Oct 2024
    * Updated CryptoExchange.Net to version 8.1.0, see https://github.com/JKorf/CryptoExchange.Net/releases/tag/8.1.0
    * Moved FormatSymbol to CryptoComExchange class
    * Added support Side setting on SharedTrade model
    * Added CryptoComTrackerFactory for creating trackers
    * Added overload to Create method on CryptoComOrderBookFactory support SharedSymbol parameter
    * Renamed CreateExchange method on CryptoComOrderBookFactory to Create

* Version 1.0.1 - 22 Oct 2024
    * Fixed CryptoExchange.Net reference

* Version 1.0.0 - 22 Oct 2024
    * Initial release

