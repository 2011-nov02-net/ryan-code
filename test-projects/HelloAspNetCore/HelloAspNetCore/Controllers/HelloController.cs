using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloAspNetCore.Controllers
{
    public class HelloController
    {
        public IActionResult Action1()
        {
            return new ContentResult
            {
                Content = "<html><head></head><body>Hello Action Method1</body></html>",
                ContentType = "text/html",
                StatusCode = StatusCodes.Status200OK
            };
        }

        public IActionResult ParameterAction1(string param1, string param2)
        {
            return new ContentResult
            {
                Content = $"<html><head></head><body>Hello Action Method1 With Params {param1} {param2}</body></html>",
                ContentType = "text/html",
                StatusCode = StatusCodes.Status200OK
            };
        }
        /*
        public IActionResult Redirect1()
        {
            return new RedirectToActionResult("Redirect2", "Hello", null);
            {
             
            };
        }

        public IActionResult Redirect2()
        {
            return new RedirectToActionResult("Redirect1", "Hello", null);
            {

            };
        }
        */
    }
}
