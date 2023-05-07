using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Filuet.Hrbl.Ordering.Proxy
{
    public class GenericExceptionAttribute : Attribute, IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception != null)
            {
                if (context.Exception is ArgumentException)
                {
                    context.Result = new ContentResult
                    {
                        StatusCode = 400,
                        Content = context.Exception.Message
                    };
                }
                else if (context.Exception is NullReferenceException)
                    context.Result = new ContentResult { StatusCode = 204 };
                else if (context.Exception is KeyNotFoundException)
                {
                    // From Microsoft guidline https://learn.microsoft.com/en-us/previous-versions/dotnet/netframework-4.0/ms229021(v=vs.100)?redirectedfrom=MSDN
                    // 'Consider throwing existing exceptions residing in the System namespaces instead of creating custom exception types'

                    context.Result = new ContentResult
                    {
                        StatusCode = 404,
                        Content = context.Exception.Message
                    };
                }
                else if (context.Exception is InvalidOperationException)
                {
                    context.Result = new ContentResult
                    {
                        StatusCode = 409,
                        Content = context.Exception.Message
                    };
                }
                else if (context.Exception is UnauthorizedAccessException)
                {
                    context.Result = new ContentResult
                    {
                        StatusCode = 401,
                        Content = context.Exception.Message
                    };
                }
                else if (context.Exception is Exception)
                {
                    context.Result = new ContentResult
                    {
                        StatusCode = 500,
                        Content = context.Exception.Message
                    };
                }

                context.ExceptionHandled = true;
            }
        }
    }
}