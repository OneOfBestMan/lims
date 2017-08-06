using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using Model.OriginalRecord;
using Dapper;

namespace DAL.OriginalRecord
{
    /// <summary>
    /// 数据访问类:tb_OriginalRecord
    /// </summary>
    public partial class D_tb_OriginalRecord
    {
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int RecordID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from tb_OriginalRecord");
            strSql.Append(" where RecordID=@RecordID");
            SqlParameter[] parameters = {
					new SqlParameter("@RecordID", SqlDbType.Int,4)
};
            parameters[0].Value = RecordID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(E_tb_OriginalRecord model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into tb_OriginalRecord(");
            strSql.Append("ProjectID,TaskNo,DetectTime,DetectPersonnelID,FilePath,Contents,AreaID,EditPersonnelID,SampleID,InsStand)");
            strSql.Append(" values (");
            strSql.Append("@ProjectID,@TaskNo,@DetectTime,@DetectPersonnelID,@FilePath,@Contents,@AreaID,@EditPersonnelID,@SampleID,@InsStand)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@ProjectID", SqlDbType.Int,4),
					new SqlParameter("@TaskNo", SqlDbType.NVarChar,50),
					new SqlParameter("@DetectTime", SqlDbType.DateTime),
					new SqlParameter("@DetectPersonnelID", SqlDbType.Int,4),
					new SqlParameter("@FilePath", SqlDbType.NVarChar,150),
					new SqlParameter("@Contents", SqlDbType.NVarChar,200),
					new SqlParameter("@AreaID", SqlDbType.Int,4),
					new SqlParameter("@EditPersonnelID", SqlDbType.Int,4),
					new SqlParameter("@SampleID", SqlDbType.Int,4),
                    new SqlParameter("@InsStand", SqlDbType.NVarChar,-1)};
            parameters[0].Value = model.ProjectID;
            parameters[1].Value = model.TaskNo;
            parameters[2].Value = model.DetectTime;
            parameters[3].Value = model.DetectPersonnelID;
            parameters[4].Value = model.FilePath;
            parameters[5].Value = model.Contents;
            parameters[6].Value = model.AreaID;
            parameters[7].Value = model.EditPersonnelID;
            parameters[8].Value = model.SampleID;
            parameters[9].Value = model.InsStand;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                model.RecordID = Convert.ToInt32(obj);
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(E_tb_OriginalRecord model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update tb_OriginalRecord set ");
            strSql.Append("ProjectID=@ProjectID,");
            strSql.Append("TaskNo=@TaskNo,");
            strSql.Append("DetectTime=@DetectTime,");
            strSql.Append("DetectPersonnelID=@DetectPersonnelID,");
            strSql.Append("FilePath=@FilePath,");
            strSql.Append("Contents=@Contents,");
            strSql.Append("AreaID=@AreaID,");
            strSql.Append("EditPersonnelID=@EditPersonnelID,");
            strSql.Append("SampleID=@SampleID,");
            strSql.Append("InsStand=@InsStand");
            strSql.Append(" where RecordID=@RecordID");
            SqlParameter[] parameters = {
					new SqlParameter("@ProjectID", SqlDbType.Int,4),
					new SqlParameter("@TaskNo", SqlDbType.NVarChar,50),
					new SqlParameter("@DetectTime", SqlDbType.DateTime),
					new SqlParameter("@DetectPersonnelID", SqlDbType.Int,4),
					new SqlParameter("@FilePath", SqlDbType.NVarChar,150),
					new SqlParameter("@Contents", SqlDbType.NVarChar,200),
					new SqlParameter("@AreaID", SqlDbType.Int,4),
					new SqlParameter("@EditPersonnelID", SqlDbType.Int,4),
					new SqlParameter("@SampleID", SqlDbType.Int,4),
					new SqlParameter("@RecordID", SqlDbType.Int,4),
                    new SqlParameter("@InsStand", SqlDbType.NVarChar,-1)};
            parameters[0].Value = model.ProjectID;
            parameters[1].Value = model.TaskNo;
            parameters[2].Value = model.DetectTime;
            parameters[3].Value = model.DetectPersonnelID;
            parameters[4].Value = model.FilePath;
            parameters[5].Value = model.Contents;
            parameters[6].Value = model.AreaID;
            parameters[7].Value = model.EditPersonnelID;
            parameters[8].Value = model.SampleID;
            parameters[9].Value = model.RecordID;
            parameters[10].Value = model.InsStand;

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
        public bool Delete(int RecordID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from tb_OriginalRecord ");
            strSql.Append(" where RecordID=@RecordID");
            SqlParameter[] parameters = {
					new SqlParameter("@RecordID", SqlDbType.Int,4)
};
            parameters[0].Value = RecordID;

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
        public bool DeleteList(string RecordIDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from tb_OriginalRecord ");
            strSql.Append(" where RecordID in (" + RecordIDlist + ")  ");
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
        public E_tb_OriginalRecord GetModel(E_tb_OriginalRecord model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append($@"select 
	                            A.*,
	                            B.PersonnelName as detectpersonnelname, --检查人名称
	                            C.SampleDataRange, --检查项目对应数据提取范围
                                C.IsPesCheck		--是否农药残留
                            from tb_OriginalRecord as A 
                            left join dbo.tb_InPersonnel as B on A.DetectPersonnelID=B.PersonnelID
                            left join dbo.tb_Project as C on A.ProjectID=C.ProjectID
                            where RecordID={model.RecordID}");
            using (IDbConnection conn = new SqlConnection(PubConstant.GetConnectionString()))
            {
                model = conn.Query<E_tb_OriginalRecord>(strSql.ToString(), model)?.FirstOrDefault();
            }
            return model;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM tb_OriginalRecord ");
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
            strSql.Append(" * ");
            strSql.Append(" FROM tb_OriginalRecord ");
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
                strSql.Append("order by T.RecordID desc");
            }
            strSql.Append(")AS Row, T.*,B.ProjectName,C.PersonnelName as DetectPersonnelName,E.name as SampleName  from tb_OriginalRecord T ");
            strSql.Append(" left join tb_Project as B on T.ProjectID=B.ProjectID");
            strSql.Append(" left join tb_InPersonnel as C on T.DetectPersonnelID=C.PersonnelID");
            strSql.Append(" left join tb_ExpePlan as D on T.TaskNo=D.TaskNo");
            strSql.Append(" left join tb_Sample as E on T.SampleID=E.id");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(strWhere);
            }
            strSql.Append(" ) TT");
            total = DbHelperSQL.GetCount(strSql.ToString());
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 根据样品名称获取对应的原始记录ID集合
        /// </summary>
        /// <param name="SampleID">样品ID</param>
        /// <returns></returns>
        public DataTable GetRecordIDListBySampleID(int SampleID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select RecordID from tb_OriginalRecord as A left join tb_ExpePlan as B on A.TaskNo=B.TaskNo where B.SampleID=" + SampleID + " group by RecordID");
            return DbHelperSQL.Query(strSql.ToString()).Tables[0];
        }
    }
}
