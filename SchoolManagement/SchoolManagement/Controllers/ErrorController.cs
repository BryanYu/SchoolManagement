using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.Logging;

namespace SchoolManagement.Controllers
{
    public class ErrorController : Controller
    {
        private readonly ILogger<ErrorController> _logger;

        public ErrorController(ILogger<ErrorController> logger)
        {
            _logger = logger;
        }

        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            var statusCodeResult = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();

            switch (statusCode)
            {
                case 404:
                    this._logger.LogWarning(
                        $"發生了404錯誤. 路徑:{statusCodeResult.OriginalPath}, 查詢字串:{statusCodeResult.OriginalQueryString}");

                    ViewBag.ErrorMessage = "抱歉 該頁面不存在";
                    break;
                    
            }

            return View("NotFound");
            
        }

        [Route("Error")]
        public IActionResult Error()
        {
            var exceptionHandlerPathFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            this._logger.LogError($"路徑{exceptionHandlerPathFeature.Path} 產生了一個錯誤 {exceptionHandlerPathFeature.Error}");
            return View("Error");
        }
    }
}
