using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;
using System.Data;


namespace LOGISTIC.DAL
{
 public class ShipperDAL
    {
    
     public List<Shipper> Getall()
     {
         using (var context = new Logisticentities( ))
         {

             var Data = context.Shippers.OrderBy(s=>s.ShipperName).ToList();
             return Data;
         }
     }

     public Shipper GetShipperById( int shpprId)
     {
         using (var context = new Logisticentities( ))
         {

             var Data = context.Shippers.Where(c=>c.ShipperId== shpprId).SingleOrDefault();
             return Data;
         }
     }

     public int Insert(Shipper objShipper)
     {
        
         if (objShipper == null) return 0;

         using (var context = new Logisticentities( ))
         {
             
             try
             {
                 context.Shippers.Add(objShipper);
                 context.SaveChanges();
                 return 1;
             }
             catch (Exception exception)
             {
                
                 throw exception.InnerException;
             }
         }
     }

     public int Update(Shipper objShipper)
     {
         try
         {
             using (var context = new Logisticentities( ))
             {
                    var obj = context.Shippers.Where(x => x.ShipperId == objShipper.ShipperId).SingleOrDefault();
                    context.Entry(obj).CurrentValues.SetValues(objShipper);
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

     public void Delete(int shperId)
     {
        
         using (var context = new Logisticentities( ))
         {
             try
             {               
                 context.Shippers.Remove(context.Shippers.Single(x => x.ShipperId == shperId));
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
