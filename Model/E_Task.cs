using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
  public  class E_Task
    {
        /// <summary>
        /// id
        /// </summary>		
        private int _id;
        public int id
        {
            get { return _id; }
            set { _id = value; }
        }
        /// <summary>
        /// taskname
        /// </summary>		
        private string _taskname;
        public string taskname
        {
            get { return _taskname; }
            set { _taskname = value; }
        }
        /// <summary>
        /// 负责人
        /// </summary>		
        private string _director;
        public string director
        {
            get { return _director; }
            set { _director = value; }
        }
        /// <summary>
        /// 发布人
        /// </summary>		
        private int _publishid;
        public int publishid
        {
            get { return _publishid; }
            set { _publishid = value; }
        }
        /// <summary>
        /// remark
        /// </summary>		
        private string _remark;
        public string remark
        {
            get { return _remark; }
            set { _remark = value; }
        }
        /// <summary>
        /// 待完成、完成、延期、延期完成(1,2,3,4)
        /// </summary>		
        private int _status;
        public int status
        {
            get { return _status; }
            set { _status = value; }
        }
        /// <summary>
        /// 计划完成时间
        /// </summary>		
        private DateTime? _tasktime;
        public DateTime? tasktime
        {
            get { return _tasktime; }
            set { _tasktime = value; }
        }
        /// <summary>
        /// 发布时间
        /// </summary>		
        private DateTime _publishtime;
        public DateTime publishtime
        {
            get { return _publishtime; }
            set { _publishtime = value; }
        }
        /// <summary>
        /// 完成日期
        /// </summary>		
        private DateTime? _finishtime;
        public DateTime? finishtime
        {
            get { return _finishtime; }
            set { _finishtime = value; }

        }
        public string publishname { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; } = 15;
    }
}
