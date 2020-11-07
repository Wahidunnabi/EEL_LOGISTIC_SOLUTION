using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;
using System.Data.Entity;
using System.Data;


namespace LOGISTIC.DAL
{
    public class TrailerNumberDal
    {
     //private int success;

     public List<TrailerNumber> Getall()
     {
         using (var context = new Logisticentities( ))
         {
             var Data = context.TrailerNumbers.Include("Trailer").OrderBy(x=>x.TrailerNumber1).ToList();
             return Data;
         }
     }

        public List<TrailerNumber> GetAllTrailernumberBytrailerId(int trailerId)
        {
            using (var context = new Logisticentities())
            {

                var Data = context.TrailerNumbers.Where(x => x.TrailerId == trailerId).OrderBy(x => x.TrailerNumber1).ToList();
                return Data;
            }
        }

        public TrailerNumber GetTrailerNumberById(int traiNumId)
     {
         using (var context = new Logisticentities( ))
         {

             var Data = context.TrailerNumbers.Include("Trailer").Where(t => t.TrailerNumberId == traiNumId).SingleOrDefault();                                   
             return Data;

         }
     }

     public List<Trailer> GetTrailerComboload()
     {
         using (var context = new Logisticentities( ))
         {
            
             var ComboData = from trailer in context.Trailers
                             orderby trailer.TrailerName
                        select trailer;
             return ComboData.ToList();
         }
     }

     public int Insert(TrailerNumber objTrailerNumber)
     {        
         if (objTrailerNumber == null) return 0;

         using (var context = new Logisticentities( ))
         {
           
             try
             {
                 context.TrailerNumbers.Add(objTrailerNumber);
                 context.SaveChanges();
                 return 1;
             }
             catch (Exception exception)
             {
                
                 throw exception.InnerException;
             }
         }
     }

     public int Update(TrailerNumber objTrailerNumber)
     {
         try
         {
             using (var context = new Logisticentities( ))
             {
                    var obj = context.TrailerNumbers.Where(x => x.TrailerNumberId == objTrailerNumber.TrailerNumberId).SingleOrDefault();
                    context.Entry(obj).CurrentValues.SetValues(objTrailerNumber);
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

     public void Delete(int trailerNumberId)
     {
        
         using (var context = new Logisticentities( ))
         {
             try
             {               
                 context.TrailerNumbers.Remove(context.TrailerNumbers.Single(x => x.TrailerNumberId == trailerNumberId));
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
