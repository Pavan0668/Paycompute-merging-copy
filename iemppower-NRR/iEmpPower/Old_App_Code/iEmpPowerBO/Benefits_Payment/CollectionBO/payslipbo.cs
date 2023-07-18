using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iEmpPower.Old_App_Code.iEmpPowerBO.Benefits_Payment.CollectionBO
{
    public class payslipbo
    {
        public payslipbo()
        {
        }

        public string Pernr { get; set; }
        public int? Year { get; set; }
        public string FilePath { get; set; }
        public int? Flag { get; set; }

        public int? Rownumber { get; set; }
        public string FileName { get; set; }
        public string Name { get; set; }
        
    }
}