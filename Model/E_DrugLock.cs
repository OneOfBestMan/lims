using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
  public   class E_DrugLock
    {
        /// <summary>
        /// id
        /// </summary>		
        public int id
        {
            get;
            set;
        }
        /// <summary>
        /// 药品柜名称
        /// </summary>		
        public string lockName
        {
            get;
            set;
        }
        /// <summary>
        /// 标志（危险品、化学品、都是、都不是）
        /// </summary>		
        public string mark
        {
            get;
            set;
        }
        /// <summary>
        /// createUser
        /// </summary>		
        public int? createUser
        {
            get;
            set;
        }
        /// <summary>
        /// createDate
        /// </summary>		
        public DateTime? createDate
        {
            get;
            set;
        }
        /// <summary>
        /// updateUser
        /// </summary>		
        public int? updateUser
        {
            get;
            set;
        }
        /// <summary>
        /// updateDate
        /// </summary>		
        public DateTime? updateDate
        {
            get;
            set;
        }
        /// <summary>
        /// 药品柜类型（玻璃柜3*4、不透明柜2*4、不透明柜3*4）
        /// </summary>		
        public string lockType
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
        /// pic
        /// </summary>		
        public string pic
        {
            get;
            set;
        }
        /// <summary>
        /// 是否化学
        /// </summary>		
        public int? ischem
        {
            get;
            set;
        }
        /// <summary>
        /// 是否危险
        /// </summary>		
        public int? isdanger
        {
            get;
            set;
        }
        public int? locktypeid { get; set; }
    }
}
