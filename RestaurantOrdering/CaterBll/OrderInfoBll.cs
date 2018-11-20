using CaterDal;
using CaterModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaterBll
{
    public partial class OrderInfoBll
    {
        private OrderInfoDal oiDal = new OrderInfoDal();

        public  int NewOrder(int tableId)
        {
            return oiDal.NewOrder(tableId);
        }

        public List<OrderDetailInfo> GetDetailList(int orderId)
        {
            return oiDal.GetDetailList(orderId);
        }
        public bool TakeOrder(int orderId, int dishId)
        {
            return oiDal.TakeOrder(orderId, dishId) > 0;
        }


        //public int GetOrderIdByTableId(int tableId)
        //{
        //    return oiDal.GetOrderIdByTableId(tableId);
        //}



        public bool UpdateCountByOid(int oid, int count)
        {
            return oiDal.UpdateCountByOId(oid, count) > 0;
        }


        public decimal GetTotalMoneyByOrderId(int orderid)
        {
            return oiDal.gettotalmoneybyorderid(orderid);
        }

        public bool SetOrderMoney(int orderid, decimal money)
        {
            return oiDal.SetOrderMomey(orderid, money) > 0;
        }

        public bool DeleteDetailById(int oid)
        {
            return oiDal.DeleteDetailById(oid) > 0;
        }

        //public bool Pay(bool isUseMoney, int memberId, decimal payMoney, int orderid, decimal discount)
        //{
        //    return oiDal.Pay(isUseMoney, memberId, payMoney, orderid, discount) > 0;
        //}

    }
}
