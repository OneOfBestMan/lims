using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using BLL.Laboratory;
using Model.Laboratory;
using Newtonsoft.Json;
using System.IO;
using System.Text;
using Comp;
using DAL.Laboratory;
using Model.ExpeStatistics;
using DAL.RoleManage;
using DAL.ClientManage;
using DAL.Statistics;
using Model.Statistics;
using BLL.RoleManage;
using Model.RoleManage;

namespace Web.Controllers
{
    public class ExpeStatisticsController : BaseController
    {
        T_tb_Laboratory tLaboratory = new T_tb_Laboratory(); //实验室操作
        T_tb_DetectProject tDetectProject = new T_tb_DetectProject();//实验项目操作
        D_tb_DetectProject dDetectProject = new D_tb_DetectProject();
        D_Statistics dStatistics = new D_Statistics(); //统计
        D_tb_Area dArea = new D_tb_Area(); //区域

        //
        // GET: /ExpeStatistics/

        public ActionResult ExpeStatisticsList()
        {
            ViewData["ddl_company"] = PublicClass.GetAreaList("请选择", CurrentUserInfo.AreaID.Value);
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem() { Text = "请选择", Value = "-1", Selected = true });
            string strWhere = "";
            if (CurrentUserInfo.DataRange != 1)
            {
                strWhere = "AreaID=" + CurrentUserInfo.AreaID;
            }
            List<E_tb_Laboratory> listmd = tLaboratory.GetModelList(strWhere);
            foreach (E_tb_Laboratory item in listmd)
            {
                list.Add(new SelectListItem() { Text = item.LaboratoryName, Value = item.LaboratoryID.ToString() });
            }
            ViewData["LaboratoryList"] = new SelectList(list, "Value", "Text");

            ViewBag.AreaList = new D_tb_Area().GetList();
            ViewBag.ClientManageList = new D_tb_ClientManage().GetList();
            return View();
        }

        /// <summary>
        /// 获取所有数据列表
        /// 作者：小朱
        /// </summary>
        /// <returns>将DataTable转换为Json数据格式通过string类型返回</returns>
        public string GetList(int pageNumber, int pageSize, string LaboratoryID, string StartTime, string EndTime)
        {
            DataTable dt = new DataTable();
            int total = 0;
            string strWhere = " T.LaboratoryID>0";
            if (LaboratoryID != "-1")
            {
                strWhere = " T.LaboratoryID=" + LaboratoryID;
            }

            if (StartTime != null && StartTime.Trim() != "")
            {
                strWhere += " and T.DetectTime>=cast('" + StartTime + "' as datetime)";
            }
            if (EndTime != null && EndTime.Trim() != "")
            {
                strWhere += " and T.DetectTime<cast('" + Convert.ToDateTime(EndTime).AddDays(1).ToShortDateString() + "' as datetime)";
            }

            if (CurrentUserInfo.DataRange != 1) //若当前用户不是超级管理员 全部数据权限
            {
                strWhere += " and T.LaboratoryID in (select LaboratoryID from tb_Laboratory where AreaID =" + CurrentUserInfo.AreaID + ")";
            }

            dt = tDetectProject.GetListByPage(strWhere, "", pageNumber * pageSize - (pageSize - 1), pageNumber * pageSize, ref total).Tables[0];

            string strJson = PublicClass.ToJson(dt, total);
            if (strJson.Trim() == "")
            {
                strJson = "{\"total\":0,\"rows\":[]}";
            }
            return strJson;
        }
        
        public string GetListByReport(E_ExpeStatisticsSearchParameter eSearchParameter)
        {
            DataTable dt = new DataTable();
            int total = 0;

            StringBuilder strWhere = new StringBuilder();


            if (!string.IsNullOrEmpty(eSearchParameter.GHS))  //检验单位
            {
                strWhere.AddWhere("T.GHS like '" + eSearchParameter.GHS + "'");
            }
            if (!string.IsNullOrEmpty(eSearchParameter.Department)) //抽送检单位
            {
                strWhere.AddWhere("T.Department like '" + eSearchParameter.Department + "'");
            }

            if (!string.IsNullOrEmpty(eSearchParameter.txt_search))
            {
                switch (eSearchParameter.ddl_type)
                {
                    case "ypmc":
                        strWhere.AddWhere("name like '%" + eSearchParameter.txt_search + "%'");
                        break;
                    case "jyxm":
                        strWhere.AddWhere("ProjectName like '%" + eSearchParameter.txt_search + "%'");
                        break;
                    case "jyr":
                        strWhere.AddWhere("TestPersonnelName like '%" + eSearchParameter.txt_search + "%'");
                        break;
                }
            }
            if (!string.IsNullOrEmpty(eSearchParameter.txt_StartTime))
            {
                strWhere.AddWhere("DetectTime >= '" + eSearchParameter.txt_StartTime + "'");
            }
            if (!string.IsNullOrEmpty(eSearchParameter.txt_EndTime))
            {
                strWhere.AddWhere("DetectTime <= '" + eSearchParameter.txt_EndTime + "'");
            }


            int startindex = eSearchParameter.pageNumber * eSearchParameter.pageSize - (eSearchParameter.pageSize - 1);
            int endindex = eSearchParameter.pageNumber * eSearchParameter.pageSize;
            dt = tDetectProject.GetListByReport(strWhere.ToString(), "", startindex, endindex, ref total).Tables[0];

            //张伟修改，增加合计
            DataTable dt2 = dt.Clone();
            DataRow dr1 = dt2.NewRow();
            dr1["name"] = "本页合计";
            dr1["QualifiedLevel"] = dt.Compute("sum(QualifiedLevel)", "");
            dr1["QualifiedLevelA"] = dt.Compute("sum(QualifiedLevelA)", "");
            dr1["QualifiedLevelB"] = dt.Compute("sum(QualifiedLevelB)", "");
            dt2.Rows.InsertAt(dr1, 0);

            //总查询合计
            DataRow dr2 = dt2.NewRow();
            dr2["name"] = "总合计";

            DataRow row = dDetectProject.GetAllListCountForReport(strWhere.ToString());
            dr2["QualifiedLevel"] = row["QualifiedLevel"].ToString() == "" ? "0": row["QualifiedLevel"].ToString();
            dr2["QualifiedLevelA"] = row["QualifiedLevelA"].ToString()==""?"0": row["QualifiedLevelA"].ToString();
            dr2["QualifiedLevelB"] = row["QualifiedLevelB"].ToString()==""?"0": row["QualifiedLevelB"].ToString();
            dt2.Rows.InsertAt(dr2, 1);

            return "{\"total\":" + total + ",\"rows\":" + JsonConvert.SerializeObject(dt) + ",\"footer\":" + JsonConvert.SerializeObject(dt2) + "}";
        }
        
        public FileResult ExportReport(E_ExpeStatisticsSearchParameter eSearchParameter)
        {
            //拼接查询条件
            StringBuilder strWhere = new StringBuilder();
            if (!string.IsNullOrEmpty(eSearchParameter.GHS))  //检验单位
            {
                strWhere.AddWhere("T.GHS like '%" + eSearchParameter.GHS + "%'");
            }
            if (!string.IsNullOrEmpty(eSearchParameter.Department)) //抽送检单位
            {
                strWhere.AddWhere("T.Department like '%" + eSearchParameter.Department + "%'");
            }

            if (!string.IsNullOrEmpty(eSearchParameter.txt_search))
            {
                switch (eSearchParameter.ddl_type)
                {
                    case "ypmc":
                        strWhere.AddWhere("name like '%" + eSearchParameter.txt_search + "%'");
                        break;
                    case "jyxm":
                        strWhere.AddWhere("ProjectName like '%" + eSearchParameter.txt_search + "%'");
                        break;
                    case "jyr":
                        strWhere.AddWhere("TestPersonnelName like '%" + eSearchParameter.txt_search + "%'");
                        break;
                }
            }
            if (!string.IsNullOrEmpty(eSearchParameter.txt_StartTime))
            {
                strWhere.AddWhere("DetectTime >= '" + eSearchParameter.txt_StartTime + "'");
            }
            if (!string.IsNullOrEmpty(eSearchParameter.txt_EndTime))
            {
                strWhere.AddWhere("DetectTime <= '" + eSearchParameter.txt_EndTime + "'");
            }

            DataTable dt = new DataTable();
            dt = tDetectProject.GetExportListByReport(strWhere.ToString(), "").Tables[0];
            
            MemoryStream stream = new MemoryStream();
            stream = PublicClass.ExportReportToExcel(dt);
            string filename = "实验统计列表" + DateTime.Now.ToFileTime() + ".xls";
            return File(stream, "application/vnd.ms-excel", filename);
        }


        /// <summary>
        /// 未完成工作统计
        /// </summary>
        public ActionResult UnfinishedWorkList()
        {
            //List<E_ExpePlanStatistics> ExpePlanStatisticslist = dStatistics.GetExpePlanStatistics();
            //ViewBag.ExpePlanStatisticslist = ExpePlanStatisticslist;
            List<E_TestReportDataStatistics> TestReportMonthDataStatisticslist = dStatistics.GetTestReportMonthDataStatistics();
            ViewBag.TestReportMonthDataStatisticslist = TestReportMonthDataStatisticslist;
            ViewBag.AreaList = dArea.GetList();
            return View("~/views/ExpeStatistics/UnfinishedWorkList.cshtml");
        }

        /// <summary>
        /// 获取未完成工作汇总
        /// </summary>
        public JsonResult SummaryWork()
        {
            List<int> result = dStatistics.SummaryWork();
            return Json(new { result = result }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取实验计划统计数据列表
        /// </summary>
        public JsonResult GetExpePlanList(int areaid, int headpersonnelid, DateTime? starttime, DateTime? endtime)
        {
            List<E_ExpePlanStatistics> ExpePlanStatisticslist = dStatistics.GetExpePlanStatistics(areaid, headpersonnelid, starttime, endtime);
            return Json(ExpePlanStatisticslist, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取实验计划统计
        /// </summary>
        public JsonResult ExpePlanStatistics(int areaid,int headpersonnelid, DateTime starttime,DateTime endtime)
        {
            List<E_ExpePlanStatistics> ExpePlanStatisticslist = dStatistics.GetExpePlanStatisticsForDay(areaid, headpersonnelid, starttime,endtime);

            //人名
            List<string> namearray = new List<string>();
            ExpePlanStatisticslist.ForEach(p =>
            {
                if (!namearray.Contains(p.headpersonnename))
                {
                    namearray.Add(p.headpersonnename);
                }
            });

            //日期集合
            List<string> dataarray = new List<string>();
            DateTime time = starttime;
            while (time < endtime)
            {
                dataarray.Add(time.ToString("MM月dd日"));
                time = time.AddDays(1);
            }

            //数据集合
            List<E_Series> serieslist = new List<E_Series>();
            foreach (var item in namearray)
            {
                List<int> dataitem = new List<int>();
                List<E_ExpePlanStatistics> tempExpePlanStatistics = ExpePlanStatisticslist.Where(p => p.headpersonnename == item).ToList();
                DateTime temptime = starttime;
                while (temptime < endtime)
                {
                    E_ExpePlanStatistics model = tempExpePlanStatistics.Where(p => p.inspectTime.ToString("yyyy-MM-dd") == temptime.ToString("yyyy-MM-dd")).FirstOrDefault();
                    if (model != null)
                    {
                        dataitem.Add(model.notcompleted);
                    }
                    else
                    {
                        dataitem.Add(0);
                    }
                    temptime = temptime.AddDays(1);
                }
                serieslist.Add(new E_Series() { name = item, data = dataitem });
            }
            return Json(new { namearray = namearray, dataarray = dataarray, serieslist = serieslist }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取未审批、未批准检验报告统计
        /// </summary>
        public JsonResult ExamineApprovalStatistics(DateTime starttime,DateTime endtime)
        {
            List<E_TestReportDataStatistics> TestReportMonthDataStatisticslist = dStatistics.GetTestReportDayDataStatistics(starttime, endtime);
            List<string> dataarray = new List<string>();
            List<int> examinearray = new List<int>();
            List<int> approvalarray = new List<int>();
            DateTime time = starttime;
            while (time < endtime)
            {
                dataarray.Add(time.ToString("MM月dd日"));
                E_TestReportDataStatistics model = TestReportMonthDataStatisticslist.Where(p => p.updatetime == time.ToString("yyyy-MM-dd")).FirstOrDefault();
                if (model != null)
                {
                    examinearray.Add(model.examinecount);
                    approvalarray.Add(model.approvalcount);
                }
                else
                {
                    examinearray.Add(0);
                    approvalarray.Add(0);
                }
                time = time.AddDays(1);
            }
            return Json(new { dataarray= dataarray, examinearray= examinearray, approvalarray= approvalarray },JsonRequestBehavior.AllowGet);
        }

    }
}
