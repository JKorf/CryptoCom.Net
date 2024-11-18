using CryptoExchange.Net.Authentication;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoCom.Net.Objects.Options
{
    /// <summary>
    /// CryptoCom options
    /// </summary>
    public class CryptoComOptions
    {
        /// <summary>
        /// Rest client options
        /// </summary>
        public CryptoComRestOptions Rest { get; set; } = new CryptoComRestOptions();

        /// <summary>
        /// Socket client options
        /// </summary>
        public CryptoComSocketOptions Socket { get; set; } = new CryptoComSocketOptions();

        /// <summary>
        /// Trade environment. Contains info about URL's to use to connect to the API. Use `CryptoComEnvironment` to swap environment, for example `Environment = CryptoComEnvironment.Live`
        /// </summary>
        public CryptoComEnvironment? Environment { get; set; }

        /// <summary>
        /// The api credentials used for signing requests.
        /// </summary>
        public ApiCredentials? ApiCredentials { get; set; }

        /// <summary>
        /// The DI service lifetime for the ICryptoComSocketClient
        /// </summary>
        public ServiceLifetime? SocketClientLifeTime { get; set; }
    }
}
