using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SQLite;

namespace CaterDal
{
    public static class SqliteHelper
    {
        //Read the connection string from configuration file.
        private static string connStr = ConfigurationManager.ConnectionStrings["itcastCater"].ConnectionString;

        public static int ExecuteNonQuery(string sql, params SQLiteParameter[] ps)
        {
            //Creating a connection object.
            using (SQLiteConnection conn=new SQLiteConnection(connStr))
            {
                SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddRange(ps);
                conn.Open();
                return cmd.ExecuteNonQuery();
            }

            
        }

    }
}
