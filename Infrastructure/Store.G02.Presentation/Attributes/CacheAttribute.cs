using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Store.G02.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Presentation.Attributes
{
    //IAsyncActionFilter: Execute it Before The End Point
    public class CacheAttribute(int timeInSec) : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            //Logic
            //Object from CacheService:
            var cacheService =  context.HttpContext.RequestServices.GetRequiredService<IServiceManager>().CacheService;


            //Generate Key
            var cachekey = GetCacheKey(context.HttpContext.Request);

            var result = await cacheService.GetAsync(cachekey);

            if(!string.IsNullOrEmpty(result))
            {
                //return response
                var response = new ContentResult //Type of ActionResult 
                {
                    Content = result,
                    ContentType = "application/json",
                    StatusCode = 200

                };
                context.Result = response;
                return;
            }

            //address of EndPoint function to call it : using next
            var actionContext = await next.Invoke();
            if(actionContext.Result is OkObjectResult okObjectResult)
            {
              await cacheService.SetAsync(cachekey,okObjectResult.Value,TimeSpan.FromSeconds(timeInSec));
            }

        }
        private string GetCacheKey(HttpRequest request)
        {
            var key = new StringBuilder();
            key.Append(request.Path);

            foreach (var item in request.Query)
            {
                key.Append($"|{item.Key}-{item.Value}");
            }

            return key.ToString();

        }

    }
}
