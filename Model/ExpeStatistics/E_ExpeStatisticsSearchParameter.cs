using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ExpeStatistics
{
    /// <summary>
    /// 实验统计查询参数实体
    /// </summary>
    public class E_ExpeStatisticsSearchParameter
    {
        /// <summary>
        /// 页索引
        /// </summary>
        public int page { get; set; } = 1;

        /// <summary>
        /// 页大小
        /// </summary>
        public int pageSize { get; set; } = 20;
        
        /// <summary>
        /// 检验开始日期
        /// </summary>
        public string txt_StartTime { get; set; }

        /// <summary>
        /// 检验结束日期
        /// </summary>
        public string txt_EndTime { get; set; }

        /// <summary>
        /// 检验单位 
        /// </summary>
        public string GHS { get; set; }

        /// <summary>
        /// 送/抽检单位 
        /// </summary>
        public string Department { get; set; }

        /// <summary>
        /// 抽样地址
        /// </summary>
        public string DetectionAdress { get; set; }

        /// <summary>
        /// 样品名称
        /// </summary>
        public string samplenum { get; set; }

        /// <summary>
        /// 检验项目
        /// </summary>
        public string projectname { get; set; }

        /// <summary>
        /// 检验人
        /// </summary>
        public string testpersonnelname { get; set; }


    }
}
