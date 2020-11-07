using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;


namespace LOGISTIC.DAL
{
    public class ContainerWightDal
    {
     
     public List<ContainerGrossWeight> Getall()
     {
         using (var context = new Logisticentities( ))
         {

                var Data = context.ContainerGrossWeights.OrderBy(x => x.GrossWeight).ToList();
                return Data;
         }
     }
     public int  Insert(ContainerGrossWeight objContWidth)
     {      

         using (var context = new Logisticentities( ))
         {            
             try
             {
                    context.ContainerGrossWeights.Add(objContWidth);
                    context.SaveChanges();
                    return 1;
              }
             catch (Exception exception)
             {                
                 throw exception.InnerException;
             }             
         }
     }
     public int Update(ContainerGrossWeight objContWidth)
     {
         try
         {
             using (var context = new Logisticentities( ))
             {
                    var obj = context.ContainerGrossWeights.Where(x => x.ContGrossWeightId == objContWidth.ContGrossWeightId).SingleOrDefault();
                    context.Entry(obj).CurrentValues.SetValues(objContWidth);
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
     public void Delete(ContainerGrossWeight objContainerWeight)
     {        
         using (var context = new Logisticentities( ))
         {
             try
             {
                
                 context.ContainerGrossWeights.Remove(context.ContainerGrossWeights.Single(x => x.ContGrossWeightId == objContainerWeight.ContGrossWeightId));
                 context.SaveChanges();
                 
             }
            catch (OptimisticConcurrencyException ex)
             {
                 throw  ex;
             }
            
         }
     }

    }
 
}
