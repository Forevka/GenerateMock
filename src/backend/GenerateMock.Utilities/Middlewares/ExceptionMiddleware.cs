﻿using GenerateMock.Utilities.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;

namespace GenerateMock.Utilities.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (BaseErrorException ex)
            {
                _logger.LogError(ex, ex.StackTrace);
                await HandleSoftExceptionAsync(httpContext, ex);
            }
            catch (BaseNormalException ex)
            {
                _logger.LogError(ex, ex.StackTrace);
                await HandleUserFriendlyExceptionAsync(httpContext, ex);
            }
            catch (BaseNotFoundException ex)
            {
                _logger.LogError(ex, ex.StackTrace);
                await HandleNotFoundExceptionAsync(httpContext, ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.StackTrace);
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleSoftExceptionAsync(HttpContext context, BaseErrorException exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = 400;

            return context.Response.WriteAsync(exception.ToString());
        }

        private Task HandleUserFriendlyExceptionAsync(HttpContext context, BaseNormalException exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = 204; //no content

            return context.Response.WriteAsync(exception.ToString());
        }

        private Task HandleNotFoundExceptionAsync(HttpContext context, BaseNotFoundException exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = 404; //no content

            return context.Response.WriteAsync(exception.ToString());
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            return context.Response.WriteAsync(exception.ToString());
        }
    }
}