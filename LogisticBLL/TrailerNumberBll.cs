using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LOGISTIC.DAL;

namespace LOGISTIC.BLL
{
    public class TrailerNumberBll
    {
        TrailerNumberDal objDal = new TrailerNumberDal();
   
        public List<TrailerNumber> Getall()
       {
           List<TrailerNumber> objlist = new List<TrailerNumber>();           
           objlist = objDal.Getall();
           return objlist;
       }

        public List<TrailerNumber> GetAllTrailernumberBytrailerId(int trlrId)
        {
            List<TrailerNumber> objlist = new List<TrailerNumber>();
            objlist = objDal.GetAllTrailernumberBytrailerId(trlrId);
            return objlist;
        }

        public TrailerNumber GetTrailerNumById(int trailerId)
        {

            var status = objDal.GetTrailerNumberById(trailerId);
            return status;

        }
       
       public List<Trailer> GetComboTrailer()
       {
           List<Trailer> objlist = new List<Trailer>();
           TrailerNumberDal objDal = new TrailerNumberDal();
           objlist = objDal.GetTrailerComboload();
           return objlist;
       }
        
        public int Insert(TrailerNumber objTrailerNumber)
       {

           var status = objDal.Insert(objTrailerNumber);
           return status;
       }

        public int Update(TrailerNumber objTrailerNumber)
       {

           var status = objDal.Update(objTrailerNumber);
           return status;
       }

       public void Delete(int trlNumId)
       {

           objDal.Delete(trlNumId);
           
       }

    }
}
