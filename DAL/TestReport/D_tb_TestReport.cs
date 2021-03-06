﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using Model.TestReport;
using Dapper;

namespace DAL.TestReport
{
    /// <summary>
    /// 数据访问类:tb_TestReport
    /// </summary>
    public partial class D_tb_TestReport
    {
        public D_tb_TestReport()
        { }
        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ReportID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from tb_TestReport");
            strSql.Append(" where ReportID=@ReportID");
            SqlParameter[] parameters = {
					new SqlParameter("@ReportID", SqlDbType.Int,4)
};
            parameters[0].Value = ReportID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(E_tb_TestReport model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into tb_TestReport(");
            strSql.Append("RecordIDS,TaskNoS,ReportNo,SampleNum,SampleName,Department,TestType,IssuedTime,TestingCompany,Specifications,ProductionTime,Packing,productNum,ToSampleMode,SendTestAddress,SamplingPlace,SamplingCompany,SamplingPersonnel,SamplingTime,TestTime,ShelfLife,TestBasis,Conclusion,Remarks,Explain,ApprovalPersonnelID,examinePersonnelID,MainTestPersonnelID,FilePath,AreaID,EditPersonnelID,AddTime,UpdateTime)");
            strSql.Append(" values (");
            strSql.Append("@RecordIDS,@TaskNoS,@ReportNo,@SampleNum,@SampleName,@Department,@TestType,@IssuedTime,@TestingCompany,@Specifications,@ProductionTime,@Packing,@productNum,@ToSampleMode,@SendTestAddress,@SamplingPlace,@SamplingCompany,@SamplingPersonnel,@SamplingTime,@TestTime,@ShelfLife,@TestBasis,@Conclusion,@Remarks,@Explain,@ApprovalPersonnelID,@examinePersonnelID,@MainTestPersonnelID,@FilePath,@AreaID,@EditPersonnelID,@AddTime,@UpdateTime)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@RecordIDS", SqlDbType.NVarChar,100),
					new SqlParameter("@TaskNoS", SqlDbType.NVarChar,200),
					new SqlParameter("@ReportNo", SqlDbType.NVarChar,100),
					new SqlParameter("@SampleNum", SqlDbType.NVarChar,100),
					new SqlParameter("@SampleName", SqlDbType.NVarChar,100),
					new SqlParameter("@Department", SqlDbType.NVarChar,50),
					new SqlParameter("@TestType", SqlDbType.Int,4),
					new SqlParameter("@IssuedTime", SqlDbType.DateTime),
					new SqlParameter("@TestingCompany", SqlDbType.NVarChar,50),
					new SqlParameter("@Specifications", SqlDbType.NVarChar,100),
					new SqlParameter("@ProductionTime", SqlDbType.DateTime),
					new SqlParameter("@Packing", SqlDbType.NVarChar,100),
					new SqlParameter("@productNum", SqlDbType.NVarChar,100),
					new SqlParameter("@ToSampleMode", SqlDbType.NVarChar,100),
					new SqlParameter("@SendTestAddress", SqlDbType.NVarChar,200),
					new SqlParameter("@SamplingPlace", SqlDbType.NVarChar,200),
					new SqlParameter("@SamplingCompany", SqlDbType.NVarChar,100),
					new SqlParameter("@SamplingPersonnel", SqlDbType.NVarChar,100),
					new SqlParameter("@SamplingTime", SqlDbType.DateTime),
					new SqlParameter("@TestTime", SqlDbType.DateTime),
					new SqlParameter("@ShelfLife", SqlDbType.NVarChar,200),
					new SqlParameter("@TestBasis", SqlDbType.NVarChar,200),
					new SqlParameter("@Conclusion", SqlDbType.NVarChar,200),
					new SqlParameter("@Remarks", SqlDbType.NVarChar,200),
					new SqlParameter("@Explain", SqlDbType.Text),
					new SqlParameter("@ApprovalPersonnelID", SqlDbType.Int,4),
					new SqlParameter("@examinePersonnelID", SqlDbType.Int,4),
					new SqlParameter("@MainTestPersonnelID", SqlDbType.Int,4),
					new SqlParameter("@FilePath", SqlDbType.NVarChar,50),
					new SqlParameter("@AreaID", SqlDbType.Int,4),
					new SqlParameter("@EditPersonnelID", SqlDbType.Int,4),
					new SqlParameter("@AddTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime)};
            parameters[0].Value = model.RecordIDS;
            parameters[1].Value = model.TaskNoS;
            parameters[2].Value = model.ReportNo;
            parameters[3].Value = model.SampleNum;
            parameters[4].Value = model.SampleName;
            parameters[5].Value = model.Department;
            parameters[6].Value = model.TestType;
            parameters[7].Value = model.IssuedTime;
            parameters[8].Value = model.TestingCompany;
            parameters[9].Value = model.Specifications;
            parameters[10].Value = model.ProductionTime;
            parameters[11].Value = model.Packing;
            parameters[12].Value = model.productNum;
            parameters[13].Value = model.ToSampleMode;
            parameters[14].Value = model.SendTestAddress;
            parameters[15].Value = model.SamplingPlace;
            parameters[16].Value = model.SamplingCompany;
            parameters[17].Value = model.SamplingPersonnel;
            parameters[18].Value = model.SamplingTime;
            parameters[19].Value = model.TestTime;
            parameters[20].Value = model.ShelfLife;
            parameters[21].Value = model.TestBasis;
            parameters[22].Value = model.Conclusion;
            parameters[23].Value = model.Remarks;
            parameters[24].Value = model.Explain;
            parameters[25].Value = model.ApprovalPersonnelID;
            parameters[26].Value = model.examinePersonnelID;
            parameters[27].Value = model.MainTestPersonnelID;
            parameters[28].Value = model.FilePath;
            parameters[29].Value = model.AreaID;
            parameters[30].Value = model.EditPersonnelID;
            parameters[31].Value = model.AddTime;
            parameters[32].Value = model.UpdateTime;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(E_tb_TestReport model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update tb_TestReport set ");
            strSql.Append("RecordIDS=@RecordIDS,");
            strSql.Append("TaskNoS=@TaskNoS,");
            strSql.Append("ReportNo=@ReportNo,");
            strSql.Append("SampleNum=@SampleNum,");
            strSql.Append("SampleName=@SampleName,");
            strSql.Append("Department=@Department,");
            strSql.Append("TestType=@TestType,");
            strSql.Append("IssuedTime=@IssuedTime,");
            strSql.Append("TestingCompany=@TestingCompany,");
            strSql.Append("Specifications=@Specifications,");
            strSql.Append("ProductionTime=@ProductionTime,");
            strSql.Append("Packing=@Packing,");
            strSql.Append("productNum=@productNum,");
            strSql.Append("ToSampleMode=@ToSampleMode,");
            strSql.Append("SendTestAddress=@SendTestAddress,");
            strSql.Append("SamplingPlace=@SamplingPlace,");
            strSql.Append("SamplingCompany=@SamplingCompany,");
            strSql.Append("SamplingPersonnel=@SamplingPersonnel,");
            strSql.Append("SamplingTime=@SamplingTime,");
            strSql.Append("TestTime=@TestTime,");
            strSql.Append("ShelfLife=@ShelfLife,");
            strSql.Append("TestBasis=@TestBasis,");
            strSql.Append("Conclusion=@Conclusion,");
            strSql.Append("Remarks=@Remarks,");
            strSql.Append("Explain=@Explain,");
            strSql.Append("ApprovalPersonnelID=@ApprovalPersonnelID,");
            strSql.Append("examinePersonnelID=@examinePersonnelID,");
            strSql.Append("MainTestPersonnelID=@MainTestPersonnelID,");
            strSql.Append("FilePath=@FilePath,");
            strSql.Append("AreaID=@AreaID,");
            strSql.Append("EditPersonnelID=@EditPersonnelID,");
            strSql.Append("AddTime=@AddTime,");
            strSql.Append("UpdateTime=@UpdateTime");
            strSql.Append(" where ReportID=@ReportID");
            SqlParameter[] parameters = {
					new SqlParameter("@RecordIDS", SqlDbType.NVarChar,100),
					new SqlParameter("@TaskNoS", SqlDbType.NVarChar,200),
					new SqlParameter("@ReportNo", SqlDbType.NVarChar,100),
					new SqlParameter("@SampleNum", SqlDbType.NVarChar,100),
					new SqlParameter("@SampleName", SqlDbType.NVarChar,100),
					new SqlParameter("@Department", SqlDbType.NVarChar,50),
					new SqlParameter("@TestType", SqlDbType.Int,4),
					new SqlParameter("@IssuedTime", SqlDbType.DateTime),
					new SqlParameter("@TestingCompany", SqlDbType.NVarChar,50),
					new SqlParameter("@Specifications", SqlDbType.NVarChar,100),
					new SqlParameter("@ProductionTime", SqlDbType.DateTime),
					new SqlParameter("@Packing", SqlDbType.NVarChar,100),
					new SqlParameter("@productNum", SqlDbType.NVarChar,100),
					new SqlParameter("@ToSampleMode", SqlDbType.NVarChar,100),
					new SqlParameter("@SendTestAddress", SqlDbType.NVarChar,200),
					new SqlParameter("@SamplingPlace", SqlDbType.NVarChar,200),
					new SqlParameter("@SamplingCompany", SqlDbType.NVarChar,100),
					new SqlParameter("@SamplingPersonnel", SqlDbType.NVarChar,100),
					new SqlParameter("@SamplingTime", SqlDbType.DateTime),
					new SqlParameter("@TestTime", SqlDbType.DateTime),
					new SqlParameter("@ShelfLife", SqlDbType.NVarChar,200),
					new SqlParameter("@TestBasis", SqlDbType.NVarChar,200),
					new SqlParameter("@Conclusion", SqlDbType.NVarChar,200),
					new SqlParameter("@Remarks", SqlDbType.NVarChar,200),
					new SqlParameter("@Explain", SqlDbType.Text),
					new SqlParameter("@ApprovalPersonnelID", SqlDbType.Int,4),
					new SqlParameter("@examinePersonnelID", SqlDbType.Int,4),
					new SqlParameter("@MainTestPersonnelID", SqlDbType.Int,4),
					new SqlParameter("@FilePath", SqlDbType.NVarChar,50),
					new SqlParameter("@AreaID", SqlDbType.Int,4),
					new SqlParameter("@EditPersonnelID", SqlDbType.Int,4),
					new SqlParameter("@AddTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@ReportID", SqlDbType.Int,4)};
            parameters[0].Value = model.RecordIDS;
            parameters[1].Value = model.TaskNoS;
            parameters[2].Value = model.ReportNo;
            parameters[3].Value = model.SampleNum;
            parameters[4].Value = model.SampleName;
            parameters[5].Value = model.Department;
            parameters[6].Value = model.TestType;
            parameters[7].Value = model.IssuedTime;
            parameters[8].Value = model.TestingCompany;
            parameters[9].Value = model.Specifications;
            parameters[10].Value = model.ProductionTime;
            parameters[11].Value = model.Packing;
            parameters[12].Value = model.productNum;
            parameters[13].Value = model.ToSampleMode;
            parameters[14].Value = model.SendTestAddress;
            parameters[15].Value = model.SamplingPlace;
            parameters[16].Value = model.SamplingCompany;
            parameters[17].Value = model.SamplingPersonnel;
            parameters[18].Value = model.SamplingTime;
            parameters[19].Value = model.TestTime;
            parameters[20].Value = model.ShelfLife;
            parameters[21].Value = model.TestBasis;
            parameters[22].Value = model.Conclusion;
            parameters[23].Value = model.Remarks;
            parameters[24].Value = model.Explain;
            parameters[25].Value = model.ApprovalPersonnelID;
            parameters[26].Value = model.examinePersonnelID;
            parameters[27].Value = model.MainTestPersonnelID;
            parameters[28].Value = model.FilePath;
            parameters[29].Value = model.AreaID;
            parameters[30].Value = model.EditPersonnelID;
            parameters[31].Value = model.AddTime;
            parameters[32].Value = model.UpdateTime;
            parameters[33].Value = model.ReportID;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int ReportID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from tb_TestReport ");
            strSql.Append(" where ReportID=@ReportID");
            SqlParameter[] parameters = {
					new SqlParameter("@ReportID", SqlDbType.Int,4)
};
            parameters[0].Value = ReportID;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string ReportIDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from tb_TestReport ");
            strSql.Append(" where ReportID in (" + ReportIDlist + ")  ");
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public E_tb_TestReport GetModel(int ReportID)
        {
            E_tb_TestReport model = null;
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT top 1 
                              A.[ReportID]
                              , A.[RecordIDS]
                              , A.[TaskNoS]
                              , A.[ReportNo]
                              , A.[SampleNum]
                              ,B.name as SampleName  --, A.[SampleName] //样品名称
                              ,case when B.isDetection = 1 then B.detectionCompany else C.ClientName end as [Department] --, A.[Department] 动态获取样品管理对应的  送/抽检单位
                              , A.[TestType]
                              , A.[IssuedTime]
                              , A.[TestingCompany]
                              , A.[Specifications]
                              , A.[ProductionTime]
                              , A.[Packing]
                              , A.[productNum]
                              , A.[ToSampleMode]
                              , A.[SendTestAddress]
                              ,B.detectionAdress as SamplingPlace    --, A.[SamplingPlace] //抽样地点
                              ,B.detectionCompany as SamplingCompany --, A.[SamplingCompany] //抽样单位
                              , A.[SamplingPersonnel]
                              , A.[SamplingTime]
                              , A.[TestTime]
                              , A.[ShelfLife]
                              , A.[TestBasis]
                              , A.[Conclusion]
                              , A.[Remarks]
                              , A.[Explain]
                              , A.[ApprovalPersonnelID]
                              , A.[examinePersonnelID]
                              , A.[MainTestPersonnelID]
                              , A.[FilePath]
                              , A.[AreaID]
                              , A.[EditPersonnelID]
                              , A.[AddTime]
                              , A.[UpdateTime]
                              , A.[issecrecy]
                              , A.[secrecyexaminepid]
                              , A.[setsecrecypid]
                          FROM [tb_TestReport] as A
                               left join tb_Sample as B on A.[SampleNum] = B.[SampleNum]
                               left join tb_ClientManage as C on B.InspectCompany = C.ClientID 
                          where ReportID=@ReportID");
            using (IDbConnection conn = new SqlConnection(PubConstant.GetConnectionString()))
            {
                 model = conn.Query<E_tb_TestReport>(strSql.ToString(), new { ReportID = ReportID }).FirstOrDefault();
                return model;
            }
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT top 1 
                              A.[ReportID]
                              , A.[RecordIDS]
                              , A.[TaskNoS]
                              , A.[ReportNo]
                              , A.[SampleNum]
                              ,B.name as SampleName  --, A.[SampleName] //样品名称
                              ,case when B.isDetection = 1 then B.detectionCompany else C.ClientName end as [Department] --, A.[Department] 动态获取样品管理对应的  送/抽检单位
                              , A.[TestType]
                              , A.[IssuedTime]
                              , A.[TestingCompany]
                              , A.[Specifications]
                              , A.[ProductionTime]
                              , A.[Packing]
                              , A.[productNum]
                              , A.[ToSampleMode]
                              , A.[SendTestAddress]
                              ,B.detectionAdress as SamplingPlace    --, A.[SamplingPlace] //抽样地点
                              ,B.detectionCompany as SamplingCompany --, A.[SamplingCompany] //抽样单位
                              , A.[SamplingPersonnel]
                              , A.[SamplingTime]
                              , A.[TestTime]
                              , A.[ShelfLife]
                              , A.[TestBasis]
                              , A.[Conclusion]
                              , A.[Remarks]
                              , A.[Explain]
                              , A.[ApprovalPersonnelID]
                              , A.[examinePersonnelID]
                              , A.[MainTestPersonnelID]
                              , A.[FilePath]
                              , A.[AreaID]
                              , A.[EditPersonnelID]
                              , A.[AddTime]
                              , A.[UpdateTime]
                              , A.[issecrecy]
                              , A.[secrecyexaminepid]
                              , A.[setsecrecypid]
                          FROM [tb_TestReport] as A
                               left join tb_Sample as B on A.[SampleNum] = B.[SampleNum]
                               left join tb_ClientManage as C on B.InspectCompany = C.ClientID ");
            
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            string sqltop = "";
            if (Top > 0)
            {
                sqltop = $" top {Top.ToString()} ";
            }

            string sqlwhere = "";
            if (strWhere.Trim() != "")
            {
                sqlwhere = $" where {strWhere.Trim()}";
            }

            string sqlorder = "";
                if (!string.IsNullOrEmpty(filedOrder))
            {
                sqlorder = $" order by {filedOrder}";
            }

            StringBuilder strSql = new StringBuilder();
            strSql.Append($@"SELECT {sqltop}
                              A.[ReportID]
                              , A.[RecordIDS]
                              , A.[TaskNoS]
                              , A.[ReportNo]
                              , A.[SampleNum]
                              ,B.name as SampleName  --, A.[SampleName] //样品名称
                              ,case when B.isDetection = 1 then B.detectionCompany else C.ClientName end as [Department] --, A.[Department] 动态获取样品管理对应的  送/抽检单位
                              , A.[TestType]
                              , A.[IssuedTime]
                              , A.[TestingCompany]
                              , A.[Specifications]
                              , A.[ProductionTime]
                              , A.[Packing]
                              , A.[productNum]
                              , A.[ToSampleMode]
                              , A.[SendTestAddress]
                              ,B.detectionAdress as SamplingPlace    --, A.[SamplingPlace] //抽样地点
                              ,B.detectionCompany as SamplingCompany --, A.[SamplingCompany] //抽样单位
                              , A.[SamplingPersonnel]
                              , A.[SamplingTime]
                              , A.[TestTime]
                              , A.[ShelfLife]
                              , A.[TestBasis]
                              , A.[Conclusion]
                              , A.[Remarks]
                              , A.[Explain]
                              , A.[ApprovalPersonnelID]
                              , A.[examinePersonnelID]
                              , A.[MainTestPersonnelID]
                              , A.[FilePath]
                              , A.[AreaID]
                              , A.[EditPersonnelID]
                              , A.[AddTime]
                              , A.[UpdateTime]
                              , A.[issecrecy]
                              , A.[secrecyexaminepid]
                              , A.[setsecrecypid]
                          FROM [tb_TestReport] as A
                               left join tb_Sample as B on A.[SampleNum] = B.[SampleNum]
                               left join tb_ClientManage as C on B.InspectCompany = C.ClientID 
                               {sqlwhere} {sqlorder}");
            
            return DbHelperSQL.Query(strSql.ToString());
        }

      

        #endregion  Method
        
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex, ref int total)
        {
            string sqlorder = "order by T.ReportID desc";
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                sqlorder = $"order by T.{orderby}";
            }

            string sqlwhere = "";
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                sqlwhere = $" WHERE {strWhere}";
            }
            
            StringBuilder strSql = new StringBuilder();
            strSql.Append($@"SELECT * FROM (
                                SELECT ROW_NUMBER() OVER ({sqlorder}) AS Row, 
                                       T.[ReportID], T.[RecordIDS], T.[TaskNoS], T.[ReportNo], T.[SampleNum]
                                       ,F.name as SampleName  --, A.[SampleName] //样品名称
                                       ,case when F.isDetection = 1 then F.detectionCompany else G.ClientName end as [Department] --, T.[Department] 动态获取样品管理对应的  送/抽检单位
                                       , T.[TestType], T.[IssuedTime], T.[TestingCompany], T.[Specifications], T.[ProductionTime]
                                       , T.[Packing], T.[productNum], T.[ToSampleMode], T.[SendTestAddress]
                                       ,F.detectionAdress as SamplingPlace    --, A.[SamplingPlace] //抽样地点
                                       ,F.detectionCompany as SamplingCompany --, A.[SamplingCompany] //抽样单位
                                       , T.[SamplingPersonnel], T.[SamplingTime], T.[TestTime], T.[ShelfLife]
                                       , T.[TestBasis], T.[Conclusion], T.[Remarks], T.[Explain], T.[ApprovalPersonnelID]
                                       , T.[examinePersonnelID], T.[MainTestPersonnelID], T.[FilePath], T.[AreaID], T.[EditPersonnelID]
                                       , T.[AddTime], T.[UpdateTime], T.[issecrecy], T.[secrecyexaminepid], T.[setsecrecypid]
                                       ,B.PersonnelName as ApprovalPersonnelName,C.PersonnelName as examinePersonnelName
                                       ,D.PersonnelName as MainTestPersonnelName,E.TypeName as TestTypeName  
                                from tb_TestReport T 
                                    left join tb_InPersonnel as B on T.ApprovalPersonnelID=B.PersonnelID
                                    left join tb_InPersonnel as C on T.examinePersonnelID=C.PersonnelID
                                    left join tb_InPersonnel as D on T.MainTestPersonnelID=D.PersonnelID
                                    left join tb_TypeDict as E on T.TestType=E.TypeID 
                                    left join tb_Sample as F on T.[SampleNum] = F.[SampleNum]
                                    left join tb_ClientManage as G on F.InspectCompany = G.ClientID 
                            {sqlwhere} ) TT");

            total = DbHelperSQL.GetCount(strSql.ToString());
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 根据添加时间获取最大编号值
        /// </summary>
        /// <param name="AddTime"></param>
        /// <returns></returns>
        public int GetMaxNoByAddTime(DateTime AddTime)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select COUNT(1) from tb_TestReport where AddTime=CAST('" + AddTime + "' as datetime)");
            return Convert.ToInt32(DbHelperSQL.GetSingle(strSql.ToString()));
        }

        /// <summary>
        /// 批量批准
        /// </summary>
        /// <param name="ids">检验报告id集合</param>
        /// <param name="examinePersonnelID">审核人</param>
        /// <returns>返回是否执行成功</returns>
        public bool Approval(string ids,int ApprovalPersonnelID,DateTime IssuedTime)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append($"update tb_TestReport set ApprovalPersonnelID={ApprovalPersonnelID},IssuedTime=cast('{IssuedTime.ToString()}' as datetime) where ReportID in ({ids}) ");//  and (ApprovalPersonnelID is null or ApprovalPersonnelID=0)

            using (IDbConnection conn = new SqlConnection(PubConstant.GetConnectionString()))
            {
                int count = conn.Execute(strSql.ToString());
                return (count > 0);
            }
        }


        /// <summary>
        /// 批量审核
        /// </summary>
        /// <param name="ids">检验报告id集合</param>
        /// <param name="examinePersonnelID">审核人</param>
        /// <returns>返回是否执行成功</returns>
        public bool Examine(string ids, int examinePersonnelID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append($"update tb_TestReport set examinePersonnelID={examinePersonnelID} where ReportID in ({ids}) and (examinePersonnelID is null or examinePersonnelID=0)");

            using (IDbConnection conn = new SqlConnection(PubConstant.GetConnectionString()))
            {
                int count = conn.Execute(strSql.ToString());
                return (count > 0);
            }
        }

        /// <summary>
        /// 设置保密
        /// </summary>
        /// <param name="model">参数实体</param>
        /// <returns>返回是否执行成功</returns>
        public bool SetSecrecy(E_tb_TestReport model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append($"update tb_TestReport set issecrecy=1,secrecyexaminepid=@secrecyexaminepid,setsecrecypid=@setsecrecypid where ReportID=@ReportID");
            using (IDbConnection conn = new SqlConnection(PubConstant.GetConnectionString()))
            {
                int count = conn.Execute(strSql.ToString(), model);
                return (count > 0);
            }
        }

        /// <summary>
        /// 取消保密
        /// </summary>
        /// <param name="ReportID">检验报告ID</param>
        /// <returns>返回是否执行成功</returns>
        public bool CancelSecrecy(int ReportID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append($"update tb_TestReport set issecrecy=0,secrecyexaminepid=0,setsecrecypid=0 where ReportID={ReportID}");
            using (IDbConnection conn = new SqlConnection(PubConstant.GetConnectionString()))
            {
                int count = conn.Execute(strSql.ToString());
                return (count > 0);
            }
        }

        /// <summary>
        /// 更新实验计划审核状态
        /// </summary>
        /// <param name="ReportIDS">检验报告ID集合</param>
        /// <returns>是否执行完毕</returns>
        public bool ExpePlanPass(string ReportIDS)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append($@"
                --更新错误数据
                update tb_ExpePlan set [status]=1 where PlanID in (
                select tasknos from dbo.tb_TestReport where ReportID in ({ReportIDS}) and ApprovalPersonnelID>0 and examinePersonnelID>0 and MainTestPersonnelID>0 and tasknos not like '%,%'
                ) and [status]!=1
                --创建临时表
                create table #T_PlanID
                (
                PlanID nvarchar(max)
                )
                --填充临时表数据
                declare T_cursor cursor for select tasknos from dbo.tb_TestReport where ReportID in ({ReportIDS}) and ApprovalPersonnelID>0 and examinePersonnelID>0 and MainTestPersonnelID>0 and tasknos like '%,%'
                 open T_cursor 
                  declare @tasknos nvarchar(max)
                  --开始遍历数据
                  fetch next from T_cursor into @tasknos
                  while @@fetch_status=0 
                   begin  
                   insert into #T_PlanID (PlanID)
                   SELECT * FROM [splitString] (@tasknos,',')
                   fetch next from T_cursor into @tasknos
                   end 
                  close T_cursor 
                  deallocate T_cursor
                --更新数据
                update tb_ExpePlan set [status]=1 where PlanID in (
                select PlanID from #T_PlanID
                ) and [status]!=1
                --删除临时表
                drop table #T_PlanID
            ");

            using (IDbConnection conn = new SqlConnection(PubConstant.GetConnectionString()))
            {
                conn.Execute(strSql.ToString());
                return true;
            }
        }

    }
}
