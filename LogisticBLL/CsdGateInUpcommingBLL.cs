using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LOGISTIC.DAL;
using System.Data;

namespace LOGISTIC.BLL
{
    public class CsdGateInUpcommingBLL
    {
        private CsdGateInUpcommingDAL objDal = new CsdGateInUpcommingDAL();

        private List<CSDGateInUPComing> objlist = new List<CSDGateInUPComing>();
        private List<Customer> objMLolist = new List<Customer>();
        private List<ContainerSize> objSizelist = new List<ContainerSize>();
        private List<ContainerType> objTypelist = new List<ContainerType>();
        private CustomerDal objMLODal = new CustomerDal();
        private ContainerTypeDal objTypeDal = new ContainerTypeDal();
        private ContainerSizeDal objSizeDal = new ContainerSizeDal();
        public List<CSDGateInUPComing> Getall()
        {
            
            objlist = objDal.Getall();
            return objlist;
        }
        
        public object CsdGateInUpcommingInsert(DataSet dataset)
        {
            try
            {
                object status = null;
                objMLolist = objMLODal.Getall();
                objTypelist = objTypeDal.Getall();
                objSizelist = objSizeDal.Getall();
                List<CSDGateInUPComing> listUpcomingCont = new List<CSDGateInUPComing>();
                
                foreach (DataRow row in dataset.Tables[0].Rows)
                {
                    CSDGateInUPComing EmptyContainer = new CSDGateInUPComing();

                    String mlo = (row["MLO Code"]).ToString();
                    Customer objcus = objMLolist.Find(x => x.CustomerCode.Trim() == mlo.Trim());
                    EmptyContainer.MLOID = objcus.CustomerId;
                    EmptyContainer.MLOCode = (row["MLO Code"]).ToString();

                    EmptyContainer.ContainerNo = (row["Container Number"]).ToString();

                    string Typ = (row["Type"]).ToString();
                    ContainerType objTyp = objTypelist.Find(x => x.ContainerTypeName.Trim() == Typ.Trim());
                    EmptyContainer.Type = (row["Type"]).ToString();
                    EmptyContainer.TypeId = objTyp.ContainerTypeId;


                    EmptyContainer.ImportVasselName = (row["Import Vassel"]).ToString().Trim();
                    EmptyContainer.RotationNumber = (row["Reg No"]).ToString().Trim();



                    decimal size = Convert.ToDecimal(row["Size"]);
                    ContainerSize objSize = objSizelist.Find(x => x.ContainerSize1 == size);
                    EmptyContainer.SizeName = (row["Size"]).ToString();
                    EmptyContainer.SizeId = objSize.ContainerSizeId;

                    var result = ValidateUpcomingContainer(EmptyContainer);

                    if (result != "")
                    {
                        listUpcomingCont.Clear();
                        return result;
                    }
                    else
                    {
                        listUpcomingCont.Add(EmptyContainer);
                    }

                }

                status = objDal.Insert(listUpcomingCont);
                return status;

            }
            catch (Exception ex)
            {             
                return ex.ToString();
            }
            
            
        }


        private string ValidateUpcomingContainer(CSDGateInUPComing UpcomingContainer)
        {
            var errMessage = "";

            if (UpcomingContainer.MLOID == 0 || UpcomingContainer.MLOID == null)
            {
                errMessage = errMessage + "* MLO code does not match !!\n";
                
            }
            if (UpcomingContainer.ContainerNo == null || UpcomingContainer.ContainerNo.Length < 11)
            {
                errMessage = errMessage + "* Container number can't be null or less than 11 digit !!\n";             

            }
            if (UpcomingContainer.SizeId == 0 || UpcomingContainer.SizeId == null)
            {
                errMessage = errMessage + "* Container size does not match !!\n";

            }
            if (UpcomingContainer.TypeId == 0 || UpcomingContainer.TypeId == null)
            {
                errMessage = errMessage + "* Container type does not match !!\n";

            }
            return errMessage;
          
        }


        public object Update(CSDGateInUPComing objUpcoming)
        {

            var status = objDal.Update(objUpcoming);
            return status;
        }

        public object Delete(int Id)
        {
            var status = objDal.Delete(Id);
            return status;
        }

    }
}
