namespace CryptoCom.Net.Objects
{
    /// <summary>
    /// Api addresses
    /// </summary>
    public class CryptoComApiAddresses
    {
        /// <summary>
        /// The address used by the CryptoComRestClient for the API
        /// </summary>
        public string RestClientAddress { get; set; } = "";
        /// <summary>
        /// The address used by the CryptoComSocketClient for the websocket API
        /// </summary>
        public string SocketClientAddress { get; set; } = "";

        /// <summary>
        /// The default addresses to connect to the CryptoCom API
        /// </summary>
        public static CryptoComApiAddresses Default = new CryptoComApiAddresses
        {
            RestClientAddress = "https://api.crypto.com",
            SocketClientAddress = "wss://stream.crypto.com"
        };

        /// <summary>
        /// The default addresses to connect to the CryptoCom Sandbox API
        /// </summary>
        public static CryptoComApiAddresses Sandbox = new CryptoComApiAddresses
        {
            RestClientAddress = "https://uat-api.3ona.co",
            SocketClientAddress = "wss://uat-stream.3ona.co"
        };
    }
}
