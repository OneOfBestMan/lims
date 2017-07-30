using Comp;
using Dapper;
using Model;
using Model.Sample;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Sample
{
    public class D_Sample
    {
        /// <summary>
        /// 获取查询条件
        /// </summary>
        /// <param name="ePageParameter">查询参数实体</param>
        /// <returns>查询条件</returns>
        private string GetStrWhere(E_PageParameter ePageParameter)
        {
            //拼接查询条件
            StringBuilder strwhere = new StringBuilder();
            if (ePageParameter.areaid > 0) //单位
            {
                strwhere.AddWhere($"A.AreaID={ePageParameter.areaid}");
            }
            if (!string.IsNullOrEmpty(ePageParameter.samplename)) //样品名称
            {
                strwhere.AddWhere($"A.name like '%{ePageParameter.samplename}%'");
            }
            if (!string.IsNullOrEmpty(ePageParameter.detectionuser)) //抽样人
            {
                strwhere.AddWhere($"A.detectionUser like '%{ePageParameter.detectionuser}%'");
            }
            if (ePageParameter.createuser > 0) //创建人
            {
                strwhere.AddWhere($"A.createUser={ePageParameter.createuser}");
            }
            if (ePageParameter.starttime != null) //抽样时间
            {
                strwhere.AddWhere($"A.detectionDate>=cast('{Convert.ToDateTime(ePageParameter.starttime).ToString("yyyy-MM-dd")}' as datetime)");
            }
            if (ePageParameter.endtime != null)
            {
                strwhere.AddWhere($"A.detectionDate<=cast('{Convert.ToDateTime(ePageParameter.endtime).ToString("yyyy-MM-dd")}' as datetime)");
            }
            return strwhere.ToString();
        }

        /// <summary>
        /// 获取实验考核数据列表
        /// </summary>
        /// <param name="ePageParameter">页面参数实体</param>
        /// <returns></returns>
        public List<Model.tb_Sample> GetSampleList(E_PageParameter ePageParameter, ref int count)
        {
            List<Model.tb_Sample> list = new List<Model.tb_Sample>();
            
            //主查询Sql
            StringBuilder search = new StringBuilder();
            search.AppendFormat(@"select  row_number()over(order by A.createDate desc) as rowid,
	                                       A.*,
	                                       B.PersonnelName as handleusername,
	                                       C.ClientName as inspectcompanyname  
                                    from tb_Sample as A 
                                    left join tb_InPersonnel as B on A.handleUser=B.PersonnelID
                                    left join tb_ClientManage as C on A.InspectCompany=C.ClientID {0} ", GetStrWhere(ePageParameter));

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
                list = conn.Query<Model.tb_Sample>(currdata.ToString(), ePageParameter)?.ToList();
                count = (int)conn.ExecuteScalar(totalcount.ToString(), ePageParameter);
            }
            return list;
        }

        /// <summary>
        /// 获取样品列表，不带分页
        /// </summary>
        /// <param name="ePageParameter">查询参数实体</param>
        /// <returns>返回对应数据集合</returns>
        public List<Model.tb_Sample> GetSampleList(E_PageParameter ePageParameter)
        {
            List<Model.tb_Sample> list = new List<Model.tb_Sample>();
            
            //主查询Sql
            StringBuilder search = new StringBuilder();
            search.AppendFormat(@"select  row_number()over(order by A.createDate desc) as rowid,
	                                       A.*,
	                                       B.PersonnelName as handleusername,
	                                       C.ClientName as inspectcompanyname  
                                    from tb_Sample as A 
                                    left join tb_InPersonnel as B on A.handleUser=B.PersonnelID
                                    left join tb_ClientManage as C on A.InspectCompany=C.ClientID {0} ", GetStrWhere(ePageParameter));
            
            //执行查询语句
            using (IDbConnection conn = new SqlConnection(PubConstant.GetConnectionString()))
            {
                list = conn.Query<Model.tb_Sample>(search.ToString(), ePageParameter)?.ToList();
            }
            return list;
        }

        /// <summary>
        /// 获取分样单数据
        /// </summary>
        /// <param name="ePageParameter">查询参数实体</param>
        /// <returns>返回分样单数据</returns>
        public List<E_SamplingSheet> GetSamplingSheetList(E_PageParameter ePageParameter)
        {
            List<E_SamplingSheet> list = new List<E_SamplingSheet>();

            //主查询Sql
            StringBuilder search = new StringBuilder();
            search.AppendFormat(@"select A.samplenum,A.name as samplename,
                                        (C.ProjectName+'--'+B.InspectMethod) as checkprojectandstandard,
                                        A.storcondition,A.checkdepar,
                                        case when A.urgentlevel=1 then '普通' else '紧急' end asurgentlevel
                                        from dbo.tb_Sample as A 
                                        left join dbo.tb_ExpePlan AS B on A.id=B.SampleID
                                        left join dbo.tb_Project as C on B.ProjectID=C.ProjectID
                                    where A.id in ({0})",ePageParameter.sampleids);

            //执行查询语句
            using (IDbConnection conn = new SqlConnection(PubConstant.GetConnectionString()))
            {
                list = conn.Query<E_SamplingSheet>(search.ToString(), ePageParameter)?.ToList();
            }
            return list;
        }

        /// <summary>
        /// 获取数据集合
        /// </summary>
        /// <param name="fields">字段列表</param>
        /// <param name="strwhere">查询条件</param>
        /// <returns>返回对应数据集合</returns>
        public DataTable GetSampleDt(string fields, string strwhere)
        {
            //主查询Sql
            StringBuilder search = new StringBuilder();
            search.AppendFormat($"select {fields} from tb_Sample where {strwhere}");
            return DbHelperSQL.Query(search.ToString()).Tables[0];
        }

        /// <summary>
        /// 获取样品详情
        /// </summary>
        /// <param name="id">样品ID</param>
        /// <returns>样品实体</returns>
        public tb_Sample GetSampleInfo(tb_Sample model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select * from tb_Sample where id =@id");
            using (IDbConnection conn = new SqlConnection(PubConstant.GetConnectionString()))
            {
                model = conn.Query<tb_Sample>(strSql.ToString(), model)?.FirstOrDefault();
            }
            return model;
        }

        /// <summary>
        /// 更新
        /// </summary>
        public bool Update(tb_Sample model)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"update tb_Sample set 
                                AreaID=@AreaID,
                                name=@name,
                                standard=@standard,
                                batch=@batch,
                                productDate=@productDate,
                                modelType=@modelType,
                                expirationDate=@expirationDate,
                                packaging=@packaging,
                                isDetection=@isDetection,
                                detectionUser=@detectionUser,
                                detectionDate=@detectionDate,
                                createUser=@createUser,
                                createDate=@createDate,
                                updateUser=@updateUser,
                                updateDate=@updateDate,
                                temp1=@temp1,
                                temp2=@temp2,
                                putArea=@putArea,
                                sampleHandle=@sampleHandle,
                                handleUser=@handleUser,
                                handleDate=@handleDate,
                                sampleAdmin=@sampleAdmin,
                                detectionGist=@detectionGist,
                                detectionMethod=@detectionMethod,
                                InspectCompany=@InspectCompany,
                                detectionAdress=@detectionAdress,
                                detectionCompany=@detectionCompany,
                                InspectAddress=@InspectAddress,
                                projectName=@projectName,
                                testMethod=@testMethod,
                                sampleNum=@sampleNum,
                                protNum=@protNum,
                                storcondition=@storcondition,
                                checkdepar=@checkdepar,
                                urgentlevel=@urgentlevel
                            where id=@id");

            using (IDbConnection conn = new SqlConnection(PubConstant.GetConnectionString()))
            {
                int count = conn.Execute(strSql.ToString(), model);
                return (count > 0);
            }
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model">样品实体</param>
        /// <returns>新增样品ID</returns>
        public int Add(tb_Sample model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into tb_Sample(");
            strSql.Append("AreaID,name,standard,batch,productDate,modelType,expirationDate,packaging,isDetection,detectionUser,detectionDate,createUser,createDate,updateUser,updateDate,temp1,temp2,putArea,sampleHandle,handleUser,handleDate,sampleAdmin,detectionGist,detectionMethod,InspectCompany,detectionAdress,detectionCompany,InspectAddress,projectName,testMethod,sampleNum,protNum,storcondition,checkdepar,urgentlevel)");
            strSql.Append(" values (");
            strSql.Append("@AreaID,@name,@standard,@batch,@productDate,@modelType,@expirationDate,@packaging,@isDetection,@detectionUser,@detectionDate,@createUser,@createDate,@updateUser,@updateDate,@temp1,@temp2,@putArea,@sampleHandle,@handleUser,@handleDate,@sampleAdmin,@detectionGist,@detectionMethod,@InspectCompany,@detectionAdress,@detectionCompany,@InspectAddress,@projectName,@testMethod,@sampleNum,@protNum,@storcondition,@checkdepar,@urgentlevel)");
            strSql.Append(";select @@IDENTITY");

            using (IDbConnection conn = new SqlConnection(PubConstant.GetConnectionString()))
            {
                int id = Convert.ToInt32(conn.ExecuteScalar(strSql.ToString(), model));
                return id;
            }
        }

        /// <summary>
        /// 销毁样品
        /// </summary>
        /// <param name="model">样品实体</param>
        /// <returns>是否执行成功</returns>
        public bool BatchDestroy(tb_Sample model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"update tb_Sample set 
                                handleUser=@handleUser,
                                handleDate=@handleDate
                            where id in (@ids)");

            using (IDbConnection conn = new SqlConnection(PubConstant.GetConnectionString()))
            {
                int count = conn.Execute(strSql.ToString(), model);
                return (count > 0);
            }
        }

        /// <summary>
        /// 删除样品
        /// </summary>
        /// <param name="model">样品实体</param>
        /// <returns>是否执行成功</returns>
        public bool Delete(tb_Sample model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"delete from tb_Sample  where id = @id");
            using (IDbConnection conn = new SqlConnection(PubConstant.GetConnectionString()))
            {
                int count = conn.Execute(strSql.ToString(), model);
                return (count > 0);
            }
        }
    }
}
