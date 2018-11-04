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
    public partial class MemberTypeInfoDal
    {
        //Retrive data without delete tag
        public List<MemberTypeInfo> GetList()
        {
            //Create sql statement
            string sql = "select * from memberTypeInfo where mIsDelete=0";

            //Execute sql and return a data table
            DataTable dt = SqliteHelper.GetDataTable(sql);

            //Iterate through the table and transfer the data to a list
            List<MemberTypeInfo> list = new List<MemberTypeInfo>();
            foreach (DataRow row in dt.Rows)
            {
                list.Add(new MemberTypeInfo()
                {
                    MId = Convert.ToInt32(row["mid"]),
                    MTitle = row["mtitle"].ToString(),
                    MDiscount = Convert.ToDecimal(row["mdiscount"])
                });
            }

            return list;     
        }

        /// <summary>
        /// Insert an item
        /// </summary>
        /// <param name="mti"></param>
        /// <returns>Number of inserted rows</returns>
        public int Insert(MemberTypeInfo mti)
        {
            string sql = "insert into MemberTypeInfo(mtitle, mdiscount, misDelete) values (@title, @discount, 0)";

            SQLiteParameter[] ps =
            {
                new SQLiteParameter("@title", mti.MTitle),
                new SQLiteParameter("@discount", mti.MDiscount)
            };

            return SqliteHelper.ExecuteNonQuery(sql, ps);
        }
        /// <summary>
        /// Update an item
        /// </summary>
        /// <param name="mti"></param>
        /// <returns>Number of updated rows</returns>
        public int Update(MemberTypeInfo mti)
        {
            string sql = "update memberTypeInfo set mtitle=@title, mdiscount=@discount where mid=@id";

            SQLiteParameter[] ps =
            {
                new SQLiteParameter("@title", mti.MTitle),
                new SQLiteParameter("@discount",mti.MDiscount),
                new SQLiteParameter("@id",mti.MId)
            };
            return SqliteHelper.ExecuteNonQuery(sql, ps);
        }
        /// <summary>
        /// Tag a deleted item by set the mIdDelete value to 1
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Number of rows which is tag as deleted</returns>
        public int Delete(int id)
        {
            string sql = "update memberTypeInfo set mIsDelete=1 where mid=@id";

            SQLiteParameter p = new SQLiteParameter("@id", id);
            return SqliteHelper.ExecuteNonQuery(sql, p);
        }
    }
}
