using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using AjaxControlToolkit;
using iEmpPowerDAL.Performance_Management_System;

public partial class UI_Performance_Management_System_appraisee_form : System.Web.UI.Page
{
   
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMessageBoard.Text = "";

        if (!IsPostBack)
        {
           funLoadDropDown();
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

            if (strStatus == "4" || strStatus=="pending")
            {
                pnlAppraisal.Enabled = false;
                btnSave.Visible = false;
                btnSaveAndSend.Visible = false;
                btnApprove.Visible = false;
                btnCancel.Visible = false;
            }
                                   
            appraisal_formbo objBo = new appraisal_formbo();
            appraisal_formbl objBl = new appraisal_formbl();
            appraisal_formcollectionbo objLst1 = new appraisal_formcollectionbo();
            appraisal_formcollectionbo objLst2 = new appraisal_formcollectionbo();
            appraisal_formcollectionbo objLst3 = new appraisal_formcollectionbo();
            string strPERNRID = User.Identity.Name;

            //to load the drop down
            //row--1
            objLst1 = objBl.Load_DropDown_Templates(strAppraisalName, "Performance improvement over previous year");

            if (objLst1.Count > 0)
            {
                drpdwnPerformance.DataSource = objLst1.ToList();
                drpdwnPerformance.DataValueField = "VALUE_TEXT";
                drpdwnPerformance.DataBind();
            }

            //row--2
            //   objLst.Clear();
            objLst1 = objBl.Load_DropDown_Templates(strAppraisalName, "Additional responsibilities / projects taken during the previous year");

            if (objLst1.Count > 0)
            {
                drpdwnAdditional.DataSource = objLst1.ToList();
                drpdwnAdditional.DataValueField = "VALUE_TEXT";
                drpdwnAdditional.DataBind();
            }

            //row--3
            //   objLst.Clear();
            objLst1 = objBl.Load_DropDown_Templates(strAppraisalName, "Any other critical incidences");

            if (objLst1.Count > 0)
            {
                drpdwncritical.DataSource = objLst1.ToList();
                drpdwncritical.DataValueField = "VALUE_TEXT";
                drpdwncritical.DataBind();
            }

            //for approve the appraisee
            if (strPERNRID == strAppraiserID)
            {
                if (strStatus == "pending" || strStatus == "save")
                {
                    drpdwnPerformance.Visible = true;
                    drpdwnAdditional.Visible = true;
                    drpdwncritical.Visible = true;
                    btnSaveAndSend.Visible = false;
                    btnApprove.Visible = true;
                    pnlAppraisal.Enabled = true;
                    btnCancel.Visible = true;
                    btnSave.Visible = true;
                }
                else
                {
                    drpdwnPerformance.Visible = true;
                    drpdwnAdditional.Visible = true;
                    drpdwncritical.Visible = true;
                }
                
                string strName = objBl.Load_AppraiseeName(strAppraiseeID);
                lblAppraiseeNameData.Text = strName;
                lblPernrData.Text = strAppraiseeID;
            }
            else
            {
                string strPernr = Session["sEmploreeId"].ToString();
                string strPernrName = Session["EmployeeName"].ToString();
                lblPernrData.Text = strPernr;
                lblAppraiseeNameData.Text = strPernrName;
            }

            appraisal_formcollectionbo objLst4 = new appraisal_formcollectionbo();
            objLst4 = objBl.Load_Appraisal_Data(strAppraisalID, strAppraisalName, strAppraiseeID, strAppraiserID);

            if (objLst4.Count > 0)
            {
                txtPerformance.Text = objLst4[0].TDLINE.ToString();
                txtAdditional.Text = objLst4[1].TDLINE.ToString();
                txtcritical.Text = objLst4[2].TDLINE.ToString();

                string strPerformance = objLst4[0].VALUE_TEXT_DRP.Trim().ToString();
                string strAdditional = objLst4[1].VALUE_TEXT_DRP.Trim().ToString();
                string strcritical = objLst4[2].VALUE_TEXT_DRP.Trim().ToString();

                if (strPerformance != "")
                {                   
                   drpdwnPerformance.Text  = strPerformance;
                }
                else
                {
                    drpdwnPerformance.SelectedIndex = 0;
                }

                if (strAdditional != "")
                {
                    drpdwnAdditional.Text = strAdditional;
                }
                else
                {
                     drpdwnAdditional.SelectedIndex = 0;
                }

                if (strcritical != "")
                {
                    drpdwncritical.Text = strcritical;
                }
                else
                {
                    drpdwncritical.SelectedIndex = 0;
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
    
    protected void btnBack_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("~/UI/Performance_Management_System/PMS.aspx");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error2 "+ex.Message.ToString());
        }
    }

    protected void btnApprove_Click(object sender, EventArgs e)
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

                string strDropDown1 = drpdwnPerformance.SelectedValue.ToString();
                string strDropDown2 = drpdwnAdditional.SelectedValue.ToString();
                string strDropDown3 = drpdwncritical.SelectedValue.ToString();

                string strElement1 = "Performance improvement over previous year";
                string strElement2 = "Additional responsibilities / projects taken during the previous year";
                string strElement3 = "Any other critical incidences";

                string strNotePerformance = txtPerformance.Text.ToString();
                string strNoteAdditional = txtAdditional.Text.ToString();
                string strNOteCritical = txtcritical.Text.ToString();
                bool? dd = true;
                int iReturn = 1;

                objBo.APPRAISAL_ID = strAppraisalID;
                objBo.APPRAISAL_NAME = strAppraisalName;
                objBo.APPRAISEE_ID = strAppraiseeID;
                objBo.APPRAISER_ID = strAppraiserID;
                objBo.STATUSAPPR = "4";

                //For first element
                objBo.ELEMENT_NAME = strElement1;
                objBo.VALUE_TEXT_DRP = strDropDown1;
                objBo.TDLINE = strNotePerformance;

                objBl.Insert_Appraisal_Data(objBo, ref dd);

                //For second element
                objBo.ELEMENT_NAME = strElement2;
                objBo.VALUE_TEXT_DRP = strDropDown2;
                objBo.TDLINE = strNoteAdditional;

                objBl.Insert_Appraisal_Data(objBo, ref dd);

                //For third element
                objBo.ELEMENT_NAME = strElement3;
                objBo.VALUE_TEXT_DRP = strDropDown3;
                objBo.TDLINE = strNOteCritical;

                iReturn = objBl.Insert_Appraisal_Data(objBo, ref dd);
                if (iReturn == 0)
                {
                    pnlAppraisal.Enabled = false;
                    btnApprove.Enabled = false;
                    btnSaveAndSend.Enabled = false;
                    btnCancel.Enabled = false;
                    btnSave.Enabled = false;

                    lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                    lblMessageBoard.Text = GetLocalResourceObject("ApproveAppraisal").ToString();
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

         
                string strDropDown = "";
                string strElement1 = "Performance improvement over previous year";
                string strElement2 = "Additional responsibilities / projects taken during the previous year";
                string strElement3 = "Any other critical incidences";

                string strNotePerformance = txtPerformance.Text.ToString();
                string strNoteAdditional = txtAdditional.Text.ToString();
                string strNOteCritical = txtcritical.Text.ToString();
                bool? dd = true;
                int iReturn = 1;

                objBo.APPRAISAL_ID = strAppraisalID;
                objBo.APPRAISAL_NAME = strAppraisalName;
                objBo.VALUE_TEXT_DRP = strDropDown;
                objBo.APPRAISEE_ID = strAppraiseeID;
                objBo.APPRAISER_ID = strAppraiserID;
                objBo.STATUSAPPR = "pending";

                //For first element
                objBo.ELEMENT_NAME = strElement1;
                objBo.TDLINE = strNotePerformance;

                objBl.Insert_Appraisal_Data(objBo, ref dd);

                //For second element
                objBo.ELEMENT_NAME = strElement2;
                objBo.TDLINE = strNoteAdditional;

                objBl.Insert_Appraisal_Data(objBo, ref dd);

                //For third element
                objBo.ELEMENT_NAME = strElement3;
                objBo.TDLINE = strNOteCritical;

                iReturn = objBl.Insert_Appraisal_Data(objBo, ref dd);
                if (iReturn == 0)
                {
                    pnlAppraisal.Enabled = false;
                    btnApprove.Enabled = false;
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

            string strDropDown1 = drpdwnPerformance.SelectedValue.ToString();
            string strDropDown2 = drpdwnAdditional.SelectedValue.ToString();
            string strDropDown3 = drpdwncritical.SelectedValue.ToString();
                string strElement1 = "Performance improvement over previous year";
                string strElement2 = "Additional responsibilities / projects taken during the previous year";
                string strElement3 = "Any other critical incidences";

                string strNotePerformance = txtPerformance.Text.ToString();
                string strNoteAdditional = txtAdditional.Text.ToString();
                string strNOteCritical = txtcritical.Text.ToString();
                bool? dd = true;
                int iReturn = 1;

                objBo.APPRAISAL_ID = strAppraisalID;
                objBo.APPRAISAL_NAME = strAppraisalName;             
                objBo.APPRAISEE_ID = strAppraiseeID;
                objBo.APPRAISER_ID = strAppraiserID;
                objBo.STATUSAPPR = "save";

                //For first element
                objBo.ELEMENT_NAME = strElement1;
                objBo.TDLINE = strNotePerformance;
                objBo.VALUE_TEXT_DRP = strDropDown1;
                objBl.Insert_Appraisal_Data(objBo, ref dd);

                //For second element
                objBo.ELEMENT_NAME = strElement2;
                objBo.TDLINE = strNoteAdditional;
                objBo.VALUE_TEXT_DRP = strDropDown2;
                objBl.Insert_Appraisal_Data(objBo, ref dd);

                //For third element
                objBo.ELEMENT_NAME = strElement3;
                objBo.TDLINE = strNOteCritical;
                objBo.VALUE_TEXT_DRP = strDropDown3;
                iReturn = objBl.Insert_Appraisal_Data(objBo, ref dd);
                if (iReturn == 0)
                {
                    pnlAppraisal.Enabled = false;
                    btnCancel.Enabled = false;

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
  
}