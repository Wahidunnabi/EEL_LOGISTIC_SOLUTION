using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LOGISTIC.DAL;

namespace LOGISTIC.BLL
{
    public class ClearAndForwaderBll
    {

        ClearAndForwaderDal objDal = new ClearAndForwaderDal();
        public List<ClearAndForwadingAgent> Getall()
       {
           List<ClearAndForwadingAgent> objlist = new List<ClearAndForwadingAgent>();          
           objlist = objDal.Getall();
           return objlist;
       }

        public ClearAndForwadingAgent GetCAFByID( int cafID)
        {
            ClearAndForwadingAgent objCAF = new ClearAndForwadingAgent();           
            objCAF = objDal.GetCAFByID(cafID);
            return objCAF;
        }

        public object Insert(ClearAndForwadingAgent objCF)
       {          
           var status = objDal.Insert(objCF);
           return status;
       }

       public object Update(ClearAndForwadingAgent objCF)
       {
            var status = objDal.Update(objCF);
           return status;
       }


       public object Delete(int cafID)
       {
            var status = objDal.Delete(cafID);
            return status;

        }

    }
}
