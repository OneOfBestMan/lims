/**  版本信息模板在安装目录下，可自行修改。
* tb_Sample.cs
*
* 功 能： N/A
* 类 名： tb_Sample
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/12/9 14:32:57   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.ComponentModel;

namespace Model
{
	/// <summary>
	/// tb_Sample:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class tb_Sample
	{
        /// <summary>
        /// ID集合
        /// </summary>
        public string ids { get; set; }

        /// <summary>
        /// 序号ID
        /// </summary>
        [DisplayName("序号")]
        public int rowid { get; set; }

        /// <summary>
        /// 处理人名称
        /// </summary>
        public string handleusername { get; set; }

        /// <summary>
        /// 送样单位名称
        /// </summary>
        [DisplayName("处理人名称")]
        public string inspectcompanyname { get; set; }

        /// <summary>
        /// 单位ID
        /// </summary>
        [DisplayName("单位ID")]
        public int AreaID { get; set; }

        /// <summary>
        /// 存储条件
        /// </summary>
        [DisplayName("存储条件")]
        public string storcondition { get; set; }

        /// <summary>
        /// 测试部门
        /// </summary>
        [DisplayName("测试部门")]
        public string checkdepar { get; set; }

        /// <summary>
        /// 紧急程度
        /// </summary>
        [DisplayName("紧急程度")]
        public int urgentlevel { get; set; }

        public tb_Sample()
        { }

        #region Model
        private int _id;
        private string _name;
        private string _standard;
        private string _batch;
        private DateTime? _productdate;
        private string _modeltype;
        private string _expirationdate;
        private string _packaging;
        private bool _isdetection;
        private string _detectionuser;
        private DateTime? _detectiondate;
        private int? _createuser;
        private DateTime? _createdate;
        private int? _updateuser;
        private DateTime? _updatedate;
        private string _temp1;
        private string _temp2;
        private string _putarea;
        private string _samplehandle;
        private int _handleuser;
        private DateTime? _handledate;
        private string _sampleadmin;
        private string _detectiongist;
        private string _detectionmethod;
        private int? _inspectcompany;
        private string _detectionadress;
        private string _detectioncompany;
        private string _inspectaddress;
        private string _projectname;
        private string _testmethod;
        private string _samplenum;
        private string _protnum;
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("样品ID")]
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 样品名称
        /// </summary>
        [DisplayName("样品名称")]
        public string name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 执行标准
        /// </summary>
        [DisplayName("执行标准")]
        public string standard
        {
            set { _standard = value; }
            get { return _standard; }
        }
        /// <summary>
        /// 样品数量
        /// </summary>
        [DisplayName("样品数量")]
        public string batch
        {
            set { _batch = value; }
            get { return _batch; }
        }
        /// <summary>
        /// 生产日期
        /// </summary>
        [DisplayName("生产日期")]
        public DateTime? productDate
        {
            set { _productdate = value; }
            get { return _productdate; }
        }
        /// <summary>
        /// 规格型号
        /// </summary>
        [DisplayName("规格型号")]
        public string modelType
        {
            set { _modeltype = value; }
            get { return _modeltype; }
        }
        /// <summary>
        /// 保质期
        /// </summary>
        [DisplayName("保质期")]
        public string expirationDate
        {
            set { _expirationdate = value; }
            get { return _expirationdate; }
        }
        /// <summary>
        /// 包装方式
        /// </summary>
        [DisplayName("包装方式")]
        public string packaging
        {
            set { _packaging = value; }
            get { return _packaging; }
        }
        /// <summary>
        /// 抽样形式
        /// </summary>
        [DisplayName("抽样形式")]
        public bool isDetection
        {
            set { _isdetection = value; }
            get { return _isdetection; }
        }
        /// <summary>
        /// 抽样人
        /// </summary>
        [DisplayName("抽样人")]
        public string detectionUser
        {
            set { _detectionuser = value; }
            get { return _detectionuser; }
        }
        /// <summary>
        /// 抽样日期
        /// </summary>
        [DisplayName("抽样日期")]
        public DateTime? detectionDate
        {
            set { _detectiondate = value; }
            get { return _detectiondate; }
        }
        /// <summary>
        /// 创建人
        /// </summary>
        [DisplayName("创建人")]
        public int? createUser
        {
            set { _createuser = value; }
            get { return _createuser; }
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        [DisplayName("创建时间")]
        public DateTime? createDate
        {
            set { _createdate = value; }
            get { return _createdate; }
        }
        /// <summary>
        /// 更新人
        /// </summary>
        [DisplayName("更新人")]
        public int? updateUser
        {
            set { _updateuser = value; }
            get { return _updateuser; }
        }
        /// <summary>
        /// 更新时间
        /// </summary>
        [DisplayName("更新时间")]
        public DateTime? updateDate
        {
            set { _updatedate = value; }
            get { return _updatedate; }
        }
        /// <summary>
        /// 商标
        /// </summary>
        public string temp1
        {
            set { _temp1 = value; }
            get { return _temp1; }
        }
        /// <summary>
        /// 项目状态（完成、检验中）
        /// </summary>
        public string temp2
        {
            set { _temp2 = value; }
            get { return _temp2; }
        }
        /// <summary>
        /// 存放位置
        /// </summary>
        public string putArea
        {
            set { _putarea = value; }
            get { return _putarea; }
        }
        /// <summary>
        /// 样品处理
        /// </summary>
        public string sampleHandle
        {
            set { _samplehandle = value; }
            get { return _samplehandle; }
        }
        /// <summary>
        /// 处理人
        /// </summary>
        [DisplayName("处理人")]
        public int handleUser
        {
            set { _handleuser = value; }
            get { return _handleuser; }
        }
        /// <summary>
        /// 处理时间
        /// </summary>
        [DisplayName("处理时间")]
        public DateTime? handleDate
        {
            set { _handledate = value; }
            get { return _handledate; }
        }
        /// <summary>
        /// 样品管理员
        /// </summary>
        [DisplayName("样品管理员")]
        public string sampleAdmin
        {
            set { _sampleadmin = value; }
            get { return _sampleadmin; }
        }
        /// <summary>
        /// 抽样依据
        /// </summary>
        [DisplayName("抽样依据")]
        public string detectionGist
        {
            set { _detectiongist = value; }
            get { return _detectiongist; }
        }
        /// <summary>
        /// 抽样方法
        /// </summary>
        [DisplayName("抽样方法")]
        public string detectionMethod
        {
            set { _detectionmethod = value; }
            get { return _detectionmethod; }
        }
        /// <summary>
        /// 送样单位
        /// </summary>
        [DisplayName("抽样人")]
        public int? InspectCompany
        {
            set { _inspectcompany = value; }
            get { return _inspectcompany; }
        }
        /// <summary>
        /// 抽样地址
        /// </summary>
        [DisplayName("抽样地址")]
        public string detectionAdress
        {
            set { _detectionadress = value; }
            get { return _detectionadress; }
        }
        /// <summary>
        /// 抽样单位
        /// </summary>
        [DisplayName("抽样单位")]
        public string detectionCompany
        {
            set { _detectioncompany = value; }
            get { return _detectioncompany; }
        }
        /// <summary>
        /// 送检地址
        /// </summary>
        [DisplayName("送检地址")]
        public string InspectAddress
        {
            set { _inspectaddress = value; }
            get { return _inspectaddress; }
        }
        /// <summary>
        /// 项目名称
        /// </summary>
        [DisplayName("项目名称")]
        public string projectName
        {
            set { _projectname = value; }
            get { return _projectname; }
        }
        /// <summary>
        /// 检验方法
        /// </summary>
        [DisplayName("检验方法")]
        public string testMethod
        {
            set { _testmethod = value; }
            get { return _testmethod; }
        }
        /// <summary>
        /// 样品编号
        /// </summary>
        [DisplayName("样品编号")]
        public string sampleNum
        {
            set { _samplenum = value; }
            get { return _samplenum; }
        }
        /// <summary>
        /// 产品批次
        /// </summary>
        [DisplayName("产品批次")]
        public string protNum
        {
            set { _protnum = value; }
            get { return _protnum; }
        }
        #endregion Model
	}
}

