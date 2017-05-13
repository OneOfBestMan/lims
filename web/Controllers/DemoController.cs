using Comp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class DemoController : Controller
    {
        //
        // GET: /Demo/

        public ActionResult Test()
        {
            int pageIndex=Utils.GetInt(Request["page"]);
            ViewBag.page = Utils.ShowPage(100, 15, pageIndex, 5);
            return View();
        }
        public ActionResult Info()
        {
            return View();
        }
        public bool Save(InfoModel model)
        {
            bool b = false;
            return b;
        }
    }
    public class InfoModel {
        public int id { get; set; }
    }
}
