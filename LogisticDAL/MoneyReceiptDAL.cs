using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Entity.Validation;

namespace LOGISTIC.CSD.DAL
{

    public class MoneyReceiptDAL
    {

        public MoneyReceipt GetMoneyReceiptById(int id)
        {

            using (var context = new Logisticentities())
            {
                try
                {
                    var objMR = context.MoneyReceipts
                               .Include("MoneyReceiptDetails")
                               .Where(m=>m.ID== id)
                               .SingleOrDefault();   
                                    
                    return objMR;
                }
                catch (Exception exception)
                {
                    throw exception;
                }

            }

        }
        public List<Service> GetAllServices()
        {

            using (var context = new Logisticentities())
            {
                try
                {

                    var listService = context.Services.ToList();
                    return listService;
                }
                catch (Exception exception)
                {
                    throw exception;
                }

            }

        }
        public int  GetMoneyReceiptSLNo()
        {

            using (var context = new Logisticentities())
            {
                try
                {
                  
                    var count = context.MoneyReceipts.Select(x => x.ID).DefaultIfEmpty(0).Max();
                    count = count + 1;
                    return count;
                }
                catch (Exception exception)
                {
                    throw exception;
                }

            }

        }
        public object GetMLOAgentData(int custId)
        {

            using (var context = new Logisticentities())
            {
                try
                {
              
                    var customer = context.Customers.Where(c => c.CustomerId.Equals(custId)).SingleOrDefault();
                    var objAgent = context.Agents.Where(x => x.AgentId == customer.AgentId).FirstOrDefault();
                    var result = new { CustomerName = customer.CustomerName, AgentName = objAgent.AgentName };
                    return result;

                }
                catch (Exception exception)
                {
                    throw exception;
                }

            }

        }
        public Object Insert(MoneyReceipt objMoneyRecept)
        {

            using (var context = new Logisticentities())
            {

                try
                {
                    context.MoneyReceipts.Add(objMoneyRecept);
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
        public object Update(MoneyReceipt objMoneyRecept)
        {
            try
            {
                using (var context = new Logisticentities())
                {
                    var oldMRObj = context.MoneyReceipts.Include("MoneyReceiptDetails").Where(x => x.ID == objMoneyRecept.ID).SingleOrDefault();

                    context.Entry(oldMRObj).CurrentValues.SetValues(objMoneyRecept);

                    foreach (var item in oldMRObj.MoneyReceiptDetails.ToList())
                    {
                        //Delete IGM Details that has been removed from IGM Import

                        if (!objMoneyRecept.MoneyReceiptDetails.Any(s => s.ID == item.ID))
                            context.MoneyReceiptDetails.Remove(item);
                    }
                    foreach (var item in objMoneyRecept.MoneyReceiptDetails)
                    {

                        //New item ...so add this item
                        if (item.ID == 0)
                        {
                            context.MoneyReceiptDetails.Add(item);
                        }
                        else
                        {
                            //Existing item ...then update this item
                            var oldMRDetails = oldMRObj.MoneyReceiptDetails.Where(s => s.ID == item.ID).SingleOrDefault();
                            context.Entry(oldMRDetails).CurrentValues.SetValues(item);
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
        public object Delete(int MRId)
        {

            using (var context = new Logisticentities())
            {
                try
                {

                    context.MoneyReceipts.Remove(context.MoneyReceipts.Single(x => x.ID == MRId));
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