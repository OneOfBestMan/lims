using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;
using BLL;
using System.Data;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using BLL.RoleManage;
using Comp;
using DAL.Sample;
using DAL.ExpePlan;
using Model.ExpePlan;
using Model.Sample;
using System.Reflection;
using NPOI.XWPF.UserModel;
using NPOI.OpenXmlFormats.Wordprocessing;
using System.Text;
using Model.ClientManage;
using BLL.ClientManage;

namespace Web.Controllers
{
    public class SampleController : BaseController
    {
        tb_SampleBLL _samplebll = new tb_SampleBLL();
        tb_DetectionBLL _detectionbll = new tb_DetectionBLL();
        tb_TestStandardBLL _teststandardbll = new tb_TestStandardBLL();
        T_tb_Area _tArea = new T_tb_Area();
        D_Sample _dSample = new D_Sample();

        /// <summary>
        /// 获取样品列表
        /// </summary>
        /// <param name="ePageParameter">查询参数实体</param>
        [Route("sample/samplelist")]
        public ActionResult GetList(E_PageParameter ePageParameter)
        {
            int pageIndex = Utils.GetInt(Request["page"]);
            ePageParameter.pageindex = pageIndex > 0 ? pageIndex - 1 : pageIndex;
            ePageParameter.pagesize = 20;
            
            List<tb_Sample> list = new List<tb_Sample>();
            int count = 0;
            
            switch (CurrentUserInfo.DataRange)
            {
                case 3: //个人
                    ePageParameter.areaid = -1;
                    ePageParameter.createuser = CurrentUserInfo.PersonnelID;
                    break;
                case 2://区域
                    ePageParameter.areaid = (int)CurrentUserInfo.AreaID;
                    break;
            }
            
            list = _dSample.GetSampleList(ePageParameter, ref count);
            ViewBag.SampleList = list;
            ViewBag.arealist = _tArea.GetModelList((CurrentUserInfo.DataRange > 1 ? $"AreaID={CurrentUserInfo.AreaID}" : ""));
            ViewBag.ePageParameter = ePageParameter;
            ViewBag.page = Utils.ShowPage(count, ePageParameter.pagesize, pageIndex, 5);
            return View("/views/Sample/SampleList.cshtml");
        }

        /// <summary>
        /// 编辑样品
        /// </summary>
        /// <param name="eSample">样品实体</param>
        [Route("sample/edit")]
        public ActionResult Edit(int id)
        {
            tb_Sample eSample = new tb_Sample();
            List<E_tb_ExpePlan> ExpePlanList = new List<E_tb_ExpePlan>();
            if (id > 0)
            {
                eSample = _dSample.GetSampleInfo(new tb_Sample() { id = id });
                ExpePlanList = new D_tb_ExpePlan().GetExpePlanList(new E_tb_ExpePlan() { SampleID = eSample.id });
            }
            else
            {
                eSample.sampleNum = GetSampleNum();
            }
            ViewBag.Info = eSample;
            ViewBag.ClientList = new BLL.ClientManage.T_tb_ClientManage().GetModelList("");
            ViewBag.ProjectList = new BLL.Laboratory.T_tb_Project().GetModelList("");
            ViewBag.ExpePlanList = ExpePlanList;
            return View("/views/Sample/SampleEdit.cshtml");
        }

        /// <summary>
        /// 保存样品
        /// </summary>
        public JsonResult Save(tb_Sample eSample)
        {
            bool result = false;
            string msg = "";
            eSample.updateUser = CurrentUserInfo.PersonnelID;
            eSample.updateDate = DateTime.Now;
            if (eSample.id > 0)//若存在数据，执行更新
            {
                result = _dSample.Update(eSample);
                msg = (result)?"修改成功": "修改失败";
            }
            else {
                eSample.createUser = CurrentUserInfo.PersonnelID;
                eSample.createDate = DateTime.Now;
                eSample.handleUser = 0;
                eSample.AreaID = (int)CurrentUserInfo.AreaID;
                eSample.sampleAdmin = CurrentUserInfo.PersonnelName;
                eSample.id = _dSample.Add(eSample);//若不存在数据，直接插入
                result = eSample.id > 0;
                msg = (result)?"添加成功": "添加失败";
            }
            return Json(new { result = result,msg=msg, sampleid = eSample.id, areaid = eSample.AreaID, createuser = eSample.createUser, createdate = eSample.createDate }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取送样地址
        /// </summary>
        /// <param name="clientid">客户ID</param>
        /// <returns>返回对应送样地址</returns>
        public JsonResult GetInspectAddress(int clientid)
        {
            bool result = false;
            string msg = "获取送样地址失败";
            string inspectaddress = "";
            E_tb_ClientManage eClientManage = new T_tb_ClientManage().GetModel(clientid);
            if (eClientManage != null)
            {
                result = true;
                msg = "送样地址获取成功！";
                inspectaddress = eClientManage.Address;
            }
            return Json(new { result= result,msg=msg, inspectaddress= inspectaddress }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 销毁样品
        /// </summary>
        /// <param name="id">样品ID集合</param>
        public JsonResult BatchDestroy(string ids)
        {
            bool result = _dSample.BatchDestroy(new tb_Sample() { ids = ids, handleUser = CurrentUserInfo.PersonnelID, handleDate = DateTime.Now });
            string str = (result ? "OK" : "销毁失败！");
            return Json(str, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 删除样品
        /// </summary>
        public JsonResult Delete(int id)
        {
            bool result = _dSample.Delete(new tb_Sample() { id = id });
            string str = (result ? "OK" : "删除失败！");
            return Json(str, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取样品编号
        /// </summary>
        public string GetSampleNum()
        {
            string samplenum = "";
            BLL.RoleManage.T_tb_Area _area = new BLL.RoleManage.T_tb_Area();
            string _areaname = _area.GetModel(CurrentUserInfo.AreaID.Value).AreaName;
            switch (_areaname)
            {
                case "食品检测中心":
                    _areaname = "JC";
                    break;
                case "葫芦岛实验室":
                    _areaname = "HL";
                    break;
                case "深圳实验室":
                    _areaname = "SZ";
                    break;
                case "湛江实验室":
                    _areaname = "ZJ";
                    break;
                case "龙口实验室":
                    _areaname = "LK";
                    break;
                case "烹饪加工营养项目":
                    _areaname = "PR";
                    break;
                case "原料食品安全项目":
                    _areaname = "YL";
                    break;
                default:
                    _areaname = "";
                    break;
            }
            var list = _samplebll.GetModelList("DATEDIFF(day , createDate, '" + DateTime.Now.ToShortDateString() + "')=0 and AreaID = " + CurrentUserInfo.AreaID);
            int count = (list != null && list.Count > 0) ? list.Count + 1 : 1;
            samplenum = _areaname + DateTime.Now.ToString("yyyyMMdd") + "—" + count.ToString("D3");
            return samplenum;
        }

        /// <summary>
        /// 导出样品
        /// </summary>
        /// <returns>样品文件</returns>
        [Route("sample/exportsample")]
        public FileResult ExportSample(E_PageParameter ePageParameter)
        {
            List<tb_Sample> list = _dSample.GetSampleList(ePageParameter);
            string filename = "样品列表" + DateTime.Now.ToFileTime() + ".xls";
            string path = Server.MapPath("/upfile/sample/" + filename);
            NPOITools.RenderToExcel<tb_Sample>(list, path);
            return File(path, "application/vnd.ms-excel", filename);
        }

        /// <summary>
        /// 分样单列表
        /// </summary>
        [Route("sample/samplingsheet")]
        public ActionResult SamplingSheet(E_PageParameter ePageParameter)
        {
            List<E_SamplingSheet> list = new List<E_SamplingSheet>();
            list = _dSample.GetSamplingSheetList(ePageParameter);

            ViewBag.SamplingSheetList = list;
            ViewBag.sampleids = ePageParameter.sampleids;

            return View("/views/sample/SamplingSheet.cshtml");
        }

        /// <summary>
        /// 导出分样单
        /// </summary>
        [Route("sample/exportsamplingsheet")]
        public FileResult ExportSamplingSheet(E_PageParameter ePageParameter)
        {
            List<E_SamplingSheet> list = _dSample.GetSamplingSheetList(ePageParameter);
            string filename = "分样单" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".doc";
            string filetemplatepath = Server.MapPath("/upfile/template/分样单模板.docx");
            string path = Server.MapPath("/upfile/samplingsheet/" + filename);
            
            NPOITools.RenderToWord<E_SamplingSheet>(list, filetemplatepath,path);

            return File(path, "application/vnd.ms-word", filename);
        }




        /*以下为旧版代码*****************************************/


        public ActionResult Index()
        {
            try
            {
                ViewData["ddl_company"] = PublicClass.GetAreaList("请选择");
                ViewBag._lid = CurrentUserInfo.AreaID;
            }
            catch
            {

            }
            return View();
        }
        #region 样品管理（新增、修改、删除、列表、导出Excel）
        /// <summary>
        /// 跳转页面
        /// 作者：章建国
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult doSampleInfo(int id)
        {
            ViewBag._PersonnelName = CurrentUserInfo.PersonnelName;
            BLL.RoleManage.T_tb_Area _area = new BLL.RoleManage.T_tb_Area();
            String _areaname = _area.GetModel(CurrentUserInfo.AreaID.Value).AreaName;
            switch (_areaname)
            {
                case "食品检测中心":
                    {
                        _areaname = "JC";
                        break;
                    }
                case "葫芦岛实验室":
                    {
                        _areaname = "HL";
                        break;
                    }
                case "深圳实验室":
                    {
                        _areaname = "SZ";
                        break;
                    }
                case "湛江实验室":
                    {
                        _areaname = "ZJ";
                        break;
                    }
                case "龙口实验室":
                    {
                        _areaname = "LK";
                        break;
                    }
                case "烹饪加工营养项目":
                    {
                        _areaname = "PR";
                        break;
                    }
                case "原料食品安全项目":
                    {
                        _areaname = "YL";
                        break;
                    }
                default:
                    {
                        _areaname = "";
                        break;
                    }
            }
            tb_Sample model = new tb_Sample();
            if (id == 0)
            {
                model.sampleNum = DateTime.Now.ToShortDateString();
                var list = _samplebll.GetModelList("DATEDIFF(day , createDate, '" + DateTime.Now.ToShortDateString() + "')=0 and createUser in (select PersonnelID from tb_InPersonnel where AreaID = " + CurrentUserInfo.AreaID + ")");
                if (list != null && list.Count > 0)
                {
                    int count = list.Count + 1;
                    model.sampleNum = _areaname+DateTime.Now.ToString("yyyyMMdd") + "—" + count.ToString("D3");
                }
                else
                {
                    model.sampleNum = _areaname+DateTime.Now.ToString("yyyyMMdd") + "—001";
                }
                return View(model);
            }
            model = _samplebll.GetModel(id);
            return View(model);
        }
        /// <summary>
        /// 获取所有数据列表
        /// 作者：章建国
        /// </summary>
        /// <returns>将DataTable转换为Json数据格式通过string类型返回</returns>
        public string GetSampleList(int pageNumber, int pageSize, string _searchtext, string _searchtype, int _cid)
        {
            DataTable dt = new DataTable();
            int total = 0;
            try
            {
                string where = " 1=1 ";
                switch (_searchtype)
                {
                    case "样品名称":
                        where += string.Format(" and name like '%%{0}%%' ", _searchtext);
                        break;
                    case "样品人":
                        where += string.Format(" and detectionUser like '%%{0}%%' ", _searchtext);
                        break;
                    case "抽样日期":
                        var times = _searchtext.Split(',');
                        where += string.Format(" and detectionDate >= '{0}' and  detectionDate <= '{1}' ", times[0], times[1]);
                        break;
                }

                if (_cid > 0)
                {
                    where += string.Format(" and createUser in (select PersonnelID from tb_InPersonnel where AreaID = {0}) ", _cid);
                }
                else
                {
                    if (CurrentUserInfo.AreaID > 0 && CurrentUserInfo.DataRange == 3)
                    {
                        int _userid = CurrentUserInfo.PersonnelID;
                        where += string.Format(" and  CreateUser = (select PersonnelID from tb_InPersonnel where AreaID = {0} and PersonnelID = {1}) ", CurrentUserInfo.AreaID, _userid);
                    }
                    else if (CurrentUserInfo.AreaID > 0 && CurrentUserInfo.DataRange == 2)
                    {
                        int _userid = CurrentUserInfo.PersonnelID;
                        where += string.Format(" and createUser in (select PersonnelID from tb_InPersonnel where AreaID = {0}) ", CurrentUserInfo.AreaID);
                    }
                }

                dt = _samplebll.GetListByPage(where, "createDate desc", pageNumber * pageSize - (pageSize - 1), pageNumber * pageSize).Tables[0];
                BLL.PersonnelManage.T_tb_InPersonnel inpersonnel = new BLL.PersonnelManage.T_tb_InPersonnel();
                BLL.ClientManage.T_tb_ClientManage clientManage = new BLL.ClientManage.T_tb_ClientManage();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    try
                    {
                        int userid = Convert.ToInt32(dt.Rows[i]["handleUser"]);
                        dt.Rows[i]["handleUser"] = inpersonnel.GetModel(userid).PersonnelName;
                    }
                    catch
                    {

                    }
                    try
                    {
                        int InspectCompany = Convert.ToInt32(dt.Rows[i]["InspectCompany"]);
                        dt.Rows[i]["InspectCompany"] = clientManage.GetModel(InspectCompany).ClientName;
                    }
                    catch
                    {
                    }
                }
                total = dt.Rows.Count;
                total = _samplebll.GetModelList(where).Count;
            }
            catch { }
            return PublicClass.ToJson(dt, total);
        }

        /// <summary>
        /// 新增、更新数据
        /// 作者：章建国
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string doSampleCU(tb_Sample model)
        {
            string flag = "0";
            try
            {
                model.updateDate = DateTime.Now;
                model.updateUser = CurrentUserInfo.PersonnelID;
                if (model.id > 0)
                {
                    if (model != null && model.id > 0)
                    {
                        model.updateDate = DateTime.Now;
                        model.updateUser = CurrentUserInfo.PersonnelID;
                        if (_samplebll.Update(model))
                        {
                            flag = "1";
                        }
                    }
                }
                else
                {
                    model.createDate = DateTime.Now;
                    model.createUser = CurrentUserInfo.PersonnelID;
                    model.updateDate = DateTime.Now;
                    model.updateUser = CurrentUserInfo.PersonnelID;
                    model.sampleAdmin = CurrentUserInfo.PersonnelName;
                    if (_samplebll.Add(model) > 0)
                    {
                        flag = "1";
                    }
                }
            }
            catch
            {
                flag = "0";
            }
            return flag;
        }

        /// <summary>
        /// 删除数据
        /// 作者：章建国
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <returns></returns>
        public JsonResult Delete_Item(int id)
        {
            string str = "删除失败！";
            try
            {
                if (_samplebll.Delete(id))
                {
                    str = "删除成功！";
                }
            }
            catch
            {
            }
            return Json(str, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 导出药品入库
        /// 作者：章建国
        /// </summary>
        /// <returns></returns>
        public FileResult Export_Sample(string _search, string _searchtype, int _companyid)
        {
            DataTable dt = new DataTable();
            System.IO.MemoryStream stream = new MemoryStream();
            try
            {
                string where = " 1=1 ";
                switch (_searchtype)
                {
                    case "样品名称":
                        where += string.Format(" and name like '%%{0}%%' ", _search);
                        break;
                    case "样品人":
                        where += string.Format(" and detectionUser like '%%{0}%%' ", _search);
                        break;
                    case "抽样日期":
                        var times = _search.Split(',');
                        where += string.Format(" and detectionDate >= '{0}' and  detectionDate <= '{1}' ", times[0], times[1]);
                        break;
                }

                if (_companyid > 0)
                {
                    where += string.Format(" and createUser in (select PersonnelID from tb_InPersonnel where AreaID = {0}) ", _companyid);
                }
                else
                {
                    if (CurrentUserInfo.AreaID > 0 && CurrentUserInfo.DataRange == 3)
                    {
                        int _userid = CurrentUserInfo.PersonnelID;
                        where += string.Format(" and  CreateUser = (select PersonnelID from tb_InPersonnel where AreaID = {0} and PersonnelID = {1}) ", CurrentUserInfo.AreaID, _userid);
                    }
                    else if (CurrentUserInfo.AreaID > 0 && CurrentUserInfo.DataRange == 2)
                    {
                        int _userid = CurrentUserInfo.PersonnelID;
                        where += string.Format(" and createUser in (select PersonnelID from tb_InPersonnel where AreaID = {0}) ", CurrentUserInfo.AreaID);
                    }
                }

                dt = _samplebll.GetList(where).Tables[0];
                BLL.PersonnelManage.T_tb_InPersonnel inpersonnel = new BLL.PersonnelManage.T_tb_InPersonnel();
                BLL.ClientManage.T_tb_ClientManage clientManage = new BLL.ClientManage.T_tb_ClientManage();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    try
                    {
                        int userid = Convert.ToInt32(dt.Rows[i]["handleUser"]);
                        dt.Rows[i]["handleUser"] = inpersonnel.GetModel(userid).PersonnelName;
                    }
                    catch
                    {

                    }
                    try
                    {
                        int InspectCompany = Convert.ToInt32(dt.Rows[i]["InspectCompany"]);
                        dt.Rows[i]["InspectCompany"] = clientManage.GetModel(InspectCompany).ClientName;
                    }
                    catch
                    {
                    }
                }
                stream = PublicClass.ExportSampleToExcel(dt);
            }
            catch { }
            string filename = "样品列表" + DateTime.Now.ToFileTime() + ".xls";
            return File(stream, "application/vnd.ms-excel", filename);
        }

        public String GetInspectAddressById(Int32 id)
        {
            var list = new BLL.ClientManage.T_tb_ClientManage().GetModel(id);
            return list.Address;
        }
        #endregion

        #region 抽样情况
        /// <summary>
        /// 跳转页面
        /// 作者：章建国
        /// </summary>
        /// <param name="_sid"></param>
        /// <returns></returns>
        public ActionResult doDetectionInfo(int _sid)
        {
            tb_Detection model = new tb_Detection();
            try
            {
                model = _detectionbll.GetModelBySID(_sid);
            }
            catch
            {

            }
            if (model == null)
            {
                model = new tb_Detection();
            }
            model.sId = _sid;
            model.name = _samplebll.GetModel(_sid).name;
            return View(model);
        }

        /// <summary>
        /// 新增、更新数据
        /// 作者：章建国
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string doDetectionCU(tb_Detection model)
        {
            string flag = "0";
            try
            {
                model.updateDate = DateTime.Now;
                model.updateUser = CurrentUserInfo.PersonnelID;
                if (model.id > 0)
                {
                    _detectionbll.Delete(model.id);
                }
                model.createDate = DateTime.Now;
                model.createUser = CurrentUserInfo.PersonnelID;
                model.updateDate = DateTime.Now;
                model.updateUser = CurrentUserInfo.PersonnelID;
                if (_detectionbll.Add(model) > 0)
                {
                    flag = "1";
                }
            }
            catch
            {
                flag = "0";
            }
            return flag;
        }
        #endregion

        #region 检验标准
        public ActionResult doTestStanderd(int _sampleId)
        {
            ViewBag._sampleId = _sampleId;
            return View();
        }

        /// <summary>
        /// 获取所有数据列表
        /// 作者：章建国
        /// </summary>
        /// <returns>将DataTable转换为Json数据格式通过string类型返回</returns>
        public string GetteststandardList(int pageNumber, int pageSize, string _search, string _searchtype, int _sampleId)
        {
            DataTable dt = new DataTable();
            int total = 0;
            try
            {
                string where = " temp1 = " + _sampleId;

                dt = _teststandardbll.GetListByPage(where, "updateDate", pageNumber * pageSize - (pageSize - 1), pageNumber * pageSize).Tables[0];
                total = dt.Rows.Count;
                total = _teststandardbll.GetModelList(where).Count;
            }
            catch { }
            return PublicClass.ToJson(dt, total);
        }

        public ActionResult doTestStanderdInfo(int _teststanderdId, int _sampleId)
        {
            tb_TestStandard model = new tb_TestStandard();
            model.temp1 = _sampleId.ToString();
            if (_teststanderdId > 0)
            {
                model = _teststandardbll.GetModel(_teststanderdId);
            }
            return View(model);
        }

        public string SaveTestStanderdInfo(tb_TestStandard _testStandard)
        {
            _testStandard.updateDate = DateTime.Now;
            _testStandard.updateUser = CurrentUserInfo.PersonnelID;
            string flag = "0";
            if (_testStandard.id > 0)
            {
                if (_teststandardbll.Update(_testStandard))
                {
                    flag = "1";
                }
            }
            else
            {
                _testStandard.createDate = DateTime.Now;
                _testStandard.createUser = CurrentUserInfo.PersonnelID;
                if (_teststandardbll.Add(_testStandard) > 0)
                {
                    flag = "1";
                }
            }
            return flag;
        }

        /// <summary>
        /// 删除数据
        /// 作者：章建国
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <returns></returns>
        public JsonResult Delete_teststandard(int id)
        {
            string str = "删除失败！";
            try
            {
                if (_teststandardbll.Delete(id))
                {
                    str = "删除成功！";
                }
            }
            catch
            {
            }
            return Json(str, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 销毁数据
        /// 作者：章建国
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <returns></returns>
        public JsonResult Destroy_Item(int id)
        {
            string str = "销毁失败！";
            try
            {
                tb_Sample model = new tb_Sample();
                model = _samplebll.GetModel(id);
                model.handleUser = CurrentUserInfo.PersonnelID;
                model.handleDate = DateTime.Now;
                if (_samplebll.Update(model))
                {
                    str = "销毁成功！";
                }
            }
            catch
            {
            }
            return Json(str, JsonRequestBehavior.AllowGet);
        }
        
        #endregion 检验标准

        public JsonResult GetProjectListForDropDownList(string q)
        {
            var list = new BLL.Laboratory.T_tb_Project().GetModelList(" ProjectName like '%%" + q + "%%' or ExpeMethod like '%%" + q + "%%'");
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetClientListForDropDownList(string q)
        {
            var list = new BLL.ClientManage.T_tb_ClientManage().GetModelList(" ClientName like '%%" + q + "%%' ");
            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}
