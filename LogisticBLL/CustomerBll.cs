using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LOGISTIC.DAL;

namespace LOGISTIC.BLL
{
    public class CustomerBll
    {
        private CustomerDal objDal = new CustomerDal();
       //public List<Depot> Getall()
       //{
       //    using (var DbContext = new BackOfficeBrokerEntities(DBConnection.dbEntityConnectionString))
       //    {
       //        var Bank = from bank in DbContext.Banks
       //                   orderby bank.Name
       //                   select bank;
       //        return Bank.ToList();
       //    }
       //}

       public List<Customer> Getall()
       {
           List<Customer> objlist = new List<Customer>();
           objlist = objDal.Getall();
           return objlist;
       }

        public List<Customer> GetallWithAgent()
        {
            List<Customer> objlist = new List<Customer>();
            objlist = objDal.GetallWithAgent();
            return objlist;
        }
        public Customer GetCustomerById(int CustId)
        {
            var obj = objDal.GetCustomerById(CustId);
            return obj;
        }     

       public object Insert(Customer objCustomer)
       {
           var status = objDal.Insert(objCustomer);
           return status;
       }
       public object Update(Customer objCustomer)
       {
            var status = objDal.Update(objCustomer);
            return status;

       }
       public object Delete(int custId)
       {

            var status = objDal.Delete(custId);
            return status;

        }
    }
}
