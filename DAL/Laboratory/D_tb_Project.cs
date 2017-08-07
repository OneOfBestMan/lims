using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using Model.Laboratory;
using Dapper;

namespace DAL.Laboratory
{
    /// <summary>
	/// 数据访问类:D_tb_Project
	/// </summary>
	public partial class D_tb_Project
	{
        /// <summary>
        /// 获取项目实体列表
        /// </summary>
        /// <param name="eProject">查询实体</param>
        /// <returns>返回项目集合</returns>
        public List<E_tb_Project> GetList(E_tb_Project eProject)
        {
            //定义返回列表
            List<E_tb_Project> list = new List<E_tb_Project>();

            //初始化查询语句
            StringBuilder search = new StringBuilder();
            search.AppendFormat(@"select * from tb_Project");

            //执行并返回结果
            using (IDbConnection conn = new SqlConnection(PubConstant.GetConnectionString()))
            {
                list = conn.Query<E_tb_Project>(search.ToString())?.ToList();
            }
            return list;

        }

        /// <summary>
        /// 获取该样品对应未完成实验计划的实验项目
        /// </summary>
        /// <param name="SampleID">样品ID</param>
        /// <returns>符合要求的项目集合</returns>
        public List<E_tb_Project> GetNoCompleteList(int SampleID)
        {
            //定义返回列表
            List<E_tb_Project> list = new List<E_tb_Project>();

            //初始化查询语句
            StringBuilder search = new StringBuilder();
            search.AppendFormat($@"select A.* from tb_Project as A
                                inner join tb_ExpePlan as B on A.ProjectID=B.ProjectID
                                where B.Status=0 and B.SampleID={SampleID}");

            //执行并返回结果
            using (IDbConnection conn = new SqlConnection(PubConstant.GetConnectionString()))
            {
                list = conn.Query<E_tb_Project>(search.ToString())?.ToList();
            }
            return list;
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ProjectID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from tb_Project");
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
        public int Add(E_tb_Project model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into tb_Project(");
            strSql.Append("AreaID,LaboratoryID,ProjectTypeID,ProjectName,ExpeType,ExpeMethod,ExpRange,FilePath,SampleDataRange,Remark,UpdateTime,FileName,IsPesCheck,InsStand)");
            strSql.Append(" values (");
            strSql.Append("@AreaID,@LaboratoryID,@ProjectTypeID,@ProjectName,@ExpeType,@ExpeMethod,@ExpRange,@FilePath,@SampleDataRange,@Remark,@UpdateTime,@FileName,@IsPesCheck,@InsStand)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@AreaID", SqlDbType.Int,4),
					new SqlParameter("@LaboratoryID", SqlDbType.Int,4),
					new SqlParameter("@ProjectTypeID", SqlDbType.Int,4),
					new SqlParameter("@ProjectName", SqlDbType.NVarChar,50),
					new SqlParameter("@ExpeType", SqlDbType.NVarChar,50),
					new SqlParameter("@ExpeMethod", SqlDbType.Text),
					new SqlParameter("@ExpRange", SqlDbType.NVarChar,50),
					new SqlParameter("@FilePath", SqlDbType.NVarChar,50),
					new SqlParameter("@SampleDataRange", SqlDbType.NVarChar,50),
					new SqlParameter("@Remark", SqlDbType.Text),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@FileName", SqlDbType.NVarChar,50),
					new SqlParameter("@IsPesCheck", SqlDbType.Int,4),
                    new SqlParameter("@InsStand", SqlDbType.NVarChar,-1)};
            parameters[0].Value = model.AreaID;
            parameters[1].Value = model.LaboratoryID;
            parameters[2].Value = model.ProjectTypeID;
            parameters[3].Value = model.ProjectName;
            parameters[4].Value = model.ExpeType;
            parameters[5].Value = model.ExpeMethod;
            parameters[6].Value = model.ExpRange;
            parameters[7].Value = model.FilePath;
            parameters[8].Value = model.SampleDataRange;
            parameters[9].Value = model.Remark;
            parameters[10].Value = model.UpdateTime;
            parameters[11].Value = model.FileName;
            parameters[12].Value = model.IsPesCheck;
            parameters[13].Value = model.InsStand;
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
        public bool Update(E_tb_Project model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update tb_Project set ");
            strSql.Append("AreaID=@AreaID,");
            strSql.Append("LaboratoryID=@LaboratoryID,");
            strSql.Append("ProjectTypeID=@ProjectTypeID,");
            strSql.Append("ProjectName=@ProjectName,");
            strSql.Append("ExpeType=@ExpeType,");
            strSql.Append("ExpeMethod=@ExpeMethod,");
            strSql.Append("ExpRange=@ExpRange,");
            strSql.Append("FilePath=@FilePath,");
            strSql.Append("SampleDataRange=@SampleDataRange,");
            strSql.Append("Remark=@Remark,");
            strSql.Append("UpdateTime=@UpdateTime,");
            strSql.Append("FileName=@FileName,");
            strSql.Append("IsPesCheck=@IsPesCheck,");
            strSql.Append("InsStand=@InsStand");
            strSql.Append(" where ProjectID=@ProjectID");
            SqlParameter[] parameters = {
					new SqlParameter("@AreaID", SqlDbType.Int,4),
					new SqlParameter("@LaboratoryID", SqlDbType.Int,4),
					new SqlParameter("@ProjectTypeID", SqlDbType.Int,4),
					new SqlParameter("@ProjectName", SqlDbType.NVarChar,50),
					new SqlParameter("@ExpeType", SqlDbType.NVarChar,50),
					new SqlParameter("@ExpeMethod", SqlDbType.Text),
					new SqlParameter("@ExpRange", SqlDbType.NVarChar,50),
					new SqlParameter("@FilePath", SqlDbType.NVarChar,50),
					new SqlParameter("@SampleDataRange", SqlDbType.NVarChar,50),
					new SqlParameter("@Remark", SqlDbType.Text),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@FileName", SqlDbType.NVarChar,50),
					new SqlParameter("@IsPesCheck", SqlDbType.Int,4),
                    new SqlParameter("@InsStand", SqlDbType.NVarChar,-1),
					new SqlParameter("@ProjectID", SqlDbType.Int,4)};
            parameters[0].Value = model.AreaID;
            parameters[1].Value = model.LaboratoryID;
            parameters[2].Value = model.ProjectTypeID;
            parameters[3].Value = model.ProjectName;
            parameters[4].Value = model.ExpeType;
            parameters[5].Value = model.ExpeMethod;
            parameters[6].Value = model.ExpRange;
            parameters[7].Value = model.FilePath;
            parameters[8].Value = model.SampleDataRange;
            parameters[9].Value = model.Remark;
            parameters[10].Value = model.UpdateTime;
            parameters[11].Value = model.FileName;
            parameters[12].Value = model.IsPesCheck;
            parameters[13].Value = model.InsStand;
            parameters[14].Value = model.ProjectID;

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
            strSql.Append("delete from tb_Project ");
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
            strSql.Append("delete from tb_Project ");
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
        public E_tb_Project GetModel(int ProjectID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 * from tb_Project ");
            strSql.Append(" where ProjectID=@ProjectID");
            SqlParameter[] parameters = {
					new SqlParameter("@ProjectID", SqlDbType.Int,4)
};
            parameters[0].Value = ProjectID;

            E_tb_Project model = new E_tb_Project();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ProjectID"].ToString() != "")
                {
                    model.ProjectID = int.Parse(ds.Tables[0].Rows[0]["ProjectID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["AreaID"].ToString() != "")
                {
                    model.AreaID = int.Parse(ds.Tables[0].Rows[0]["AreaID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["LaboratoryID"].ToString() != "")
                {
                    model.LaboratoryID = int.Parse(ds.Tables[0].Rows[0]["LaboratoryID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ProjectTypeID"].ToString() != "")
                {
                    model.ProjectTypeID = int.Parse(ds.Tables[0].Rows[0]["ProjectTypeID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ProjectName"] != null)
                {
                    model.ProjectName = ds.Tables[0].Rows[0]["ProjectName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ExpeType"] != null)
                {
                    model.ExpeType = ds.Tables[0].Rows[0]["ExpeType"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ExpeMethod"] != null)
                {
                    model.ExpeMethod = ds.Tables[0].Rows[0]["ExpeMethod"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ExpRange"] != null)
                {
                    model.ExpRange = ds.Tables[0].Rows[0]["ExpRange"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FilePath"] != null)
                {
                    model.FilePath = ds.Tables[0].Rows[0]["FilePath"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SampleDataRange"] != null)
                {
                    model.SampleDataRange = ds.Tables[0].Rows[0]["SampleDataRange"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Remark"] != null)
                {
                    model.Remark = ds.Tables[0].Rows[0]["Remark"].ToString();
                }
                if (ds.Tables[0].Rows[0]["UpdateTime"].ToString() != "")
                {
                    model.UpdateTime = DateTime.Parse(ds.Tables[0].Rows[0]["UpdateTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["FileName"] != null)
                {
                    model.FileName = ds.Tables[0].Rows[0]["FileName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["IsPesCheck"].ToString() != "")
                {
                    model.IsPesCheck = int.Parse(ds.Tables[0].Rows[0]["IsPesCheck"].ToString());
                }
                if (ds.Tables[0].Rows[0]["InsStand"].ToString() != "")
                {
                    model.InsStand = ds.Tables[0].Rows[0]["InsStand"].ToString();
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
            strSql.Append("select * ");
            strSql.Append(" FROM tb_Project ");
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
            strSql.Append(" FROM tb_Project ");
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
            strSql.Append($@"select * from (
		                            SELECT 
			                            ROW_NUMBER() OVER (order by T.UpdateTime desc)AS Row, 
			                            T.*,B.AreaName,C.LaboratoryName,D.TypeName  
		                            from tb_Project T 
		                            left join tb_Area as B on T.AreaID=B.AreaID 
		                            left join tb_Laboratory as C on T.LaboratoryID=C.LaboratoryID 
		                            left join tb_TypeDict as D on T.ProjectTypeID=D.TypeID 
	                            ) as temp {strWhere}");
            
            total = DbHelperSQL.GetCount(strSql.ToString());

            string listSql = $"SELECT * FROM ({strSql.ToString()}) TT  WHERE TT.Row between {startIndex} and {endIndex}";
            return DbHelperSQL.Query(listSql);
        }

        /// <summary>
        /// 根据原始记录要求获取项目列表
        /// </summary>
        /// <param name="strWhere">查询条件</param>
        /// <returns></returns>
        public DataTable GetListByOriginalRecord(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from (select * from tb_Project where ProjectID in (select ProjectID from tb_ExpePlan group by ProjectID)) as T");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString()).Tables[0];
        }
	}
}
