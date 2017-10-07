using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Statistics
{
    /// <summary>
    /// 实验计划统计列表
    /// </summary>
    public class E_ExpePlanStatistics
    {
        /// <summary>
        /// 主要负责人ID
        /// </summary>
        public int headpersonnelid { get; set; }

        /// <summary>
        /// 主要负责人名称
        /// </summary>
        public string headpersonnename { get; set; }

        /// <summary>
        /// 完成
        /// </summary>
        public int completed { get; set; }

        /// <summary>
        /// 未完成
        /// </summary>
        public int notcompleted { get; set; }
    }
}
