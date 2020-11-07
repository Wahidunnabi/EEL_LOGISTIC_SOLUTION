using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LOGISTIC.DAL;

namespace LOGISTIC.BLL
{
   public class ShipperBLL
    {

       ShipperDAL objDal = new ShipperDAL();

       public List<Shipper> Getall()
       {
           List<Shipper> objlist = new List<Shipper>();         
           objlist = objDal.Getall();
           return objlist;
       }

       public Shipper GetShipperById( int shprId)
       {

            Shipper objShper = new Shipper();
            objShper = objDal.GetShipperById(shprId);
            return objShper;
       }

       public int Insert(Shipper objShper)
       {

           var status = objDal.Insert(objShper);
           return status;
       }

       public int Update(Shipper objShper)
       {

           var status = objDal.Update(objShper);
           return status;
       }

       public void Delete(int shperId)
       {

           objDal.Delete(shperId);
           
       }
    }
}
