using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LOGISTIC.DAL;

namespace LOGISTIC.BLL
{
    public class DepotBll
    {
        DepotDal objDal = new DepotDal();
        public List<Depot> Getall()
        {
            List<Depot> objlist = new List<Depot>();
            objlist = objDal.Getall();
            return objlist;
        }
        public int Insert(Depot objdepot)
        {

            var status = objDal.Insert(objdepot);
            return status;

        }
        public int Update(Depot objdepot)
        {

            var status = objDal.Update(objdepot);
            return status;
        }
        public void Delete(Depot objdepot)
        {

            objDal.Delete(objdepot);
        }
    }
}
