using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Entity.Validation;

namespace LOGISTIC.DAL
{
    public class ServiceNameDAL
    {
    

     public List<ChartOfService> Getall()
     {
         using (var context = new Logisticentities( ))
         {
             var objService = context.ChartOfServices.Include("ChartOfServiceCategory").OrderBy(x=>x.ServiceName).ToList();
             return objService;
         }
     }

        public List<ChartOfService> GetallParent()
        {
            using (var context = new Logisticentities())
            {
                var objService = context.ChartOfServices.Include("ChartOfServiceCategory").Where(x=>x.IsTransaction==false).OrderBy(x => x.ServiceName).ToList();
                return objService;
            }
        }

        public ChartOfService GetServiceDetailsById(int serviceId)
     {
         using (var context = new Logisticentities( ))
         {

             var Data = context.ChartOfServices.Include("ChartOfServiceCategory").Where(t => t.ServiceId == serviceId).SingleOrDefault();                                   
             return Data;

         }
     }

     public List<ChartOfServiceCategory> GetAllServiceCategory()
     {
         using (var context = new Logisticentities( ))
         {

                var categoryList = context.ChartOfServiceCategories.OrderBy(x => x.CategoryName).ToList();
                return categoryList;

         }
     }

     public object Insert(ChartOfService objService)
     {        
         
         using (var context = new Logisticentities( ))
         {
           
             try
             {
                 context.ChartOfServices.Add(objService);
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

     public object Update(ChartOfService objService)
     {
            using (var context = new Logisticentities())
            {
                try
                {
                    var obj = context.ChartOfServices.Where(x => x.ServiceId == objService.ServiceId).SingleOrDefault();
                    context.Entry(obj).CurrentValues.SetValues(objService);
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

     public object Delete(int serviceId)
     {
        
         using (var context = new Logisticentities( ))
         {
                try {

                    context.ChartOfServices.Remove(context.ChartOfServices.Single(x => x.ServiceId == serviceId));
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
