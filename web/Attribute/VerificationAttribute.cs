using BLL.PersonnelManage;
using Model.PersonnelManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Attribute
{
    public class VerificationAttribute: ActionFilterAttribute
    {
        /// <summary>  
        /// OnActionExecuting是Action执行前的操作  
        /// </summary>  
        /// <param name="filterContext"></param>  
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //判断Cookie用户名密码是否存在
            if(filterContext.HttpContext.Session["UserInfo"]==null)
            {
                if (filterContext.HttpContext.Response.Cookies["lims.uid"].Value == null || filterContext.HttpContext.Response.Cookies["lims.passport"].Value == null)
                {
                    filterContext.Result = new RedirectResult("/Login/login");
                }
                else
                {
                    string passportinfo = filterContext.HttpContext.Response.Cookies["lims.passport"].Value;
                    string uid = filterContext.HttpContext.Response.Cookies["lims.uid"].Value;
                    E_tb_InPersonnel eInPersonnel = new T_tb_InPersonnel().GetModel(Convert.ToInt32(uid));
                    if (passportinfo == Comp.Utils.Md5(eInPersonnel.PersonnelID + eInPersonnel.UserName + eInPersonnel.PassWord))
                    {
                        filterContext.HttpContext.Session["UserInfo"] = new T_tb_InPersonnel().Login(eInPersonnel.UserName, eInPersonnel.PassWord);
                    }
                    else
                    {
                        filterContext.Result = new RedirectResult("/Login/login");
                    }
                }
            }
        }




    }
}