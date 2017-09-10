using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
  public  class E_DrugCheck
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; } = 15;

        /// <summary>
        /// 药品名称
        /// </summary>		
        public string drugName
        {
            get;
            set;
        }
        /// <summary>
        /// 编号
        /// </summary>		
        public string drugCode
        {
            get;
            set;
        }
        /// <summary>
        /// 药品类别
        /// </summary>		
        public int drugType
        {
            get;
            set;
        }
        /// <summary>
        /// 单位
        /// </summary>		
        public int unit
        {
            get;
            set;
        }
        /// <summary>
        /// 生产日期
        /// </summary>		
        public DateTime productDate
        {
            get;
            set;
        }
        /// <summary>
        /// 有效期
        /// </summary>		
        public DateTime validDate
        {
            get;
            set;
        }
        /// <summary>
        /// 数量
        /// </summary>		
        public decimal amount
        {
            get;
            set;
        }
        /// <summary>
        /// 生产厂家
        /// </summary>		
        public string manufacturers
        {
            get;
            set;
        }
        /// <summary>
        /// 药品柜ID
        /// </summary>		
        public int cabinet
        {
            get;
            set;
        }
        /// <summary>
        /// 登记人
        /// </summary>		
        public int registrant
        {
            get;
            set;
        }
        /// <summary>
        /// 危险性分类
        /// </summary>		
        public string riskLevel
        {
            get;
            set;
        }
        /// <summary>
        /// 是否建立MSDS
        /// </summary>		
        public bool isMSDS
        {
            get;
            set;
        }
        /// <summary>
        /// 备注
        /// </summary>		
        public string remark
        {
            get;
            set;
        }
        /// <summary>
        /// createUser
        /// </summary>		
        public int createUser
        {
            get;
            set;
        }
        /// <summary>
        /// createDate
        /// </summary>		
        public DateTime createDate
        {
            get;
            set;
        }
        /// <summary>
        /// updateUser
        /// </summary>		
        public int updateUser
        {
            get;
            set;
        }
        /// <summary>
        /// updateDate
        /// </summary>		
        public DateTime updateDate
        {
            get;
            set;
        }
        /// <summary>
        /// temp1
        /// </summary>		
        public string temp1
        {
            get;
            set;
        }
        /// <summary>
        /// temp2
        /// </summary>		
        public string temp2
        {
            get;
            set;
        }
        /// <summary>
        /// 危险货物编号
        /// </summary>		
        public string un
        {
            get;
            set;
        }
        /// <summary>
        /// 药品规格型号
        /// </summary>		
        public string dsm
        {
            get;
            set;
        }
        /// <summary>
        /// 化学试剂纯度
        /// </summary>		
        public int purity
        {
            get;
            set;
        }
        /// <summary>
        /// 入库数量
        /// </summary>
        public int rukucount { get; set; }
        /// <summary>
        /// 出库数量
        /// </summary>
        public int chukucount { get; set; }

        public string putArea { get; set; }

        public DateTime? yxq { get; set; }

        public int AreaId { get; set; }

  
    }
}
