using Comp;
using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class TaskController : BaseController
    {
        D_Task dal = new D_Task();
        // GET: Task
        public ActionResult List(E_Task model)
        {
            model.PageIndex = Utils.GetInt(Request["page"]);
            int count = 0;
            ViewBag.list = dal.GetList(model, ref count);
            ViewBag.page = Utils.ShowPage(count, model.PageSize, model.PageIndex, 5);
            return View();
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Save(E_Task model)
        {
            model.publishtime = DateTime.Now;
            model.publishid= CurrentUserInfo.PersonnelID;
            if (model != null && model.id > 0)
            {
                return dal.Update(model);
            }
            return dal.Add(model);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Delete(E_Task model)
        {

            return dal.DeleteById(model);


        }
 
        /// <summary>
        /// 详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Info(int id)
        {

            E_Task model = new E_Task();
            model.id = id;
            ViewBag.Info = dal.GetInfoById(model);

            return View();
        }
    }
}