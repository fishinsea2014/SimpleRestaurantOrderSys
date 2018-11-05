using CaterDal;
using CaterModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaterBll
{
    class DishTypeInfoBll
    {
        private DishTypeInfoDal dtiDal = new DishTypeInfoDal();
        public List<DishTypeInfo> GetList()
        {
            return dtiDal.GetList();    
        }

        public bool Add(DishTypeInfo dti)
        {
            return dtiDal.Insert(dti) > 0;
        }

        public bool Edit(DishTypeInfo dti)
        {
            return dtiDal.Update(dti) > 0;
        }

        public bool Delete(int id)
        {
            return dtiDal.Delete(id) > 0;
        }
    }
}
