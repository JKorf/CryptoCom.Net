using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Objects.Options;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoCom.Net.Objects.Options
{
    /// <summary>
    /// CryptoCom options
    /// </summary>
    public class CryptoComOptions : LibraryOptions<CryptoComRestOptions, CryptoComSocketOptions, ApiCredentials, CryptoComEnvironment>
    {
    }
}
