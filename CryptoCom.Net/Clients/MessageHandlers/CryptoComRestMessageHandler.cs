using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Converters.SystemTextJson.MessageHandlers;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Errors;
using System.IO;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace CryptoCom.Net.Clients.MessageHandlers
{
    internal class CryptoComRestMessageHandler : JsonRestMessageHandler
    {
        private readonly ErrorMapping _errorMapping;

        public override JsonSerializerOptions Options { get; } = SerializerOptions.WithConverters(CryptoComExchange._serializerContext);

        public CryptoComRestMessageHandler(ErrorMapping errorMapping)
        {
            _errorMapping = errorMapping;
        }

        /// <inheritdoc />
        public override async ValueTask<Error> ParseErrorResponse(
            int httpStatusCode,
            HttpResponseHeaders responseHeaders,
            Stream responseStream)
        {
            var (jsonError, jsonDocument) = await GetJsonDocument(responseStream).ConfigureAwait(false);
            using var document = jsonDocument;
            if (jsonError != null || jsonDocument!.RootElement.ValueKind != JsonValueKind.Object)
                return new ServerError(new ErrorInfo(httpStatusCode == 401 || httpStatusCode == 403 ? ErrorType.Unauthorized : ErrorType.Unknown, $"HTTP {httpStatusCode}"));

            int? code = jsonDocument!.RootElement.TryGetProperty("code", out var codeProp) && codeProp.ValueKind == JsonValueKind.Number && codeProp.TryGetInt32(out var value) ? value : null;
            var msg = jsonDocument.RootElement.TryGetProperty("message", out var msgProp) && msgProp.ValueKind == JsonValueKind.String ? msgProp.GetString() : null;
            if (msg == null && code == null)
                return new ServerError(new ErrorInfo(httpStatusCode == 401 || httpStatusCode == 403 ? ErrorType.Unauthorized : ErrorType.Unknown, $"HTTP {httpStatusCode}"));

            if (code == null)
                return new ServerError(ErrorInfo.Unknown with { Message = msg });

            return new ServerError(code.Value, _errorMapping.GetErrorInfo(code.Value.ToString(), msg));
        }

        /// <inheritdoc />
        public override async ValueTask<ServerRateLimitError> ParseErrorRateLimitResponse(int httpStatusCode, HttpResponseHeaders responseHeaders, Stream responseStream)
        {
            var rateLimit = await base.ParseErrorRateLimitResponse(httpStatusCode, responseHeaders, responseStream).ConfigureAwait(false);
            var error = await ParseErrorResponse(httpStatusCode, responseHeaders, responseStream).ConfigureAwait(false);
            return new ServerRateLimitError(error.Message ?? error.ErrorDescription)
            {
                ErrorCode = error.ErrorCode,
                RetryAfter = rateLimit.RetryAfter
            };
        }
    }
}
