using FluentValidation;
using MediatR;
using Withdraw.Cash.Application.Exceptions;

namespace Withdraw.Cash.Application.Abstractions.Behaviors;
public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var context = new ValidationContext<TRequest>(request);

        var tasks = _validators.Select(validator => validator.ValidateAsync(context));

        var results = await Task.WhenAll(tasks);

        var errors = results.Where(x => !x.IsValid)
            .SelectMany(x => x.Errors)
            .Select(x => new ValidationError(x.PropertyName,x.ErrorMessage))
            .ToArray();

        if (errors.Length != 0)
        {
            throw new Exceptions.ValidationException(errors);
        }

        var response = await next();

        return response;
    }
}
