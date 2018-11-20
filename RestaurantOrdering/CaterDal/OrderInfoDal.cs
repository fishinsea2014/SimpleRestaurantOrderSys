using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CaterModel;

namespace CaterDal
{
    public class OrderInfoDal
    {
        /// <summary>
        /// Insert an order
        /// Update the table status to occupied
        /// Combine the two actions into one query
        /// </summary>
        /// <param name="tableId"></param>
        /// <returns></returns>
        public int NewOrder(int tableId)
        {
             
            string sql = "insert into orderinfo(odate,ispay,tableId) values(datetime('now', 'localtime'),0,@tid);" +
                //Update the table status
                "update tableinfo set tIsFree=0 where tid=@tid;" +
                //Get the new order id;
                "select oid from orderinfo order by oid desc limit 0,1";
            SQLiteParameter p = new SQLiteParameter("@tid", tableId);
            return Convert.ToInt32(SqliteHelper.ExecuteScalar(sql, p));
        }

        public decimal gettotalmoneybyorderid(int orderid)
        {
            string sql = @"select sum(oti.count*di.dprice)
                        from orderdetailinfo as oti
                        inner join dishinfo as di
                        on oti.dishid=di.did
                        where oti.orderid=@orderid";
            SQLiteParameter p = new SQLiteParameter("@orderid", orderid);

            object obj = SqliteHelper.ExecuteScalar(sql, p);

            if (obj == DBNull.Value)
            {
                return 0;
            }
            return Convert.ToDecimal(obj);
        }

        
        public int UpdateCountByOId(int oid, int count)
        {
            string sql = "update orderDetailInfo set count=@count where oid=@oid";
            SQLiteParameter[] ps =
            {
                new SQLiteParameter("@count", count),
                new SQLiteParameter("@oid", oid)
            };
            return SqliteHelper.ExecuteNonQuery(sql, ps);
        }

        public int TakeOrder(int orderId, int dishId)
        {
            //Is the current cuisine being ordered?
            string sql = "select count(*) from orderDetailInfo where orderId=@oid and dishId=@did";
            SQLiteParameter[] ps =
            {
                new SQLiteParameter("@oid", orderId),
                new SQLiteParameter("@did", dishId)
            };
            int count = Convert.ToInt32(SqliteHelper.ExecuteScalar(sql, ps));
            if (count > 0)
            {
                //If is being ordered, plus the quantity by one
                sql = "update orderDetailInfo set count=count+1 where orderId=@oid and dishId=@did";
            }
            else
            {
                //If not being ordered so far, add the cuisine to the order
                sql = "insert into orderDetailInfo(orderid,dishId,count) values(@oid,@did,1)";
            }
            return SqliteHelper.ExecuteNonQuery(sql, ps);
        }

        public List<OrderDetailInfo> GetDetailList(int orderId)
        {
            //throw new NotImplementedException();
            string sql = @"select odi.oid,di.dTitle,di.dPrice,odi.count from dishinfo as di
            inner join orderDetailInfo as odi
            on di.did=odi.dishid
            where odi.orderId=@orderid";
            SQLiteParameter p = new SQLiteParameter("@orderid", orderId);

            DataTable dt = SqliteHelper.GetDataTable(sql, p);
            List<OrderDetailInfo> list = new List<OrderDetailInfo>();

            foreach (DataRow row in dt.Rows)
            {
                list.Add(new OrderDetailInfo()
                {
                    OId = Convert.ToInt32(row["oid"]),
                    DTitle = row["dtitle"].ToString(),
                    DPrice = Convert.ToDecimal(row["dprice"]),
                    Count = Convert.ToInt32(row["count"])
                });
            }

            return list;
        }

        public int DeleteDetailById(int oid)
        {
            string sql = "delete from orderDetailInfo where oid=@oid";
            SQLiteParameter p = new SQLiteParameter("@oid", oid);
            return SqliteHelper.ExecuteNonQuery(sql, p);
        }

        public int SetOrderMomey(int orderid, decimal money)
        {
            orderid = 27;
            money = 100;
            string sql = "update orderinfo set omoney=@money where oid=@oid";
            SQLiteParameter[] ps =
            {
                new SQLiteParameter("@money", money),
                new SQLiteParameter("@oid", orderid)
            };
            int result = SqliteHelper.ExecuteNonQuery(sql, ps);
            return result;
        }

    }
}
