using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Web.Configuration;
using Weixin.Code;

namespace Weixin.DBUtility
{
    /// <summary>
    /// 数据访问抽象基础类
    /// </summary>
    public abstract class DbHelperSQL
    {
        public static string ParamKey = "@";
        public static string connectionString = WebConfigurationManager.AppSettings["ConnectionString"];
        private static readonly LogHelper loghelper = new LogHelper("SqlServer数据库操作日志");

        public DbHelperSQL() { }

        #region 公用方法

        public static List<SqlParameter> GetParametersList(Hashtable ht, DataTable dt)
        {
            List<SqlParameter> list = new List<SqlParameter>();
            foreach (string keys in ht.Keys)
            {
                if (keys.ToUpper().IndexOf("QTY") > -1)
                {
                    list.Add(new SqlParameter() { ParameterName = "@" + keys, Value = ht[keys].ToString() == "" ? "0" : ht[keys].ToString(), DbType = DbType.Decimal });
                }
                else
                {
                    list.Add(new SqlParameter() { ParameterName = "@" + keys, Value = ht[keys].ToString() });
                }
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    if (list.Find(
                        delegate(SqlParameter Parameter)
                        {
                            if (Parameter.ParameterName == "@" + dt.Columns[j].ToString())
                            {
                                if (dt.Columns[j].ToString().ToUpper().IndexOf("QTY") > -1)
                                {
                                    Parameter.Value = Parameter.Value.ToString() + '★' + (dt.Rows[i][j].ToString() == "" ? "0" : dt.Rows[i][j].ToString());
                                }
                                else
                                {
                                    Parameter.Value = Parameter.Value.ToString() + '★' + dt.Rows[i][j].ToString();
                                }
                                return true;
                            }
                            return false;
                        }) != null)
                    {
                        continue;
                    }

                    if (dt.Columns[j].ToString().ToUpper().IndexOf("QTY") > -1)
                    {
                        list.Add(new SqlParameter() { ParameterName = "@" + dt.Columns[j].ColumnName, Value = dt.Rows[i][j].ToString() == "" ? "0.00" : dt.Rows[i][j].ToString() });
                    }
                    else
                    {
                        list.Add(new SqlParameter() { ParameterName = "@" + dt.Columns[j].ColumnName, Value = dt.Rows[i][j].ToString() });
                    }

                }
            }
            return list;
        }
        /// <summary>
        /// 将HashTable转换为参数数组
        /// </summary>
        /// <param name="ht"></param>
        /// <returns></returns>
        public static SqlParameter[] HashTableToParameters(Hashtable ht)
        {
            int i = 0;
            SqlParameter[] _params = new SqlParameter[ht.Count];
            foreach (string key in ht.Keys)
            {
                object value = null;
                if (string.IsNullOrEmpty(ht[key].ToString().Trim()))
                {
                    value = null;
                }
                else
                {
                    value = ht[key];
                }
                _params[i] = new SqlParameter(ParamKey + key, value);
                i++;
            }
            return _params;
        }
        /// <summary>
        /// 实体对象转换为参数数组
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static SqlParameter[] ModelToParameters<T>(T entity)
        {
            DbType dbType = new DbType();
            Type type = entity.GetType();
            PropertyInfo[] props = type.GetProperties();
            List<SqlParameter> listSqlParam = new List<SqlParameter>();
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
                    SqlParameter sqlPara = new SqlParameter(ParamKey + prop.Name, prop.GetValue(entity, null));
                    sqlPara.DbType = dbType;
                    listSqlParam.Add(sqlPara);
                }
            }
            return listSqlParam.ToArray();
        }
        /// <summary>
        /// 获取最大序号
        /// </summary>
        /// <param name="FieldName"></param>
        /// <param name="TableName"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public static int GetMax(string FieldName, string TableName, string pkName = "", string pkVal = "")
        {
            StringBuilder strSql = new StringBuilder("SELECT MAX(ISNULL(" + FieldName + ",0))+1 FROM " + TableName);
            SqlParameter[] param = null;
            if (pkName != "")
            {
                strSql.Append(" WHERE " + pkName + " = " + ParamKey + pkName);
                param = new SqlParameter[] { new SqlParameter(ParamKey + pkName + "", pkVal) };
            }
            object obj = GetSingle(strSql.ToString(), param);
            if (obj == null)
            {
                return 1;
            }
            else
            {
                return int.Parse(obj.ToString());
            }
        }
        /// <summary>
        /// 获取条件和参数
        /// </summary>
        /// <param name="htKeyValue">条件键值对</param>
        /// <param name="sbWhere">返回Where条件</param>
        /// <param name="listPara">返回参数列表</param>
        public static void GetWhereAndParam(Hashtable htKeyValue, out StringBuilder sbWhere, out List<SqlParameter> listPara)
        {
            sbWhere = new StringBuilder();
            listPara = new List<SqlParameter>();
            if (htKeyValue != null)
            {
                foreach (DictionaryEntry item in htKeyValue)
                {
                    if (item.Value != null && !string.IsNullOrEmpty(item.Value.ToString()))
                    {
                        //起始时间
                        if (item.Key.ToString().Contains("TimeBegin"))
                        {
                            sbWhere.Append(string.Format(" AND DATEDIFF(DAY,{0},{1}) <= 0 ", item.Key.ToString().Replace("TimeBegin", "Time"), "@" + item.Key));
                            listPara.Add(new SqlParameter("@" + item.Key, item.Value.ToString()));
                        }
                        //截止时间
                        else if (item.Key.ToString().Contains("TimeEnd"))
                        {
                            sbWhere.Append(string.Format(" AND DATEDIFF(DAY,{0},{1}) >= 0 ", item.Key.ToString().Replace("TimeEnd", "Time"), "@" + item.Key));
                            listPara.Add(new SqlParameter("@" + item.Key, item.Value.ToString()));
                        }
                        //条件关键字
                        else if (item.Key.Equals("Condition"))
                        {
                            string value = htKeyValue["Keyword"].ToString().Trim();
                            if (!string.IsNullOrEmpty(value))
                            {
                                sbWhere.Append(" AND " + item.Value + " LIKE @" + item.Value);
                                listPara.Add(new SqlParameter("@" + item.Value, '%' + value + '%'));
                            }
                        }
                        //其他条件
                        else if (!item.Key.Equals("Condition") && !item.Key.Equals("Keyword"))
                        {
                            sbWhere.Append(" AND " + item.Key + " LIKE @" + item.Key);
                            listPara.Add(new SqlParameter("@" + item.Key, '%' + SqlFilterHelper.SelectFilter(item.Value.ToString().Trim()) + '%'));
                        }
                    }
                }
            }
        }

        #endregion

        #region 判断是否存在

        public static bool Exists(string strSql, params SqlParameter[] cmdParms)
        {
            object obj = GetSingle(strSql, cmdParms);
            if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        /// <summary>
        /// 影响行数
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="pkName">字段主键</param>
        /// <param name="pkVal">字段值</param>
        /// <returns>返回数量</returns>
        public static bool Exists(string tableName, string pkName, string pkVal)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" SELECT Count(1) FROM " + tableName + "");
            strSql.Append(" WHERE " + pkName + " = " + ParamKey + pkName);
            SqlParameter[] param = { new SqlParameter(ParamKey + pkName + "", pkVal) };
            return DbHelperSQL.Exists(strSql.ToString(), param);
        }

        #endregion

        #region  执行不带参数的SQL

        /// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <returns>影响的记录数</returns>
        public static int ExecuteSql(string SQLString, int Times = 0)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(SQLString, connection))
                {
                    try
                    {
                        connection.Open();
                        if (Times != 0) cmd.CommandTimeout = Times;
                        int rows = cmd.ExecuteNonQuery();
                        return rows;
                    }
                    catch (System.Data.SqlClient.SqlException e)
                    {
                        connection.Close();
                        throw e;
                    }
                }
            }
        }
        /// <summary>
        /// 执行一条计算查询结果语句，返回查询结果（object）。
        /// </summary>
        /// <param name="SQLString">计算查询结果语句</param>
        /// <returns>查询结果（object）</returns>
        public static object GetSingle(string SQLString, int Times = 0)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(SQLString, connection))
                {
                    try
                    {
                        connection.Open();
                        if (Times != 0) cmd.CommandTimeout = Times;
                        object obj = cmd.ExecuteScalar();
                        if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                        {
                            return null;
                        }
                        else
                        {
                            return obj;
                        }
                    }
                    catch (System.Data.SqlClient.SqlException e)
                    {
                        connection.Close();
                        throw e;
                    }
                }
            }
        }
        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="SQLString">查询语句</param>
        /// <returns>DataSet</returns>
        public static DataSet GetDataSetBySql(string SQLString, int Times = 0)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                DataSet ds = new DataSet();
                try
                {
                    connection.Open();
                    SqlDataAdapter command = new SqlDataAdapter(SQLString, connection);
                    if (Times != 0) command.SelectCommand.CommandTimeout = Times;
                    command.Fill(ds);
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    throw new Exception(ex.Message);
                }
                return ds;
            }
        }
        /// <summary>
        /// 向数据库里插入图像格式的字段(和上面情况类似的另一种实例)
        /// </summary>
        /// <param name="strSQL">SQL语句</param>
        /// <param name="fs">图像字节,数据库的字段类型为image的情况</param>
        /// <returns>影响的记录数</returns>
        public static int ExecuteSqlInsertImg(string strSQL, byte[] fs)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(strSQL, connection);
                System.Data.SqlClient.SqlParameter myParameter = new System.Data.SqlClient.SqlParameter("@fs", SqlDbType.Image);
                myParameter.Value = fs;
                cmd.Parameters.Add(myParameter);
                try
                {
                    connection.Open();
                    int rows = cmd.ExecuteNonQuery();
                    return rows;
                }
                catch (System.Data.SqlClient.SqlException e)
                {
                    throw e;
                }
                finally
                {
                    cmd.Dispose();
                    connection.Close();
                }
            }
        }

        #endregion

        #region 执行带参数的SQL语句

        /// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <returns>影响的记录数</returns>
        public static int ExecuteSql(string SQLString, params SqlParameter[] cmdParms)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
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
                        //loghelper.WriteLog("执行SQL出错：" + SQLString + "；错误信息：" + e.Message);
                        throw e;
                    }
                }
            }
        }
        /// <summary>
        /// 批量执行SQL语句
        /// </summary>
        /// <param name="sqls">sql语句</param>
        /// <param name="m_param">参数化</param>
        /// <returns></returns>
        public static int BatchExecuteBySql(object[] sqls, object[] param)
        {
            int num = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlTransaction trans = connection.BeginTransaction())
                    {
                        SqlCommand cmd = new SqlCommand();
                        try
                        {
                            for (int i = 0; i < sqls.Length; i++)
                            {
                                StringBuilder builder = (StringBuilder)sqls[i];
                                if (builder != null)
                                {
                                    SqlParameter[] paramArray = (SqlParameter[])param[i];
                                    PrepareCommand(cmd, connection, trans, builder.ToString(), paramArray);
                                    int val = cmd.ExecuteNonQuery();
                                    cmd.Parameters.Clear();
                                }
                            }
                            trans.Commit();
                            num = 1;
                        }
                        catch
                        {
                            num = -1;
                            trans.Rollback();
                            throw;
                        }
                        finally
                        {
                            connection.Close();
                            connection.Dispose();
                            trans.Dispose();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
            }
            return num;
        }
        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">SQL语句的哈希表（key为sql语句，value是该语句的SqlParameter[]）</param>
        public static int ExecuteSqlTran(List<CommandInfo> cmdList)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    SqlCommand cmd = new SqlCommand();
                    try
                    {
                        int count = 0;
                        //循环
                        foreach (CommandInfo myDE in cmdList)
                        {
                            string cmdText = myDE.CommandText;
                            SqlParameter[] cmdParms = (SqlParameter[])myDE.Parameters;
                            PrepareCommand(cmd, conn, trans, cmdText, cmdParms);

                            if (myDE.EffentNextType == EffentNextType.WhenHaveContine || myDE.EffentNextType == EffentNextType.WhenNoHaveContine)
                            {
                                if (myDE.CommandText.ToLower().IndexOf("count(") == -1)
                                {
                                    trans.Rollback();
                                    return 0;
                                }

                                object obj = cmd.ExecuteScalar();
                                bool isHave = false;
                                if (obj == null && obj == DBNull.Value)
                                {
                                    isHave = false;
                                }
                                isHave = Convert.ToInt32(obj) > 0;

                                if (myDE.EffentNextType == EffentNextType.WhenHaveContine && !isHave)
                                {
                                    trans.Rollback();
                                    return 0;
                                }
                                if (myDE.EffentNextType == EffentNextType.WhenNoHaveContine && isHave)
                                {
                                    trans.Rollback();
                                    return 0;
                                }
                                continue;
                            }
                            int val = cmd.ExecuteNonQuery();
                            count += val;
                            if (myDE.EffentNextType == EffentNextType.ExcuteEffectRows && val == 0)
                            {
                                trans.Rollback();
                                return 0;
                            }
                            cmd.Parameters.Clear();
                        }
                        trans.Commit();
                        return count;
                    }
                    catch
                    {
                        trans.Rollback();
                        throw;
                    }
                }
            }
        }
        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">SQL语句的哈希表（key为sql语句，value是该语句的SqlParameter[]）</param>
        public static void ExecuteSqlTran(Hashtable SQLStringList)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    SqlCommand cmd = new SqlCommand();
                    try
                    {
                        foreach (DictionaryEntry myDE in SQLStringList)
                        {
                            string cmdText = myDE.Key.ToString();
                            SqlParameter[] cmdParms = (SqlParameter[])myDE.Value;
                            PrepareCommand(cmd, conn, trans, cmdText, cmdParms);
                            int val = cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();
                        }
                        trans.Commit();
                    }
                    catch
                    {
                        trans.Rollback();
                        throw;
                    }
                }
            }
        }
        /// <summary>
        /// 执行一条计算查询结果语句，返回查询结果（object）。
        /// </summary>
        /// <param name="SQLString">计算查询结果语句</param>
        /// <returns>查询结果（object）</returns>
        public static object GetSingle(string SQLString, params SqlParameter[] cmdParms)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
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
                    catch (System.Data.SqlClient.SqlException e)
                    {
                        throw e;
                    }
                }
            }
        }
        /// <summary>
        /// 执行查询语句，返回SqlDataReader ( 注意：调用该方法后，一定要对SqlDataReader进行Close )
        /// </summary>
        /// <param name="strSQL">查询语句</param>
        /// <returns>SqlDataReader</returns>
        public static SqlDataReader ExecuteReader(string SQLString, params SqlParameter[] cmdParms)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();
            try
            {
                PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                SqlDataReader myReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return myReader;
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                throw e;
            }
            finally
            {
                cmd.Dispose();
                connection.Close();
            }
        }
        /// <summary>
        /// 执行查询语句，返回DataTable
        /// </summary>
        /// <param name="SQLString">查询语句</param>
        /// <returns>DataTable</returns>
        public static DataTable GetDataTableBySQL(StringBuilder SQLString, params SqlParameter[] cmdParms)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                PrepareCommand(cmd, connection, null, SQLString.ToString(), cmdParms);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        da.Fill(ds);
                        cmd.Parameters.Clear();
                    }
                    catch (System.Data.SqlClient.SqlException ex)
                    {
                        throw new Exception(ex.Message);
                    }
                    return ds.Tables[0];
                }
            }
        }
        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="SQLString">查询语句</param>
        /// <returns>DataSet</returns>
        public static DataSet GetDataSetBySql(string SQLString, params SqlParameter[] cmdParms)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        da.Fill(ds, "ds");
                        cmd.Parameters.Clear();
                    }
                    catch (System.Data.SqlClient.SqlException ex)
                    {
                        throw new Exception(ex.Message);
                    }
                    return ds;
                }
            }
        }
        private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, string cmdText, SqlParameter[] cmdParms)
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
                foreach (SqlParameter parameter in cmdParms)
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

        #region 执行存储过程操作
        /// <summary>
        /// 执行存储过程，返回一个整数		
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <param name="rowsAffected">影响的行数</param>
        /// <returns></returns>
        public static int RunProcedure(string storedProcName, IDataParameter[] parameters)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = BuildIntCommand(connection, storedProcName, parameters);
                int rowsAffected = command.ExecuteNonQuery();
                return (int)command.Parameters["ReturnValue"].Value;
            }
        }
        /// <summary>
        /// 执行存储过程,返回一个整数和消息
        /// </summary>
        /// <param name="storedProcName"></param>
        /// <param name="msg"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static int RunProcedure(string storedProcName, IDataParameter[] parameters, out string msg)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = BuildIntCommand(connection, storedProcName, parameters);
                    int rowsAffected = command.ExecuteNonQuery();
                    msg = command.Parameters["@Msg"].Value.ToString();
                    return (int)command.Parameters["ReturnValue"].Value;
                }
            }
            catch (Exception e)
            {
                msg = e.ToString();
                return -1;
            }

        }
        /// <summary>
        /// 执行存储过程,返回一个整数和消息 支持超时
        /// </summary>
        /// <param name="storedProcName"></param>
        /// <param name="parameters"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static int RunProceduretimelong(string storedProcName, IDataParameter[] parameters, out string msg)
        {
            try
            {
                connectionString = connectionString.Replace("Connection Timeout=30", "Connection Timeout=600");
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = BuildIntCommand(connection, storedProcName, parameters, 600);
                    int rowsAffected = command.ExecuteNonQuery();
                    msg = command.Parameters["@Msg"].Value.ToString();
                    return (int)command.Parameters["ReturnValue"].Value;
                }
            }
            catch (Exception e)
            {
                msg = e.ToString();
                return -1;
            }

        }
        /// <summary>
        /// 执行存储过程，返回SqlDataReader 
        ///( 注意：调用该方法后，一定要对SqlDataReader进行Close )
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns>SqlDataReader</returns>
        public static SqlDataReader GetDataReaderByProc(string storedProcName, IDataParameter[] parameters)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = BuildQueryCommand(connection, storedProcName, parameters);
                SqlDataReader returnReader = command.ExecuteReader(CommandBehavior.CloseConnection);
                return returnReader;
            }
        }
        /// <summary>
        /// 执行存储过程返回DataSet
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>    
        public static DataSet GetDataSetByProc(string storedProcName, IDataParameter[] parameters, int Times = 0)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                DataSet dataSet = new DataSet();
                connection.Open();
                SqlDataAdapter sqlDA = new SqlDataAdapter();
                sqlDA.SelectCommand = BuildQueryCommand(connection, storedProcName, parameters);
                if (Times != 0) sqlDA.SelectCommand.CommandTimeout = Times;
                sqlDA.Fill(dataSet);
                connection.Close();
                return dataSet;
            }
        }
        /// <summary>
        /// 执行存储过程返回DataSet
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>    
        /// <param name="msg">存储过程参数 返回消息</param>    
        /// <param name="ReturnValue">返回整数是否报错</param>  
        /// <param name="Times">存储过程参数等待响应秒数</param>    
        public static DataSet GetDataSetByProc(string storedProcName, IDataParameter[] parameters, out string msg, out int ReturnValue, int Times = 0)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                DataSet dataSet = new DataSet();
                connection.Open();
                SqlDataAdapter sqlDA = new SqlDataAdapter();
                sqlDA.SelectCommand = BuildQueryCommand(connection, storedProcName, parameters);
                if (Times != 0) sqlDA.SelectCommand.CommandTimeout = Times;
                sqlDA.Fill(dataSet);
                msg = sqlDA.SelectCommand.Parameters["@Msg"].Value.ToString();
                ReturnValue = (int)sqlDA.SelectCommand.Parameters["ReturnValue"].Value;
                connection.Close();
                return dataSet;
            }
        }
        /// <summary>
        /// 执行存储过程，返回DataTable
        /// </summary>
        /// <param name="storedProcName"></param>
        /// <param name="ht"></param>
        /// <returns></returns>
        public static DataTable GetDataTableByProc(string storedProcName, Hashtable ht)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                DataSet dataSet = new DataSet();
                connection.Open();
                SqlDataAdapter sqlDA = new SqlDataAdapter();
                sqlDA.SelectCommand = BuildQueryCommand(connection, storedProcName, HashTableToParameters(ht));
                sqlDA.Fill(dataSet);
                connection.Close();
                return dataSet.Tables[0];
            }
        }
        public static DataTable GetDataTableByProc(string storedProcName, IDataParameter[] parameters, int Times = 0)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                DataSet dataSet = new DataSet();
                connection.Open();
                SqlDataAdapter sqlDA = new SqlDataAdapter();
                sqlDA.SelectCommand = BuildQueryCommand(connection, storedProcName, parameters);
                if (Times != 0) sqlDA.SelectCommand.CommandTimeout = Times;
                sqlDA.Fill(dataSet);
                connection.Close();
                if (dataSet.Tables.Count <= 0)
                {
                    return null;
                }
                return dataSet.Tables[0];
            }
        }
        public static DataTable GetDataTableByProc(string storedProcName, IDataParameter[] parameters, out string msg)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                DataSet dataSet = new DataSet();
                connection.Open();
                SqlDataAdapter sqlDA = new SqlDataAdapter();
                sqlDA.SelectCommand = BuildQueryCommand(connection, storedProcName, parameters);
                sqlDA.Fill(dataSet);
                connection.Close();
                msg = sqlDA.SelectCommand.Parameters["@Msg"].Value.ToString();
                if (dataSet.Tables.Count <= 0)
                {
                    return null;
                }
                return dataSet.Tables[0];
            }
        }
        /// <summary>
        /// 构建 SqlCommand 对象实例
        /// </summary>
        /// <param name="connection">数据库连接</param>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns>SqlCommand</returns>
        private static SqlCommand BuildQueryCommand(SqlConnection connection, string storedProcName, IDataParameter[] parameters, int Times = 0)
        {
            SqlCommand command = new SqlCommand(storedProcName, connection);
            command.CommandType = CommandType.StoredProcedure;
            if (Times != 0) command.CommandTimeout = Times;
            foreach (SqlParameter parameter in parameters)
            {
                if (parameter != null)
                {
                    // 检查未分配值的输入输出参数,将其分配以DBNull.Value.
                    if ((parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Input) && (parameter.Value == null))
                    {
                        parameter.Value = DBNull.Value;
                    }
                    command.Parameters.Add(parameter);
                }
            }
            return command;
        }
        /// <summary>
        /// 创建 SqlCommand 对象实例
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns>SqlCommand 对象实例</returns>
        private static SqlCommand BuildIntCommand(SqlConnection connection, string storedProcName, IDataParameter[] parameters, int Times = 0)
        {
            SqlCommand command = BuildQueryCommand(connection, storedProcName, parameters, Times);
            command.Parameters.Add(new SqlParameter("ReturnValue", SqlDbType.Int, 4, ParameterDirection.ReturnValue, false, 0, 0, string.Empty, DataRowVersion.Default, null));
            return command;
        }
        #endregion

        #region 获取HashTable或实体

        /// <summary>
        /// 根据唯一ID获取对象,返回Hashtable
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="pkName">字段主键</param>
        /// <param name="pkVal">字段值</param>
        /// <returns>返回Hashtable</returns>
        public static Hashtable GetHashtableById(string tableName, string pkName, string pkVal)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ").Append(tableName).Append(" Where ").Append(pkName).Append("=@ID");
            DataTable dt = GetDataTableBySQL(strSql, new SqlParameter[] { new SqlParameter("@ID", pkVal) });
            return Code.DataTableHelper.DataTableToHashtable(dt);
        }
        /// <summary>
        /// 根据唯一ID获取对象,返回Hashtable
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static Hashtable GetHashtableById(StringBuilder strSql, string ID)
        {
            DataTable dt = GetDataTableBySQL(strSql, new SqlParameter[] { new SqlParameter("@ID", ID) });
            return Code.DataTableHelper.DataTableToHashtable(dt);
        }
        /// <summary>
        /// 根据唯一ID获取对象,返回实体
        /// </summary>
        /// <param name="pkName">字段主键</param>
        /// <param name="pkVal">字段值</param>
        /// <returns>返回实体类</returns>
        public static T GetModelById<T>(string pkName, string pkVal)
        {
            if (string.IsNullOrEmpty(pkVal))
            {
                return default(T);
            }
            T model = Activator.CreateInstance<T>();
            Type type = model.GetType();
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM ").Append(type.Name).Append(" Where ").Append(pkName).Append("=" + ParamKey + pkName);
            DataTable dt = DbHelperSQL.GetDataTableBySQL(sb, new SqlParameter[] { new SqlParameter(ParamKey + pkName, pkVal) });
            if (dt.Rows.Count > 0)
            {
                return Code.DataTableHelper.DataRowToModel<T>(dt.Rows[0]);
            }
            return model;
        }

        #endregion

        #region 通过HashTable或实体增删改

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="ht"></param>
        /// <returns></returns>
        public static int Insert(string tableName, Hashtable ht)
        {
            return ExecuteSql(GetInsertSql(tableName, ht).ToString(), HashTableToParameters(ht));
        }
        public static int Insert<T>(T entity)
        {
            return ExecuteSql(GetInsertSql(entity).ToString(), ModelToParameters(entity));
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="pkName">字段主键</param>
        /// <param name="pkValue"></param>
        /// <param name="ht">Hashtable</param>
        /// <returns>int</returns>
        public static int Update(string tableName, string pkName, string pkVal, Hashtable ht)
        {
            ht[pkName] = pkVal;
            return ExecuteSql(GetUpdateSql(tableName, pkName, ht).ToString(), HashTableToParameters(ht));
        }
        public static int Update(string tableName, string pkName, string pkVal, string userid, Hashtable ht)
        {
            ht[pkName] = pkVal;
            return ExecuteSql(GetUpdateSql(tableName, pkName, userid, ht).ToString(), HashTableToParameters(ht));
        }
        public static int Update<T>(T entity, string pkName)
        {
            return ExecuteSql(GetUpdateSql(entity, pkName).ToString(), ModelToParameters(entity));
        }


        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="pkName"></param>
        /// <param name="pkVal"></param>
        /// <returns></returns>
        public static int Delete(string tableName, string pkName, string pkVal)
        {
            return ExecuteSql(GetDeleteSql(tableName, pkName).ToString(), new SqlParameter[] { new SqlParameter(ParamKey + pkName, pkVal) });
        }
        public static int Delete(string tableName, Hashtable ht)
        {
            return ExecuteSql(GetDeleteSql(tableName, ht).ToString(), HashTableToParameters(ht));
        }

        #endregion

        #region 通过HashTable或实体拼接SQL

        /// <summary>
        /// 哈希表生成InsertSql语句
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="ht">Hashtable</param>
        /// <returns>StringBuilder</returns>
        public static StringBuilder GetInsertSql(string tableName, Hashtable ht)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" INSERT INTO ");
            sb.Append(tableName);
            sb.Append("(");
            StringBuilder sp = new StringBuilder();
            StringBuilder sb_prame = new StringBuilder();
            foreach (string key in ht.Keys)
            {
                sb_prame.Append("," + key);
                sp.Append("," + ParamKey + "" + key);
            }
            sb.Append(sb_prame.ToString().Substring(1, sb_prame.ToString().Length - 1) + ") VALUES (");
            sb.Append(sp.ToString().Substring(1, sp.ToString().Length - 1) + ")");
            return sb;
        }
        /// <summary>
        /// 泛型方法，反射生成InsertSql语句
        /// </summary>
        /// <param name="entity">实体类</param>
        /// </param>
        /// <returns>int</returns>
        public static StringBuilder GetInsertSql<T>(T entity)
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
                if (prop.GetValue(entity, null) != null && prop.Name.ToLower() != type.Name.ToLower() + "id")
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
        /// 哈希表生成UpdateSql语句
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="pkName">主键</param>
        /// <param name="ht">Hashtable</param>
        /// <returns></returns>
        public static StringBuilder GetUpdateSql(string tableName, string pkName, Hashtable ht)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" UPDATE ");
            sb.Append(tableName);
            sb.Append(" SET ");
            bool isFirstValue = true;
            foreach (string key in ht.Keys)
            {
                if (!key.Equals(pkName))   //主键不用更新
                {
                    if (isFirstValue)
                    {
                        isFirstValue = false;
                        sb.Append(key);
                        sb.Append("=");
                        sb.Append(ParamKey + key);
                    }
                    else
                    {
                        sb.Append("," + key);
                        sb.Append("=");
                        sb.Append(ParamKey + key);
                    }
                }
            }
            sb.Append(" WHERE ").Append(pkName).Append("=").Append(ParamKey + pkName);
            return sb;
        }
        /// <summary>
        /// 哈希表生成UpdateSql语句
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="pkName">主键</param>
        /// <param name="ht">Hashtable</param>
        /// <param name="userid">用户ID</param>
        /// <returns></returns>
        public static StringBuilder GetUpdateSql(string tableName, string pkName, string userid, Hashtable ht)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" UPDATE ");
            sb.Append(tableName);
            sb.Append(" SET ");
            bool isFirstValue = true;
            foreach (string key in ht.Keys)
            {
                if (!key.Equals(pkName))   //主键不用更新
                {
                    if (isFirstValue)
                    {
                        isFirstValue = false;
                        sb.Append(key);
                        sb.Append("=");
                        sb.Append(ParamKey + key);
                    }
                    else
                    {
                        sb.Append("," + key);
                        sb.Append("=");
                        sb.Append(ParamKey + key);
                    }
                }
            }
            sb.Append(",ModifyDate='" + DateTime.Now.ToString() + "',ModifyUserid='" + userid + "' ");
            sb.Append(" WHERE ").Append(pkName).Append("=").Append(ParamKey + pkName);
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
                    if (!prop.Name.Equals(pkName))   //主键不用更新
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
        /// <summary>
        /// 拼接删除SQL语句
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="ht">多参数</param>
        /// <returns></returns>
        public static StringBuilder GetDeleteSql(string tableName, Hashtable ht)
        {
            StringBuilder sb = new StringBuilder("DELETE FROM " + tableName + " WHERE 1=1 ");
            foreach (string key in ht.Keys)
            {
                sb.Append(" AND " + key + " = " + ParamKey + "" + key + "");
            }
            return sb;
        }

        #endregion

        #region 数据分页查询

        ///<summary>
        ///摘要:数据分页
        ///sql：传入要执行sql语句
        ///orderField：排序字段
        ///orderType：排序类型
        ///pageIndex：当前页
        ///pageSize：页大小
        ///count：返回查询条数
        ///param：参数数组
        /// </summary>
        public static DataTable GetPageList(string orderField, string orderType, int pageIndex, int pageSize, ref int count, StringBuilder sql, SqlParameter[] param = null)
        {
            StringBuilder sb = new StringBuilder();
            try
            {
                if (pageIndex == 0) pageIndex = 1;
                int num = (pageIndex - 1) * pageSize;
                int num1 = (pageIndex) * pageSize;
                sb.Append("Select * From (Select ROW_NUMBER() Over (Order By " + orderField + " " + orderType + "");
                sb.Append(") As rowNum, * From (" + sql + ") As T ) As N Where rowNum > " + num + " And rowNum <= " + num1 + "");
                count = Convert.ToInt32(GetSingle("Select Count(1) From (" + sql + ") As t", param));
                return GetDataTableBySQL(sb, param);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                return null; ;
            }
        }

        #endregion

        #region 批量数据处理

        /// <summary>
        ///大批量数据插入
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="table">数据表</param>
        /// <returns></returns>
        public static bool BulkInsert(DataTable dt)
        {
            DbHelperExpand copy = new DbHelperExpand();
            return copy.SqlServerBulkInsert(dt, connectionString);
        }

        #endregion

        #region 获取表变量
        public static DataTable GetSqltableType(string type)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "GetSqltableType";
                    cmd.Parameters.Add(new SqlParameter("@type", type));
                    cmd.Parameters.Add(new SqlParameter("ReturnValue", SqlDbType.Int, 4, ParameterDirection.ReturnValue, false, 0, 0, string.Empty, DataRowVersion.Default, null));
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    if ((int)cmd.Parameters["ReturnValue"].Value < 0)
                    {
                        return null;
                    }
                }
                return dt;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                return null; ;
            }
        }
        #endregion
    }
}