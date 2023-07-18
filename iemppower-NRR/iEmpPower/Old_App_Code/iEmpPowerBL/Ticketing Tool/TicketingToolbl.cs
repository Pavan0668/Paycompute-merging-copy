using iEmpPower.Old_App_Code.iEmpPowerDAL.Ticketing_Tool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

//namespace iEmpPower.Old_App_Code.iEmpPowerBL.Ticketing_Tool
//{
public class TicketingToolbl
{
    public TicketingToolbl()
    {

    }

    TicketingTooldalDataContext objPersonalIdsDataContext = new TicketingTooldalDataContext();

    public static TicketingToolCollectionbo Load_Priority()
    {
        TicketingTooldalDataContext objDataContext = new TicketingTooldalDataContext();
        TicketingToolCollectionbo objList = new TicketingToolCollectionbo();
        foreach (var vRow in objDataContext.usp_tcikety_load_priority())
        {
            TicketingToolbo objBo = new TicketingToolbo();
            objBo.PriorityID = vRow.PriorityID;
            objBo.PriorityTxt = vRow.PriorityTxt.Trim();
            objList.Add(objBo);
        }
        objDataContext.Dispose();
        return objList;
    }

    public static TicketingToolCollectionbo Load_Category()
    {
        TicketingTooldalDataContext objDataContext = new TicketingTooldalDataContext();
        TicketingToolCollectionbo objList = new TicketingToolCollectionbo();
        foreach (var vRow in objDataContext.usp_tcikety_load_category())
        {
            TicketingToolbo objBo = new TicketingToolbo();
            objBo.CategoryID = vRow.CategoryID;
            objBo.CategoryTxt = vRow.CategoryTxt.Trim();
            objList.Add(objBo);
        }
        objDataContext.Dispose();
        return objList;
    }

    public static TicketingToolCollectionbo Load_IssueCategoryCSS(int type, string pernr, long ticketid)
    {
        TicketingTooldalDataContext objDataContext = new TicketingTooldalDataContext();
        TicketingToolCollectionbo objList = new TicketingToolCollectionbo();
        foreach (var vRow in objDataContext.usp_tcikety_load_Issuecategory(type, pernr, ticketid))
        {
            TicketingToolbo objBo = new TicketingToolbo();
            objBo.IssueCategoryID = vRow.IssueCategoryID;
            objBo.IssueCategoryTxt = vRow.IssueCategoryTxt.Trim();
            objList.Add(objBo);
        }
        objDataContext.Dispose();
        return objList;
    }



    public static TicketingToolCollectionbo Load_Status(int type, string pernr, long ticketid)
    {
        TicketingTooldalDataContext objDataContext = new TicketingTooldalDataContext();
        TicketingToolCollectionbo objList = new TicketingToolCollectionbo();
        foreach (var vRow in objDataContext.usp_tcikety_load_status(type, pernr, ticketid))
        {
            TicketingToolbo objBo = new TicketingToolbo();
            objBo.StatusID = vRow.StatusID;
            objBo.StatusTxt = vRow.StatusTxt.Trim();
            objList.Add(objBo);
        }
        objDataContext.Dispose();
        return objList;
    }

    public static TicketingToolCollectionbo Load_StatusForTask(int type, string pernr, long ticketid)
    {
        TicketingTooldalDataContext objDataContext = new TicketingTooldalDataContext();
        TicketingToolCollectionbo objList = new TicketingToolCollectionbo();
        foreach (var vRow in objDataContext.usp_tcikety_load_statusForTask(type, pernr, ticketid))
        {
            TicketingToolbo objBo = new TicketingToolbo();
            objBo.StatusID = vRow.StatusID;
            objBo.StatusTxt = vRow.StatusTxt.Trim();
            objList.Add(objBo);
        }
        objDataContext.Dispose();
        return objList;
    }

    public static TicketingToolCollectionbo Load_StatusSearch(string Pernr)
    {
        TicketingTooldalDataContext objDataContext = new TicketingTooldalDataContext();
        TicketingToolCollectionbo objList = new TicketingToolCollectionbo();
        foreach (var vRow in objDataContext.usp_tcikety_load_statusSearch(Pernr))
        {
            TicketingToolbo objBo = new TicketingToolbo();
            objBo.SearchStatusID = vRow.SearchStatusID;
            objBo.SearchStatusTxt = vRow.SearchStatusTxt.Trim();
            objList.Add(objBo);
        }
        objDataContext.Dispose();
        return objList;
    }

    public List<TicketingToolbo> GetTickety_Employee_Names(string prefixText)
    {
        List<TicketingToolbo> TicketingboList = new List<TicketingToolbo>();
        TicketingTooldalDataContext objTicketingToolDataContext = new TicketingTooldalDataContext();
        foreach (var vRow in objTicketingToolDataContext.usp_tcikety_load_assignee(prefixText))
        {
            TicketingToolbo TicketingboObj = new TicketingToolbo();
            TicketingboObj.EMPLOYEE_NAME = vRow.ENAME;
            TicketingboObj.EMPLOYEE_NO = vRow.PERNR;
            TicketingboObj.EMPLOYEE_NONAME = vRow.PERNR + " - " + vRow.ENAME;
            TicketingboList.Add(TicketingboObj);
        }
        return TicketingboList;
    }



   public static TicketingToolCollectionbo GetTickety_Load_Employee_Names(string Pernr, long TID, int type)
   {
       TicketingTooldalDataContext objDataContext = new TicketingTooldalDataContext();
       TicketingToolCollectionbo objList = new TicketingToolCollectionbo();
       foreach (var vRow in objDataContext.usp_tcikety_load_assignee_names(Pernr, TID, type))
       {
           TicketingToolbo TicketingboObj = new TicketingToolbo();
           TicketingboObj.EMPLOYEE_NAME = vRow.ENAME;
           TicketingboObj.EMPLOYEE_NO = vRow.PERNR;
           TicketingboObj.EMPLOYEE_NONAME = vRow.PERNR + " - " + vRow.ENAME;
           objList.Add(TicketingboObj);
       }
       objDataContext.Dispose();
       return objList;
   }


   public static TicketingToolCollectionbo GetTickety_Load_TaskEmployee_Names(string Pernr, long TID, long Taskid, int type)
   {
       TicketingTooldalDataContext objDataContext = new TicketingTooldalDataContext();
       TicketingToolCollectionbo objList = new TicketingToolCollectionbo();
       foreach (var vRow in objDataContext.usp_tcikety_load_Taskassignee_names(Pernr, TID, Taskid, type))
       {
           TicketingToolbo TicketingboObj = new TicketingToolbo();
           TicketingboObj.EMPLOYEE_NAME = vRow.ENAME.ToString().Trim();
           TicketingboObj.EMPLOYEE_NO = vRow.PERNR.ToString().Trim();
           TicketingboObj.EMPLOYEE_NONAME = vRow.PERNR + " - " + vRow.ENAME;
           objList.Add(TicketingboObj);
       }
       objDataContext.Dispose();
       return objList;
   }



    public int CREATE_TICKET(TicketingToolbo objTicketingBo, ref long? TicketRefIdOut)
    {
        try
        {
            TicketingTooldalDataContext objTicketingToolDataContext = new TicketingTooldalDataContext();

            int iResultCode = objTicketingToolDataContext.usp_tcikety_Create_Ticket(objTicketingBo.TID
                , objTicketingBo.TITLE
                , objTicketingBo.ISSDESC
                , objTicketingBo.CLIENT
                , objTicketingBo.FRMUSR
                , objTicketingBo.USRMAILID
                , objTicketingBo.CCMAILID
                , objTicketingBo.FRMASSIGNEE
                , objTicketingBo.TOASSIGNEE
                , objTicketingBo.PRIORITY
                , objTicketingBo.CATEGORY
                , objTicketingBo.ISSCATEGORYCSS
                , objTicketingBo.ISSTYPE
                , objTicketingBo.FRMSTATUS
                , objTicketingBo.TOSTATUS
                , objTicketingBo.ATTACHEMENT_FILE
                , objTicketingBo.ATTACHEMENT_FID
                , objTicketingBo.ATTACHEMENT_FPATH
                , objTicketingBo.COMMENTS
                , objTicketingBo.CBINREVIEWNEEDED
                , objTicketingBo.CREATED_ON
                , objTicketingBo.LASTMODIFIED_ON
                , objTicketingBo.LASTMODIFIED_BY
                , objTicketingBo.Flag
                , objTicketingBo.TICKETACTION
               , objTicketingBo.Plndhrs
               , objTicketingBo.Actualhrs
                , ref TicketRefIdOut);

            return iResultCode;
        }
        catch (Exception Ex)
        { throw new Exception(Ex.Message); }
    }




    public List<TicketingToolbo> Load_AllTickets(string Pernr, int StatusTyp, string custtype, DateTime Fromdate, DateTime Todate, long tid, TicketingToolbo TicketingToolbo, int Percnt, ref int? RecordCnt)
    {
        TicketingTooldalDataContext objTicketingToolDataContext = new TicketingTooldalDataContext();
        List<TicketingToolbo> TicketingboList = new List<TicketingToolbo>();

        foreach (var vRow in objTicketingToolDataContext.usp_tcikety_load_allTickets(Pernr, StatusTyp, custtype, Fromdate, Todate, tid,TicketingToolbo.PageIndex, TicketingToolbo.PageSize,Percnt, ref RecordCnt))
        {
            TicketingToolbo TicketingboObj = new TicketingToolbo();

            TicketingboObj.TID = vRow.TID;
            TicketingboObj.TITLE = vRow.TITLE;
            TicketingboObj.ISSDESC = vRow.ISSDESC;
            TicketingboObj.CLIENT = vRow.CLIENT;
            TicketingboObj.FRMUSR = vRow.FRMUSR;
            TicketingboObj.USRMAILID = vRow.USRMAILID;
            TicketingboObj.ASSIGNEE = vRow.ASSIGNEE;
            TicketingboObj.PRIORITY = vRow.PRIORITY;
            TicketingboObj.PriorityTxt = vRow.PriorityTxt;
            TicketingboObj.CATEGORY = vRow.CATEGORY;
            TicketingboObj.CategoryTxt = vRow.CategoryTxt;
            TicketingboObj.STATUS = vRow.STATUS;
            TicketingboObj.StatusTxt = vRow.StatusTxt;
            TicketingboObj.CREATED_ON = vRow.CREATED_ON;
            TicketingboObj.LASTMODIFIED_ON = vRow.LASTMODIFIED_ON;
            TicketingboObj.LASTMODIFIED_BY = vRow.LASTMODIFIED_BY;
            TicketingboObj.TIDREF = vRow.TIDREF;
            TicketingboObj.TICKETACTION = vRow.Action;
            TicketingboObj.AGENT = string.IsNullOrEmpty(vRow.Agent) ? "" : vRow.Agent;
            TicketingboObj.CLIENTNNAME = vRow.Clientname;
            TicketingboObj.MODIFIEDONNAME = vRow.modifiedonname;
            TicketingboObj.AGENTNAME = vRow.Agentname;
            TicketingboObj.TOASSIGNEENNAME = vRow.Toassigneename;
            TicketingboObj.PERCENTAGE = vRow.percentage;
            TicketingboObj.SecSLA = vRow.breacheddiffHr;
            TicketingboObj.ISSTYPE = vRow.ISSUETYPE;
            TicketingboObj.IssueTypeTxt = vRow.ISSUETYPETxt;
            TicketingboList.Add(TicketingboObj);
        }
        return TicketingboList;
    }



    public List<TicketingToolbo> Load_AllTask(string Pernr, int StatusTyp, TicketingToolbo TicketingToolbo, ref int? RecordCnt)
    {
        TicketingTooldalDataContext objTicketingToolDataContext = new TicketingTooldalDataContext();
        List<TicketingToolbo> TicketingboList = new List<TicketingToolbo>();
        foreach (var vRow in objTicketingToolDataContext.usp_tcikety_load_allTask(Pernr, StatusTyp, TicketingToolbo.PageIndex, TicketingToolbo.PageSize, ref RecordCnt))
        {
            TicketingToolbo TicketingboObj = new TicketingToolbo();

            TicketingboObj.TICKETID = vRow.TICKETID;
            TicketingboObj.TASKID = vRow.TASKID;
            TicketingboObj.TASKTITLE = vRow.TASKTITLE;
            TicketingboObj.TASKDESC = vRow.TASKDESC;
            TicketingboObj.TASKAGENT = vRow.TASKAGENT;
            TicketingboObj.TASKTOSTATUS = vRow.TASKSTATUS;
            TicketingboObj.TASKCREATED_ON = vRow.TASKCREATED_ON;
            TicketingboObj.TASKCREATED_BY = vRow.TASKCREATED_BY;
            TicketingboObj.TASKMODIFIED_ON = vRow.TASKMODIFIED_ON;
            TicketingboObj.TASKMODIFIED_BY = vRow.TASKMODIFIED_BY;
            TicketingboObj.StatusTxt = vRow.StatusTxt;
            TicketingboObj.CREATEDONNAME = vRow.createdonname;
            TicketingboObj.MODIFIEDONNAME = vRow.modifiedonname;
            TicketingboObj.AGENTNAME = vRow.agentname;
            TicketingboObj.TaskActAgentname = vRow.TaskActAgentname;
            TicketingboObj.TASKACTUALAGENT = vRow.TASKACTUALAGENT;
            TicketingboObj.TASKLINEID = vRow.TASKLINEID;
            TicketingboList.Add(TicketingboObj);
        }
        return TicketingboList;
    }

    public List<TicketingToolbo> Load_Ticket(long TicketID)
    {
        TicketingTooldalDataContext objTicketingToolDataContext = new TicketingTooldalDataContext();
        List<TicketingToolbo> TicketingboList = new List<TicketingToolbo>();
        foreach (var vRow in objTicketingToolDataContext.usp_tcikety_load_Ticket(TicketID))
        {
            TicketingToolbo TicketingboObj = new TicketingToolbo();

            TicketingboObj.TID = vRow.TID;
            TicketingboObj.TITLE = vRow.TITLE;
            TicketingboObj.ISSDESC = vRow.ISSDESC;
            TicketingboObj.CLIENT = vRow.CLIENT;
            TicketingboObj.FRMUSR = vRow.FRMUSR;
            TicketingboObj.USRMAILID = vRow.USRMAILID;
            TicketingboObj.ASSIGNEE = vRow.ASSIGNEE;
            TicketingboObj.PRIORITY = vRow.PRIORITY;
            TicketingboObj.PriorityTxt = vRow.PriorityTxt;
            TicketingboObj.CATEGORY = vRow.CATEGORY;
            TicketingboObj.CategoryTxt = vRow.CategoryTxt;
            TicketingboObj.STATUS = vRow.STATUS;
            TicketingboObj.StatusTxt = vRow.StatusTxt;
            TicketingboObj.CREATED_ON = vRow.CREATED_ON;
            TicketingboObj.LASTMODIFIED_ON = vRow.LASTMODIFIED_ON;
            TicketingboObj.LASTMODIFIED_BY = vRow.LASTMODIFIED_BY;
            TicketingboObj.TIDREF = vRow.TIDREF;
            TicketingboObj.AGENT = vRow.Agent;
            TicketingboObj.TASKSSTS = vRow.TasksStatus;
            TicketingboObj.CBINREVIEWNEEDED = vRow.CBINREVIEWNEEDED;
            TicketingboObj.CLIENTNNAME = vRow.Clientname;
            TicketingboObj.MODIFIEDONNAME = vRow.modifiedonname;
            TicketingboObj.AGENTNAME = vRow.Agentname;
            TicketingboObj.TOASSIGNEENNAME = vRow.Toassigneename;
            TicketingboObj.ISSCATEGORYCSS = vRow.ISSCATEGORYCSS;
            TicketingboObj.ISSTYPE = vRow.ISSCATEGORYTYPE;
            TicketingboObj.Plndhrs = vRow.PlndHrs;
            TicketingboObj.Actualhrs = vRow.ActualHrs;
            TicketingboObj.CCMAILID = vRow.CCMAILID;
            TicketingboList.Add(TicketingboObj);
        }
        return TicketingboList;
    }

    public List<TicketingToolbo> Load_Ticket_Attachments(long TicketID, string pernr)
    {
        TicketingTooldalDataContext objTicketingToolDataContext = new TicketingTooldalDataContext();
        List<TicketingToolbo> TicketingboList = new List<TicketingToolbo>();
        foreach (var vRow in objTicketingToolDataContext.usp_tcikety_load_Ticket_Attachments(TicketID, pernr))
        {
            TicketingToolbo TicketingboObj = new TicketingToolbo();

            TicketingboObj.TID = vRow.TID;
            TicketingboObj.ID = vRow.ID;
            TicketingboObj.AID = vRow.AID;
            TicketingboObj.TASKID = vRow.TASKID;
            TicketingboObj.ATTACHEMENT_FILE = vRow.ATTACHEMNT_FILE;
            TicketingboObj.ATTACHEMENT_FID = vRow.ATTACHEMENT_FID;
            TicketingboObj.ATTACHEMENT_FPATH = vRow.ATTACHEMENT_FPATH;
            TicketingboObj.MODIFIED_ON = vRow.MODIFIED_ON;
            TicketingboObj.MODIFIED_BY = vRow.MODIFIED_BY;
            TicketingboObj.STATUS = vRow.STATUS;
            TicketingboObj.TIDREF = vRow.TIDREF;
            TicketingboObj.StatusTxt = vRow.StatusTxt;
            TicketingboObj.MODIFIEDONNAME = vRow.modifiedonname;

            TicketingboList.Add(TicketingboObj);
        }
        return TicketingboList;
    }


    public List<TicketingToolbo> Load_Ticket_Comments(long TicketID, string pernr)
    {
        TicketingTooldalDataContext objTicketingToolDataContext = new TicketingTooldalDataContext();
        List<TicketingToolbo> TicketingboList = new List<TicketingToolbo>();
        foreach (var vRow in objTicketingToolDataContext.usp_tcikety_load_Ticket_Comments(TicketID, pernr))
        {
            TicketingToolbo TicketingboObj = new TicketingToolbo();

            TicketingboObj.TID = vRow.TID;
            TicketingboObj.ID = vRow.ID;
            TicketingboObj.CID = vRow.CID;
            TicketingboObj.TASKID = vRow.TASKID;
            TicketingboObj.COMMENTS = vRow.COMMENTS;
            TicketingboObj.MODIFIED_ON = vRow.MODIFIED_ON;
            TicketingboObj.MODIFIED_BY = vRow.MODIFIED_BY;
            TicketingboObj.STATUS = vRow.STATUS;
            TicketingboObj.TIDREF = vRow.TIDREF;
            TicketingboObj.StatusTxt = vRow.StatusTxt;
            TicketingboObj.MODIFIEDONNAME = vRow.modifiedonname;
            TicketingboList.Add(TicketingboObj);
        }
        return TicketingboList;
    }


    public List<TicketingToolbo> Load_Ticket_Status(long TicketID)
    {
        TicketingTooldalDataContext objTicketingToolDataContext = new TicketingTooldalDataContext();
        List<TicketingToolbo> TicketingboList = new List<TicketingToolbo>();
        foreach (var vRow in objTicketingToolDataContext.usp_tcikety_load_Ticket_Status(TicketID))
        {
            TicketingToolbo TicketingboObj = new TicketingToolbo();

            TicketingboObj.TID = vRow.TID;
            TicketingboObj.ID = vRow.ID;
            TicketingboObj.SID = vRow.SID;
            TicketingboObj.TASKID = vRow.TASKID;
            TicketingboObj.FRMSTATUS = vRow.FRMSTATUS;
            TicketingboObj.TOSTATUS = vRow.TOSTATUS;
            TicketingboObj.FRMASSIGNEE = vRow.FRMASSIGNEE;
            TicketingboObj.TOASSIGNEE = vRow.TOASSIGNEE;
            TicketingboObj.FRMSTATUSTXT = vRow.FRMSTATUSTXT;
            TicketingboObj.TOSTATUSTXT = vRow.TOSTATUSTXT;
            TicketingboObj.MODIFIED_ON = vRow.MODIFIED_ON;
            TicketingboObj.MODIFIED_BY = vRow.MODIFIED_BY;
            TicketingboObj.TIDREF = vRow.TIDREF;
            TicketingboObj.MODIFIEDONNAME = vRow.modifiedonname;
            TicketingboObj.FRMASSIGNEENNAME = vRow.FrmAssigneename;
            TicketingboObj.TOASSIGNEENNAME = vRow.ToAssigneename;
            
            TicketingboList.Add(TicketingboObj);
        }
        return TicketingboList;
    }



    public TicketingToolCollectionbo Get_Clients(int flag)
    {
        TicketingTooldalDataContext objTicketingToolDataContext = new TicketingTooldalDataContext();
        TicketingToolCollectionbo objTicketingToolList = new TicketingToolCollectionbo();
        foreach (var vRow in objTicketingToolDataContext.usp_tcikety_get_clients(flag))
        {
            TicketingToolbo objBo = new TicketingToolbo();
            objBo.PERNR = vRow.USErName.ToString();
            objTicketingToolList.Add(objBo);
        }
        objTicketingToolDataContext.Dispose();
        return objTicketingToolList;
    }


    public int CheckIfclients(int flag, string Pernr, ref string Status)
    {
        try
        {
            TicketingTooldalDataContext objTicketingToolDataContext = new TicketingTooldalDataContext();
            int iResultCode = objTicketingToolDataContext.usp_tcikety_CheckIf_clients(flag, Pernr, ref Status);
            return iResultCode;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message.ToString());
        }
    }




    public int CREATE_TICKETTASK(TicketingToolbo objTicketingBo, ref long? TaskRefIdOut)
    {
        try
        {
            TicketingTooldalDataContext objTicketingToolDataContext = new TicketingTooldalDataContext();


            int iResultCode = objTicketingToolDataContext.usp_tcikety_Create_TicketTask(objTicketingBo.TASKID
                , objTicketingBo.TICKETID
                , objTicketingBo.TASKTITLE
                , objTicketingBo.TASKDESC
                , objTicketingBo.TASKFRM
                , objTicketingBo.TASKAGENT
                , objTicketingBo.TASKCCMAILID
                , objTicketingBo.TaskComments
                , objTicketingBo.TASKFROMSTATUS
                , objTicketingBo.TASKTOSTATUS
                , objTicketingBo.TASKATTACHEMENT_FILE
                , objTicketingBo.TASKATTACHEMENT_FID
                , objTicketingBo.TASKATTACHEMENT_FPATH
                , objTicketingBo.TASKCREATED_ON
                , objTicketingBo.TASKCREATED_BY
                , objTicketingBo.TASKMODIFIED_ON
                , objTicketingBo.TASKMODIFIED_BY
                , objTicketingBo.Flag
                , objTicketingBo.TaskPlndhrs
                , objTicketingBo.TaskActualhrs
                , ref TaskRefIdOut);

            return iResultCode;
        }
        catch (Exception Ex)
        { throw new Exception(Ex.Message); }
    }

    public List<TicketingToolbo> Load_TaskDataofTicket(long TicketID, string pernr)
    {
        TicketingTooldalDataContext objTicketingToolDataContext = new TicketingTooldalDataContext();
        List<TicketingToolbo> TicketingboList = new List<TicketingToolbo>();
        foreach (var vRow in objTicketingToolDataContext.usp_tcikety_load_allTasksofTicket(TicketID, pernr))
        {
            TicketingToolbo TicketingboObj = new TicketingToolbo();

            TicketingboObj.TICKETID = vRow.TICKETID;
            TicketingboObj.TASKID = vRow.TASKID;
            TicketingboObj.TASKTITLE = vRow.TASKTITLE;
            TicketingboObj.TASKDESC = vRow.TASKDESC;
            TicketingboObj.TASKAGENT = vRow.TASKAGENT;
            TicketingboObj.TASKTOSTATUS = vRow.TASKSTATUS;
            TicketingboObj.TASKCREATED_ON = vRow.TASKCREATED_ON;
            TicketingboObj.TASKCREATED_BY = vRow.TASKCREATED_BY;
            TicketingboObj.TASKMODIFIED_ON = vRow.TASKMODIFIED_ON;
            TicketingboObj.TASKMODIFIED_BY = vRow.TASKMODIFIED_BY;
            TicketingboObj.StatusTxt = vRow.StatusTxt;
            TicketingboObj.CREATEDONNAME = vRow.createdonname;
            TicketingboObj.MODIFIEDONNAME = vRow.modifiedonname;
            TicketingboObj.AGENTNAME = vRow.agentname;
            TicketingboObj.TaskActAgentname = vRow.TaskActAgentname;
            TicketingboObj.TASKACTUALAGENT = vRow.TASKACTUALAGENT;
            TicketingboObj.TASKLINEID = vRow.TASKLINEID;
            TicketingboList.Add(TicketingboObj);
        }
        return TicketingboList;
    }

    public List<TicketingToolbo> Load_TaskData(long TicketID, long TaskID, string pernr)
    {
        TicketingTooldalDataContext objTicketingToolDataContext = new TicketingTooldalDataContext();
        List<TicketingToolbo> TicketingboList = new List<TicketingToolbo>();
        foreach (var vRow in objTicketingToolDataContext.usp_tcikety_load_Task(TicketID, TaskID, pernr))
        {
            TicketingToolbo TicketingboObj = new TicketingToolbo();

            TicketingboObj.TICKETID = vRow.TICKETID;
            TicketingboObj.TASKID = vRow.TASKID;
            TicketingboObj.TASKTITLE = vRow.TASKTITLE;
            TicketingboObj.TASKDESC = vRow.TASKDESC;
            TicketingboObj.TASKAGENT = vRow.TASKAGENT;
            TicketingboObj.TASKTOSTATUS = vRow.TASKSTATUS;
            TicketingboObj.TASKCREATED_ON = vRow.TASKCREATED_ON;
            TicketingboObj.TASKCREATED_BY = vRow.TASKCREATED_BY;
            TicketingboObj.TASKMODIFIED_ON = vRow.TASKMODIFIED_ON;
            TicketingboObj.TASKMODIFIED_BY = vRow.TASKMODIFIED_BY;
            TicketingboObj.StatusTxt = vRow.StatusTxt;
            TicketingboObj.CREATEDONNAME = vRow.createdonname;
            TicketingboObj.MODIFIEDONNAME = vRow.modifiedonname;
            TicketingboObj.AGENTNAME = vRow.agentname;
            TicketingboObj.TaskPlndhrs = vRow.TaskPlndHrs;
            TicketingboObj.TaskActualhrs = vRow.TaskActualHrs;
            TicketingboObj.TASKACTUALAGENT = vRow.TASKACTUALAGENT;
            TicketingboObj.TASKCCMAILID = vRow.TASKCCMAILID;
            TicketingboObj.TASKLINEID = vRow.TASKLINEID;
            TicketingboList.Add(TicketingboObj);
        }
        return TicketingboList;
    }



    public List<TicketingToolbo> Load_Task_Comments(long TicketID, long taskId)
    {
        TicketingTooldalDataContext objTicketingToolDataContext = new TicketingTooldalDataContext();
        List<TicketingToolbo> TicketingboList = new List<TicketingToolbo>();
        foreach (var vRow in objTicketingToolDataContext.usp_tcikety_load_Task_Comments(TicketID, taskId))
        {
            TicketingToolbo TicketingboObj = new TicketingToolbo();

            TicketingboObj.TICKETID = vRow.TASKTICKETID;
            TicketingboObj.ID = vRow.ID;
            TicketingboObj.TASKCID = vRow.TASKCID;
            TicketingboObj.TASKID = vRow.TASKID;
            TicketingboObj.TASKCCOMMENTS = vRow.TASKCCOMMENTS;
            TicketingboObj.MODIFIED_ON = vRow.TASKCMODIFIED_ON;
            TicketingboObj.MODIFIED_BY = vRow.TASKCMODIFIED_BY;
            TicketingboObj.STATUS = vRow.TASKCSTATUS;
            TicketingboObj.StatusTxt = vRow.StatusTxt;
            TicketingboObj.MODIFIEDONNAME = vRow.modifiedonname;
            TicketingboObj.TASKLINEID = vRow.TASKLINEID;
            TicketingboList.Add(TicketingboObj);
        }
        return TicketingboList;
    }

    public List<TicketingToolbo> Load_Task_Attachments(long TicketID, long taskId)
    {

        TicketingTooldalDataContext objTicketingToolDataContext = new TicketingTooldalDataContext();
        List<TicketingToolbo> TicketingboList = new List<TicketingToolbo>();
        foreach (var vRow in objTicketingToolDataContext.usp_tcikety_load_Task_Attachments(TicketID, taskId))
        {
            TicketingToolbo TicketingboObj = new TicketingToolbo();

            TicketingboObj.TICKETID = vRow.TASKTICKETID;
            TicketingboObj.ID = vRow.ID;
            TicketingboObj.TASKAID = vRow.TASKAID;
            TicketingboObj.TASKID = vRow.TASKID;
            TicketingboObj.TASKATTACHEMENT_FILE = vRow.TASKATTACHEMNT_FILE;
            TicketingboObj.TASKATTACHEMENT_FID = vRow.TASKATTACHEMENT_FID;
            TicketingboObj.TASKATTACHEMENT_FPATH = vRow.TASKATTACHEMENT_FPATH;
            TicketingboObj.MODIFIED_ON = vRow.TASKMODIFIED_ON;
            TicketingboObj.MODIFIED_BY = vRow.TASKMODIFIED_BY;
            TicketingboObj.STATUS = vRow.TASKASTATUS;
            TicketingboObj.StatusTxt = vRow.StatusTxt;
            TicketingboObj.MODIFIEDONNAME = vRow.modifiedonname;
            TicketingboObj.TASKLINEID = vRow.TASKLINEID;
            TicketingboList.Add(TicketingboObj);
        }
        return TicketingboList;
    }

    public List<TicketingToolbo> Load_Task_Status(long TicketID, long taskId)
    {
        TicketingTooldalDataContext objTicketingToolDataContext = new TicketingTooldalDataContext();
        List<TicketingToolbo> TicketingboList = new List<TicketingToolbo>();
        foreach (var vRow in objTicketingToolDataContext.usp_tcikety_load_Task_Status(TicketID, taskId))
        {
            TicketingToolbo TicketingboObj = new TicketingToolbo();

            TicketingboObj.TICKETID = vRow.TASKTICKETID;
            TicketingboObj.ID = vRow.ID;
            TicketingboObj.TASKSID = vRow.TASKSID;
            TicketingboObj.TASKID = vRow.TASKID;
            TicketingboObj.FRMSTATUS = vRow.TASKSFRMSTATUS;
            TicketingboObj.TOSTATUS = vRow.TASKSTOSTATUS;
            TicketingboObj.FRMASSIGNEE = vRow.TASKSFRMASSIGNEE;
            TicketingboObj.TOASSIGNEE = vRow.TASKSTOASSIGNEE;
            TicketingboObj.FRMSTATUSTXT = vRow.FRMSTATUSTXT;
            TicketingboObj.TOSTATUSTXT = vRow.TOSTATUSTXT;
            TicketingboObj.MODIFIED_ON = vRow.TASKSMODIFIED_ON;
            TicketingboObj.MODIFIED_BY = vRow.TASKSMODIFIED_BY;
            TicketingboObj.MODIFIEDONNAME = vRow.modifiedonname;
            TicketingboObj.FRMASSIGNEENNAME = vRow.FrmAssigneename;
            TicketingboObj.TOASSIGNEENNAME = vRow.ToAssigneename;
            TicketingboObj.TASKLINEID = vRow.TASKLINEID;
            TicketingboList.Add(TicketingboObj);
        }
        return TicketingboList;
    }



    public List<TicketingToolbo> GetRemainingTime(long TicketID)
    {
        TicketingTooldalDataContext objTicketingToolDataContext = new TicketingTooldalDataContext();
        List<TicketingToolbo> TicketingboList = new List<TicketingToolbo>();
        foreach (var vRow in objTicketingToolDataContext.usp_tcikety_getTimeRemaining(TicketID))
        {
            TicketingToolbo TicketingboObj = new TicketingToolbo();


            TicketingboObj.CREATED_ON = vRow.CREATED_ON;
            TicketingboObj.ENDDATE = vRow.enddate;
            TicketingboObj.NOOFDAYS = vRow.resolution;
            TicketingboList.Add(TicketingboObj);
        }
        return TicketingboList;
    }



    public List<TicketingToolbo> Load_TicketForReport(long TicketID)
    {
        TicketingTooldalDataContext objTicketingToolDataContext = new TicketingTooldalDataContext();
        List<TicketingToolbo> TicketingboList = new List<TicketingToolbo>();
        foreach (var vRow in objTicketingToolDataContext.usp_tcikety_load_TicketForReport(TicketID))
        {
            TicketingToolbo TicketingboObj = new TicketingToolbo();

            TicketingboObj.TID = vRow.TID;
            TicketingboObj.TITLE = vRow.TITLE;
            TicketingboObj.ISSDESC = vRow.ISSDESC;
            TicketingboObj.CLIENT = vRow.CLIENT;
            TicketingboObj.FRMUSR = vRow.FRMUSR;
            TicketingboObj.USRMAILID = vRow.USRMAILID;
            TicketingboObj.ASSIGNEE = vRow.ASSIGNEE;
            TicketingboObj.PRIORITY = vRow.PRIORITY;
            TicketingboObj.PriorityTxt = vRow.PriorityTxt;
            TicketingboObj.CATEGORY = vRow.CATEGORY;
            TicketingboObj.CategoryTxt = vRow.CategoryTxt;
            TicketingboObj.STATUS = vRow.STATUS;
            TicketingboObj.StatusTxt = vRow.StatusTxt;
            TicketingboObj.CREATED_ON = vRow.CREATED_ON;
            TicketingboObj.LASTMODIFIED_ON = vRow.LASTMODIFIED_ON;
            TicketingboObj.LASTMODIFIED_BY = vRow.LASTMODIFIED_BY;
            TicketingboObj.TIDREF = vRow.TIDREF;
            TicketingboObj.AGENT = vRow.Agent;
            TicketingboObj.CBINREVIEWNEEDED = vRow.CBINREVIEWNEEDED;
            TicketingboObj.CLIENTNNAME = vRow.Clientname;
            TicketingboObj.MODIFIEDONNAME = vRow.modifiedonname;
            TicketingboObj.AGENTNAME = vRow.Agentname;
            TicketingboObj.TOASSIGNEENNAME = vRow.Toassigneename;
            TicketingboObj.IssueCategoryTxt = vRow.IssueCategoryTxt;
            TicketingboList.Add(TicketingboObj);
        }
        return TicketingboList;
    }

    public int REWORK_TICKET(TicketingToolbo objTicketingBo, ref long? REOWRKTICKETIDOUT)
    {
        try
        {
            TicketingTooldalDataContext objTicketingToolDataContext = new TicketingTooldalDataContext();

            int iResultCode = objTicketingToolDataContext.usp_tcikety_Rework_Ticket(objTicketingBo.TID
                , objTicketingBo.ATTACHEMENT_FILE
                , objTicketingBo.ATTACHEMENT_FID
                , objTicketingBo.ATTACHEMENT_FPATH
                , objTicketingBo.COMMENTS
                , objTicketingBo.LASTMODIFIED_BY
                , objTicketingBo.Flag
                , ref REOWRKTICKETIDOUT);

            return iResultCode;
        }
        catch (Exception Ex)
        { throw new Exception(Ex.Message); }
    }


    public static TicketingToolCollectionbo Load_StatusSearch_Rprt(string Pernr)
    {
        TicketingTooldalDataContext objDataContext = new TicketingTooldalDataContext();
        TicketingToolCollectionbo objList = new TicketingToolCollectionbo();
        foreach (var vRow in objDataContext.usp_tcikety_load_statusRprt(Pernr))
        {
            TicketingToolbo objBo = new TicketingToolbo();
            objBo.RptStatusID = vRow.RptStatusID;
            objBo.RptStatusTxt = vRow.RptStatusTxt.Trim();
            objList.Add(objBo);
        }
        objDataContext.Dispose();
        return objList;
    }



    public static TicketingToolCollectionbo Load_customerwiseSrch(string Pernr)
    {
        TicketingTooldalDataContext objDataContext = new TicketingTooldalDataContext();
        TicketingToolCollectionbo objList = new TicketingToolCollectionbo();
        foreach (var vRow in objDataContext.usp_tcikety_load_customerwiseSrch(Pernr))
        {
            TicketingToolbo objBo = new TicketingToolbo();
            objBo.customerId = vRow.customerId;
            objBo.customertxt = vRow.customertxt.Trim();
            objList.Add(objBo);
        }
        objDataContext.Dispose();
        return objList;
    }



    public List<TicketingToolbo> Load_AllTickets_Reports(int flag,string Pernr, string custtype, int StatusTyp, DateTime Fromdate, DateTime Todate,TicketingToolbo TicketingObjBo, int Percnt, ref int? RecordCnt)
    {
        TicketingTooldalDataContext objTicketingToolDataContext = new TicketingTooldalDataContext();
        List<TicketingToolbo> TicketingboList = new List<TicketingToolbo>();
        foreach (var vRow in objTicketingToolDataContext.usp_tcikety_load_TicketsReports(flag,Pernr, custtype, StatusTyp, Fromdate, Todate, TicketingObjBo.PageIndex, TicketingObjBo.PageSize,Percnt, ref RecordCnt))
        {
            TicketingToolbo TicketingboObj = new TicketingToolbo();

            TicketingboObj.TID = vRow.TID;
            TicketingboObj.TITLE = vRow.TITLE;
            TicketingboObj.ISSDESC = vRow.ISSDESC;
            TicketingboObj.CLIENT = vRow.CLIENT;
            TicketingboObj.FRMUSR = vRow.FRMUSR;
            TicketingboObj.USRMAILID = vRow.USRMAILID;
            TicketingboObj.ASSIGNEE = vRow.ASSIGNEE;
            TicketingboObj.PRIORITY = vRow.PRIORITY;
            TicketingboObj.PriorityTxt = vRow.PriorityTxt;
            TicketingboObj.CATEGORY = vRow.CATEGORY;
            TicketingboObj.CategoryTxt = vRow.CategoryTxt;
            TicketingboObj.STATUS = vRow.STATUS;
            TicketingboObj.StatusTxt = vRow.StatusTxt;
            TicketingboObj.CREATED_ON = vRow.CREATED_ON;
            TicketingboObj.LASTMODIFIED_ON = vRow.LASTMODIFIED_ON;
            TicketingboObj.LASTMODIFIED_BY = vRow.LASTMODIFIED_BY;
            TicketingboObj.TIDREF = vRow.TIDREF;
            TicketingboObj.TICKETACTION = vRow.Action;
            TicketingboObj.AGENT = string.IsNullOrEmpty(vRow.Agent) ? "" : vRow.Agent;
            TicketingboObj.CLIENTNNAME = vRow.Clientname;
            TicketingboObj.MODIFIEDONNAME = vRow.modifiedonname;
            TicketingboObj.AGENTNAME = vRow.Agentname;
            TicketingboObj.TOASSIGNEENNAME = vRow.Toassigneename;
            TicketingboObj.PERCENTAGE = vRow.percentage;
            TicketingboObj.Plndhrs = vRow.PlndHrs;
            TicketingboObj.SecSLA = vRow.breacheddiffHr;
            TicketingboObj.ISSTYPE = vRow.ISSUETYPE;
            TicketingboObj.IssueTypeTxt = vRow.ISSUETYPETxt;
            TicketingboObj.TRAVERAGE = vRow.TRAVERAGE;
            TicketingboObj.Actualhrs = vRow.Actualhrs;
            TicketingboObj.ISSCATEGORYCSS = vRow.Modu_ID;
            TicketingboObj.ISSCATEGORYCSSTxt = vRow.Modu_text;
            TicketingboList.Add(TicketingboObj);
        }
        return TicketingboList;
    }

    public List<TicketingToolbo> Load_AllTickets_BasdOnChart(string Pernr, int ChartNo, string TypeClick, DateTime FrmDt, DateTime ToDt, TicketingToolbo TicketingToolbo, ref int? RecordCnt)
    {
        TicketingTooldalDataContext objTicketingToolDataContext = new TicketingTooldalDataContext();
        List<TicketingToolbo> TicketingboList = new List<TicketingToolbo>();
        foreach (var vRow in objTicketingToolDataContext.usp_tcikety_load_basedOn_ChartClick(Pernr, ChartNo, TypeClick, FrmDt, ToDt, TicketingToolbo.PageIndex, TicketingToolbo.PageSize, ref RecordCnt))
        {
            TicketingToolbo TicketingboObj = new TicketingToolbo();

            TicketingboObj.TID = vRow.TID;
            TicketingboObj.TITLE = vRow.TITLE;
            TicketingboObj.ISSDESC = vRow.ISSDESC;
            TicketingboObj.CLIENT = vRow.CLIENT;
            TicketingboObj.FRMUSR = vRow.FRMUSR;
            TicketingboObj.USRMAILID = vRow.USRMAILID;
            TicketingboObj.ASSIGNEE = vRow.ASSIGNEE;
            TicketingboObj.PRIORITY = vRow.PRIORITY;
            TicketingboObj.PriorityTxt = vRow.PriorityTxt;
            TicketingboObj.CATEGORY = vRow.CATEGORY;
            TicketingboObj.CategoryTxt = vRow.CategoryTxt;
            TicketingboObj.STATUS = vRow.STATUS;
            TicketingboObj.StatusTxt = vRow.StatusTxt;
            TicketingboObj.CREATED_ON = vRow.CREATED_ON;
            TicketingboObj.LASTMODIFIED_ON = vRow.LASTMODIFIED_ON;
            TicketingboObj.LASTMODIFIED_BY = vRow.LASTMODIFIED_BY;
            TicketingboObj.TIDREF = vRow.TIDREF;
            TicketingboObj.TICKETACTION = vRow.Action;
            TicketingboObj.AGENT = string.IsNullOrEmpty(vRow.Agent) ? "" : vRow.Agent;
            TicketingboObj.CLIENTNNAME = vRow.Clientname;
            TicketingboObj.MODIFIEDONNAME = vRow.modifiedonname;
            TicketingboObj.AGENTNAME = vRow.Agentname;
            TicketingboObj.TOASSIGNEENNAME = vRow.Toassigneename;
            TicketingboObj.PERCENTAGE = vRow.percentage;
            TicketingboObj.SecSLA = vRow.breacheddiffHr;
            TicketingboObj.ISSTYPE = vRow.ISSUETYPE;
            TicketingboObj.IssueTypeTxt = vRow.ISSUETYPETxt;
            TicketingboList.Add(TicketingboObj);
        }
        return TicketingboList;
    }

    public List<TicketingToolbo> Get_Employee_MailIDS(string prefixText)
    {
        List<TicketingToolbo> TicketingToolboList = new List<TicketingToolbo>();
        TicketingTooldalDataContext objTicketingToolDataContext = new TicketingTooldalDataContext();
        foreach (var vRow in objTicketingToolDataContext.usp_tickety_get_employee_mailids(prefixText))
        {
            TicketingToolbo TicketingToolboObj = new TicketingToolbo();
            TicketingToolboObj.EMPMAILIDS = vRow.EMailID;
            TicketingToolboList.Add(TicketingToolboObj);
        }
        return TicketingToolboList;
    }

    public List<TicketingToolbo> LoadSLADtls()
    {
        List<TicketingToolbo> TicketingToolboList = new List<TicketingToolbo>();
        TicketingTooldalDataContext objTicketingToolDataContext = new TicketingTooldalDataContext();
        foreach (var vRow in objTicketingToolDataContext.usp_tcikety_load_SLA_Details())
        {

            TicketingToolbo objBo = new TicketingToolbo();
            objBo.CLIENT = vRow.Client;
            objBo.Trouble = vRow.Trouble.Trim();
            objBo.priority = vRow.Priority.ToString().Trim();
            objBo.priority_type = vRow.Priority_type.Trim();
            objBo.Resp_time = vRow.Response_Time.ToString().Trim() + " Minutes";
            objBo.Resol_time = vRow.Resolution_Time.ToString().Trim() + " Hours";
            objBo.Resol_days = vRow.Resolution_Days.ToString().Trim() + " Days";
            TicketingToolboList.Add(objBo);
        }
        //objDataContext.Dispose();
        return TicketingToolboList;
    }

    public static TicketingToolCollectionbo Load_Tickets(string pernr)
    {
        TicketingTooldalDataContext objDataContext = new TicketingTooldalDataContext();
        TicketingToolCollectionbo objList = new TicketingToolCollectionbo();
        foreach (var vRow in objDataContext.usp_tcikety_load_Ticket_RWT(pernr))
        {
            TicketingToolbo objBo = new TicketingToolbo();
            objBo.TID = vRow.TID;
            objBo.TIDTITLE = vRow.TID + " - " + vRow.TITLE.ToString();
            objList.Add(objBo);
        }
        objDataContext.Dispose();
        return objList;
    }

    public List<TicketingToolbo> LoadMandateDocs(string PERNR)
    {
        List<TicketingToolbo> TicketingToolboList = new List<TicketingToolbo>();
        TicketingTooldalDataContext objTicketingToolDataContext = new TicketingTooldalDataContext();
        foreach (var vRow in objTicketingToolDataContext.usp_tickety_get_mandate_template_docs(PERNR))
        {

            TicketingToolbo objBo = new TicketingToolbo();
            objBo.FileName = vRow.FileName.ToString().Trim();
            objBo.FileURL = vRow.URL.ToString().Trim();
            TicketingToolboList.Add(objBo);
        }
        //objDataContext.Dispose();
        return TicketingToolboList;
    }

    public static TicketingToolCollectionbo Load_IssueType(int type, string pernr, long ticketid)
    {
        TicketingTooldalDataContext objDataContext = new TicketingTooldalDataContext();
        TicketingToolCollectionbo objList = new TicketingToolCollectionbo();
        foreach (var vRow in objDataContext.usp_tcikety_load_IssueType(type, pernr, ticketid))
        {
            TicketingToolbo objBo = new TicketingToolbo();
            objBo.IssueTypeID = vRow.IssueTypeID;
            objBo.IssueTypeTxt = vRow.IssueTypeTxt.Trim();
            objList.Add(objBo);
        }
        objDataContext.Dispose();
        return objList;
    }

    public int GetPostBackValue(TicketingToolbo objTicketingBo, ref string outp)
    {
        TicketingTooldalDataContext objDataContext = new TicketingTooldalDataContext();
        return objDataContext.usp_tcikety_get_postbackValue_on_chartclick(objTicketingBo.USER, objTicketingBo.CHARTNO, objTicketingBo.POSTEDVALUE, ref outp);
    }

    public static TicketingToolCollectionbo Load_customerForRating(string Pernr)
    {
        TicketingTooldalDataContext objDataContext = new TicketingTooldalDataContext();
        TicketingToolCollectionbo objList = new TicketingToolCollectionbo();
        foreach (var vRow in objDataContext.usp_tcikety_load_customerforRating(Pernr))
        {
            TicketingToolbo objBo = new TicketingToolbo();
            objBo.customerId = vRow.customerId;
            objBo.customertxt = vRow.customertxt.Trim();
            objList.Add(objBo);
        }
        objDataContext.Dispose();
        return objList;
    }

    public static TicketingToolCollectionbo Load_EmployeeForRating(string Pernr)
    {
        TicketingTooldalDataContext objDataContext = new TicketingTooldalDataContext();
        TicketingToolCollectionbo objList = new TicketingToolCollectionbo();
        foreach (var vRow in objDataContext.usp_tcikety_load_EmployeeforRating(Pernr))
        {
            TicketingToolbo objBo = new TicketingToolbo();
            objBo.EMPLOYEE_NO = vRow.PERNR;
            objBo.EMPLOYEE_NAME = vRow.ENAME.Trim();
            objList.Add(objBo);
        }
        objDataContext.Dispose();
        return objList;
    }

   public List<TicketingToolbo> Load_CustomerFeedbackRating(int flag, string Pernr, string CustTyp, string Emptype, long tid, TicketingToolbo TicketingToolbo, ref int? RecordCnt)
    {
        TicketingTooldalDataContext objTicketingToolDataContext = new TicketingTooldalDataContext();
        List<TicketingToolbo> TicketingboList = new List<TicketingToolbo>();

        foreach (var vRow in objTicketingToolDataContext.usp_tcikety_load_CustomerFeedbackTickets(flag, Pernr, CustTyp, Emptype, tid, TicketingToolbo.PageIndex, TicketingToolbo.PageSize, ref RecordCnt))
        {
            TicketingToolbo TicketingboObj = new TicketingToolbo();
            TicketingboObj.RID = vRow.RID;
            TicketingboObj.TID = vRow.TID;
            TicketingboObj.CLIENT = vRow.CLIENT;
            TicketingboObj.AGENT = string.IsNullOrEmpty(vRow.Agent) ? "" : vRow.Agent;
            TicketingboObj.AGENTNAME = vRow.Agentname;
            TicketingboObj.Q1 = vRow.Q1;
            TicketingboObj.Q2 = vRow.Q2;
            TicketingboObj.Q3 = vRow.Q3;
            TicketingboObj.Q4 = vRow.Q4;
            TicketingboObj.Q5 = vRow.Q5;
            TicketingboObj.RATINGCOMMENTS = vRow.RATINGCOMMENTS;
            TicketingboObj.SUMRATING = vRow.SUMRATING;
            TicketingboObj.TRAVERAGE = vRow.AVGRATING;
            TicketingboList.Add(TicketingboObj);
        }
        return TicketingboList;
    }
}
//}