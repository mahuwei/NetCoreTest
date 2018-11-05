using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace Project.Web2.Models.MediatRTest {
    public class RequestLogger<TRequest> : IRequestPreProcessor<TRequest> {
        private readonly ILogger _logger;

        public RequestLogger(ILogger<TRequest> logger) {
            _logger = logger;
        }

        public Task Process(TRequest request, CancellationToken cancellationToken) {
            var name = typeof(TRequest).Name;

            // TODO: Add User Details

            _logger.LogInformation("Northwind Request: {Name} {@Request}", name,
                request);

            return Task.CompletedTask;
        }
    }

    public class
        ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest,
            TResponse>
        where TRequest : IRequest<TResponse> {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators) {
            _validators = validators;
        }

        public Task<TResponse> Handle(TRequest request,
            CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next) {
            var context = new ValidationContext(request);
            var failures = _validators
                .Select(v => v.Validate(context))
                .SelectMany(result => result.Errors)
                .Where(f => f != null)
                .ToList();

            if (failures.Count != 0) throw new ValidationException(failures);

            return next();
        }
    }
}