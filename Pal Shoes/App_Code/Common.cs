using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using SqlAccess;
using System.Text;

/// <summary>
/// Summary description for Common
/// </summary>
public static class Common
{
    public static Int64 GenerateSUK(string col, string table)
    {
        int count = 0;
        while (true)
        {
            count = count + 1;
            string query = "select isnull(max(" + col + "), 0) from " + table + "";
            object intReturnValue = MySqlDataAccess.ExecuteScalar(MySqlDataAccess.ConnectionString, CommandType.Text, query);
            string values = Convert.ToString(intReturnValue);
            values = values.Replace(@"NZ", "");
            Int64 intvalue = 0;
            if (Convert.ToInt64(values) == 0)
            {
                intvalue = 1;
            }
            else
            {
                intvalue = Convert.ToInt64(values) + 1;
            }
            return Convert.ToInt64(intvalue);
        }
    }


    public static long GenUserIDno(string col, string table)
    {
        int count = 0;
        while (true)
        {
            count = count + 1;
            string query = "select isnull(max(" + col + "), 0) from " + table + "";
            object intReturnValue = MySqlDataAccess.ExecuteScalar(MySqlDataAccess.ConnectionString, CommandType.Text, query);
            if (Convert.ToInt64(intReturnValue) == 0)
            {
                intReturnValue = 1000001;
            }
            else
            {
                intReturnValue = Convert.ToInt64(intReturnValue) + 1;
            }
            return Convert.ToInt64(intReturnValue);
        }
    }

}