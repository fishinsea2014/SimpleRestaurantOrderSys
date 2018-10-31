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

        /// <summary>
        /// Execute the sql demand.
        /// </summary>
        /// <param name="sql">SQL string</param>
        /// <param name="ps">A variable parameter</param>
        /// <returns></returns>
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

        //Get the value of first raw and first column
        public static object ExecuteScalar(string sql, params SQLiteParameter[] ps)
        {
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddRange(ps);

                conn.Open();
                return cmd.ExecuteScalar();
            }
        }

        //Get the query result
        public static DataTable GetDataTable(string sql, params SQLiteParameter[] ps)
        {
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(sql, conn);
                DataTable dt = new DataTable();
                adapter.SelectCommand.Parameters.AddRange(ps);
                adapter.Fill(dt);
                return dt;
            }
        }
    }
}
