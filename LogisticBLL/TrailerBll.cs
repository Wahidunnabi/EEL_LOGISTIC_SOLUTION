using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LOGISTIC.DAL;

namespace LOGISTIC.BLL
{
    public class TrailerBll
    {
        TrailerDal objDal = new TrailerDal();
     
        public List<Trailer> Getall()
       {
           List<Trailer> objlist = new List<Trailer>();
                     objlist = objDal.Getall();
           return objlist;
       }

        public Trailer GetTrailerById(int trlrId)
        {
            Trailer objTrlr = new Trailer();
            objTrlr = objDal.GetTrailerById(trlrId);
            return objTrlr;
        }

        public int Insert(Trailer objTrailer)
       {

           var status = objDal.Insert(objTrailer);
           return status;
       }

        public int Update(Trailer objTrailer)
       {

           var status = objDal.Update(objTrailer);
           return status;

       }

        public void Delete(int traiId)
       {
         
           objDal.Delete(traiId);
           
       }
    }
}
