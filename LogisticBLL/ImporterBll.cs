using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LOGISTIC.DAL;

namespace LOGISTIC.BLL
{
   public class ImporterBll
    {

       ImporterDal objDal = new ImporterDal();
      
       public List<Importer> Getall()
       {
           List<Importer> objlist = new List<Importer>();          
           objlist = objDal.Getall();
           return objlist;
       }

       public Importer GetImporterById( int imptId)
       {
           Importer objImporter= new Importer();
           objImporter = objDal.GetImporterById(imptId);
           return objImporter;
       }

       public object Insert(Importer objImpt)
       {
           var status = objDal.Insert(objImpt);
           return status;
       }

       public object Update(Importer objImpt)
       {
           var status = objDal.Update(objImpt);
           return status;
       }

       public object Delete(int imptId)
       {
            var status = objDal.Delete(imptId);
            return status;
        }
    }
}
