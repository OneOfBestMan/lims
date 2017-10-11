using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.ExpePlan;
using Model.ExpePlan;
using System.Data;
using Common;
using BLL.Laboratory;
using BLL.PersonnelManage;
using System.Collections;
using BLL.EntrustManage;
using Model.EntrustManage;
using BLL.RoleManage;
using BLL;
using Model.Laboratory;
using DAL.Sample;
using Model;
using System.Text;
using Comp;
using DAL.PersonnelManage;
using Model.PersonnelManage;

namespace Web.Controllers
{
    public class ExpePlanController : BaseController
    {
        T_tb_ExpePlan tExpePlan = new T_tb_ExpePlan(); //实验室管理
        T_tb_Project tProject = new T_tb_Project();
        T_tb_InPersonnel tInPersonnel = new T_tb_InPersonnel();
        T_tb_EntrustTesting tEntrustTesting = new T_tb_EntrustTesting(); //委托检验
        tb_SampleBLL tSample = new tb_SampleBLL(); //样品管理
        D_Sample _dSample = new D_Sample();
        T_tb_Area tArea = new T_tb_Area();
        D_tb_InPersonnel dInPersonnel = new D_tb_InPersonnel();//人员管理
        //
        // GET: /DetectProject/

        public ActionResult ExpePlanList(E_PageParameter ePageParameter)
        {
            int pageIndex = Utils.GetInt(Request["page"]);
            ePageParameter.pageindex = pageIndex > 0 ? pageIndex - 1 : pageIndex;
            ePageParameter.pagesize = 20;

            ViewBag.ExpePlanList = this.GetList(ePageParameter);
            ViewBag.PersonnelList = dInPersonnel.GetList(new E_tb_InPersonnel() { });
            
            ViewBag.ePageParameter = ePageParameter;
            ViewBag.page = Utils.ShowPage(ePageParameter.count, ePageParameter.pagesize, pageIndex, 5);

            ViewData["PlanTypeList"] = this.GetPlanTypeList(true);
            ViewData["ProjectList"] = PageTools.GetSelectList(tProject.GetList("").Tables[0], "ProjectID", "ProjectName", true);
            ViewData["AreaList"] = PageTools.GetSelectList(tArea.GetList("").Tables[0], "AreaID", "AreaName", true);

            return View("/views/ExpePlan/ExpePlanList.cshtml");

            //if (Request["ApprovalPersonnelName"] != null)
            //    ViewData["ApprovalPersonnelName"] = Request["ApprovalPersonnelName"].ToString();
            //else
            //    ViewData["ApprovalPersonnelName"] = "";
            //ViewBag._userName = CurrentUserInfo.UserName;
            //eExpePlan.AreaID = CurrentUserInfo.AreaID;
            //ViewBag.IsDisabled = (CurrentUserInfo.RoleID != 1) ? "true" : "false"; //权限判断
        }

        /// <summary>
        /// 获取所有数据列表
        /// 作者：小朱
        /// </summary>
        /// <returns>将DataTable转换为Json数据格式通过string类型返回</returns>
        public DataTable GetList(E_PageParameter ePageParameter)
        {
            StringBuilder strWhere = new StringBuilder();
            if (ePageParameter.planid>0)
            {
                strWhere.AddWhere("T.PlanID=" + ePageParameter.planid + " ");
            }
            if (ePageParameter.areaid>0)//区域
            {
                strWhere.AddWhere("T.AreaID=" + ePageParameter.areaid + " ");
            }
            if (ePageParameter.plantype>-1)//计划类别
            {
                strWhere.AddWhere("T.PlanTypeID=" + ePageParameter.plantype + " ");
            }
            if (ePageParameter.starttime!=null)//检验开始时间
            {
                strWhere.AddWhere("InspectTime>=cast('" + ePageParameter.starttime.ToString() + "' as datetime) ");
            }
            if (ePageParameter.endtime!=null)//送检结束时间
            {
                strWhere.AddWhere("InspectTime<=cast('" + ePageParameter.endtime.ToString() + "' as datetime) ");
            }
            if (!string.IsNullOrEmpty(ePageParameter.taskno))//任务单号
            {
                strWhere.AddWhere("T.TaskNo like '%" + ePageParameter.taskno.Trim() + "%' ");
            }
            if (ePageParameter.projectid>0)//检验项目
            {
                strWhere.AddWhere("T.ProjectID=" + ePageParameter.projectid + " ");
            }
            if (!string.IsNullOrEmpty(ePageParameter.samplename))//样品名称
            {
                strWhere.AddWhere("C.name like '%" + ePageParameter.samplename + "%' ");
            }
            if (ePageParameter.status>0)//样品名称
            {
                strWhere.AddWhere("T.Status =" + ePageParameter.status);
            }
            if (ePageParameter.headpersonnelid > 0) //实验计划主要负责人
            {
                strWhere.AddWhere("T.headpersonnelid=" + ePageParameter.headpersonnelid);
            }
            //添加数据权限判断
            switch (CurrentUserInfo.DataRange)
            {
                case 2://区域
                    strWhere.AddWhere("T.AreaID=" + CurrentUserInfo.AreaID + " ");
                    break;
                case 3://个人
                    strWhere.AddWhere("T.EditPersonnelID=" + CurrentUserInfo.PersonnelID + " ");
                    break;
            }

            DataTable dt = new DataTable();
            int total = 0;
            int startindex = ePageParameter.pageindex * ePageParameter.pagesize + 1;
            int endindex = (ePageParameter.pageindex + 1) * ePageParameter.pagesize;
            dt = tExpePlan.GetListByPage(strWhere.ToString(), "InspectTime desc", startindex, endindex, ref total).Tables[0];
            ePageParameter.count = total;
            return dt;
        }

        /// <summary>
        /// 显示详情页
        /// </summary>
        /// <param name="EditType">编辑类型</param>
        /// <returns>返回编辑结果</returns>
        public ActionResult ExpePlanEdit(E_tb_ExpePlan eExpePlan, string EditType, int? InfoID)
        {
            ViewData["PlanTypeList"] = this.GetPlanTypeList(false);
            ViewData["ProjectList"] = PageTools.GetSelectList(tProject.GetList("").Tables[0], "ProjectID", "ProjectName", false);
            ViewData["PersonnelList"] = PageTools.GetSelectList(tInPersonnel.GetList(" AreaID=" + CurrentUserInfo.AreaID.ToString()).Tables[0], "PersonnelID", "PersonnelName", false);

            //ViewData["SampleList"] = PageTools.GetSelectList(tSample.GetList(" (handleUser='' or handleUser is null) order by id ").Tables[0], "id", "name", false);
            ViewBag.PersonnelID = CurrentUserInfo.PersonnelID;
            ViewBag.AreaAddr = tArea.GetModel(int.Parse(CurrentUserInfo.AreaID.ToString())).AreaName;
            ViewBag.SampleID = 0;
            ViewBag.ProjectID = 0;
            if (EditType == "Edit")
            {
                eExpePlan = tExpePlan.GetModel(Convert.ToInt32(InfoID));
                ViewBag.SampleID = eExpePlan.SampleID;
                ViewBag.ProjectID = eExpePlan.ProjectID;
            }

            //默认获取前200条数据，避免因option过多导致加载过慢
            List<tb_Sample> SampleList = _dSample.GetModelList(400, "id,name", " where handleUser=0", eExpePlan.SampleID != null ? Convert.ToInt32(eExpePlan.SampleID) : 0);

            ViewData["SampleList"] = SampleList;

            eExpePlan.EditType = EditType;
            return View(eExpePlan);
        }

        /// <summary>
        /// 保存实验室信息
        /// 作者：小朱
        /// </summary>
        /// <param name="eExpePlan">要处理的对象</param>
        /// <returns>返回是否处理成功</returns>
        public JsonResult Save(E_tb_ExpePlan eExpePlan)
        {
            string msg = "";
            bool result = false;
            eExpePlan.UpdateTime = DateTime.Now;
            eExpePlan.EditPersonnelID = CurrentUserInfo.PersonnelID;
            eExpePlan.AreaID = CurrentUserInfo.AreaID;
            if (eExpePlan.EditType == "Add")
            {
                eExpePlan.Status = 2;//2：未完成
                if (tExpePlan.IsExistsTaskNo(eExpePlan.TaskNo) > 0)
                {
                    msg = "任务单号已存在！";
                }
                else
                {
                    tExpePlan.Add(eExpePlan);
                    msg = "添加成功";
                    result = true;
                }
            }
            else
            {
                tExpePlan.Update(eExpePlan);
                msg = "修改成功";
                result = true;
            }
            return Json(new { result = result, msg = msg });
        }

        /// <summary>
        /// 添加实验计划
        /// </summary>
        /// <param name="eExpePlan"></param>
        /// <returns></returns>
        [Route("expeplan/addexpeplan")]
        public JsonResult AddExpePlan(E_tb_ExpePlan eExpePlan)
        {
            E_tb_Project eProject = tProject.GetModel((int)eExpePlan.ProjectID);

            eExpePlan.InspectMethod = eProject.ExpeMethod;
            eExpePlan.PlanTypeID = 1;//默认为计划内
            eExpePlan.HeadPersonnelID = CurrentUserInfo.PersonnelID;
            eExpePlan.TaskNo = CreateTaskNoList(eExpePlan.ProjectID.ToString(), eExpePlan.SampleID.ToString());
            eExpePlan.Status = 2;
            eExpePlan.AreaID = CurrentUserInfo.AreaID;
            eExpePlan.EditPersonnelID = CurrentUserInfo.PersonnelID;
            eExpePlan.UpdateTime = DateTime.Now;
            eExpePlan.InspectPlace = tArea.GetModel((int)CurrentUserInfo.AreaID).AreaName;

            string str = "";
            int ExpePlanID = 0;
            if (tExpePlan.IsExistsTaskNo(eExpePlan.TaskNo) > 0)
            {
                str = "任务单号重复！";
            }
            else
            {
                ExpePlanID = tExpePlan.Add(eExpePlan);
                str = ExpePlanID > 0 ? "OK" : "添加失败！";
            }
            return Json(new { msg = str, id = ExpePlanID }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 删除实验室信息
        /// 作者：小朱
        /// </summary>
        /// <param name="InfoID">要删除的实验室</param>
        /// <returns>返回是否删除成功</returns>
        public JsonResult Delete(int id)
        {
            string str = (tExpePlan.Delete(id)) ? "删除成功！" : "删除失败！";
            return Json(str, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 是否有效下拉框数据
        /// </summary>
        /// <returns></returns>
        private SelectList GetPlanTypeList(bool IsSearch)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            if (IsSearch)
            {
                list.Add(new SelectListItem() { Text = "请选择", Value = "-1", Selected = true });
            }
            list.Add(new SelectListItem() { Text = "计划外", Value = "2" });
            list.Add(new SelectListItem() { Text = "计划内", Value = "1" });
            list.First().Selected = true;
            return new SelectList(list, "Value", "Text");
        }

        /// <summary>
        /// 获取该项目下的所有任务单号
        /// </summary>
        /// <param name="ProjectID">项目ID</param>
        /// <returns></returns>
        public JsonResult GetTaskNoList(string ProjectID, string SampleID)
        {
            List<E_tb_EntrustTesting> list = tEntrustTesting.GetModelList("ProjectID=" + ProjectID + " and SampleID=" + SampleID);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 根据计划类型获取对应的样品列表
        /// </summary>
        /// <param name="PlanTypeID">计划类型（1：计划内、2：计划外）</param>
        /// <returns></returns>
        public JsonResult GetSampleList(string PlanTypeID, int SampleID)
        {
            List<tb_Sample> list = new List<tb_Sample>();
            if (PlanTypeID == "2")
            {
                //读取计划外的 委托检验 未销毁 未完成的委托检验对应的样品
                list = _dSample.GetModelList(400, "id,name", "where handleUser=0 and id in (select SampleID from tb_EntrustTesting where IsComplete=0)", SampleID);
            }
            else
            {
                //获取全部未销毁样品
                list = _dSample.GetModelList(400, "id,name", "where handleUser=0", SampleID);
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        protected override JsonResult Json(object data, string contentType, System.Text.Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return new JsonResult()
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = behavior,
                MaxJsonLength = Int32.MaxValue
            };
        }


        /// <summary>
        /// 根据计划类型以及对应的样品 获取对应的委托检验里面对应的项目
        /// </summary>
        /// <param name="PlanTypeID"></param>
        /// <param name="SampleID"></param>
        /// <returns></returns>
        public JsonResult GetProjectList(string PlanTypeID, string SampleID)
        {
            List<E_tb_Project> list = new List<E_tb_Project>();
            if (PlanTypeID == "2" && !string.IsNullOrEmpty(SampleID))
            {
                //读取计划外的 委托检验 未销毁 未完成的委托检验对应的 检验项目
                list = tProject.GetModelList("ProjectID in (select ProjectID from tb_EntrustTesting where IsComplete=0 and SampleID=" + SampleID + ")");
            }
            else if (PlanTypeID == "1")
            {
                //获取全部未销毁样品
                list = tProject.GetModelList("");
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取对应委托检验对应的负责人
        /// </summary>
        /// <param name="ProjectID"></param>
        /// <param name="SampleID"></param>
        /// <returns></returns>
        public string GetEntrustTestingPersonnelID(string ProjectID, string SampleID)
        {
            if (!string.IsNullOrEmpty(ProjectID) && !string.IsNullOrEmpty(SampleID))
            {
                List<E_tb_EntrustTesting> list = tEntrustTesting.GetModelList(string.Format("IsComplete=0 and ProjectID={0} and SampleID={1}", ProjectID, SampleID));
                if (list != null && list.Count > 0)
                {
                    return list.First().TestPersonnelID.ToString();
                }
            }
            return "0";

        }
        /// <summary>
        /// 获取负责人对应的检验地点
        /// </summary>
        /// <returns></returns>
        public string GetAreaNameByPersonnelID(string PID)
        {
            if (!string.IsNullOrEmpty(PID))
            {
                return tInPersonnel.GetAreaNameByPersonId(PID);
            }
            return "0";

        }

        /// <summary>
        /// 生成计划内任务单号
        /// </summary>
        /// <param name="ProjectID">项目ID</param>
        /// <param name="SampleID">样品ID</param>
        /// <returns></returns>
        public string CreateTaskNoList(string ProjectID, string SampleID)
        {
            return "JN" + DateTime.Now.ToString("yyyyMMdd") + "0" + SampleID + "0" + ProjectID;
        }

        /// <summary>
        /// 根据项目ID 获取对应的检验标准
        /// </summary>
        /// <param name="ProjectID"></param>
        /// <returns></returns>
        public string GetExpeMethod(string ProjectID)
        {
            E_tb_Project eProject = tProject.GetModel(int.Parse(ProjectID));
            return (eProject != null) ? eProject.ExpeMethod : "";
        }
    }
}
