using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LOGISTIC.DAL;

namespace LOGISTIC.BLL
{
    public class LocationBLL
    {
        LocationDAL objDal = new LocationDAL();
        public List<Location> Getall()
        {
            List<Location> objlist = new List<Location>();
            objlist = objDal.Getall();
            return objlist;
        }
        public object Insert(Location objLocation)
        {

            var status = objDal.Insert(objLocation);
            return status;

        }
        public object Update(Location objLocation)
        {

            var status = objDal.Update(objLocation);
            return status;
        }
        public object Delete(Location objLocation)
        {

            var status = objDal.Delete(objLocation);
            return status;
        }
    }
}
