using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LOGISTIC.DAL;

namespace LOGISTIC.BLL
{
   public class UnitofMeasureBll
    {

       UnitofMeasureDal objDal = new UnitofMeasureDal();

       public List<UnintOfMeasure> Getall()
       {
           List<UnintOfMeasure> objlist = new List<UnintOfMeasure>();          
           objlist = objDal.Getall();
           return objlist;
       }

       public UnintOfMeasure GetUnitDetailsByID( int unitId)
       {
           UnintOfMeasure objUnit = new UnintOfMeasure();
           objUnit = objDal.GetUnitDetailsbyId(unitId);
           return objUnit;
       }

       public int Insert(UnintOfMeasure objdepot)
       {
           var status = objDal.Insert(objdepot);
           return status;
       }

       public int Update( UnintOfMeasure objUnintOfMeasure)
       {

           var status = objDal.Update(objUnintOfMeasure);
           return status;
       }
       public void Delete(int unitId)
       {

           objDal.Delete(unitId);
          
       }
    }
}
