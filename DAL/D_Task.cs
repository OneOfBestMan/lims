using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Model;
using Dapper;
using System.Data.SqlClient;
using System.Data;

namespace DAL
{
   public class D_Task
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<E_Task> GetList(E_Task model, ref int total)
        {
            if (model.PageIndex <= 0) { model.PageIndex = 1; }


            List<E_Task> list;
            StringBuilder strSql = new StringBuilder();
            StringBuilder whereSql = new StringBuilder(" where 1=1 ");
            strSql.Append(" select  ROW_NUMBER() OVER ( ORDER BY id desc) AS RID,  task.*,i.PersonnelName as publishname from task left join dbo.tb_InPersonnel i on task.publishid=i.PersonnelID ");
        
            if (!String.IsNullOrEmpty(model.taskname))
            {
                whereSql.Append(" and taskname like '%'+@taskname +'%'");
            }
       
            strSql.Append(whereSql);
            string CountSql = "SELECT COUNT(1) as RowsCount FROM (" + strSql.ToString() + ") AS CountList";


            string pageSqlStr = "select * from ( " + strSql.ToString() + " ) as Temp_PageData where Temp_PageData.RID BETWEEN {0} AND {1}";
            pageSqlStr = string.Format(pageSqlStr, (model.PageSize * (model.PageIndex - 1) + 1).ToString(), (model.PageSize * model.PageIndex).ToString());
            using (IDbConnection conn = new SqlConnection(PubConstant.GetConnectionString()))
            {
                list = conn.Query<E_Task>(pageSqlStr, model)?.ToList();
                total = conn.ExecuteScalar<int>(CountSql, model);
            }
            return list;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public E_Task GetInfoById(E_Task model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from task where id=@id");
            using (IDbConnection conn = new SqlConnection(PubConstant.GetConnectionString()))
            {
                model = conn.Query<E_Task>(strSql.ToString(), model)?.FirstOrDefault();

            }
            return model;
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Add(E_Task model)
        {
            string sql = "INSERT INTO task(taskname, director, publishid, remark, status, tasktime, publishtime, finishtime) VALUES (@taskname, @director, @publishid, @remark, @status, @tasktime, @publishtime, @finishtime)";
            using (IDbConnection conn = new SqlConnection(PubConstant.GetConnectionString()))
            {
                int count = conn.Execute(sql, model);
                if (count > 0)//如果更新失败
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Update(E_Task model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update task set taskname=@taskname, director=@director, remark=@remark, status=@status, tasktime=@tasktime, finishtime=@finishtime  where id=@id ");
            using (IDbConnection conn = new SqlConnection(PubConstant.GetConnectionString()))
            {
                int count = conn.Execute(strSql.ToString(), model);
                if (count > 0)//如果更新失败
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool DeleteById(E_Task model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete task   where id=@id ");
            using (IDbConnection conn = new SqlConnection(PubConstant.GetConnectionString()))
            {
                int count = conn.Execute(strSql.ToString(), model);
                if (count > 0)//如果更新失败
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// 获取首页任务列表
        /// </summary>
        public List<E_Task> GetIndexTaskList()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 7 * from task order by publishtime desc");
            using (IDbConnection conn = new SqlConnection(PubConstant.GetConnectionString()))
            {
                return conn.Query<E_Task>(strSql.ToString())?.ToList();
            }
        }
       
    }
}
