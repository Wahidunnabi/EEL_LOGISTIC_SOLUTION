using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LOGISTIC.DAL;

namespace LOGISTIC.BLL
{
    public class UserBLL
    {
        UserDAL objDal = new UserDAL();
        public List<UserInfo> Getall()
       {
           List<UserInfo> objlist = new List<UserInfo>();         
           objlist = objDal.Getall();
           return objlist;
       }

        public UserInfo GetUserByID( int id)
        {
            UserInfo objUser = new UserInfo();
            objUser = objDal.GetUserByID(id);
            return objUser;
        }

        public object Insert(UserInfo objUser)
       {          
           var status = objDal.Insert(objUser);
           return status;
       }

       public object Update(UserInfo objUser)
       {         
          var status= objDal.Update(objUser);
           return status;
       }


       public object Delete(int userId)
       {
            var status = objDal.Delete(userId);
            return status;
                   
       }

    }
}
