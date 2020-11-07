using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Entity.Validation;

namespace LOGISTIC.Export.DAL
{
   
    public class CargoStuffingDAL
    {



        #region GET DATA

        public object GetCSDContInformation(int CSDGateEntryId)

        {

            using (var context = new Logisticentities())
            {
                var objCSD = context.CSDContGateInOuts                                         
                                         .Include("ContainerType")
                                         .Include("ContainerSize")
                                         .Where(x => x.ContainerGateEntryId == CSDGateEntryId).SingleOrDefault();


                if (objCSD != null)
                {
                    var result = new { RefNo = objCSD.RefNo, SizeId = objCSD.ContainerSize.ContainerSizeId, Size = objCSD.ContainerSize.ContainerSize1, Typeid = objCSD.ContainerType.ContainerTypeId, Type = objCSD.ContainerType.ContainerTypeName };
                    return result;
                }
                else
                {
                    return null;
                }

                return objCSD;
            }
        }

        public List<CargoRecieving> GetClientwiseAllCargoReceive(int clientId)
        {
            using (var context = new Logisticentities())
            {
                try
                {
                    var result = context.CargoRecievings.Where(x => x.CustId == clientId).OrderByDescending(x => x.CargoReceiveId).ToList();

                    return result;
                }
                catch (DbEntityValidationException ex)
                {
                    string errorMessages = string.Join("; ", ex.EntityValidationErrors.SelectMany(x => x.ValidationErrors).Select(x => x.PropertyName + " : " + x.ErrorMessage));
                    throw ex;
                }
            }

        }

        public List<CSDContGateInOut> GetClientwiseCSDContainer(int clientId)
        {
            using (var context = new Logisticentities())
            {
                try
                {
                    var result = context.CSDContGateInOuts.Where(x => x.CustId == clientId && x.InOutStatus == 1).ToList();

                    return result;
                }
                catch (DbEntityValidationException ex)
                {
                    string errorMessages = string.Join("; ", ex.EntityValidationErrors.SelectMany(x => x.ValidationErrors).Select(x => x.PropertyName + " : " + x.ErrorMessage));
                    throw ex;
                }
            }

        }

        public CargoRecieving GetCargoRecevingByRefNumber(string refNo)
        {
            using (var context = new Logisticentities())
            {
                try
                {
                    var result = context.CargoRecievings.Include("CargoDetails").Where(x => x.SLNo.Contains(refNo)).FirstOrDefault();
                    return result;
                }
                catch (DbEntityValidationException ex)
                {
                    string errorMessages = string.Join("; ", ex.EntityValidationErrors.SelectMany(x => x.ValidationErrors).Select(x => x.PropertyName + " : " + x.ErrorMessage));
                    throw ex;
                }
            }

        }

        public CargoRecieving GetCargoRecevingByCargoReceiveId(int CargoReceiveId)
        {
            using (var context = new Logisticentities())
            {
                try
                {
                    var result = context.CargoRecievings.Include("CargoDetails").Where(x => x.CargoReceiveId == CargoReceiveId).SingleOrDefault();
                    return result;
                }
                catch (DbEntityValidationException ex)
                {
                    string errorMessages = string.Join("; ", ex.EntityValidationErrors.SelectMany(x => x.ValidationErrors).Select(x => x.PropertyName + " : " + x.ErrorMessage));
                    throw ex;
                }
            }

        }

        public StuffingDetail GetStuffingDetailsByCargoDetailsId(long cargoDetailsId)
        {
            using (var context = new Logisticentities())
            {
                try
                {
                    var result = context.StuffingDetails.Where(x => x.CargoDetailsId == cargoDetailsId).SingleOrDefault();
                    return result;
                }
                catch (DbEntityValidationException ex)
                {
                    string errorMessages = string.Join("; ", ex.EntityValidationErrors.SelectMany(x => x.ValidationErrors).Select(x => x.PropertyName + " : " + x.ErrorMessage));
                    throw ex;
                }
            }

        }


        #endregion



        public object Insert(StuffingDetail objStuffing)
        {
                        
            using (var context = new Logisticentities())
            {             
                try
                {
                    context.StuffingDetails.Add(objStuffing);
                    var status = context.SaveChanges();


                    if (status > 0)
                    {
                        try
                        {
                            CargoDetail objCD = context.CargoDetails.Where(x => x.CargoDetailsId == objStuffing.CargoDetailsId).SingleOrDefault();
                            objCD.IsStuffed = true;


                            CSDContGateInOut objCSD = context.CSDContGateInOuts.Where(x => x.ContainerGateEntryId == objStuffing.CSDGateEntryId).SingleOrDefault();
                            objCSD.LoadEmptyStatus = 2; // Loaded =2 
                            objCSD.ContInCondition = 1; // Sound = 1
                            objCSD.RemarkIn = "Stuffed container for export.";
                            context.SaveChanges();

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


        public object Update(StuffingDetail objStuffing)
        {
            try
            {
                using (var context = new Logisticentities())
                {
                    StuffingDetail OldStuffing = context.StuffingDetails.Where(x => x.StuffingDetailsId == objStuffing.StuffingDetailsId).SingleOrDefault();
                    var OldCSDGateInId = OldStuffing.CSDGateEntryId;
                    context.Entry(OldStuffing).CurrentValues.SetValues(objStuffing);                   
                    

                    if (OldCSDGateInId != objStuffing.CSDGateEntryId)
                    {
                        try
                        {
                           
                            CSDContGateInOut objCSD = context.CSDContGateInOuts.Where(x => x.ContainerGateEntryId == objStuffing.CSDGateEntryId).SingleOrDefault();
                            objCSD.LoadEmptyStatus = 2;


                            CSDContGateInOut objCSD2 = context.CSDContGateInOuts.Where(x => x.ContainerGateEntryId == OldCSDGateInId).SingleOrDefault();
                            objCSD2.LoadEmptyStatus = 1;
                           // context.SaveChanges();

                        }
                        catch (DbEntityValidationException ex)
                        {
                            string errorMessages = string.Join("; ", ex.EntityValidationErrors.SelectMany(x => x.ValidationErrors).Select(x => x.PropertyName + " : " + x.ErrorMessage));
                            return errorMessages;
                        }

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

        public bool CheckDuplicateBookingNo(string bookingNo)
        {
            try
            {
                using (var context = new Logisticentities())
                {
                    StuffingDetail objStuffing = context.StuffingDetails.Where(x => x.BookingNo == bookingNo).FirstOrDefault();
                    if (objStuffing == null)
                    {
                        return false;
                    }
                   
                    return true;
                }
            }
            catch (DbEntityValidationException ex)
            {
                string errorMessages = string.Join("; ", ex.EntityValidationErrors.SelectMany(x => x.ValidationErrors).Select(x => x.PropertyName + " : " + x.ErrorMessage));
                throw ex;
            }

        }

        public object Delete(CargoRecieving objCargoReceiving)
        {

            using (var context = new Logisticentities())
            {
                try
                {

                    context.CargoRecievings.Remove(context.CargoRecievings.Single(x => x.CargoReceiveId == objCargoReceiving.CargoReceiveId));
                    context.SaveChanges();
                    return "Data has been deleted successfully. !!";

                }
                catch (DbEntityValidationException ex)
                {
                    string errorMessages = string.Join("; ", ex.EntityValidationErrors.SelectMany(x => x.ValidationErrors).Select(x => x.PropertyName + " : " + x.ErrorMessage));
                    return errorMessages;
                }

            }
        }

    }
}
