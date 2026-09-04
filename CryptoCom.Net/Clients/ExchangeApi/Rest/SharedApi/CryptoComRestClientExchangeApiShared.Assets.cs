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
        #region Get All Assets

        async Task<ICallResult<SharedAsset[]>> IGetAllAssets.GetAllAssetsAsync(GetAssetsRequest request, CancellationToken ct)
            => await GetAllAssetsAsync(request, ct).ConfigureAwait(false);

        Task<HttpResult<SharedAsset[]>> IAssetsRestClient.GetAssetsAsync(GetAssetsRequest request, CancellationToken ct)
            => GetAllAssetsAsync(request, ct);
        GetAllAssetsOptions IAssetsRestClient.GetAssetsOptions => GetAllAssetsOptions;

        public GetAllAssetsOptions GetAllAssetsOptions { get; } = new GetAllAssetsOptions(_exchangeName, true);

        public async Task<HttpResult<SharedAsset[]>> GetAllAssetsAsync(GetAssetsRequest request, CancellationToken ct)
        {
            var validationError = GetAllAssetsOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedAsset[]>(Exchange, validationError);

            var assets = await _api.Account.GetAssetsAsync(ct: ct).ConfigureAwait(false);
            if (!assets.Success)
                return HttpResult.Fail<SharedAsset[]>(assets);

            return HttpResult.Ok(assets, assets.Data.Select(x => new SharedAsset(x.Key)
            {
                FullName = x.Value.FullName,
                Networks = x.Value.Networks.Select(x => new SharedAssetNetwork(x.NetworkId)
                {
                    MinConfirmations = x.ConfirmationRequired,
                    DepositEnabled = x.DepositEnabled,
                    MinWithdrawQuantity = x.MinWithdrawalQuantity,
                    WithdrawEnabled = x.WithdrawEnabled,
                    WithdrawFee = x.WithdrawalFee
                }).ToArray()
            }).ToArray());
        }

        #endregion

        #region Get Asset

        async Task<ICallResult<SharedAsset>> IGetAsset.GetAssetAsync(GetAssetRequest request, CancellationToken ct)
            => await GetAssetAsync(request, ct).ConfigureAwait(false);

        public GetAssetOptions GetAssetOptions { get; } = new GetAssetOptions(_exchangeName, false);
        public async Task<HttpResult<SharedAsset>> GetAssetAsync(GetAssetRequest request, CancellationToken ct)
        {
            var validationError = GetAssetOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedAsset>(Exchange, validationError);

            var assets = await _api.Account.GetAssetsAsync(ct: ct).ConfigureAwait(false);
            if (!assets.Success)
                return HttpResult.Fail<SharedAsset>(assets);

            var asset = assets.Data.SingleOrDefault(x => x.Key.Equals(request.Asset, StringComparison.InvariantCultureIgnoreCase));
            if (asset.Value == null)
                return HttpResult.Fail<SharedAsset>(Exchange, new ServerError(new ErrorInfo(ErrorType.UnknownAsset, "Asset not found")));

            return HttpResult.Ok(assets, new SharedAsset(asset.Key)
            {
                FullName = asset.Value.FullName,
                Networks = asset.Value.Networks.Select(x => new SharedAssetNetwork(x.NetworkId)
                {
                    MinConfirmations = x.ConfirmationRequired,
                    DepositEnabled = x.DepositEnabled,
                    MinWithdrawQuantity = x.MinWithdrawalQuantity,
                    WithdrawEnabled = x.WithdrawEnabled,
                    WithdrawFee = x.WithdrawalFee,
                }).ToArray()
            });
        }

        #endregion

    }
}
