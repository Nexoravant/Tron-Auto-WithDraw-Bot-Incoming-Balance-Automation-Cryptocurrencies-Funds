namespace Withdraw.Cash.Domain.Entities;
public class Currency
{
    public int Id { get; set; }
    public string Name { get; set; }   // UAH
    public string Symbol { get; set; } // ₴
    public decimal ExchangeRate { get; set; }
}
