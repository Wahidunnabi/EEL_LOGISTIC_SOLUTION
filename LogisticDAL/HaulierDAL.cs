using System;
using System.Linq;
using System.Data;
using System.Collections.Generic;

namespace LOGISTIC
{   
    public class HaulierDAL
    {

        public List<Haulier> GetAll()
        {
            using (var context = new Logisticentities())
            {

                var Data = context.Hauliers.OrderBy(x=>x.HaulierNo).ToList();
                return Data;
            }
        }
        public Haulier GetHaulierById(int hlrId)
        {
            using (var context = new Logisticentities())
            {

                var Data = context.Hauliers.Where(t => t.HaulierId == hlrId).SingleOrDefault();
                return Data;
            }
        }

        public int Insert(Haulier objHaulier)
        {

            using (var context = new Logisticentities())
            {

                try
                {
                    context.Hauliers.Add(objHaulier);
                    context.SaveChanges();
                    return 1;
                }
                catch (Exception exception)
                {

                    throw exception.InnerException;
                }

            }
        }

        public int Update(Haulier objHaulier)
        {
            try
            {
                using (var context = new Logisticentities())
                {
                    var obj = context.Hauliers.Where(x => x.HaulierId == objHaulier.HaulierId).SingleOrDefault();
                    context.Entry(obj).CurrentValues.SetValues(objHaulier);
                    context.SaveChanges();
                    return 1;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }           
        }

        public void Delete(int hlrId)
        {


            using (var context = new Logisticentities())
            {
                try
                {
                    context.Hauliers.Remove(context.Hauliers.Single(x => x.HaulierId == hlrId));
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
