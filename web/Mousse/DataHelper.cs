using BLL.RoleManage;
using Comp;
using DAL;
using DAL.RoleManage;
using Model;
using Model.RoleManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace System.Web.Mvc
{
    public class DataHelper
    {
       static  tb_BaseDAL tbase = new tb_BaseDAL();

        public static Dictionary<int,String> GetStatusTypeList()
        {
            //待完成、完成、延期、延期完成
            Dictionary<int, String> dic = new Dictionary<int, string>();
            dic.Add(1, "待完成");
            dic.Add(2, "完成");
            dic.Add(3, "延期");
            dic.Add(4, "延期完成");
            return dic;
        }
        public static List<String> GetLockList()
        {
            //待完成、完成、延期、延期完成
            List<String> list = new List<string>();
            list.Add("玻璃柜2*4");
            list.Add("玻璃柜3*4");
            list.Add("不透明柜2*4");
            list.Add("不透明柜3*4");
            list.Add("不透明柜4*4");
            list.Add("不透明柜5*4");
            list.Add("不透明柜6*4");
            list.Add("冷藏柜");
            return list;
        }
        public static String GetStatusName(int id) {
            if (GetStatusTypeList().ContainsKey(id)) {
                return GetStatusTypeList()[id];
            }
            return "";
        }
        public static List<tb_Base> GetBaseList()
        {
            List<tb_Base> list = null; 
            object obj= Utils.Cache("GetBaseList");
            if (obj != null)
            {
                list = obj as List<tb_Base>;
            }
            else {
                list= tbase.GetBaseList();
                Utils.Cache("GetBaseList",list);
            }
            return list;
        }
        /// <summary>
        /// 获取基础数据的名称
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string GetBaseName(int id) {
            return GetBaseList().Find(a => a.id == id)?.baseName;
        }
        /// <summary>
        /// 获取单位
        /// </summary>
        /// <returns></returns>
        public static List<E_tb_Area> GetAreaList() {
            List<E_tb_Area> list = null;
            object obj = Utils.Cache("GetAreaList");
            if (obj != null)
            {
                list = obj as List<E_tb_Area>;
            }
            else
            {
                list = new T_tb_Area().GetModelList("");
                Utils.Cache("GetAreaList", list);
            }
            return list;
        }
    }
}