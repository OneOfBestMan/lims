using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    /// <summary>
    /// 实验室管理
    /// </summary>
    public class LabManageController : Controller
    {
        /// <summary>
        /// 实验考核
        /// </summary>
        /// <returns></returns>
        public ActionResult ExperimentCheck()
        {
            return View();
        }

    }
}
