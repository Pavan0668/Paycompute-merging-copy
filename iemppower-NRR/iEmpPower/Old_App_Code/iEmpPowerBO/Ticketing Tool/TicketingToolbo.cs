using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

//namespace iEmpPower.Old_App_Code.iEmpPowerBO.Ticketing_Tool
//{
public class TicketingToolbo
{
    public TicketingToolbo()
    {

    }


    public int? StatusID { get; set; }
    public string StatusTxt { get; set; }

    public int? SearchStatusID { get; set; }
    public string SearchStatusTxt { get; set; } 

    public int? CategoryID { get; set; }
    public string CategoryTxt { get; set; }

     public int? IssueCategoryID { get; set; }
    public string IssueCategoryTxt { get; set; }


    public int? PriorityID { get; set; }
    public string PriorityTxt { get; set; }

    public string EMPLOYEE_NO { get; set; }
    public string EMPLOYEE_NAME { get; set; }

    public long? TID { get; set; } 
    public string TITLE { get; set; } 
    public string ISSDESC { get; set; }
    public string CLIENT { get; set; }
    public string FRMUSR { get; set; }
    public string USRMAILID { get; set; }
    public string CCMAILID { get; set; }
    public string TASKCCMAILID { get; set; }  
    public string ASSIGNEE { get; set; }
    public int? PRIORITY { get; set; }
    public int? CATEGORY { get; set; }
    public int? ISSCATEGORYCSS { get; set; }
    public string ISSCATEGORYCSSTxt { get; set; } 
    public int? STATUS { get; set; }   
    public DateTime? CREATED_ON { get; set; }
    public DateTime? LASTMODIFIED_ON { get; set; }
    public string LASTMODIFIED_BY { get; set; } 
    public long? TIDREF { get; set; }

    public long? ID { get; set; } 


    public long? AID { get; set; }
    public string ATTACHEMENT_FILE { get; set; }
    public string ATTACHEMENT_FID { get; set; }
    public string ATTACHEMENT_FPATH { get; set; }

    public long? CID { get; set; }
    public string COMMENTS { get; set; }

    public long? SID { get; set; }
    public int? FRMSTATUS { get; set; }
    public int? TOSTATUS { get; set; } 
    public string FRMASSIGNEE { get; set; }
    public string TOASSIGNEE { get; set; }

    public string FRMSTATUSTXT { get; set; }
    public string TOSTATUSTXT { get; set; } 

    public DateTime? MODIFIED_ON { get; set; }
    public string MODIFIED_BY { get; set; }

    public int? Flag { get; set; }

    public string EMPLOYEE_NONAME { get; set; }

    public string PERNR { get; set; }

    public string TICKETACTION { get; set; }
    public string AGENT { get; set; }

    public long? TASKID { get; set; }
    public long? TICKETID { get; set; } 
    
    public string TASKTITLE { get; set; }
    public string TASKDESC { get; set; } 
    public string TASKAGENT { get; set; }
    public string TASKACTUALAGENT { get; set; } 
    public string TaskComments { get; set; }
    
        public DateTime? TASKCREATED_ON { get; set; }
    public DateTime? TASKMODIFIED_ON { get; set; }
        public string TASKCREATED_BY { get; set; }
    public string TASKMODIFIED_BY { get; set; }
    public string TASKFRM { get; set; }

    public int? TASKFROMSTATUS { get; set; }
    public int? TASKTOSTATUS { get; set; }


    public long? TASKCID { get; set; }
    public long? TASKAID { get; set; } 
    public string TASKCCOMMENTS { get; set; }

    public long? TASKSID { get; set; }

    public string TASKSSTS { get; set; } 
        
    public string TASKATTACHEMENT_FILE { get; set; }
    public string TASKATTACHEMENT_FID { get; set; }
    public string TASKATTACHEMENT_FPATH { get; set; }

     public string CBINREVIEWNEEDED { get; set; }

     public string CREATEDONNAME { get; set; }
     public string MODIFIEDONNAME { get; set; }
     public string AGENTNAME { get; set; }
     public string FRMASSIGNEENNAME { get; set; }
     public string TOASSIGNEENNAME { get; set; }
     public string CLIENTNNAME { get; set; }

     public DateTime? ENDDATE { get; set; } 
     public int? NOOFDAYS { get; set; }

     public decimal? PERCENTAGE { get; set; }


     public string customerId { get; set; }
     public string customertxt { get; set; }
     public int? RptStatusID { get; set; }
     public string RptStatusTxt { get; set; }

     public decimal? Plndhrs { get; set; }
     public decimal TaskPlndhrs { get; set; }
     public decimal? Actualhrs { get; set; }
     public decimal TaskActualhrs { get; set; }

     public string TaskActAgentname { get; set; }


     public string EMPMAILIDS { get; set; } 

     public int? PageIndex { get; set; }

     public int? PageSize { get; set; }

     public int? RecordCnt { get; set; }

     public int? RowNumber { get; set; }

     //
     //public string Client { get; set; }
     public string Trouble { get; set; }
     public string priority { get; set; }
     public string priority_type { get; set; }
     public string Resp_time { get; set; }
     public string Resol_time { get; set; }
     public string Resol_days { get; set; }
     public decimal? SecSLA { get; set; }
     public string TIDTITLE { get; set; }

     public string FileName { get; set; }
     public string FileURL { get; set; }
     public int? ISSTYPE { get; set; }
     public int? IssueTypeID { get; set; }
     public string IssueTypeTxt { get; set; }
     public long? TASKLINEID { get; set; }

     ///public decimal? TRAVERAGE { get; set; }

     //chartClickpostback
     public string USER { get; set; }
     public int CHARTNO { get; set; }
     public string POSTEDVALUE { get; set; }
    //public string POSTEDVALUEOUT? { get; set; }


     public decimal? TRAVERAGE { get; set; }
     public int? RID { get; set; }
     public decimal? Q1 { get; set; }
     public decimal? Q2 { get; set; }
     public decimal? Q3 { get; set; }
     public decimal? Q4 { get; set; }
     public decimal? Q5 { get; set; }
     public string RATINGCOMMENTS { get; set; }
     public decimal? SUMRATING { get; set; }

}
//}