# AI-Friendly Examples

These examples are optimized for AI coding assistants and quick onboarding. Each file is:

- **Compilable** - drop into a console project with `dotnet add package CryptoCom.Net` and it builds.
- **Self-contained** - single file, no external setup, no shared helpers.
- **Heavily commented** - explains why each line matters, not just what it does.
- **Idiomatic** - follows current CryptoCom.Net 3.x patterns.

## Files

| File | What it shows |
|---|---|
| `01-exchange-quickstart.cs` | Client setup, public ticker, symbols, authenticated balance, place limit order, query order status |
| `02-derivatives.cs` | Derivatives/perpetuals: positions, isolated margin order parameters, close position |
| `03-websocket.cs` | Subscribe to ticker, klines, trades, authenticated order and balance streams with proper teardown |
| `04-multi-exchange.cs` | `CryptoExchange.Net.SharedApis` pattern for exchange-agnostic code |
| `05-error-handling.cs` | `WebCallResult` patterns, retry, symbol precision, batch-order nested result checks |

## Running

```bash
dotnet new console -n MyCryptoComApp
cd MyCryptoComApp
dotnet add package CryptoCom.Net
# Copy the example .cs file content into Program.cs
# Replace API_KEY / API_SECRET placeholders with your own for private endpoints
dotnet run
```
