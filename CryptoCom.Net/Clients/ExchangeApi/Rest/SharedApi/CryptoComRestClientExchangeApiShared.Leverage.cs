using CryptoExchange.Net.SharedApis;
using System;
using System.Collections.Generic;
using CryptoCom.Net.Interfaces.Clients.ExchangeApi;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using CryptoExchange.Net.Objects;
using CryptoCom.Net.Enums;
using CryptoExchange.Net;
using CryptoCom.Net.Objects.Models;
using CryptoExchange.Net.Objects.Errors;

namespace CryptoCom.Net.Clients.ExchangeApi
{
    internal partial class CryptoComRestClientExchangeSharedApi
    {
        #region Leverage client
        public SharedLeverageSettingMode LeverageSettingType => SharedLeverageSettingMode.PerSymbol;

        public GetLeverageOptions GetLeverageOptions { get; } = new GetLeverageOptions(_exchangeName, true)
        {
            RequestNotes = "Returns the max account leverage"
        };
        public async Task<HttpResult<SharedLeverage>> GetLeverageAsync(GetLeverageRequest request, CancellationToken ct)
        {
            var validationError = GetLeverageOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedLeverage>(Exchange, validationError);

            var result = await _api.Account.GetAccountSettingsAsync(ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedLeverage>(result);

            if (!result.Data.Any())
                return HttpResult.Fail<SharedLeverage>(Exchange, new ServerError(new ErrorInfo(ErrorType.Unknown, "Not found")));

            return HttpResult.Ok(result, new SharedLeverage(result.Data.First().Leverage));
        }

        public SetLeverageOptions SetLeverageOptions { get; } = new SetLeverageOptions(_exchangeName)
        {
            RequestNotes = "This sets the max account leverage. If the AccountId exchange parameter is not provided it will be requested when executing this method",
            OptionalExchangeParameters = [
                new ParameterDescription(["AccountId", "account_id"],  typeof(string), "The account id to set the leverage for. If not provided the master account id will be used", 123L)
                ]
        };
        public async Task<HttpResult<SharedLeverage>> SetLeverageAsync(SetLeverageRequest request, CancellationToken ct)
        {
            var validationError = SetLeverageOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedLeverage>(Exchange, validationError);

            var userId = request.GetParamValue<string>(Exchange, "AccountId", "account_id");
            if (userId == null)
            {
                var user = await _api.Account.GetAccountInfoAsync(ct: ct).ConfigureAwait(false);
                if (!user.Success)
                    return HttpResult.Fail<SharedLeverage>(Exchange, user.Error!);

                userId = user.Data.MasterAccount.Uuid;
            }

            var result = await _api.Account.SetAccountLeverageAsync(userId, (int)request.Leverage, ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedLeverage>(result);

            return HttpResult.Ok(result, new SharedLeverage((int)request.Leverage));
        }
        #endregion
    }
}
