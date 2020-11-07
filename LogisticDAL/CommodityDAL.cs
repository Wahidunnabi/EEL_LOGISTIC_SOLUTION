using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;
using System.Data;


namespace LOGISTIC.DAL
{
   public class CommodityDAL
    {
       
        public List<Commodity> Getall()
        {
            using (var context = new Logisticentities( ))
            {               
                var Data = context.Commodities.OrderBy(x=>x.CommodityName).ToList();
                return Data;
            }
        }

        public Commodity GetCommodityDetailsByID(int comID)
        {
            using (var context = new Logisticentities( ))
            {

                var Data = context.Commodities.Where(obj => obj.CommodityId == comID).SingleOrDefault();
                return Data;
            }
        }

        public int Insert(Commodity objCom)
        {

            using (var context = new Logisticentities( ))
            {

                try
                {
                    context.Commodities.Add(objCom);
                    context.SaveChanges();
                    return 1;
                }
                catch (Exception exception)
                {
                    throw exception;                  
                }

            }
        }

        public Commodity Update(int id, Commodity objCom)
        {
            try
            {
                using (var context = new Logisticentities( ))
                {
                    var obj = context.Commodities.Where(x => x.CommodityId == objCom.CommodityId).SingleOrDefault();
                    context.Entry(obj).CurrentValues.SetValues(objCom);                   
                    context.SaveChanges();
                    return objCom;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {

            }

        }

        public void Delete(int comID)
        {

            using (var context = new Logisticentities( ))
            {
                try
                {

                    context.Commodities.Remove(context.Commodities.Single(x => x.CommodityId == comID));
                    context.SaveChanges();

                }
                catch (OptimisticConcurrencyException ex)
                {
                    throw ex;
                }

            }
        }

    }
}
