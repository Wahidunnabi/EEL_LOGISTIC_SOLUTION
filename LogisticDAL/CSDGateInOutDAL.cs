using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Entity.Validation;
using LOGISTIC.UserDefinedModel;

namespace LOGISTIC.CSD.DAL
{
   
    public class CSDGateInOutDAL
    {

        public object SetCSDRefNo(int custId)
        {

            using (var context = new Logisticentities())
            {
                try
                {
                    string refNo;
                    long newRefNo;

                    var customer = context.Customers.Where(c => c.CustomerId.Equals(custId)).SingleOrDefault();

                    var maxRef = context.CSDContGateInOuts.Where(x=>x.CustId==custId).Max(x => x.RefNo);
                   
                    if (maxRef != 0)
                    {
                        newRefNo = Convert.ToInt64(maxRef) + 1;
                        if (newRefNo < 10)
                        {
                            refNo = newRefNo.ToString("00");
                        }
                        else
                        {
                            refNo = newRefNo.ToString();
                        }

                    }
                    else
                    {
                        refNo = "01";
                    }

                    var result = new { CustomerName = customer.CustomerName, RefNo = refNo };
                    return result;
                }
                catch (Exception exception)
                {
                    throw exception;
                }
               
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



        public List<CSDContGateInOut> GetAllCSDData()
        {
            using (var context = new Logisticentities())
            {

                var Data = context.CSDContGateInOuts.OrderByDescending(x => x.ContainerGateEntryId).ToList();                                                                            
                return Data;
            }
        }

        
        #region Search CSD

        public CSDContGateInOut GetCSDById(int id)
        {
            using (var context = new Logisticentities())
            {
                var objCSD = context.CSDContGateInOuts
                    .Include("Customer")
                    .Include("ContainerSize")
                    .Include("ContainerType")
                    .Where(x => x.ContainerGateEntryId == id).SingleOrDefault();
                return objCSD;
            }
        }

        //return CSDContGateInOut objects where only Gate In is true....Gate Out not yet completed
        public List<CSDContGateInOut> GetAllCSDGateInByMLOId(int custId)
        {
            using (var context = new Logisticentities())
            {

                var Data = context.CSDContGateInOuts.Where(x => x.InOutStatus == 1 && x.CustId == custId).ToList();
                return Data;
            }
        }

        public CSDContGateInOut GetCSDByContNumber(string contNumber)
        {
            using (var context = new Logisticentities())
            {
                var objCSD = context.CSDContGateInOuts
                    .Include("Customer")
                    .Include("ContainerSize")
                    .Include("ContainerType")
                    .Where(x => x.ContNo == contNumber && x.InOutStatus == 1).SingleOrDefault();
                return objCSD;
            }
        }

        public List<SerachCSDGateInOutData_Result> SearchCSDGateInOutData(int searchBy, string searchText)
        {
            using (var context = new Logisticentities())
            {
                var listCSD = context.SerachCSDGateInOutData(searchBy, searchText).ToList();
                return listCSD;
            }
        }

        public List<SerachCSDGateInData_Result> SearchCSDGateInData(int searchBy, string searchText)
        {
            using (var context = new Logisticentities())
            {
                var listCSD = context.SerachCSDGateInData(searchBy, searchText).ToList();
                return listCSD;
            }
        }
        
        public string GetCustNameById(int custId)
        {

            using (var context = new Logisticentities())
            {
                try
                {
                   
                    var customer = context.Customers.Where(c => c.CustomerId == custId).SingleOrDefault();              

                    if (customer != null)
                    {
                        return customer.CustomerName;
                    }
                    else
                    {
                        return null;
                    }
                  
                }
                catch (Exception exception)
                {
                    throw exception;
                }

            }

        }

        public List<CSDContGateInOut> GetlistCSDByContNumber( string contNo)
        {
            using (var context = new Logisticentities())
            {

                var Data = context.CSDContGateInOuts
                           .Include("Customer")
                           .Include("ContainerSize")
                           .Include("ContainerType")
                           .Where(x => x.ContNo.Contains(contNo)).ToList();
                return Data;
            }
        }

        public List<CSDContGateInOut> GetListCSDByRefNumber(long refNo)
        {
            using (var context = new Logisticentities())
            {

                var Data = context.CSDContGateInOuts
                            .Include("Customer")
                            .Include("ContainerSize")
                            .Include("ContainerType")
                            .Where(x => x.RefNo == refNo).ToList();
                return Data;
            }
        }


        #endregion

        public List<TrailerNumber> GetAllTrailernumber(int trailerId)
        {
            using (var context = new Logisticentities())
            {

                var Data = context.TrailerNumbers.Where(x => x.TrailerId == trailerId).OrderBy(x => x.TrailerNumber1).ToList();
                return Data;
            }
        }


        public clsContainerHistory GetContainerHistory(long id, int inOutStatus)
        {
            using (var context = new Logisticentities())
            {
                clsContainerHistory obj = new clsContainerHistory();
                if (inOutStatus == 1)
                {
                     obj = (from GE in context.CSDContGateInOuts
                              // join C in context.Customers on GE.CustId equals C.CustomerId
                               join D in context.Depots on GE.DepotFrom equals D.DepotId                              
                               join H in context.Hauliers on GE.HaulierIn equals H.HaulierId                              
                               join Con in context.Conditions on GE.ContInCondition equals Con.ConditionId
                               join U in context.UserInfoes on GE.UserIdGateIn equals U.UserId                              
                               where GE.ContainerGateEntryId == id
                               select new clsContainerHistory()
                               {
                                  // Customer = C.CustomerCode,
                                   DateIn = GE.DateIn.Value,
                                   VesslIn = GE.ImpVssl,
                                   RotationIn = GE.RotImp,
                                   BroughtFrom = D.DepotName,
                                   HaulierIn = H.HaulierNo,
                                   TrailerIn = GE.TrailerInNo,
                                   ChallanIn = GE.ChallanNo,
                                   StatusIn = GE.ContInCondition,
                                   RemarkIn = GE.RemarkIn,
                                   UserGateIn = U.FirstName + " " + U.LastName,                                 

                               }).SingleOrDefault();
                }
                else
                {
                     obj = (from GE in context.CSDContGateInOuts
                               //join C in context.Customers on GE.CustId equals C.CustomerId
                               join D in context.Depots on GE.DepotFrom equals D.DepotId
                               join Do in context.Depots on GE.DepotTo equals Do.DepotId
                               join H in context.Hauliers on GE.HaulierIn equals H.HaulierId
                               join Ho in context.Hauliers on GE.HaulierOut equals Ho.HaulierId
                               join Con in context.Conditions on GE.ContInCondition equals Con.ConditionId
                               join U in context.UserInfoes on GE.UserIdGateIn equals U.UserId
                               join Uo in context.UserInfoes on GE.UserIdGateOut equals Uo.UserId
                               where GE.ContainerGateEntryId == id
                               select new clsContainerHistory()
                               {
                                   //Customer = C.CustomerCode,
                                   DateIn = GE.DateIn.Value,
                                   VesslIn = GE.ImpVssl,
                                   RotationIn = GE.RotImp,
                                   BroughtFrom = D.DepotName,
                                   HaulierIn = H.HaulierNo,
                                   TrailerIn = GE.TrailerInNo,
                                   ChallanIn = GE.ChallanNo,
                                   StatusIn = GE.ContInCondition,
                                   RemarkIn = GE.RemarkIn,
                                   UserGateIn = U.FirstName + " " + U.LastName,

                                   DateOut = GE.DateOut.Value,
                                   VesslOut = GE.ExpVssl,
                                   RotationOut = GE.RotExp,
                                   OutTo = Do.DepotName,
                                   HaulierOut = Ho.HaulierNo,
                                   TrailerOut = GE.TrailerOutNo,
                                   ChallanOut = GE.ChallanOut,
                                   StatusOut = GE.ContOutCondition,
                                   RemarkOut = GE.RemarkOut,
                                   UserGateOut = Uo.FirstName + " " + Uo.LastName,

                               }).SingleOrDefault();


                }

               

                var stuffingDetails = context.StuffingDetails.Where(x => x.CSDGateEntryId == id).FirstOrDefault();

                if (stuffingDetails != null)
                {

                    var obj2 = (from S in context.StuffingDetails
                                join C in context.Customers on S.CustId equals C.CustomerId
                                join L in context.Locations on S.Location equals L.LocationId
                                join Us in context.UserInfoes on S.StuffedById equals Us.UserId
                                where S.CSDGateEntryId == id
                                select new clsContainerHistory()
                                {
                                    Customer=C.CustomerCode,
                                    StuffingDate = S.StuffingDate,
                                    SealNo = S.SealNo,
                                    Location = L.LocationName,
                                    Shift = S.DayNightShift,
                                    TareWT = S.TareWT,
                                    UserStuffed = Us.FirstName + " " + Us.LastName,

                                }).FirstOrDefault();

                    if (obj2 != null)
                    {
                        obj.Customer = obj2.Customer;
                        obj.StuffingDate = obj2.StuffingDate;
                        obj.SealNo = obj2.SealNo;
                        obj.Location = obj2.Location;
                        obj.Shift = obj2.Shift;
                        obj.TareWT = obj2.TareWT;
                        obj.UserStuffed = obj2.UserStuffed;

                    }

                }


                return obj;



                //var obj = (from GE in context.CSDContGateInOuts
                //           join C in context.Customers on GE.CustId equals C.CustomerId
                //           join D in context.Depots on GE.DepotFrom equals D.DepotId
                //           join Do in context.Depots on GE.DepotTo equals Do.DepotId
                //           join H in context.Hauliers on GE.HaulierIn equals H.HaulierId
                //           join Ho in context.Hauliers on GE.HaulierOut equals Ho.HaulierId                           
                //           join Con in context.Conditions on GE.ContInCondition equals Con.ConditionId
                //           join U in context.UserInfoes on GE.UserIdGateIn equals U.UserId
                //           join Uo in context.UserInfoes on GE.UserIdGateOut equals Uo.UserId
                //           where GE.ContainerGateEntryId == id
                //           select new clsContainerHistory()
                //           {
                //               Customer = C.CustomerCode,
                //               DateIn = GE.DateIn.Value,
                //               VesslIn = GE.ImpVssl,
                //               RotationIn = GE.RotImp,
                //               BroughtFrom = D.DepotName,
                //               HaulierIn = H.HaulierNo,
                //               TrailerIn = GE.TrailerInNo,
                //               ChallanIn = GE.ChallanNo,
                //               StatusIn = GE.ContInCondition,
                //               RemarkIn = GE.RemarkIn,
                //               UserGateIn = U.FirstName + " " + U.LastName,

                //               DateOut = GE.DateOut.Value,
                //               VesslOut = GE.ExpVssl,
                //               RotationOut=GE.RotExp,
                //               OutTo= Do.DepotName,
                //               HaulierOut = Ho.HaulierNo,
                //               TrailerOut = GE.TrailerOutNo,
                //               ChallanOut = GE.ChallanOut,
                //               StatusOut = GE.ContOutCondition,
                //               RemarkOut = GE.RemarkOut,
                //               UserGateOut = Uo.FirstName + " " + Uo.LastName,

                //           }).SingleOrDefault();

                //var stuffingDetails = context.StuffingDetails.Where(x => x.CSDGateEntryId == id).FirstOrDefault();

                //if (stuffingDetails != null)
                //{

                //    var  obj2 = (from S in context.StuffingDetails                                 
                //                 join L in context.Locations on S.Location equals L.LocationId                                                          
                //               join Us in context.UserInfoes on S.StuffedById equals Us.UserId                              
                //               where S.CSDGateEntryId == id
                //               select new clsContainerHistory()
                //               {
                //                   StuffingDate = S.StuffingDate.Value,
                //                   SealNo = S.SealNo,
                //                   Location = L.LocationName,
                //                   Shift = S.DayNightShift,
                //                   TareWT = S.TareWT,
                //                   UserStuffed = Us.FirstName+" "+Us.LastName,                                   

                //               }).FirstOrDefault();

                //    if (obj2 != null)
                //    {
                //        obj.StuffingDate = obj2.StuffingDate;
                //        obj.SealNo = obj2.SealNo;
                //        obj.Location = obj2.Location;
                //        obj.Shift = obj2.Shift;
                //        obj.TareWT = obj2.TareWT;
                //        obj.UserStuffed = obj2.UserStuffed;

                //    }

                //}


                //return obj;

            }
        }

        public object Insert(CSDContGateInOut objCSD, long CSDUpcomingId)
        {
                        
            using (var context = new Logisticentities())
            {
                try
                { //check whether this container is in Gate-In stage or any Reference No of this MLO already exist
                    //var obj = context.CSDContGateInOuts.Where(c => c.CustId == objCSD.CustId && c.RefNo == objCSD.RefNo).Where(o=>o.ContNo== objCSD.ContNo && o.InOutStatus==1).FirstOrDefault();
                    var obj = context.CSDContGateInOuts.Where(o => o.ContNo == objCSD.ContNo && o.InOutStatus == 1).FirstOrDefault();
                    if (obj == null) //no object exist....add new object
                    {
                        try 
                        {
                            context.CSDContGateInOuts.Add(objCSD);
                            context.SaveChanges();
                            if (CSDUpcomingId > 0) // if CSDContGateInOut object is from CSDGateInUpcoming
                            {
                                try //delete CSDGateInUpcoming
                                {
                                    context.CSDGateInUPComings.Remove(context.CSDGateInUPComings.Single(x => x.Id == CSDUpcomingId));
                                    context.SaveChanges();

                                }
                                catch (DbEntityValidationException ex)
                                {
                                    string errorMessages = string.Join("; ", ex.EntityValidationErrors.SelectMany(x => x.ValidationErrors).Select(x => x.PropertyName + " : " + x.ErrorMessage));
                                    throw ex;
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
                    else  //object already exist in Gate-In stage....return
                    {
                      return "This container already Gate-In !!, or \n Reference number already exist.";
                    }
                }
                catch (DbEntityValidationException ex)
                {
                    string errorMessages = string.Join("; ", ex.EntityValidationErrors.SelectMany(x => x.ValidationErrors).Select(x => x.PropertyName + " : " + x.ErrorMessage));
                    return errorMessages;
                }
            }
        }


        public object InsertCSDList(List<CSDContGateInOut>  listCSD)
        {

            using (var context = new Logisticentities())
            {
                try
                {
                    foreach (CSDContGateInOut item in listCSD)
                    {
                        context.CSDContGateInOuts.Add(item);
                        context.SaveChanges();
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

        public object Update(CSDContGateInOut objGatEntry)
        {
            try
            {
                using (var context = new Logisticentities())
                {

                    var obj = context.CSDContGateInOuts.Where(x => x.ContainerGateEntryId == objGatEntry.ContainerGateEntryId).SingleOrDefault();
                    context.Entry(obj).CurrentValues.SetValues(objGatEntry);
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

        public object UpdateCSDList(List<CSDContGateInOut> listCSD)
        {
            using (var context = new Logisticentities())
            {
                try
                {
                    foreach (CSDContGateInOut item in listCSD)
                    {

                        var obj = context.CSDContGateInOuts.Where(x => x.ContainerGateEntryId == item.ContainerGateEntryId).SingleOrDefault();
                        context.Entry(obj).CurrentValues.SetValues(item);
                        context.SaveChanges();                       
                    }
                    return "Data has been updated successfully.";
                }
                catch (DbEntityValidationException ex)
                {
                    string errorMessages = string.Join("; ", ex.EntityValidationErrors.SelectMany(x => x.ValidationErrors).Select(x => x.PropertyName + " : " + x.ErrorMessage));
                    return errorMessages;
                }

            }               
        }

        public object Delete(long csdId )
        {
           
            using (var context = new Logisticentities())
            {
                try
                {
                    
                    context.CSDContGateInOuts.Remove(context.CSDContGateInOuts.Single(x => x.ContainerGateEntryId == csdId));
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

        public void DeleteUpComingContainer(long Id)
        {

            using (var context = new Logisticentities())
            {
                try
                {

                    context.CSDGateInUPComings.Remove(context.CSDGateInUPComings.Single(x => x.Id == Id));
                    context.SaveChanges();
                  
                }
                catch (DbEntityValidationException ex)
                {
                    string errorMessages = string.Join("; ", ex.EntityValidationErrors.SelectMany(x => x.ValidationErrors).Select(x => x.PropertyName + " : " + x.ErrorMessage));
                   throw ex;
                }

            }
        }

    }
}
