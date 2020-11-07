using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LOGISTIC.DAL;

namespace LOGISTIC.BLL
{
    public class ChargeSetupBLL
    {
        ChargeSetupDAL objDal = new ChargeSetupDAL();
   
        public List<ChartOfService> GetallParent()
       {
           List<ChartOfService> objlist = new List<ChartOfService>();           
           objlist = objDal.GetallParent();
           return objlist;
       }

        public List<ChartOfService> GetallChildByParentId(int id)
        {
            List<ChartOfService> objlist = new List<ChartOfService>();
            objlist = objDal.GetallChildByParentId(id);
            return objlist;
        }

        public string GetClientNameById(int id)
        {          
            string name = objDal.GetClientNameById(id);
            return name;
        }

        public List<ClientBillSetup> GetAllBillSetupByClientId(int id)
        {
            List<ClientBillSetup> objlist = new List<ClientBillSetup>();
            objlist = objDal.GetAllBillSetupByClientId(id);
            return objlist;
        }


        // public List<ChartOfService> GetallParent()
        // {
        //     List<ChartOfService> objlist = new List<ChartOfService>();
        //     objlist = objDal.GetallParent();
        //     return objlist;
        // }

        // public ChartOfService GetServiceDetailsById(int serviceId)
        // {

        //     var status = objDal.GetServiceDetailsById(serviceId);
        //     return status;

        // }

        //public List<ChartOfServiceCategory> GetAllServiceCategory()
        //{
        //    List<ChartOfServiceCategory> objlist = new List<ChartOfServiceCategory>();          
        //    objlist = objDal.GetAllServiceCategory();
        //    return objlist;
        //}

        public object Insert(ClientBillSetup objBillSetup)
        {

            var status = objDal.Insert(objBillSetup);
            return status;
        }

        public object Update(ClientBillSetup objBillSetup)
        {

            var status = objDal.Update(objBillSetup);
            return status;
        }

        public object Delete(int Id)
        {

            var status = objDal.Delete(Id);
            return status;

        }

    }
}
