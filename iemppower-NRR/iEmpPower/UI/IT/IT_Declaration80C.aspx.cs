using iEmpPower.Old_App_Code.iEmpPowerBL.IT;
using iEmpPower.Old_App_Code.iEmpPowerBO.IT;
using iEmpPower.Old_App_Code.iEmpPowerDAL.IT;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace iEmpPower.UI.IT
{
    public partial class IT_Declaration80C : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    pageLoadEvents();
                    GetFinancialDates();
                    //LoadGrid();
                }

            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        }


        public void pageLoadEvents()
        {
            try
            {
                bool? itlock = false;
                ITdalDataContext objDataContext = new ITdalDataContext();
                objDataContext.usp_IT_GetLockStatus(User.Identity.Name, ref itlock);

                if (itlock == false)
                {
                    MsgCls("", LblLockSts, Color.Transparent);
                    DivSec80C.Visible = true;
                    GetFinancialDates();
                    LoadGrid();

                }
                else if (itlock == true)
                {
                    DivSec80C.Visible = false;
                    MsgCls("IT Declaration has been locked. Please contact Payroll Admin.", LblLockSts, Color.Red);
                }




            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
        }

        private void LoadGrid()
        {
            try
            {
                int? count = 0;
                ITbl ITblObj = new ITbl();
                List<ITbo> ITboObj = new List<ITbo>();
                List<ITbo> ITboObj1 = new List<ITbo>();
                string ApproverId = User.Identity.Name;
                ITboObj1 = ITblObj.Load_IT_Section80CDetails(User.Identity.Name, ref count);
                Session.Add("ITSec80CGrdInfo", ITboObj1);

                if (ITboObj1 == null || ITboObj1.Count == 0)
                {
                    MsgCls("No Records Found !", lblMessageBoard, Color.Red);
                    GVITSec80C.Visible = false;
                    GVITSec80C.DataSource = null;
                    GVITSec80C.DataBind();
                    return;
                }
                else
                {
                    GVITSec80C.Visible = true;
                    GVITSec80C.DataSource = ITboObj1;
                    GVITSec80C.SelectedIndex = -1;
                    GVITSec80C.DataBind();
                }


                ViewState["ID"] = ITboObj1[0].ID == null ? "0" : ITboObj1[0].ID.ToString().Trim();
                ConsiderActProp(ITboObj1[0].CONACTPROP == null ? "0" : ITboObj1[0].CONACTPROP.ToString().Trim());
                if (count > 0)
                {
                    DisableControls(ITboObj1[0].CONACTPROP == null ? "0" : ITboObj1[0].CONACTPROP.ToString().Trim());
                    btnSubmitClaims.Visible = false;
                    BtnUpdate.Visible = false;
                    BtnCancel.Visible = false;
                    BtnEdit.Visible = true;

                    //if (GVITSec80.Rows.Count > 0)
                    //{
                    //    for (int i = 0; i <= GVITSec80.Rows.Count - 1; i++)
                    //    {
                    //        using (LinkButton UpdateBtn = (LinkButton)GVITSec80.Rows[i].FindControl("LbtnUpload"))
                    //        using (LinkButton DeleteBtn = (LinkButton)GVITSec80.Rows[i].FindControl("LbtnDelete"))
                    //        {
                    //            UpdateBtn.Visible  = true;
                    //            DeleteBtn.Visible = true;
                    //        }
                    //    }
                    //}

                }
                else
                {
                    EnableControls(ITboObj1[0].CONACTPROP == null ? "0" : ITboObj1[0].CONACTPROP.ToString().Trim());
                    btnSubmitClaims.Visible = true;
                    BtnUpdate.Visible = false;
                    BtnCancel.Visible = false;
                    BtnEdit.Visible = false;

                    if (GVITSec80C.Rows.Count > 0)
                    {
                        for (int i = 0; i <= GVITSec80C.Rows.Count - 1; i++)
                        {
                           // using (LinkButton UpdateBtn = (LinkButton)GVITSec80C.Rows[i].FindControl("LbtnUpload"))
                            using (LinkButton DeleteBtn = (LinkButton)GVITSec80C.Rows[i].FindControl("LbtnDelete"))
                            {
                                //UpdateBtn.Visible = false;
                                DeleteBtn.Visible = false;
                            }
                        }
                    }
                }



            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }

        }


        public void DisableControls(string type)
        {
            try
            {
                if (GVITSec80C.Rows.Count > 0)
                {
                    for (int i = 0; i <= GVITSec80C.Rows.Count - 1; i++)
                    {
                        using (TextBox txtPropContr = (TextBox)GVITSec80C.Rows[i].FindControl("txtPropContr"))
                        using (TextBox txtActContr = (TextBox)GVITSec80C.Rows[i].FindControl("txtActContr"))
                        using (FileUpload fu = (FileUpload)GVITSec80C.Rows[i].FindControl("fuAttachments"))
                        using (TextBox txtRemarks = (TextBox)GVITSec80C.Rows[i].FindControl("txtRemarks"))
                        //using (LinkButton UpdateBtn = (LinkButton)GVITSec80C.Rows[i].FindControl("LbtnUpload"))
                        using (LinkButton DeleteBtn = (LinkButton)GVITSec80C.Rows[i].FindControl("LbtnDelete"))
                        {
                            if (txtPropContr != null)
                            {
                                if (txtActContr != null)
                                {
                                    if (fu != null)
                                    {
                                        if (txtRemarks != null)
                                        {
                                            txtPropContr.Enabled = false;
                                            fu.Enabled = false;
                                            txtActContr.Enabled = false;
                                            txtRemarks.Enabled = false;
                                            CB_ConsAct.Enabled = false;
                                           // UpdateBtn.Enabled = false;
                                            DeleteBtn.Enabled = false;
                                        }
                                        else
                                        {
                                            throw new Exception("-1");
                                        }
                                    }
                                    else
                                    {
                                        throw new Exception("-1");
                                    }
                                }
                                else
                                {
                                    throw new Exception("-1");
                                }
                            }
                            else
                            {
                                throw new Exception("-1");
                            }
                        }
                    }
                }

            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        }

        public void EnableControls(string type)
        {
            try
            {
                if (GVITSec80C.Rows.Count > 0)
                {
                    for (int i = 0; i <= GVITSec80C.Rows.Count - 1; i++)
                    {
                        using (TextBox txtPropContr = (TextBox)GVITSec80C.Rows[i].FindControl("txtPropContr"))
                        using (TextBox txtActContr = (TextBox)GVITSec80C.Rows[i].FindControl("txtActContr"))
                        using (FileUpload fu = (FileUpload)GVITSec80C.Rows[i].FindControl("fuAttachments"))
                        using (TextBox txtRemarks = (TextBox)GVITSec80C.Rows[i].FindControl("txtRemarks"))
                        //using (LinkButton UpdateBtn = (LinkButton)GVITSec80C.Rows[i].FindControl("LbtnUpload"))
                        using (LinkButton DeleteBtn = (LinkButton)GVITSec80C.Rows[i].FindControl("LbtnDelete"))
                        {
                            if (txtPropContr != null)
                            {
                                if (txtActContr != null)
                                {
                                    if (fu != null)
                                    {
                                        if (txtRemarks != null)
                                        {
                                            //txtPropContr.Enabled = true;
                                            //fu.Enabled = true;
                                            //txtActContr.Enabled = true;
                                            //txtRemarks.Enabled = true;
                                            //CB_ConsAct.Enabled = true; 

                                            if (type.Trim() == "1")
                                            {
                                                //txtActContr.Enabled = true;
                                                //txtPropContr.Enabled = false;

                                                txtPropContr.Enabled = false;
                                                fu.Enabled = true;
                                                txtActContr.Enabled = true;
                                                txtRemarks.Enabled = true;
                                                CB_ConsAct.Enabled = true;
                                               //UpdateBtn.Enabled = true;
                                                DeleteBtn.Enabled = true;
                                            }
                                            else if (type.Trim() == "0")
                                            {
                                                //txtActContr.Enabled = false;
                                                //txtPropContr.Enabled = true;

                                                txtPropContr.Enabled = true;
                                                fu.Enabled = true;
                                                txtActContr.Enabled = false;
                                                txtRemarks.Enabled = true;
                                                CB_ConsAct.Enabled = true;
                                                //UpdateBtn.Enabled = true;
                                                DeleteBtn.Enabled = true;
                                            }

                                        }
                                        else
                                        {
                                            throw new Exception("-1");
                                        }
                                    }
                                    else
                                    {
                                        throw new Exception("-1");
                                    }
                                }
                                else
                                {
                                    throw new Exception("-1");
                                }
                            }
                            else
                            {
                                throw new Exception("-1");
                            }
                        }
                    }
                }

            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        }




        private void GetFinancialDates()
        {
            try
            {
                //DateTime dt = DateTime.Now;
                int month = int.Parse(DateTime.Today.Month.ToString());
                if (month > 3)
                {
                    LblFromDate.Text = DateTime.Today.Year.ToString();
                    LblToDate.Text = (DateTime.Today.Year + 1).ToString();
                }
                else if (month <= 3)
                {
                    LblFromDate.Text = (DateTime.Today.Year - 1).ToString();
                    LblToDate.Text = DateTime.Today.Year.ToString();
                }
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        }

        private void MsgCls(string LblTxt, Label Lbl, Color Clr)
        {
            try
            {
                Lbl.Text = string.Empty;
                Lbl.Text = LblTxt;
                Lbl.ForeColor = Clr;
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
        }

        protected void btnSubmitITSec80_Click(object sender, EventArgs e)
        {
           
                try
                {


                    int RecordCount = 0;

                    if (GVITSec80C.Rows.Count > 0)
                    {
                        for (int i = 0; i < GVITSec80C.Rows.Count; i++)
                        {
                            using (TextBox txtPropContr = (TextBox)GVITSec80C.Rows[i].FindControl("txtPropContr"))
                            using (TextBox txtActContr = (TextBox)GVITSec80C.Rows[i].FindControl("txtActContr"))
                            {
                                if (((!string.IsNullOrEmpty(txtPropContr.Text)) && (decimal.Parse(txtPropContr.Text.ToString().Trim()) > 0)) ||
                                    ((!string.IsNullOrEmpty(txtActContr.Text)) && (decimal.Parse(txtActContr.Text.ToString().Trim()) > 0)))
                                {
                                    RecordCount = RecordCount + 1;
                                }

                            }
                        }
                    }

                    if (RecordCount <= 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Please enter atleast one record before submiting');", true);
                        MsgCls("Please enter atleast one record before submiting", lblMessageBoard, System.Drawing.Color.Red);
                        return;
                    }

                    bool? sts = false;
                    int? ITHID = 0;
                    ITbl ITblObj = new ITbl();
                    ITbo ITboObj = new ITbo();

                    ITboObj.CREATED_BY = User.Identity.Name;
                    ITboObj.PERNR = User.Identity.Name;
                    ITboObj.STATUS = "Requested";
                    ITboObj.CREATED_ON = DateTime.Now;
                    ITboObj.BEGDA = DateTime.Now;
                    ITboObj.ENDDA = DateTime.Now;
                    ITboObj.MODIFIED_BY = "";
                    ITboObj.Flag = 1;
                    ITboObj.ID = 0;
                    ITboObj.CONACTPROP = CB_ConsAct.Checked ? "1" : "0";
                    ITblObj.Create_ITSECTION80CHEADR(ITboObj, ref ITHID); 

                    if (ITHID != null)
                    {
                        string date1;
                        DataTable Dt = GetSec80CDt();
                        if (GVITSec80C.Rows.Count > 0)
                        {
                            for (int i = 0; i < GVITSec80C.Rows.Count; i++)
                            {
                                using (TextBox txtPropContr = (TextBox)GVITSec80C.Rows[i].FindControl("txtPropContr"))
                                using (TextBox txtActContr = (TextBox)GVITSec80C.Rows[i].FindControl("txtActContr"))
                                using (FileUpload fu = (FileUpload)GVITSec80C.Rows[i].FindControl("fuAttachments"))
                                using (TextBox txtRemarks = (TextBox)GVITSec80C.Rows[i].FindControl("txtRemarks"))
                                {
                                    if (txtPropContr != null)
                                    {
                                        if (txtActContr != null)
                                        {
                                            if (fu != null)
                                            {
                                                if (txtRemarks != null)
                                                {
                                                    date1 = DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss");

                                                    //  <%-- //select t8.SBSEC,t8.SBDIV,t8.SBDDS, t9.SDVLT,t9.TXEXM--%>
                                                    ITboObj.ICODE = decimal.Parse(GVITSec80C.DataKeys[i]["ICODE"].ToString());
                                                    ITboObj.PROPINVST = string.IsNullOrEmpty(txtPropContr.Text.ToString()) ? 0 : decimal.Parse(txtPropContr.Text.ToString());
                                                    ITboObj.ACTINVST = string.IsNullOrEmpty(txtActContr.Text.ToString()) ? 0 : decimal.Parse(txtActContr.Text.ToString());
                                                    ITboObj.EMPCOMMENTS = txtRemarks.Text;
                                                    ITboObj.RECEIPT_FID = fu.HasFile ? fu.PostedFile.FileName : "";
                                                    ITboObj.RECEIPT_FILE = fu.HasFile ? "YES" : "NO";
                                                    ITboObj.RECEIPT_FPATH = fu.HasFile ? "~/ITDoc/" + User.Identity.Name + "-" + date1 + Path.GetExtension(fu.FileName) : "";
                                                    if (fu.HasFile)
                                                    { fu.SaveAs(Server.MapPath("~/ITDoc/" + User.Identity.Name + "-" + date1) + Path.GetExtension(fu.FileName)); }
                                                    Dt.Rows.Add(ITHID, i + 1, ITboObj.ICODE, ITboObj.PROPINVST, ITboObj.ACTINVST, ITboObj.RECEIPT_FILE, ITboObj.RECEIPT_FID, ITboObj.RECEIPT_FPATH, ITboObj.EMPCOMMENTS);


                                                }

                                                else
                                                {
                                                    throw new Exception("-1");
                                                }
                                            }
                                            else
                                            {
                                                throw new Exception("-1");
                                            }
                                        }
                                        else
                                        {
                                            throw new Exception("-1");
                                        }
                                    }
                                    else
                                    {
                                        throw new Exception("-1");
                                    }
                                }
                            }
                        }

                        if (Dt != null)
                        {
                            if (Dt.Rows.Count > 0)
                            {
                                for (int y = 0; y < Dt.Rows.Count; y++)
                                {
                                    ITbl ITblObj1 = new ITbl();
                                    ITbo ITboObj1 = new ITbo();
                                    //ITHID, i, ITboObj.SBSEC, ITboObj.SBDIV, ITboObj.PROPCONTR, ITboObj.ACTCONTR,
                                    //ITboObj.RECEIPT_FILE, ITboObj.RECEIPT_FID, ITboObj.RECEIPT_FPATH, ITboObj.EMPCOMMENTS

                                    ITboObj1.ID = int.Parse(Dt.Rows[y]["ITHID"].ToString());
                                    ITboObj1.LID = int.Parse(Dt.Rows[y]["ITLID"].ToString());
                                    ITboObj1.ICODE = decimal.Parse(Dt.Rows[y]["ITCODE"].ToString());
                                    ITboObj1.PROPINVST = decimal.Parse(Dt.Rows[y]["PROPINVST"].ToString());
                                    ITboObj1.ACTINVST = decimal.Parse(Dt.Rows[y]["ACTINVST"].ToString());
                                    ITboObj1.RECEIPT_FILE = Dt.Rows[y]["RECEIPT_FILE"].ToString();
                                    ITboObj1.RECEIPT_FID = Dt.Rows[y]["RECEIPT_FID"].ToString();
                                    ITboObj1.RECEIPT_FPATH = Dt.Rows[y]["RECEIPT_FPATH"].ToString();
                                    ITboObj1.EMPCOMMENTS = Dt.Rows[y]["EMPCOMMENTS"].ToString();
                                    ITboObj1.Flag = 1;


                                    //ITblObj.Create_ITSection80Transaction(int.Parse(Dt.Rows[y]["ITHID"].ToString()), int.Parse(Dt.Rows[y]["ITLID"].ToString()),
                                    //decimal.Parse(Dt.Rows[y]["SBSEC"].ToString()), decimal.Parse(Dt.Rows[y]["SBDIV"].ToString()),
                                    //decimal.Parse(Dt.Rows[y]["PROPCONTR"].ToString()), decimal.Parse(Dt.Rows[y]["ACTCONTR"].ToString()),
                                    //Dt.Rows[y]["RECEIPT_FILE"].ToString(), Dt.Rows[y]["RECEIPT_FID"].ToString(), Dt.Rows[y]["RECEIPT_FPATH"].ToString(),
                                    //Dt.Rows[y]["EMPCOMMENTS"].ToString());

                                    ITblObj.Create_ITSection80CTransaction(ITboObj1, ref sts);
                                    GetFinancialDates();
                                    LoadGrid();
                                    btnSubmitClaims.Visible = false;
                                    BtnUpdate.Visible = false;
                                    BtnCancel.Visible = false;
                                    BtnEdit.Visible = true;
                                }
                            }
                        }
                        //ITblObj.Create_ITSection80Trans(Dt);
                        if(sts == true)
                        {
                            SendMailSec80C(ITHID, "Submit"); 
                        }
                    }
                }

                catch (Exception Ex)
                {
                    switch (Ex.Message)
                    {
                        case "-1":
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Invalid');", true);
                            MsgCls("Invalid", lblMessageBoard, System.Drawing.Color.Red);
                            return;
                            break;

                        case "-10":
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Record Already Exists');", true);
                            MsgCls("Record Already Exists", lblMessageBoard, System.Drawing.Color.Red);
                            return;
                            break;
                        default:
                            break;
                    }
                }





                ////////string date1;
                ////////if (GVITSec80C.Rows.Count > 0)
                ////////{
                ////////    for (int i = 0; i < GVITSec80C.Rows.Count - 1; i++)
                ////////    {
                ////////        using (TextBox txtPropContr = (TextBox)GVITSec80C.Rows[i].FindControl("txtPropContr"))
                ////////        using (TextBox txtActContr = (TextBox)GVITSec80C.Rows[i].FindControl("txtActContr"))
                ////////        using (FileUpload fu = (FileUpload)GVITSec80C.Rows[i].FindControl("fuAttachments"))
                ////////        using (TextBox txtRemarks = (TextBox)GVITSec80C.Rows[i].FindControl("txtRemarks"))
                ////////        {
                ////////            date1 = DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss");
                ////////            ITbl ITblObj = new ITbl();
                ////////            ITbo ITboObj = new ITbo();
                ////////            //  <%-- //select t8.SBSEC,t8.SBDIV,t8.SBDDS, t9.SDVLT,t9.TXEXM--%>
                ////////            ITboObj.ICODE = decimal.Parse(GVITSec80C.DataKeys[i]["ICODE"].ToString());
                ////////            ITboObj.PROPCONTR = string.IsNullOrEmpty(txtPropContr.Text.ToString()) ? 0 : decimal.Parse(txtPropContr.Text.ToString());
                ////////            ITboObj.ACTCONTR = string.IsNullOrEmpty(txtActContr.Text.ToString()) ? 0 : decimal.Parse(txtPropContr.Text.ToString());
                ////////            ITboObj.REMARKS = txtRemarks.Text;
                ////////            ITboObj.RECEIPT_FID = fu.HasFile ? fu.PostedFile.FileName : "";
                ////////            ITboObj.RECEIPT_FILE = fu.HasFile ? "YES" : "NO";
                ////////            ITboObj.RECEIPT_FPATH = fu.HasFile ? "~/FBPDoc/" + User.Identity.Name + "-" + date1 + Path.GetExtension(fu.FileName) : "";


                ////////        }
                ////////    }
                ////////}
            
        }


        private void SendMailSec80C(int? ITHID, string type)
        {
            try
            {
                string strSubject = string.Empty;
                string RecipientsString = string.Empty;
                string strPernr_Mail = string.Empty;
                //string APPROVED_BY1 = "";
                //string Approver_Name = "";
                //string Approver_Email = "";
                string EMP_Name = "";
                string EMP_Email = "";

                ITdalDataContext objcontext = new ITdalDataContext();

                objcontext.usp_IT_Get_MailListSec80(ITHID, User.Identity.Name, ref EMP_Name, ref EMP_Email);

                strSubject = "Income Tax Request " + ITHID + " has been " + type + " by " + EMP_Name + "  |  " + User.Identity.Name + " and is pending for the Approval.";

                RecipientsString = "monica.ks@itchamps.com";
                strPernr_Mail = "latha.mg@itchamps.com";

                //    //Preparing the mail body--------------------------------------------------
                string body = "<b>Income Tax Request " + ITHID + " has been " + type + " by " + EMP_Name + "  |  " + User.Identity.Name + " and is pending for the Approval.<br/><br/></b>";
                body += "<b>Income Tax ID  :  " + ITHID + "</b><br/><br/>";
                body += "<b>Income Tax Type :  " + "Section 80 C" + "</b><br/><br/>";

                //    //End of preparing the mail body-------------------------------------------
                iEmpPowerMaster_Load.masterbl.DispatchMail(RecipientsString, User.Identity.Name, strSubject, strPernr_Mail, body);
                lblMessageBoard.ForeColor = System.Drawing.Color.Green;
                lblMessageBoard.Text = "Income Tax Request submitted successfully and Mail sent successfully.";
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        } 

        private DataTable GetSec80CDt()
        {
            try
            {
                DataTable Dt = new DataTable("ITSEC80C");
                Dt.Columns.Add("ITHID", typeof(int));
                Dt.Columns.Add("ITLID", typeof(int));
                Dt.Columns.Add("ITCODE", typeof(decimal));
                Dt.Columns.Add("PROPINVST", typeof(decimal));
                Dt.Columns.Add("ACTINVST", typeof(decimal));
                Dt.Columns.Add("RECEIPT_FILE", typeof(string));
                Dt.Columns.Add("RECEIPT_FID", typeof(string));
                Dt.Columns.Add("RECEIPT_FPATH", typeof(string));
                Dt.Columns.Add("EMPCOMMENTS", typeof(string));

                return Dt;
            }
            catch (Exception Ex)
            { throw Ex; return null; }
        }

        protected void CB_ConsAct_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                string type = string.Empty;
                if (CB_ConsAct.Checked)
                {
                    type = "1";
                }
                else
                {
                    type = "0";
                }
                ConsiderActProp(type);
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        }


        public void ConsiderActProp(string typ)
        {
            try
            {
                if (GVITSec80C.Rows.Count > 0)
                {
                    for (int i = 0; i < GVITSec80C.Rows.Count; i++)
                    {

                        using (TextBox txtPropContr = (TextBox)GVITSec80C.Rows[i].FindControl("txtPropContr"))
                        using (TextBox txtActContr = (TextBox)GVITSec80C.Rows[i].FindControl("txtActContr"))
                        {
                            if (txtPropContr != null)
                            {
                                if (txtActContr != null)
                                {
                                    if (typ.Trim() == "1")
                                    {
                                        txtActContr.Enabled = true;
                                        txtPropContr.Enabled = false;
                                    }
                                    else if (typ.Trim() == "0")
                                    {
                                        txtActContr.Enabled = false;
                                        txtPropContr.Enabled = true;
                                    }
                                }
                                else
                                {
                                    throw new Exception("-1");
                                }

                            }
                            else
                            {
                                throw new Exception("-1");
                            }

                        }
                    }
                }

            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        }





        protected void GVITSec80C_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName.ToUpper())
                {
                    case "DOWNLOAD":
                        //  string filename= grd_IExpInfo.DataKeys[int.Parse(e.CommandArgument.ToString())]["RECEIPT_FPATH"].ToString();
                        string filePath = e.CommandArgument.ToString();
                        Response.ContentType = ContentType;
                        Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
                        Response.WriteFile(filePath);
                        Response.End();
                        break;

                    //case "UPLOAD":

                    //    string date1 = DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss");

                    //    ITbl ITObjbl = new ITbl();
                    //    List<ITbo> ITObjboList = new List<ITbo>();
                    //    ITbo ITObjbo = new ITbo();


                    //    int ID = int.Parse(GVITSec80C.DataKeys[int.Parse(e.CommandArgument.ToString())]["ID"].ToString());
                    //    int LID = int.Parse(GVITSec80C.DataKeys[int.Parse(e.CommandArgument.ToString())]["LID"].ToString());

                    //    ITObjbo.ID = ID;
                    //    ITObjbo.LID = LID;
                    //    ITObjbo.CREATED_BY = User.Identity.Name;
                    //    using (FileUpload fu = (FileUpload)GVITSec80C.Rows[int.Parse(e.CommandArgument.ToString())].FindControl("fuAttachments"))
                    //    {
                    //        ITObjbo.RECEIPT_FILE = fu.HasFile ? "YES" : "NO";
                    //        ITObjbo.RECEIPT_FID = fu.HasFile ? fu.PostedFile.FileName : "";
                    //        ITObjbo.RECEIPT_FPATH = fu.HasFile ? "~/ITDoc/" + User.Identity.Name + "-" + date1 + Path.GetExtension(fu.FileName) : "";


                    //        if (fu.HasFile)
                    //        { fu.SaveAs(Server.MapPath("~/ITDoc/" + User.Identity.Name + "-" + date1) + Path.GetExtension(fu.FileName)); }
                    //        ITObjbl.ITSec80C_fileUpdate(ITObjbo);

                    //        using (LinkButton ltnfu = (LinkButton)GVITSec80C.Rows[int.Parse(e.CommandArgument.ToString())].FindControl("LbtnUpload"))
                    //        {
                    //            ltnfu.Visible = false;

                    //        }
                    //        LoadGrid();

                    //    }
                    //    LoadGrid();
                    //    break;

                    case "DELETE":


                        ITbl ITObjbld = new ITbl();
                        List<ITbo> ITObjboListd = new List<ITbo>();
                        ITbo ITObjbod = new ITbo();

                        //string date2 = DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss");

                        int IDd = int.Parse(GVITSec80C.DataKeys[int.Parse(e.CommandArgument.ToString())]["ID"].ToString());
                        int LIDd = int.Parse(GVITSec80C.DataKeys[int.Parse(e.CommandArgument.ToString())]["LID"].ToString());

                        ITObjbod.ID = IDd;
                        ITObjbod.LID = LIDd;
                        ITObjbod.CREATED_BY = User.Identity.Name;


                        ITObjbld.ITSec80C_fileDelete(ITObjbod);


                        //using (LinkButton ltnfu = (LinkButton)GVITSec80C.Rows[int.Parse(e.CommandArgument.ToString())].FindControl("LbtnUpload"))
                        //{
                        //    ltnfu.Visible = false;
                        //}

                        LoadGrid();


                        break;




                    default:
                        break;
                }
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        }

        protected void GVITSec80C_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {

                int RecordCountu = 0;

                if (GVITSec80C.Rows.Count > 0)
                {
                    for (int i = 0; i < GVITSec80C.Rows.Count; i++)
                    {
                        using (TextBox txtPropContr = (TextBox)GVITSec80C.Rows[i].FindControl("txtPropContr"))
                        using (TextBox txtActContr = (TextBox)GVITSec80C.Rows[i].FindControl("txtActContr"))
                        {
                            if (((!string.IsNullOrEmpty(txtPropContr.Text)) && (decimal.Parse(txtPropContr.Text.ToString().Trim()) > 0)) ||
                                ((!string.IsNullOrEmpty(txtActContr.Text)) && (decimal.Parse(txtActContr.Text.ToString().Trim()) > 0)))
                            {
                                RecordCountu = RecordCountu + 1;
                            }

                        }
                    }
                }

                if (RecordCountu <= 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Please enter atleast one record before submiting');", true);
                    MsgCls("Please enter atleast one record before submiting", lblMessageBoard, System.Drawing.Color.Red);
                    return;
                }



                bool? stus = false;
                int? ITHID = 0;
                ITbl ITblObj = new ITbl();
                ITbo ITboObj = new ITbo();

                ITboObj.CREATED_BY = User.Identity.Name;
                ITboObj.PERNR = User.Identity.Name;
                ITboObj.STATUS = "Updated";
                ITboObj.CREATED_ON = DateTime.Now;
                ITboObj.BEGDA = DateTime.Now;
                ITboObj.ENDDA = DateTime.Now;
                ITboObj.MODIFIED_BY = User.Identity.Name;
                ITboObj.Flag = 2;
                ITboObj.ID = int.Parse(ViewState["ID"].ToString().Trim());
                ITboObj.CONACTPROP = CB_ConsAct.Checked ? "1" : "0";
                ITblObj.Create_ITSECTION80CHEADR(ITboObj, ref ITHID);

                if (ITHID != null)
                {
                    string date1;
                    DataTable Dtu = GetSec80CDt();
                    if (GVITSec80C.Rows.Count > 0)
                    {
                        for (int i = 0; i < GVITSec80C.Rows.Count; i++)
                        {
                            using (TextBox txtPropContr = (TextBox)GVITSec80C.Rows[i].FindControl("txtPropContr"))
                            using (TextBox txtActContr = (TextBox)GVITSec80C.Rows[i].FindControl("txtActContr"))
                            using (FileUpload fu = (FileUpload)GVITSec80C.Rows[i].FindControl("fuAttachments"))
                            using (TextBox txtRemarks = (TextBox)GVITSec80C.Rows[i].FindControl("txtRemarks"))
                            {
                                if (txtPropContr != null)
                                {
                                    if (txtActContr != null)
                                    {
                                        if (fu != null)
                                        {
                                            if (txtRemarks != null)
                                            {
                                                date1 = DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss");

                                                //  <%-- //select t8.SBSEC,t8.SBDIV,t8.SBDDS, t9.SDVLT,t9.TXEXM--%>
                                                ITboObj.ICODE = decimal.Parse(GVITSec80C.DataKeys[i]["ICODE"].ToString());
                                                ITboObj.PROPINVST = string.IsNullOrEmpty(txtPropContr.Text.ToString()) ? 0 : decimal.Parse(txtPropContr.Text.ToString());
                                                ITboObj.ACTINVST = string.IsNullOrEmpty(txtActContr.Text.ToString()) ? 0 : decimal.Parse(txtActContr.Text.ToString());
                                                ITboObj.EMPCOMMENTS = txtRemarks.Text;
                                                ITboObj.RECEIPT_FID = fu.HasFile ? fu.PostedFile.FileName : "";
                                                ITboObj.RECEIPT_FILE = fu.HasFile ? "YES" : "NO";
                                                ITboObj.RECEIPT_FPATH = fu.HasFile ? "~/ITDoc/" + User.Identity.Name + "-" + date1 + Path.GetExtension(fu.FileName) : "";

                                                if (fu.HasFile)
                                                { fu.SaveAs(Server.MapPath("~/ITDoc/" + User.Identity.Name + "-" + date1) + Path.GetExtension(fu.FileName)); }
                                                Dtu.Rows.Add(ITHID, i + 1, ITboObj.ICODE, ITboObj.PROPINVST, ITboObj.ACTINVST, ITboObj.RECEIPT_FILE, ITboObj.RECEIPT_FID, ITboObj.RECEIPT_FPATH, ITboObj.EMPCOMMENTS);


                                            }

                                            else
                                            {
                                                throw new Exception("-1");
                                            }
                                        }
                                        else
                                        {
                                            throw new Exception("-1");
                                        }
                                    }
                                    else
                                    {
                                        throw new Exception("-1");
                                    }
                                }
                                else
                                {
                                    throw new Exception("-1");
                                }
                            }
                        }
                    }

                    if (Dtu != null)
                    {
                        if (Dtu.Rows.Count > 0)
                        {
                            for (int y = 0; y < Dtu.Rows.Count; y++)
                            {
                                ITbl ITblObj1 = new ITbl();
                                ITbo ITboObj1 = new ITbo();
                                //ITHID, i, ITboObj.SBSEC, ITboObj.SBDIV, ITboObj.PROPCONTR, ITboObj.ACTCONTR,
                                //ITboObj.RECEIPT_FILE, ITboObj.RECEIPT_FID, ITboObj.RECEIPT_FPATH, ITboObj.EMPCOMMENTS

                                ITboObj1.ID = int.Parse(Dtu.Rows[y]["ITHID"].ToString());
                                ITboObj1.LID = int.Parse(Dtu.Rows[y]["ITLID"].ToString());
                                ITboObj1.ICODE = decimal.Parse(Dtu.Rows[y]["ITCODE"].ToString());
                                ITboObj1.PROPINVST = decimal.Parse(Dtu.Rows[y]["PROPINVST"].ToString());
                                ITboObj1.ACTINVST = decimal.Parse(Dtu.Rows[y]["ACTINVST"].ToString());
                                ITboObj1.RECEIPT_FILE = Dtu.Rows[y]["RECEIPT_FILE"].ToString();
                                ITboObj1.RECEIPT_FID = Dtu.Rows[y]["RECEIPT_FID"].ToString();
                                ITboObj1.RECEIPT_FPATH = Dtu.Rows[y]["RECEIPT_FPATH"].ToString();
                                ITboObj1.EMPCOMMENTS = Dtu.Rows[y]["EMPCOMMENTS"].ToString();
                                ITboObj1.Flag = 2;


                                //ITblObj.Create_ITSection80Transaction(int.Parse(Dt.Rows[y]["ITHID"].ToString()), int.Parse(Dt.Rows[y]["ITLID"].ToString()),
                                //decimal.Parse(Dt.Rows[y]["SBSEC"].ToString()), decimal.Parse(Dt.Rows[y]["SBDIV"].ToString()),
                                //decimal.Parse(Dt.Rows[y]["PROPCONTR"].ToString()), decimal.Parse(Dt.Rows[y]["ACTCONTR"].ToString()),
                                //Dt.Rows[y]["RECEIPT_FILE"].ToString(), Dt.Rows[y]["RECEIPT_FID"].ToString(), Dt.Rows[y]["RECEIPT_FPATH"].ToString(),
                                //Dt.Rows[y]["EMPCOMMENTS"].ToString());

                                ITblObj.Create_ITSection80CTransaction(ITboObj1,ref stus);
                                GetFinancialDates();
                                LoadGrid();
                                btnSubmitClaims.Visible = false;
                                BtnUpdate.Visible = false;
                                BtnCancel.Visible = false;
                                BtnEdit.Visible = true;
                            }
                        }
                    }
                    //ITblObj.Create_ITSection80Trans(Dt);

                    if(stus == true)
                    {
                        SendMailSec80C(int.Parse(ViewState["ID"].ToString().Trim()), "Updated");
                    }
                }

            }
            catch (Exception Ex)
            {

                switch (Ex.Message)
                {
                    case "-1":
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Invalid');", true);
                        MsgCls("Invalid", lblMessageBoard, System.Drawing.Color.Red);
                        return;
                        break;

                    case "-10":
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Record Already Exists');", true);
                        MsgCls("Record Already Exists", lblMessageBoard, System.Drawing.Color.Red);
                        return;
                        break;
                    default:
                        break;
                }
            }
        }

        protected void BtnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                EnableControls(CB_ConsAct.Checked ? "1" : "0");
                btnSubmitClaims.Visible = false;
                BtnUpdate.Visible = true;
                BtnCancel.Visible = true;
                BtnEdit.Visible = false;
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        }

        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                GetFinancialDates();
                LoadGrid();
                btnSubmitClaims.Visible = false;
                BtnUpdate.Visible = false;
                BtnCancel.Visible = false;
                BtnEdit.Visible = true;
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        }
    }
}