using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using LOGISTIC;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Configuration;
using System.Data.SqlClient;

namespace LOGISTIC.DAL
{
   public class IGMImportDAL
    {
        #region IGM Import

        public int GetIGMRowCount()
        {
            using (var context = new Logisticentities())
            {
                try
                {
                    var totalRow = context.IGMImports.Where(x=>x.EntryDate.Value.Year==DateTime.Now.Year).Count();                    
                    return totalRow;
                }
                catch (Exception exception)
                {
                    throw exception;
                }
            }
        }

        public Object CheckDuplicateEntry(IGMImport objIGMImport)
        {

            using (var context = new Logisticentities())
            {

                try
                {
                    var duplicateEntry = "";
                    foreach (var item in objIGMImport.IGMImportDetails)
                    {
                        try
                        {
                            IGMImportDetail objDetails = context.IGMImportDetails.Where(x => x.ContainerNo == item.ContainerNo && x.InOutStatus != 2).FirstOrDefault();
                            if (objDetails != null)
                            {
                                duplicateEntry = duplicateEntry + objDetails.ContainerNo + " already exist !!\n";
                            }
                        }
                        catch (Exception e)
                        {
                            return e.ToString();
                        } 

                    }                    
                    return duplicateEntry;
                }
                catch (DbEntityValidationException ex)
                {
                    string errorMessages = string.Join("; ", ex.EntityValidationErrors.SelectMany(x => x.ValidationErrors).Select(x => x.PropertyName + " : " + x.ErrorMessage));
                    return errorMessages;
                }

            }
        }

        public Object Insert(IGMImport objIGMImport)
        {

            using (var context = new Logisticentities( ))
            {

                try
                {
                    context.IGMImports.Add(objIGMImport);
                    context.SaveChanges();
                    return "Data has been saved successfully.";
                }
                catch (DbEntityValidationException ex)
                {
                    string errorMessages = string.Join("; ", ex.EntityValidationErrors.SelectMany(x => x.ValidationErrors).Select(x => x.PropertyName + " : " + x.ErrorMessage));
                    return errorMessages;
                }

            }
        }

        public object Update(IGMImport updatedIGMImport)
        {
            try
            {
                using (var context = new Logisticentities())
                {
                    var oldIGMObj = context.IGMImports.Include("IGMImportDetails").Where(x => x.IGMImportId == updatedIGMImport.IGMImportId).SingleOrDefault();

                    context.Entry(oldIGMObj).CurrentValues.SetValues(updatedIGMImport);

                    foreach (var item in oldIGMObj.IGMImportDetails.ToList())
                    {
                        //Delete IGM Details that has been removed from IGM Import

                        if (!updatedIGMImport.IGMImportDetails.Any(s => s.IGMDetailsId == item.IGMDetailsId))
                            context.IGMImportDetails.Remove(item);
                    }                                          
                    foreach (var item in updatedIGMImport.IGMImportDetails)
                    {                      

                        //New item ...so add this item
                        if (item.IGMDetailsId == 0)
                        {
                            context.IGMImportDetails.Add(item);                            
                        }
                        else
                        {
                           //Existing item ...then update this item
                            var oldIGMDetails = oldIGMObj.IGMImportDetails.Where(s => s.IGMDetailsId == item.IGMDetailsId).SingleOrDefault();
                            context.Entry(oldIGMDetails).CurrentValues.SetValues(item);
                        }
                          

                        ////Check whether this item is new or existion one
                        //var oldIGMDetails = oldIGMObj.IGMImportDetails.Where(s => s.IGMDetailsId == item.IGMDetailsId).SingleOrDefault();
                        ////Existing item ...then update this item
                        //if (oldIGMDetails != null)
                        //{
                        //    context.Entry(oldIGMDetails).CurrentValues.SetValues(item);

                        //}
                        //else
                        //    //New item ...so add this item
                        //    context.IGMImportDetails.Add(item);
                    }

                    context.SaveChanges();
                    return "Data has been updated successfully.";
                }
            }
            catch (DbEntityValidationException ex)
            {
                string errorMessages = string.Join("; ", ex.EntityValidationErrors.SelectMany(x => x.ValidationErrors).Select(x => x.PropertyName + " : " + x.ErrorMessage));
                return errorMessages;
            }
           
        }

        public object Delete(IGMImport objIGMImport)
        {

            using (var context = new Logisticentities( ))
            {
                try
                {

                    context.IGMImports.Remove(context.IGMImports.Single(x => x.IGMImportId == objIGMImport.IGMImportId));
                    context.SaveChanges();
                    return "Data has been deleted successfully.";

                }
                catch (DbEntityValidationException ex)
                {
                    string errorMessages = string.Join("; ", ex.EntityValidationErrors.SelectMany(x => x.ValidationErrors).Select(x => x.PropertyName + " : " + x.ErrorMessage));
                    return errorMessages;
                }

            }
        }

        public List<IGMImport> GetAllIGMImport()
        {
            using (var context = new Logisticentities( ))
            {
                //var Data = context.IGMImports
                //    .Include("Customer")
                //    .Include("Importer")
                //    .Include("Vessel")
                //    .ToList();
                var Data = context.IGMImports.ToList();
                return Data;

            }
        }


        public List<IGMImport> GetAllIGMImportByImporterId(int id)
        {
            using (var context = new Logisticentities())
            {               
                var Data = context.IGMImports.Where(x=>x.ImporterId== id).OrderByDescending(x=>x.IGMImportId).Take(50).ToList();
                return Data;

            }
        }

        public List<IGMImport> GetIGMImportByBLNumber(string BLNumber, int importerId)
        {
            using (var context = new Logisticentities())
            {
                var Data = context.IGMImports.Where(x => x.BLnumber.Contains(BLNumber) && x.ImporterId == importerId).ToList();
                return Data;

            }
        }
        public List<IGMImport> GetIGMImportByBLNumberforGridContainer(string BLNumber, int importerId)
        {
            using (var context = new Logisticentities())
            {
                var Data = context.IGMImports.Where(x => x.BLnumber.Contains(BLNumber) && x.ImporterId == importerId).ToList();
                return Data;

            }
        }
        //Single IGM Import without details
        public IGMImport GetIGMImportByIGMId(int id)
        {
            using (var context = new Logisticentities( ))
            {
                var Data = context.IGMImports.Where(x => x.IGMImportId == id).SingleOrDefault();
                return Data;
            }
        }

        //Single IGM Import with details
        public IGMImport GetIGMDetailsByIGMId(int id)
        {
            using (var context = new Logisticentities( ))
            {
                var Data = context.IGMImports.Include("IGMImportDetails").Where(x => x.IGMImportId == id).SingleOrDefault();
                return Data;
            }
        }

        public IGMImport GetIGMImportByBL(string blNumber)
        {
            using (var context = new Logisticentities( ))
            {

                var Data = context.IGMImports
                    .Include("IGMImportDetails")
                    .Include("UnintOfMeasure")
                    .Where(obj => obj.BLnumber == blNumber).SingleOrDefault();
                return Data;
            }
        }

        public List<IGMImportDetail> GetAllIGMImportDetailsByBL(string blNumber)
        {
            using (var context = new Logisticentities( ))
            {

                var Data = context.IGMImports.Where(obj => obj.BLnumber.Contains(blNumber)).FirstOrDefault();
                if (Data != null)
                {
                    var obj = context.IGMImportDetails
                        .Include("IGMContGateInOuts")
                        .Include("ContainerSize")
                        .Include("ContainerType")
                        .Where(x => x.IGMImportId==Data.IGMImportId).ToList();
                    return obj;
                }
                return null;    
                
            }
        }


        public List<SerachIGMImportData_Result> SearchIGMImportData(int searchBy, string searchText)
        {
            using (var context = new Logisticentities())
            {
                var listCSD = context.SerachIGMImportData(searchBy, searchText).ToList();
                return listCSD;
            }
        }


        #endregion

        #region IGM Import Details


        //Get all IGM Import details 
        public List<IGMImportDetail> GetAllIGMImportDetails()
        {
            using (var context = new Logisticentities( ))
            {

                var Data = context.IGMImportDetails
                    .Include("IGMImport")
                    .Include("ContainerSize")
                    .Include("ContainerType")
                    .Take(50000000)
                    .OrderByDescending(x=>x.IGMDetailsId)
                    .ToList();
                return Data;
            }
        }

        //Get IGM Import detail
        public IGMImportDetail GetIGMImportDetailById(long id)
        {
            using (var context = new Logisticentities( ))
            {

                var Data = context.IGMImportDetails
                    .Include("IGMImport")
                    .Include("IGMContGateInOuts")
                    .Include("ContainerSize")
                    .Include("ContainerType")
                    .Where(x => x.IGMDetailsId == id).SingleOrDefault();
                return Data;
            }
        }


        //Get all IGM Import details of a IGM/BL number
        public List<IGMImportDetail> GetAllIGMImportDetailbyIGMId(int IGMId)
        {
            using (var context = new Logisticentities( ))
            {

                var Data = context.IGMImportDetails
                    .Include("ContainerSize")
                    .Include("ContainerType")
                    .Where(x => x.IGMImportId == IGMId).ToList();
                return Data;
            }
        }

        public List<IGMImportDetail> GetIGMImportDetailsByContNum(string contNumber)
        {
            using (var context = new Logisticentities( ))
            {              
                    var Data = context.IGMImportDetails
                                        .Include("IGMImport")
                                        .Include("ContainerSize")
                                        .Include("ContainerType")
                                        .Where(x => x.ContainerNo== contNumber).ToList();
                    return Data;

            }
        }

        public IGMImportDetail GetIGMImportDetailByContNum(string contNumber)
        {
            using (var context = new Logisticentities())
            {
                var Data = context.IGMImportDetails
                                    .Include("IGMContGateInOut")
                                    .Include("ContainerSize")
                                    .Include("ContainerType")
                                    .Where(x => x.ContainerNo == contNumber).OrderByDescending(x=>x.IGMDetailsId).FirstOrDefault();
                return Data;

            }
        }
        public List<IGMImportDetail> GetIGMImportDetailByBlnum(string blnumber)
        {
            using (var context = new Logisticentities())
            {
                var Data = context.IGMImportDetails
                                    .Include("IGMContGateInOuts").ToList();
                                  
                                  
                return Data;

            }
        }

        public DataTable Get_Indate_and_Size_and_BL_WiseSummery(string BlNumber)
        {
            // ok for Load
            string constring = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constring))
            {
                using (SqlCommand cmd = new SqlCommand("Get_Indate_and_Size_and_BL_WiseSummery", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                   
                    cmd.Parameters.AddWithValue("@Blnumber", BlNumber);
                   
                    con.Open();
                    DataTable dt = new DataTable();
                    dt.Load(cmd.ExecuteReader());
                    con.Close();
                    return dt;
                }
            }
        }
        public DataTable Get_Indatewise_ImportContainer_Position(string BlNumber)
        {
            // ok for Load
            string constring = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constring))
            {
                using (SqlCommand cmd = new SqlCommand("Get_Indatewise_ImportContainer_Position", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Blnumber", BlNumber);

                    con.Open();
                    DataTable dt = new DataTable();
                    dt.Load(cmd.ExecuteReader());
                    con.Close();
                    return dt;
                }
            }
        }
        public string GetInvoiceNumber(string Configtype)
        {


            string exist = null;
            string constring = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection conn = new SqlConnection(constring);

            conn.Open();
            SqlCommand comm = new SqlCommand("sp_GetPatternConfig", conn);
            comm.Parameters.AddWithValue("ConfigType", Configtype);
            comm.CommandType = CommandType.StoredProcedure;

            //returning the output

            exist = (string)comm.ExecuteScalar();

            return exist;
            
        }
        public IGMImportDetail GetIGMImportDetailstoGateIn(string contNumber)
        {
            using (var context = new Logisticentities())
            {
                var Data = context.IGMImportDetails.Where(x => x.ContainerNo == contNumber && x.InOutStatus == 0).FirstOrDefault();
                return Data;

            }
        }
        public List<IGMImportDetail> GetIGMImportDetailsByIGMNumber(string IGMNumber)
        {
            using (var context = new Logisticentities( ))
            {
                List<IGMImportDetail> listIGMDetails = new List<IGMImportDetail>();
                var objIGMImport = context.IGMImports.Where(x => x.ReferanceNumber.Contains(IGMNumber)).ToList();

                if (objIGMImport.Count > 0)
                {
                   
                    foreach (var item in objIGMImport)
                    {
                        var objIGMDetails = context.IGMImportDetails
                                                           .Include("IGMImport")
                                                           .Include("ContainerSize")
                                                           .Include("ContainerType")
                                                           .Where(x => x.IGMImportId == item.IGMImportId).ToList();
                        foreach (var obj in objIGMDetails)
                        {
                            listIGMDetails.Add(obj);
                        }
                    }
                }  
                             
                return listIGMDetails;
            }
        }

        public List<IGMImportDetail> GetIGMImportDetailsBLNumber(string blNumber)
        {


            using (var context = new Logisticentities())
            {
                var objIGMImport = context.IGMImports.Where(x => x.BLnumber.Contains(blNumber)).FirstOrDefault();
                if (objIGMImport != null)
                {
                    var Data = context.IGMImportDetails
                                                        .Include("IGMImport")
                                                        .Include("ContainerSize")
                                                        .Include("ContainerType")
                                                        .Where(x => x.IGMImportId == objIGMImport.IGMImportId).ToList();
                    return Data;

                }
                return null;

            }
            //using (var context = new Logisticentities())



            //{
            //    var objIGMImport = context.IGMImports.Where(x => x.BLnumber.Contains(blNumber)).FirstOrDefault();
            //    var objIGMImportDetails = context.IGMImportDetails.Where(x => x.IGMImportId == objIGMImport.IGMImportId).FirstOrDefault();
            //    var obj = context.IGMContGateInOuts.Where(x => x.IGMDetailsId == objIGMImportDetails.IGMDetailsId).FirstOrDefault();


            //    //var Data = context.IGMImportDetails
            //    //  .Include("IGMImport")
            //    //  .Include("IGMContGateInOuts")
            //    //  .Include("ContainerSize")
            //    //  .Include("ContainerType")
            //    //  .Where(x => x.IGMDetailsId == id).SingleOrDefault();
            //    //return Data;


            //    if (objIGMImport != null)
            //    {
            //        var Data = context.IGMImportDetails.Include("IGMContGateInOuts")
            //                                            .Include("ContainerSize")
            //                                            .Include("ContainerType")

            //                                            .Where(x => x.IGMDetailsId == objIGMImportDetails.IGMDetailsId).ToList();
            //        return Data;

            //    }
            //    return null;

            //}
        }

        public List<IGMImportDetail> GetIGMImportDetailsByRotation(string rotation)
        {
            using (var context = new Logisticentities( ))
            {
                var objIGMImport = context.IGMImports.Where(x => x.Rotation.Contains(rotation)).FirstOrDefault();
                if (objIGMImport != null)
                {
                    var Data = context.IGMImportDetails
                                                        .Include("IGMImport")
                                                        .Include("ContainerSize")
                                                        .Include("ContainerType")
                                                        .Where(x => x.IGMImportId == objIGMImport.IGMImportId).ToList();

                    return Data;
                }
                
                return null;

            }
        }

      

       


        public string GetISOCode(int sizeId, int typeId)
        {
            using (var context = new Logisticentities())
            {

                var objISO = context.ISOMappings.Where(x => x.SizeId == sizeId && x.TypeId == typeId).FirstOrDefault();
                return objISO.ISOCode;

            }
        }


        #endregion

        #region Container Gate In  Out


        public List<IGMContGateInOut> GetAllIGMContGateIn()
        {
            using (var context = new Logisticentities())
            {
                List<IGMContGateInOut> listIGMGateIn = new List<IGMContGateInOut>();
                listIGMGateIn = context.IGMContGateInOuts
                    .Include("IGMImportDetail")
                    .Where(x => x.GateOutDate == null).OrderByDescending(x => x.IGMContGateInOutId).ToList();
                return listIGMGateIn;
            }
        }
       

        public List<IGMContGateInOut> GetAllIGMContGateOut()
        {
            using (var context = new Logisticentities())
            {
                List<IGMContGateInOut> listIGMGateOut = new List<IGMContGateInOut>();
                listIGMGateOut = context.IGMContGateInOuts
                    .Include("IGMImportDetail")
                    .Where(x => x.GateOutDate != null).OrderByDescending(x => x.IGMContGateInOutId).ToList();
                return listIGMGateOut;
            }
        }

        public IGMContGateInOut GetIGMContGateInOutByID( long IGMContGateInOutId)
        {
            using (var context = new Logisticentities())
            {
                IGMContGateInOut objInOut = new IGMContGateInOut();
                objInOut = context.IGMContGateInOuts
                    .Include("IGMImportDetail")
                    .Where(x => x.IGMContGateInOutId == IGMContGateInOutId).SingleOrDefault();
                return objInOut;
            }
        }


        //public List<IGMContGateInOut> GetIGMGateInByDateRange(DateTime StartDate, DateTime EndDate)
        //{
        //    using (var context = new Logisticentities())
        //    {
        //        List<IGMContGateInOut> listIGMDetails = new List<IGMContGateInOut>();
        //        listIGMDetails = context.IGMContGateInOuts.Include("IGMImportDetail").Where(x => x.GateInDate >= StartDate && x.GateInDate <= EndDate).ToList();
        //        return listIGMDetails;
        //    }
        //}

        public string SetContGateInOutRefNo()
        {
            using (var context = new Logisticentities())
            {
                try
                {
                    string Refno;
                    long newRefNo;

                    var IGMmaxRef = context.IGMContGateInOuts.Max(x => x.RefNo);
                  
                    if (IGMmaxRef != 0)
                    {
                        newRefNo = Convert.ToInt64(IGMmaxRef) + 1;
                        if (newRefNo < 10)
                        {
                            Refno = newRefNo.ToString("00");
                        }
                        else
                        {
                            Refno = newRefNo.ToString();
                        }
                    }
                    else
                    {
                        Refno = "01";
                    }

                    return Refno;

                }
                catch (Exception exception)
                {
                    throw exception;
                }


                //try
                //{
                //    string Refno;
                //    var count = context.IGMContGateInOuts.Where(x => x.GateInDate.Value.Year == DateTime.Now.Year && x.GateInDate.Value.Month==DateTime.Now.Month).Count();
                //    if (count != 0)
                //    {
                //        count = count + 1;
                //        if (count < 10)
                //        {


                //            Refno = DateTime.Now.Year + DateTime.Now.ToString("MM") + count.ToString("00");

                //        }
                //        else
                //        {

                //            Refno = DateTime.Now.Year + DateTime.Now.ToString("MM") + count;

                //        }

                //    }
                //    else
                //    {
                //        Refno = DateTime.Now.Year + DateTime.Now.ToString("MM") + "01";
                //    }

                //    return Refno;

                //}
                //catch (Exception exception)
                //{
                //    throw exception;
                //}

            }

        }

        public IGMContGateInOut GetContGateInOutByIGMdetailsId(long IGMDetailsId)
        {

            using (var context = new Logisticentities( ))
            {

                try
                {
                    //var objGateInOut = context.IGMContGateInOuts.Include("IGMImportDetails").Where(x => x.IGMDetailsId == IGMDetailsId).SingleOrDefault();
                    var objGateInOut = context.IGMContGateInOuts.Include("IGMImportDetail").Where(x => x.IGMDetailsId == IGMDetailsId).SingleOrDefault();
                    return objGateInOut;

                }
                catch (Exception exception)
                {
                    throw exception;
                }

            }

        }

       
        //Save IGMContGateInOut object when Gate In complete
        public object SaveIGMContGateIn(IGMContGateInOut objGateInOut)
        {

            using (var context = new Logisticentities( ))
            {

                try
                {                   
                    context.IGMContGateInOuts.Add(objGateInOut);
                    var success = context.SaveChanges();
                    if (success > 0)
                    {
                        try
                        {
                            var IGMDetails = context.IGMImportDetails.Where(x => x.IGMDetailsId == objGateInOut.IGMDetailsId).SingleOrDefault();
                            if (IGMDetails != null)
                            {
                                context.IGMImportDetails.Attach(IGMDetails);
                                IGMDetails.InOutStatus = 1;
                                context.SaveChanges();

                            }
                        }
                        catch (DbEntityValidationException ex)
                        {
                            string errorMessages = string.Join("; ", ex.EntityValidationErrors.SelectMany(x => x.ValidationErrors).Select(x => x.PropertyName + " : " + x.ErrorMessage));
                            return errorMessages;
                        }                        
                    }
                    return "Data has been saved successfully.";
                }
                catch (DbEntityValidationException ex)
                {
                    string errorMessages = string.Join("; ", ex.EntityValidationErrors.SelectMany(x => x.ValidationErrors).Select(x => x.PropertyName + " : " + x.ErrorMessage));
                    return errorMessages;
                }

            }

        }


        //Save or rather update IGMContGateInOut object when Gate Out complete
        public object SaveIGMContGateOut(IGMContGateInOut objGateInOut)
        {

            using (var context = new Logisticentities( ))
            {

                try
                {
                    var objOldIGMGateInOut = context.IGMContGateInOuts.Where(x => x.IGMContGateInOutId == objGateInOut.IGMContGateInOutId).SingleOrDefault();
                    context.Entry(objOldIGMGateInOut).CurrentValues.SetValues(objGateInOut);


                    var IGMDetails = context.IGMImportDetails.Include("IGMImport").Where(x => x.IGMDetailsId == objGateInOut.IGMDetailsId).SingleOrDefault();
                    context.IGMImportDetails.Attach(IGMDetails);
                    //IGMDetails.InOutStatus = 2;

                        if (objGateInOut.DeliveryType == 1) //"Unstafing" Unstafing mean container unload and Gate in into csd module
                        {

                            IGMDetails.InOutStatus = 2;
                        try
                            {

                                CSDContGateInOut objCSDCont = new CSDContGateInOut();
                                objCSDCont.CustId = Convert.ToInt32(IGMDetails.IGMImport.CustomerId);
                                objCSDCont.RefNo = GetCSDRefNo(Convert.ToInt32(IGMDetails.IGMImport.CustomerId));
                                //objCSDCont.RefNo = objGateInOut.RefNo;
                                objCSDCont.IsFromIGMImport = true;
                                objCSDCont.ContNo = IGMDetails.ContainerNo.Trim();
                                objCSDCont.ContType = IGMDetails.TypeId;
                                objCSDCont.ContSize = IGMDetails.SizeId;
                                objCSDCont.ISO = IGMDetails.ISO.Trim();
                                objCSDCont.ImpVssl = IGMDetails.IGMImport.Vessel.VesselName.Trim();
                                objCSDCont.RotImp = IGMDetails.IGMImport.Rotation.Trim();
                                objCSDCont.TrailerIn = objGateInOut.TrailerId;
                                objCSDCont.TrailerInNo = objGateInOut.TrailerNumber;
                                objCSDCont.HaulierIn = 2; // N/A non applicable
                                objCSDCont.DateIn = objGateInOut.GateOutDate;
                                objCSDCont.ContInCondition = objGateInOut.GateOutCondition;
                                objCSDCont.DepotFrom = 79;                                
                                objCSDCont.RemarkIn = objGateInOut.RemarksOut.Trim();
                                objCSDCont.LoadEmptyStatus = 1;
                                objCSDCont.InOutStatus = 1;
                                context.CSDContGateInOuts.Add(objCSDCont);
                                context.SaveChanges();
                            }
                            catch (DbEntityValidationException ex)
                            {
                                string errorMessages = string.Join("; ", ex.EntityValidationErrors.SelectMany(x => x.ValidationErrors).Select(x => x.PropertyName + " : " + x.ErrorMessage));
                                return errorMessages;
                            }

                        }
                    if (objGateInOut.DeliveryType == 2)
                    {

                        IGMDetails.InOutStatus = 2;
                        try
                        {

                            CSDContGateInOut objCSDCont = new CSDContGateInOut();
                            objCSDCont.CustId = Convert.ToInt32(IGMDetails.IGMImport.CustomerId);
                            objCSDCont.RefNo = GetCSDRefNo(Convert.ToInt32(IGMDetails.IGMImport.CustomerId));
                            //objCSDCont.RefNo = objGateInOut.RefNo;
                            objCSDCont.IsFromIGMImport = true;
                            objCSDCont.ContNo = IGMDetails.ContainerNo.Trim();
                            objCSDCont.ContType = IGMDetails.TypeId;
                            objCSDCont.ContSize = IGMDetails.SizeId;
                            objCSDCont.ISO = IGMDetails.ISO.Trim();
                            objCSDCont.ImpVssl = IGMDetails.IGMImport.Vessel.VesselName.Trim();
                            objCSDCont.RotImp = IGMDetails.IGMImport.Rotation.Trim();
                            objCSDCont.TrailerIn = objGateInOut.TrailerId;
                            objCSDCont.TrailerInNo = objGateInOut.TrailerNumber;
                            objCSDCont.HaulierIn = 2; // N/A non applicable
                            objCSDCont.DateIn = objGateInOut.GateOutDate;
                            objCSDCont.ContInCondition = objGateInOut.GateOutCondition;
                            objCSDCont.DepotFrom = 79;
                            objCSDCont.RemarkIn = objGateInOut.RemarksOut.Trim();
                            objCSDCont.LoadEmptyStatus = 1;
                            objCSDCont.InOutStatus = 1;
                            //context.CSDContGateInOuts.Add(objCSDCont);
                            //context.SaveChanges();
                        }
                        catch (DbEntityValidationException ex)
                        {
                            string errorMessages = string.Join("; ", ex.EntityValidationErrors.SelectMany(x => x.ValidationErrors).Select(x => x.PropertyName + " : " + x.ErrorMessage));
                            return errorMessages;
                        }

                    }


                    context.SaveChanges();
                    return "Data has been saved successfully";
                }
                catch (DbEntityValidationException ex)
                {
                    string errorMessages = string.Join("; ", ex.EntityValidationErrors.SelectMany(x => x.ValidationErrors).Select(x => x.PropertyName + " : " + x.ErrorMessage));                    
                    return errorMessages;
                }
               
            }

        }


        //Update IGMContGateInOut object when field old value are changed 
        public object UpdateIGMContGateInOut(IGMContGateInOut objGateInOut)
        {

            using (var context = new Logisticentities( ))
            {
                try
                {
                    // This Below Code is before
                    //var objOldGateInOut = context.IGMContGateInOuts.Where(x => x.IGMContGateInOutId == objGateInOut.IGMContGateInOutId).SingleOrDefault();
                    //context.Entry(objOldGateInOut).CurrentValues.SetValues(objGateInOut);
                    //var success = context.SaveChanges();       
                    //return "Data has been updated successfully.";

                    var objOldGateInOut = context.IGMContGateInOuts.Where(x => x.IGMContGateInOutId == objGateInOut.IGMContGateInOutId).SingleOrDefault();
                    context.Entry(objOldGateInOut).CurrentValues.SetValues(objGateInOut);


                    var IGMDetails = context.IGMImportDetails.Include("IGMImport").Where(x => x.IGMDetailsId == objGateInOut.IGMDetailsId).SingleOrDefault();
                    context.IGMImportDetails.Attach(IGMDetails);
                    //IGMDetails.InOutStatus = 2;

                    if (objGateInOut.DeliveryType == 1)
                    {
                        IGMDetails.InOutStatus = 2;
                        try
                        {
                            CSDContGateInOut objCSDCont = new CSDContGateInOut();
                            objCSDCont.CustId = Convert.ToInt32(IGMDetails.IGMImport.CustomerId);
                            objCSDCont.RefNo = GetCSDRefNo(Convert.ToInt32(IGMDetails.IGMImport.CustomerId));
                            objCSDCont.RefNo = objGateInOut.RefNo;
                            objCSDCont.IsFromIGMImport = true;
                            objCSDCont.ContNo = IGMDetails.ContainerNo.Trim();
                            objCSDCont.ContType = IGMDetails.TypeId;
                            objCSDCont.ContSize = IGMDetails.SizeId;
                            objCSDCont.ISO = IGMDetails.ISO.Trim();
                            //objCSDCont.ImpVssl = IGMDetails.IGMImport.Vessel.VesselName.Trim();
                            objCSDCont.RotImp = IGMDetails.IGMImport.Rotation.Trim();
                            objCSDCont.TrailerIn = objGateInOut.TrailerId;
                            objCSDCont.TrailerInNo = objGateInOut.TrailerNumber;
                            objCSDCont.HaulierIn = 2; // N/A non applicable
                            objCSDCont.DateIn = DateTime.Now;
                            objCSDCont.ContInCondition = objGateInOut.GateOutCondition;
                            objCSDCont.DepotFrom = 79;
                            objCSDCont.RemarkIn = objGateInOut.RemarksOut.Trim();
                            objCSDCont.LoadEmptyStatus = 1;
                            objCSDCont.InOutStatus = 2;
                            context.CSDContGateInOuts.Add(objCSDCont); /// when the Container Delevery type is Unstafing then container will receive / In will happend in CSD container
                            context.SaveChanges();
                        }
                        catch (DbEntityValidationException ex)
                        {
                            string errorMessages = string.Join("; ", ex.EntityValidationErrors.SelectMany(x => x.ValidationErrors).Select(x => x.PropertyName + " : " + x.ErrorMessage));
                            return errorMessages;
                        }

                    }
                    if (objGateInOut.DeliveryType == 2)
                    {
                        IGMDetails.InOutStatus = 2;
                        try
                        {
                            CSDContGateInOut objCSDCont = new CSDContGateInOut();
                            objCSDCont.CustId = Convert.ToInt32(IGMDetails.IGMImport.CustomerId);
                            objCSDCont.RefNo = GetCSDRefNo(Convert.ToInt32(IGMDetails.IGMImport.CustomerId));
                            objCSDCont.RefNo = objGateInOut.RefNo;
                            objCSDCont.IsFromIGMImport = true;
                            objCSDCont.ContNo = IGMDetails.ContainerNo.Trim();
                            objCSDCont.ContType = IGMDetails.TypeId;
                            objCSDCont.ContSize = IGMDetails.SizeId;
                            objCSDCont.ISO = IGMDetails.ISO.Trim();
                            objCSDCont.ImpVssl = IGMDetails.IGMImport.Vessel.VesselName.Trim();
                            objCSDCont.RotImp = IGMDetails.IGMImport.Rotation.Trim();
                            objCSDCont.TrailerIn = objGateInOut.TrailerId;
                            objCSDCont.TrailerInNo = objGateInOut.TrailerNumber;
                            objCSDCont.HaulierIn = 2; // N/A non applicable
                            objCSDCont.DateIn = DateTime.Now;
                            objCSDCont.ContInCondition = objGateInOut.GateOutCondition;
                            objCSDCont.DepotFrom = 79;
                            objCSDCont.RemarkIn = objGateInOut.RemarksOut.Trim();
                            objCSDCont.LoadEmptyStatus = 1;
                            objCSDCont.InOutStatus = 2;
                            //context.CSDContGateInOuts.Add(objCSDCont);
                            //context.SaveChanges();
                        }
                        catch (DbEntityValidationException ex)
                        {
                            string errorMessages = string.Join("; ", ex.EntityValidationErrors.SelectMany(x => x.ValidationErrors).Select(x => x.PropertyName + " : " + x.ErrorMessage));
                            return errorMessages;
                        }

                    }

                    context.SaveChanges();
                    return "Data has been saved successfully";

                   
                }
                catch (DbEntityValidationException ex)
                {
                    string errorMessages = string.Join("; ", ex.EntityValidationErrors.SelectMany(x => x.ValidationErrors).Select(x => x.PropertyName + " : " + x.ErrorMessage));
                    return errorMessages;
                }

            }

        }


        public object DeleteIGMContGateInOut(IGMContGateInOut ContInOut)
        {

            using (var context = new Logisticentities())
            {

                try
                {
                    var objOldGateInOut = context.IGMContGateInOuts.Where(x => x.IGMContGateInOutId == ContInOut.IGMContGateInOutId).SingleOrDefault();
                    context.IGMContGateInOuts.Remove(objOldGateInOut);

                    var objIGMDetails = context.IGMImportDetails.Where(x => x.IGMDetailsId == ContInOut.IGMDetailsId).SingleOrDefault();
                    context.IGMImportDetails.Attach(objIGMDetails);
                    objIGMDetails.InOutStatus = 0;
                    context.SaveChanges();
                    return "Data has been deleted successfully.";
                }
                catch (DbEntityValidationException ex)
                {
                    string errorMessages = string.Join("; ", ex.EntityValidationErrors.SelectMany(x => x.ValidationErrors).Select(x => x.PropertyName + " : " + x.ErrorMessage));
                    return errorMessages;
                }

            }

        }
        public object DeleteIGMContGateOut(IGMContGateInOut objGateInOut)
        {

            using (var context = new Logisticentities())
            {

                try
                {
                    //var objOldGateInOut = context.IGMContGateInOuts.Where(x => x.IGMContGateInOutId == objGateInOut.IGMContGateInOutId).SingleOrDefault();
                    //context.IGMContGateInOuts.Attach(objOldGateInOut);
                    //context.SaveChanges();

                    var objOldGateInOut = context.IGMContGateInOuts.Where(x => x.IGMContGateInOutId == objGateInOut.IGMContGateInOutId).SingleOrDefault();
                    context.Entry(objOldGateInOut).CurrentValues.SetValues(objGateInOut);
                    context.SaveChanges();
                    var objIGMDetails = context.IGMImportDetails.Where(x => x.IGMDetailsId == objGateInOut.IGMDetailsId).SingleOrDefault();
                    context.IGMImportDetails.Attach(objIGMDetails);
                    objIGMDetails.InOutStatus = 1;
                    context.SaveChanges();
                    return "Data has been deleted successfully.";
                }
                catch (DbEntityValidationException ex)
                {
                    string errorMessages = string.Join("; ", ex.EntityValidationErrors.SelectMany(x => x.ValidationErrors).Select(x => x.PropertyName + " : " + x.ErrorMessage));
                    return errorMessages;
                }

            }

        }


        public List<SerachIGMGateInOutData_Result> SearchIGMGateInOutData(int searchBy, string searchText)
        {
            using (var context = new Logisticentities())
            {
                var listCSD = context.SerachIGMGateInOutData(searchBy, searchText).ToList();
                return listCSD;
            }
        }


        public long GetCSDRefNo( int custId)
        {

            using (var context = new Logisticentities())
            {
                try
                {

                    long newRefNo;                  

                    var maxRef = context.CSDContGateInOuts.Where(x => x.CustId == custId).Max(x => x.RefNo);

                    if (maxRef != 0)
                    {
                        newRefNo = Convert.ToInt64(maxRef) + 1;
                        
                    }
                    else
                    {
                        newRefNo = 1;
                    }

                    return newRefNo;
                }
                catch (Exception exception)
                {
                    throw exception;
                }

            }

        }

        #endregion
    }
}
