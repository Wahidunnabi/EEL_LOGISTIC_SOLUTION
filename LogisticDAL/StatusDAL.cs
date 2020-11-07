using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LOGISTIC
{
    
    public class StatusDAL
    {
       // private int success;
        public List<Status > GetAllStatus()
        {
            using (var context = new Logisticentities())
            {

                //var Data = from depot in context.Depots.Include("UserInfo").Include("")
                var Data = from status in context.Status
                           //.Include("UserInfo")
                           //.Include("CompanyInfo")
                           orderby status.StatusName
                           select status;
                return Data.ToList();
            }
        }
        //public Trailer Insert(Trailer objTraile)
        //{
        //    //var connectionString = ConfigurationManager.ConnectionStrings["Logisticentities"].ConnectionString;

        //    if (objTraile == null) return null;

        //    using (var context = new Logisticentities( ))
        //    {
        //        //if (objdpot.DepotId >= 1)
        //        //{
        //        //    throw new FormatException();
        //        //}
        //        try
        //        {
        //            context.Trailers.AddObject(objTraile);
        //            success = context.SaveChanges(SaveOptions.DetectChangesBeforeSave);
        //        }
        //        catch (Exception exception)
        //        {
        //            //return null;
        //            //String innerMessage = (exception.InnerException != null) ? exception.InnerException.Message: "";
        //            //innerMessage.ToString();
        //            throw exception.InnerException;
        //        }

        //        return objTraile;
        //    }
        //}
        //public Trailer Update(int id, Trailer objTrailer)
        //{
        //    try
        //    {
        //        using (var context = new Logisticentities( ))
        //        {
        //            context.Trailers.Attach(objTrailer);
        //            context.DetectChanges();
        //            context.ObjectStateManager.ChangeObjectState(objTrailer, EntityState.Modified);
        //            success = context.SaveChanges();
        //            return objTrailer;
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //    finally
        //    {

        //    }

        //}
        //public object Delete(int trailerId, Trailer objTrailer)
        //{
        //    int success;
        //    EntityKey productKey = new EntityKey("Logisticentities.Trailers", "DepotID", trailerId);
        //    if (string.IsNullOrEmpty(objTrailer.TrailerName) || string.IsNullOrEmpty(objTrailer.TrailerId.ToString()))
        //    {
        //        return false;
        //    }

        //    using (var context = new Logisticentities( ))
        //    {
        //        try
        //        {
        //            //context.Depots.DeleteObject(context.BankBranches.Single(x => x.BankId == entityObj.Id));
        //            context.Trailers.DeleteObject(context.Trailers.Single(x => x.TrailerId == trailerId));
        //            success = context.SaveChanges();

        //        }
        //        catch (OptimisticConcurrencyException ex)
        //        {
        //            throw new InvalidOperationException(string.Format(
        //                "The product with an ID of '{0}' could not be deleted.\n"
        //                + "Make sure that any related objects are already deleted.\n",
        //                productKey.EntityKeyValues[0].Value), ex);
        //        }
        //        //catch (Exception)
        //        //{
        //        //    return false;
        //        //}
        //        //if (context.TryGetObjectByKey(DepotID, out objdpot))
        //        //{
        //        //    try
        //        //    {
        //        //        // Delete the object with the specified key
        //        //        // and save changes to delete the row from the data source.
        //        //        context.DeleteObject(deletedProduct);
        //        //        context.SaveChanges();
        //        //    }
        //        //    catch (OptimisticConcurrencyException ex)
        //        //    {
        //        //        throw new InvalidOperationException(string.Format(
        //        //            "The product with an ID of '{0}' could not be deleted.\n"
        //        //            + "Make sure that any related objects are already deleted.\n",
        //        //            productKey.EntityKeyValues[0].Value), ex);
        //        //    }
        //        //}
        //        return success; ;
        //    }
        //}

    }
}
