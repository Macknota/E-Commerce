using Store.G02.Domain.Exceptions.BadRequest;
using Store.G02.Domain.Exceptions.NotFound;
using Store.G02.Domain.Exceptions.Unauthorized;
using Store.G02.Shared.ErrorModels;

namespace Store.G02.Web.Middlewares
{
    public class GlobalErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        //CLR Will Give Me object(next) it's represent the address of the function of the Next Middleware
        public GlobalErrorHandlingMiddleware(RequestDelegate next )
        {
            _next = next;
        }

        //HttpContext object for Request & Response
        public async Task InvokeAsync(HttpContext context)
        {
            //Trains it into next Middleware

            try
            {
                await _next.Invoke(context);
                if (context.Response.StatusCode == 404) // routing middleware
                {
                    context.Response.ContentType = "application/json";
                    var response = new ErrorDetails()
                    {
                        StatusCode = context.Response.StatusCode,
                        ErrorMessage = $"endpoint {context.Request.Path} was not found !!"

                    };
                    await context.Response.WriteAsJsonAsync( response );
                }
                
                
            }
            catch (Exception ex)
            {
                //logic

                //1. Set Status Code of Response
                context.Response.StatusCode = ex switch
                {
                    NotFoundException => StatusCodes.Status404NotFound,
                    BadRequestException => StatusCodes.Status400BadRequest,
                    UnauthorizedException => StatusCodes.Status401Unauthorized,
                    //else
                    _ => StatusCodes.Status500InternalServerError
                };

                //2. Set Content Type value of Response

                context.Response.ContentType = "application/json";

                //3. Set Body of Response

                var response = new ErrorDetails()
                { 
                    StatusCode = context.Response.StatusCode,
                    ErrorMessage = ex.Message
                };

                //return response
                await context.Response.WriteAsJsonAsync(response);


            }


        }



    }
}
