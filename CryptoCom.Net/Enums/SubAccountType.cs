using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace CryptoCom.Net.Enums
{
    /// <summary>
    /// Account type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<SubAccountType>))]
    public enum SubAccountType
    {
        /// <summary>
        /// ["<c>MASTER</c>"] Master account
        /// </summary>
        [Map("MASTER")]
        Master,
        /// <summary>
        /// ["<c>SUB_ACCOUNT</c>"] Sub account
        /// </summary>
        [Map("SUB_ACCOUNT")]
        SubAccount
    }
}
