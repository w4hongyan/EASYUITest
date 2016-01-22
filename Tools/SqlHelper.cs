using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools
{
   public class SqlHelper
    {
       public static string ConnectingString = ConfigurationManager.ConnectionStrings["ConnectingString"].ConnectionString;
       public static SqlConnection GetConnection(string connectionString) {
           return new SqlConnection(connectionString);
       }
       private static void AttachParameters(SqlCommand command, params SqlParameter[] commandParameters)
       {
           SqlParameter[] sqlParameters = commandParameters;
           foreach (SqlParameter sqlParameter in sqlParameters)
           {
               if ((sqlParameter.Direction == ParameterDirection.InputOutput || sqlParameter.Direction == ParameterDirection.Input) && (sqlParameter.Value == null))
               {
                   sqlParameter.Value = DBNull.Value;
               }
               command.Parameters.Add(sqlParameter);
           }
       }
       public static void DisposeSqlConnection(SqlConnection connection)
       {
           if (connection.State != ConnectionState.Closed)
               connection.Close();
           connection.Dispose();
       }
       public static SqlTransaction InitializingSqlTransaction(string connectionString)
       {
           SqlConnection connection = GetConnection(connectionString);
           if (connection.State != ConnectionState.Open)
           {
               connection.Open();
           }
           SqlTransaction transaction = connection.BeginTransaction();
           return transaction;
       }
       public static SqlTransaction InitializingSqlTransaction(SqlConnection connection)
       {
           SqlTransaction transaction = connection.BeginTransaction();
           return transaction;
       }
       private static bool AssignParameterValues(SqlParameter[] commandParameters, object[] parameterValues)
       {
           bool bolresult;
           bolresult = !(commandParameters == null) || (parameterValues == null);
           if (bolresult)
           {
               if (commandParameters.Length != parameterValues.Length)
               {
                   //在向方法提供的其中一个参数无效时引发的异常。 
                   throw new ArgumentException("Parameter count does not match Parameter Value count.");
               }
               int i = 0;
               int j = commandParameters.Length;
               while (i < j)
               {
                   commandParameters[i].Value = parameterValues[i];
                   i++;
               }
           }
           return bolresult;
       }
       private static void PrepareCommand(SqlCommand command, SqlConnection connection
          , SqlTransaction transaction, CommandType commandType, string commandText
          , params SqlParameter[] commandParameters)
       {

           if (connection.State != ConnectionState.Open)
           {
               connection.Open();
           }
           command.Connection = connection;
           command.CommandText = commandText;
           if (transaction != null)
           {
               command.Transaction = transaction;
           }
           command.CommandType = commandType;
           if (commandParameters != null)
           {
               SqlHelper.AttachParameters(command, commandParameters);
           }
       }
       private static void PrepareCommand(SqlCommand command, SqlConnection connection
           , CommandType commandType, string commandText, params SqlParameter[] commandParameters)
       {
           PrepareCommand(command, connection, null, commandType, commandText, commandParameters);
       }
       public static int ExecuteNonQuery(string connectionString, CommandType commandType, string commandText)
       {
           return ExecuteNonQuery(connectionString, commandType, commandText);
       }
       public static int ExecuteNonQuery(string connectionString, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
       {
           int i = -1;
           SqlConnection connection = new SqlConnection(connectionString);
           try
           {
               connection.Open();
               SqlCommand sqlCommand = new SqlCommand();
               sqlCommand.CommandTimeout = 1800;
               SqlHelper.PrepareCommand(sqlCommand, connection, commandType, commandText, commandParameters);
               i = sqlCommand.ExecuteNonQuery();
               connection.Close();
           }
           catch (Exception ex)
           {
               throw ex;
           }
           finally
           {
               if (connection != null)
               {
                   connection.Dispose();
               }
           }
           return i;
       }
       public static int ExecuteNonQuery(SqlConnection connection, CommandType commandType, string commandText)
       {
           return SqlHelper.ExecuteNonQuery(connection, commandType, commandText, null);
       }
       public static int ExecuteNonQuery(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
       {
           SqlCommand sqlCommand = new SqlCommand();
           sqlCommand.CommandTimeout = 1800;

           SqlHelper.PrepareCommand(sqlCommand, connection,  commandType, commandText, commandParameters);
           int j = sqlCommand.ExecuteNonQuery();
           sqlCommand.Parameters.Clear();
           return j;
       }
       public static int ExecuteNonQuery(SqlTransaction transaction, CommandType commandType, string commandText)
       {
           return SqlHelper.ExecuteNonQuery(transaction, commandType, commandText, null);
       }
       public static int ExecuteNonQuery(SqlConnection connection, SqlTransaction transaction, CommandType commandType, string commandText)
       {
           return SqlHelper.ExecuteNonQuery(connection, transaction, commandType, commandText, null);
       }
       public static int ExecuteNonQuery(SqlTransaction transaction, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
       {
           return ExecuteNonQuery(transaction.Connection, transaction, commandType, commandText, commandParameters);
       }
       public static int ExecuteNonQuery(SqlConnection connection, SqlTransaction transaction, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
       {
           SqlCommand sqlCommand = new SqlCommand();
           sqlCommand.CommandTimeout = 1800;

           SqlHelper.PrepareCommand(sqlCommand, connection, transaction, commandType, commandText, commandParameters);
           int j = sqlCommand.ExecuteNonQuery();
           sqlCommand.Parameters.Clear();
           return j;
       }
       public static DataSet ExecuteDataset(string connectionString, CommandType commandType, string commandText)
       {
           return SqlHelper.ExecuteDataset(connectionString, commandType, commandText, null);
       }
       public static DataSet ExecuteDataset(string connectionString, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
       {
           DataSet dataSet;
           SqlConnection connection = new SqlConnection(connectionString);
           try
           {
               connection.Open();
               dataSet = SqlHelper.ExecuteDataset(connection, commandType, commandText, commandParameters);
           }
           catch (Exception ex)
           {
               throw ex;
           }
           finally
           {
               if (connection != null)
               {
                   connection.Dispose();
               }
           }
           return dataSet;
       }
       public static DataSet ExecuteDataset(SqlConnection connection, CommandType commandType, string commandText)
       {
           return SqlHelper.ExecuteDataset(connection, commandType, commandText, null);
       }
       public static DataSet ExecuteDataset(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
       {

           //			SqlCommand sqlCommand = new SqlCommand ();
           //			SqlHelper.PrepareCommand (sqlCommand, connection, null, commandType, commandText, commandParameters);
           //			SqlDataAdapter sqlDataAdapter = new SqlDataAdapter (sqlCommand);
           //			DataSet dataSet1 = new DataSet ();
           //			sqlDataAdapter.Fill (dataSet1);
           //			sqlCommand.Parameters.Clear ();				
           //			return dataSet1;
           return ExecuteDataset(connection, null, commandType, commandText, commandParameters);
       }
       public static DataSet ExecuteDataset(SqlConnection connection, SqlTransaction transaction, CommandType commandType, string commandText)
       {
           return ExecuteDataset(connection, transaction, commandType, commandText, null);
       }
       public static DataSet ExecuteDataset(SqlConnection connection, SqlTransaction transaction, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
       {
           SqlCommand sqlCommand = new SqlCommand();
           sqlCommand.CommandTimeout = 1800;

           SqlHelper.PrepareCommand(sqlCommand, connection, transaction, commandType, commandText, commandParameters);
           SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
           DataSet dataSet = new DataSet();
           sqlDataAdapter.Fill(dataSet);
           sqlCommand.Parameters.Clear();
           return dataSet;
       }
       public static DataSet ExecuteDataset(SqlTransaction transaction, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
       {
           return ExecuteDataset(transaction.Connection, transaction, commandType, commandText, commandParameters);
       }
       public static DataSet ExecuteDataset(SqlTransaction transaction, CommandType commandType, string commandText)
       {
           return SqlHelper.ExecuteDataset(transaction, commandType, commandText, null);
       }
       public static DataTable ExecuteDataTable(string connectionString, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
       {

           DataTable dataTable;
           SqlConnection connection = new SqlConnection(connectionString);

           try
           {
               connection.Open();
               dataTable = SqlHelper.ExecuteDataTable(connection, commandType, commandText, commandParameters);
           }
           catch (Exception ex)
           {
               throw ex;
           }
           finally
           {
               if (connection != null)
               {
                   connection.Dispose();
               }
           }
           return dataTable;
       }
       public static DataTable ExecuteDataTable(SqlConnection connection, CommandType commandType, string commandText)
       {
           return SqlHelper.ExecuteDataTable(connection, commandType, commandText, null);
       }
       public static DataTable ExecuteDataTable(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
       {
           SqlCommand sqlCommand = new SqlCommand();
           sqlCommand.CommandTimeout = 1800;

           SqlHelper.PrepareCommand(sqlCommand, connection, null, commandType, commandText, commandParameters);
           SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
           DataTable dataTable = new DataTable();
           sqlDataAdapter.Fill(dataTable);
           sqlCommand.Parameters.Clear();
           return dataTable;
       }
       public static DataTable ExecuteDataTable(SqlTransaction transaction, CommandType commandType, string commandText)
       {
           return SqlHelper.ExecuteDataTable(transaction, commandType, commandText, null);
       }
       public static DataTable ExecuteDataTable(SqlConnection connection, SqlTransaction transaction, CommandType commandType, string commandText)
       {
           return SqlHelper.ExecuteDataTable(connection, transaction, commandType, commandText, null);
       }
       public static DataTable ExecuteDataTable(SqlTransaction transaction, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
       {
           SqlCommand sqlCommand = new SqlCommand();
           sqlCommand.CommandTimeout = 1800;

           SqlHelper.PrepareCommand(sqlCommand, transaction.Connection, transaction, commandType, commandText, commandParameters);
           SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
           DataTable dataTable = new DataTable();
           sqlDataAdapter.Fill(dataTable);
           sqlCommand.Parameters.Clear();
           return dataTable;
           //ExecuteDataTable(transaction.Connection,transaction,commandType,commandText,commandParameters);
       }
       public static DataTable ExecuteDataTable(SqlConnection connection, SqlTransaction transaction, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
       {
           SqlCommand sqlCommand = new SqlCommand();
           sqlCommand.CommandTimeout = 1800;

           SqlHelper.PrepareCommand(sqlCommand, connection, transaction, commandType, commandText, commandParameters);
           SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
           DataTable dataTable = new DataTable();
           sqlDataAdapter.Fill(dataTable);
           sqlCommand.Parameters.Clear();
           return dataTable;
       }
       public static object ExecuteScalar(string connectionString, CommandType commandType, string commandText)
       {
           return SqlHelper.ExecuteScalar(connectionString, commandType, commandText, null);
       }
       public static object ExecuteScalar(string connectionString, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
       {
           object obj;
           SqlConnection connection = new SqlConnection(connectionString);
           try
           {
               connection.Open();
               obj = SqlHelper.ExecuteScalar(connection, commandType, commandText, commandParameters);
           }
           catch (Exception ex)
           {
               throw ex;
           }
           finally
           {
               if (connection != null)
               {
                   connection.Dispose();
               }
           }
           return obj;
       }
       public static object ExecuteScalar(SqlConnection connection, CommandType commandType, string commandText)
       {
           return SqlHelper.ExecuteScalar(connection, commandType, commandText, null);
       }
       public static object ExecuteScalar(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
       {
           SqlCommand sqlCommand = new SqlCommand();
           sqlCommand.CommandTimeout = 1800;

           SqlHelper.PrepareCommand(sqlCommand, connection, null, commandType, commandText, commandParameters);
           object obj = sqlCommand.ExecuteScalar();
           sqlCommand.Parameters.Clear();
           return obj;
       }
       public static object ExecuteScalar(SqlTransaction transaction, CommandType commandType, string commandText)
       {
           return SqlHelper.ExecuteScalar(transaction, commandType, commandText, null);
       }
       public static object ExecuteScalar(SqlConnection connection, SqlTransaction transaction, CommandType commandType, string commandText)
       {
           return SqlHelper.ExecuteScalar(connection, transaction, commandType, commandText, null);
       }
       public static object ExecuteScalar(SqlTransaction transaction, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
       {
           SqlCommand sqlCommand = new SqlCommand();
           sqlCommand.CommandTimeout = 1800;

           SqlHelper.PrepareCommand(sqlCommand, transaction.Connection, transaction, commandType, commandText, commandParameters);
           object obj = sqlCommand.ExecuteScalar();
           sqlCommand.Parameters.Clear();
           return obj;
       }
       public static object ExecuteScalar(SqlConnection connection, SqlTransaction transaction, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
       {
           SqlCommand sqlCommand = new SqlCommand();
           sqlCommand.CommandTimeout = 1800;

           SqlHelper.PrepareCommand(sqlCommand, connection, transaction, commandType, commandText, commandParameters);
           object obj = sqlCommand.ExecuteScalar();
           sqlCommand.Parameters.Clear();
           return obj;
       }
       public static DataTable GetPagedDataTable(SqlConnection conn, string sql, string orderStr, int pageIndex, int pageSize, params SqlParameter[] cmdParams)
       {
           DataTable dt = new DataTable();
           StringBuilder sb = new StringBuilder();
           try
           {
               int num = (pageIndex - 1) * pageSize;
               int num1 = (pageIndex) * pageSize;
               sb.Append("Select * From (Select ROW_NUMBER() Over (Order By " + orderStr + "");
               sb.Append(") As rowNum, * From (" + sql + ") As T ) As N Where rowNum > " + num + " And rowNum <= " + num1 + "");
               dt = SqlHelper.ExecuteDataTable(conn, CommandType.Text, sb.ToString(), cmdParams);
               return dt;

           }
           catch (Exception e)
           {
               return null;

           }
       }
       public static int GetRowsCount(SqlConnection conn, string sql, params SqlParameter[] cmdParams)
       {
           StringBuilder queryStr = new StringBuilder("SELECT count(1) FROM (" + sql+")t");
           object obj = SqlHelper.ExecuteScalar(conn, CommandType.Text, queryStr.ToString(), cmdParams);
           return Convert.ToInt32(obj);
       }
       
    }
}
