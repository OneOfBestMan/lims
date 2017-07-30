using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Sample
{
    /// <summary>
    /// 分样单
    /// </summary>
    public class E_SamplingSheet
    {
        /// <summary>
        /// 样品编号
        /// </summary>
        public string samplenum { get; set; }

        /// <summary>
        /// 样品名称
        /// </summary>
        public string samplename { get; set; }

        /// <summary>
        /// 检验项目及标准
        /// </summary>
        public string checkprojectandstandard { get; set; }

        /// <summary>
        /// 存储条件
        /// </summary>
        public string storcondition { get; set; }

        /// <summary>
        /// 测试部门
        /// </summary>
        public string checkdepar { get; set; }

        /// <summary>
        /// 紧急程度
        /// </summary>
        public string urgentlevel { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string remarks { get; set; }
    }
}
