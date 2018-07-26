using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;

namespace Weixin.Code
{
    /// <summary>
    /// DataTable 公共帮助类
    /// </summary>
    public class DataTableHelper
    {
        #region DataTable转XML

        /// <summary>
        /// DataTable 转 XML
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string DataTableToXML(DataTable dt)
        {
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    System.IO.StringWriter writer = new System.IO.StringWriter();
                    dt.WriteXml(writer);
                    return writer.ToString();
                }
            }
            return String.Empty;
        }
        /// <summary>
        /// DataSet 转 XML
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static string DataSetToXML(DataSet ds)
        {
            if (ds != null)
            {
                System.IO.StringWriter writer = new System.IO.StringWriter();
                ds.WriteXml(writer);
                return writer.ToString();
            }
            return String.Empty;
        }

        #endregion

        #region DataTable转Hashtable

        /// <summary>
        /// DataTable 转 DataTableToHashtable
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static Hashtable DataTableToHashtable(DataTable dt)
        {
            Hashtable ht = new Hashtable();
            foreach (DataRow dr in dt.Rows)
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    string key = dt.Columns[i].ColumnName;
                    ht[key.ToUpper()] = dr[key];
                }
            }
            return ht;
        }

        #endregion

        #region DataRow转HashTable

        /// <summary>
        /// DataRow  转  HashTable
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public static Hashtable DataRowToHashTable(DataRow dr)
        {
            Hashtable htReturn = new Hashtable(dr.ItemArray.Length);
            foreach (DataColumn dc in dr.Table.Columns)
            {
                htReturn.Add(dc.ColumnName, dr[dc.ColumnName]);
            }
            return htReturn;
        }

        #endregion

        #region DataTable转IList

        /// <summary>
        /// DataTable转IList数据行是用Hashtable对象存
        /// </summary>
        /// <param name="dt"></param>
        /// <returns>数据行是用Hashtable对象存</returns>
        public static IList<Hashtable> DataTableToArrayList(DataTable dt)
        {
            if (dt == null) return new List<Hashtable>();
            IList<Hashtable> datas = new List<Hashtable>();
            foreach (DataRow dr in dt.Rows)
            {
                Hashtable ht = DataRowToHashTable(dr);
                datas.Add(ht);
            }
            return datas;
        }

        #endregion

        #region DataRow转换为实体类

        public static T DataRowToModel<T>(DataRow dr)
        {
            T model = Activator.CreateInstance<T>();
            foreach (PropertyInfo pi in model.GetType().GetProperties(BindingFlags.GetProperty | BindingFlags.Public | BindingFlags.Instance))
            {
                if (!IsNullOrDBNull(dr[pi.Name]))
                {
                    pi.SetValue(model, HackType(dr[pi.Name], pi.PropertyType), null);
                }
            }
            return model;
        }
        public static object HackType(object value, Type conversionType)
        {
            if (conversionType.IsGenericType && conversionType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                if (value == null) return null;
                System.ComponentModel.NullableConverter nullableConverter = new System.ComponentModel.NullableConverter(conversionType);
                conversionType = nullableConverter.UnderlyingType;
            }
            return Convert.ChangeType(value, conversionType);
        }
        /// <summary>
        /// 对可空类型进行判断转换
        /// </summary>
        /// <param name="value"></param>
        /// <param name="conversionType"></param>
        /// <returns></returns>
        public static bool IsNullOrDBNull(object obj)
        {
            return ((obj is DBNull) || string.IsNullOrEmpty(obj.ToString())) ? true : false;
        }

        #endregion

        #region DataTable转实体类对象LIST

        /// <summary>
        /// DataTable转为对象LIST
        /// </summary>
        /// <param name="dt"></param>
        /// <returns>数据行是对象的类，类的属性与数据字段一致</returns>
        public static IList DataTableToIList<T>(DataTable dt)
        {
            // 定义集合    
            string tempName = "";
            IList list = new List<T>();
            foreach (DataRow dr in dt.Rows)
            {
                T obj = Activator.CreateInstance<T>();
                //获得此模型的公共属性
                PropertyInfo[] propertys = obj.GetType().GetProperties();
                foreach (PropertyInfo pi in propertys)
                {
                    tempName = pi.Name;
                    //检查DataTable是否包含此列
                    if (dt.Columns.Contains(tempName))
                    {
                        //判断此属性是否有Setter    
                        if (!pi.CanWrite) continue;
                        object value = dr[tempName];
                        if (value != DBNull.Value)
                        {
                            pi.SetValue(obj, value, null);
                        }
                    }
                }
                list.Add(obj);
            }
            return list;
        }

        #endregion

        #region DataTable根据条件过滤表

        /// <summary>
        /// 根据条件过滤表的内容
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static DataTable DataTableSelect(DataTable dt, string condition, string orderField = "")
        {
            if (DataTableHelper.IsExistRow(dt))
            {
                if (string.IsNullOrEmpty(condition))
                {
                    return dt;
                }
                else
                {
                    DataTable newdt = new DataTable();
                    newdt = dt.Clone();
                    DataRow[] dr;
                    if (orderField == "")
                    {
                        dr = dt.Select(condition);
                    }
                    else
                    {
                        dr = dt.Select(condition, orderField);
                    }
                    for (int i = 0; i < dr.Length; i++)
                    {
                        newdt.ImportRow((DataRow)dr[i]);
                    }
                    return newdt;
                }
            }
            else
            {
                return null;
            }
        }

        #endregion

        #region DataTable过滤重复数据

        /// <summary>
        /// 返回执行Select distinct后的DataTable
        /// </summary>
        /// <param name="SourceTable">源数据表</param>
        /// <param name="FieldNames">字段集</param>
        /// <returns></returns>
        public static DataTable SelectDistinct(DataTable SourceTable, string[] FieldNames)
        {
            object[] lastValues;
            DataTable newTable;
            DataRow[] orderedRows;

            if (FieldNames == null || FieldNames.Length == 0)
            {
                throw new ArgumentNullException("FieldNames");
            }
            lastValues = new object[FieldNames.Length];
            newTable = new DataTable();
            foreach (string fieldName in FieldNames)
            {
                newTable.Columns.Add(fieldName, SourceTable.Columns[fieldName].DataType);
            }
            orderedRows = SourceTable.Select("", string.Join(",", FieldNames));

            foreach (DataRow row in orderedRows)
            {
                if (!fieldValuesAreEqual(lastValues, row, FieldNames))
                {
                    newTable.Rows.Add(createRowClone(row, newTable.NewRow(), FieldNames));

                    setLastValues(lastValues, row, FieldNames);
                }
            }
            return newTable;
        }
        private static DataRow createRowClone(DataRow sourceRow, DataRow newRow, string[] fieldNames)
        {
            foreach (string field in fieldNames)
            {
                newRow[field] = sourceRow[field];
            }
            return newRow;
        }
        private static void setLastValues(object[] lastValues, DataRow sourceRow, string[] fieldNames)
        {
            for (int i = 0; i < fieldNames.Length; i++)
                lastValues[i] = sourceRow[fieldNames[i]];
        }
        private static bool fieldValuesAreEqual(object[] lastValues, DataRow currentRow, string[] fieldNames)
        {
            bool areEqual = true;

            for (int i = 0; i < fieldNames.Length; i++)
            {
                if (lastValues[i] == null || !lastValues[i].Equals(currentRow[fieldNames[i]]))
                {
                    areEqual = false;
                    break;
                }
            }
            return areEqual;
        }

        #endregion

        #region 检查DataTable是否有数据行

        /// <summary>
        /// 检查DataTable 是否有数据行
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <returns></returns>
        public static bool IsExistRow(DataTable dt)
        {
            if (dt != null && dt.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

        #region 判断DataTable中是否有某列
        /// <summary>
        /// 判断DataTable中是否有某列
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public static bool IsExistColumn(DataTable dt, string columnName)
        {
            bool b = false;
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                if (dt.Columns[i].ColumnName == columnName)
                {
                    b = true;
                }
            }
            return b;
        }
        #endregion

        #region 排序表的视图

        /// <summary>
        /// 排序表的视图
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="sorts"></param>
        /// <returns></returns>
        public static DataTable SortedTable(DataTable dt, params string[] sorts)
        {
            if (dt.Rows.Count > 0)
            {
                string tmp = "";
                for (int i = 0; i < sorts.Length; i++)
                {
                    tmp += sorts[i] + ",";
                }
                dt.DefaultView.Sort = tmp.TrimEnd(',');
            }
            return dt;
        }

        #endregion

        #region DataTable分页

        /// <summary>
        /// Datatable分页
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="PageIndex">当前页</param>
        /// <param name="PageSize">页大小</param>
        /// <returns></returns>
        public static DataTable GetPagedTable(DataTable dt, int PageIndex, int PageSize)
        {
            if (PageIndex == 0) return dt;
            DataTable newdt = dt.Copy();
            newdt.Clear();
            int rowbegin = (PageIndex - 1) * PageSize;
            int rowend = PageIndex * PageSize;
            if (rowbegin >= dt.Rows.Count) return newdt;
            if (rowend > dt.Rows.Count) rowend = dt.Rows.Count;
            for (int i = rowbegin; i <= rowend - 1; i++)
            {
                DataRow newdr = newdt.NewRow();
                DataRow dr = dt.Rows[i];
                foreach (DataColumn column in dt.Columns)
                {
                    newdr[column.ColumnName] = dr[column.ColumnName];
                }
                newdt.Rows.Add(newdr);
            }
            return newdt;
        }

        #endregion

        #region StringKeyValue转为DataTable
        /// <summary>
        /// 字符串分割转换为DataTable   ≌; ☻; ☺
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static DataTable StringKeyValueToDataTable(string str)
        {
            DataTable dt = new DataTable();
            if (!string.IsNullOrEmpty(str))
            {
                string[] arrs1 = str.Trim().Split('≌');
                foreach (string arr1 in arrs1)
                {
                    DataRow dr = dt.NewRow();
                    string[] arrs2 = arr1.Trim().Split('☺');
                    foreach (string arr2 in arrs2)
                    {
                        if (arr2.Trim().Length > 0)
                        {
                            string[] arrs3 = arr2.Trim().Split('☻');
                            string col = arrs3[0].ToString().Trim();
                            if (!string.IsNullOrEmpty(col))
                            {
                                if (dt.Columns.IndexOf(col) == -1)
                                {
                                    dt.Columns.Add(col);
                                }
                                dr[col] = arrs3[1].ToString().Trim();
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty(arr1))
                    {
                        dt.Rows.Add(dr);
                    }
                }
            }
            return dt;
        }
        #endregion

        #region DataTable转为表类型
        public static DataTable GetSqlTableType(DataTable olddt, DataTable newdt)
        {
            for (int i = 0; i < olddt.Rows.Count; i++)
            {
                newdt.Rows.Add();
                foreach (DataColumn Col in newdt.Columns)
                {
                    if (!olddt.Columns.Contains(Col.ColumnName))
                    {
                        continue;
                    }
                    else if (string.IsNullOrEmpty(olddt.Rows[i][Col.ColumnName].ToString()))
                    {
                        continue;
                    }
                    newdt.Rows[i][Col.ColumnName] = olddt.Rows[i][Col.ColumnName];
                }
            }
            return newdt;
        }
        #endregion


    }
}