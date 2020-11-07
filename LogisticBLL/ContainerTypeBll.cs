using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LOGISTIC.DAL;

namespace LOGISTIC.BLL
{
    public class ContainerTypeBll
    {
       private ContainerTypeDal objDal = new ContainerTypeDal();
      

       public List<ContainerType> Getall()
       {
           List<ContainerType> objlist = new List<ContainerType>();         
           objlist = objDal.Getall();
           return objlist;
       }

       public ContainerType GetConTypeById(int contId)
       {
           ContainerType objConTyp = new ContainerType();
           objConTyp = objDal.GetContenTypeById(contId);
           return objConTyp;
       }

       public int Insert(ContainerType objContainerType)
       {

           var status = objDal.Insert(objContainerType);
           return status;
       }

       public int Update(ContainerType objContainerType)
       {

           var status = objDal.Update(objContainerType);
           return status;
       }

       public void Delete(int contId )
       {
           objDal.Delete(contId);          
       }
    }
}
