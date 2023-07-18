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
    public partial class IT_Declaration : System.Web.UI.Page
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
                    DivSec80.Visible = true;
                    GetFinancialDates();
                    LoadGrid();
                    
                }
                else if (itlock == true)
                {
                    DivSec80.Visible = false;
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
                ITboObj1 = ITblObj.Load_IT_Section80Details(User.Identity.Name, ref count);
                Session.Add("ITSec80GrdInfo", ITboObj1);

                if (ITboObj1 == null || ITboObj1.Count == 0)
                {
                    MsgCls("No Records Found !", lblMessageBoard, Color.Red);
                    GVITSec80.Visible = false;
                    GVITSec80.DataSource = null;
                    GVITSec80.DataBind();
                    return;
                }
                else
                {
                    GVITSec80.Visible = true;
                    GVITSec80.DataSource = ITboObj1;
                    GVITSec80.SelectedIndex = -1;
                    GVITSec80.DataBind();
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

                  if (GVITSec80.Rows.Count > 0)
                  {
                      for (int i = 0; i <= GVITSec80.Rows.Count - 1; i++)
                      {
                         // using (LinkButton UpdateBtn = (LinkButton)GVITSec80.Rows[i].FindControl("LbtnUpload"))
                          using (LinkButton DeleteBtn = (LinkButton)GVITSec80.Rows[i].FindControl("LbtnDelete"))
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
                if (GVITSec80.Rows.Count > 0)
                {
                    for (int i = 0; i <= GVITSec80.Rows.Count - 1; i++)
                    {
                        using (TextBox txtPropContr = (TextBox)GVITSec80.Rows[i].FindControl("txtPropContr"))
                        using (TextBox txtActContr = (TextBox)GVITSec80.Rows[i].FindControl("txtActContr"))
                        using (FileUpload fu = (FileUpload)GVITSec80.Rows[i].FindControl("fuAttachments"))
                        using (TextBox txtRemarks = (TextBox)GVITSec80.Rows[i].FindControl("txtRemarks"))
                       // using (LinkButton UpdateBtn = (LinkButton)GVITSec80.Rows[i].FindControl("LbtnUpload"))
                        using (LinkButton DeleteBtn = (LinkButton)GVITSec80.Rows[i].FindControl("LbtnDelete"))
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
                                            //UpdateBtn.Enabled = false;
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
                if (GVITSec80.Rows.Count > 0)
                {
                    for (int i = 0; i <= GVITSec80.Rows.Count - 1; i++)
                    {
                        using (TextBox txtPropContr = (TextBox)GVITSec80.Rows[i].FindControl("txtPropContr"))
                        using (TextBox txtActContr = (TextBox)GVITSec80.Rows[i].FindControl("txtActContr"))
                        using (FileUpload fu = (FileUpload)GVITSec80.Rows[i].FindControl("fuAttachments"))
                        using (TextBox txtRemarks = (TextBox)GVITSec80.Rows[i].FindControl("txtRemarks")) 
                        //using (LinkButton UpdateBtn = (LinkButton)GVITSec80.Rows[i].FindControl("LbtnUpload"))
                        using (LinkButton DeleteBtn = (LinkButton)GVITSec80.Rows[i].FindControl("LbtnDelete"))
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
                                               // UpdateBtn.Enabled = true;
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

                if (GVITSec80.Rows.Count > 0)
                {
                    for (int i = 0; i < GVITSec80.Rows.Count; i++)
                    {
                            using (TextBox txtPropContr = (TextBox)GVITSec80.Rows[i].FindControl("txtPropContr"))
                            using (TextBox txtActContr = (TextBox)GVITSec80.Rows[i].FindControl("txtActContr"))
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
                int rowid141 = 0;
                int rowid142 = 0;
                int rowid31 = 0;
                int rowid32 = 0;
                if (GVITSec80.Rows.Count > 0)
                {
                    for (int i = 0; i < GVITSec80.Rows.Count; i++)
                    {
                        if ((decimal.Parse(GVITSec80.DataKeys[i]["SBSEC"].ToString()) == 14) && (decimal.Parse(GVITSec80.DataKeys[i]["SBDIV"].ToString()) == 1))
                        {
                            rowid141 = i;
                        }
                        else if ((decimal.Parse(GVITSec80.DataKeys[i]["SBSEC"].ToString()) == 14) && (decimal.Parse(GVITSec80.DataKeys[i]["SBDIV"].ToString()) == 2))
                        {
                            rowid142 = i;
                        }
                        else if ((decimal.Parse(GVITSec80.DataKeys[i]["SBSEC"].ToString()) == 3) && (decimal.Parse(GVITSec80.DataKeys[i]["SBDIV"].ToString()) == 1))
                        {
                            rowid31 = i;
                        }
                        else if ((decimal.Parse(GVITSec80.DataKeys[i]["SBSEC"].ToString()) == 3) && (decimal.Parse(GVITSec80.DataKeys[i]["SBDIV"].ToString()) == 2))
                        {
                            rowid32 = i;
                        }

                    }
                }

                using (TextBox txtPropContr141 = (TextBox)GVITSec80.Rows[rowid141].FindControl("txtPropContr"))
                using (TextBox txtActContr141 = (TextBox)GVITSec80.Rows[rowid141].FindControl("txtActContr"))

                using (TextBox txtPropContr142 = (TextBox)GVITSec80.Rows[rowid142].FindControl("txtPropContr"))
                using (TextBox txtActContr142 = (TextBox)GVITSec80.Rows[rowid142].FindControl("txtActContr"))

                using (TextBox txtPropContr31 = (TextBox)GVITSec80.Rows[rowid31].FindControl("txtPropContr"))
                using (TextBox txtActContr31 = (TextBox)GVITSec80.Rows[rowid31].FindControl("txtActContr"))

                using (TextBox txtPropContr32 = (TextBox)GVITSec80.Rows[rowid32].FindControl("txtPropContr"))
                using (TextBox txtActContr32 = (TextBox)GVITSec80.Rows[rowid32].FindControl("txtActContr"))
                {
                    if (((!string.IsNullOrEmpty(txtPropContr141.Text.ToString())) && (decimal.Parse(txtPropContr141.Text.ToString().Trim()) > 0)) && ((!string.IsNullOrEmpty(txtPropContr142.Text.ToString())) && (decimal.Parse(txtPropContr142.Text.ToString().Trim()) > 0)))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Cannot fil both Deduction for self disability and Deduction for self severe disability');", true);
                        MsgCls("Cannot fil both Deduction for self disability and Deduction for self severe disability", lblMessageBoard, System.Drawing.Color.Red);
                        return;
                    }

                    if (((!string.IsNullOrEmpty(txtPropContr31.Text.ToString())) && (decimal.Parse(txtPropContr31.Text.ToString().Trim()) > 0)) && ((!string.IsNullOrEmpty(txtPropContr32.Text.ToString())) && (decimal.Parse(txtPropContr32.Text.ToString().Trim()) > 0)))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Cannot fil both Deduction for dependent with disability and Deduction for dependent with severe disability');", true);
                        MsgCls("Cannot fil both Deduction for dependent with disability and Deduction for dependent with severe disability", lblMessageBoard, System.Drawing.Color.Red);
                        return;
                    }
                }
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
                ITblObj.Create_ITSECTION80HEADR(ITboObj, ref ITHID);

                if (ITHID != null)
                {
                    string date1;
                    DataTable Dt = GetSec80Dt();
                    if (GVITSec80.Rows.Count > 0)
                    {
                        for (int i = 0; i < GVITSec80.Rows.Count; i++)
                        {
                            using (TextBox txtPropContr = (TextBox)GVITSec80.Rows[i].FindControl("txtPropContr"))
                            using (TextBox txtActContr = (TextBox)GVITSec80.Rows[i].FindControl("txtActContr"))
                            using (FileUpload fu = (FileUpload)GVITSec80.Rows[i].FindControl("fuAttachments"))
                            using (TextBox txtRemarks = (TextBox)GVITSec80.Rows[i].FindControl("txtRemarks"))
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
                                                ITboObj.SBSEC = decimal.Parse(GVITSec80.DataKeys[i]["SBSEC"].ToString());
                                                ITboObj.SBDIV = decimal.Parse(GVITSec80.DataKeys[i]["SBDIV"].ToString());
                                                ITboObj.PROPCONTR = string.IsNullOrEmpty(txtPropContr.Text.ToString()) ? 0 : decimal.Parse(txtPropContr.Text.ToString());
                                                ITboObj.ACTCONTR = string.IsNullOrEmpty(txtActContr.Text.ToString()) ? 0 : decimal.Parse(txtActContr.Text.ToString());
                                                ITboObj.EMPCOMMENTS = txtRemarks.Text;
                                                ITboObj.RECEIPT_FID = fu.HasFile ? fu.PostedFile.FileName : "";
                                                ITboObj.RECEIPT_FILE = fu.HasFile ? "YES" : "NO";
                                                ITboObj.RECEIPT_FPATH = fu.HasFile ? "~/ITDoc/" + User.Identity.Name + "-" + date1 + Path.GetExtension(fu.FileName) : "";
                                                if (fu.HasFile)
                                                { fu.SaveAs(Server.MapPath("~/ITDoc/" + User.Identity.Name + "-" + date1) + Path.GetExtension(fu.FileName)); }
                                                Dt.Rows.Add(ITHID, i + 1, ITboObj.SBSEC, ITboObj.SBDIV, ITboObj.PROPCONTR, ITboObj.ACTCONTR, ITboObj.RECEIPT_FILE, ITboObj.RECEIPT_FID, ITboObj.RECEIPT_FPATH, ITboObj.EMPCOMMENTS);


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
                                ITboObj1.SBSEC = decimal.Parse(Dt.Rows[y]["SBSEC"].ToString());
                                ITboObj1.SBDIV = decimal.Parse(Dt.Rows[y]["SBDIV"].ToString());
                                ITboObj1.PROPCONTR = decimal.Parse(Dt.Rows[y]["PROPCONTR"].ToString());
                                ITboObj1.ACTCONTR = decimal.Parse(Dt.Rows[y]["ACTCONTR"].ToString());
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

                                ITblObj.Create_ITSection80Transaction(ITboObj1, ref sts);
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

                    if (sts == true)
                    {
                        SendMailSec80(ITHID,"Submit");
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

        private void SendMailSec80(int? ITHID, string type)
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
               body += "<b>Income Tax Type :  " + "Section 80" + "</b><br/><br/>";
             
               //    //End of preparing the mail body-------------------------------------------
               iEmpPowerMaster_Load.masterbl.DispatchMail(RecipientsString, User.Identity.Name, strSubject, strPernr_Mail, body);
               lblMessageBoard.ForeColor = System.Drawing.Color.Green;
               lblMessageBoard.Text = "Income Tax Request submitted successfully and Mail sent successfully.";
           }
           catch (Exception Ex)
           { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        }


        private DataTable GetSec80Dt()
        {
            try
            {
                DataTable Dt = new DataTable("ITSEC80");
                Dt.Columns.Add("ITHID", typeof(int));
                Dt.Columns.Add("ITLID", typeof(int));
                Dt.Columns.Add("SBSEC", typeof(decimal));
                Dt.Columns.Add("SBDIV", typeof(decimal));
                Dt.Columns.Add("PROPCONTR", typeof(decimal));
                Dt.Columns.Add("ACTCONTR", typeof(decimal));
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
                if (GVITSec80.Rows.Count > 0)
                {
                    for (int i = 0; i < GVITSec80.Rows.Count; i++)
                    {

                        using (TextBox txtPropContr = (TextBox)GVITSec80.Rows[i].FindControl("txtPropContr"))
                        using (TextBox txtActContr = (TextBox)GVITSec80.Rows[i].FindControl("txtActContr"))
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

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                int RecordCountu = 0;

                if (GVITSec80.Rows.Count > 0)
                {
                    for (int i = 0; i < GVITSec80.Rows.Count; i++)
                    {
                        using (TextBox txtPropContr = (TextBox)GVITSec80.Rows[i].FindControl("txtPropContr"))
                        using (TextBox txtActContr = (TextBox)GVITSec80.Rows[i].FindControl("txtActContr"))
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
                int rowid141 = 0;
                int rowid142 = 0;
                int rowid31 = 0;
                int rowid32 = 0;
                if (GVITSec80.Rows.Count > 0)
                {
                    for (int i = 0; i < GVITSec80.Rows.Count; i++)
                    {
                        if ((decimal.Parse(GVITSec80.DataKeys[i]["SBSEC"].ToString()) == 14) && (decimal.Parse(GVITSec80.DataKeys[i]["SBDIV"].ToString()) == 1))
                        {
                            rowid141 = i;
                        }
                        else if ((decimal.Parse(GVITSec80.DataKeys[i]["SBSEC"].ToString()) == 14) && (decimal.Parse(GVITSec80.DataKeys[i]["SBDIV"].ToString()) == 2))
                        {
                            rowid142 = i;
                        }
                        else if ((decimal.Parse(GVITSec80.DataKeys[i]["SBSEC"].ToString()) == 3) && (decimal.Parse(GVITSec80.DataKeys[i]["SBDIV"].ToString()) == 1))
                        {
                            rowid31 = i;
                        }
                        else if ((decimal.Parse(GVITSec80.DataKeys[i]["SBSEC"].ToString()) == 3) && (decimal.Parse(GVITSec80.DataKeys[i]["SBDIV"].ToString()) == 2))
                        {
                            rowid32 = i;
                        }

                    }
                }

                using (TextBox txtPropContr141 = (TextBox)GVITSec80.Rows[rowid141].FindControl("txtPropContr"))
                using (TextBox txtActContr141 = (TextBox)GVITSec80.Rows[rowid141].FindControl("txtActContr"))

                using (TextBox txtPropContr142 = (TextBox)GVITSec80.Rows[rowid142].FindControl("txtPropContr"))
                using (TextBox txtActContr142 = (TextBox)GVITSec80.Rows[rowid142].FindControl("txtActContr"))

                using (TextBox txtPropContr31 = (TextBox)GVITSec80.Rows[rowid31].FindControl("txtPropContr"))
                using (TextBox txtActContr31 = (TextBox)GVITSec80.Rows[rowid31].FindControl("txtActContr"))

                using (TextBox txtPropContr32 = (TextBox)GVITSec80.Rows[rowid32].FindControl("txtPropContr"))
                using (TextBox txtActContr32 = (TextBox)GVITSec80.Rows[rowid32].FindControl("txtActContr"))
                {
                    if (((!string.IsNullOrEmpty(txtPropContr141.Text.ToString())) && (decimal.Parse(txtPropContr141.Text.ToString().Trim()) > 0)) && ((!string.IsNullOrEmpty(txtPropContr142.Text.ToString())) && (decimal.Parse(txtPropContr142.Text.ToString().Trim()) > 0)))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Cannot fil both Deduction for self disability and Deduction for self severe disability');", true);
                        MsgCls("Cannot fil both Deduction for self disability and Deduction for self severe disability", lblMessageBoard, System.Drawing.Color.Red);
                        return;
                    }

                    if (((!string.IsNullOrEmpty(txtPropContr31.Text.ToString())) && (decimal.Parse(txtPropContr31.Text.ToString().Trim()) > 0)) && ((!string.IsNullOrEmpty(txtPropContr32.Text.ToString())) && (decimal.Parse(txtPropContr32.Text.ToString().Trim()) > 0)))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Cannot fil both Deduction for dependent with disability and Deduction for dependent with severe disability');", true);
                        MsgCls("Cannot fil both Deduction for dependent with disability and Deduction for dependent with severe disability", lblMessageBoard, System.Drawing.Color.Red);
                        return;
                    }
                }
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
                ITblObj.Create_ITSECTION80HEADR(ITboObj, ref ITHID);

                if (ITHID != null)
                {
                    string date1; 
                    DataTable Dtu = GetSec80Dt();
                    if (GVITSec80.Rows.Count > 0)
                    {
                        for (int i = 0; i < GVITSec80.Rows.Count; i++)
                        {
                            using (TextBox txtPropContr = (TextBox)GVITSec80.Rows[i].FindControl("txtPropContr"))
                            using (TextBox txtActContr = (TextBox)GVITSec80.Rows[i].FindControl("txtActContr"))
                            using (FileUpload fu = (FileUpload)GVITSec80.Rows[i].FindControl("fuAttachments"))
                            using (TextBox txtRemarks = (TextBox)GVITSec80.Rows[i].FindControl("txtRemarks"))
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
                                                ITboObj.SBSEC = decimal.Parse(GVITSec80.DataKeys[i]["SBSEC"].ToString());
                                                ITboObj.SBDIV = decimal.Parse(GVITSec80.DataKeys[i]["SBDIV"].ToString());
                                                ITboObj.PROPCONTR = string.IsNullOrEmpty(txtPropContr.Text.ToString()) ? 0 : decimal.Parse(txtPropContr.Text.ToString());
                                                ITboObj.ACTCONTR = string.IsNullOrEmpty(txtActContr.Text.ToString()) ? 0 : decimal.Parse(txtActContr.Text.ToString());
                                                ITboObj.EMPCOMMENTS = txtRemarks.Text;
                                                ITboObj.RECEIPT_FID = fu.HasFile ? fu.PostedFile.FileName : "";
                                                ITboObj.RECEIPT_FILE = fu.HasFile ? "YES" : "NO";
                                                ITboObj.RECEIPT_FPATH = fu.HasFile ? "~/ITDoc/" + User.Identity.Name + "-" + date1 + Path.GetExtension(fu.FileName) : "";

                                                if (fu.HasFile)
                                                { fu.SaveAs(Server.MapPath("~/ITDoc/" + User.Identity.Name + "-" + date1) + Path.GetExtension(fu.FileName)); }
                                                Dtu.Rows.Add(ITHID, i + 1, ITboObj.SBSEC, ITboObj.SBDIV, ITboObj.PROPCONTR, ITboObj.ACTCONTR, ITboObj.RECEIPT_FILE, ITboObj.RECEIPT_FID, ITboObj.RECEIPT_FPATH, ITboObj.EMPCOMMENTS);


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
                                ITboObj1.SBSEC = decimal.Parse(Dtu.Rows[y]["SBSEC"].ToString());
                                ITboObj1.SBDIV = decimal.Parse(Dtu.Rows[y]["SBDIV"].ToString());
                                ITboObj1.PROPCONTR = decimal.Parse(Dtu.Rows[y]["PROPCONTR"].ToString());
                                ITboObj1.ACTCONTR = decimal.Parse(Dtu.Rows[y]["ACTCONTR"].ToString());
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

                                ITblObj.Create_ITSection80Transaction(ITboObj1, ref stus);
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

                    if (stus == true)
                    {
                        SendMailSec80(int.Parse(ViewState["ID"].ToString().Trim()),"Updated");
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
                EnableControls(CB_ConsAct.Checked? "1" : "0");
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

        protected void GVITSec80_RowCommand(object sender, GridViewCommandEventArgs e)
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


                    //    int ID = int.Parse(GVITSec80.DataKeys[int.Parse(e.CommandArgument.ToString())]["ID"].ToString());
                    //    int LID = int.Parse(GVITSec80.DataKeys[int.Parse(e.CommandArgument.ToString())]["LID"].ToString());

                    //    ITObjbo.ID = ID;
                    //    ITObjbo.LID = LID;
                    //    ITObjbo.CREATED_BY = User.Identity.Name;
                    //    using (FileUpload fu = (FileUpload)GVITSec80.Rows[int.Parse(e.CommandArgument.ToString())].FindControl("fuAttachments"))
                    //    {
                    //        ITObjbo.RECEIPT_FILE = fu.HasFile ? "YES" : "NO";
                    //        ITObjbo.RECEIPT_FID = fu.HasFile ? fu.PostedFile.FileName : "";
                    //        ITObjbo.RECEIPT_FPATH = fu.HasFile ? "~/ITDoc/" + User.Identity.Name + "-" + date1 + Path.GetExtension(fu.FileName) : "";
                           

                    //        if (fu.HasFile)
                    //        { fu.SaveAs(Server.MapPath("~/ITDoc/" + User.Identity.Name + "-" + date1) + Path.GetExtension(fu.FileName)); }
                    //        ITObjbl.ITSec80_fileUpdate(ITObjbo);  

                    //        using (LinkButton ltnfu = (LinkButton)GVITSec80.Rows[int.Parse(e.CommandArgument.ToString())].FindControl("LbtnUpload"))
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
                        ITbo ITObjbod= new ITbo(); 

                        //string date2 = DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss");

                        int IDd = int.Parse(GVITSec80.DataKeys[int.Parse(e.CommandArgument.ToString())]["ID"].ToString());
                        int LIDd = int.Parse(GVITSec80.DataKeys[int.Parse(e.CommandArgument.ToString())]["LID"].ToString());

                        ITObjbod.ID = IDd;
                        ITObjbod.LID = LIDd;
                        ITObjbod.CREATED_BY = User.Identity.Name;


                        ITObjbld.ITSec80_fileDelete(ITObjbod);


                        //using (LinkButton ltnfu = (LinkButton)GVITSec80.Rows[int.Parse(e.CommandArgument.ToString())].FindControl("LbtnUpload"))
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

        protected void GVITSec80_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }






    }
}