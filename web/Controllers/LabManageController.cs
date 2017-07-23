using Model;
using Model.LabManage;
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
    public class LabManageController : BaseController
    {
        /// <summary>
        /// 实验考核
        /// </summary>
        [Route("labmanage/experimentcheck")]
        public ActionResult ExperimentCheck(E_PageParameter ePageParameter)
        {
            List<E_ExperimentCheck> list = new List<E_ExperimentCheck>();
            if (ePageParameter.issearch > 0)
            {
                list = new List<E_ExperimentCheck>();
            }
            ViewBag.ExperimentCheckList = list;
            ViewBag.ePageParameter = ePageParameter;
            return View("/views/labmanage/experimentcheck.cshtml");
        }

    }
}
