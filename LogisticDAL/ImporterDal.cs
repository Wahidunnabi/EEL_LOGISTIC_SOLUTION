using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;
using System.Data;
using System.Data.Entity.Validation;

namespace LOGISTIC.DAL
{
 public class ImporterDal
    {

     private int success;

     public List<Importer> Getall()
     {
         using (var context = new Logisticentities( ))
         {

             var Data = context.Importers.OrderBy(x=>x.ImporterName).ToList();
             return Data;
         }
     }

     public Importer GetImporterById(int impId)
     {
         using (var context = new Logisticentities( ))
         {

             var Data = context.Importers.Where(i=>i.ImporterId==impId).SingleOrDefault();            
             return Data;
         }
     }

     public object Insert(Importer objImpt)
     {        
         using (var context = new Logisticentities( ))
         {
                try
                {
                    context.Importers.Add(objImpt);
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

     public object Update(Importer objImpt)
     {
            try
            {
                using (var context = new Logisticentities())
                {
                    var obj = context.Importers.Where(x => x.ImporterId == objImpt.ImporterId).SingleOrDefault();
                    context.Entry(obj).CurrentValues.SetValues(objImpt);
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

     public object Delete(int ImporterId )
     {
        
         using (var context = new Logisticentities( ))
         {
                try
                {

                    context.Importers.Remove(context.Importers.Single(x => x.ImporterId == ImporterId));
                    success = context.SaveChanges();
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
