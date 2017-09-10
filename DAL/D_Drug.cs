using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Model;
using Dapper;
using System.Data.SqlClient;
using System.Data;
using Comp;

namespace DAL
{
   public  class D_Drug
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<E_DrugCheck> GetList(E_DrugCheck model, ref int total)
        {
            if (model.PageIndex <= 0) { model.PageIndex = 1; }


            List<E_DrugCheck> list;
            StringBuilder strSql = new StringBuilder();
            StringBuilder whereSql = new StringBuilder(" where 1=1 ");
            strSql.Append(@"select ROW_NUMBER() OVER ( ORDER BY a.id desc) AS RID,  c.*,a.putArea,a.validDate yxq,a.amount rukucount,(select SUM(amount) from dbo.tb_DrugOUT WHERE temp2=a.id) chukucount  from dbo.tb_DrugIN a
                            left join dbo.tb_DrugOUT  b on b.temp2=a.id
                            inner join dbo.tb_Drug c on a.drugId=c.id ");

 
            if (!string.IsNullOrEmpty(model.drugName))
            {
                whereSql.Append(" and drugName like '%'+@drugName+'%'");
            }
            if (!string.IsNullOrEmpty(model.putArea))
            {
                whereSql.Append(" and a.putArea like '%'+@putArea+'%'");
            }
            if (Utils.GetInt(model.riskLevel) > 0)
            {
                whereSql.Append(" and c.riskLevel=@riskLevel");
            }
            if (model.drugType > 0)
            {
                whereSql.Append(" and c.drugType=@drugType");
            }
         

            strSql.Append(whereSql);
            string CountSql = "SELECT COUNT(1) as RowsCount FROM (" + strSql.ToString() + ") AS CountList";


            string pageSqlStr = "select * from ( " + strSql.ToString() + " ) as Temp_PageData where Temp_PageData.RID BETWEEN {0} AND {1}";
            pageSqlStr = string.Format(pageSqlStr, (model.PageSize * (model.PageIndex - 1) + 1).ToString(), (model.PageSize * model.PageIndex).ToString());
            using (IDbConnection conn = new SqlConnection(PubConstant.GetConnectionString()))
            {
                list = conn.Query<E_DrugCheck>(pageSqlStr, model)?.ToList();
                total = conn.ExecuteScalar<int>(CountSql, model);
            }
            return list;
        }
    }
}
