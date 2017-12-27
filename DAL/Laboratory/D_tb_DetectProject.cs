using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using Model.Laboratory;
using Comp;

namespace DAL.Laboratory
{
    /// <summary>
    /// 数据访问类:D_tb_DetectProject
    /// </summary>
    public partial class D_tb_DetectProject
    {
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ProjectID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from tb_DetectProject");
            strSql.Append(" where ProjectID=@ProjectID");
            SqlParameter[] parameters = {
					new SqlParameter("@ProjectID", SqlDbType.Int,4)
};
            parameters[0].Value = ProjectID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }
        
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(E_tb_DetectProject model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into tb_DetectProject(");
            strSql.Append("LaboratoryID,RelationProjectID,TaskNo,ProjectName,DetectTime,HeadPersonnelID,MainPerson,Tel,UpdateTime)");
            strSql.Append(" values (");
            strSql.Append("@LaboratoryID,@RelationProjectID,@TaskNo,@ProjectName,@DetectTime,@HeadPersonnelID,@MainPerson,@Tel,@UpdateTime)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@LaboratoryID", SqlDbType.Int,4),
					new SqlParameter("@RelationProjectID", SqlDbType.Int,4),
					new SqlParameter("@TaskNo", SqlDbType.NVarChar,50),
					new SqlParameter("@ProjectName", SqlDbType.NVarChar,50),
					new SqlParameter("@DetectTime", SqlDbType.DateTime),
					new SqlParameter("@HeadPersonnelID", SqlDbType.Int,4),
					new SqlParameter("@MainPerson", SqlDbType.NVarChar,50),
					new SqlParameter("@Tel", SqlDbType.NVarChar,50),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime)};
            parameters[0].Value = model.LaboratoryID;
            parameters[1].Value = model.RelationProjectID;
            parameters[2].Value = model.TaskNo;
            parameters[3].Value = model.ProjectName;
            parameters[4].Value = model.DetectTime;
            parameters[5].Value = model.HeadPersonnelID;
            parameters[6].Value = model.MainPerson;
            parameters[7].Value = model.Tel;
            parameters[8].Value = model.UpdateTime;

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
        public bool Update(E_tb_DetectProject model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update tb_DetectProject set ");
            strSql.Append("LaboratoryID=@LaboratoryID,");
            strSql.Append("RelationProjectID=@RelationProjectID,");
            strSql.Append("TaskNo=@TaskNo,");
            strSql.Append("ProjectName=@ProjectName,");
            strSql.Append("DetectTime=@DetectTime,");
            strSql.Append("HeadPersonnelID=@HeadPersonnelID,");
            strSql.Append("MainPerson=@MainPerson,");
            strSql.Append("Tel=@Tel,");
            strSql.Append("UpdateTime=@UpdateTime");
            strSql.Append(" where ProjectID=@ProjectID");
            SqlParameter[] parameters = {
					new SqlParameter("@LaboratoryID", SqlDbType.Int,4),
					new SqlParameter("@RelationProjectID", SqlDbType.Int,4),
					new SqlParameter("@TaskNo", SqlDbType.NVarChar,50),
					new SqlParameter("@ProjectName", SqlDbType.NVarChar,50),
					new SqlParameter("@DetectTime", SqlDbType.DateTime),
					new SqlParameter("@HeadPersonnelID", SqlDbType.Int,4),
					new SqlParameter("@MainPerson", SqlDbType.NVarChar,50),
					new SqlParameter("@Tel", SqlDbType.NVarChar,50),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@ProjectID", SqlDbType.Int,4)};
            parameters[0].Value = model.LaboratoryID;
            parameters[1].Value = model.RelationProjectID;
            parameters[2].Value = model.TaskNo;
            parameters[3].Value = model.ProjectName;
            parameters[4].Value = model.DetectTime;
            parameters[5].Value = model.HeadPersonnelID;
            parameters[6].Value = model.MainPerson;
            parameters[7].Value = model.Tel;
            parameters[8].Value = model.UpdateTime;
            parameters[9].Value = model.ProjectID;

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
        public bool Delete(int ProjectID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from tb_DetectProject ");
            strSql.Append(" where ProjectID=@ProjectID");
            SqlParameter[] parameters = {
					new SqlParameter("@ProjectID", SqlDbType.Int,4)
};
            parameters[0].Value = ProjectID;

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
        public bool DeleteList(string ProjectIDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from tb_DetectProject ");
            strSql.Append(" where ProjectID in (" + ProjectIDlist + ")  ");
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
        public E_tb_DetectProject GetModel(int ProjectID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ProjectID,LaboratoryID,RelationProjectID,TaskNo,ProjectName,DetectTime,HeadPersonnelID,MainPerson,Tel,UpdateTime from tb_DetectProject ");
            strSql.Append(" where ProjectID=@ProjectID");
            SqlParameter[] parameters = {
					new SqlParameter("@ProjectID", SqlDbType.Int,4)
};
            parameters[0].Value = ProjectID;

            E_tb_DetectProject model = new E_tb_DetectProject();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ProjectID"].ToString() != "")
                {
                    model.ProjectID = int.Parse(ds.Tables[0].Rows[0]["ProjectID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["LaboratoryID"].ToString() != "")
                {
                    model.LaboratoryID = int.Parse(ds.Tables[0].Rows[0]["LaboratoryID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["RelationProjectID"].ToString() != "")
                {
                    model.RelationProjectID = int.Parse(ds.Tables[0].Rows[0]["RelationProjectID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["TaskNo"] != null)
                {
                    model.TaskNo = ds.Tables[0].Rows[0]["TaskNo"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ProjectName"] != null)
                {
                    model.ProjectName = ds.Tables[0].Rows[0]["ProjectName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["DetectTime"].ToString() != "")
                {
                    model.DetectTime = DateTime.Parse(ds.Tables[0].Rows[0]["DetectTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["HeadPersonnelID"].ToString() != "")
                {
                    model.HeadPersonnelID = int.Parse(ds.Tables[0].Rows[0]["HeadPersonnelID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["MainPerson"] != null)
                {
                    model.MainPerson = ds.Tables[0].Rows[0]["MainPerson"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Tel"] != null)
                {
                    model.Tel = ds.Tables[0].Rows[0]["Tel"].ToString();
                }
                if (ds.Tables[0].Rows[0]["UpdateTime"].ToString() != "")
                {
                    model.UpdateTime = DateTime.Parse(ds.Tables[0].Rows[0]["UpdateTime"].ToString());
                }
                return model;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ProjectID,LaboratoryID,RelationProjectID,TaskNo,ProjectName,DetectTime,HeadPersonnelID,MainPerson,Tel,UpdateTime ");
            strSql.Append(" FROM tb_DetectProject ");
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
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" ProjectID,LaboratoryID,RelationProjectID,TaskNo,ProjectName,DetectTime,HeadPersonnelID,MainPerson,Tel,UpdateTime ");
            strSql.Append(" FROM tb_DetectProject ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }
            
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex, ref int total)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.ProjectID desc");
            }
            strSql.Append(")AS Row, T.*,B.ExpeType,case when C.PlanTypeID=1 then '计划内' else '计划外' end as PlanType ");
            strSql.Append("from tb_DetectProject T left join tb_Project as B on T.RelationProjectID=B.ProjectID ");
            strSql.Append("left join tb_ExpePlan as C on T.RelationProjectID=C.ProjectID and T.TaskNo=C.TaskNo ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            total = DbHelperSQL.GetCount(strSql.ToString());
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }





        /// <summary>
        /// 获取列表查询语句
        /// </summary>
        public string GetSearchSql(string order, string strwhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append($@"select * from 
                            (
	                            SELECT  ROW_NUMBER() OVER ({order})AS Row,
			                            name,        --样品名称
			                            ProjectName, --项目名称
			                            count(QualifiedLevel) as QualifiedLevel,  --检验次数
			                            sum(case when QualifiedLevel =  '合格' then 1 else 0 end) as QualifiedLevelA, --合格个数
			                            sum(case when QualifiedLevel != '合格' then 1 else 0 end) as QualifiedLevelB, --非合格个数
			                            TestPersonnelName,  --检验人名称
			                            DetectTime,   --检验日期
			                            Department,	  --送/抽检单位
			                            GHS	--检验中心名称
	                            FROM
	                            (
		                            select * from
		                            (
			                            select A.name,A.DetectionAdress,C.ProjectName,D.QualifiedLevel,D.TestPersonnelName,B.DetectTime,E.Department,F.TestReportName as GHS
			                            FROM dbo.tb_Sample as A
				                             INNER JOIN dbo.tb_OriginalRecord as B ON A.id = B.SampleID		--原始记录
				                             INNER JOIN dbo.tb_Project as C ON B.ProjectID = C.ProjectID		--检验项目
				                             INNER JOIN dbo.tb_TestReportData as D ON B.RecordID = D.RecordID --检验报告数据
				                             INNER JOIN dbo.tb_TestReport as E ON E.ReportID = D.ReportID --检验报告
				                             INNER JOIN dbo.tb_Area as F ON F.AreaId = E.AreaId   --区域
		                            ) as T1
		                            {strwhere}
	                            ) as T2
	                            group by name,ProjectName,TestPersonnelName,DetectTime,Department,GHS
                            ) as TT");
            return strSql.ToString();
        }

        /// <summary>
        /// 实验统计 列表查询
        /// </summary>
        public DataSet GetListByReport(string strWhere, string orderby, int startIndex, int endIndex, ref int total)
        {
            string ordersql = "";
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                ordersql="order by " + orderby;
            }
            else
            {
                ordersql = "order by DetectTime desc";
            }
            StringBuilder strSql = new StringBuilder();
            strSql.Append(this.GetSearchSql(ordersql, strWhere));
            total = DbHelperSQL.GetCount(strSql.ToString());
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }
       
        /// <summary>
        /// 总数汇总查询
        /// </summary>
        public DataRow GetAllListCountForReport(string strWhere)
        {
            string order = "order by DetectTime desc";
            StringBuilder strSql = new StringBuilder();
            strSql.Append($@"select sum(QualifiedLevel) as QualifiedLevel,sum(QualifiedLevelA) as QualifiedLevelA,sum(QualifiedLevelB) as QualifiedLevelB from 
                            (
	                            {this.GetSearchSql(order,strWhere)}
                            ) as T");
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            if (ds != null && ds.Tables != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0].Rows[0];
            }
            return null;
        }

        /// <summary>
        /// 导出列表数据
        /// </summary>
        public DataSet GetExportListByReport(string strWhere, string orderby)
        {
            string orderbystr = "order by DetectTime desc";
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                orderbystr = "order by " + orderby;
            }
            StringBuilder strSql = new StringBuilder();
            strSql.Append(this.GetSearchSql(orderbystr, strWhere));
            return DbHelperSQL.Query(strSql.ToString());
        }
    }
}
