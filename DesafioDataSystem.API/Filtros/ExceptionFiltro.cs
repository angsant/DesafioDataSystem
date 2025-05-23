﻿using DesafioDataSystem.API.Response;
using DesafioDataSystem.Exceptions;
using DesafioDataSystem.Exceptions.ExceptionBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace DesafioDataSystem.API.Filtros
{
    public class ExceptionFiltro : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is DesafioDataSystemException)
            {
                HandleProjectException(context);
            }
            else
            {
                ThrowUnknowException(context);
            }
        }

        private void HandleProjectException(ExceptionContext context)
        {
            if (context.Exception is ErrorOnValidationException)
            {
                var exception = context.Exception as ErrorOnValidationException;

                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Result = new BadRequestObjectResult(new ResponseErrorJson(exception.ErrorMessages));
            }
        }

        private void ThrowUnknowException(ExceptionContext context)
        {
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Result = new ObjectResult(new ResponseErrorJson(ResourceMessagesException.UNKNOWN_ERROR));
        }
    }
}
