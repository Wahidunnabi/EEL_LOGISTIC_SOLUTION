using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;
using System.Data;
using System.Data.Entity.Validation;

namespace LOGISTIC.DAL
{
    public class ServiceCategoryDAL
    {

     public List<ChartOfServiceCategory> Getall()
     {
         using (var context = new Logisticentities( ))
         {


             var Data = context.ChartOfServiceCategories;
             return Data.ToList();
         }
     }

     public ChartOfServiceCategory GetServiceCategoryById( int id)
     {
      
         using (var context = new Logisticentities( ))
         {

             var obj = context.ChartOfServiceCategories.Where(x=>x.CateId==id).SingleOrDefault();
             return obj;
            
         }
     }

     public object Insert(ChartOfServiceCategory objCategory)
     {
                  
         using (var context = new Logisticentities( ))
         {
                try
                {
                    context.ChartOfServiceCategories.Add(objCategory);
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

     public object Update(ChartOfServiceCategory objCategory)
     {
            try
            {
                using (var context = new Logisticentities())
                {
                    var obj = context.ChartOfServiceCategories.Where(x => x.CateId == objCategory.CateId).SingleOrDefault();
                    context.Entry(obj).CurrentValues.SetValues(objCategory);
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

     public object Delete(int categoryId)
     {
        
         using (var context = new Logisticentities( ))
         {
                try
                {
                    context.ChartOfServiceCategories.Remove(context.ChartOfServiceCategories.Single(x => x.CateId == categoryId));
                    context.SaveChanges();
                    return "Data has been deleted successfully. !!";

                }
                catch(DbEntityValidationException ex)
                {
                    string errorMessages = string.Join("; ", ex.EntityValidationErrors.SelectMany(x => x.ValidationErrors).Select(x => x.PropertyName + " : " + x.ErrorMessage));
                    return errorMessages;

                }
           
         }
     }

    }
 
}
