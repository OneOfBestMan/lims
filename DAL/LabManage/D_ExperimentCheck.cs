using Model;
using Model.LabManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using Comp;
using System.Configuration;

namespace DAL.LabManage
{
    /// <summary>
    /// 实验考核
    /// </summary>
    public class D_ExperimentCheck
    {
        /// <summary>
        /// 获取实验考核数据列表
        /// </summary>
        /// <param name="ePageParameter">页面参数实体</param>
        /// <returns></returns>
        public List<E_ExperimentCheck> GetExperimentCheckList(E_PageParameter ePageParameter,ref int count)
        {
            List<E_ExperimentCheck> list = new List<E_ExperimentCheck>();

            //拼接查询条件
            StringBuilder strwhere = new StringBuilder();
            strwhere.AddWhere("PlanID is not null"); //排除无实验计划的记录
            if (ePageParameter.starttime != null)
            {
                strwhere.AddWhere($"detectionDate>=cast('{Convert.ToDateTime(ePageParameter.starttime).ToString("yyyy-MM-dd")}' as datetime)");
            }
            if (ePageParameter.endtime != null)
            {
                strwhere.AddWhere($"detectionDate<=cast('{Convert.ToDateTime(ePageParameter.endtime).ToString("yyyy-MM-dd")}' as datetime)");
            }

            //主查询Sql
            StringBuilder search = new StringBuilder();
            search.AppendFormat(@"select 
                                    row_number()over(order by detectionDate desc) as rowid,
                                    A.id as sampleid,A.name as samplename,A.detectionDate,A.urgentlevel,C.ProjectID,C.ProjectName
                                    ,D.DetectTime,D.DetectPersonnelID,E.PersonnelName as detectpersonnelname 
	                                from tb_Sample as A
	                                left join tb_ExpePlan as B on A.id=B.SampleID
	                                left join tb_Project as C on B.ProjectID=C.ProjectID
	                                left join tb_OriginalRecord as D on B.TaskNo=D.TaskNo
	                                left join tb_InPersonnel as E on D.DetectPersonnelID=E.PersonnelID
	                                {0}", strwhere.ToString());
            
            //当前页数据Sql
            StringBuilder currdata = new StringBuilder();
            currdata.AppendFormat(@"select * from 
                                (
	                                {0}
                                ) as t where t.rowid between @pageindex*@pagesize+1 and (@pageindex+1)*@pagesize ", search.ToString());

            //当前条件下的数据总量
            StringBuilder totalcount = new StringBuilder();
            totalcount.AppendFormat(@"select count(1) from 
                                (
	                                {0}
                                ) as t ", search.ToString());

            //执行查询语句
            using (IDbConnection conn = new SqlConnection(PubConstant.GetConnectionString()))
            {
                list = conn.Query<E_ExperimentCheck>(currdata.ToString(),ePageParameter)?.ToList();
                count = (int)conn.ExecuteScalar(totalcount.ToString(), ePageParameter);
            }
            return list;
        }
    }
}
