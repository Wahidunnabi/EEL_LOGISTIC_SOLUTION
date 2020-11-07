using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LOGISTIC.DAL;

namespace LOGISTIC.BLL
{
    public class ServiceCategoryBLL
    {
        private ServiceCategoryDAL objDal = new ServiceCategoryDAL();
     
        public List<ChartOfServiceCategory> Getall()
       {
           List<ChartOfServiceCategory> objlist = new List<ChartOfServiceCategory>();         
           objlist = objDal.Getall();
           return objlist;
       }

        public ChartOfServiceCategory GetServiceCategoryById(int categoryId)
        {
            ChartOfServiceCategory obj = new ChartOfServiceCategory();
            obj = objDal.GetServiceCategoryById(categoryId);
            return obj;
        }

        public object Insert(ChartOfServiceCategory objCategory)
       {

           var status = objDal.Insert(objCategory);
           return status;
       }

        public object Update(ChartOfServiceCategory objCategory)
       {

           var status = objDal.Update(objCategory);
           return status;

       }

        public object Delete(int categoryId)
       {

            var status = objDal.Delete(categoryId);
            return status;

        }
    }
}
