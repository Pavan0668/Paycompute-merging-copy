using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iEmpPower.Old_App_Code.iEmpPowerDAL.User_Account;

//namespace iEmpPower.Old_App_Code.iEmpPowerBL.User_Account
//{
public class Announcementbl
{
    public Announcementbl()
    {

    }

    AnnouncementdalDataContext objAnnouncementDataContext = new AnnouncementdalDataContext();

    public int CREATE_ANNOUNCEMENT(Announcementbo Announcementbo, ref string CreatedByName, ref  string CreatedByEmailid, ref int? OUTAID)
    {
        try
        {
            AnnouncementdalDataContext objAnnouncementDataContext = new AnnouncementdalDataContext();

            int iResultCode = objAnnouncementDataContext.usp_Create_Announcements(Announcementbo.AID
                , Announcementbo.ATITLE
                , Announcementbo.ADESC
                , Announcementbo.ADATE
                 , Announcementbo.TOADATE
                , Announcementbo.ATIME
                , Announcementbo.APLACE
                , Announcementbo.CREATED_BY
                , Announcementbo.CREATED_ON
                 , Announcementbo.UPDATED_BY
                , Announcementbo.UPDATED_ON, ref  CreatedByName, ref   CreatedByEmailid, ref  OUTAID);

            return iResultCode;
        }
        catch (Exception Ex)
        { throw new Exception(Ex.Message); }
    }

    public List<Announcementbo> GET_ANNOUNCEMENT(int pageindex,int pagesize)
    {
        AnnouncementdalDataContext objAnnouncementDataContext = new AnnouncementdalDataContext();
        List<Announcementbo> AnnouncementboList = new List<Announcementbo>();

        foreach (var vRow in objAnnouncementDataContext.usp_Get_Announcements(pageindex, pagesize))
        {
            Announcementbo AnnouncementboObj = new Announcementbo();

            AnnouncementboObj.AID = vRow.AID;
            AnnouncementboObj.ATITLE = vRow.ATITLE;
            AnnouncementboObj.ADESC = vRow.ADESC;
            AnnouncementboObj.ADATE = vRow.ADATE;
            AnnouncementboObj.TOADATE = vRow.TOADATE;
            AnnouncementboObj.ATIME = vRow.ATIME.ToString();
            AnnouncementboObj.APLACE = vRow.APLACE;
            AnnouncementboObj.CREATED_BY = vRow.CREATED_BY;
            AnnouncementboObj.CREATED_ON = vRow.CREATED_ON;
            AnnouncementboObj.RecordCnt = vRow.RecordCnt;
            AnnouncementboObj.ENAME = vRow.ENAME;
            AnnouncementboList.Add(AnnouncementboObj);
        }
        return AnnouncementboList;
    }

    //public List<Announcementbo> Load_Announcement()
    //{
    //    AnnouncementdalDataContext objAnnouncementDataContext = new AnnouncementdalDataContext();
    //    List<Announcementbo> AnnouncementboList = new List<Announcementbo>();

    //    foreach (var vRow in objAnnouncementDataContext.usp_Get_AnnouncementsSiteMaster())
    //    {
    //        Announcementbo AnnouncementboObj = new Announcementbo();

    //        AnnouncementboObj.AID = vRow.AID;
    //        AnnouncementboObj.ATITLE = vRow.ATITLE;
    //        AnnouncementboObj.ADESC = vRow.ADESC;
    //        AnnouncementboObj.ADATE = vRow.ADATE;
    //        AnnouncementboObj.ATIME = vRow.ATIME.ToString();
    //        AnnouncementboObj.APLACE = vRow.APLACE;
    //        AnnouncementboObj.CREATED_BY = vRow.CREATED_BY;
    //        AnnouncementboObj.CREATED_ON = vRow.CREATED_ON;
    //        AnnouncementboList.Add(AnnouncementboObj);
    //    }
    //    return AnnouncementboList;
    //}

    public AnnouncementToolCollectionbo Load_Announcement()
    {
        AnnouncementToolCollectionbo AnnouncementboList = new AnnouncementToolCollectionbo();
        AnnouncementdalDataContext objAnnouncementDataContext = new AnnouncementdalDataContext();
        foreach (var vRow in objAnnouncementDataContext.usp_Get_AnnouncementsSiteMaster())
        {
            Announcementbo AnnouncementboObj = new Announcementbo();
            AnnouncementboObj.AID = vRow.AID;
            AnnouncementboObj.ATITLE = vRow.ATITLE;
            AnnouncementboObj.ADESC = vRow.ADESC;
            AnnouncementboObj.ADATE = vRow.ADATE;
            AnnouncementboObj.TOADATE = vRow.TOADATE;
            AnnouncementboObj.ATIME = vRow.ATIME.ToString();
            AnnouncementboObj.APLACE = vRow.APLACE;
            AnnouncementboObj.CREATED_BY = vRow.CREATED_BY;
            AnnouncementboObj.CREATED_ON = vRow.CREATED_ON;
            AnnouncementboObj.ENAME = vRow.ENAME;
            AnnouncementboList.Add(AnnouncementboObj);
        }
        objAnnouncementDataContext.Dispose();
        return AnnouncementboList;
    }

     public List<Announcementbo> GET_ANNOUNCEMENTSEARCH(string Titlesearch, string Placesearch, DateTime Fromdate, int pageindex,int pagesize, ref int? RecCount)
    {
        AnnouncementdalDataContext objAnnouncementDataContext = new AnnouncementdalDataContext();
        List<Announcementbo> AnnouncementboList = new List<Announcementbo>();

        foreach (var vRow in objAnnouncementDataContext.usp_Get_AnnouncementsSearch(Titlesearch, Placesearch, Fromdate, pageindex, pagesize, ref RecCount))
        {
            Announcementbo AnnouncementboObj = new Announcementbo();

            AnnouncementboObj.AID = vRow.AID;
            AnnouncementboObj.ATITLE = vRow.ATITLE;
            AnnouncementboObj.ADESC = vRow.ADESC;
            AnnouncementboObj.ADATE = vRow.ADATE;
            AnnouncementboObj.TOADATE = vRow.TOADATE;
            AnnouncementboObj.ATIME = vRow.ATIME.ToString();
            AnnouncementboObj.APLACE = vRow.APLACE;
            AnnouncementboObj.CREATED_BY = vRow.CREATED_BY;
            AnnouncementboObj.CREATED_ON = vRow.CREATED_ON;
            AnnouncementboObj.ENAME = vRow.ENAME;
            AnnouncementboList.Add(AnnouncementboObj);
        }
        return AnnouncementboList;
    }

    public List<Announcementbo> GET_AIDANNOUNCEMENT(int AID)
    {
        AnnouncementdalDataContext objAnnouncementDataContext = new AnnouncementdalDataContext();
        List<Announcementbo> AnnouncementboList = new List<Announcementbo>();

        foreach (var vRow in objAnnouncementDataContext.usp_Get_AIDAnnouncements(AID))
        {
            Announcementbo AnnouncementboObj = new Announcementbo();

            AnnouncementboObj.AID = vRow.AID;
            AnnouncementboObj.ATITLE = vRow.ATITLE;
            AnnouncementboObj.ADESC = vRow.ADESC;
            AnnouncementboObj.ADATE = vRow.ADATE;
            AnnouncementboObj.TOADATE = vRow.TOADATE;
            AnnouncementboObj.ATIME = vRow.ATIME.ToString();
            AnnouncementboObj.APLACE = vRow.APLACE;
            AnnouncementboObj.CREATED_BY = vRow.CREATED_BY;
            AnnouncementboObj.CREATED_ON = vRow.CREATED_ON;
            AnnouncementboList.Add(AnnouncementboObj);
        }
        return AnnouncementboList;
    }

}
//}