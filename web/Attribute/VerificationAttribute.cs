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
                filterContext.Result = new RedirectResult("/Login/login");
            }
        }




    }
}