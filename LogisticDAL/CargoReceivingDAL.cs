using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Entity.Validation;
using System.Text.RegularExpressions;

namespace LOGISTIC.Export.DAL
{
   
    public class CargoReceivingDAL
    {
        public string GetCustSLNo(int CustId,DateTime Dat)
        { 

            using (var context = new Logisticentities())
            {
                string SL;
                var customer = context.Customers.Where(c => c.CustomerId.Equals(CustId)).SingleOrDefault();
                var SLNo = context.CargoRecievings.Where(x => x.CustId == CustId && x.ReceivingDate.Value.Year == Dat.Year).Max(x=>x.SLNo);
                /// here Yearly base Sl Calculaton.
                if (SLNo != null)
                {
                    string value = Regex.Replace(SLNo, "[A-Za-z ]", "");
                    value = value.Substring(4);
                    int currentSL = Convert.ToInt32(value);
                    int newSL = currentSL + 1;
                    if (newSL < 10)
                    {
                        SL = "0" + newSL.ToString();

                    }
                    else
                    {
                        SL =newSL.ToString();
                    }
                    string yy = Dat.Year.ToString("yy");
                    string serialNo = customer.CustomerCode.Trim()+ Dat.ToString("yy") + Dat.ToString("MM") + SL;
                    
                    return serialNo;

                }
                else
                {
                    string serialNo = customer.CustomerCode.Trim() + Dat.ToString("yy") + Dat.ToString("MM") + "01";
                    return serialNo;
                }
                // var SLNo = context.CargoRecievings.Where(c => c.CustId.Equals(CustId)).Count();
                //SLNo = SLNo + 1;
                //var result =customer.CustomerCode.Trim() + SLNo;
                //return result;
            }

        }


        public List<CargoRecieving> GetCargoReceivingList(int custId)
        {
            using (var context = new Logisticentities())
            {
                var listCR = context.CargoRecievings.Where(x=>x.CustId == custId).OrderByDescending(x => x.CargoReceiveId).Take(50).ToList();
                return listCR;
            }
        }
       
        public CargoRecieving GetCRById( int id)
        {
            using (var context = new Logisticentities())
            {
                var objCR = context.CargoRecievings
                    .Include("CargoDetails")
                    .Where(x => x.CargoReceiveId==id).SingleOrDefault();
                return objCR;
            }
        }
      

        public object Insert(CargoRecieving objCargoReceiving)
        {
                        
            using (var context = new Logisticentities())
            {             
                try
                {
                    context.CargoRecievings.Add(objCargoReceiving);
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

        public object Update(CargoRecieving updatedCR)
        {
            try
            {
                using (var context = new Logisticentities())
                {
                    var oldCRObj = context.CargoRecievings.Include("CargoDetails").Where(x => x.CargoReceiveId == updatedCR.CargoReceiveId).SingleOrDefault();

                    context.Entry(oldCRObj).CurrentValues.SetValues(updatedCR);

                    foreach (var item in oldCRObj.CargoDetails.ToList())
                    {
                        //Delete IGM Details that has been removed from IGM Import

                        if (!updatedCR.CargoDetails.Any(s => s.CargoDetailsId == item.CargoDetailsId))
                            context.CargoDetails.Remove(item);
                    }
                    foreach (var item in updatedCR.CargoDetails)
                    {

                        //New item ...so add this item
                        if (item.CargoDetailsId == 0)
                        {
                            context.CargoDetails.Add(item);
                        }
                        else
                        {
                            //Existing item ...then update this item
                            var oldCRDetails = oldCRObj.CargoDetails.Where(s => s.CargoDetailsId == item.CargoDetailsId).SingleOrDefault();
                            context.Entry(oldCRDetails).CurrentValues.SetValues(item);
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
