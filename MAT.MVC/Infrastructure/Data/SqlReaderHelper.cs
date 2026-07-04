using System;
using System.Data.SqlClient;

namespace MAT.MVC.Infrastructure.Data
{
    /// <summary>
    /// Helpers de lectura tipada sobre SqlDataReader compartidos por las clases *DataAccess (NetTiers F7+).
    /// </summary>
    internal static class SqlReaderHelper
    {
        public static string GetString(this SqlDataReader reader, string column)
        {
            var ordinal = reader.GetOrdinal(column);
            return reader.IsDBNull(ordinal) ? null : reader.GetString(ordinal);
        }

        public static Guid GetGuid(this SqlDataReader reader, string column)
        {
            return reader.GetGuid(reader.GetOrdinal(column));
        }

        public static Guid? GetNullableGuid(this SqlDataReader reader, string column)
        {
            var ordinal = reader.GetOrdinal(column);
            return reader.IsDBNull(ordinal) ? (Guid?)null : reader.GetGuid(ordinal);
        }

        public static int GetInt(this SqlDataReader reader, string column)
        {
            return Convert.ToInt32(reader.GetValue(reader.GetOrdinal(column)));
        }

        public static int? GetNullableInt(this SqlDataReader reader, string column)
        {
            var ordinal = reader.GetOrdinal(column);
            return reader.IsDBNull(ordinal) ? (int?)null : Convert.ToInt32(reader.GetValue(ordinal));
        }

        public static bool GetBool(this SqlDataReader reader, string column)
        {
            var ordinal = reader.GetOrdinal(column);
            return !reader.IsDBNull(ordinal) && Convert.ToBoolean(reader.GetValue(ordinal));
        }

        public static DateTime? GetNullableDateTime(this SqlDataReader reader, string column)
        {
            var ordinal = reader.GetOrdinal(column);
            return reader.IsDBNull(ordinal) ? (DateTime?)null : reader.GetDateTime(ordinal);
        }
    }
}
