using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Withdraw.Cash.Application.Commands.CreateCashWithdrawRequest;
using Withdraw.Cash.Application.Queries.GetStatusByClientId;
using Withdraw.Cash.Application.Queries.GetStatusByWithdrawRequestId;
using Withdraw.Cash.Contracts.CashWithdraw.CreateCashWithdrawRequest;

namespace Withdraw.Cash.Api.Controllers;

[ApiController]
[Route("api/cash-withdraw")]
public class CashWithdrawController : ControllerBase
{
    private readonly IValidator<CreateCashWithdrawRequestCommand> _validator;
    private readonly ISender _mediator;

    public CashWithdrawController(IValidator<CreateCashWithdrawRequestCommand> validator, ISender mediator)
    {
        _validator = validator;
        _mediator = mediator;
    }

    [HttpPost("create")]
    public  async Task<IResult> CreateCashWithdrawRequest(CreateCashWithdrawRequest request)
    {
        var ip = RetrieveClientIp();
        var command = new CreateCashWithdrawRequestCommand(
                request.ClientId,
                request.DepartmentAddress,
                request.Amount,
                request.Currency,
                ip);
        var validationResult = await _validator.ValidateAsync(command);

        if (!validationResult.IsValid)
        {
            return Results.BadRequest(validationResult.Errors);
        }
        
        var response = await _mediator.Send(command);
        
        return Results.Ok(response);
    }

    [HttpGet("{withdrawRequestId}")]
    public async Task<IResult> GetStatusByWithdrawRequestId([FromQuery] Guid withdrawRequestId)
    {
        // { reuqest id, status, currency, amount }
        var response = await _mediator.Send(new GetStatusByWithdrawRequestIdQuery(withdrawRequestId));

        return Results.Ok(response);
    }
    
    [HttpGet("{clientId}/statuses")]
    public async Task<IResult> GetStatusByClientId([FromQuery] Guid clientId, [FromQuery] string departmentAddress)
    {
        //[{ reuqest id, status, currency, amount }, { reuqest id, status, currency, amount }, { reuqest id, status, currency, amount }]
        var response = await _mediator.Send(new GetStatusByClientIdQuery(clientId, departmentAddress));

        return Results.Ok(response);
    }

    private string RetrieveClientIp()
    {
        var ip = HttpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault();
        if (string.IsNullOrEmpty(ip))
        {
            ip = HttpContext.Connection.RemoteIpAddress?.ToString();
        }
        
        return ip;
    }
}
