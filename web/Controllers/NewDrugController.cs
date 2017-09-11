using Comp;
using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class NewDrugController : BaseController
    {
        D_Drug dDrug = new D_Drug();
        D_DrugLock dDrugLock = new D_DrugLock();
        // GET: NewDrug
        public ActionResult DrugCheckList(E_DrugCheck model)
        {
            model.PageIndex = Utils.GetInt(Request["page"]);
            StringBuilder sbWhere = new StringBuilder();

            int count = 0;
            ViewBag.list = dDrug.GetList(model, ref count);
            ViewBag.page = Utils.ShowPage(count, model.PageSize, model.PageIndex, 5);
            return View();
          
        }

        /// <summary>
        /// 详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DrugLockInfo(int id)
        {
            ViewBag.Info = dDrugLock.GetInfoById(new E_DrugLock(){ id=id }); 
            
            return View();
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool DrugLockSave(E_DrugLock model)
        {
            model.updateDate = DateTime.Now;
            if (model != null && model.id > 0)
            {
                model.updateUser = CurrentUserInfo.PersonnelID;
                return dDrugLock.Update(model);
            }
            model.createUser = CurrentUserInfo.PersonnelID;
            model.createDate = DateTime.Now;
            return dDrugLock.Add(model);
        }
    }
}