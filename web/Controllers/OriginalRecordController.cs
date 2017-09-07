using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.OriginalRecord;
using System.Data;
using Model.OriginalRecord;
using Common;
using BLL.Laboratory;
using BLL.EntrustManage;
using System.Text;
using System.IO;
using System.Web.UI;
using BLL.PersonnelManage;
using Model.Laboratory;
using System.Text.RegularExpressions;
using BLL;
using Model;
using BLL.RoleManage;
using BLL.ExpePlan;
using Model.ExpePlan;
using Model.TestReport;
using BLL.TestReport;
using Model.EntrustManage;
using System.Collections;
using Comp;
using DAL.OriginalRecord;
using DAL.Sample;
using DAL.Laboratory;
using DAL.ExpePlan;
using PageOffice;

namespace Web.Controllers
{
    public class OriginalRecordController : BaseController
    {
        T_tb_Area tArea = new T_tb_Area();
        T_tb_OriginalRecord tOriginalRecord = new T_tb_OriginalRecord(); //原始记录管理
        D_tb_OriginalRecord _dOriginalRecord = new D_tb_OriginalRecord();
        T_tb_Project tProject = new T_tb_Project();//项目管理
        D_tb_Project _dProject = new D_tb_Project();
        T_tb_EntrustTesting tEntrustTesting = new T_tb_EntrustTesting();//委托检验管理
        T_tb_InPersonnel tInPersonnel = new T_tb_InPersonnel();//内部人员管理
        tb_SampleBLL tSample = new tb_SampleBLL();//样品管理
        D_Sample _dSample = new D_Sample();
        T_tb_RecordSample tRecordSample = new T_tb_RecordSample();//原始记录样品计算结果
        T_tb_ExpePlan tExpePlan = new T_tb_ExpePlan();//实验计划
        D_tb_ExpePlan _dExpePlan = new D_tb_ExpePlan();
        T_tb_TestReport tTestReport = new T_tb_TestReport();//检验报告
        T_tb_TestReportData tTestReportData = new T_tb_TestReportData();//检验报告数据

        //
        // GET: /Laboratory/
        public ActionResult OriginalRecordList(E_PageParameter ePageParameter)
        {
            int pageIndex = Utils.GetInt(Request["page"]);
            ePageParameter.pageindex = pageIndex > 0 ? pageIndex - 1 : pageIndex;
            ePageParameter.pagesize = 20;

            ViewData["AreaList"] = PageTools.GetSelectList(tArea.GetList("").Tables[0], "AreaID", "AreaName", true);
            ViewData["ProjectList"] = PageTools.GetSelectList(tProject.GetList("").Tables[0], "ProjectID", "ProjectName", true);

            ViewBag.OriginalRecordList = this.GetList(ePageParameter);
            ViewBag.ePageParameter = ePageParameter;
            ViewBag.page = Utils.ShowPage(ePageParameter.count, ePageParameter.pagesize, pageIndex, 5);
            return View("/views/OriginalRecord/OriginalRecordList.cshtml");
        }

        /// <summary>
        /// 获取所有数据列表
        /// </summary>
        /// <returns>将DataTable转换为Json数据格式通过string类型返回</returns>
        public DataTable GetList(E_PageParameter ePageParameter)
        {
            int total = 0;
            StringBuilder strWhere = new StringBuilder();
            if (ePageParameter.recordid > 0)
            {
                strWhere.AddWhere("T.RecordID=" + ePageParameter.recordid);
            }
            if (ePageParameter.areaid > 0)
            {
                strWhere.AddWhere("T.AreaID=" + ePageParameter.areaid);
            }
            if (ePageParameter.projectid > 0)
            {
                strWhere.AddWhere("T.ProjectID=" + ePageParameter.projectid);
            }
            if (ePageParameter.starttime != null)
            {
                strWhere.AddWhere("T.DetectTime>=cast('" + ePageParameter.starttime + "' as datetime)");
            }
            if (ePageParameter.endtime != null)
            {
                strWhere.AddWhere("T.DetectTime<=cast('" + ePageParameter.endtime + "' as datetime)");
            }
            if (!string.IsNullOrEmpty(ePageParameter.taskno))
            {
                strWhere.AddWhere("T.TaskNo like  '%" + ePageParameter.taskno + "%'");
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
            int startindex = ePageParameter.pageindex * ePageParameter.pagesize + 1;
            int endindex = (ePageParameter.pageindex + 1) * ePageParameter.pagesize;
            dt = _dOriginalRecord.GetListByPage(strWhere.ToString(), "", startindex, endindex, ref total).Tables[0];
            ePageParameter.count = total;
            return dt;
        }

        /// <summary>
        /// 生成原始记录
        /// </summary>
        /// <param name="PlanID">实验计划ID</param>
        /// <returns>返回生成结果</returns>
        public JsonResult CreateOriginalRecord(int PlanID)
        {
            E_tb_ExpePlan eExpePlan = _dExpePlan.GetModel(PlanID);
            E_tb_Project eProject = _dProject.GetModel(Convert.ToInt32(eExpePlan.ProjectID));

            //定义原始记录实体
            E_tb_OriginalRecord eOriginalRecord = new E_tb_OriginalRecord();
            eOriginalRecord.ProjectID = eExpePlan.ProjectID;
            eOriginalRecord.TaskNo = eExpePlan.TaskNo;
            eOriginalRecord.DetectTime = eExpePlan.InspectTime;
            eOriginalRecord.DetectPersonnelID = CurrentUserInfo.PersonnelID;
            //提前给定最终文件保存名称
            eOriginalRecord.FilePath = "OriginalRecordFile/LIMS" + DateTime.Now.ToString("yyyyMMddhhmmss") + "." + eProject.FilePath.Split('.')[1];
            eOriginalRecord.Contents = "";
            eOriginalRecord.AreaID = CurrentUserInfo.AreaID;
            eOriginalRecord.EditPersonnelID = CurrentUserInfo.PersonnelID;
            eOriginalRecord.SampleID = eExpePlan.SampleID;
            //默认给第一个标准， 后续可修改
            eOriginalRecord.InsStand = eProject.InsStand.Split('、')[0].ToString();

            bool result = _dOriginalRecord.Add(eOriginalRecord) > 0;
            return Json(new { result = result, msg = "生成成功！" }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 显示详情页
        /// </summary>
        /// <param name="EditType">编辑类型</param>
        /// <returns>返回编辑结果</returns>
        public ActionResult OriginalRecordEdit(int InfoID)
        {
            E_tb_OriginalRecord eOriginalRecord = _dOriginalRecord.GetModel(new E_tb_OriginalRecord() { RecordID = Convert.ToInt32(InfoID) });
            List<string> InsStandList = new List<string>();
            E_tb_Project eProject = _dProject.GetModel(Convert.ToInt32(eOriginalRecord.ProjectID));
            if (!string.IsNullOrEmpty(eProject.InsStand))
            {
                foreach (var item in eProject.InsStand.Split('、'))
                {
                    InsStandList.Add(item);
                }
            }

            ViewBag.DetectPersonnelID = CurrentUserInfo.PersonnelID;
            ViewBag.DetectPersonnelName = CurrentUserInfo.PersonnelName;
            ViewData["InsStandList"] = InsStandList;
            return View(eOriginalRecord);
        }
        
        /// <summary>
        /// 删除实验室信息
        /// </summary>
        /// <param name="InfoID">要删除的实验室</param>
        /// <returns>返回是否删除成功</returns>
        public JsonResult Delete(int id)
        {
            tRecordSample.DeleteListByWhere("RecordID=" + id); //删除对应的样品记录信息
            bool result = tOriginalRecord.Delete(id); //删除原始记录
            return Json("删除成功！", JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 保存实验室信息
        /// </summary>
        /// <param name="eOriginalRecord">要处理的对象</param>
        /// <returns>返回是否处理成功</returns>
        public JsonResult Save(E_tb_OriginalRecord eOriginalRecord)
        {
            eOriginalRecord.AreaID = CurrentUserInfo.AreaID;
            eOriginalRecord.EditPersonnelID = CurrentUserInfo.PersonnelID;
            bool result = tOriginalRecord.Update(eOriginalRecord);

            //关联样品数据(废弃，因无录入行为)
            //tRecordSample.UpdateRecordIDByFilePath(eOriginalRecord.RecordID, eOriginalRecord.FilePath);

            ////生成检验报告(该功能有前台触发)
            //E_tb_ExpePlan eExpePlan = tExpePlan.GetModelList("TaskNo='" + eOriginalRecord.TaskNo + "' and SampleID=" + eOriginalRecord.SampleID.Value).FirstOrDefault();
            //Updatethings(eOriginalRecord, eExpePlan);

            return Json(new { result = result, msg = "修改成功！" });
        }
        
        /// <summary>
        /// 更新检验报告
        /// </summary>
        /// <param name="eOriginalRecord"></param>
        /// <param name="eExpePlan"></param>
        public JsonResult UpdateTestReport(int RecordID)
        {
            E_tb_OriginalRecord eOriginalRecord = _dOriginalRecord.GetModel(new E_tb_OriginalRecord() { RecordID = RecordID });
            E_tb_ExpePlan eExpePlan = _dExpePlan.GetExpePlanInfo(new E_tb_ExpePlan() { TaskNo = eOriginalRecord.TaskNo });

            tb_Sample eSample = tSample.GetModel(eOriginalRecord.SampleID.Value);
            string productNum = eSample.protNum;//产品批次
            E_tb_TestReport eTestReport = null;
            var tempmodel = tTestReport.GetModelList(" SampleNum = '" + eSample.sampleNum + "'");
            if (tempmodel != null && tempmodel.Count > 0)
            {
                eTestReport = tempmodel.First();
                eTestReport.SampleNum = eSample.sampleNum;//样品编号
                eTestReport.SampleName = eSample.name;//样品名称
            }
            if (eTestReport == null)
            {
                eTestReport = new E_tb_TestReport();
                eTestReport.productNum = productNum; //批次号
                eTestReport.SampleNum = eSample.sampleNum;//样品编号
                eTestReport.SampleName = eSample.name;//样品名称
                eTestReport.ProductionTime = eSample.productDate;//生产日期
                eTestReport.Specifications = eSample.modelType;//规格型号
                eTestReport.ShelfLife = eSample.expirationDate;//保质期
                string Department = "/";
                if (eSample.isDetection)
                {
                    Department = eSample.detectionCompany;
                }
                else
                {
                    var clint = new BLL.ClientManage.T_tb_ClientManage().GetModel(Convert.ToInt32(eSample.InspectCompany));
                    Department = clint.ClientName;
                }
                eTestReport.Department = Department;//送/抽检单位
                eTestReport.SendTestAddress = eSample.InspectAddress;//送检单位地址
                eTestReport.SamplingPlace = eSample.detectionAdress;//抽样地点
                eTestReport.SamplingCompany = eSample.detectionCompany; //抽样单位
                eTestReport.SamplingPersonnel = eSample.detectionUser;//抽样者
                eTestReport.Packing = eSample.packaging;//包装形式
                eTestReport.SamplingTime = eSample.detectionDate;//抽样日期
                eTestReport.TestBasis = new BLL.Laboratory.T_tb_Project().GetModel(eOriginalRecord.ProjectID.Value).ExpeMethod;
                eTestReport.TestTime = eOriginalRecord.DetectTime;//检验日期
                DataTable dt = tOriginalRecord.GetRecordIDListBySampleID(int.Parse(eExpePlan.SampleID.ToString()));
                string RecordIDS = "";
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        RecordIDS += dt.Rows[i]["RecordID"].ToString() + ",";
                    }
                    RecordIDS = RecordIDS.TrimEnd(',');
                }
                eTestReport.RecordIDS = RecordIDS;//原始记录IDS
                DataTable dtExpePlan = tExpePlan.GetList("SampleID=" + eExpePlan.SampleID.ToString()).Tables[0];
                string TaskNoS = "";
                if (dtExpePlan != null && dtExpePlan.Rows.Count > 0)
                {
                    for (int i = 0; i < dtExpePlan.Rows.Count; i++)
                    {
                        TaskNoS += dtExpePlan.Rows[i]["PlanID"].ToString() + ",";
                    }
                    TaskNoS = TaskNoS.TrimEnd(',');
                }
                eTestReport.TaskNoS = TaskNoS;//任务单号 对应的检验计划IDS 
                eTestReport.EditPersonnelID = CurrentUserInfo.PersonnelID;
                eTestReport.AreaID = CurrentUserInfo.AreaID;
                eTestReport.AddTime = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                eTestReport.UpdateTime = DateTime.Now;
                eTestReport.ReportID = tTestReport.Add(eTestReport);
            }
            else
            {
                DataTable dt = tOriginalRecord.GetRecordIDListBySampleID(int.Parse(eExpePlan.SampleID.ToString()));
                string RecordIDS = "";
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        RecordIDS += dt.Rows[i]["RecordID"].ToString() + ",";
                    }
                    RecordIDS = RecordIDS.TrimEnd(',');
                }
                eTestReport.RecordIDS = RecordIDS;//原始记录IDS
                DataTable dtExpePlan = tExpePlan.GetList("SampleID=" + eExpePlan.SampleID.ToString()).Tables[0];
                string TaskNoS = "";
                if (dtExpePlan != null && dtExpePlan.Rows.Count > 0)
                {
                    for (int i = 0; i < dtExpePlan.Rows.Count; i++)
                    {
                        TaskNoS += dtExpePlan.Rows[i]["PlanID"].ToString() + ",";
                    }
                    TaskNoS = TaskNoS.TrimEnd(',');
                }
                eTestReport.TaskNoS = TaskNoS;//任务单号 对应的检验计划IDS 
                eTestReport.AreaID = CurrentUserInfo.AreaID;
                eTestReport.EditPersonnelID = CurrentUserInfo.PersonnelID;
                eTestReport.UpdateTime = DateTime.Now;
                eTestReport.TestBasis = new BLL.Laboratory.T_tb_Project().GetModel(eOriginalRecord.ProjectID.Value).ExpeMethod;
                tTestReport.Update(eTestReport);
            }

            //更新检验报告数据
            List<E_tb_TestReportData> eTestReportDataList = tTestReportData.GetModelList("RecordFilePath='" + eOriginalRecord.FilePath + "'");
            if (eTestReportDataList != null)
            {
                E_tb_Project eProject = tProject.GetModel(Convert.ToInt32(eOriginalRecord.ProjectID));
                if (eProject.IsPesCheck != null && int.Parse(eProject.IsPesCheck.ToString()) == 1)//判断是否为农药残留检查项目
                {
                    foreach (E_tb_TestReportData eTestReportData in eTestReportDataList)
                    {
                        eTestReportData.RecordID = eOriginalRecord.RecordID;    //原始记录ID
                        eTestReportData.ReportID = eTestReport.ReportID;        //检验报告ID
                        eTestReportData.TestStandard = eProject.ExpeMethod;     //检验标准

                        if (!String.IsNullOrEmpty(eOriginalRecord.InsStand))
                        {
                            String[] strs = eOriginalRecord.InsStand.Split('：');
                            if (strs == null || strs.Length <= 2)
                            {
                                strs = eOriginalRecord.InsStand.Split(':');
                            }
                            if (strs != null && strs.Length >= 2)
                            {
                                String str = strs[1];
                                int result = 0;
                                if (!String.IsNullOrEmpty(str))
                                {
                                    // 正则表达式剔除非数字字符（不包含小数点.） 
                                    str = Regex.Replace(str, @"[^\d.\d]", "");
                                    // 如果是数字，则转换为decimal类型 
                                    if (Regex.IsMatch(str, @"^[+-]?\d*[.]?\d*$"))
                                    {
                                        result = int.Parse(str);
                                    }
                                    if (!String.IsNullOrEmpty(eTestReportData.TestResult))
                                    {
                                        Decimal _testResult = Convert.ToDecimal(eTestReportData.TestResult);
                                        if (_testResult <= result)
                                        {
                                            eTestReportData.QualifiedLevel = "合格";
                                        }
                                        else
                                        {
                                            eTestReportData.QualifiedLevel = "不合格";
                                        }
                                    }
                                }
                            }
                        }

                        tTestReportData.Update(eTestReportData);
                    }
                }
                else
                {
                    foreach (E_tb_TestReportData eTestReportData in eTestReportDataList)
                    {
                        eTestReportData.RecordID = eOriginalRecord.RecordID;    //原始记录ID
                        eTestReportData.ReportID = eTestReport.ReportID;        //检验报告ID
                        eTestReportData.TestName = eProject.ProjectName;        //检验名称/检验项目名称
                        eTestReportData.TestStandard = eProject.ExpeMethod;     //检验标准
                        if (!String.IsNullOrEmpty(eOriginalRecord.InsStand))
                        {
                            String[] strs = eOriginalRecord.InsStand.Split('：');
                            if (strs == null || strs.Length <= 2)
                            {
                                strs = eOriginalRecord.InsStand.Split(':');
                            }
                            if (strs != null && strs.Length >= 2)
                            {
                                String str = strs[1];
                                int result = 0;
                                if (!String.IsNullOrEmpty(str))
                                {
                                    // 正则表达式剔除非数字字符（不包含小数点.） 
                                    str = Regex.Replace(str, @"[^\d.\d]", "");
                                    // 如果是数字，则转换为decimal类型 
                                    if (Regex.IsMatch(str, @"^[+-]?\d*[.]?\d*$"))
                                    {
                                        result = int.Parse(str);
                                    }
                                    if (!String.IsNullOrEmpty(eTestReportData.TestResult))
                                    {
                                        Decimal _testResult = Convert.ToDecimal(eTestReportData.TestResult);
                                        if (_testResult <= result)
                                        {
                                            eTestReportData.QualifiedLevel = "合格";
                                        }
                                        else
                                        {
                                            eTestReportData.QualifiedLevel = "不合格";
                                        }
                                    }
                                }
                            }
                        }
                        tTestReportData.Update(eTestReportData);
                    }
                }
            }
            return Json(new { result = true, msg = "更新成功！" }, JsonRequestBehavior.AllowGet);
        }
        
        //*********************************原有代码****************************************************
        
        /// <summary>
        /// 阅览文件
        /// 作者：章建国
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <returns></returns>
        public JsonResult GetFileURLForView(int wid)
        {
            E_tb_OriginalRecord eOriginalRecord = _dOriginalRecord.GetModel(new E_tb_OriginalRecord() { RecordID = wid });
            E_tb_Project eProject = tProject.GetModel(Convert.ToInt32(eOriginalRecord.ProjectID));
            string SaveFilePage = "/OriginalRecord/SaveDoc?filename=" + eOriginalRecord.FilePath;
            string SaveDataPage = "/OriginalRecord/SaveData?FilePath=" + eOriginalRecord.FilePath + "|ProjectID=" + eOriginalRecord.ProjectID.ToString();
            string DataRange = eProject.SampleDataRange.Replace("：", ":").ToUpper();
            string UrlParams = string.Format("filename={0}&SaveFilePage={1}&SaveDataPage={2}&DataRange={3}", eOriginalRecord.FilePath, SaveFilePage, SaveDataPage, DataRange);
            return Json(UrlParams, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 根据样品ID 项目ID
        /// </summary>
        /// <param name="SampleID"></param>
        /// <returns></returns>
        public JsonResult GetTaskList(string SampleID, string ProjectID)
        {
            List<E_tb_ExpePlan> list = new List<E_tb_ExpePlan>();
            list = new T_tb_ExpePlan().GetModelList(" SampleID=" + SampleID + " and ProjectID=" + ProjectID);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 通过pageoffice展示数据
        /// 作者：章建国
        /// </summary>
        /// <param name="filename">文件名</param>
        /// <returns></returns>
        public ActionResult FileView(string filename, string _pname, string _ptype)
        {
            if (!string.IsNullOrEmpty(filename))
            {
                ViewBag._pname = _pname;
                ViewBag._ptype = _ptype;
                ViewBag.Message = "Your contact page.";
                Page page = new Page();
                string controlOutput = string.Empty;
                PageOffice.PageOfficeCtrl pc = new PageOffice.PageOfficeCtrl();
                pc.SaveFilePage = "/OriginalRecord/SaveDoc?filename=" + filename;
                pc.ServerPage = "/pageoffice/server.aspx";
                var openmodeltype = PageOffice.OpenModeType.docAdmin;
                try
                {
                    var filenames = filename.Split('.');
                    switch (filenames[1])
                    {
                        case "doc":
                            openmodeltype = PageOffice.OpenModeType.docNormalEdit;
                            break;
                        case "docx":
                            openmodeltype = PageOffice.OpenModeType.docNormalEdit;
                            break;
                        case "xlsx":
                            openmodeltype = PageOffice.OpenModeType.xlsNormalEdit;
                            break;
                        case "xls":
                            openmodeltype = PageOffice.OpenModeType.xlsNormalEdit;
                            break;
                        case "pptx":
                            openmodeltype = PageOffice.OpenModeType.pptNormalEdit;
                            break;
                        case "ppt":
                            openmodeltype = PageOffice.OpenModeType.pptNormalEdit;
                            break;
                    }
                }
                catch
                {

                }
                pc.WebOpen("/UpFile/" + filename, openmodeltype, "s");
                page.Controls.Add(pc);
                StringBuilder sb = new StringBuilder();
                using (StringWriter sw = new StringWriter(sb))
                {
                    using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                    {
                        Server.Execute(page, htw, false); controlOutput = sb.ToString();
                    }
                }
                ViewBag.EditorHtml = controlOutput;
            }
            return View();
        }

        /// <summary>
        /// 通过FlexPaper展示PDF
        /// 作者：章建国
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public ActionResult SWFShow(string filename)
        {
            ViewBag._SWFURL = filename;
            return View();
        }
        
    }
}
