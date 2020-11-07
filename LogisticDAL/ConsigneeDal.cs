using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;
using System.Data;


namespace LOGISTIC.DAL
{
 public class ConsigneeDal
    {
    
     public List<Consignee> Getall()
     {
         using (var context = new Logisticentities( ))
         {

             var Data = context.Consignees.OrderBy(s=>s.ConsigneeName).ToList();
             return Data;
         }
     }

     public Consignee GetConsigneeById( int consId)
     {
         using (var context = new Logisticentities( ))
         {

             var Data = context.Consignees.Where(c=>c.ConsigneeId==consId).SingleOrDefault();
             return Data;
         }
     }

     public int Insert(Consignee objconsignee)
     {
        
         if (objconsignee == null) return 0;

         using (var context = new Logisticentities( ))
         {
             
             try
             {
                 context.Consignees.Add(objconsignee);
                 context.SaveChanges();
                 return 1;
             }
             catch (Exception exception)
             {
                
                 throw exception.InnerException;
             }
         }
     }

     public int Update(Consignee objconsignee)
     {
         try
         {
             using (var context = new Logisticentities( ))
             {
                    var obj = context.Consignees.Where(x => x.ConsigneeId == objconsignee.ConsigneeId).SingleOrDefault();
                    context.Entry(obj).CurrentValues.SetValues(objconsignee);
                    context.SaveChanges();
                    return 1;
             }
         }
         catch (Exception ex)
         {

             throw ex;
         }
         finally
         { 

         }
     }

     public void Delete(int ConsigneeId)
     {
        
         using (var context = new Logisticentities( ))
         {
             try
             {               
                 context.Consignees.Remove(context.Consignees.Single(x => x.ConsigneeId == ConsigneeId));
                context.SaveChanges();
                 
             }
             catch (OptimisticConcurrencyException ex)
             {
                 throw ex;
             }
                        
         }
     }

    }
 
}
