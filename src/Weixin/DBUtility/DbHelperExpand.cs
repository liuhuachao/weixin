using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace Weixin.DBUtility
{
    /// <summary>
    /// 数据库访问扩展
    /// 版本：2.0
    /// <author>
    ///		<name>yinbingyin</name>
    ///		<date>2013.09.27</date>
    /// </author>
    /// </summary>
    public sealed partial class DbHelperExpand
    {
        /// <summary>
        /// 批量操作每批次记录数
        /// </summary>
        public static int BatchSize = 2000;
        /// <summary>
        /// 超时时间
        /// </summary>
        public int CommandTimeOut = 600;
        /// <summary>
        /// 构造方法
        /// </summary>
        public DbHelperExpand()
        {
        }
        /// <summary>
        ///大批量数据插入
        /// </summary>
        /// <param name="table">数据表</param>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <returns></returns>
        public bool SqlServerBulkInsert(DataTable table, string connectionString)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlTransaction trans = conn.BeginTransaction();
                    SqlBulkCopy sqlbulkCopy = new SqlBulkCopy(conn, SqlBulkCopyOptions.Default, trans);
                    sqlbulkCopy.DestinationTableName = table.TableName;  //设置源表名称                    
                    sqlbulkCopy.BulkCopyTimeout = CommandTimeOut;        //设置超时限制
                    foreach (DataColumn dtColumn in table.Columns)
                    {
                        sqlbulkCopy.ColumnMappings.Add(dtColumn.ColumnName, dtColumn.ColumnName);
                    }
                    try
                    {
                        sqlbulkCopy.WriteToServer(table); //写入                        
                        trans.Commit();                   //提交事务
                        return true;
                    }
                    catch
                    {
                        trans.Rollback();
                        sqlbulkCopy.Close();
                        return false;
                    }
                    finally
                    {
                        conn.Close();
                        conn.Dispose();
                        sqlbulkCopy.Close();
                    }
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                return false;
            }
        }

    }
}