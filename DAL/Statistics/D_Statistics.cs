using Dapper;
using Model.Statistics;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Comp;

namespace DAL.Statistics
{
    /// <summary>
    /// 统计
    /// </summary>
    public class D_Statistics
    {
        /// <summary>
        /// 统计未完成工作（未完成实验计划、未审核检验报告、未批准检验报告）
        /// </summary>
        public List<int> SummaryWork()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append($@"
                select count(1) as num from[dbo].[tb_ExpePlan] where Status in (0, 2)--未完成实验计划
                union all
                select count(1) as num from tb_TestReport where examinePersonnelID = 0 or examinePersonnelID is null--未审核检验报告
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
        public List<E_ExpePlanStatistics> GetExpePlanStatistics(int areaid,int headpersonnelid, DateTime? starttime, DateTime? endtime)
        {
            StringBuilder strWhere = new StringBuilder();
            if (areaid > 0) //地区ID
            {
                strWhere.AddWhere($"areaid={areaid}");
            }
            if (headpersonnelid > 0)//检验负责人ID
            {
                strWhere.AddWhere($"headpersonnelid={headpersonnelid}");
            }
            if(starttime!=null) //检验开始时间
            {
                strWhere.AddWhere($"InspectTime>=cast('{starttime.ToString()}' as datetime)");
            }
            if (endtime != null) //检验结束时间
            {
                strWhere.AddWhere($"InspectTime<cast('{endtime.ToString()}' as datetime)");
            }

            StringBuilder strSql = new StringBuilder();
            strSql.Append($@"
                select A.*,B.PersonnelName as headpersonnename 
                from (
	                select 
		                headpersonnelid,
		                sum(case when [Status]=1 then 1 else 0 end) as completed,
		                sum(case when [Status]!=1 then 1 else 0 end) as notcompleted 
	                from [tb_ExpePlan] 
	                {strWhere.ToString()}
	                group by headpersonnelid
                ) as A inner join tb_InPersonnel as B on A.headpersonnelid=B.PersonnelID
            ");

            using (IDbConnection conn = new SqlConnection(PubConstant.GetConnectionString()))
            {
                return conn.Query<E_ExpePlanStatistics>(strSql.ToString()).ToList();
            }
        }

        /// <summary>
        /// 获取实验计划统计列表 按照天进行统计
        /// </summary>
        //public List<E_ExpePlanStatistics> GetExpePlanStatisticsForDay(int areaid, int headpersonnelid, DateTime? starttime, DateTime? endtime)
        //{
        //    StringBuilder strWhere = new StringBuilder();
        //    if (areaid > 0) //地区ID
        //    {
        //        strWhere.AddWhere($"areaid={areaid}");
        //    }
        //    if (headpersonnelid > 0)//检验负责人ID
        //    {
        //        strWhere.AddWhere($"headpersonnelid={headpersonnelid}");
        //    }
        //    if (starttime != null) //检验开始时间
        //    {
        //        strWhere.AddWhere($"InspectTime>=cast('{starttime.ToString()}' as datetime)");
        //    }
        //    if (endtime != null) //检验结束时间
        //    {
        //        strWhere.AddWhere($"InspectTime<cast('{endtime.ToString()}' as datetime)");
        //    }

        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append($@"
        //        select A.*,B.PersonnelName as headpersonnename 
        //        from (
	       //         select 
		      //          headpersonnelid,
		      //          inspectTime,
		      //          sum(case when [Status]=1 then 1 else 0 end) as completed,
		      //          sum(case when [Status]!=1 then 1 else 0 end) as notcompleted 
	       //         from [tb_ExpePlan] 
	       //         {strWhere.ToString()}
	       //         group by headpersonnelid,inspectTime
        //        ) as A inner join tb_InPersonnel as B on A.headpersonnelid=B.PersonnelID
        //    ");

        //    using (IDbConnection conn = new SqlConnection(PubConstant.GetConnectionString()))
        //    {
        //        return conn.Query<E_ExpePlanStatistics>(strSql.ToString()).ToList();
        //    }
        //}


        /// <summary>
        /// 获取检验报告按月统计
        /// </summary>
        public List<E_TestReportDataStatistics> GetTestReportMonthDataStatistics(int areaid,DateTime starttime,DateTime endtime)
        {
            StringBuilder strWhere = new StringBuilder();
            strWhere.AddWhere("SamplingTime is not null");
            if (areaid > 0) //地区ID
            {
                strWhere.AddWhere($"areaid={areaid}");
            }
            if (starttime != null) //抽样开始时间
            {
                strWhere.AddWhere($"SamplingTime>=cast('{starttime.ToString()}' as datetime)");
            }
            if (endtime != null) //抽样结束时间
            {
                strWhere.AddWhere($"SamplingTime<cast('{endtime.ToString()}' as datetime)");
            }

            StringBuilder strSql = new StringBuilder();
            strSql.Append($@"
                 select updatetime,sum(examine) as examinecount, sum(Approval) as approvalcount,sum(total) as total
                 from (
                     select 
                         substring(CONVERT(nvarchar(50),SamplingTime,23),0,8) as updatetime, --日期时间格式
                         1 as total, --总数
                         case when examinePersonnelID is not null and examinePersonnelID>0 then 0 else 1 end as examine, --未审核个数
                         case when ApprovalPersonnelID is not null and ApprovalPersonnelID>0 then 0 else 1 end as Approval --未批准个数
                     from [tb_TestReport] {strWhere.ToString()}
                 ) T group by updatetime order by updatetime desc
            ");

            using (IDbConnection conn = new SqlConnection(PubConstant.GetConnectionString()))
            {
                return conn.Query<E_TestReportDataStatistics>(strSql.ToString()).ToList();
            }
        }

        /// <summary>
        /// 获取未批准、未审核检验报告按天统计
        /// </summary>
        /// <returns></returns>
        public List<E_TestReportDataStatistics> GetTestReportDayDataStatistics(DateTime starttime,DateTime endtime)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append($@"
                 select updatetime,sum(examine) as examinecount, sum(Approval) as approvalcount 
                 from (
	                 select 
		                 CONVERT(nvarchar(50),SamplingTime,23) as updatetime, --日期时间格式
		                 case when examinePersonnelID is not null and examinePersonnelID>0 then 0 else 1 end as examine, --未审核个数
		                 case when ApprovalPersonnelID is not null and ApprovalPersonnelID>0 then 0 else 1 end as Approval --未批准个数
	                 from [tb_TestReport] 
                     where SamplingTime is not null and SamplingTime>=cast('{starttime.ToString("yyyy-MM-dd")}' as datetime) and SamplingTime<cast('{endtime.ToString("yyyy-MM-dd")}' as datetime)
                 ) T group by updatetime order by updatetime desc
            ");

            using (IDbConnection conn = new SqlConnection(PubConstant.GetConnectionString()))
            {
                return conn.Query<E_TestReportDataStatistics>(strSql.ToString()).ToList();
            }
        }

        /// <summary>
        /// 获取未完成实验计划
        /// </summary>
        public int GetNoFinishExpePlanCount(int HeadPersonnelID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append($@"
                 select count(1) from dbo.tb_ExpePlan where HeadPersonnelID={HeadPersonnelID} and status!=1
            ");
            using (IDbConnection conn = new SqlConnection(PubConstant.GetConnectionString()))
            {
                return Convert.ToInt32(conn.ExecuteScalar(strSql.ToString()));
            }
        }

        /// <summary>
        /// 获取未审核检验报告
        /// </summary>
        public int GetNoExamineTestReportCount()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append($@"
                 select count(1) from dbo.tb_TestReport where examinePersonnelID=0 or examinePersonnelID is null
            ");
            using (IDbConnection conn = new SqlConnection(PubConstant.GetConnectionString()))
            {
                return Convert.ToInt32(conn.ExecuteScalar(strSql.ToString()));
            }
        }

        /// <summary>
        /// 获取未批准检验报告
        /// </summary>
        public int GetNoApprovalTestReportCount()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append($@"
                 select count(1) from dbo.tb_TestReport where ApprovalPersonnelID=0 or ApprovalPersonnelID is null
            ");
            using (IDbConnection conn = new SqlConnection(PubConstant.GetConnectionString()))
            {
                return Convert.ToInt32(conn.ExecuteScalar(strSql.ToString()));
            }
        }
    }
}
