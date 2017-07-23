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
    }
}
