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
        
    }
}