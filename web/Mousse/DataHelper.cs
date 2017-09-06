using Comp;
using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web
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

        public static String GetStatusName(int id) {
            return GetStatusTypeList()[id];
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
        
    }
}