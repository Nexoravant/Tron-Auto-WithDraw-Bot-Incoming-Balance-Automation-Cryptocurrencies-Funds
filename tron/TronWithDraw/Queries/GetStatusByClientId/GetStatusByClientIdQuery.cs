using MediatR;
using Withdraw.Cash.Contracts.CashWithdraw.GetStatusByClientId;

namespace Withdraw.Cash.Application.Queries.GetStatusByClientId;

public record GetStatusByClientIdQuery(Guid ClientId, string DepartmentAddress) : IRequest<GetStatusByClientIdResponse>;
