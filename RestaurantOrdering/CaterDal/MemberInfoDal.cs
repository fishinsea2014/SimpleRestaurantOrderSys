﻿using CaterModel;
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

        public int Insert (MemberInfo mi)
        {
            string sql = "insert into memberinfo(mtypeid, mname, mphone, mmoney, misDelete) values(@tid, @name, @phone, @money,0)";

            SQLiteParameter[] ps =
            {
                new SQLiteParameter("@tid", mi.MTypeId),
                new SQLiteParameter("@name", mi.MName),
                new SQLiteParameter("@phone", mi.MPhone),
                new SQLiteParameter("@money", mi.MMoney)
            };

            return SqliteHelper.ExecuteNonQuery(sql, ps);
        }

        public int Update(MemberInfo mi)
        {
            string sql = "update memberinfo set mname=@name,mphone=@phone,mmoney=@money,mtypeid=@tid where mid=@id";
            
            SQLiteParameter[] ps =
            {
                new SQLiteParameter("@name",mi.MName),
                new SQLiteParameter("@phone",mi.MPhone),
                new SQLiteParameter("@money",mi.MMoney),
                new SQLiteParameter("@tid",mi.MTypeId),
                new SQLiteParameter("@id",mi.MId)
            };
            
            return SqliteHelper.ExecuteNonQuery(sql, ps);
        }

        public int Delete(int id)
        {
            string sql = "update memberinfo set mIsDelete=1 where mid=@id";
            SQLiteParameter p = new SQLiteParameter("@id", id);
            return SqliteHelper.ExecuteNonQuery(sql, p);
        }
    }
}
