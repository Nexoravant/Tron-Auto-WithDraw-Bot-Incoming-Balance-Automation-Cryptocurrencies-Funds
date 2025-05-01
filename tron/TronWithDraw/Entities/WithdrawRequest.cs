namespace Withdraw.Cash.Domain.Entities;
public class WithdrawRequest
{
    public Guid Id { get; set; }
    public DateTime? CreatedAt { get; set; }
    
    public string ClientId { get; set; }
    public Client Client { get; set; }
    
    public int StatusId { get; set; }
    public WithdrawStatus Status { get; set; }

    public Guid CurrencyId { get; set; }
    public Currency Currency { get; set; }
}
