using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LOGISTIC.DAL;

namespace LOGISTIC.BLL
{
    public class AgentBLL
    {
        AgentDAL objDal = new AgentDAL();
     
        public List<Agent> Getall()
       {
            List<Agent> objlist = new List<Agent>();
            objlist = objDal.Getall();
            return objlist;
       }

        public Agent GetAgentById(int agntId)
        {
            Agent objAgnt = new Agent();
            objAgnt = objDal.GetAgentById(agntId);
            return objAgnt;
        }

        public object Insert(Agent objAgent)
       {
           var status = objDal.Insert(objAgent);
           return status;
       }

        public object Update(Agent objAgent)
       {

           var status = objDal.Update(objAgent);
           return status;

       }

        public object Delete(int agentId)
       {

            var status = objDal.Delete(agentId);
            return status;

        }
    }
}
