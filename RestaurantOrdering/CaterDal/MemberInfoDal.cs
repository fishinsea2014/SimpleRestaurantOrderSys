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
    public partial class MemberInfoDal
    {
        /// <summary>
        /// Get the list of VIP member
        /// </summary>
        /// <param name="dic">Query string, in the form of pairs, like "name":"***" </param>
        /// <returns></returns>
        public List<MemberInfo> GetList(Dictionary<string, string> dic)
        {
            string sql = "select mi.*, mti.mTitle as MTypeTitle "
                       + "from MemberInfo as mi "
                       + "inner join MemberTypeInfo as mti "
                       + "on mi.mTypeId=mti.mid "
                       + "where mi.mIsDelete=0";

            List<SQLiteParameter> listP = new List<SQLiteParameter>();

            //Splicing query condition with query string in dic
            if (dic.Count > 0)
            {
                foreach (var pair in dic)
                {
                    sql += " and " + pair.Key + " like @" + pair.Key;
                    listP.Add(new SQLiteParameter("@" + pair.Key, "%" + pair.Value + "%"));
                }
            }

            //Execute the sql and get the result set.
            DataTable dt = SqliteHelper.GetDataTable(sql, listP.ToArray());

            List<MemberInfo> list = new List<MemberInfo>();
            foreach (DataRow row in dt.Rows)
            {
                list.Add(new MemberInfo()
                {
                    MId = Convert.ToInt32(row["mid"]),
                    MName = row["mname"].ToString(),
                    MPhone = row["mphone"].ToString(),
                    MMoney = Convert.ToDecimal(row["mmoney"]),
                    MTypeId = Convert.ToInt32(row["MTypeId"]),
                    MTypeTitle = row["MTypeTitle"].ToString()
                });
            }

            return list;

        }
    }
}
