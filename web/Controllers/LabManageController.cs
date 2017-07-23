using Comp;
using DAL.LabManage;
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
            int pageIndex = Utils.GetInt(Request["page"]);
            ePageParameter.pageindex = pageIndex > 0 ? pageIndex - 1 : pageIndex;
            ePageParameter.pagesize = 20;
            D_ExperimentCheck dExperimentCheck = new D_ExperimentCheck();
            List<E_ExperimentCheck> list = new List<E_ExperimentCheck>();
            int count = 0;
            if (ePageParameter.issearch > 0)
            {
                list = dExperimentCheck.GetExperimentCheckList(ePageParameter, ref count);
            }
            ViewBag.ExperimentCheckList = list;
            ViewBag.ePageParameter = ePageParameter;
            ViewBag.page = Utils.ShowPage(count, ePageParameter.pagesize, pageIndex, 5);
            return View("/views/labmanage/experimentcheck.cshtml");
        }

    }
}
