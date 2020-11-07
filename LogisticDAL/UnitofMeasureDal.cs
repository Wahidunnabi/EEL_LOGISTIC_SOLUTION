using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;
using System.Data;
using LOGISTIC;


namespace LOGISTIC.DAL
{
    public class UnitofMeasureDal
    {
     //private int success;
     public List<UnintOfMeasure> Getall()
     {
         using (var context = new Logisticentities( ))
         {

             var Data = context.UnintOfMeasures.OrderBy(X=>X.UnitOfMeasureName).ToList();
             return Data;
         }
     }

     public UnintOfMeasure GetUnitDetailsbyId(int unitId)
     {
         using (var context = new Logisticentities( ))
         {

             var Data = context.UnintOfMeasures.Where(u => u.UnitOfMeasureId == unitId).SingleOrDefault();
             return Data;
         }
     }

     public int Insert(UnintOfMeasure objUnintOfMeasure)
     {
        
         if (objUnintOfMeasure == null) return 0;

         using (var context = new Logisticentities( ))
         {
            
             try
             {
                 context.UnintOfMeasures.Add(objUnintOfMeasure);
                 context.SaveChanges();
                 return 1;
             }
             catch (Exception exception)
             {
               
                 throw exception.InnerException;
             }
         }
     }

     public int Update( UnintOfMeasure objUnintOfMeasure)
     {
         try
         {
             using (var context = new Logisticentities( ))
             {
                    var obj = context.UnintOfMeasures.Where(x => x.UnitOfMeasureId == objUnintOfMeasure.UnitOfMeasureId).SingleOrDefault();

                    context.Entry(obj).CurrentValues.SetValues(objUnintOfMeasure);
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
     public void Delete(int unitOfMeasureId)
     {
        
         using (var context = new Logisticentities( ))
         {
             try
             {
               
                 context.UnintOfMeasures.Remove(context.UnintOfMeasures.Single(x => x.UnitOfMeasureId == unitOfMeasureId));
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
