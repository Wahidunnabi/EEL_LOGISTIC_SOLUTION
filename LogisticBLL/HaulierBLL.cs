using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LOGISTIC.BLL
{    
    public class HaulierBLL
    {
        HaulierDAL objDal = new HaulierDAL();
        public List<Haulier> GetHalrList()
        {
            List<Haulier> objlist = new List<Haulier>();            
            objlist = objDal.GetAll();
            return objlist;
        }
        public Haulier GetHaulierById(int hlrId)
        {
            Haulier objTrlr = new Haulier();
            objTrlr = objDal.GetHaulierById(hlrId);
            return objTrlr;
        }

        public int Insert(Haulier objHaulier)
        {

            var status = objDal.Insert(objHaulier);
            return status;
        }

        public int Update(Haulier objHaulier)
        {

            var status = objDal.Update(objHaulier);
            return status;

        }

        public void Delete(int hlrId)
        {

            objDal.Delete(hlrId);

        }
    }
}
