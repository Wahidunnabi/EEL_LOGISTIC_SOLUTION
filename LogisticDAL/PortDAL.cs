using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace LOGISTIC.DAL
{
  public  class PortDAL
    {

        public List<Port> Getall()
        {
            using (var context = new Logisticentities())
            {
                var Data = context.Ports.OrderBy(s=>s.PortName).ToList();
                return Data;
            }
        }

        public Port GetPortDetailsByID(int portId)
        {
            using (var context = new Logisticentities())
            {

                var Data = context.Ports.Where(obj => obj.PortOfLandId == portId).SingleOrDefault();
                return Data;
            }
        }

        public int Insert(Port objPort)
        {

            using (var context = new Logisticentities())
            {

                try
                {
                    context.Ports.Add(objPort);
                    context.SaveChanges();
                    return 1;
                }
                catch (Exception exception)
                {
                    throw exception;
                   
                }
            }
        }

        public Port Update(Port objPort)
        {
            try
            {
                using (var context = new Logisticentities())
                {
                    var obj = context.Ports.Where(x => x.PortOfLandId == objPort.PortOfLandId).SingleOrDefault();
                    context.Entry(obj).CurrentValues.SetValues(objPort);
                    context.SaveChanges();
                    return objPort;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {

            }

        }

        public void Delete(int portId)
        {

           using (var context = new Logisticentities())
            {
                try
                {
                    context.Ports.Remove(context.Ports.Single(x => x.PortOfLandId == portId));
                    context.SaveChanges();

                }
                catch (OptimisticConcurrencyException ex)
                {
                    throw ex;
                }

            }
        }
    }
}
