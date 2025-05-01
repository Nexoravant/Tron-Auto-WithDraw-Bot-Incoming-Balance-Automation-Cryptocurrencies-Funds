using Withdraw.Cash.Contracts.DTOs;
using Withdraw.Cash.Domain.Entities;

namespace Withdraw.Cash.Api.Extensions.CurrencyExtensions;

public static class CurrencyExtensions
{
    public static CurrencyDto ToDto(this Currency currency)
    {
        return new CurrencyDto(
            currency.Name,
            currency.Symbol,
            currency.ExchangeRate);
    }
}
