namespace Withdraw.Cash.Contracts.CashWithdraw.GetStatusByClientId;
public record GetStatusByClientIdRequest(Guid ClientId, string DepartmentAddress);
