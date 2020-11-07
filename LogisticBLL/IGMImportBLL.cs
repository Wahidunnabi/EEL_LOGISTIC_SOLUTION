using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LOGISTIC.DAL;
using System.Data;

namespace LOGISTIC.BLL
{
   public class IGMImportBLL
    {

       IGMImportDAL objDal = new IGMImportDAL();

        #region IGM Import
        //Get all IGM Import loading Customer(MLO),Importer,Vessel
        public List<IGMImport> GetAllIGMImport()
        {
            List<IGMImport> objlist = new List<IGMImport>();
            objlist = objDal.GetAllIGMImport();
            return objlist;
        }

        public List<IGMImport> GetAllIGMImportByImporterId( int id)
        {
            List<IGMImport> objlist = new List<IGMImport>();
            objlist = objDal.GetAllIGMImportByImporterId(id);
            return objlist;
        }
        public List<IGMImportDetail> GetIGMImportDetailBlnum(string blnumber)
        {
            List<IGMImportDetail> objlist = new List<IGMImportDetail>();
            objlist = objDal.GetIGMImportDetailByBlnum(blnumber);
            return objlist;
        }
        public DataTable Get_Indate_and_Size_and_BL_WiseSummery(string blnumber)
        {
            DataTable result = objDal.Get_Indate_and_Size_and_BL_WiseSummery(blnumber);
            return result;
           
        }
        public DataTable Get_Indatewise_ImportContainer_Position(string blnumber)
        {
            DataTable result = objDal.Get_Indatewise_ImportContainer_Position(blnumber);
            return result;

        }
        public string GetImportInvoicenumber(string Configtype)
        {
            string result = objDal.GetInvoiceNumber(Configtype);
            return result;

        }
        public List<IGMImport> GetIGMImportByBLNumber(string BLNumber, int importerId)
        {
            List<IGMImport> objlist = new List<IGMImport>();
            objlist = objDal.GetIGMImportByBLNumber(BLNumber, importerId);
            return objlist;
        }
        public List<IGMImport> GetIGMImportByBLNumberforGridContainer(string BLNumber, int importerId)
        {
            List<IGMImport> objlist = new List<IGMImport>();
            objlist = objDal.GetIGMImportByBLNumber(BLNumber, importerId);
            return objlist;
        }
        
        //Get single IGM Import without details
        public IGMImport GetIGMImportByIGMId(int id)
        {
            IGMImport objIGMImport = new IGMImport();
            objIGMImport = objDal.GetIGMImportByIGMId(id);
            return objIGMImport;
        }
        //Get single IGM Import including details
        public IGMImport GetIGMDetailsByIGMId(int id)
        {
            IGMImport objIGMImport = new IGMImport();
            objIGMImport = objDal.GetIGMDetailsByIGMId(id);
            return objIGMImport;
        }

        public IGMImport GetIGMImportByBL(string BLNumber)
        {
            IGMImport objIGMImport = new IGMImport();
            objIGMImport = objDal.GetIGMImportByBL(BLNumber);
            return objIGMImport;
        }

        public List<IGMImportDetail> GetAllIGMImportDetailsByBL(string BLnumber)
        {           
            List<IGMImportDetail> listIGMImportDetail = objDal.GetAllIGMImportDetailsByBL(BLnumber);
            return listIGMImportDetail;
        }

        public List<SerachIGMImportData_Result> SearchIGMImportData(int searchBy, string seatchText)
        {
            var listCSD = objDal.SearchIGMImportData(searchBy, seatchText);
            return listCSD;

        }

        public object CheckDuplicateEntry(IGMImport objIGMImport)
        {           
            var status = objDal.CheckDuplicateEntry(objIGMImport);
            return status;

        }

        public object Insert(IGMImport objIGMImport)
        {
            objIGMImport.EntryDate = DateTime.Now;
            var status = objDal.Insert(objIGMImport);
            return status;
                        
        }

        public object Update(IGMImport objIGMImport)
        {
            objIGMImport.ModifyDate = DateTime.Now;
            var status = objDal.Update(objIGMImport);
            return status;
        }

        public object Delete(IGMImport objIGMImport)
        {

            var status = objDal.Delete(objIGMImport);
            return status;

        }

        public int GetIGMRowCount()
        {
            var result = objDal.GetIGMRowCount();
            return result;

        }


        #endregion

        #region IGM Import Details

        //Get all IGM Import details including IGM-Import,container type,size
        public List<IGMImportDetail> GetAllIGMImportDetails()
        {
            List<IGMImportDetail> objlist = new List<IGMImportDetail>();
            objlist = objDal.GetAllIGMImportDetails();
            return objlist;
        }


        //Get IGM Import detail including IGM-Import,container type,size by IGMImportdetailId
        public IGMImportDetail GetIGMImportDetailById(long id)
        {
            IGMImportDetail objIGMImpDetails = new IGMImportDetail();
            objIGMImpDetails = objDal.GetIGMImportDetailById(id);
            return objIGMImpDetails;
        }

        //Get all IGM Import details of a IGM/BL number
        public List<IGMImportDetail> GetAllIGMImportDetailbyIGMId(int id)
        {
            List<IGMImportDetail> objlist = new List<IGMImportDetail>();
            objlist = objDal.GetAllIGMImportDetailbyIGMId(id);
            return objlist;
        }

        public List<IGMImportDetail> GetIGMImportDetailsByContNum(string contNumber)
        {
            List<IGMImportDetail> objlist = new List<IGMImportDetail>();
            objlist = objDal.GetIGMImportDetailsByContNum(contNumber);
            return objlist;
        }

        public IGMImportDetail GetIGMImportDetailstoGateIn(string contNumber)
        {
            IGMImportDetail objIGMDetails = new IGMImportDetail();
            objIGMDetails = objDal.GetIGMImportDetailstoGateIn(contNumber);
            return objIGMDetails;
        }

        public IGMImportDetail GetIGMImportDetailByContNum(string contNumber)
        {
           IGMImportDetail objIGMDetails = new IGMImportDetail();
            objIGMDetails = objDal.GetIGMImportDetailByContNum(contNumber);
            return objIGMDetails;
        }

        public List<IGMImportDetail> GetIGMImportDetailsByIGMNumber(string IGMNumber)
        {
            List<IGMImportDetail> objlist = new List<IGMImportDetail>();
            objlist = objDal.GetIGMImportDetailsByIGMNumber(IGMNumber);
            return objlist;
        }
        public List<IGMImportDetail> GetIGMImportDetailByBlnum(string BLnumber)
        {
            List<IGMImportDetail> objlist = new List<IGMImportDetail>();
            objlist = objDal.GetIGMImportDetailByBlnum(BLnumber);
            return objlist;
        }
        
        public List<IGMImportDetail> GetIGMImportDetailsBLNumber(string blNumber)
        {
            List<IGMImportDetail> objlist = new List<IGMImportDetail>();
            objlist = objDal.GetIGMImportDetailsBLNumber(blNumber);
            return objlist;
        }

        public List<IGMImportDetail> GetIGMImportDetailsByRotation(string rotation)
        {
            List<IGMImportDetail> objlist = new List<IGMImportDetail>();
            objlist = objDal.GetIGMImportDetailsByRotation(rotation);
            return objlist;
        }

        
        public string GetISOCode(int sizeId, int typeId)
        {
            string ISOCode = objDal.GetISOCode(sizeId, typeId);
            return ISOCode;
        }


        #endregion




        #region IGM Container Gate IN Out


        public List<IGMContGateInOut> GetAllIGMContGateIn()
        {
            List<IGMContGateInOut> listIGMGateIn = new List<IGMContGateInOut>();
            listIGMGateIn = objDal.GetAllIGMContGateIn();
            return listIGMGateIn;
        }

        //public List<IGMContGateInOut> GetIGMGateInByDateRange(DateTime StartDate, DateTime EndDate)
        //{
        //    List<IGMContGateInOut> listIGMDetails = new List<IGMContGateInOut>();
        //    listIGMDetails = objDal.GetIGMGateInByDateRange(StartDate, EndDate);
        //    return listIGMDetails;
        //}

        public List<IGMContGateInOut> GetAllIGMContGateOut()
        {
            List<IGMContGateInOut> listIGMGateOut = new List<IGMContGateInOut>();
            listIGMGateOut = objDal.GetAllIGMContGateOut();
            return listIGMGateOut;
        }

        public IGMContGateInOut GetIGMContGateInOutByID(long IGMContGateInOutId)
        {
            IGMContGateInOut objInOut = new IGMContGateInOut();
            objInOut = objDal.GetIGMContGateInOutByID(IGMContGateInOutId);
            return objInOut;
        }

        public IGMContGateInOut GetContGateInOutByIGMdetailsId(long Id)
        {
            var obj = objDal.GetContGateInOutByIGMdetailsId(Id);
            return obj;
        }

       
        //Save IGMContGateInOut object when Gate In complete
        public object SaveIGMContGateIn(IGMContGateInOut objGateInOut)
        {
            var status = objDal.SaveIGMContGateIn(objGateInOut);
            return status;
        }


        //Save or rather update IGMContGateInOut object when Gate Out complete
        public object SaveIGMContGateOut(IGMContGateInOut objGateInOut)
        {
            var status = objDal.SaveIGMContGateOut(objGateInOut);
            return status;
        }


        //Update IGMContGateInOut object when field old value are changed 
        public object UpdateIGMContGateInOut(IGMContGateInOut objGateInOut)
        {
            var status = objDal.UpdateIGMContGateInOut(objGateInOut);
            return status;
        }

        public object DeleteResetImportDetailsIGMContGateInOut(IGMContGateInOut objGateInOut)
        {
            var status = objDal.DeleteIGMContGateOut(objGateInOut);
            return status;
        }
        public object DeleteIGMContGateInOut(IGMContGateInOut ContInOut)
        {
            var status = objDal.DeleteIGMContGateInOut(ContInOut);
            return status;
        }


        public List<SerachIGMGateInOutData_Result> SearchIGMGateInOutData(int searchBy, string seatchText)
        {
            var listCSD = objDal.SearchIGMGateInOutData(searchBy, seatchText);
            return listCSD;

        }
        public string SetContGateInOutRefNo()
        {
            var refNo = objDal.SetContGateInOutRefNo();
            return refNo;
        }


        #endregion
    }
}
