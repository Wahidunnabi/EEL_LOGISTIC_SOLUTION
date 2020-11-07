using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;
using System.Data;


namespace LOGISTIC.DAL
{
 public class ContainerTypeDal
    {
     //private int success;
     public List<ContainerType> Getall()
     {
         using (var context = new Logisticentities( ))
         {

             //var Data = from depot in context.Depots.Include("UserInfo").Include("")
             var Data = context.ContainerTypes.ToList();
             return Data;
         }
     }

     public ContainerType GetContenTypeById( int contypeId)
     {
         using (var context = new Logisticentities( ))
         {

            
             var Data = context.ContainerTypes.Where(ct=>ct.ContainerTypeId==contypeId).SingleOrDefault();
             return Data;
         }
     }

     public int Insert(ContainerType objContainerType)
     {
        

         if (objContainerType == null) return 0;

         using (var context = new Logisticentities( ))
         {
             
             try
             {
                 context.ContainerTypes.Add(objContainerType);
                 context.SaveChanges();
                 return 1;
             }
             catch (Exception exception)
             {
              
                 throw exception.InnerException;
             }
         }
     }

     public int Update(ContainerType objContainerType)
     {
         try
         {
             using (var context = new Logisticentities( ))
             {
                    var obj = context.ContainerTypes.Where(x => x.ContainerTypeId == objContainerType.ContainerTypeId).SingleOrDefault();
                    context.Entry(obj).CurrentValues.SetValues(objContainerType);
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

     public void Delete(int ContainerTypeId)
     {
         
         using (var context = new Logisticentities( ))
         {
             try
             {
                 
                 context.ContainerTypes.Remove(context.ContainerTypes.Single(x => x.ContainerTypeId == ContainerTypeId));
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
