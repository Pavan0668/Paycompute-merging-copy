using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

//namespace iEmpPower.Old_App_Code.iEmpPowerBO.User_Account
//{
public class Announcementbo
{
    public Announcementbo()
    {
           
    }

    public int? AID { get; set; }
    public string ATITLE { get; set; } 
    public string ADESC { get; set; }
    public DateTime? ADATE { get; set; }
    public DateTime? TOADATE { get; set; }
    public string ATIME { get; set; }
    public string APLACE { get; set; }
    public string CREATED_BY { get; set; }
    public DateTime? CREATED_ON { get; set; }
    public string UPDATED_BY { get; set; }
    public DateTime? UPDATED_ON { get; set; }
    public int? RecordCnt { get; set; }
    public string ENAME { get; set; }

}
//}