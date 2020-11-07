using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;
using System.Data;
using System.Data.Entity.Validation;

namespace LOGISTIC.DAL
{
 public class LocationDAL
    {
   
     public List<Location> Getall()
     {
         using (var context = new Logisticentities( ))
         {

                var Data = context.Locations.OrderBy(x => x.LocationName).ToList();
                return Data;
         }
     }
     public object Insert(Location objLocation)
     {
              
         using (var context = new Logisticentities( ))
         {
                try
                {
                    context.Locations.Add(objLocation);
                    context.SaveChanges();
                    return "Data has been saved successfully.";
                }                
                catch (DbEntityValidationException ex)
                {
                    string errorMessages = string.Join("; ", ex.EntityValidationErrors.SelectMany(x => x.ValidationErrors).Select(x => x.PropertyName + " : " + x.ErrorMessage));
                    return errorMessages;
                }

            }
     }
     public object Update(Location objLocation)
     {
            try
            {
                using (var context = new Logisticentities())
                {
                    var obj = context.Locations.Where(x => x.LocationId == objLocation.LocationId).SingleOrDefault();
                    context.Entry(obj).CurrentValues.SetValues(objLocation);
                    context.SaveChanges();
                    return "Data has been updated successfully.";
                }
            }           
            catch (DbEntityValidationException ex)
            {
                string errorMessages = string.Join("; ", ex.EntityValidationErrors.SelectMany(x => x.ValidationErrors).Select(x => x.PropertyName + " : " + x.ErrorMessage));
                return errorMessages;
            }

        }
     public object Delete(Location objLocation)
     {
         
         using (var context = new Logisticentities( ))
         {
                try
                {

                    context.Locations.Remove(context.Locations.Single(x => x.LocationId == objLocation.LocationId));
                    context.SaveChanges();
                    return "Data has been deleted successfully. !!";

                }              
                catch (DbEntityValidationException ex)
                {
                    string errorMessages = string.Join("; ", ex.EntityValidationErrors.SelectMany(x => x.ValidationErrors).Select(x => x.PropertyName + " : " + x.ErrorMessage));
                    return errorMessages;
                }

            }
     }

    }
 
}
