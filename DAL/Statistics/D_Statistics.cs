using Dapper;
using Model.Statistics;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Statistics
{
    /// <summary>
    /// 统计
    /// </summary>
    public class D_Statistics
    {
        /// <summary>
        /// 统计未完成工作（未完成实验计划、未审批检验报告、未批准检验报告）
        /// </summary>
        public List<int> SummaryWork()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append($@"
                select count(1) as num from[dbo].[tb_ExpePlan] where Status in (0, 2)--未完成实验计划
                union all
                select count(1) as num from tb_TestReport where examinePersonnelID = 0 or examinePersonnelID is null--未审批检验报告
                union all
                select count(1) as num from tb_TestReport where ApprovalPersonnelID = 0 or ApprovalPersonnelID is null--未批准检验报告
            ");

            using (IDbConnection conn = new SqlConnection(PubConstant.GetConnectionString()))
            {
                List<int> result = conn.Query<int>(strSql.ToString()).ToList();
                return result;
            }
        }

        /// <summary>
        /// 获取实验计划统计列表
        /// </summary>
        public List<E_ExpePlanStatistics> GetExpePlanStatistics()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append($@"
                select A.*,B.PersonnelName as headpersonnename from (
                select headpersonnelid,sum(case when [Status]=1 then 1 else 0 end) as completed,sum(case when [Status]!=1 then 1 else 0 end) as notcompleted 
                from [tb_ExpePlan] group by headpersonnelid
                ) as A inner join tb_InPersonnel as B on A.headpersonnelid=B.PersonnelID
            ");

            using (IDbConnection conn = new SqlConnection(PubConstant.GetConnectionString()))
            {
                return conn.Query<E_ExpePlanStatistics>(strSql.ToString()).ToList();
            }
        }
    }
}
