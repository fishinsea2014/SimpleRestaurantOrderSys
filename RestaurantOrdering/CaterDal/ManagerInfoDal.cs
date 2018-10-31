using CaterModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaterDal
{
    class ManagerInfoDal
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
    }
}
