using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;
using System.Data;
using System.Data.Entity.Validation;

namespace LOGISTIC.DAL
{
 public class BankDAL
    {
   
     public List<Bank> Getall()
     {
         using (var context = new Logisticentities( ))
         {

                var Data = context.Banks.OrderBy(x => x.BankName).ToList();
                return Data;
         }
     }
     public object Insert(Bank objBank)
     {
              
         using (var context = new Logisticentities( ))
         {
                try
                {
                    context.Banks.Add(objBank);
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
     public object Update(Bank objBank)
     {
            try
            {
                using (var context = new Logisticentities())
                {
                    var obj = context.Banks.Where(x => x.BankId == objBank.BankId).SingleOrDefault();
                    context.Entry(obj).CurrentValues.SetValues(objBank);
                    context.SaveChanges();
                    return "Data has been updated successfully.";
                }
            }           
            catch (DbEntityValidationException ex)
            {
                string errorMessages = string.Join("; ", ex.EntityValidationErrors.SelectMany(x => x.ValidationErrors).Select(x => x.PropertyName + " : " + x.ErrorMessage));
                return errorMessages;
            }

        }
     public object Delete(Bank objBank)
     {
         
         using (var context = new Logisticentities( ))
         {
                try
                {

                    context.Banks.Remove(context.Banks.Single(x => x.BankId == objBank.BankId));
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
