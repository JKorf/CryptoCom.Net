using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace CryptoCom.Net.Enums
{
    /// <summary>
    /// Withdrawal status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<WithdrawalStatus>))]
    public enum WithdrawalStatus
    {
        /// <summary>
        /// ["<c>0</c>"] Pending
        /// </summary>
        [Map("0")]
        Pending,
        /// <summary>
        /// ["<c>1</c>"] Processing
        /// </summary>
        [Map("1")]
        Processing,
        /// <summary>
        /// ["<c>2</c>"] Rejected
        /// </summary>
        [Map("2")]
        Rejected,
        /// <summary>
        /// ["<c>3</c>"] Payment in-progress
        /// </summary>
        [Map("3")]
        PaymentInProgress,
        /// <summary>
        /// ["<c>4</c>"] Payment failed
        /// </summary>
        [Map("4")]
        PaymentFailed,
        /// <summary>
        /// ["<c>5</c>"] Completed
        /// </summary>
        [Map("5")]
        Completed,
        /// <summary>
        /// ["<c>6</c>"] Cancelled
        /// </summary>
        [Map("6")]
        Canceled
    }

}
