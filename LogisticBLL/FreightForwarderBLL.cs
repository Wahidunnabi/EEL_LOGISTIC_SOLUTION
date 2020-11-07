using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LOGISTIC.DAL;

namespace LOGISTIC.BLL
{
    public class FreightForwarderBLL
    {
        FreightForwarderDAL objDal = new FreightForwarderDAL();
        public List<FreightForwarderAgent> Getall()
       {
           List<FreightForwarderAgent> objlist = new List<FreightForwarderAgent>();         
           objlist = objDal.Getall();
           return objlist;
       }

        public FreightForwarderAgent GetFreightForwarderByID( int id)
        {
            FreightForwarderAgent objFF = new FreightForwarderAgent();            
            objFF = objDal.GetFreightForwarderByID(id);
            return objFF;
        }

        public object Insert(FreightForwarderAgent objff)
       {
            var status = objDal.Insert(objff);
            return status;
       }

       public object Update(FreightForwarderAgent objff)
       {
            var status = objDal.Update(objff);
            return status;
       }


       public object Delete(int ffId)
       {
            var status = objDal.Delete(ffId);
            return status;
        }

    }
}
