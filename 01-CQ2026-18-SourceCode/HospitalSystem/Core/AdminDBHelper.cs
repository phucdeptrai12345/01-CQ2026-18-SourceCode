using System.Data;
using Oracle.ManagedDataAccess.Client;
using HospitalSystem.DAL;

namespace HospitalSystem.Core;

public static class AdminDBHelper
{
    private static OracleConnection OpenConnection()
    {
        var conn = OracleHelper.GetConnection();
        if (conn.State != System.Data.ConnectionState.Open)
            conn.Open();
        return conn;
    }

    public static int ExecuteNonQuery(string sql)
    {
        var conn = OpenConnection();
        try
        {
            using var cmd = new OracleCommand(sql, conn);
            return cmd.ExecuteNonQuery();
        }
        catch (Exception ex) { throw new Exception("Lỗi Admin: " + ex.Message); }
    }

    public static DataTable GetDataTable(string sql)
    {
        var conn = OpenConnection();
        var dt = new DataTable();
        try
        {
            using var cmd = new OracleCommand(sql, conn);
            using var adapter = new OracleDataAdapter(cmd);
            adapter.Fill(dt);
        }
        catch (Exception ex) { throw new Exception("Lỗi Admin: " + ex.Message); }
        return dt;
    }
}
