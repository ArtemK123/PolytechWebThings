using System;
using System.Collections.Generic;
using System.Net;
using Domain.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Web.Models.OperationResults;

namespace Web.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("/internal/error")]
    public class ExceptionHandlingController : ControllerBase
    {
        private readonly ILogger logger;

        public ExceptionHandlingController(ILoggerFactory loggerFactory)
        {
            logger = loggerFactory.CreateLogger(nameof(ExceptionHandlingController));
        }

        private IReadOnlyDictionary<Type, Func<Exception, OperationResult>> ExceptionHandlersMapping => new Dictionary<Type, Func<Exception, OperationResult>>
        {
            { typeof(ValidationException), exception => new OperationResult(OperationStatus.Error, exception.Message) },
            { typeof(EmailTakenByOtherUserException), exception => new OperationResult(OperationStatus.Error, exception.Message) },
            { typeof(UserNotFoundByEmailException), exception => new OperationResult(OperationStatus.Error, exception.Message) },
            { typeof(WrongUserPasswordException), exception => new OperationResult(OperationStatus.Error, exception.Message) },
            { typeof(CanNotConnectToGatewayException), exception => new OperationResult(OperationStatus.Error, exception.Message) },
            { typeof(GatewayAlreadyRegisteredException), exception => new OperationResult(OperationStatus.Error, exception.Message) },
            { typeof(WorkspaceNotFoundByIdException), exception => new OperationResult(OperationStatus.Error, exception.Message) },
            { typeof(UserDoesNotHaveRequiredRightsException), exception => new OperationResult(OperationStatus.Forbidden, exception.Message) },
            {
                typeof(BrokenGatewayCommunicationException), exception =>
                {
                    if (exception.InnerException is not null)
                    {
                        logger.LogInformation($"Inner exception in gateway communication {exception.InnerException?.Message}");
                    }

                    return new OperationResult(OperationStatus.Error, exception.Message);
                }
            },
        };

        public IActionResult Error()
        {
            IExceptionHandlerFeature context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            Exception? exception = context.Error;

            if (exception == null)
            {
                logger.LogError("Executed without exception. Probably, direct request to /error page");
                return new NotFoundResult();
            }

            Type exceptionType = exception.GetType();
            string exceptionLogMessage = $"{exceptionType.Name}: {exception.Message}";

            if (ExceptionHandlersMapping.TryGetValue(exceptionType, out Func<Exception, OperationResult>? handler))
            {
                logger.LogInformation($"{exceptionLogMessage} - Exception is handled by an exception handler");
                return new OkObjectResult(handler(exception));
            }

            logger.LogError($"{exceptionLogMessage} - Exception handler is not found" + Environment.NewLine + exception.StackTrace);
            return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
        }
    }
}