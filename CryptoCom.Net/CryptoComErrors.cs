using CryptoExchange.Net.Objects.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoCom.Net
{
    internal static class CryptoComErrors
    {
        public static ErrorMapping Errors { get; } = new ErrorMapping([
            new ErrorInfo(ErrorType.Unauthorized, false, "Account suspended", "202"),
            new ErrorInfo(ErrorType.Unauthorized, false, "User does not have derivatives access", "411", "412"),
            new ErrorInfo(ErrorType.Unauthorized, false, "Unauthorized", "40101"),
            new ErrorInfo(ErrorType.Unauthorized, false, "IP not allowed", "40103"),
            new ErrorInfo(ErrorType.Unauthorized, false, "Not allowed based on user tier", "40104"),

            new ErrorInfo(ErrorType.InvalidTimestamp, false, "Invalid timestamp", "40102"),

            new ErrorInfo(ErrorType.Timeout, false, "Request timeout", "40801"),

            new ErrorInfo(ErrorType.SystemError, false, "Internal error", "50001"),

            new ErrorInfo(ErrorType.NoPosition, false, "No position", "201", "317"),

            new ErrorInfo(ErrorType.DuplicateClientOrderId, false, "Duplicate client order id", "204"),

            new ErrorInfo(ErrorType.InvalidParameter, false, "Duplicate order id", "205"),
            new ErrorInfo(ErrorType.InvalidParameter, false, "Invalid settle asset", "214"),
            new ErrorInfo(ErrorType.InvalidParameter, false, "Invalid fee asset", "215"),
            new ErrorInfo(ErrorType.InvalidParameter, false, "Maximum entry leverage exceeded", "225"),
            new ErrorInfo(ErrorType.InvalidParameter, false, "Invalid leverage", "226"),
            new ErrorInfo(ErrorType.InvalidParameter, false, "Invalid slippage", "227"),
            new ErrorInfo(ErrorType.InvalidParameter, false, "Invalid trigger type", "230"),
            new ErrorInfo(ErrorType.InvalidParameter, false, "Max effective leverage exceeded", "501"),
            new ErrorInfo(ErrorType.InvalidParameter, false, "Invalid collateral price", "604"),
            new ErrorInfo(ErrorType.InvalidParameter, false, "Max allowed slippage exceeded", "606"),
            new ErrorInfo(ErrorType.InvalidParameter, false, "Bad/invalid request", "40001", "40003"),
            new ErrorInfo(ErrorType.InvalidParameter, false, "Invalid timestamp", "40005"),

            new ErrorInfo(ErrorType.InvalidPrice, false, "Invalid floor price", "228"),
            new ErrorInfo(ErrorType.InvalidPrice, false, "Invalid reference price", "229"),
            new ErrorInfo(ErrorType.InvalidPrice, false, "Invalid price", "308"),
            new ErrorInfo(ErrorType.InvalidPrice, false, "Order price is beyond liquidation price", "310"),
            new ErrorInfo(ErrorType.InvalidPrice, false, "Order price greater than limit up price", "312"),
            new ErrorInfo(ErrorType.InvalidPrice, false, "Order price less than limit down price", "313"),
            new ErrorInfo(ErrorType.InvalidPrice, false, "Limit price too far from current price", "315"),

            new ErrorInfo(ErrorType.InvalidQuantity, false, "Order quantity invalid", "213"),
            new ErrorInfo(ErrorType.InvalidQuantity, false, "Position quantity invalid", "216"),
            new ErrorInfo(ErrorType.InvalidQuantity, false, "Open quantity invalid", "217"),
            new ErrorInfo(ErrorType.InvalidQuantity, false, "Max order quantity exceeded", "314"),
            new ErrorInfo(ErrorType.InvalidQuantity, false, "Less than min order quantity", "415"),

            new ErrorInfo(ErrorType.RejectedOrderConfiguration, false, "Invalid order type", "218"),
            new ErrorInfo(ErrorType.RejectedOrderConfiguration, false, "Invalid execution instruction", "219"),
            new ErrorInfo(ErrorType.RejectedOrderConfiguration, false, "Invalid side", "220"),
            new ErrorInfo(ErrorType.RejectedOrderConfiguration, false, "Invalid timeInForce", "221"),
            new ErrorInfo(ErrorType.RejectedOrderConfiguration, false, "Rejected by matching engine", "224"),
            new ErrorInfo(ErrorType.RejectedOrderConfiguration, false, "FillOrKill order could not be filled immediately", "43003"),
            new ErrorInfo(ErrorType.RejectedOrderConfiguration, false, "ImmediateOrCancel order could not be filled immediately", "43004"),
            new ErrorInfo(ErrorType.RejectedOrderConfiguration, false, "PostOnly order could not be posted as maker", "43005"),
            new ErrorInfo(ErrorType.RejectedOrderConfiguration, false, "Canceled because of Self Trade Prevention", "43012"),

            new ErrorInfo(ErrorType.RiskError, false, "Exceeds account risk limit", "302"),
            new ErrorInfo(ErrorType.RiskError, false, "Exceeds position risk limit", "303"),
            new ErrorInfo(ErrorType.RiskError, false, "Order would lead to immediate liquidation", "304"),
            new ErrorInfo(ErrorType.RiskError, false, "Order would trigger margin call", "305"),

            new ErrorInfo(ErrorType.IncorrectState, false, "Invalid order status", "307"),
            new ErrorInfo(ErrorType.IncorrectState, false, "Position in liquidation", "311"),

            new ErrorInfo(ErrorType.InsufficientBalance, false, "Insufficient balance", "306"),
            new ErrorInfo(ErrorType.InsufficientBalance, false, "Exceeds maximum available balance", "321"),
            new ErrorInfo(ErrorType.InsufficientBalance, false, "Insufficient balance available for withdrawal", "30024"),

            new ErrorInfo(ErrorType.UnknownSymbol, false, "Symbol expired", "206"),
            new ErrorInfo(ErrorType.UnknownSymbol, false, "Invalid symbol", "209"),

            new ErrorInfo(ErrorType.UnknownAsset, false, "Invalid asset", "211"),

            new ErrorInfo(ErrorType.UnknownOrder, false, "Invalid order id", "212"),
            new ErrorInfo(ErrorType.UnknownOrder, false, "No active order", "316"),

            new ErrorInfo(ErrorType.UnavailableSymbol, false, "Symbol not currently trading", "208", "309"),

            new ErrorInfo(ErrorType.InvalidOperation, false, "Account is in margin call", "301"),

            new ErrorInfo(ErrorType.RateLimitOrder, false, "Max amount of orders reached", "319"),

            new ErrorInfo(ErrorType.RateLimitRequest, false, "Too many requests", "42901"),

            ],
            [
                new ErrorEvaluator("40004", (code, msg) =>
                {
                    if (msg?.Equals("Invalid instrument_name") == true)
                        return new ErrorInfo(ErrorType.UnknownSymbol, false, "Invalid symbol name", code);

                    return new ErrorInfo(ErrorType.InvalidParameter, false, "Missing or empty parameter", code);
                })
                ]);
    }
}
