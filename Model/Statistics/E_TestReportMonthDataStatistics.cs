using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Statistics
{
    /// <summary>
    /// 检验报告按月份统计
    /// </summary>
    public class E_TestReportDataStatistics
    {
        /// <summary>
        /// 月份时间名称
        /// </summary>
        public string updatetime { get; set; }

        /// <summary>
        /// 未审批个数
        /// </summary>
        public int examinecount { get; set; }

        /// <summary>
        /// 未批准个数
        /// </summary>
        public int approvalcount { get; set; }

        /// <summary>
        /// 总检验报告个数
        /// </summary>
        public int total { get; set; }
    }
}
