using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class UI_Performance_Management_System_appraisal_review_form : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMessageBoard.Text = "";

        if (!IsPostBack)
        {
            funLoadDropDown();
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("~/UI/Performance_Management_System/PMS.aspx");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error2 " + ex.Message.ToString());
        }
    }

    protected void funLoadDropDown()
    {
        try
        {
            GridViewRow grdRow = (GridViewRow)Session["currentSelectedRow"];
         
            string strAppraisalID = grdRow.Cells[0].Text;
            string strAppraisalName = grdRow.Cells[1].Text;
            string strAppraiseeID = grdRow.Cells[2].Text;
            string strAppraiserID = grdRow.Cells[3].Text;
            string strStatus = grdRow.Cells[4].Text;

            appraisal_formbo objBo = new appraisal_formbo();
            appraisal_formbl objBl = new appraisal_formbl();
            appraisal_formcollectionbo objLst1 = new appraisal_formcollectionbo();
            string strPERNRID = User.Identity.Name;

            //to load the drop down
            //row--1
            objLst1 = objBl.Load_DropDown_Templates(strAppraisalName, "1.1");

            if (objLst1.Count > 0)
            {
                drpdwnCust1.DataSource = objLst1.ToList();
                drpdwnCust1.DataValueField = "VALUE_TEXT";
                drpdwnCust1.DataBind();

                drpdwnCust2.DataSource = objLst1.ToList();
                drpdwnCust2.DataValueField = "VALUE_TEXT";
                drpdwnCust2.DataBind();

                drpdwnCust3.DataSource = objLst1.ToList();
                drpdwnCust3.DataValueField = "VALUE_TEXT";
                drpdwnCust3.DataBind();

                drpdwnCust4.DataSource = objLst1.ToList();
                drpdwnCust4.DataValueField = "VALUE_TEXT";
                drpdwnCust4.DataBind();

                drpdwnCommit1.DataSource = objLst1.ToList();
                drpdwnCommit1.DataValueField = "VALUE_TEXT";
                drpdwnCommit1.DataBind();

                drpdwnCommit2.DataSource = objLst1.ToList();
                drpdwnCommit2.DataValueField = "VALUE_TEXT";
                drpdwnCommit2.DataBind();

                drpdwnCommit3.DataSource = objLst1.ToList();
                drpdwnCommit3.DataValueField = "VALUE_TEXT";
                drpdwnCommit3.DataBind();

                drpdwnCommit4.DataSource = objLst1.ToList();
                drpdwnCommit4.DataValueField = "VALUE_TEXT";
                drpdwnCommit4.DataBind();

                drpdwnTeam1.DataSource = objLst1.ToList();
                drpdwnTeam1.DataValueField = "VALUE_TEXT";
                drpdwnTeam1.DataBind();

                drpdwnTeam2.DataSource = objLst1.ToList();
                drpdwnTeam2.DataValueField = "VALUE_TEXT";
                drpdwnTeam2.DataBind();

                drpdwnTeam3.DataSource = objLst1.ToList();
                drpdwnTeam3.DataValueField = "VALUE_TEXT";
                drpdwnTeam3.DataBind();

                drpdwnTeam4.DataSource = objLst1.ToList();
                drpdwnTeam4.DataValueField = "VALUE_TEXT";
                drpdwnTeam4.DataBind();

                drpdwnKnow1.DataSource = objLst1.ToList();
                drpdwnKnow1.DataValueField = "VALUE_TEXT";
                drpdwnKnow1.DataBind();

                drpdwnKnow2.DataSource = objLst1.ToList();
                drpdwnKnow2.DataValueField = "VALUE_TEXT";
                drpdwnKnow2.DataBind();

                drpdwnKnow3.DataSource = objLst1.ToList();
                drpdwnKnow3.DataValueField = "VALUE_TEXT";
                drpdwnKnow3.DataBind();

                drpdwnKnow4.DataSource = objLst1.ToList();
                drpdwnKnow4.DataValueField = "VALUE_TEXT";
                drpdwnKnow4.DataBind();

                drpdwnSub1.DataSource = objLst1.ToList();
                drpdwnSub1.DataValueField = "VALUE_TEXT";
                drpdwnSub1.DataBind();

                drpdwnSub2.DataSource = objLst1.ToList();
                drpdwnSub2.DataValueField = "VALUE_TEXT";
                drpdwnSub2.DataBind();

                drpdwnSub3.DataSource = objLst1.ToList();
                drpdwnSub3.DataValueField = "VALUE_TEXT";
                drpdwnSub3.DataBind();

                drpdwnSub4.DataSource = objLst1.ToList();
                drpdwnSub4.DataValueField = "VALUE_TEXT";
                drpdwnSub4.DataBind();

                drpdwnPers1.DataSource = objLst1.ToList();
                drpdwnPers1.DataValueField = "VALUE_TEXT";
                drpdwnPers1.DataBind();

                drpdwnPers2.DataSource = objLst1.ToList();
                drpdwnPers2.DataValueField = "VALUE_TEXT";
                drpdwnPers2.DataBind();

                drpdwnPers3.DataSource = objLst1.ToList();
                drpdwnPers3.DataValueField = "VALUE_TEXT";
                drpdwnPers3.DataBind();

                drpdwnPers4.DataSource = objLst1.ToList();
                drpdwnPers4.DataValueField = "VALUE_TEXT";
                drpdwnPers4.DataBind();

                drpdwnJob1.DataSource = objLst1.ToList();
                drpdwnJob1.DataValueField = "VALUE_TEXT";
                drpdwnJob1.DataBind();

                drpdwnJob2.DataSource = objLst1.ToList();
                drpdwnJob2.DataValueField = "VALUE_TEXT";
                drpdwnJob2.DataBind();

                drpdwnJob3.DataSource = objLst1.ToList();
                drpdwnJob3.DataValueField = "VALUE_TEXT";
                drpdwnJob3.DataBind();

                drpdwnJob4.DataSource = objLst1.ToList();
                drpdwnJob4.DataValueField = "VALUE_TEXT";
                drpdwnJob4.DataBind();

            }

            //to load data
            if (strStatus == "4")
            {
                    pnlAppraisalReview.Enabled = false;
                    btnSave.Visible = false;
                    btnSaveAndSend.Visible = false;
                    btnCancel.Visible = false;               
            }
           
            //loadind appraisal data
                appraisal_formcollectionbo objLst2 = new appraisal_formcollectionbo();
                objLst2= objBl.Load_Appraisal_Data(strAppraisalID, strAppraisalName, strAppraiseeID, strAppraiserID);

                if (objLst2.Count > 0)
                {
                    for (int i = 0; i <= objLst2.Count - 1; i++)
                    {
                        string strElemnt = objLst2[i].ELEMENT_NAME.Trim().ToString();
                        string strValueTxt = objLst2[i].VALUE_TEXT_DRP.Trim().ToString();

                        switch (strElemnt)
                        {
                            case "1.1":
                                         drpdwnCust1.Text = strValueTxt;
                                         break;

                            case "1.2":
                                         drpdwnCust2.Text = strValueTxt;
                                         break;

                            case "1.3":
                                         drpdwnCust3.Text = strValueTxt;
                                         break;

                            case "1.4":
                                         drpdwnCust4.Text = strValueTxt;
                                         break;

                            case "2.1":
                                         drpdwnCommit1.Text = strValueTxt;
                                         break;

                            case "2.2":
                                         drpdwnCommit2.Text = strValueTxt;                                        
                                         break;

                            case "2.3":
                                         drpdwnCommit3.Text = strValueTxt;
                                         break;

                            case "2.4":
                                         drpdwnCommit4.Text = strValueTxt;
                                         break;

                            case "3.1": 
                                         drpdwnTeam1.Text = strValueTxt;
                                         break;

                            case "3.2":
                                         drpdwnTeam2.Text = strValueTxt;
                                         break;

                            case "3.3":
                                         drpdwnTeam3.Text = strValueTxt;
                                         break;

                            case "3.4":
                                         drpdwnTeam4.Text = strValueTxt;
                                         break;

                            case "4.1":
                                         drpdwnKnow1.Text = strValueTxt;
                                         break;

                            case "4.2":
                                         drpdwnKnow2.Text = strValueTxt;
                                         break;

                            case "4.3":
                                         drpdwnKnow3.Text = strValueTxt;
                                         break;

                            case "4.4":
                                         drpdwnKnow4.Text = strValueTxt;
                                         break;

                            case "5.1": 
                                         drpdwnSub1.Text = strValueTxt;
                                         break;

                            case "5.2":
                                         drpdwnSub2.Text = strValueTxt;
                                         break;

                            case "5.3":
                                         drpdwnSub3.Text = strValueTxt;
                                         break;

                            case "5.4":
                                         drpdwnSub4.Text = strValueTxt;
                                         break;

                            case "6.1":
                                         drpdwnPers1.Text = strValueTxt;
                                         break;

                            case "6.2":
                                         drpdwnPers2.Text = strValueTxt;
                                         break;

                            case "6.3":
                                         drpdwnPers3.Text = strValueTxt;
                                         break;

                            case "6.4":
                                         drpdwnPers4.Text = strValueTxt;
                                         break;

                            case "7.1": 
                                         drpdwnJob1.Text = strValueTxt;
                                         break;

                            case "7.2":
                                         drpdwnJob2.Text = strValueTxt;
                                         break;

                            case "7.3":
                                         drpdwnJob3.Text = strValueTxt;
                                         break;

                            case "7.4":
                                         drpdwnJob4.Text = strValueTxt;
                                         break;

                            case "9":
                                         txtStrengths.Text = objLst2[i].TDLINE.ToString();
                                         break;

                            case "10":
                                         txtImprovements.Text = objLst2[i].TDLINE.ToString();
                                         break;

                            case "11":
                                         txtTraining.Text = objLst2[i].TDLINE.ToString();
                                         break;


                        }

                    }

                        if (objLst2[0].ELEMENT_NAME == "1.1")
                        {
                            drpdwnCust1.Text = objLst2[0].VALUE_TEXT_DRP.Trim().ToString();
                        }
                        else if (objLst2[1].ELEMENT_NAME == "1.1")
                        {
                            drpdwnCust1.Text = objLst2[0].VALUE_TEXT_DRP.Trim().ToString();
                        }
                }   
          

        }
        catch (Exception ex)
        {
            lblMessageBoard.ForeColor = System.Drawing.Color.Red;
            lblMessageBoard.Text = GetLocalResourceObject("UnkownError").ToString();
            return;
        }

    }

    protected void btnReview_Click(object sender, EventArgs e)
    {
        pnlAppraisalReview.Enabled = false;
        btnSaveAndSend.Enabled = true;
    }
 
    
    public Dictionary<string, string> funLoadElmntDropDwn()
    {
        Dictionary<string, string> lst = new Dictionary<string, string>();

        //Block 1
            string strCust1 = drpdwnCust1.SelectedValue.ToString();
            string strCust2 = drpdwnCust2.SelectedValue.ToString();
            string strCust3 = drpdwnCust3.SelectedValue.ToString();
            string strCust4 = drpdwnCust4.SelectedValue.ToString();

            //Block 2
            string strCommit1 = drpdwnCommit1.SelectedValue.ToString();
            string strCommit2 = drpdwnCommit2.SelectedValue.ToString();
            string strCommit3 = drpdwnCommit3.SelectedValue.ToString();
            string strCommit4 = drpdwnCommit4.SelectedValue.ToString();
        
            //Block 3
            string strTeam1 = drpdwnTeam1.SelectedValue.ToString();
            string strTeam2 = drpdwnTeam2.SelectedValue.ToString();
            string strTeam3 = drpdwnTeam3.SelectedValue.ToString();
            string strTeam4 = drpdwnTeam4.SelectedValue.ToString();

            //Block 4
            string strKnow1 = drpdwnKnow1.SelectedValue.ToString();
            string strknow2 = drpdwnKnow2.SelectedValue.ToString();
            string strknow3 = drpdwnKnow3.SelectedValue.ToString();
            string strknow4 = drpdwnKnow4.SelectedValue.ToString();

            //Block 5
            string strSub1 = drpdwnSub1.SelectedValue.ToString();
            string strSub2 = drpdwnSub2.SelectedValue.ToString();
            string strSub3 = drpdwnSub3.SelectedValue.ToString();
            string strSub4 = drpdwnSub4.SelectedValue.ToString();

            //Block 6
            string strPers1 = drpdwnPers1.SelectedValue.ToString();
            string strPers2 = drpdwnPers2.SelectedValue.ToString();
            string strPers3 = drpdwnPers3.SelectedValue.ToString();
            string strPers4 = drpdwnPers4.SelectedValue.ToString();

            //Block 7
            string strJob1 = drpdwnJob1.SelectedValue.ToString();
            string strJob2 = drpdwnJob2.SelectedValue.ToString();
            string strJob3 = drpdwnJob3.SelectedValue.ToString();
            string strJob4 = drpdwnJob4.SelectedValue.ToString();

            lst.Add("1.1", strCust1);
            lst.Add("1.2", strCust2);
            lst.Add("1.3", strCust3);
            lst.Add("1.4", strCust4);

            lst.Add("2.1", strCommit1);
            lst.Add("2.2", strCommit2);
            lst.Add("2.3", strCommit3);
            lst.Add("2.4", strCommit4);

            lst.Add("3.1", strTeam1);
            lst.Add("3.2", strTeam2);
            lst.Add("3.3", strTeam3);
            lst.Add("3.4", strTeam4);

            lst.Add("4.1", strKnow1);
            lst.Add("4.2", strknow2);
            lst.Add("4.3", strknow3);
            lst.Add("4.4", strknow4);

            lst.Add("5.1", strSub1);
            lst.Add("5.2", strSub2);
            lst.Add("5.3", strSub3);
            lst.Add("5.4", strSub4);

            lst.Add("6.1", strPers1);
            lst.Add("6.2", strPers2);
            lst.Add("6.3", strPers3);
            lst.Add("6.4", strPers4);

            lst.Add("7.1", strJob1);
            lst.Add("7.2", strJob2);
            lst.Add("7.3", strJob3);
            lst.Add("7.4", strJob4);
        
            return lst;

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("~/UI/Performance_Management_System/PMS.aspx");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error2 " + ex.Message.ToString());
        }
    }
    protected void btnSaveAndSend_Click(object sender, EventArgs e)
    {
        try
        {
            GridViewRow grdRow = (GridViewRow)Session["currentSelectedRow"];

            string strAppraisalID = grdRow.Cells[0].Text;
            string strAppraisalName = grdRow.Cells[1].Text;
            string strAppraiseeID = grdRow.Cells[2].Text;
            string strAppraiserID = grdRow.Cells[3].Text;
            string strStatus = grdRow.Cells[4].Text;

            appraisal_formbo objBo = new appraisal_formbo();
            appraisal_formbl objBl = new appraisal_formbl();

          
                bool? dd = true;
                int iReturn = 1;

                objBo.APPRAISAL_ID = strAppraisalID;
                objBo.APPRAISAL_NAME = strAppraisalName;
                objBo.APPRAISEE_ID = strAppraiseeID;
                objBo.APPRAISER_ID = strAppraiserID;
                objBo.STATUSAPPR = "4";

                Dictionary<string, string> lstElmnDrpdwn = new Dictionary<string, string>();

                lstElmnDrpdwn = funLoadElmntDropDwn();

                if (lstElmnDrpdwn.Count > 0)
                {
                    for (int i = 0; i <= lstElmnDrpdwn.Count - 1; i++)
                    {
                        string strElem = lstElmnDrpdwn.ElementAt(i).Key.ToString();
                        string strDrpDwn = lstElmnDrpdwn.ElementAt(i).Value.ToString();

                        objBo.ELEMENT_NAME = strElem;
                        objBo.VALUE_TEXT_DRP = strDrpDwn;
                        objBo.TDLINE = "";

                        objBl.Insert_Appraisal_Data(objBo, ref dd);
                    }
                }

                string strNote1 = "9";
                string strNote2 = "10";
                string strNote3 = "11";
                string strDrpdwn = "";

                string strNoteData1 = txtStrengths.Text.ToString();
                string strNoteData2 = txtImprovements.Text.ToString();
                string strNoteData3 = txtTraining.Text.ToString();

                //For note1
                objBo.ELEMENT_NAME = strNote1;
                objBo.VALUE_TEXT_DRP = strDrpdwn;
                objBo.TDLINE = strNoteData1;

                objBl.Insert_Appraisal_Data(objBo, ref dd);

                //For note2
                objBo.ELEMENT_NAME = strNote2;
                objBo.VALUE_TEXT_DRP = strDrpdwn;
                objBo.TDLINE = strNoteData2;

                objBl.Insert_Appraisal_Data(objBo, ref dd);

                //For note3
                objBo.ELEMENT_NAME = strNote3;
                objBo.VALUE_TEXT_DRP = strDrpdwn;
                objBo.TDLINE = strNoteData3;

                iReturn = objBl.Insert_Appraisal_Data(objBo, ref dd);

                if (iReturn == 0)
                {
                    pnlAppraisalReview.Enabled = false;
                    btnSaveAndSend.Enabled = false;
                    btnCancel.Enabled = false;
                    btnSave.Enabled = false;

                    lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                    lblMessageBoard.Text = GetLocalResourceObject("SavedAppraisal").ToString();
                    return;
                }
        }
        catch (Exception ex)
        {
            lblMessageBoard.ForeColor = System.Drawing.Color.Red;
            lblMessageBoard.Text = GetLocalResourceObject("UnkownError").ToString();
            return;
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            GridViewRow grdRow = (GridViewRow)Session["currentSelectedRow"];

            string strAppraisalID = grdRow.Cells[0].Text;
            string strAppraisalName = grdRow.Cells[1].Text;
            string strAppraiseeID = grdRow.Cells[2].Text;
            string strAppraiserID = grdRow.Cells[3].Text;
            string strStatus = grdRow.Cells[4].Text;


            appraisal_formbo objBo = new appraisal_formbo();
            appraisal_formbl objBl = new appraisal_formbl();

                bool? dd = true;
                int iReturn = 1;

                objBo.APPRAISAL_ID = strAppraisalID;
                objBo.APPRAISAL_NAME = strAppraisalName;
                objBo.APPRAISEE_ID = strAppraiseeID;
                objBo.APPRAISER_ID = strAppraiserID;
                objBo.STATUSAPPR = "save";

                Dictionary<string, string> lstElmnDrpdwn = new Dictionary<string, string>();

                lstElmnDrpdwn = funLoadElmntDropDwn();

                if (lstElmnDrpdwn.Count > 0)
                {
                    for (int i = 0; i <= lstElmnDrpdwn.Count - 1; i++)
                    {
                        string strElem = lstElmnDrpdwn.ElementAt(i).Key.ToString();
                        string strDrpDwn = lstElmnDrpdwn.ElementAt(i).Value.ToString();

                        objBo.ELEMENT_NAME = strElem;
                        objBo.VALUE_TEXT_DRP = strDrpDwn;
                        objBo.TDLINE = "";

                        objBl.Insert_Appraisal_Data(objBo, ref dd);
                    }
                }

                string strNote1 = "9";
                string strNote2 = "10";
                string strNote3 = "11";
                string strDrpdwn = "";

                string strNoteData1 = txtStrengths.Text.ToString();
                string strNoteData2 = txtImprovements.Text.ToString();
                string strNoteData3 = txtTraining.Text.ToString();

                //For note1
                objBo.ELEMENT_NAME = strNote1;
                objBo.VALUE_TEXT_DRP = strDrpdwn;
                objBo.TDLINE = strNoteData1;

                objBl.Insert_Appraisal_Data(objBo, ref dd);

                //For note2
                objBo.ELEMENT_NAME = strNote2;
                objBo.VALUE_TEXT_DRP = strDrpdwn;
                objBo.TDLINE = strNoteData2;

                objBl.Insert_Appraisal_Data(objBo, ref dd);

                //For note3
                objBo.ELEMENT_NAME = strNote3;
                objBo.VALUE_TEXT_DRP = strDrpdwn;
                objBo.TDLINE = strNoteData3;

                iReturn = objBl.Insert_Appraisal_Data(objBo, ref dd);

                if (iReturn == 0)
                {
                    pnlAppraisalReview.Enabled = false;
                    btnSaveAndSend.Enabled = false;
                    btnCancel.Enabled = false;
                    btnSave.Enabled = false;

                    lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                    lblMessageBoard.Text = GetLocalResourceObject("SaveAppraisal").ToString();
                    return;
                }
        }
        catch (Exception ex)
        {
            lblMessageBoard.ForeColor = System.Drawing.Color.Red;
            lblMessageBoard.Text = GetLocalResourceObject("UnkownError").ToString();
            return;
        }
    }
    //protected void btnPrint_Click(object sender, EventArgs e)
    //{
    //    Session["ctrl"] = pnlAppraisalReview ;
    //    ClientScript.RegisterStartupScript(this.GetType(), "onclick",
    //        "<script language=javascript>window.open('','','height=500px,width=800px,scrollbars=1');</script>");
    //}
}