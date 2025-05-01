namespace Withdraw.Cash.Domain.Entities;
public class WithdrawStatus
{
    public int Id { get; set; }
    public string Status { get; set; } // i.g. Pending | Failed | Accepted | Ready | Finished 
}
