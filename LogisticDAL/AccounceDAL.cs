using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.Entity.Validation;
using System.Linq;
using System.Collections.Generic;

namespace LOGISTIC.DAL
{

    public class AccounceDAL
    {

        #region Voucher


        public List<AccVoucherType> GetAllVoucherType()
        {
            using (var context = new Logisticentities())
            {
                var objList = context.AccVoucherTypes.ToList();
                return objList;
            }
        }

        public List<VoucherMaster> GetAllVoucherMaster()
        {
            using (var context = new Logisticentities())
            {
                var objList = context.VoucherMasters.Include("VoucherDetails").Take(100).OrderByDescending(v=>v.VoucherMstrId).ToList();
                return objList;
            }
        }

        public int GetCurrentVoucherMasterSlNo()
        {
            using (var context = new Logisticentities())
            {
                var count = context.VoucherMasters.Count();
                return count;
            }
        }

        public Object InsertVoucher(VoucherMaster objVoucher)
        {

            using (var context = new Logisticentities())
            {

                try
                {
                    context.VoucherMasters.Add(objVoucher);
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

        public object UpdateVoucher(VoucherMaster objVoucher)
        {
            try
            {
                using (var context = new Logisticentities())
                {
                    var oldVoucherMaster = context.VoucherMasters.Include("VoucherDetails").Where(x => x.VoucherMstrId == objVoucher.VoucherMstrId).SingleOrDefault();

                    context.Entry(oldVoucherMaster).CurrentValues.SetValues(objVoucher);

                    foreach (var item in oldVoucherMaster.VoucherDetails.ToList())
                    {
                        //Delete Voucher Details that has been removed from Voucher Master

                        if (!objVoucher.VoucherDetails.Any(s => s.VoucherDtlsId == item.VoucherDtlsId))
                            context.VoucherDetails.Remove(item);
                    }
                    foreach (var item in objVoucher.VoucherDetails)
                    {

                        //New item ...so add this item
                        if (item.VoucherDtlsId == 0)
                        {
                            context.VoucherDetails.Add(item);
                        }
                        else
                        {
                            //Existing item ...then update this item
                            var oldIGMDetails = oldVoucherMaster.VoucherDetails.Where(s => s.VoucherDtlsId == item.VoucherDtlsId).SingleOrDefault();
                            context.Entry(oldIGMDetails).CurrentValues.SetValues(item);
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

        public object DeleteVoucher(VoucherMaster objVoucher)
        {

            using (var context = new Logisticentities())
            {
                try
                {

                    context.VoucherMasters.Remove(context.VoucherMasters.Single(x => x.VoucherMstrId == objVoucher.VoucherMstrId));
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

        #endregion


        #region Chart of Accounte

        public DataTable GetAllChartOfAccount()
        {
            string constring = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constring))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT *  FROM [Accounts].[ChartOfAccounts]", con))
                {
                    con.Open();
                    DataTable dt = new DataTable();
                    dt.Load(cmd.ExecuteReader());
                    con.Close();
                    return dt;
                }
            }
        }

        public List<ChartOfAccount> GetAllTransactionHead()
        {
            try
            {
                using (var context = new Logisticentities())
                {

                    var objList = context.ChartOfAccounts.Where(x => x.type == "Transaction Account").ToList();
                    return objList;
                }
            }
            catch (DbEntityValidationException ex)
            {
                string errorMessages = string.Join("; ", ex.EntityValidationErrors.SelectMany(x => x.ValidationErrors).Select(x => x.PropertyName + " : " + x.ErrorMessage));
                throw (ex);
            }

        }

        public object Insert(ChartOfAccount objCOA)
        {

            using (var context = new Logisticentities())
            {
                try
                {
                    context.ChartOfAccounts.Add(objCOA);
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

        public object Update(ChartOfAccount objCOA)
        {
            try
            {
                using (var context = new Logisticentities())
                {

                    var obj = context.ChartOfAccounts.Where(x => x.ID == objCOA.ID).SingleOrDefault();
                    context.Entry(obj).CurrentValues.SetValues(objCOA);
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

        public object Delete(int Id)
        {

            using (var context = new Logisticentities())
            {
                try
                {

                    context.ChartOfAccounts.Remove(context.ChartOfAccounts.Single(x => x.ID == Id));
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


        #endregion





    }
}