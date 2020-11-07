
using System.Collections.Generic;

namespace LOGISTIC
{
   
    public class AuthenticationBLL
    {
        AuthenticationDAL objDal = new AuthenticationDAL();

        public UserInfo GetCustByUserNamePass(string userName, string pass)
        {
            var obj = objDal.GetCustByUserNamePass(userName, pass);
            return obj;
        }

        public UserInfo GetUserByUserName(string userName)
        {
            var obj = objDal.GetUserByUserName(userName);
            return obj;
        }

        public List<UserRole> GetAllUserRole()
        {
            var obj = objDal.GetAllUserRole();
            return obj;
        }

        public List<FormList> GetAllFormList()
        {
            var obj = objDal.GetAllFormList();
            return obj;
        }

        public List<UserPermissionMapping> GetAllUserPermission()
        {
            var obj = objDal.GetAllUserPermission();
            return obj;
        }

        public List<UserPermissionMapping> GetAllUserPermissionByRoleId(int roleId)
        {
            var obj = objDal.GetAllUserPermissionByRoleId(roleId);
            return obj;
        }

        public object Insert(UserPermissionMapping objMapping)
        {
            var status = objDal.Insert(objMapping);
            return status;
        }


        public object Update(UserPermissionMapping objMapping)
        {

            var status = objDal.Update(objMapping);
            return status;
        }

        public object Delete(int Id)
        {
            var status = objDal.Delete(Id);
            return status;
        }



    }
}
