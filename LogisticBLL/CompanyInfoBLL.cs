using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LOGISTIC.DAL;

namespace LOGISTIC.BLL
{
    public class CompanyInfoBLL
    {
        private CompanyInfoDAL objDal = new CompanyInfoDAL();
     
       public List<CompanyInfo> Getall()
       {
           List<CompanyInfo> objlist = new List<CompanyInfo>();
           objlist = objDal.Getall();
           return objlist;
       }


        public CompanyInfo GetCompanyInfoById(int Id)
        {
            var objCompany = objDal.GetCompanyInfoById(Id);
            return objCompany;
        }     

       public object Insert(CompanyInfo objCompany)
       {
           var status = objDal.Insert(objCompany);
           return status;
       }
       public object Update(CompanyInfo objCompany)
       {
            var status = objDal.Update(objCompany);
            return status;

       }
       public object Delete(int Id)
       {

            var status = objDal.Delete(Id);
            return status;

        }
    }
}
