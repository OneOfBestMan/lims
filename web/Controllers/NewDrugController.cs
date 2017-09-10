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
    }
}