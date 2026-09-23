using CryptoExchange.Net.SharedApis;

namespace CryptoCom.Net.Interfaces.Clients.ExchangeApi
{
    /// <summary>
    /// Shared interface for CryptoCom socket API usage
    /// </summary>
    public interface ICryptoComSocketClientExchangeApiShared :
        ITickerSocketClient,
        IBookTickerSocketClient,
        IKlineSocketClient,
        IOrderBookSocketClient,
        ITradeSocketClient,
        IUserTradeSocketClient,
        ISpotOrderSocketClient,
        IFuturesOrderSocketClient,
        IPositionSocketClient,
        IBalanceSocketClient,
        ISpotOrderManagementSocketClient,
        IFuturesOrderManagementSocketClient
    {
    }

    /// <summary>
    /// Shared API interface. Shared APIs provide a common,
    /// exchange-independent contract for accessing functionality across different
    /// exchange client libraries.
    /// </summary>
    public interface ICryptoComSocketClientExchangeSharedApi :
        ISubscribeTickerSocket,
        ISubscribeBookTickerSocket,
        ISubscribeKlinesSocket,
        ISubscribeOrderBookSocket,
        ISubscribeTradesSocket,
        ISubscribeUserTradesSocket,
        ISubscribeSpotOrdersSocket,
        ISubscribeFuturesOrdersSocket,
        ISubscribePositionsSocket,
        ISubscribeBalancesSocket,
        IPlaceSpotOrderSocket,
        ICancelSpotOrderSocket,
        IPlaceFuturesOrderSocket,
        ICancelFuturesOrderSocket
    { }
}
