using CaterCommon;
using CaterModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaterDal
{
    public partial class ManagerInfoDal
    {
        public List<ManagerInfo> GetList()
        {
            string sql = "select * from ManagerInfo";
            DataTable dt = SqliteHelper.GetDataTable(sql);
            List<ManagerInfo> list = new List<ManagerInfo>();
            foreach (DataRow row in dt.Rows)
            {
                list.Add(new ManagerInfo()
                {
                    MId = Convert.ToInt32(row["mid"]),
                    MName = row["mname"].ToString(),
                    MPwd = row["mpwd"].ToString(),
                    MType = Convert.ToInt32(row["mtype"])
                });
            }
            return list;
        }


        /// <summary>
        /// Insert an item
        /// </summary>
        /// <param name="mi">Object of ManagerInfo</param>
        /// <returns></returns>
        public int Insert(ManagerInfo mi)
        {
            string sql = "insert into ManagerInfo(mname,mpwd,mtype) values(@name,@pwd,@type)";
            SQLiteParameter[] ps = 
            {
                new SQLiteParameter("@name", mi.MName),
                new SQLiteParameter("@pwd", Md5Helper.EncryptString(mi.MPwd)),//Encapsulate with md5
                new SQLiteParameter("@type", mi.MType)
            };
            return SqliteHelper.ExecuteNonQuery(sql, ps);
        }

        /// <summary>
        /// Update an item, password should be dealt with specifically.
        /// </summary>
        /// <param name="mi"></param>
        /// <returns></returns>
        public int Update(ManagerInfo mi)
        {
            
            List<SQLiteParameter> listPs = new List<SQLiteParameter>();
            string sql = "update ManagerInfo set mname=@name";
            listPs.Add(new SQLiteParameter("@name", mi.MName));
            
            if (!mi.MPwd.Equals("This is original password"))
            {
                sql += ",mpwd=@pwd";
                listPs.Add(new SQLiteParameter("@pwd", Md5Helper.EncryptString(mi.MPwd)));
            }
            
            sql += ",mtype=@type where mid=@id";
            listPs.Add(new SQLiteParameter("@type", mi.MType));
            listPs.Add(new SQLiteParameter("@id", mi.MId));

            
            return SqliteHelper.ExecuteNonQuery(sql, listPs.ToArray());
        }

        /// <summary>
        /// Delete an item by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int Delete(int id)
        {
            
            string sql = "delete from ManagerInfo where mid=@id";
            
            SQLiteParameter p = new SQLiteParameter("@id", id);
            
            return SqliteHelper.ExecuteNonQuery(sql, p);
        }
        /// <summary>
        /// Get an user object by a name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ManagerInfo GetByName(string name)
        {
            ManagerInfo mi = null;
            //Construct the sql query statement
            string sql = "select * from managerInfo where mname=@name";
            //Set the parameter for the query
            SQLiteParameter p = new SQLiteParameter("@name", name);
            //Execute the query
            DataTable dt = SqliteHelper.GetDataTable(sql, p);

            if (dt.Rows.Count > 0)
            {
                mi = new ManagerInfo()
                {

                    MId = Convert.ToInt32(dt.Rows[0][0]),
                    MName = name,
                    MPwd = dt.Rows[0][2].ToString(),
                    MType = Convert.ToInt32(dt.Rows[0][3])
                };
            }
            else
            {
                Console.WriteLine("The user does not exist");
            }

            return mi;
        }
    }
}
