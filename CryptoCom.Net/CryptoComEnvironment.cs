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
        /// ctor for DI, use <see cref="CreateCustom"/> for creating a custom environment
        /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        public CryptoComEnvironment() : base(TradeEnvironmentNames.Live)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        { }

        /// <summary>
        /// Get the CryptoCom environment by name
        /// </summary>
        public static CryptoComEnvironment? GetEnvironmentByName(string? name)
         => name switch
         {
             TradeEnvironmentNames.Live => Live,
             TradeEnvironmentNames.Testnet => Sandbox,
             "" => Live,
             null => Live,
             _ => default
         };

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
