using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;
using System.Data.Entity;
using System.Data;
using System.Data.Entity.Validation;

namespace LOGISTIC.DAL
{
    public class ISOMappingDAL
    {

     public List<ISOMapping> Getall()
     {
         using (var context = new Logisticentities( ))
         {
                var Data = context.ISOMappings
                       .Include("ContainerSize")
                       .Include("ContainerType")
                       .OrderBy(x => x.ContainerType.ContainerTypeName).ToList();

                return Data;
         }
     }

        public ISOMapping GetISOdetailsById(int id)
     {
         using (var context = new Logisticentities( ))
         {

             var Data = context.ISOMappings.Where(x => x.ID == id).SingleOrDefault();                                   
             return Data;

         }
     }

     
     public object Insert(ISOMapping objISOmapping)
     {        
        
         using (var context = new Logisticentities( ))
         {

                try
                {
                    context.ISOMappings.Add(objISOmapping);
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

        public object Update(ISOMapping objISOmapping)
        {
            using (var context = new Logisticentities())
            {            
                try
                {

                    var obj = context.ISOMappings.Where(x => x.ID == objISOmapping.ID).SingleOrDefault();
                    context.Entry(obj).CurrentValues.SetValues(objISOmapping);
                    context.SaveChanges();
                    return "Data has been updated successfully.";
                }
                catch (DbEntityValidationException ex)
                {
                    string errorMessages = string.Join("; ", ex.EntityValidationErrors.SelectMany(x => x.ValidationErrors).Select(x => x.PropertyName + " : " + x.ErrorMessage));
                    return errorMessages;
                }
            }
        }

     public object Delete(int id)
     {
        
         using (var context = new Logisticentities( ))
         {
                try
                {
                    context.ISOMappings.Remove(context.ISOMappings.Single(x => x.ID == id));
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
