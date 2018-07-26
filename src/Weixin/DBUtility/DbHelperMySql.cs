using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using System.Reflection;
using System.Web.Configuration;
using Weixin.Code;

namespace Weixin.DBUtility
{
    public class DbHelperMySql
    {
        public static string ParamKey = "@";
        public static string connectionString = WebConfigurationManager.AppSettings["ConnectionString"];
        private static readonly LogHelper log = new LogHelper("SqlServer数据库操作日志");

        public DbHelperMySql() { }

        #region 公用方法
        /// <summary>
        /// 实体对象转换为参数数组
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static MySqlParameter[] ModelToParameters<T>(T entity)
        {
            DbType dbType = new DbType();
            Type type = entity.GetType();
            PropertyInfo[] props = type.GetProperties();
            List<MySqlParameter> listSqlParam = new List<MySqlParameter>();
            foreach (PropertyInfo prop in props)
            {
                if (prop.GetValue(entity, null) != null)
                {
                    if (prop.PropertyType.ToString() == "System.Nullable`1[System.DateTime]")
                    {
                        dbType = DbType.DateTime;
                    }
                    else
                    {
                        dbType = DbType.AnsiString;
                    }
                    object value = prop.GetValue(entity, null);
                    MySqlParameter sqlPara = new MySqlParameter(ParamKey + prop.Name, prop.GetValue(entity, null));
                    sqlPara.DbType = dbType;
                    listSqlParam.Add(sqlPara);
                }
            }
            return listSqlParam.ToArray();
        }
        #endregion 

        #region 执行带参数的SQL
        /// <summary>
        /// 执行一条计算查询结果语句，返回查询结果（object）。
        /// </summary>
        /// <param name="SQLString">计算查询结果语句</param>
        /// <returns>查询结果（object）</returns>
        public static object GetSingle(string SQLString, params MySqlParameter[] cmdParms)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    try
                    {
                        PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                        object obj = cmd.ExecuteScalar();
                        cmd.Parameters.Clear();
                        if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                        {
                            return null;
                        }
                        else
                        {
                            return obj;
                        }
                    }
                    catch (MySqlException ex)
                    {
                        log.WriteLog("MySql获取单个数据出错，错误信息："+ex.Message);
                        throw ex;
                    }
                }
            }
        }
        /// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <returns>影响的记录数</returns>
        public static int ExecuteSql(string SQLString, params MySqlParameter[] cmdParms)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    try
                    {
                        PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                        int rows = cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                        return rows;
                    }
                    catch (System.Data.SqlClient.SqlException e)
                    {
                        log.WriteLog("执行SQL出错：" + SQLString + "；错误信息：" + e.Message);
                        throw e;
                    }
                }
            }
        }
        /// <summary>
        /// 准备Command的方法
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="conn"></param>
        /// <param name="trans"></param>
        /// <param name="cmdText"></param>
        /// <param name="cmdParms"></param>
        private static void PrepareCommand(MySqlCommand cmd, MySqlConnection conn, MySqlTransaction trans, string cmdText, MySqlParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if (trans != null)
            {
                cmd.Transaction = trans;
            }
            cmd.CommandType = CommandType.Text;
            if (cmdParms != null)
            {
                foreach (MySqlParameter parameter in cmdParms)
                {
                    if ((parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Input) && (parameter.Value == null))
                    {
                        parameter.Value = DBNull.Value;
                    }
                    cmd.Parameters.Add(parameter);
                }
            }
        }
        #endregion

        #region 通过实体增删改
        /// <summary>
        /// 新增
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="entity">实体</param>
        /// <returns>返回整数</returns>
        public static int Insert<T>(T entity,string pkName)
        {
            return ExecuteSql(GetInsertSql(entity,pkName).ToString(), ModelToParameters(entity));
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="entity">实体</param>
        /// <param name="pkName">主键</param>
        /// <returns></returns>
        public static int Update<T>(T entity, string pkName)
        {
            return ExecuteSql(GetUpdateSql(entity, pkName).ToString(), ModelToParameters(entity));
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="pkName">主键名</param>
        /// <param name="pkVal">主键值</param>
        /// <returns></returns>
        public static int Delete(string tableName, string pkName, string pkVal)
        {
            return ExecuteSql(GetDeleteSql(tableName, pkName).ToString(), new MySqlParameter[] { new MySqlParameter(ParamKey + pkName, pkVal) });
        }
        #endregion

        #region 拼接增删改的Sql
        /// <summary>
        /// 泛型方法，反射生成InsertSql语句
        /// </summary>
        /// <param name="entity">实体类</param>
        /// </param>
        /// <returns>int</returns>
        public static StringBuilder GetInsertSql<T>(T entity,string pkName)
        {
            Type type = entity.GetType();
            PropertyInfo[] props = type.GetProperties();
            StringBuilder sb = new StringBuilder();
            sb.Append(" INSERT INTO ");
            sb.Append(type.Name);
            sb.Append("(");
            StringBuilder sp = new StringBuilder();
            StringBuilder sb_prame = new StringBuilder();
            foreach (PropertyInfo prop in props)
            {
                if (prop.GetValue(entity, null) != null && prop.Name.ToLower() != pkName.ToLower())
                {
                    sb_prame.Append("," + (prop.Name));
                    sp.Append("," + ParamKey + "" + (prop.Name));
                }
            }
            sb.Append(sb_prame.ToString().Substring(1, sb_prame.ToString().Length - 1) + ") VALUES (");
            sb.Append(sp.ToString().Substring(1, sp.ToString().Length - 1) + ")");
            return sb;
        }
        /// <summary>
        /// 泛型方法，反射生成UpdateSql语句
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <param name="pkName">主键</param>
        /// <returns>StringBuilder</returns>
        public static StringBuilder GetUpdateSql<T>(T entity, string pkName)
        {
            Type type = entity.GetType();
            PropertyInfo[] props = type.GetProperties();
            StringBuilder sb = new StringBuilder();
            sb.Append(" UPDATE ");
            sb.Append(type.Name);
            sb.Append(" SET ");
            bool isFirstValue = true;
            foreach (PropertyInfo prop in props)
            {
                if (prop.GetValue(entity, null) != null)
                {
                    if (!prop.Name.Equals(pkName)) 
                    {
                        if (isFirstValue)
                        {
                            isFirstValue = false;
                            sb.Append(prop.Name);
                            sb.Append("=");
                            sb.Append(ParamKey + prop.Name);
                        }
                        else
                        {
                            sb.Append("," + prop.Name);
                            sb.Append("=");
                            sb.Append(ParamKey + prop.Name);
                        }
                    }
                }
            }
            sb.Append(" WHERE ").Append(pkName).Append("=").Append(ParamKey + pkName);
            return sb;
        }
        /// <summary>
        /// 拼接删除SQL语句
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="pkName">字段主键</param>
        /// <param name="pkVal">字段值</param>
        /// <returns></returns>
        public static StringBuilder GetDeleteSql(string tableName, string pkName)
        {
            StringBuilder sb = new StringBuilder("DELETE FROM " + tableName + " WHERE " + pkName + " = " + ParamKey + pkName + "");
            return sb;
        }
        #endregion

    }
}