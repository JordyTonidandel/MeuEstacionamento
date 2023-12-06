using MeuEstacionamento.Api.ViewModel;
using MeuEstacionamento.Core.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace MeuEstacionamento.Api.Configuration
{
    public static class ExceptionHandlerConfiguration
    {
        public static void ConfigureExceptionHandler(this WebApplication app)
        {
            app.UseExceptionHandler("/error");
            app.Map("/error", (HttpContext http) =>
            {
                var error = http.Features?.Get<IExceptionHandlerFeature>()?.Error;

                if (error != null)
                {
                    var response = new ResultViewModel(false, error.Message, null);

                    if (error is DomainException)
                    {
                        http.Response.StatusCode = 400;
                    }
                    else
                        http.Response.StatusCode = 500;

                    http.Response.WriteAsJsonAsync(response);
                }
            });
        }
    }
}
