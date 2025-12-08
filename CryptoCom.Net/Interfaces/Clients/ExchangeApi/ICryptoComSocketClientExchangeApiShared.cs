using CryptoExchange.Net.SharedApis;

namespace CryptoCom.Net.Interfaces.Clients.ExchangeApi
{
    /// <summary>
    /// Shared interface for TP_RP_API_NAME socket API usage
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
        IBalanceSocketClient
    {
    }
}
