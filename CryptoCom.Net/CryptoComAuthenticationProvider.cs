using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using CryptoCom.Net.Objects.Internal;
using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Objects;

namespace CryptoCom.Net
{
    internal class CryptoComAuthenticationProvider : AuthenticationProvider
    {
        public CryptoComAuthenticationProvider(ApiCredentials credentials) : base(credentials)
        {
        }

        public override void AuthenticateRequest(
            RestApiClient apiClient,
            Uri uri,
            HttpMethod method,
            ref IDictionary<string, object>? uriParameters,
            ref IDictionary<string, object>? bodyParameters,
            ref Dictionary<string, string>? headers,
            bool auth,
            ArrayParametersSerialization arraySerialization,
            HttpMethodParameterPosition parameterPosition,
            RequestBodyFormat requestBodyFormat)
        {
            headers = new Dictionary<string, string>() { };

            if (!auth || bodyParameters == null)
                return;

            var x = (CryptoComRequest)bodyParameters["_BODY_"];

            var parameters = x.Parameters.OrderBy(x => x.Key);
            var paramString = string.Join("", parameters.Select(x => $"{x.Key}{Convert.ToString(x.Value, CultureInfo.InvariantCulture)}"));
            var nonce = DateTimeConverter.ConvertToMilliseconds(GetTimestamp(apiClient));
            var signString = $"{x.Method}{x.Id}{ApiKey}{paramString}{nonce}";

            var sign = SignHMACSHA256(signString, SignOutputType.Hex).ToLowerInvariant();
            x.Nonce = nonce;
            x.ApiKey = ApiKey;
            x.Signature = sign;
        }
    }
}
