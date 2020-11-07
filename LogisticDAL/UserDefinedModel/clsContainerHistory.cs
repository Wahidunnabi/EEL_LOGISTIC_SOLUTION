using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LOGISTIC.UserDefinedModel
{
   public class clsContainerHistory
    {

        public string Customer { get; set; }            
        public string VesslIn { get; set; }
        public string VesslOut { get; set; }
        public string RotationIn { get; set; }
        public string RotationOut { get; set; }
        public string ChallanIn { get; set; }
        public string ChallanOut { get; set; }
        public string TrailerIn { get; set; }
        public string TrailerOut { get; set; }
        public string HaulierIn { get; set; }
        public string HaulierOut { get; set; }
        public string OutTo { get; set; }
        public string BroughtFrom { get; set; }
        public string UserGateOut { get; set; }
        public string UserGateIn { get; set; }
        public DateTime DateIn { get; set; }
        public DateTime DateOut { get; set; }
        public int? StatusIn { get; set; }
        public int? StatusOut { get; set; }
        public string RemarkIn { get; set; }
        public string RemarkOut { get; set; }

        public string BillTo { get; set; }
        public DateTime StuffingDate { get; set; }
        public string SealNo { get; set; }
        public string Location { get; set; }
        public int? Shift { get; set; }
        public int? TareWT { get; set; }
        public DateTime PlugIn { get; set; }
        public string LTemp { get; set; }
        public string RemarkStuffing { get; set; }
        public string UserStuffed { get; set; }

    }
}
