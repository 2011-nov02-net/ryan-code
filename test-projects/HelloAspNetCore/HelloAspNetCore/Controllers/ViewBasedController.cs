using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloAspNetCore.Controllers
{
    public class ViewBasedController : Controller
    {
        public IActionResult HomePage()
        {
            //usually return viewresults with the view helper from the controiller base class
            return View("home");
        }

        public IActionResult ViewWithLayout1()
        {
            //views can have a layout
            //a layout is a special kind of view that cant stand on its own and at some point it calls @RenderBody()
            // and that is where the actual view will go
            return View("ViewWithLayout1");
        }

        public IActionResult ViewWithLayout2()
        {
            return View();
        }
    }
}
