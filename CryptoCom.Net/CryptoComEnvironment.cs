using CryptoExchange.Net.Objects;
using CryptoCom.Net.Objects;

namespace CryptoCom.Net
{
    /// <summary>
    /// CryptoCom environments
    /// </summary>
    public class CryptoComEnvironment : TradeEnvironment
    {
        /// <summary>
        /// Rest API address
        /// </summary>
        public string RestClientAddress { get; }

        /// <summary>
        /// Socket API address
        /// </summary>
        public string SocketClientAddress { get; }

        internal CryptoComEnvironment(
            string name,
            string restAddress,
            string streamAddress) :
            base(name)
        {
            RestClientAddress = restAddress;
            SocketClientAddress = streamAddress;
        }

        /// <summary>
        /// Live environment
        /// </summary>
        public static CryptoComEnvironment Live { get; }
            = new CryptoComEnvironment(TradeEnvironmentNames.Live,
                                     CryptoComApiAddresses.Default.RestClientAddress,
                                     CryptoComApiAddresses.Default.SocketClientAddress);

        /// <summary>
        /// Sandbox environment
        /// </summary>
        public static CryptoComEnvironment Sandbox { get; }
            = new CryptoComEnvironment(TradeEnvironmentNames.Testnet,
                                     CryptoComApiAddresses.Sandbox.RestClientAddress,
                                     CryptoComApiAddresses.Sandbox.SocketClientAddress);

        /// <summary>
        /// Create a custom environment
        /// </summary>
        /// <param name="name"></param>
        /// <param name="spotRestAddress"></param>
        /// <param name="spotSocketStreamsAddress"></param>
        /// <returns></returns>
        public static CryptoComEnvironment CreateCustom(
                        string name,
                        string spotRestAddress,
                        string spotSocketStreamsAddress)
            => new CryptoComEnvironment(name, spotRestAddress, spotSocketStreamsAddress);
    }
}
