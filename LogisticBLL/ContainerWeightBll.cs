using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LOGISTIC.DAL;

namespace LOGISTIC.BLL
{
    public class ContainerWeightBll
    {
        private ContainerWightDal objDal = new ContainerWightDal();
       
        public List<ContainerGrossWeight> Getall()
       {
           List<ContainerGrossWeight> objlist = new List<ContainerGrossWeight>();
           objlist = objDal.Getall();
           return objlist;
       }
        public int Insert(ContainerGrossWeight objContainerWeight)
       {
            var status = objDal.Insert(objContainerWeight);
            return status;
       }
        public int Update(ContainerGrossWeight objContainerWeight)
       {
            var status = objDal.Update(objContainerWeight);
            return status;
       }
        public ContainerGrossWeight Delete(ContainerGrossWeight objContainerWeight)
       {
           objDal.Delete(objContainerWeight);
           return objContainerWeight;
       }
    }
}
