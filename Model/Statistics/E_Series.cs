using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Statistics
{
    /// <summary>
    /// 统计报表数据单元实体
    /// </summary>
    public class E_Series
    {
        /// <summary>
        /// 对应名称
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 显示方式
        /// </summary>
        public string type { get; set; } = "line";

        /// <summary>
        /// 数据值
        /// </summary>
        public List<int> data { get; set; }
    }
}
