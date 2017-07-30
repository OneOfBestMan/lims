using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Comp
{
    public static class NPOITools
    {
        /// <summary>
        /// 将对象集合导出为Execl
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="datas">对象数据集合</param>
        public static void RenderToExcel<T>(List<T> datas,string path)
        {
            MemoryStream ms = new MemoryStream();
            IWorkbook workbook = new HSSFWorkbook();
            ISheet sheet = workbook.CreateSheet("导出数据");
            IRow headerRow = sheet.CreateRow(0);
            int rowIndex = 1, piIndex = 0,cellIndex=0;
            Type type = typeof(T);
            PropertyInfo[] pis = type.GetProperties();
            //int pisLen = pis.Length - 2;//减2是多了2个外键引用  
            PropertyInfo pi = null;
            while (piIndex < pis.Length)
            {
                pi = pis[piIndex];
                string displayName = GetDisplayName(type, pi.Name);
                if (!displayName.Equals(string.Empty))
                {//如果该属性指定了DisplayName，则输出  
                    try
                    {
                        headerRow.CreateCell(cellIndex).SetCellValue(displayName);
                    }
                    catch (Exception)
                    {
                        headerRow.CreateCell(cellIndex).SetCellValue("");
                    }
                    cellIndex++;
                }
                piIndex++;
            }


            foreach (T data in datas)
            {
                piIndex = 0;
                cellIndex = 0;
                IRow dataRow = sheet.CreateRow(rowIndex);
                while (piIndex < pis.Length)
                {
                    pi = pis[piIndex];
                    string displayName = GetDisplayName(type, pi.Name);
                    if (!displayName.Equals(string.Empty))
                    {//如果该属性指定了DisplayName，则输出 
                        try
                        {
                            dataRow.CreateCell(cellIndex).SetCellValue(pi.GetValue(data, null).ToString());
                        }
                        catch (Exception)
                        {
                            dataRow.CreateCell(cellIndex).SetCellValue("");
                        }
                        cellIndex++;
                    }
                    piIndex++;
                }
                rowIndex++;
            }
            workbook.Write(ms);
            FileStream dumpFile = new FileStream(path, FileMode.Create, FileAccess.ReadWrite);
            ms.WriteTo(dumpFile);
            ms.Flush();
            ms.Position = 0;
            dumpFile.Close();
        }

        /// <summary>
        /// 获取自定义属性
        /// </summary>
        /// <param name="modelType"></param>
        /// <param name="propertyDisplayName"></param>
        /// <returns></returns>
        public static string GetDisplayName(Type modelType, string propertyDisplayName)
        {
            return (System.ComponentModel.TypeDescriptor.GetProperties(modelType)[propertyDisplayName].Attributes[typeof(System.ComponentModel.DisplayNameAttribute)] as System.ComponentModel.DisplayNameAttribute).DisplayName;
        }
    }
}
