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
        
    }
}
