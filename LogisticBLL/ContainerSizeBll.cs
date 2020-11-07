using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LOGISTIC.DAL;

namespace LOGISTIC.BLL
{
    public class ContainerSizeBll
    {
        private ContainerSizeDal objDal = new ContainerSizeDal();
     
        public List<ContainerSize> Getall()
       {
           List<ContainerSize> objlist = new List<ContainerSize>();         
           objlist = objDal.Getall();
           return objlist;
       }

        public ContainerSize GetContSizeDetailsById(int contsizeId)
        {
            ContainerSize obj = new ContainerSize();
            obj = objDal.GetContsizeDetailsById(contsizeId);
            return obj;
        }

        public int Insert(ContainerSize objContainerSize)
       {

           var status = objDal.Insert(objContainerSize);
           return status;
       }

        public int Update(ContainerSize objContainerSize)
       {

           var status = objDal.Update(objContainerSize);

           return status;
       }

        public void Delete(int contsizeId)
       {

           objDal.Delete(contsizeId);
          
       }
    }
}
