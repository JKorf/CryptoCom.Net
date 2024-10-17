using CryptoExchange.Net.SharedApis;
using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoCom.Net.Interfaces.Clients.ExchangeApi
{
    /// <summary>
    /// Shared interface for Exchange rest API usage
    /// </summary>
    public interface ICryptoComRestClientExchangeApiShared :
        IAssetsRestClient,
        IBalanceRestClient,
        IDepositRestClient,
        IKlineRestClient,
        IOrderBookRestClient,
        IRecentTradeRestClient,
        IWithdrawalRestClient,
        IWithdrawRestClient,
        ISpotSymbolRestClient,
        ISpotTickerRestClient,
        ISpotOrderRestClient,
        IFundingRateRestClient,
        IFuturesSymbolRestClient,
        IFuturesTickerRestClient,
        ILeverageRestClient,
        IOpenInterestRestClient,
        IFuturesOrderRestClient
    {
    }
}
