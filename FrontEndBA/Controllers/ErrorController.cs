using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace FrontEndBA.Controllers
{
    [Route("error")]
    public class ErrorController : Controller
    {

        // Inspiration https://joonasw.net/view/custom-error-pages
        private readonly TelemetryClient _telemetryClient;

        public ErrorController(TelemetryClient telemetryClient)
        {
            _telemetryClient = telemetryClient;
        }
        [Route("500")]
        public IActionResult AppError()
        {
            //Exception is logged in Application 
            //This will ease maintance of the website. 
            var exceptionHandlerPathFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            _telemetryClient.TrackException(exceptionHandlerPathFeature.Error);
            //The orginal URL is preserved in the browser. This will enable the user to press F5 to retry. 
            _telemetryClient.TrackEvent("Error.ServerError", new Dictionary<string, string>
            {
                ["originalPath"] = exceptionHandlerPathFeature.Path,
                ["error"] = exceptionHandlerPathFeature.Error.Message
            });
            return View();
        }

        [Route("404")]
        public IActionResult PageNotFound()
        {
            //Redirect when a 404 happens. 
            string originalPath = "unknown";
            if (HttpContext.Items.ContainsKey("originalPath"))
            {
                originalPath = HttpContext.Items["originalPath"] as string;
            }
            _telemetryClient.TrackEvent("Error.PageNotFound", new Dictionary<string, string>
            {
                ["originalPath"] = originalPath
            });
            return View();
        }
    }
}