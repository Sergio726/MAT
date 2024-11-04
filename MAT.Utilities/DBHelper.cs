using MAT.Entities;
using MAT.Enums.SharedModels;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
//using System.Data.EntityClient;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Web;


namespace MAT.Utilities
{
    public static class DBHelper
    {
        public static DataSet ExecuteDataSet(string sqlSpName, SqlParameter[] dbParams)
        {
            string sValue = "";
            DataSet ds = new DataSet();
            string sParams = "";
            bool bLogSpSpeed = false;
            if (ConfigurationManager.AppSettings.Get("LogSpSpeed").ToString() == "YES")
            {
                bLogSpSpeed = true;
                LogFollowSpName(sqlSpName, "-----------------------------------------------------------------------------------------------", "");
            }
            bool bRunUsingEntityFramework = true;
            if (ConfigurationManager.AppSettings.Get("UseEntityFramework").ToString() == "NO")
                bRunUsingEntityFramework = false;

            if (bRunUsingEntityFramework)
            {
                //using (EDMContainer db = new EDMContainer())
                //{
                //    // In V1 of the EF, the context connection is always an EntityConnection
                //    EntityConnection entityConnection = (EntityConnection)db.Connection;

                //    // The EntityConnection exposes the underlying store connection
                //    DbConnection storeConnection = entityConnection.StoreConnection;
                //    DbCommand command = storeConnection.CreateCommand();
                //    command.CommandText = sqlSpName;
                //    command.CommandType = CommandType.StoredProcedure;
                //    command.CommandTimeout = SpTimeout(sqlSpName);
                //    if (dbParams != null)
                //    {
                //        foreach (SqlParameter dbParam in dbParams)
                //        {
                //            if (dbParam.Value != null)
                //                sValue = dbParam.Value.ToString();
                //            else
                //                sValue = "";
                //            command.Parameters.Add(new SqlParameter(dbParam.ParameterName.Replace("@", ""), sValue));
                //            sParams += dbParam.ParameterName + " = " + sValue + ",";
                //        }
                //    }
                //    try
                //    {
                //        db.Connection.Open();
                //        if (bLogSpSpeed)
                //            LogFollowSpName(sqlSpName, "EntityFramework ExecuteReader Starts ", "");
                //        // Run the sproc 
                //        IDataReader reader = command.ExecuteReader();
                //        if (bLogSpSpeed)
                //            LogFollowSpName(sqlSpName, "EntityFramework ExecuteReader Ends ", sqlSpName + " " + sParams);

                //        string tableName = "t";
                //        int i = 0;
                //        // Move to second result set and read Posts
                //        while (!reader.IsClosed)
                //        {
                //            ds.Load(reader, LoadOption.PreserveChanges, tableName + i.ToString());
                //            i++;
                //        }
                //        if (bLogSpSpeed)
                //            LogFollowSpName(sqlSpName, "EntityFramework - ds.Load", "");

                //        reader.Close();
                //    }
                //    finally
                //    {
                //        db.Connection.Close();
                //    }
                //}
            }
            else
            {
                SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["MAT.Data.ConnectionString"].ToString());
                SqlCommand cmd = new SqlCommand(sqlSpName, cn);
                cmd.CommandTimeout = SpTimeout(sqlSpName);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                if (dbParams != null)
                {
                    foreach (SqlParameter dbParam in dbParams)
                    {
                        if (dbParam.Value != null)
                            sValue = dbParam.Value.ToString();
                        else
                            sValue = "";
                        da.SelectCommand.Parameters.Add(dbParam);
                        sParams += dbParam.ParameterName + " = " + sValue + ",";
                    }
                }
                if (bLogSpSpeed)
                    LogFollowSpName(sqlSpName, "SqlDataAdapter Starts ", "");
                da.Fill(ds);
                if (bLogSpSpeed)
                    LogFollowSpName(sqlSpName, "SqlDataAdapter Ends ", sqlSpName + " " + sParams);
            }
            return ds;
        }

        public static Int16 SpTimeout(string sqlSpName)
        {
            Int16 iTimeOut = 0;

            switch (sqlSpName)
            {
                case "":
                    iTimeOut = 600;
                    break;
                default:
                    iTimeOut = Convert.ToInt16(ConfigurationManager.AppSettings.Get("connectionCommandTimeout"));
                    break;
            }
            return iTimeOut;
        }

        public static void LogFollowSpName(string sqlSpName, string message, string message2)
        {
            if (sqlSpName == ConfigurationManager.AppSettings.Get("LogSpName").ToString())
            {
                string urlTxtFile = ConfigurationManager.AppSettings.Get("SystemPath") + "Log\\" + "FollowSPName_log.txt";
                StreamWriter SW;
                if (System.IO.File.Exists(urlTxtFile))
                    SW = File.AppendText(urlTxtFile);
                else
                    SW = File.CreateText(urlTxtFile);
                DateTime a = new DateTime();
                a = DateTime.Now;
                SW.WriteLine(message + ": " + a.TimeOfDay.ToString());
                if (message2 != "") SW.WriteLine(message2);
                SW.Close();
            }
        }

        public static bool ExecuteXml(string sqlSpName, SqlParameter[] dbParams, System.Xml.XmlDocument dXml)
        {
            SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["MAT.Data.ConnectionString"].ToString());
            SqlCommand cmd = new SqlCommand(sqlSpName, cn);
            cmd.CommandTimeout = Convert.ToInt16(ConfigurationManager.AppSettings.Get("connectionCommandTimeout"));
            cmd.CommandType = CommandType.StoredProcedure;

            if (dbParams != null)
            {
                foreach (SqlParameter dbParam in dbParams)
                {
                    cmd.Parameters.Add(dbParam);
                }
            }
            cn.Open();
            bool bReturn;
            try
            {
                //dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (dr.Read())
                    {
                        System.Data.SqlTypes.SqlXml oXml = dr.GetSqlXml(dr.GetOrdinal("Xml"));
                        dXml.LoadXml(oXml.Value);
                        bReturn = true;
                    }
                    else
                    {
                        bReturn = false;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return bReturn;
        }
        public static SqlDataReader ExecuteDataReader(string sqlSpName, SqlParameter[] dbParams)
        {
            SqlDataReader dr;

            SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["MAT.Data.ConnectionString"].ToString());
            SqlCommand cmd = new SqlCommand(sqlSpName, cn);
            cmd.CommandTimeout = Convert.ToInt16(ConfigurationManager.AppSettings.Get("connectionCommandTimeout"));
            cmd.CommandType = CommandType.StoredProcedure;

            if (dbParams != null)
            {
                foreach (SqlParameter dbParam in dbParams)
                {
                    cmd.Parameters.Add(dbParam);
                }
            }
            cn.Open();

            try
            {
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception)
            {
                throw;
            }
            return dr;
        }

        public static void ExecuteNonQuery(string sqlSpName, SqlParameter[] dbParams)
        {
            SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["MAT.Data.ConnectionString"].ToString());
            SqlCommand cmd = new SqlCommand(sqlSpName, cn);
            cmd.CommandTimeout = Convert.ToInt16(ConfigurationManager.AppSettings.Get("connectionCommandTimeout"));
            cmd.CommandType = CommandType.StoredProcedure;

            if (dbParams != null)
            {
                foreach (SqlParameter dbParam in dbParams)
                {
                    cmd.Parameters.Add(dbParam);
                }
            }

            cn.Open();

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (null != cn)
                    cn.Close();

            }
        }

        public static object ExecuteScalar(string sqlSpName, SqlParameter[] dbParams)
        {
            object retVal = null;
            SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["MAT.Data.ConnectionString"].ToString());
            SqlCommand cmd = new SqlCommand(sqlSpName, cn);
            cmd.CommandTimeout = Convert.ToInt16(ConfigurationManager.AppSettings.Get("connectionCommandTimeout"));
            cmd.CommandType = CommandType.StoredProcedure;

            if (dbParams != null)
            {
                foreach (SqlParameter dbParam in dbParams)
                {
                    cmd.Parameters.Add(dbParam);
                }
            }

            cn.Open();

            try
            {
                retVal = cmd.ExecuteScalar();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (null != cn)
                    cn.Close();
            }

            return retVal;
        }

        public static SqlParameter MakeParam(string paramName, SqlDbType dbType, int size, object objValue)
        {
            SqlParameter param;

            if (size > 0)
                param = new SqlParameter(paramName, dbType, size);
            else
                param = new SqlParameter(paramName, dbType);

            param.Value = objValue;

            return param;
        }

        public static SqlParameter MakeTableParam(string sParamName, TableDataType TableType, string sListIds)
        {
            List<SqlDataRecord> MyList = new List<SqlDataRecord>();
            string sParamType = "";


            switch (TableType)
            {
                case TableDataType.tvp_int_unique:

                    sParamType = "tvp_int_unique";
                    SqlMetaData[] MyList_tblType = { new SqlMetaData("Value", SqlDbType.Int, false, true, SortOrder.Unspecified, -1) };

                    if (sListIds != "")
                    {
                        var distinctIds = (from w in sListIds.Split(',')
                                           select w).Distinct().ToList();

                        foreach (string sId in distinctIds)
                        {
                            if (sId != "")
                            {
                                SqlDataRecord Row = new SqlDataRecord(MyList_tblType);
                                Row.SetInt32(0, Convert.ToInt32(sId));
                                MyList.Add(Row);
                            }
                        }
                    }
                    break;

                case TableDataType.tvp_int:

                    sParamType = "tvp_int";
                    SqlMetaData[] MyList_tblType_int = { new SqlMetaData("Value", SqlDbType.Int, false, true, SortOrder.Unspecified, -1) };

                    if (sListIds != "")
                    {
                        var Ids = (from w in sListIds.Split(',')
                                           select w).ToList();

                        foreach (string sId in Ids)
                        {
                            if (sId != "")
                            {
                                SqlDataRecord Row = new SqlDataRecord(MyList_tblType_int);
                                Row.SetInt32(0, Convert.ToInt32(sId));
                                MyList.Add(Row);
                            }
                        }
                    }
                    break;
                case TableDataType.tvp_uniqueidentifier:

                    sParamType = "tvp_uniqueidentifier";
                    SqlMetaData[] MyList_tblType_uniqueidentifie = { new SqlMetaData("Value", SqlDbType.UniqueIdentifier, false, true, SortOrder.Unspecified, -1) };

                    if (sListIds != "")
                    {
                        var distinctIds = (from w in sListIds.Split(',')
                                           select w).Distinct().ToList();

                        foreach (string sId in distinctIds)
                        {
                            if (sId != "")
                            {
                                SqlDataRecord Row = new SqlDataRecord(MyList_tblType_uniqueidentifie);
                                Row.SetString(0, sId);
                                MyList.Add(Row);
                            }
                        }
                    }
                    break;
            }


            SqlParameter tvpListInt = new SqlParameter(sParamName, SqlDbType.Structured);
            tvpListInt.TypeName = sParamType;
            tvpListInt.Value = MyList.Count == 0 ? null : MyList;
            return tvpListInt;
        }

        public static SqlParameter MakeTableParam<T>(string sParamName, TableDataType TableType, List<T> items)
        {
            var strategy = TableStrategyFactory.GetStrategy<T>(TableType);
            var MyList = strategy.CreateRecords(items);
            var sParamType = strategy.GetParamType();

            var tvpList = new SqlParameter(sParamName, SqlDbType.Structured)
            {
                TypeName = sParamType,
                Value = MyList.Count == 0 ? null : MyList
            };

            return tvpList;
        }


        public static SqlParameter MakeParamOutput(string paramName, SqlDbType dbType, int size)
        {
            SqlParameter param;

            if (size > 0)
                param = new SqlParameter(paramName, dbType, size);
            else
                param = new SqlParameter(paramName, dbType);

            param.Direction = ParameterDirection.Output;

            return param;
        }

        public static SqlParameter MakeParamReturnValue(SqlDbType dbType, int size)
        {
            SqlParameter returnCode;

            returnCode = new SqlParameter("RETURN_VALUE", dbType);
            returnCode.Direction = ParameterDirection.ReturnValue;

            return returnCode;
        }

        public static int ExecuteNonQueryOutput(string sqlSpName, SqlParameter[] dbParams, string paramName, SqlDbType dbType, int size)
        {
            SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["MAT.Data.ConnectionString"].ToString());
            SqlCommand cmd = new SqlCommand(sqlSpName, cn);
            cmd.CommandTimeout = Convert.ToInt16(ConfigurationManager.AppSettings.Get("connectionCommandTimeout"));
            cmd.CommandType = CommandType.StoredProcedure;

            if (dbParams != null)
            {
                foreach (SqlParameter dbParam in dbParams)
                    cmd.Parameters.Add(dbParam);
            }
            SqlParameter OutParam = MakeParamOutput(paramName, dbType, size);
            cmd.Parameters.Add(OutParam);

            cn.Open();

            try
            {
                cmd.ExecuteNonQuery();

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (null != cn)
                    cn.Close();

            }
            if (OutParam.Value == null) return 0;
            else return System.Convert.ToInt16(OutParam.Value);
        }

        public static string ExecuteNonQueryOutputString(string sqlSpName, SqlParameter[] dbParams, string paramName, SqlDbType dbType, int size)
        {
            SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["MAT.Data.ConnectionString"].ToString());
            SqlCommand cmd = new SqlCommand(sqlSpName, cn);
            cmd.CommandTimeout = Convert.ToInt16(ConfigurationManager.AppSettings.Get("connectionCommandTimeout"));
            cmd.CommandType = CommandType.StoredProcedure;

            if (dbParams != null)
            {
                foreach (SqlParameter dbParam in dbParams)
                    cmd.Parameters.Add(dbParam);
            }
            SqlParameter OutParam = MakeParamOutput(paramName, dbType, size);
            cmd.Parameters.Add(OutParam);

            cn.Open();

            try
            {
                cmd.ExecuteNonQuery();

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (null != cn)
                    cn.Close();

            }
            if (OutParam.Value == null) return "";
            else return System.Convert.ToString(OutParam.Value);
        }




    }

    public enum TableDataType
    {
        tvp_uniqueidentifier = 7,
        tvp_int = 8,
        tvp_int_unique = 9,
        tvp_pasaje = 10,
        
    }

    public interface ITableStrategy<T>
    {
        List<SqlDataRecord> CreateRecords(List<T> items);
        string GetParamType();
    }
    public class TvpIntUniqueStrategy : ITableStrategy<int>
    {
        public List<SqlDataRecord> CreateRecords(List<int> items)
        {
            List<int> uniqueIds = items.Distinct().ToList();
            var MyList = new List<SqlDataRecord>();
            var MyList_tblType = new SqlMetaData[] { new SqlMetaData("Value", SqlDbType.Int) };
            foreach (var item in uniqueIds)
            {
                var Row = new SqlDataRecord(MyList_tblType);
                Row.SetInt32(0, item);
                MyList.Add(Row);
            }
            return MyList;
        }

        public string GetParamType()
        {
            return "tvp_int_unique";
        }
    }
    public class TvpUniqueIdentifierStrategy : ITableStrategy<Guid>
    {
        public List<SqlDataRecord> CreateRecords(List<Guid> items)
        {
            List<Guid> uniqueIds = items.Distinct().ToList();
            var MyList = new List<SqlDataRecord>();
            var MyList_tblType = new SqlMetaData[] { new SqlMetaData("Value", SqlDbType.UniqueIdentifier) };
            foreach (var item in uniqueIds)
            {
                var Row = new SqlDataRecord(MyList_tblType);
                Row.SetGuid(0, item);
                MyList.Add(Row);
            }
            return MyList;
        }

        public string GetParamType()
        {
            return "tvp_uniqueidentifier";
        }
    }

    public class TvpPasajeStrategy : ITableStrategy<TablePasaje>
    {
        public List<SqlDataRecord> CreateRecords(List<TablePasaje> pasajes)
        {
            
            var MyList = new List<SqlDataRecord>();
            var MyList_tblType = new SqlMetaData[] { 
                new SqlMetaData("PasajeId", SqlDbType.UniqueIdentifier),
                new SqlMetaData("PasajeroId", SqlDbType.UniqueIdentifier),
                new SqlMetaData("ButacaId", SqlDbType.UniqueIdentifier),
                new SqlMetaData("ButacaCodigo", SqlDbType.VarChar, 4),
                new SqlMetaData("ButacaPrecio", SqlDbType.Decimal,18, 2),
                new SqlMetaData("AdicionalesIds", SqlDbType.VarChar, -1),
                new SqlMetaData("HabitacionId", SqlDbType.UniqueIdentifier),
            };
            foreach (var pasaje in pasajes)
            {
                var Row = new SqlDataRecord(MyList_tblType);
                Row.SetGuid(0, pasaje.PasajeId);
                Row.SetGuid(1, pasaje.PasajeroId);
                Row.SetGuid(2, pasaje.ButacaId);
                Row.SetString(3, pasaje.ButacaCodigo);
                Row.SetDecimal(4, pasaje.ButacaPrecio);
                Row.SetString(5, string.Join(",", pasaje.AdicionalesIds));
                Row.SetGuid(6, pasaje.HabitacionId);
                MyList.Add(Row);
            }
            return MyList;
        }

        public string GetParamType()
        {
            return "tvp_pasaje";
        }
    }


    public class TableStrategyFactory
    {
        public static ITableStrategy<T> GetStrategy<T>(TableDataType tableType)
        {
            switch (tableType)
            {
                case TableDataType.tvp_int_unique:
                    return (ITableStrategy<T>)new TvpIntUniqueStrategy();
                case TableDataType.tvp_uniqueidentifier:
                    return (ITableStrategy<T>)new TvpUniqueIdentifierStrategy();
                case TableDataType.tvp_pasaje:
                    return (ITableStrategy<T>)new TvpPasajeStrategy();
                default:
                    throw new ArgumentOutOfRangeException(nameof(tableType), "Unknown table type");
            }
        }
    }
}
