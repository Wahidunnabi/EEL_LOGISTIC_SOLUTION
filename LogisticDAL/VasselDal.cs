using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;
using System.Data;
using LOGISTIC;


namespace LOGISTIC.DAL
{
    public class VasselDal
    {
 
     public List<Vessel> Getall()
     {
         using (var context = new Logisticentities( ))
         {

             var Data = context.Vessels.OrderBy(X=>X.VesselName).ToList();
             return Data;
         }
     }

     public Vessel GetVesselByID( int vessId)
     {
         using (var context = new Logisticentities( ))
         {

             var Data = context.Vessels.Where(v=>v.VesselId==vessId).SingleOrDefault();
             return Data;
         }
     }


     public int Insert(Vessel objVessel)
     {
        
         if (objVessel == null) return 0;

         using (var context = new Logisticentities( ))
         {
           
             try
             {
                 context.Vessels.Add(objVessel);
                 context.SaveChanges();
                 return 1;
             }
             catch (Exception exception)
             {
               
                 throw exception.InnerException;
             }            
         }
     }

     public int  Update(Vessel objVessel)
     {
         try
         {
             using (var context = new Logisticentities( ))
             {
                    var obj = context.Vessels.Where(x => x.VesselId == objVessel.VesselId).SingleOrDefault();
                    context.Entry(obj).CurrentValues.SetValues(objVessel);
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

     public int Delete(int VasselId)
     {
       
         using (var context = new Logisticentities( ))
         {
             try
             {

                 context.Vessels.Remove(context.Vessels.Single(v => v.VesselId == VasselId));
                 context.SaveChanges();
                 return 1;

             }
             catch (OptimisticConcurrencyException ex)
             {
                 throw ex;
             }
         }
     }

    }
 
}
