using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LOGISTIC.DAL;

namespace LOGISTIC.BLL
{
   public class VesselBll
    {
       VasselDal objDal = new VasselDal();

       public List<Vessel> Getall()
       {
           List<Vessel> objlist = new List<Vessel>();
           objlist = objDal.Getall();
           return objlist;
       }

       public Vessel GetVesselByID(int vessId)
       {

           var obj = objDal.GetVesselByID(vessId);
           return obj;

       }

       public int Insert(Vessel objVessel)
       {
           if (objVessel != null)
           {
               var status = objDal.Insert(objVessel);
               return status;
           }
           return 0;

       }
       public int Update(Vessel objdepot)
       {

           var status = objDal.Update(objdepot);
           return status;

       }
       public int Delete(int vessId)
       {

           var status = objDal.Delete(vessId);
           return status;

       }
    }
}
