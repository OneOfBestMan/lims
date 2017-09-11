using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Model;
using System.Data.SqlClient;
using System.Data;
using Comp;

namespace DAL
{
 public    class D_DrugLock
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public E_DrugLock GetInfoById(E_DrugLock model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from tb_DrugLock where id=@id");

            using (IDbConnection conn = new SqlConnection(PubConstant.GetConnectionString()))
            {
                model = conn.Query<E_DrugLock>(strSql.ToString(), model)?.FirstOrDefault();

            }
            return model;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Add(E_DrugLock model)
        {
            string sql = "INSERT INTO tb_DrugLock(temp2,pic,ischem,isdanger,locktypeid,lockName,mark,createUser,createDate,updateUser,updateDate,lockType,temp1) VALUES (@temp2,@pic,@ischem,@isdanger,@locktypeid,@lockName,@mark,@createUser,@createDate,@updateUser,@updateDate,@lockType,@temp1)";
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
        public bool Update(E_DrugLock model)
        {
         
            StringBuilder strSql = new StringBuilder();
            //  strSql.Append("update dp_food set areaid=@areaid, classinfoid=@classinfoid,foodname=@foodname,pid=@pid,pname=@pname,pic=@pic,updatetime=getdate()  where foodid=@foodid ");
            strSql.Append("update tb_DrugLock set " + Utils.SetUpdateSql(model, new string[] { "id" }) + " where id=@id ");

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
    }
}
