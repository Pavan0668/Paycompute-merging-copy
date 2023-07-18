using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class UI_Performance_Management_System_PMS : System.Web.UI.Page
{
   protected void Page_Load(object sender, EventArgs e)
    {
        lblMessageBoard.Text = "";

        if (!IsPostBack)
        {
            loadApprTemplates();
        }
    }

    public void loadApprTemplates()
    {
        try
        {
            PMSbo objPMSBo = new PMSbo();
            PMSbl objPMSBl = new PMSbl();
            PMScollectionbo objPMSLst = new PMScollectionbo();
            string strAppraiseeID = User.Identity.Name;                    

            objPMSLst = objPMSBl.Load_Appraisal_Templates(strAppraiseeID);
            if (objPMSLst.Count > 0)
            {
                //Session.Add("TemplateList", objPMSLst);
                //grdSelfAppraisalReview.DataSource = objPMSLst;
                //grdSelfAppraisalReview.DataBind();
                DataTable dt = new DataTable();
                dt.Columns.Add("APPRAISAL_ID");
                dt.Columns.Add("APPRAISAL_NAME");
                dt.Columns.Add("APPRAISEE_ID");
                dt.Columns.Add("APPRAISER_ID");
                dt.Columns.Add("STATUSAPPR");
                              
               for (int i = 0; i <= objPMSLst.Count - 1; i++)
                {
                    string strApprID = objPMSLst[i].APPRAISAL_ID.Trim().ToString();
                    string strAppName = objPMSLst[i].APPRAISAL_NAME.Trim().ToString();
                    string strAppraisee = objPMSLst[i].APPRAISEE_ID.Trim().ToString();
                    string strAppraiser = objPMSLst[i].APPRAISER_ID.Trim().ToString();
                    string strStatus = objPMSLst[i].STATUSAPPR.Trim().ToString();                                        

                    if (strAppraisee == strAppraiseeID)
                    {
                        DataRow dr = dt.NewRow();
                        dr["APPRAISAL_ID"] = strApprID;
                        dr["APPRAISAL_NAME"] = strAppName;
                        dr["APPRAISEE_ID"] = strAppraisee;
                        dr["APPRAISER_ID"] = strAppraiser;
                        dr["STATUSAPPR"] = strStatus;
                        dt.Rows.Add(dr);                    
                    }

                    if (strAppraiser == strAppraiseeID)
                    {
                        if ((strAppName == "NRRS Appraisee Template") & ((strStatus == "pending") || (strStatus == "4") || (strStatus == "save")))
                        {
                            DataRow dr = dt.NewRow();
                            dr["APPRAISAL_ID"] = strApprID;
                            dr["APPRAISAL_NAME"] = strAppName;
                            dr["APPRAISEE_ID"] = strAppraisee;
                            dr["APPRAISER_ID"] = strAppraiser;
                            dr["STATUSAPPR"] = strStatus;
                            dt.Rows.Add(dr);    
                        }
                        else if (strAppName == "NRRS Appraiser Template")
                        {
                            DataRow dr = dt.NewRow();
                            dr["APPRAISAL_ID"] = strApprID;
                            dr["APPRAISAL_NAME"] = strAppName;
                            dr["APPRAISEE_ID"] = strAppraisee;
                            dr["APPRAISER_ID"] = strAppraiser;
                            dr["STATUSAPPR"] = strStatus;
                            dt.Rows.Add(dr);    
                        }
                    }       

                }

                   if (dt.Rows.Count > 0)
                   {
                       dt.AcceptChanges();
                       grdSelfAppraisalReview.DataSource = dt;
                       grdSelfAppraisalReview.DataBind();
                   }
            }
            else
            {
                pnlFill.Visible = false;
                lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                lblMessageBoard.Text = GetLocalResourceObject("NoTemplatesFound").ToString();
                return;
            }
        }
        catch(Exception ex)
        {
            lblMessageBoard.ForeColor = System.Drawing.Color.Red;
            lblMessageBoard.Text = GetLocalResourceObject("UnkownError").ToString();
            return;
        }
    }

    public void loadApprTemplatesForApprove()
    {
        try
        {
            PMSbo objPMSBo = new PMSbo();
            PMSbl objPMSBl = new PMSbl();
            PMScollectionbo objPMSLst = new PMScollectionbo();
            string strAppraiseeID = User.Identity.Name;

            objPMSLst = objPMSBl.Load_Appraisal_Templates(strAppraiseeID);
            if (objPMSLst.Count > 0)
            {
                Session.Add("TemplateList", objPMSLst);
                grdSelfAppraisalReview.DataSource = objPMSLst;
                grdSelfAppraisalReview.DataBind();
            }
            else
            {
                pnlFill.Visible = false;
                lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                lblMessageBoard.Text = GetLocalResourceObject("NoTemplatesFound").ToString();
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


    protected void grdSelfAppraisalReview_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            GridViewRow grdRow = grdSelfAppraisalReview.SelectedRow;
            Session.Add("currentSelectedRow", grdRow);
            string strAppraisalID = grdRow.Cells[0].Text;
            string strAppraisalName = grdRow.Cells[1].Text;
            string strAppraiseeID = grdRow.Cells[2].Text;
            string strAppraiserID = grdRow.Cells[3].Text;
            
            if (strAppraisalName == "NRRS Appraisee Template")
            {
                Response.Redirect("~/UI/Performance_Management_System/appraisal_form.aspx",false);
            }
            else if (strAppraisalName == "NRRS Appraiser Template")
            {
                Response.Redirect("~/UI/Performance_Management_System/appraisal_review_form.aspx", false);
            }
        }
        catch (Exception ex)
        {
            lblMessageBoard.ForeColor = System.Drawing.Color.Red;
            lblMessageBoard.Text = GetLocalResourceObject("UnkownError").ToString();
            return;
        }
        //catch (System.Threading.ThreadAbortException lException)
        //{

        //    // do nothing

        //}
    }
    protected void grdSelfAppraisalReview_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onmouseover"] = "this.style.cursor='hand';this.style.textDecoration='underline';";
                e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';";

                e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.grdSelfAppraisalReview, "Select$" + e.Row.RowIndex);
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