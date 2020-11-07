using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Entity.Validation;

namespace LOGISTIC.DAL
{
    public class AgentDAL
    {
     
     public List<Agent> Getall()
     {
         using (var context = new Logisticentities( ))
         {

             var Data = context.Agents.ToList();
             return Data;
         }
     }

     public Agent GetAgentById(int agntId)
     {
         using (var context = new Logisticentities( ))
         {

             var Data = context.Agents.Where(t=>t.AgentId== agntId).SingleOrDefault();
             return Data;
         }
     }

     public object Insert(Agent objAgnt)
     {
               
         using (var context = new Logisticentities( ))
         {

                try
                {
                    context.Agents.Add(objAgnt);
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

     public object Update(Agent objAgnt)
     {
            try
            {
                using (var context = new Logisticentities())
                {
                    var obj = context.Agents.Where(x => x.AgentId == objAgnt.AgentId).SingleOrDefault();
                    context.Entry(obj).CurrentValues.SetValues(objAgnt);
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

     public object Delete(int agntId)
     {        
         using (var context = new Logisticentities( ))
         {
                try
                {
                    context.Agents.Remove(context.Agents.Single(x => x.AgentId == agntId));
                    context.SaveChanges();
                    return "Data has been deleted successfully.";

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
