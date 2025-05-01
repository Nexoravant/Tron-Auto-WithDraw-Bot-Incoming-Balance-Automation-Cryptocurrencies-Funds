using FluentValidation;

namespace Withdraw.Cash.Application.Commands.CreateCashWithdrawRequest;

public class CreateCashWithdrawRequestValidator : AbstractValidator<CreateCashWithdrawRequestCommand>
{
    public CreateCashWithdrawRequestValidator()
    {
        RuleFor(x => x.Amount)
            .LessThanOrEqualTo(100000)
                .WithMessage("Amount should be less than 100 000")
            .GreaterThanOrEqualTo(100)
                .WithMessage("Amount should be greater than 100");
    }
}
