using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;
using System.Data;
using System.Data.Entity.Validation;

namespace LOGISTIC.DAL
{
    public class CustomerDal
    {

     public List<Customer> Getall()
     {
         using (var context = new Logisticentities( ))
         {
                try
                {
                    var Data = context.Customers.OrderBy(c => c.CustomerName).ToList();
                    return Data;
                }
                catch (DbEntityValidationException ex)
                {
                    string errorMessages = string.Join("; ", ex.EntityValidationErrors.SelectMany(x => x.ValidationErrors).Select(x => x.PropertyName + " : " + x.ErrorMessage));
                    throw ex;
                }

            }
     }

        public List<Customer> GetallWithAgent()
        {
            using (var context = new Logisticentities())
            {
                try
                {
                    var Data = context.Customers
                                      .Include("Agent")
                                      .OrderBy(c => c.CustomerName).ToList();
                    return Data;
                }
                catch (DbEntityValidationException ex)
                {
                    string errorMessages = string.Join("; ", ex.EntityValidationErrors.SelectMany(x => x.ValidationErrors).Select(x => x.PropertyName + " : " + x.ErrorMessage));
                    throw ex;
                }

            }
        }
     public Customer GetCustomerById(int CustId)
        {

            using (var context = new Logisticentities())
            {
                var customer = context.Customers.Include("Agent").Where(c => c.CustomerId.Equals(CustId)).SingleOrDefault();                
                return customer;
            }

        }
    
     public object Insert(Customer objCust)
     {
       
         using (var context = new Logisticentities( ))
         {
            
               try
               {
                    context.Customers.Add(objCust);
                    context.SaveChanges();
                    return "Data has been saved successfully.";

                }
                catch (DbEntityValidationException ex)
                {
                    string errorMessages = string.Join("; ", ex.EntityValidationErrors.SelectMany(x => x.ValidationErrors).Select(x => x.PropertyName + " : " + x.ErrorMessage));
                    return errorMessages;
                }


            }
     }
     public object Update(Customer objCustomer)
     {
            using (var context = new Logisticentities())
            {
                try
                {

                    var obj = context.Customers.Where(x => x.CustomerId == objCustomer.CustomerId).SingleOrDefault();
                    context.Entry(obj).CurrentValues.SetValues(objCustomer);
                    context.SaveChanges();
                    return "Data has been updated successfully.";

                }
                catch (DbEntityValidationException ex)
                {
                    string errorMessages = string.Join("; ", ex.EntityValidationErrors.SelectMany(x => x.ValidationErrors).Select(x => x.PropertyName + " : " + x.ErrorMessage));
                    return errorMessages;
                }
            }
        }
     public object Delete(int CustId)
     {
        
         using (var context = new Logisticentities( ))
         {
             try
             {

                    context.Customers.Remove(context.Customers.Single(x => x.CustomerId == CustId));
                    context.SaveChanges();
                    return "Data has been deleted successfully. !!";

                }
                catch (DbEntityValidationException ex)
                {
                    string errorMessages = string.Join("; ", ex.EntityValidationErrors.SelectMany(x => x.ValidationErrors).Select(x => x.PropertyName + " : " + x.ErrorMessage));
                    return errorMessages;
                }
            }
     }

    }
 
}
