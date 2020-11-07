using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LOGISTIC.DAL;

namespace LOGISTIC.BLL
{
    public class ISOMappingBLL
    {
        ISOMappingDAL objDal = new ISOMappingDAL();
     
        public List<ISOMapping> Getall()
       {
            List<ISOMapping> objlist = new List<ISOMapping>();
            objlist = objDal.Getall();
            return objlist;
       }
     
        public ISOMapping GetISOdetailsById(int Id)
        {
            ISOMapping objISO = new ISOMapping();
            objISO = objDal.GetISOdetailsById(Id);
            return objISO;
        }

        public object Insert(ISOMapping objISOmapping)
       {

           var status = objDal.Insert(objISOmapping);
           return status;
       }

        public object Update(ISOMapping objISOmapping)
       {

           var status = objDal.Update(objISOmapping);
           return status;

       }

        public object Delete(int Id)
       {

            var status = objDal.Delete(Id);
            return status;
        }
    }
}
