using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CaterCommon;
using CaterDal;
using CaterModel;

namespace CaterBll
{
    public class ManagerInfoBll
    {
        ManagerInfoDal miDal = new ManagerInfoDal();

        public List<ManagerInfo> GetList()
        {
            return miDal.GetList();
        }

        public bool Add(global::CaterModel.ManagerInfo mi)
        {
            //throw new NotImplementedException();
            return miDal.Insert(mi) > 0;
        }

        public bool Edit(ManagerInfo mi)
        {
            //throw new NotImplementedException();
            return miDal.Update(mi) > 0;
        }

        public bool Remove(int id)
        {
            return miDal.Delete(id) > 0;
        }

        public LoginState Login(string name, string pwd, out int type)
        {
            type = -1;
            ManagerInfo mi = miDal.GetByName(name);
            if (mi == null)
            {
                return LoginState.NameError;
            }
            else
            {
                //Username and password is correct?
                if (mi.MPwd.Equals(Md5Helper.EncryptString(pwd)))
                {
                    type=mi.MType;
                    
                    return LoginState.Ok;
                }
                else
                {
                    return LoginState.PwdError;
                }                
            }            
        }
        
    }
}
