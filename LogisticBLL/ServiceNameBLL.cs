using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LOGISTIC.DAL;

namespace LOGISTIC.BLL
{
    public class ServiceNameBLL
    {
        ServiceNameDAL objDal = new ServiceNameDAL();
   
        public List<ChartOfService> Getall()
       {
           List<ChartOfService> objlist = new List<ChartOfService>();           
           objlist = objDal.Getall();
           return objlist;
       }

        public List<ChartOfService> GetallParent()
        {
            List<ChartOfService> objlist = new List<ChartOfService>();
            objlist = objDal.GetallParent();
            return objlist;
        }

        public ChartOfService GetServiceDetailsById(int serviceId)
        {

            var status = objDal.GetServiceDetailsById(serviceId);
            return status;

        }
       
       public List<ChartOfServiceCategory> GetAllServiceCategory()
       {
           List<ChartOfServiceCategory> objlist = new List<ChartOfServiceCategory>();          
           objlist = objDal.GetAllServiceCategory();
           return objlist;
       }
        
        public object Insert(ChartOfService objService)
       {

           var status = objDal.Insert(objService);
           return status;
       }

        public object Update(ChartOfService objService)
       {

           var status = objDal.Update(objService);
           return status;
       }

       public object Delete(int serviceId)
       {

            var status = objDal.Delete(serviceId);
            return status;

        }

    }
}
