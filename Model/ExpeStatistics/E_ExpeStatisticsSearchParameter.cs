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
        //int pageNumber, int pageSize, string ddl_selecttype, string txt_dept, string ddl_type, string txt_search, string txt_StartTime, string txt_EndTime, string GHS, string Department

        /// <summary>
        /// 页索引
        /// </summary>
        public int pageNumber { get; set; }

        /// <summary>
        /// 页大小
        /// </summary>
        public int pageSize { get; set; }

        /// <summary>
        /// 送/抽检单位 查询方式（mhcx:模糊查询、qzpp:全字匹配）
        /// </summary>
        public string ddl_selecttype { get; set; }
        /// <summary>
        /// 送/抽检单位名称
        /// </summary>
        public string txt_dept { get; set; }
        
        /// <summary>
        /// 查询类别(ypmc:样品名称、jyxm:检验项目、jyr:检验人)
        /// </summary>
        public string ddl_type { get; set; }
        /// <summary>
        /// 查询内容
        /// </summary>
        public string txt_search { get; set; }

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

    }
}
