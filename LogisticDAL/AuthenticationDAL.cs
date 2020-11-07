using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Entity.Validation;

namespace LOGISTIC
{
   
    public class AuthenticationDAL
    {
        public UserInfo GetCustByUserNamePass(string userName, string pass)
        {

            using (var context = new Logisticentities())
            {

                var user = context.UserInfoes.Where(c => c.LoginId.Equals(userName) && c.Password.Equals(pass)).SingleOrDefault();               
                return user;
            }

        }

        public UserInfo GetUserByUserName(string userName)
        {

            using (var context = new Logisticentities())
            {

                var user = context.UserInfoes.Where(c => c.LoginId.Contains(userName)).FirstOrDefault();
                return user;
            }

        }

        public List<UserRole>  GetAllUserRole()
        {
            using (var context = new Logisticentities())
            {

                var roles = context.UserRoles.ToList();
                return roles;
            }
        }


        public List<FormList> GetAllFormList()
        {
            using (var context = new Logisticentities())
            {
                var forms = context.FormLists.ToList();
                return forms;
            }
        }

        public List<UserPermissionMapping> GetAllUserPermission()
        {
            using (var context = new Logisticentities())
            {

                var listPermission = context.UserPermissionMappings
                    .Include("UserRole")
                    .Include("FormList")
                    .OrderBy(p => p.RoleId)
                    .ThenBy(p => p.FormId)
                    .ToList();
                return listPermission;
            }
        }

        public List<UserPermissionMapping> GetAllUserPermissionByRoleId( int roleId )
        {
            using (var context = new Logisticentities())
            {

                var listPermission = context.UserPermissionMappings                   
                    .Include("FormList")
                    .Where(p => p.RoleId == roleId)
                    .ToList();
                return listPermission;
            }
        }

        public object Insert(UserPermissionMapping objMapping)
        {

            using (var context = new Logisticentities())
            {
                try
                {
                    context.UserPermissionMappings.Add(objMapping);
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

        public object Update(UserPermissionMapping objMapping)
        {
            try
            {
                using (var context = new Logisticentities())
                {

                    var obj = context.UserPermissionMappings.Where(x => x.Id == objMapping.Id).SingleOrDefault();
                    context.Entry(obj).CurrentValues.SetValues(objMapping);
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

        public object Delete(int Id)
        {

            using (var context = new Logisticentities())
            {
                try
                {

                    context.UserPermissionMappings.Remove(context.UserPermissionMappings.Single(x => x.Id == Id));
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
