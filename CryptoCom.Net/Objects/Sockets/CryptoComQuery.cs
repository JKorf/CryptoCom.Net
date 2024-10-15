using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using System.Collections.Generic;
using CryptoCom.Net.Objects.Models;

namespace CryptoCom.Net.Objects.Sockets
{
    internal class CryptoComQuery<T> : Query<T>
    {
        public override HashSet<string> ListenerIdentifiers { get; set; }

        public CryptoComQuery(CryptoComModel request, bool authenticated, int weight = 1) : base(request, authenticated, weight)
        {
            ListenerIdentifiers = new HashSet<string> { };
        }

        public override CallResult<T> HandleMessage(SocketConnection connection, DataEvent<T> message)
        {
            return message.ToCallResult();
        }
    }
}
