using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;
using System.Data;


namespace LOGISTIC.DAL
{
 public class DepotDal
    {
    // private int success;
     public List<Depot> Getall()
     {
         using (var context = new Logisticentities( ))
         {

                var Data = context.Depots.OrderBy(x => x.DepotName).ToList();
                return Data;
         }
     }
     public int Insert(Depot objdpot)
     {
              
         using (var context = new Logisticentities( ))
         {
             try
             {
                    context.Depots.Add(objdpot);
                    context.SaveChanges();
                    return 1;
             }
             catch (Exception exception)
             {
                
                 throw exception.InnerException;
             }
             
         }
     }
     public int Update(Depot objdpot)
     {
         try
         {
             using (var context = new Logisticentities( ))
             {
                    var obj = context.Depots.Where(x => x.DepotId == objdpot.DepotId).SingleOrDefault();
                    context.Entry(obj).CurrentValues.SetValues(objdpot);
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
     public void Delete(Depot objdpot)
     {
         
         using (var context = new Logisticentities( ))
         {
             try
             {
                 
                 context.Depots.Remove(context.Depots.Single(x => x.DepotId == objdpot.DepotId));
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
