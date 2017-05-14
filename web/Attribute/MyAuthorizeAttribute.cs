using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.PersonnelManage;
using BLL.PersonnelManage;
namespace Web.Attribute
{
    /// <summary>
    /// 权限验证过滤器
    /// </summary>
    public class MyAuthorizeAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// 权限验证
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns>true：验证通过，false：验证失败</returns>
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            //string currentRole = (Session["user"] as User).Role;
            ////从Session中获取User对象，然后得到其角色信息。如果用户重写了Identity, 则可以在httpContext.Current.User.Identity中获取
            //if (Roles.Contains(currentRole))
            //    return true;
            //string UserName = httpContext.User.Identity.Name;
            //return true;
            //return base.AuthorizeCore(httpContext);
            return true;//httpContext.Session["UserInfo"] != null;

        }

        /// <summary>
        /// 验证失败时调用
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.HttpContext.Response.Redirect("/Login/Login");
            //base.HandleUnauthorizedRequest(filterContext);
        }
    }
}
