using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;
using System.Data;
using System.Data.Entity.Validation;

namespace LOGISTIC.DAL
{
    public class CompanyInfoDAL
    {

     public List<CompanyInfo> Getall()
     {
         using (var context = new Logisticentities( ))
         {
                try
                {
                    var Data = context.CompanyInfoes.ToList();
                    return Data;
                }
                catch (DbEntityValidationException ex)
                {
                    string errorMessages = string.Join("; ", ex.EntityValidationErrors.SelectMany(x => x.ValidationErrors).Select(x => x.PropertyName + " : " + x.ErrorMessage));
                    throw ex;
                }

            }
     }

        public CompanyInfo GetCompanyInfoById(int Id)
        {

            using (var context = new Logisticentities())
            {
                var objCompany = context.CompanyInfoes.Where(c => c.CompanyId == Id).SingleOrDefault();                
                return objCompany;
            }

        }

       

     public object Insert(CompanyInfo objCompany)
     {
       
         using (var context = new Logisticentities( ))
         {
            
               try
               {
                    context.CompanyInfoes.Add(objCompany);
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
     public object Update(CompanyInfo objCompany)
     {
            using (var context = new Logisticentities())
            {
                try
                {

                    var obj = context.CompanyInfoes.Where(x => x.CompanyId == objCompany.CompanyId).SingleOrDefault();
                    context.Entry(obj).CurrentValues.SetValues(objCompany);
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
     public object Delete(int Id)
     {
        
         using (var context = new Logisticentities( ))
         {
             try
             {

                    context.CompanyInfoes.Remove(context.CompanyInfoes.Single(x => x.CompanyId == Id));
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
