using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// 页面查询参数实体
    /// </summary>
    public class E_PageParameter
    {
        /// <summary>
        /// 起始时间
        /// </summary>
        public DateTime? starttime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? endtime { get; set; }

        /// <summary>
        /// 是否查询
        /// </summary>
        public int issearch { get; set; }

        /// <summary>
        /// 每页显示数据条数
        /// </summary>
        public int pagesize { get; set; }

        /// <summary>
        /// 页码
        /// </summary>
        public int pageindex { get; set; }

        /// <summary>
        /// 作业区ID（单位ID）
        /// </summary>
        public int areaid{get;set;}

        /// <summary>
        /// 样品名称
        /// </summary>
        public string samplename { get; set; }

        /// <summary>
        /// 抽样人
        /// </summary>
        public string detectionuser { get; set; }
        
        /// <summary>
        /// 创建人ID
        /// </summary>
        public int createuser { get; set; }

        /// <summary>
        /// 样品ID集合
        /// </summary>
        public string sampleids { get; set; }

        /// <summary>
        /// 签发开始时间
        /// </summary>
        public DateTime? issuedtimestart { get; set; }

        /// <summary>
        /// 签发结束时间
        /// </summary>
        public DateTime? issuedtimeend { get; set; }

        /// <summary>
        /// 抽样开始日期
        /// </summary>
        public DateTime? samplingtimestart { get; set; }

        /// <summary>
        /// 抽样结束日期
        /// </summary>
        public DateTime? samplingtimeend { get; set; }

        /// <summary>
        /// 样品编号
        /// </summary>
        public string samplenum { get; set; }

        /// <summary>
        /// 抽送检单位
        /// </summary>
        public string department { get; set; }

        /// <summary>
        /// 主检人
        /// </summary>
        public string maintestpersonne { get; set; }

        /// <summary>
        /// 数据行数
        /// </summary>
        public int count { get; set; }

        /// <summary>
        /// 项目ID
        /// </summary>
        public int projectid { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        public string projectname { get; set; }

        /// <summary>
        /// 任务单号
        /// </summary>
        public string taskno { get; set; }

        /// <summary>
        /// 原始记录ID
        /// </summary>
        public int recordid { get; set; }

        /// <summary>
        /// 实验计划ID
        /// </summary>
        public int planid { get; set; }

        /// <summary>
        /// 实验计划类别
        /// </summary>
        public int plantype { get; set; } = -1;

        /// <summary>
        /// 状态
        /// </summary>
        public int status { get; set; }
    }
}
