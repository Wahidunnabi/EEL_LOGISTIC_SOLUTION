using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LOGISTIC.BLL
{
	public class StatusBLL
	{
        public List<Status> GetStatusList()
        {
            List<Status> objlist = new List<Status>();
            StatusDAL objDal = new StatusDAL();
            objlist = objDal.GetAllStatus();
            return objlist;
        }
	}
}
