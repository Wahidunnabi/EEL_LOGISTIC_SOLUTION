using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LOGISTIC.DAL;

namespace LOGISTIC.BLL
{
   public class ConsigneeBll
    {

       ConsigneeDal objDal = new ConsigneeDal();

       public List<Consignee> Getall()
       {
           List<Consignee> objlist = new List<Consignee>();         
           objlist = objDal.Getall();
           return objlist;
       }

       public Consignee GetConsigneeById( int consId)
       {
        
           Consignee objCons = new Consignee();
           objCons = objDal.GetConsigneeById(consId);
           return objCons;
       }

       public int Insert(Consignee objCons)
       {

           var status = objDal.Insert(objCons);
           return status;
       }

       public int Update( Consignee objCons)
       {

           var status = objDal.Update(objCons);
           return status;
       }

       public void Delete(int consId)
       {

           objDal.Delete(consId);
           
       }
    }
}
