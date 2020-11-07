using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Entity.Validation;

namespace LOGISTIC.DAL
{
    public class UserDAL
    {    
     public List<UserInfo> Getall()
     {
         using (var context = new Logisticentities( ))
         {         
             var Data = context.UserInfoes.OrderBy(x=>x.FirstName).ToList();
             return Data;
         }
     }

     public UserInfo GetUserByID(int id)
     {
         using (var context = new Logisticentities( ))
         {
             var Data = context.UserInfoes.Where(obj => obj.UserId == id).SingleOrDefault();
             return Data;
         }
     }

     public object Insert(UserInfo objUser)
     {
                
         using (var context = new Logisticentities())
         {           
             try
             {
                 context.UserInfoes.Add(objUser);
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

     public object Update(UserInfo objUser)
        {
            try
            {
                using (var context = new Logisticentities())
                {
                    var obj = context.UserInfoes.Where(x => x.UserId == objUser.UserId).SingleOrDefault();
                    context.Entry(obj).CurrentValues.SetValues(objUser);
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

     public object Delete(int id)
     {       
         using (var context = new Logisticentities( ))
         {
                try
                {
                    context.UserInfoes.Remove(context.UserInfoes.Single(x => x.UserId == id));
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
