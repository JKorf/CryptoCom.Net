using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Web;
using CryptoCom.Net.Objects.Internal;
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
            AuthenticateRequest(apiClient, x);
        }

        public void AuthenticateRequest(RestApiClient? client, CryptoComRequest request)
        {
            var paramString = ToParamString(request.Parameters);
            var nonce = DateTimeConverter.ConvertToMilliseconds(client != null ? GetTimestamp(client) : DateTime.UtcNow);
            var signString = $"{request.Method}{request.Id}{ApiKey}{paramString}{nonce}";

            var sign = SignHMACSHA256(signString, SignOutputType.Hex).ToLowerInvariant();
            request.Nonce = nonce;
            request.ApiKey = ApiKey;
            request.Signature = sign;
        }

        public string ToParamString(Dictionary<string, object> parameters)
        {
            var result = string.Empty;
            foreach(var parameter in parameters.OrderBy(x => x.Key))
            {
                result += parameter.Key;
                if (parameter.Value == null)
                {
                    result += "null";
                }
                else if (parameter.Value is Array array)
                {
                    foreach (var item in array)
                    {
                        if (item is string str)
                        {
                            result += str;
                        }
                        else
                        {
                            var dict = ToDictionary(item);
                            if (dict != null)
                                result += ToParamString(dict);
                        }
                    }
                }
                else
                {
                    result += Convert.ToString(parameter.Value, CultureInfo.InvariantCulture);
                }
            }

            return result;
        }

#if NET5_0_OR_GREATER
        [UnconditionalSuppressMessage("AssemblyLoadTrimming", "IL3050:RequiresUnreferencedCode", Justification = "JsonSerializerOptions provided here has TypeInfoResolver set")]
        [UnconditionalSuppressMessage("AssemblyLoadTrimming", "IL2026:RequiresUnreferencedCode", Justification = "JsonSerializerOptions provided here has TypeInfoResolver set")]
        [UnconditionalSuppressMessage("AssemblyLoadTrimming", "IL2075:RequiresUnreferencedCode", Justification = "All send types are registered in the TypeInfoResolver used and are therefor not trimmed")]
#endif
        private static Dictionary<string, object>? ToDictionary(object? obj)
        {
            if (obj == null)
                return null;

            var result = new Dictionary<string, object>();
            var properties = obj.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
            foreach(var prop in properties)
            {
                var nameAttr = prop.GetCustomAttribute<JsonPropertyNameAttribute>();
                if (nameAttr == null)
                    continue;

                var propValue = prop.GetValue(obj);
                var ignoreAttr = prop.GetCustomAttribute<JsonIgnoreAttribute>();
                if (ignoreAttr != null && propValue == null)
                    continue;

                var converterAttr = prop.GetCustomAttribute<JsonConverterAttribute>();
                if (converterAttr != null)
                {
                    var options = new JsonSerializerOptions();
                    options.TypeInfoResolver = CryptoComExchange._serializerContext.Options.TypeInfoResolver;
                    options.Converters.Add((JsonConverter)Activator.CreateInstance(converterAttr.ConverterType!)!);
                    propValue = JsonSerializer.Serialize(propValue, options).Replace("\"", "");
                }

                if (propValue is Array arr)
                {
                    foreach (var arrValue in arr)
                        result.Add(nameAttr.Name, arrValue);
                }
                else
                {
                    result.Add(nameAttr.Name, propValue!);
                }
            }

            return result;
        }
    }
}
