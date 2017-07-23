using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.LabManage
{
    /// <summary>
    /// 实验考核
    /// </summary>
    public class E_ExperimentCheck
    {
        /// <summary>
        /// 样品ID
        /// </summary>
        public int sampleid { get; set; }

        /// <summary>
        /// 样品名称
        /// </summary>
        public string samplename { get; set; }

        /// <summary>
        /// 项目ID
        /// </summary>
        public int projectid { get; set; }
        
        /// <summary>
        /// 项目名称
        /// </summary>
        public string projectname { get; set; }

        /// <summary>
        /// 抽/送检日期
        /// </summary>
        public string detectiondate { get; set; }

        /// <summary>
        /// 完成日式（既：原始记录：检测日期）
        /// </summary>
        public DateTime detecttime { get; set; }

        /// <summary>
        /// 完成人（既：原始记录：检验人）
        /// </summary>
        public int detectpersonnelid { get; set; }

        /// <summary>
        /// 完成人名称
        /// </summary>
        public string detectpersonnelname { get; set; }
    }
}
