using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using System.Collections;

namespace QTP.service
{
    static class DataTableConverter
    {
        public static DataTable from<TResult>(this IEnumerable<TResult> ListValue) where TResult : class, new()
        {
            //建立一個回傳用的 DataTable
            DataTable dt = new DataTable();

            //取得映射型別
            Type type = typeof(TResult);

            //宣告一個 PropertyInfo 陣列，來接取 Type 所有的共用屬性
            PropertyInfo[] PI_List = null;

            foreach (var item in ListValue)
            {
                //判斷 DataTable 是否已經定義欄位名稱與型態
                if (dt.Columns.Count == 0)
                {
                    //取得 Type 所有的共用屬性
                    PI_List = item.GetType().GetProperties();

                    //將 List 中的 名稱 與 型別，定義 DataTable 中的欄位 名稱 與 型別
                    foreach (var item1 in PI_List)
                    {
                        dt.Columns.Add(item1.Name, item1.PropertyType);
                    }
                }

                //在 DataTable 中建立一個新的列
                DataRow dr = dt.NewRow();

                //將資料逐筆新增到 DataTable 中
                foreach (var item2 in PI_List)
                {
                    dr[item2.Name] = item2.GetValue(item, null);
                }

                dt.Rows.Add(dr);
            }

            dt.AcceptChanges();

            return dt;
        }

        public static List<TResult> to<TResult>(this DataTable DataTableValue) where TResult : class, new()
        {
            //建立一個回傳用的 List<TResult>
            List<TResult> Result_List = new List<TResult>();

            //取得映射型別
            Type type = typeof(TResult);

            //儲存 DataTable 的欄位名稱
            List<PropertyInfo> pr_List = new List<PropertyInfo>();

            foreach (PropertyInfo item in type.GetProperties())
            {
                if (DataTableValue.Columns.IndexOf(item.Name) == -1)
                {
                    pr_List.Add(item);
                }

            }

            //逐筆將 DataTable 的值新增到 List<TResult> 中
            foreach (DataRow item in DataTableValue.Rows)
            {
                TResult tr = new TResult();

                foreach (PropertyInfo item1 in pr_List)
                {
                    if (item.Table.Columns[item1.Name] == null) continue;
                    if (item[item1.Name] != DBNull.Value)
                    {
                        item1.SetValue(tr, item[item1.Name], null);
                    }
                }

                Result_List.Add(tr);
            }

            return Result_List;
        }
    }
}
