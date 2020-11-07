using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;
using System.Data;
using System.Data.Entity.Validation;

namespace LOGISTIC.DAL
{
    public class ClearAndForwaderDal
    {
     

     public List<ClearAndForwadingAgent> Getall()
     {
         using (var context = new Logisticentities())
         {

             //var Data = from depot in context.Depots.Include("UserInfo").Include("")
             var Data = context.ClearAndForwadingAgents.OrderBy(s=>s.CFAgentName).ToList();
             return Data;
         }
     }

     public ClearAndForwadingAgent GetCAFByID(int cafID)
     {
         using (var context = new Logisticentities())
         {

             var Data = context.ClearAndForwadingAgents.Where(obj => obj.ClearAndForwadingAgentId == cafID).SingleOrDefault();
             return Data;
         }
     }

     public object Insert(ClearAndForwadingAgent objdpot)
     {
                
         using (var context = new Logisticentities())
         {

                try
                {
                    context.ClearAndForwadingAgents.Add(objdpot);
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

     public object Update(ClearAndForwadingAgent objCFAgent)
     {
            try
            {
                using (var context = new Logisticentities())
                {
                    var obj = context.ClearAndForwadingAgents.Where(x => x.ClearAndForwadingAgentId == objCFAgent.ClearAndForwadingAgentId).SingleOrDefault();
                    context.Entry(obj).CurrentValues.SetValues(objCFAgent);
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
        
         using (var context = new Logisticentities())
         {
                try
                {

                    context.ClearAndForwadingAgents.Remove(context.ClearAndForwadingAgents.Single(x => x.ClearAndForwadingAgentId == id));
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
