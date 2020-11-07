using System;
using System.Collections.Generic;
using LOGISTIC.CSD.DAL;
using LOGISTIC.UserDefinedModel;
using System.Linq;

namespace LOGISTIC.CSD.BLL
{
   
    public class CSDGateInOutBLL
    {
      CSDGateInOutDAL objDal = new CSDGateInOutDAL();


        public object SetCSDRefNo(int custId)
        {
            var refNo = objDal.SetCSDRefNo(custId);
            return refNo;
        }

        public string GetCustNameById(int custId)
        {
            var custName = objDal.GetCustNameById(custId);
            return custName;
        }
        public string GetISOCode(int sizeId, int typeId)
        {
            string ISOCode = objDal.GetISOCode(sizeId, typeId);
            return ISOCode;
        }



        //Get all CSD GateIn GateOut data
        public List<CSDContGateInOut> GetAllCSDData()
        {
            List<CSDContGateInOut> objlist = new List<CSDContGateInOut>();          
            objlist = objDal.GetAllCSDData();
            return objlist;
        }



        #region Search CSD


        public CSDContGateInOut GetCSDById(int id)
        {
            var objCSD = objDal.GetCSDById(id);
            return objCSD;

        }

        public List<SerachCSDGateInOutData_Result> SearchCSDGateInOutData(int searchBy, string seatchText)
        {
            var listCSD = objDal.SearchCSDGateInOutData(searchBy, seatchText);
            return listCSD;

        }

        public List<SerachCSDGateInData_Result> SearchCSDGateInData(int searchBy, string seatchText)
        {
            var listCSD = objDal.SearchCSDGateInData(searchBy, seatchText);
            return listCSD;

        }

        //return CSDContGateInOut objects where only Gate In is true....Gate Out not yet completed
        public List<CSDContGateInOut> GetAllCSDGateInByMLOId(int custId)
        {
            List<CSDContGateInOut> objlist = new List<CSDContGateInOut>();
            objlist = objDal.GetAllCSDGateInByMLOId(custId);
            return objlist;
        }

        public CSDContGateInOut GetCSDByContNumber(string contNumber)
        {
            var objCSD = objDal.GetCSDByContNumber(contNumber);
            return objCSD;

        }

        //for searching in container history form
        public List<CSDContGateInOut> GetlistCSDByContNumber(string contNo)
        {
            List<CSDContGateInOut> objlist = new List<CSDContGateInOut>();
            objlist = objDal.GetlistCSDByContNumber(contNo);
            return objlist;
        }

        //for searching in container history form
        public List<CSDContGateInOut> GetListCSDByRefNumber(long refNo)
        {
            List<CSDContGateInOut> objlist = new List<CSDContGateInOut>();
            objlist = objDal.GetListCSDByRefNumber(refNo);
            return objlist;
        }

        #endregion


        public List<TrailerNumber> GetAllTrailerNumber(int trailerId)
        {
            List<TrailerNumber> objlist = new List<TrailerNumber>();
            objlist = objDal.GetAllTrailernumber(trailerId);
            return objlist;
        }

        public clsContainerHistory GetContainerHistory(long csdId, int inOutStatus)
        {
            var obj = objDal.GetContainerHistory(csdId, inOutStatus);
            return obj;
        }

        public object Insert(CSDContGateInOut objCSD, long CSDUpcomingId)
        {          
          var status = objDal.Insert(objCSD, CSDUpcomingId);
          return status;
        }

        public object InsertCSDList(List<CSDContGateInOut> listCSD)
        {
            var status = objDal.InsertCSDList(listCSD);
            return status;
        }

        public object Update(CSDContGateInOut objCSD)
        {
          
            var status = objDal.Update(objCSD);
            return status;
        }
        public object UpdateCSDList(List<CSDContGateInOut> listCSD)
        {
            var status = objDal.UpdateCSDList(listCSD);
            return status;
        }

        public object Delete(long csdId)
        {
            var status = objDal.Delete(csdId);
            return status;
        }

        public void DeleteUpComingContainer(long Id)
        {
           objDal.DeleteUpComingContainer(Id);
           
        }
    }
}
