using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Entity.Validation;

namespace LOGISTIC.DAL
{
    public class ChargeSetupDAL
    {


        //public List<ChartOfService> Getall()
        //{
        //    using (var context = new Logisticentities( ))
        //    {
        //        var objService = context.ChartOfServices.Include("ChartOfServiceCategory").OrderBy(x=>x.ServiceName).ToList();
        //        return objService;
        //    }
        //}

        public List<ChartOfService> GetallParent()
        {
            using (var context = new Logisticentities())
            {
                var objService = context.ChartOfServices.Where(x => x.IsTransaction == false && x.IsActive == true).OrderBy(x => x.ServiceName).ToList();
                return objService;
            }
        }

        public List<ChartOfService> GetallChildByParentId( int id)
        {
            using (var context = new Logisticentities())
            {
                var objService = context.ChartOfServices.Where(x => x.ParentId == id).OrderBy(x => x.ServiceName).ToList();
                return objService;
            }
        }

        public string GetClientNameById(int Id)
        {
            using (var context = new Logisticentities())
            {

                var Data = context.Customers.Where(c => c.CustomerId == Id).SingleOrDefault();
                return Data.CustomerName;

            }
        }


        public List<ClientBillSetup> GetAllBillSetupByClientId(int Id)
        {
            using (var context = new Logisticentities())
            {

                var listSetup = context.ClientBillSetups
                               .Include("ChartOfService")
                               .Include("ContainerSize")
                               .Where(c => c.CustId == Id).OrderBy(x=>x.ServiceId).ThenBy(x=>x.SizeId).ToList();
                return listSetup;

            }
        }

        //public List<ChartOfServiceCategory> GetAllServiceCategory()
        //{
        //    using (var context = new Logisticentities( ))
        //    {

        //           var categoryList = context.ChartOfServiceCategories.OrderBy(x => x.CategoryName).ToList();
        //           return categoryList;

        //    }
        //}

        public object Insert(ClientBillSetup objbillSetup)
        {

            using (var context = new Logisticentities())
            {

                try
                {
                    context.ClientBillSetups.Add(objbillSetup);
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

        public object Update(ClientBillSetup objbillSetup)
        {
            using (var context = new Logisticentities())
            {
                try
                {
                    var obj = context.ClientBillSetups.Where(x => x.BillSetupId == objbillSetup.BillSetupId).SingleOrDefault();
                    context.Entry(obj).CurrentValues.SetValues(objbillSetup);
                    context.SaveChanges();
                    return "Data has been updated successfully.";

                }
                catch (DbEntityValidationException ex)
                {
                    string errorMessages = string.Join("; ", ex.EntityValidationErrors.SelectMany(x => x.ValidationErrors).Select(x => x.PropertyName + " : " + x.ErrorMessage));
                    return errorMessages;
                }

            }

        }

        public object Delete(int billSetupId)
        {

            using (var context = new Logisticentities())
            {
                try
                {

                    context.ClientBillSetups.Remove(context.ClientBillSetups.Single(x => x.BillSetupId == billSetupId));
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
