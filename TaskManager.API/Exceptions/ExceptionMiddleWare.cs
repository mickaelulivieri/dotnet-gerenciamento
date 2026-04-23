using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System;

namespace TaskManager.API.Exceptions
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
                // Deixa a requisição seguir o fluxo normal
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                // Se der erro em qualquer lugar (Service ou Repo), cai aqui
                _logger.LogError($"Erro capturado pelo Middleware: {ex}");
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            // Define valores padrão (Erro 500)
            var statusCode = (int)HttpStatusCode.InternalServerError;
            var message = "Ocorreu um erro interno no servidor.";

            // Mapeia suas exceções customizadas para os códigos HTTP certos
            if (exception is NotFoundException)
            {
                statusCode = (int)HttpStatusCode.NotFound;
                message = exception.Message;
            }
            else if (exception is BadRequestException)
            {
                statusCode = (int)HttpStatusCode.BadRequest;
                message = exception.Message;
            }

            context.Response.StatusCode = statusCode;

            // Cria o objeto de erro que o React vai ler
            var response = new ErrorResponse
            {
                StatusCode = statusCode,
                Message = message
            };

            // Transforma em JSON e envia para o navegador usando o ToString() que você criou
            return context.Response.WriteAsync(response.ToString());
        }
    }
}