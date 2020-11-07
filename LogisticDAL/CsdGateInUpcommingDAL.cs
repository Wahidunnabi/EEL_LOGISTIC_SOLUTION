using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Entity.Validation;

namespace LOGISTIC.DAL
{
    public class CsdGateInUpcommingDAL
    {

        public List<CSDGateInUPComing> Getall()
        {
            using (var context = new Logisticentities())
            {

                var Data = context.CSDGateInUPComings.ToList();
                return Data;
            }
        }

     
        public  object Insert(List<CSDGateInUPComing>  listUpcomingCont)
         {
               
             using (var context = new Logisticentities( ))
             {
             
                 try
                 {
                    foreach (CSDGateInUPComing objUpcoming in listUpcomingCont)
                    {
                        context.CSDGateInUPComings.Add(objUpcoming);
                        context.SaveChanges();
                    }                   
                     
                     return "Data has been saved successfully.";
                    }
                    catch (DbEntityValidationException ex)
                    {
                        string errorMessages = string.Join("; ", ex.EntityValidationErrors.SelectMany(x => x.ValidationErrors).Select(x => x.PropertyName + " : " + x.ErrorMessage));
                        return errorMessages;
                    }

                }
     }


        public object Update(CSDGateInUPComing objUpcomingCont)
        {
            try
            {
                using (var context = new Logisticentities())
                {

                    var obj = context.CSDGateInUPComings.Where(x => x.Id == objUpcomingCont.Id).SingleOrDefault();
                    context.Entry(obj).CurrentValues.SetValues(objUpcomingCont);
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

        public object Delete(int ID)
        {

            using (var context = new Logisticentities())
            {
                try
                {

                    context.CSDGateInUPComings.Remove(context.CSDGateInUPComings.Single(x => x.Id == ID));
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
