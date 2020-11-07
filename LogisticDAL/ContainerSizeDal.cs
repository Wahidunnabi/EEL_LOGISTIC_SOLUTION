using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;
using System.Data;


namespace LOGISTIC.DAL
{
    public class ContainerSizeDal
    {

     public List<ContainerSize> Getall()
     {
         using (var context = new Logisticentities( ))
         {


             var Data = context.ContainerSizes;
             return Data.ToList();
         }
     }

     public ContainerSize GetContsizeDetailsById( int contsizeId)
     {
      
         using (var context = new Logisticentities( ))
         {

             var obj = context.ContainerSizes.Where(cs=>cs.ContainerSizeId==contsizeId).SingleOrDefault();
             return obj;
            
         }
     }

     public int Insert(ContainerSize objContainerSize)
     {
         

         if (objContainerSize == null) return 0;

         using (var context = new Logisticentities( ))
         {          
             try
             {
                 context.ContainerSizes.Add(objContainerSize);
                 context.SaveChanges();
                 return 1;
             }
             catch (Exception exception)
             {                
                 throw exception.InnerException;
             }

            
         }
     }

     public int Update( ContainerSize objContainerSize)
     {
         try
         {
             using (var context = new Logisticentities( ))
             {
                    var obj = context.ContainerSizes.Where(x => x.ContainerSizeId == objContainerSize.ContainerSizeId).SingleOrDefault();
                    context.Entry(obj).CurrentValues.SetValues(objContainerSize);
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

     public void Delete(int containerSizeId)
     {
        
         using (var context = new Logisticentities( ))
         {
             try
             {                
                 context.ContainerSizes.Remove(context.ContainerSizes.Single(x => x.ContainerSizeId == containerSizeId));
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
