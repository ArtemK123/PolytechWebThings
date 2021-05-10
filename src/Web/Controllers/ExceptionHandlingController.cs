﻿using System;
using System.Collections.Generic;
using System.Net;
using Domain.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Web.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("/internal/error")]
    public class ExceptionHandlingController : ControllerBase
    {
        private readonly ILogger logger;

        private readonly IReadOnlyDictionary<Type, Func<Exception, IActionResult>> exceptionHandlersMapping = new Dictionary<Type, Func<Exception, IActionResult>>
        {
            { typeof(ValidationException), exception => new BadRequestObjectResult(exception.Message) },
            { typeof(EmailTakenByOtherUserException), exception => new BadRequestObjectResult(exception.Message) },
            { typeof(UserNotFoundByEmailException), exception => new BadRequestObjectResult(exception.Message) },
            { typeof(WrongUserPasswordException), exception => new BadRequestObjectResult(exception.Message) },
            { typeof(CanNotConnectToGatewayException), exception => new BadRequestObjectResult(exception.Message) },
            { typeof(GatewayAlreadyRegisteredException), exception => new BadRequestObjectResult(exception.Message) },
            { typeof(UserDoesNotHaveRequiredRightsException), exception => new ObjectResult(exception.Message) { StatusCode = (int)HttpStatusCode.Forbidden } },
            { typeof(WorkspaceNotFoundByIdException), exception => new BadRequestObjectResult(exception.Message) },
        };

        public ExceptionHandlingController(ILoggerFactory loggerFactory)
        {
            logger = loggerFactory.CreateLogger(nameof(ExceptionHandlingController));
        }

        public IActionResult Error()
        {
            IExceptionHandlerFeature context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            Exception? exception = context.Error;

            if (exception == null)
            {
                logger.LogError("Executed without exception. Probably, direct request to /error page");
                return new RedirectToRouteResult("~/");
            }

            Type exceptionType = exception.GetType();
            string exceptionLogMessage = $"{exceptionType.Name}: {exception.Message}";

            if (exceptionHandlersMapping.TryGetValue(exceptionType, out Func<Exception, IActionResult>? handler))
            {
                logger.LogInformation($"{exceptionLogMessage} - Exception is handled by an exception handler");
                return handler(exception);
            }

            logger.LogError($"{exceptionLogMessage} - Exception handler is not found" + Environment.NewLine + exception.StackTrace);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}