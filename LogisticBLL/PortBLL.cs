
using System.Collections.Generic;
using LOGISTIC.DAL;

namespace LOGISTIC.BLL
{
   public class PortBLL
    {

       PortDAL objDal = new PortDAL();

        public List<Port> Getall()
        {
            List<Port> objlist = new List<Port>();           
            objlist = objDal.Getall();
            return objlist;
        }

        public Port GetPortByID(int portId)
        {
            Port objPort = new Port();
            objPort = objDal.GetPortDetailsByID(portId);
            return objPort;
        }

        public int Insert(Port objPort)
        {

            var status = objDal.Insert(objPort);
            return status;
        }

        public Port Update(Port objPort)
        {

            objDal.Update(objPort);
            return objPort;
        }


        public void Delete(int PortId)
        {

            objDal.Delete(PortId);

        }
    }
}
