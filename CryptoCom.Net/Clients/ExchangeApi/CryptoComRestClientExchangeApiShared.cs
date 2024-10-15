using CryptoExchange.Net.SharedApis;
using System;
using System.Collections.Generic;
using System.Text;
using CryptoCom.Net.Interfaces.Clients.ExchangeApi;

namespace CryptoCom.Net.Clients.ExchangeApi
{
    internal partial class CryptoComRestClientExchangeApi : ICryptoComRestClientExchangeApiShared
    {
        public string Exchange => "CryptoCom";

        public TradingMode[] SupportedTradingModes => new[] { TradingMode.Spot };

        public void SetDefaultExchangeParameter(string key, object value) => ExchangeParameters.SetStaticParameter(Exchange, key, value);
        public void ResetDefaultExchangeParameters() => ExchangeParameters.ResetStaticParameters();
    }
}
