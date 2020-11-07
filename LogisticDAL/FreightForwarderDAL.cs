using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;
using System.Data;
using System.Data.Entity.Validation;

namespace LOGISTIC.DAL
{
    public class FreightForwarderDAL
    {    
     public List<FreightForwarderAgent> Getall()
     {
         using (var context = new Logisticentities( ))
         {         
             var Data = context.FreightForwarderAgents.OrderBy(x=>x.FreightForwarderName).ToList();
             return Data;
         }
     }

     public FreightForwarderAgent GetFreightForwarderByID(int id)
     {
         using (var context = new Logisticentities( ))
         {
             var Data = context.FreightForwarderAgents.Where(obj => obj.FreightForwarderId == id).SingleOrDefault();
             return Data;
         }
     }

     public object Insert(FreightForwarderAgent objFF)
     {
                
         using (var context = new Logisticentities())
         {
                try
                {
                    context.FreightForwarderAgents.Add(objFF);
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

     public object Update(FreightForwarderAgent objFF)
     {
            try
            {
                using (var context = new Logisticentities())
                {
                    var obj = context.FreightForwarderAgents.Where(x => x.FreightForwarderId == objFF.FreightForwarderId).SingleOrDefault();
                    context.Entry(obj).CurrentValues.SetValues(objFF);
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

     public object Delete(int id)
     {       
         using (var context = new Logisticentities( ))
         {
                try
                {
                    context.FreightForwarderAgents.Remove(context.FreightForwarderAgents.Single(x => x.FreightForwarderId == id));
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
