using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Project_GradeBook_Web.Filters
{
    public class IdNotFoundException(string message) : Exception(message)
    {
    }
    public class StudentNoAddressException(string message) : Exception(message)
    {
    }
    public class NoMarksFoundException(string message) : Exception(message)
    {
    }

    public class CustomExceptionFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception is IdNotFoundException)
            {
                context.Result = new ObjectResult(context.Exception.Message)
                {
                    StatusCode = StatusCodes.Status404NotFound
                };

                context.ExceptionHandled = true;
            }
            else if (context.Exception is StudentNoAddressException)
            {
                context.Result = new ObjectResult(context.Exception.Message)
                {
                    StatusCode = StatusCodes.Status404NotFound
                };

                context.ExceptionHandled = true;
            }
            else if (context.Exception is NoMarksFoundException)
            {
                context.Result = new ObjectResult(context.Exception.Message)
                {
                    StatusCode = StatusCodes.Status404NotFound
                };
                context.ExceptionHandled = true;
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
        }
    }
}
