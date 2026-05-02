using FluentValidation;
using IdentityService.application.Common.Dtos;
using IdentityService.application.Common.Dtos.Constants;
using MediatR;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace IdentityService.application.Common.PipleLineValidation
{
    public class PipelineValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
         where TRequest : IRequest<TResponse>
         where TResponse : IRequestResponse, new()
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        public PipelineValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);

                var validationResults = await Task.WhenAll(
                    _validators.Select(v => v.ValidateAsync(context, cancellationToken)));

                var failures = validationResults
                    .SelectMany(result => result.Errors)
                    .Where(f => f != null)
                    .ToList();

                if (failures.Count != 0)
                {
                    //var errorMessages = failures.Select(x => x.ErrorMessage).ToList();
                    //var response = new RequestResponse<string>
                    //{
                    //    Errors = errorMessages
                    //};
                    //return (dynamic)response;

                    var response = new TResponse();
                    response.Errors = failures.Select(x => x.ErrorMessage).ToList();
                    response.ResponseMessage = ResponseMessages.GetValidationMessage();
                    response.ResponseCode = (int)HttpStatusCode.BadRequest;
                    return response;
                }
            }

            return await next();
        }
    }
}
