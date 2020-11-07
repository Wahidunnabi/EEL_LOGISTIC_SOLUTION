using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;


namespace LOGISTIC.DAL
{
    public class TrailerDal
    {
     
     public List<Trailer> Getall()
     {
         using (var context = new Logisticentities( ))
         {

             var Data = context.Trailers.ToList();
             return Data;
         }
     }

     public Trailer GetTrailerById(int traId)
     {
         using (var context = new Logisticentities( ))
         {

             var Data = context.Trailers.Where(t=>t.TrailerId==traId).SingleOrDefault();
             return Data;
         }
     }

     public int Insert(Trailer objTraile)
     {
         
         if (objTraile == null) return 0;

         using (var context = new Logisticentities( ))
         {
             
             try
             {
                 context.Trailers.Add(objTraile);
                 context.SaveChanges();
                 return 1;
             }
             catch (Exception exception)
             {
                
                 throw exception.InnerException;
             }

         }
     }

     public int Update(Trailer objTrailer)
     {
         try
         {
             using (var context = new Logisticentities( ))
             {
                    var obj = context.Trailers.Where(x => x.TrailerId == objTrailer.TrailerId).SingleOrDefault();
                    context.Entry(obj).CurrentValues.SetValues(objTrailer);
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

     public void Delete(int trailerId)
     {
         

         using (var context = new Logisticentities( ))
         {
             try
             {               
                 context.Trailers.Remove(context.Trailers.Single(x => x.TrailerId == trailerId));
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
