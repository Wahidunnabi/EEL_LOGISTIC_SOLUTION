using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LOGISTIC.DAL;

namespace LOGISTIC.BLL
{
   public class CommodityBLL
    {
        CommodityDAL objDal = new CommodityDAL();
        public List<Commodity> Getall()
        {
            List<Commodity> objlist = new List<Commodity>();            
            objlist = objDal.Getall();
            return objlist;
        }

        public Commodity GetCommodityByID(int comID)
        {
            Commodity commodity = new Commodity();
            commodity = objDal.GetCommodityDetailsByID(comID);
            return commodity;
        }

        public int Insert(Commodity objcom)
        {
            var status = objDal.Insert(objcom);
            return status;
        }

        public Commodity Update(int id, Commodity objcom)
        {
            objDal.Update(id, objcom);
            return objcom;
        }


        public void Delete(int comID)
        {

            objDal.Delete(comID);

        }
    }
}
