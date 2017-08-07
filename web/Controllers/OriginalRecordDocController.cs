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
    public class OriginalRecordDocController : Controller
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

        /// <summary>
        /// 原始记录文档在线编辑
        /// </summary>
        /// <param name="InfoID">原始记录ID</param>
        /// <returns></returns>
        public ActionResult OriginalRecordDocEdit(int InfoID)
        {
            E_tb_OriginalRecord eOriginalRecord = _dOriginalRecord.GetModel(new E_tb_OriginalRecord() { RecordID = Convert.ToInt32(InfoID) });
            E_tb_Project eProject = _dProject.GetModel(Convert.ToInt32(eOriginalRecord.ProjectID));

            //判断是否已存在原始记录文件，若不存在默认读取检测项目对应的模板
            string filepath = eProject.FilePath;
            if (System.IO.File.Exists(Server.MapPath("~/upfile/" + eOriginalRecord.FilePath)))
            {
                filepath = eOriginalRecord.FilePath;
            }

            //初始化PageOffice控件
            PageOfficeCtrl pc = this.GetOfficeCtrl(
                "/OriginalRecordDoc/SaveDoc?filename=" + eOriginalRecord.FilePath,
                "/OriginalRecordDoc/SaveData?FilePath=" + eOriginalRecord.FilePath + "&ProjectID=" + eOriginalRecord.ProjectID+ "&RecordID="+ InfoID,
                filepath);
            //定义提交数据范围
            PageOffice.ExcelWriter.Workbook wb = new PageOffice.ExcelWriter.Workbook();
            PageOffice.ExcelWriter.Sheet sheetOrder = wb.OpenSheet("Sheet1");
            PageOffice.ExcelWriter.Table table = sheetOrder.OpenTable(eProject.SampleDataRange.Replace("：", ":").ToUpper());
            pc.SetWriter(wb);
            //判断模板类型，是否为非农残检测项目
            if (eOriginalRecord.IsPesCheck != 1)
            {
                PageOffice.ExcelWriter.Workbook workBook = this.GetNoPesCheckWorkbook(Convert.ToInt32(eOriginalRecord.SampleID), eProject.SampleDataRange);
                pc.SetWriter(workBook);// 注意不要忘记此代码，如果缺少此句代码，不会赋值成功。
            }
            //实例化控件输出内容
            Page page = new Page();
            page.Controls.Add(pc);
            StringBuilder controlOutput = new StringBuilder();
            using (StringWriter sw = new StringWriter(controlOutput))
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    Server.Execute(page, htw, false);
                }
            }
            ViewBag.EditorHtml = controlOutput.ToString();

            return View("/views/OriginalRecord/FileView.cshtml");
        }

        /// <summary>
        /// 获取PageOffice控件
        /// </summary>
        /// <param name="SaveFilePageUrl">文件保存处理页面</param>
        /// <param name="SaveDataPageUrl">文档数据保存处理页面</param>
        /// <param name="FileName">需要打开的文件</param>
        /// <returns>返回初始化完毕的控件</returns>
        public PageOfficeCtrl GetOfficeCtrl(string SaveFilePageUrl, string SaveDataPageUrl, string FileName)
        {
            PageOfficeCtrl pc = new PageOfficeCtrl();
            pc.SaveFilePage = SaveFilePageUrl;
            pc.ServerPage = "/pageoffice/server.aspx";
            pc.SaveDataPage = SaveDataPageUrl;
            var openmodeltype = PageOffice.OpenModeType.docAdmin;
            var filenames = FileName.Split('.');
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
            pc.WebOpen("/UpFile/" + FileName, openmodeltype, "s");
            return pc;
        }

        /// <summary>
        /// 获取非农残检测项目 需要填充的数据
        /// </summary>
        /// <param name="SampleID">样品ID</param>
        /// <param name="SampleDataRange">检测项目对应模板中的数据范围</param>
        /// <returns>需要填充的数据</returns>
        public PageOffice.ExcelWriter.Workbook GetNoPesCheckWorkbook(int SampleID, string SampleDataRange)
        {
            tb_Sample eSample = tSample.GetModel(SampleID);
            ArrayList CellArr = new ArrayList { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
            string StrDataRange = SampleDataRange.Replace("：", ":").ToUpper();
            int CellStartIndex = CellArr.IndexOf(StrDataRange.Split(':')[0].Substring(0, 1).ToUpper());
            int DRangeStartIndex = int.Parse(StrDataRange.Split(':')[0].Substring(1, StrDataRange.Split(':')[0].Length - 1)) + 1;
            int DRangeEndIndex = int.Parse(StrDataRange.Split(':')[1].Substring(1, StrDataRange.Split(':')[0].Length - 1));
            //定义Workbook对象
            PageOffice.ExcelWriter.Workbook workBook = new PageOffice.ExcelWriter.Workbook();
            //定义Sheet对象，"Sheet1"是打开的Excel表单的名称
            PageOffice.ExcelWriter.Sheet sheet = workBook.OpenSheet("Sheet1");
            for (int i = DRangeStartIndex; i <= DRangeEndIndex; i++)
            {
                //或者直接给Cell赋值
                sheet.OpenCell(CellArr[CellStartIndex].ToString() + i).Value = eSample.sampleNum;                         //样品编号
                sheet.OpenCell(CellArr[CellStartIndex + 1].ToString() + i).Value = eSample.name;                          //样品名称
                sheet.OpenCell(CellArr[CellStartIndex + 2].ToString() + i).Value = eSample.productDate.ToString(); ;      //生产日期
                sheet.OpenCell(CellArr[CellStartIndex + 3].ToString() + i).Value = eSample.detectionDate.ToString();      //抽样日期
            }
            return workBook;
        }

        /// <summary>
        /// 提取并保存文档中的数据
        /// </summary>
        /// <param name="FilePath"></param>
        /// <param name="ProjectID"></param>
        /// <returns></returns>
        public ActionResult SaveData(string FilePath, int ProjectID,int RecordID)
        {
            PageOffice.ExcelReader.Workbook wb = new PageOffice.ExcelReader.Workbook();
            PageOffice.ExcelReader.Sheet sheet = wb.OpenSheet("Sheet1");
            E_tb_Project eProject = tProject.GetModel(ProjectID);
            PageOffice.ExcelReader.Table table = sheet.OpenTable(eProject.SampleDataRange.Replace("：", ":").ToUpper());
            DataTable dt = new DataTable();
            for (int i = 0; i < table.DataFields.Count; i++)
            {
                dt.Columns.Add(table.DataFields[i].Text);
            }
            table.NextRow();
            while (!table.EOF)
            {
                //获取提交的数值
                if (!table.DataFields.IsEmpty)
                {
                    DataRow row = dt.NewRow();
                    for (int i = 0; i < table.DataFields.Count; i++)
                    {
                        row[i] = table.DataFields[i].Text;
                    }
                    dt.Rows.Add(row);
                }

                try
                {
                    table.NextRow(); //循环进入下一行
                }
                catch
                {
                    break;
                }
            }

            //删除历史数据
            tRecordSample.DeleteListByWhere("RecordFilePath='" + FilePath + "'");
            tTestReportData.DeleteByWhere("RecordFilePath='" + FilePath + "'");

            //关联检验报告
            Regex reg = new Regex("^[0-9]+(.[0-9])?$");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (eProject.IsPesCheck != null && Convert.ToInt32(eProject.IsPesCheck.ToString()) == 1) //判断是否为检验农药残留项目
                {
                    DataRow item = dt.Rows[i];
                    string SampleName = item["样品名称"].ToString(); //样品名称
                    string strResult = item["复检值2"].ToString();//复检值2
                    if (strResult.Trim() == "")
                    {
                        strResult = item["最终值"].ToString();//最终值
                    }
                    if (!string.IsNullOrEmpty(SampleName.Trim()))
                    {
                        E_tb_TestReportData eTestReportData = new E_tb_TestReportData();
                        eTestReportData.RecordID = RecordID;        //原始记录ID
                        eTestReportData.RecordFilePath = FilePath;  //原始记录文件名
                        eTestReportData.ReportID = 0;               //检验报告ID
                        eTestReportData.TestName = SampleName;      //检验名称/检验项目名称 (农药残留检验项目，直接显示样品名称)
                        eTestReportData.TestStandard = "";          //检验标准
                        eTestReportData.TestResult = strResult;     //检验结果
                        eTestReportData.QualifiedLevel = "";        //是否合格
                        eTestReportData.TestPersonnelName = "";     //检验人
                        tTestReportData.Add(eTestReportData);
                    }
                }
                else
                {
                    DataRow item = dt.Rows[i];
                    string strSampleID = item["编号"].ToString();
                    string strResult = item["最终值"].ToString();
                    tb_Sample eSample = tSample.GetModelList("sampleNum='" + strSampleID.Trim() + "'").FirstOrDefault();
                    if (eSample != null && !string.IsNullOrEmpty(strResult))
                    {
                        E_tb_TestReportData eTestReportData = new E_tb_TestReportData();
                        eTestReportData.RecordID = RecordID;        //原始记录ID
                        eTestReportData.RecordFilePath = FilePath;  //原始记录文件名
                        eTestReportData.ReportID = 0;               //检验报告ID
                        eTestReportData.TestName = "";              //检验名称/检验项目名称
                        eTestReportData.TestStandard = "";          //检验标准
                        eTestReportData.TestResult = strResult;     //检验结果
                        eTestReportData.QualifiedLevel = "";        //是否合格
                        eTestReportData.TestPersonnelName = "";     //检验人
                        tTestReportData.Add(eTestReportData);
                    }
                }
            }
            table.Close();
            wb.Close();
            return View("/views/OriginalRecord/SaveData.cshtml");
        }

        /// <summary>
        /// 保存在线修改的office文件
        /// </summary>
        /// <returns></returns>
        public ActionResult SaveDoc(string filename)
        {
            ViewBag.Message = "Your app description page.";
            string filePath = AppDomain.CurrentDomain.BaseDirectory + "UpFile//" + filename;
            PageOffice.FileSaver fs = new PageOffice.FileSaver();
            fs.SaveToFile(filePath);
            fs.Close();
            return View("/views/OriginalRecord/SaveDoc.cshtml");
        }
    }
}