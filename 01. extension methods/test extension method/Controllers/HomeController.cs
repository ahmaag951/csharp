using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace test_extension_method.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(){
        
            var debug = "qwer".AddABC();
            return View();
        }

    }

    public static class StringHandler {

        public static string AddABC(this string source) {
            return source + "abc";
        }
    }
}
