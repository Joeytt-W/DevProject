using Microsoft.AspNetCore.Mvc.Filters;

namespace Dev.CoreWeb.Filters
{
    public class CustomExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}
