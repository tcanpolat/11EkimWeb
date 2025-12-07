using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace _15_Filter_Operations.Filters
{
    public class ActionFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            // Action metodu çalıştıktan sonra yapılacak işlemler
            Debug.WriteLine($"Action Executed. Executed Time: {DateTime.UtcNow.ToString("hh.mm.ss.ffffff")}");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            // Action metodu çalıştığı sırada yapılacak işlemler
            Debug.WriteLine($"Action Executing. Executing Time: {DateTime.UtcNow.ToString("hh.mm.ss.ffffff")}");
        }
    }
}
