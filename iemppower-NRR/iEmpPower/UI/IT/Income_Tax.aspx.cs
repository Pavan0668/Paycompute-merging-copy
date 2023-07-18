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
using System.Configuration;
using System.Data.SqlClient;

namespace iEmpPower.UI.IT
{
    public partial class Income_Tax : System.Web.UI.Page
    {
        string constr = ConfigurationManager.ConnectionStrings["iEmpPowerConnectionString"].ConnectionString;

        protected void Page_PreRender(object sender, EventArgs e)
        {
            minmaxdate();
        }

        public void minmaxdate()
        {
            try
            {
                string month = DateTime.Today.Month.ToString();
                CPV_txtFromDate.ValueToCompare = DateTime.Parse(Session["DOJ"].ToString()).ToString("dd/MM/yyyy");
                CPV_txtToDate.ValueToCompare = DateTime.Parse(Session["DOJ"].ToString()).ToString("dd/MM/yyyy");
                if (int.Parse(month.Trim()) >= 4)
                {
                    RV_txtFromDate.MinimumValue = RV_txtITPreEmprFrmDt.MinimumValue = new DateTime(DateTime.Today.Year, 4, 1).ToString("dd/MM/yyyy");
                    RV_txtToDate.MinimumValue = RVtxtITPreEmprToDt.MinimumValue = new DateTime(DateTime.Today.Year, 4, 1).ToString("dd/MM/yyyy");
                    ViewState["mindate"] = new DateTime(DateTime.Today.Year, 4, 1).ToString("dd/MM/yyyy");
                }
                else
                {
                    RV_txtFromDate.MinimumValue = RV_txtITPreEmprFrmDt.MinimumValue = new DateTime(DateTime.Today.Year, 4, 1).AddYears(-1).ToString("dd/MM/yyyy");
                    RV_txtToDate.MinimumValue = RVtxtITPreEmprToDt.MinimumValue = new DateTime(DateTime.Today.Year, 4, 1).AddYears(-1).ToString("dd/MM/yyyy");
                    ViewState["mindate"] = new DateTime(DateTime.Today.Year, 4, 1).AddYears(-1).ToString("dd/MM/yyyy");
                }




                //RV_txtFromDate.MaximumValue = DateTime.Now.ToString("dd/MM/yyyy");
                //RV_txtFromDate.ErrorMessage = "From date should be between " + RV_txtFromDate.MinimumValue + "- " + RV_txtFromDate.MaximumValue;
                //RV_txtToDate.MaximumValue = DateTime.Now.ToString("dd/MM/yyyy");
                //RV_txtToDate.ErrorMessage = "To date should be between " + RV_txtToDate.MinimumValue + "- " + RV_txtToDate.MaximumValue;



                RV_txtFromDate.MaximumValue = RV_txtITPreEmprFrmDt.MaximumValue = new DateTime(DateTime.Today.Year, 3, 31).AddYears(1).ToString("dd/MM/yyyy");//DateTime.Now.ToString("dd/MM/yyyy");
                RV_txtFromDate.ErrorMessage = RV_txtITPreEmprFrmDt.ErrorMessage = "From date should be between " + RV_txtFromDate.MinimumValue + "- " + RV_txtFromDate.MaximumValue;
                RV_txtToDate.MaximumValue = RVtxtITPreEmprToDt.MaximumValue = new DateTime(DateTime.Today.Year, 3, 31).AddYears(1).ToString("dd/MM/yyyy");//DateTime.Now.AddYears(1).ToString("dd/MM/yyyy");
                RV_txtToDate.ErrorMessage = RVtxtITPreEmprToDt.ErrorMessage = "To date should be between " + RV_txtToDate.MinimumValue + "- " + RV_txtToDate.MaximumValue;
                ViewState["maxdate"] = new DateTime(DateTime.Today.Year, 3, 31).AddYears(1).ToString("dd/MM/yyyy");
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "'minmaxdate())", true); }



        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                minmaxdate();
                if (DateTime.Parse(Session["DOJ"].ToString()) > DateTime.Parse(ViewState["mindate"].ToString()) &&
                    DateTime.Parse(Session["DOJ"].ToString()) < DateTime.Parse(ViewState["maxdate"].ToString()))
                    Tab6.Visible = true;
                else
                {
                    Tab6.Visible = false;
                    Tab6.CssClass = "nav-link p-2";
                }
                if (!IsPostBack)
                {
                    if (Request.QueryString["PC"] != null)
                    {
                        if (Request.QueryString["PC"] == "C")
                        {
                            loadTab2();
                            goto displayInfo;
                        }
                        else if (Request.QueryString["PC"] == "H")
                        {
                            loadTab3();
                            goto displayInfo;
                        }

                        else if (Request.QueryString["PC"] == "O")
                        {
                            loadTab4();
                            goto displayInfo;
                        }
                        else if (Request.QueryString["PC"] == "V")
                        {
                            loadTab5();
                            goto displayInfo;
                        }
                        else if (Request.QueryString["PC"] == "P")
                        {
                            LoadTab6();
                            goto displayInfo;
                        }
                    }

                    Tab2.CssClass = "nav-link active p-2";
                    MainView.ActiveViewIndex = 1;


                ////minmaxdate();
                //LoadGrid();
                displayInfo: { }
                    pageLoadEvents();
                    GetFinancialDates();
                }

            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "'Page_Load())", true); }
        }

        protected void Tab1_Click(object sender, EventArgs e)
        {
            Tab1.CssClass = "nav-link active p-2";
            Tab2.CssClass = "nav-link p-2";
            Tab3.CssClass = "nav-link p-2";
            Tab4.CssClass = "nav-link p-2";
            Tab5.CssClass = "nav-link p-2";
            Tab6.CssClass = "nav-link p-2";
            ////Tab4.CssClass = "nav-link p-2";
            MainView.ActiveViewIndex = 0;
            ////LoadTravelClaimGridView();
            LoadGrid();
            lblMessageBoard.Text = "";
        }

        protected void Tab2_Click(object sender, EventArgs e)
        {
            loadTab2();
        }

        void loadTab2()
        {
            Tab1.CssClass = "nav-link p-2";
            Tab2.CssClass = "nav-link active p-2";
            Tab3.CssClass = "nav-link p-2";
            Tab4.CssClass = "nav-link p-2";
            Tab5.CssClass = "nav-link p-2";
            Tab6.CssClass = "nav-link p-2";
            Tab7.CssClass = "nav-link p-2";
            MainView.ActiveViewIndex = 1;
            //LoadPRRequestGridView();
            //LoadIExpenseMPGridView();
            pageLoadEvents2();
            lblMessageBoard.Text = "";
        }

        protected void Tab3_Click(object sender, EventArgs e)
        {
            loadTab3();
        }

        void loadTab3()
        {
            Tab1.CssClass = "nav-link p-2";
            Tab2.CssClass = "nav-link p-2";
            Tab3.CssClass = "nav-link active p-2";
            Tab4.CssClass = "nav-link p-2";
            Tab5.CssClass = "nav-link p-2";
            MainView.ActiveViewIndex = 2;
            Tab6.CssClass = "nav-link p-2";
            Tab7.CssClass = "nav-link p-2";
            //LoadPRRequestCompletedGridView();
            //LoadIExpenseMCGridView();
            pageLoadEvents3(); lblMessageBoard.Text = "";
        }

        protected void Tab4_Click(object sender, EventArgs e)
        {
            loadTab4();
        }

        void loadTab4()
        {
            Tab1.CssClass = "nav-link p-2";
            Tab2.CssClass = "nav-link p-2";
            Tab3.CssClass = "nav-link p-2";
            Tab4.CssClass = "nav-link active p-2";
            Tab5.CssClass = "nav-link p-2";
            Tab6.CssClass = "nav-link p-2";
            Tab7.CssClass = "nav-link p-2";
            MainView.ActiveViewIndex = 3;
            //LoadPRRequestCompletedGridView();
            //LoadIExpenseMCGridView();
            pageLoadEvents4(); lblMessageBoard.Text = "";
        }

        protected void Tab5_Click(object sender, EventArgs e)
        {
            loadTab5();
        }

        void loadTab5()
        {
            Tab6.CssClass = "nav-link p-2";
            Tab1.CssClass = "nav-link p-2";
            Tab2.CssClass = "nav-link p-2";
            Tab3.CssClass = "nav-link p-2";
            Tab4.CssClass = "nav-link p-2";
            Tab5.CssClass = "nav-link active p-2";
            Tab7.CssClass = "nav-link p-2";
            MainView.ActiveViewIndex = 4;
            //LoadPRRequestCompletedGridView();
            //LoadIExpenseMCGridView();
            pageLoadEvents5(); lblMessageBoard.Text = "";

            lnkDwnlITFile.Visible = checkITFileExts();
        }

        /*------------IT_DeclarationSec80 Starts------*/

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
                    LoadGrid2();

                }
                else if (itlock == true)
                {
                    DivSec80C.Visible = false;
                    DivSec80.Visible = false;
                    MsgCls("IT Declaration has been locked. Please contact Payroll Admin.", LblLockSts, Color.Red);
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('IT Declaration has been locked. Please contact Payroll Admin.')", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('IT Declaration has been locked. Please contact Payroll Admin.');", true);
                }




            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "'pageLoadEvents());", true); }
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
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "'LoadGrid())", true); }

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
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "'DisableControls())", true); }
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
                                                fu.Enabled = false;
                                                txtActContr.Enabled = false;
                                                txtRemarks.Enabled = true;
                                                CB_ConsAct.Enabled = true;
                                                // UpdateBtn.Enabled = true;
                                                DeleteBtn.Enabled = false;
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
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "'EnableControls())", true); }
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
                    //if (((!string.IsNullOrEmpty(txtPropContr141.Text.ToString())) && (decimal.Parse(txtPropContr141.Text.ToString().Trim()) > 0)) && ((!string.IsNullOrEmpty(txtPropContr142.Text.ToString())) && (decimal.Parse(txtPropContr142.Text.ToString().Trim()) > 0)))
                    //{
                    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Cannot fil both Deduction for self disability and Deduction for self severe disability');", true);
                    //    MsgCls("Cannot fil both Deduction for self disability and Deduction for self severe disability", lblMessageBoard, System.Drawing.Color.Red);
                    //    return;
                    //}

                    //if (((!string.IsNullOrEmpty(txtPropContr31.Text.ToString())) && (decimal.Parse(txtPropContr31.Text.ToString().Trim()) > 0)) && ((!string.IsNullOrEmpty(txtPropContr32.Text.ToString())) && (decimal.Parse(txtPropContr32.Text.ToString().Trim()) > 0)))
                    //{
                    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Cannot fil both Deduction for dependent with disability and Deduction for dependent with severe disability');", true);
                    //    MsgCls("Cannot fil both Deduction for dependent with disability and Deduction for dependent with severe disability", lblMessageBoard, System.Drawing.Color.Red);
                    //    return;
                    //}
                }
                int? ITHID = 0;
                ITbl ITblObj = new ITbl();
                ITbo ITboObj = new ITbo();

                ITboObj.CREATED_BY = User.Identity.Name;
                ITboObj.PERNR = User.Identity.Name;
                ITboObj.STATUS = "Saved";//"Requested";
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
                        SendMailSec80(ITHID, "Submit");
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Income Tax Sec 80 Request saved successfully');", true);
                        lblMessageBoard.Text = "Income Tax Sec 80 Request saved successfully.";
                    }
                }
                loadTab3();
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

                ////strSubject = "Income Tax Request " + ITHID + " has been " + type + " by " + EMP_Name + "  |  " + User.Identity.Name + " and is pending for the Approval.";

                strSubject = "Income Tax Request " + ITHID + " has been " + type + " by " + EMP_Name + "  |  " + User.Identity.Name;// +" and is pending for the Approval.";

                RecipientsString = "karthik.k@itchamps.com";
                strPernr_Mail = "vaishnavi.k@itchamps.com";

                //    //Preparing the mail body--------------------------------------------------
                ////string body = "<b>Income Tax Request " + ITHID + " has been " + type + " by " + EMP_Name + "  |  " + User.Identity.Name + " and is pending for the Approval.<br/><br/></b>";

                string body = "<b>Income Tax Request " + ITHID + " has been " + type + " by " + EMP_Name + "  |  " + User.Identity.Name;// +" and is pending for the Approval.<br/><br/></b>";
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
                        using (FileUpload fuAttachments = (FileUpload)GVITSec80.Rows[i].FindControl("fuAttachments"))
                        {
                            if (txtPropContr != null)
                            {
                                if (txtActContr != null)
                                {
                                    if (typ.Trim() == "1")
                                    {
                                        txtActContr.Enabled = true;
                                        txtPropContr.Enabled = false;
                                        fuAttachments.Enabled = true;
                                    }
                                    else if (typ.Trim() == "0")
                                    {
                                        txtActContr.Enabled = false;
                                        txtPropContr.Enabled = true;
                                        fuAttachments.Enabled = false;
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

                if (RecordCountu < 0)
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
                ITboObj.STATUS = "Saved";//"Updated";
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
                        ////SendMailSec80(int.Parse(ViewState["ID"].ToString().Trim()), "Updated");
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Income Tax Sec 80 Request saved successfully');", true);
                        lblMessageBoard.Text = "Income Tax Sec 80 Request saved successfully.";
                    }
                }
                loadTab3();
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
                ITbl itblObj = new ITbl();
                List<ITbo> itboObj = new List<ITbo>();
                List<ITbo> itboObj1 = new List<ITbo>();
                string ApproverId = User.Identity.Name;
                itboObj1 = itblObj.Load_IT_Locking(ApproverId);
                EnableControls(itboObj1[0].CA80 == true ? "1" : "0");
                CB_ConsAct.Checked = itboObj1[0].CA80 == true ? true : false;
                //EnableControls(CB_ConsAct.Checked ? "1" : "0");
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
                        ITbo ITObjbod = new ITbo();

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
        /*------------IT_DeclarationSec80 Ends------*/

        /*------------IT_DeclarationSec80C Starts------*/

        public void pageLoadEvents2()
        {
            try
            {
                bool? itlock = false;
                ITdalDataContext objDataContext = new ITdalDataContext();
                objDataContext.usp_IT_GetLockStatus(User.Identity.Name, ref itlock);

                if (itlock == false)
                {
                    MsgCls("", LblLockSts2, Color.Transparent);
                    DivSec80.Visible = true;
                    //GetFinancialDates();
                    GetFinancialDates2();
                    LoadGrid2();

                }
                else if (itlock == true)
                {
                    DivSec80.Visible = false;
                    MsgCls("IT Declaration has been locked. Please contact Payroll Admin.", LblLockSts, Color.Red);
                }




            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "'pageLoadEvents2());", true); }
        }

        private void LoadGrid2()
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

                //if (ITboObj1 == null || ITboObj1.Count == 0)
                //{
                //    MsgCls("No Records Found !", lblMessageBoard, Color.Red);
                //    GVITSec80C.Visible = false;
                //    GVITSec80C.DataSource = null;
                //    GVITSec80C.DataBind();
                //    return;
                //}
                //else
                //{
                GVITSec80C.Visible = true;
                GVITSec80C.DataSource = ITboObj1;
                GVITSec80C.SelectedIndex = -1;
                GVITSec80C.DataBind();



                //GridView1.DataSource = ITboObj1;
                //GridView1.SelectedIndex = -1;
                //GridView1.DataBind();
                //}








                ViewState["ID"] = ITboObj1[0].ID == null ? "0" : ITboObj1[0].ID.ToString().Trim();
                ConsiderActProp2(ITboObj1[0].CONACTPROP == null ? "0" : ITboObj1[0].CONACTPROP.ToString().Trim());
                if (count > 0)
                {
                    DisableControls2(ITboObj1[0].CONACTPROP == null ? "0" : ITboObj1[0].CONACTPROP.ToString().Trim());
                    btnSubmitClaims2.Visible = false;
                    BtnUpdate2.Visible = false;
                    BtnCancel2.Visible = false;
                    BtnEdit2.Visible = true;

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
                    EnableControls2(ITboObj1[0].CONACTPROP == null ? "0" : ITboObj1[0].CONACTPROP.ToString().Trim());
                    btnSubmitClaims2.Visible = true;
                    BtnUpdate2.Visible = false;
                    BtnCancel2.Visible = false;
                    BtnEdit2.Visible = false;

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
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "'LoadGrid2())", true); }

        }

        public void DisableControls2(string type)
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
                                            CB_ConsAct2.Enabled = false;
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
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "'DisableControls2())", true); }
        }

        public void EnableControls2(string type)
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
                                                CB_ConsAct2.Enabled = true;
                                                //UpdateBtn.Enabled = true;
                                                DeleteBtn.Enabled = true;
                                            }
                                            else if (type.Trim() == "0")
                                            {
                                                //txtActContr.Enabled = false;
                                                //txtPropContr.Enabled = true;

                                                txtPropContr.Enabled = true;
                                                fu.Enabled = false;
                                                ////txtActContr.Enabled = false;
                                                //Newly added starts
                                                if (CB_ConsAct2.Checked == true)
                                                {
                                                    txtActContr.Enabled = true;
                                                }
                                                else
                                                {
                                                    txtActContr.Enabled = false;
                                                }
                                                //Newly added ends
                                                
                                                txtRemarks.Enabled = true;
                                                CB_ConsAct2.Enabled = true;
                                                //UpdateBtn.Enabled = true;
                                                DeleteBtn.Enabled = false;
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
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "'EnableControls2())", true); }
        }

        private void GetFinancialDates2()
        {
            try
            {
                //DateTime dt = DateTime.Now;
                int month = int.Parse(DateTime.Today.Month.ToString());
                if (month > 3)
                {
                    LblFromDate2.Text = DateTime.Today.Year.ToString();
                    LblToDate2.Text = (DateTime.Today.Year + 1).ToString();
                }
                else if (month <= 3)
                {
                    LblFromDate2.Text = (DateTime.Today.Year - 1).ToString();
                    LblToDate2.Text = DateTime.Today.Year.ToString();
                }
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "'GetFinancialDates2())", true); }
        }

        protected void btnSubmitITSec802_Click(object sender, EventArgs e)
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
                ITboObj.STATUS = "Saved";//"Requested";
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
                                GetFinancialDates2();
                                LoadGrid2();
                                btnSubmitClaims2.Visible = false;
                                BtnUpdate2.Visible = false;
                                BtnCancel2.Visible = false;
                                BtnEdit2.Visible = true;
                            }
                        }
                    }
                    //ITblObj.Create_ITSection80Trans(Dt);
                    if (sts == true)
                    {
                        SendMailSec80C(ITHID, "Submit");
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Income Tax Sec 80C Request saved successfully');", true);
                        lblMessageBoard.Text = "Income Tax Sec 80C Request saved successfully.";
                    }
                }
                Tab1.CssClass = "nav-link active p-2";
                Tab2.CssClass = "nav-link p-2";
                Tab3.CssClass = "nav-link p-2";
                Tab4.CssClass = "nav-link p-2";
                Tab5.CssClass = "nav-link p-2";
                Tab6.CssClass = "nav-link p-2";
                ////Tab4.CssClass = "nav-link p-2";
                MainView.ActiveViewIndex = 0;
                ////LoadTravelClaimGridView();
                LoadGrid();
                lblMessageBoard.Text = "";
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

                //strSubject = "Income Tax Request " + ITHID + " has been " + type + " by " + EMP_Name + "  |  " + User.Identity.Name + " and is pending for the Approval.";

                strSubject = "Income Tax Request " + ITHID + " has been " + type + " by " + EMP_Name + "  |  " + User.Identity.Name;// +" and is pending for the Approval.";

                RecipientsString = "karthik.k@itchamps.com";
                strPernr_Mail = "vaishnavi.k@itchamps.com";

                //    //Preparing the mail body--------------------------------------------------
                string body = "<b>Income Tax Request " + ITHID + " has been " + type + " by " + EMP_Name + "  |  " + User.Identity.Name;// +" and is pending for the Approval.<br/><br/></b>";
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

        protected void CB_ConsAct2_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                string type = string.Empty;
                if (CB_ConsAct2.Checked)
                {
                    type = "1";
                }
                else
                {
                    type = "0";
                }
                ConsiderActProp2(type);
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        }


        public void ConsiderActProp2(string typ)
        {
            try
            {
                if (GVITSec80C.Rows.Count > 0)
                {
                    for (int i = 0; i < GVITSec80C.Rows.Count; i++)
                    {
                        using (FileUpload fuAttachments = (FileUpload)GVITSec80C.Rows[i].FindControl("fuAttachments"))
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
                                        fuAttachments.Enabled = true;
                                    }
                                    else if (typ.Trim() == "0")
                                    {
                                        txtActContr.Enabled = false;
                                        txtPropContr.Enabled = true;
                                        fuAttachments.Enabled = false;
                                        //Newly added starts
                                        if (CB_ConsAct2.Checked == true)
                                        {
                                            txtActContr.Enabled = true;
                                        }
                                        else
                                        {
                                            txtActContr.Enabled = false;
                                        }
                                        //Newly added ends
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

                        LoadGrid2();


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

        protected void BtnUpdate2_Click(object sender, EventArgs e)
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

                if (RecordCountu < 0)
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
                ITboObj.STATUS = "Saved";//"Updated";
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

                                ITblObj.Create_ITSection80CTransaction(ITboObj1, ref stus);
                                GetFinancialDates2();
                                LoadGrid2();
                                btnSubmitClaims2.Visible = false;
                                BtnUpdate2.Visible = false;
                                BtnCancel2.Visible = false;
                                BtnEdit2.Visible = true;
                            }
                        }
                    }
                    //ITblObj.Create_ITSection80Trans(Dt);

                    if (stus == true)
                    {
                        //// SendMailSec80C(int.Parse(ViewState["ID"].ToString().Trim()), "Updated");
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Income Tax Sec 80C Request saved successfully');", true);
                        lblMessageBoard.Text = "Income Tax Sec 80C Request saved successfully.";
                    }
                }
                Tab1.CssClass = "nav-link active p-2";
                Tab2.CssClass = "nav-link p-2";
                Tab3.CssClass = "nav-link p-2";
                Tab4.CssClass = "nav-link p-2";
                Tab5.CssClass = "nav-link p-2";
                Tab6.CssClass = "nav-link p-2";
                ////Tab4.CssClass = "nav-link p-2";
                MainView.ActiveViewIndex = 0;
                ////LoadTravelClaimGridView();
                LoadGrid();
                lblMessageBoard.Text = "";
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

        protected void BtnEdit2_Click(object sender, EventArgs e)
        {
            try
            {
                ITbl itblObj = new ITbl();
                List<ITbo> itboObj = new List<ITbo>();
                List<ITbo> itboObj1 = new List<ITbo>();
                string ApproverId = User.Identity.Name;
                itboObj1 = itblObj.Load_IT_Locking(ApproverId);
                EnableControls2(itboObj1[0].CA80C == true ? "1" : "0");
                CB_ConsAct2.Checked = itboObj1[0].CA80C == true ? true : false;
                //EnableControls2(CB_ConsAct2.Checked ? "1" : "0");
                btnSubmitClaims2.Visible = false;
                BtnUpdate2.Visible = true;
                BtnCancel2.Visible = true;
                BtnEdit2.Visible = false;
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        }

        protected void BtnCancel2_Click(object sender, EventArgs e)
        {
            try
            {
                GetFinancialDates2();
                LoadGrid2();
                btnSubmitClaims2.Visible = false;
                BtnUpdate2.Visible = false;
                BtnCancel2.Visible = false;
                BtnEdit2.Visible = true;
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        }
        /*------------IT_DeclarationSec80C Ends------*/

        /*------------IT_DeclarationHousing Starts------*/
        public void pageLoadEvents3()
        {
            try
            {
                bool? itlock = false;
                ITdalDataContext objDataContext = new ITdalDataContext();
                objDataContext.usp_IT_GetLockStatus(User.Identity.Name, ref itlock);

                if (itlock == false)
                {
                    MsgCls("", LblLockSts3, Color.Transparent);
                    DivHosuing.Visible = true;
                    GetFinancialDates3();
                    //GetDeclarationHousing();
                    BindGrid();

                    minmaxdate();
                    LoadSlctdCountryStatesDropDown();
                }
                else if (itlock == true)
                {
                    DivHosuing.Visible = false;
                    MsgCls("IT Declaration has been locked. Please contact Payroll Admin.", LblLockSts, Color.Red);
                }
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
        }

        private void BindGrid()
        {

            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("usp_IT_GetHousing"))
                {
                    cmd.Parameters.AddWithValue("@PERNR", User.Identity.Name);
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            gvHRA.DataSource = dt;
                            gvHRA.DataBind();
                        }
                    }
                }
            }
        }

        private void LoadSlctdCountryStatesDropDown()
        {
            masterbo mBo = new masterbo();
            mBo.LL = "IN";
            mastercollectionbo objLst = iEmpPowerMaster_Load.masterbl.Load_States_For_Slctd_Country(mBo);
            drpdwnState.DataSource = objLst;
            drpdwnState.DataTextField = "BEZEI";
            drpdwnState.DataValueField = "BLAND";
            drpdwnState.DataBind();
            drpdwnState.Items.Insert(0, new ListItem("--Select State--", "0"));
        }

        public void GetDeclarationHousing()
        {
            try
            {
                ITbl ITblObj = new ITbl();
                List<ITbo> ITboObj = new List<ITbo>();
                List<ITbo> ITboObj1 = new List<ITbo>();
                // string ApproverId = User.Identity.Name;
                ITboObj1 = ITblObj.Load_IT_HousingDetails(User.Identity.Name);

                if (ITboObj1 == null || ITboObj1.Count == 0)
                {
                    ////NewEntry();

                    return;
                }
                else
                {
                    ////UpDateEntry(ITboObj1);
                    ///loadDetails(ITboObj1);
                    BindGrid();
                }

            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        }

        private void loadDetails(List<ITbo> ITboObj1)
        {
            try
            {
                gvHRA.DataSource = ITboObj1;
                gvHRA.DataBind();
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        }

        private void GetFinancialDates3()
        {
            try
            {
                //DateTime dt = DateTime.Now;
                int month = int.Parse(DateTime.Today.Month.ToString());
                if (month > 3)
                {
                    LblFromDate3.Text = DateTime.Today.Year.ToString();
                    LblToDate3.Text = (DateTime.Today.Year + 1).ToString();
                }
                else if (month <= 3)
                {
                    LblFromDate3.Text = (DateTime.Today.Year - 1).ToString();
                    LblToDate3.Text = DateTime.Today.Year.ToString();
                }
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        }

        private void SendMailHousing(int? ITHID, string type)
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

                ////strSubject = "Income Tax Request " + ITHID + " has been " + type + " by " + EMP_Name + "  |  " + User.Identity.Name + " and is pending for the Approval.";

                strSubject = "Income Tax Request " + ITHID + " has been " + type + " by " + EMP_Name + "  |  " + User.Identity.Name;// +" and is pending for the Approval.";

                RecipientsString = "karthik.k@itchamps.com";
                strPernr_Mail = "vaishnavi.k@itchamps.com";

                //    //Preparing the mail body--------------------------------------------------
                //string body = "<b>Income Tax Request " + ITHID + " has been " + type + " by " + EMP_Name + "  |  " + User.Identity.Name + " and is pending for the Approval.<br/><br/></b>";
                string body = "<b>Income Tax Request " + ITHID + " has been " + type + " by " + EMP_Name + "  |  " + User.Identity.Name;// +" and is pending for the Approval.<br/><br/></b>";
                body += "<b>Income Tax ID  :  " + ITHID + "</b><br/><br/>";
                body += "<b>Income Tax Type :  " + "Housing" + "</b><br/><br/>";

                //    //End of preparing the mail body-------------------------------------------
                iEmpPowerMaster_Load.masterbl.DispatchMail(RecipientsString, User.Identity.Name, strSubject, strPernr_Mail, body);
                lblMessageBoard.ForeColor = System.Drawing.Color.Green;
                lblMessageBoard.Text = "Income Tax Request submitted successfully and Mail sent successfully.";
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        }

        //protected void BtnCancel3_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        GetDeclarationHousing();
        //    }
        //    catch (Exception Ex)
        //    { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
        //}

        protected void Insert(object sender, EventArgs e)
        {
            try
            {
                bool? sts = false;
                int? ITHOUSINGIT = 0;
                ITbl ITblObj = new ITbl();
                ITbo ITboObj = new ITbo();

                ITboObj.CREATED_BY = User.Identity.Name;
                ITboObj.PERNR = User.Identity.Name;
                ITboObj.STATUS = "Saved";//"Requested";
                ITboObj.CREATED_ON = DateTime.Now;
                ITboObj.BEGDA = DateTime.Parse(txtFromDate.Text);////DateTime.Now;
                ITboObj.ENDDA = DateTime.Parse(txtToDate.Text);////DateTime.Now;
                ITboObj.MODIFIED_BY = "";
                ITboObj.Flag = 1;
                ITboObj.ID = 0;
                ITboObj.EMPCOMMENTS = "";//// TXTCOMMENTS.Text.ToString().Trim();

                ////if (DDl_AccomTyp.SelectedValue.ToString().Trim() == "1")
                {

                    ITboObj.ACCOM = "1";//// DDl_AccomTyp.SelectedValue.ToString().Trim();
                    ITboObj.METRO = DDL_CityCat.SelectedValue.ToString().Trim() == "1" ? "1" : "0";
                    ITboObj.RTAMT = decimal.Parse(TXT_ActAmount.Text.ToString().Trim());
                    ITboObj.HRTXE = 0;//// CB_ConsAct.Checked ? int.Parse("1") : int.Parse("0");
                    ITboObj.LDAD1 = TXTLandLordAddr.Text.ToString().Trim();
                    ITboObj.LDAID = TXTPANLAndLord.Text.ToString().Trim();
                    ITboObj.LDADE = "0";//// CHK_LLDECLARATION.Checked ? "1" : "0";
                    ITboObj.Address = txtAddress.Text;
                    ITboObj.State = drpdwnState.SelectedValue.ToString();
                    ITboObj.City = txtCity.Text;
                    ITboObj.LDNAM = TXTLandLordName.Text;

                    ITblObj.Create_ITHousing(ITboObj, ref ITHOUSINGIT, ref sts);

                    ////GetDeclarationHousing();
                    this.BindGrid();
                    if (sts == true)
                    {
                        SendMailHousing(ITHOUSINGIT, "Submit");
                        //MsgCls("IT Housing created successfully !", lblMessageBoard, Color.Green);
                        //ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('IT Housing created successfully !')", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Income Tax HRA Request saved successfully');", true);
                        lblMessageBoard.Text = "Income Tax Request HRA saved successfully.";

                        lblRentPeryear.Text = "";//Newly added


                        ClearHRA();
                    }

                }

            }
            catch (Exception Ex)
            {

                switch (Ex.Message)
                {


                    case "-10":
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Record already exists for this financial year');", true);
                        MsgCls("Record already exists for this financial year", lblMessageBoard, Color.Red);

                        break;
                    default:
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true);
                        break;
                }

            }






        }

        void ClearHRA()
        {
            txtFromDate.Text = "";
            txtToDate.Text = "";
            TXT_ActAmount.Text = "";
            txtAddress.Text = "";
            drpdwnState.SelectedValue = "0";
            txtCity.Text = "";
            TXTLandLordName.Text = "";
            TXTLandLordAddr.Text = "";
            TXTPANLAndLord.Text = "";
        }

        protected void OnRowEditing(object sender, GridViewEditEventArgs e)
        {
            gvHRA.EditIndex = e.NewEditIndex;
            this.BindGrid();

            try
            {
                RangeValidator grdRgfromdate = gvHRA.Rows[e.NewEditIndex].FindControl("RV_txtFromDate") as RangeValidator;
                RangeValidator grdRgTodate = gvHRA.Rows[e.NewEditIndex].FindControl("RV_txtToDate") as RangeValidator;
                CompareValidator CmpVTodate = gvHRA.Rows[e.NewEditIndex].FindControl("CPV_txtToDate") as CompareValidator;
                CompareValidator CmpVFromdate = gvHRA.Rows[e.NewEditIndex].FindControl("CPV_txtFromDate") as CompareValidator;
                string month = DateTime.Today.Month.ToString();
                CmpVFromdate.ValueToCompare = DateTime.Parse(Session["DOJ"].ToString()).ToString("dd/MM/yyyy");
                CmpVTodate.ValueToCompare = DateTime.Parse(Session["DOJ"].ToString()).ToString("dd/MM/yyyy");
                if (int.Parse(month.Trim()) >= 4)
                {
                    grdRgfromdate.MinimumValue = RV_txtITPreEmprFrmDt.MinimumValue = new DateTime(DateTime.Today.Year, 4, 1).ToString("dd/MM/yyyy");
                    grdRgTodate.MinimumValue = RVtxtITPreEmprToDt.MinimumValue = new DateTime(DateTime.Today.Year, 4, 1).ToString("dd/MM/yyyy");
                    ViewState["mindate"] = new DateTime(DateTime.Today.Year, 4, 1).ToString("dd/MM/yyyy");
                }
                else
                {
                    grdRgfromdate.MinimumValue = RV_txtITPreEmprFrmDt.MinimumValue = new DateTime(DateTime.Today.Year, 4, 1).AddYears(-1).ToString("dd/MM/yyyy");
                    grdRgTodate.MinimumValue = RVtxtITPreEmprToDt.MinimumValue = new DateTime(DateTime.Today.Year, 4, 1).AddYears(-1).ToString("dd/MM/yyyy");
                    ViewState["mindate"] = new DateTime(DateTime.Today.Year, 4, 1).AddYears(-1).ToString("dd/MM/yyyy");
                }



                grdRgfromdate.MaximumValue = RV_txtITPreEmprFrmDt.MaximumValue = new DateTime(DateTime.Today.Year, 3, 31).AddYears(1).ToString("dd/MM/yyyy");//DateTime.Now.ToString("dd/MM/yyyy");
                grdRgfromdate.ErrorMessage = RV_txtITPreEmprFrmDt.ErrorMessage = "From date should be between " + grdRgfromdate.MinimumValue + "- " + grdRgfromdate.MaximumValue;
                grdRgTodate.MaximumValue = RVtxtITPreEmprToDt.MaximumValue = new DateTime(DateTime.Today.Year, 3, 31).AddYears(1).ToString("dd/MM/yyyy");//DateTime.Now.AddYears(1).ToString("dd/MM/yyyy");
                grdRgTodate.ErrorMessage = RVtxtITPreEmprToDt.ErrorMessage = "To date should be between " + grdRgTodate.MinimumValue + "- " + grdRgTodate.MaximumValue;
                ViewState["maxdate"] = new DateTime(DateTime.Today.Year, 3, 31).AddYears(1).ToString("dd/MM/yyyy");
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "'minmaxdate())", true); }
        }

        protected void OnRowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {

                bool? sts = false;
                int? ITHOUSINGIT = 0;
                ITbl ITblObj = new ITbl();
                ITbo ITboObj = new ITbo();


                GridViewRow row = gvHRA.Rows[e.RowIndex];
                int Id = Convert.ToInt32(gvHRA.DataKeys[e.RowIndex].Values[0]);


                //string name = (row.FindControl("txtName") as TextBox).Text;
                //string country = (row.FindControl("txtCountry") as TextBox).Text;


                ITboObj.CREATED_BY = User.Identity.Name;
                ITboObj.PERNR = User.Identity.Name;
                ITboObj.STATUS = "Saved";//"Updated";
                ITboObj.CREATED_ON = DateTime.Now;
                ITboObj.BEGDA = DateTime.Parse((row.FindControl("txtFromDate") as TextBox).Text);////DateTime.Now;
                ITboObj.ENDDA = DateTime.Parse((row.FindControl("txtToDate") as TextBox).Text);////DateTime.Now;
                ITboObj.MODIFIED_BY = User.Identity.Name;
                ITboObj.Flag = 2;
                ITboObj.ID = Id;
                ITboObj.EMPCOMMENTS = "";//// TXTCOMMENTS.Text.ToString().Trim();

                ////if (DDl_AccomTyp.SelectedValue.ToString().Trim() == "1")
                {

                    ITboObj.ACCOM = "1";//// DDl_AccomTyp.SelectedValue.ToString().Trim();
                    ITboObj.METRO = DDL_CityCat.SelectedValue.ToString().Trim() == "1" ? "1" : "0";
                    ITboObj.RTAMT = decimal.Parse((row.FindControl("txtRentPerMonth") as TextBox).Text.ToString().Trim());
                    ITboObj.HRTXE = 0;//// CB_ConsAct.Checked ? int.Parse("1") : int.Parse("0");
                    ITboObj.LDAD1 = (row.FindControl("txtLandLordsAddress") as TextBox).Text.ToString().Trim();
                    ITboObj.LDAID = (row.FindControl("txtPANofLandLord") as TextBox).Text.ToString().Trim();
                    ITboObj.LDADE = "0";//// CHK_LLDECLARATION.Checked ? "1" : "0";
                    ITboObj.Address = (row.FindControl("txtAddress") as TextBox).Text.ToString();
                    ITboObj.State = (row.FindControl("drpdwnState") as DropDownList).SelectedValue.ToString();
                    ITboObj.City = (row.FindControl("txtCity") as TextBox).Text;
                    ITboObj.LDNAM = (row.FindControl("txtLandLordsName") as TextBox).Text;



                    //ITblObj.Create_ITHousing(ITboObj, ref ITHOUSINGIT, ref sts);

                    //if (sts == true)
                    //{
                    //    SendMailHousing(ITHOUSINGIT, "Updated");
                    //    MsgCls("IT Housing updated successfully !", lblMessageBoard, Color.Green);
                    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('IT Housing updated successfully !')", true);
                    //}

                    if (ITboObj.BEGDA > ITboObj.ENDDA)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('From date must be less than To date !')", true);
                    }
                    else if ((ITboObj.RTAMT >= decimal.Parse("8333.00")) && ITboObj.LDAID == "")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please Enter Land Lord PAN ID !')", true);
                    }
                    else
                    {
                        ITblObj.Create_ITHousing(ITboObj, ref ITHOUSINGIT, ref sts);
                    }


                    if (sts == true)
                    {
                        //SendMailHousing(ITHOUSINGIT, "Updated");
                        //MsgCls("IT Housing updated successfully !", lblMessageBoard, Color.Green);
                        //ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('IT Housing updated successfully !')", true);
                        gvHRA.EditIndex = -1;
                        this.BindGrid();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Income Tax HRA Request saved successfully');", true);
                        lblMessageBoard.Text = "Income Tax HRA Request saved successfully.";
                    }





                }

                //gvHRA.EditIndex = -1;
                //this.BindGrid();
            }
            //catch (Exception Ex)
            //{ ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
            catch (Exception ex)
            {
                switch (ex.Message)
                {




                    case "-10":
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Record already exists for this financial year');", true);
                        MsgCls("Record already exists for this financial year", lblMessageBoard, Color.Red);



                        break;
                    default:
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + ex.Message + "');", true);
                        break;
                }
            }
        }

        protected void OnRowCancelingEdit(object sender, EventArgs e)
        {
            gvHRA.EditIndex = -1;
            this.BindGrid();
        }

        protected void OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int Id = Convert.ToInt32(gvHRA.DataKeys[e.RowIndex].Values[0]);
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("usp_IT_Submit_Housing_Delete"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", Id);
                    cmd.Parameters.AddWithValue("@PERNR", User.Identity.Name);
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            this.BindGrid();
        }

        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
            }

            if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowIndex != gvHRA.EditIndex)
            {
                (e.Row.Cells[10].Controls[2] as LinkButton).Attributes["onclick"] = "return confirm('Do you want to delete this row?');";
            }

            if ((e.Row.RowState & DataControlRowState.Edit) > 0)
            {
                // BIND THE "DROPDOWNLIST" WITH THE DATASET FILLED WITH "QUALIFICATION" DETAILS.
                DropDownList drpdwnState = new DropDownList();
                drpdwnState = (DropDownList)e.Row.FindControl("drpdwnState");

                if (drpdwnState != null)
                {
                    masterbo mBo = new masterbo();
                    mBo.LL = "IN";
                    mastercollectionbo objLst = iEmpPowerMaster_Load.masterbl.Load_States_For_Slctd_Country(mBo);
                    drpdwnState.DataSource = objLst;
                    drpdwnState.DataTextField = "BEZEI";
                    drpdwnState.DataValueField = "BLAND";
                    drpdwnState.DataBind();
                    drpdwnState.Items.Insert(0, new ListItem("--Select State--", "0"));

                    // ASSIGN THE SELECTED ROW VALUE ("QUALIFICATION CODE") TO THE DROPDOWNLIST SELECTED VALUE.
                    ((DropDownList)e.Row.FindControl("drpdwnState")).SelectedValue = DataBinder.Eval(e.Row.DataItem, "state").ToString();
                }
            }
        }

        protected void TXT_ActAmount_TextChanged(object sender, EventArgs e)
        {
            //DateTime FromDate = DateTime.Parse(txtFromDate.Text);
            //DateTime ToDate = DateTime.Parse(txtToDate.Text);
            //int m1 = (ToDate.Month - FromDate.Month);//for years
            //int m2 = (ToDate.Year - FromDate.Year) * 12; //for months
            //int months = m1 + m2 + 1;
            //lblPeriodRent.Text = (months * int.Parse(TXT_ActAmount.Text)).ToString();

            DateTime FromDate = DateTime.Parse(txtFromDate.Text);
            DateTime ToDate = DateTime.Parse(txtToDate.Text);
            int m1 = (ToDate.Month - FromDate.Month);//for years
            int m2 = (ToDate.Year - FromDate.Year) * 12; //for months
            int months = m1 + m2 + 1;
            lblPeriodRent.Text = (months * decimal.Parse(TXT_ActAmount.Text)).ToString();

            lblRentPeryear.Text = (months * decimal.Parse(TXT_ActAmount.Text)).ToString();// +" / Year";//Newly added



            RFV_TXTPANLAndLord.Enabled = (decimal.Parse(TXT_ActAmount.Text.Trim()) >= decimal.Parse("8333.00")) ? true : false;
        }

        protected void OnTextChanged(object sender, EventArgs e)
        {
            try
            {
                TextBox thisTextBox = (TextBox)sender;
                GridViewRow currentRow = (GridViewRow)thisTextBox.Parent.Parent;
                int rowindex = 0;
                rowindex = currentRow.RowIndex;
                TextBox txtRentPerMonth = (TextBox)currentRow.FindControl("txtRentPerMonth");
                TextBox txtFromDate = (TextBox)currentRow.FindControl("txtFromDate");
                TextBox txtToDate = (TextBox)currentRow.FindControl("txtToDate");
                Label lblPeriodRent = (Label)currentRow.FindControl("lblPeriodRent");
                DateTime FromDate = DateTime.Parse(txtFromDate.Text);
                DateTime ToDate = DateTime.Parse(txtToDate.Text);
                int m1 = (ToDate.Month - FromDate.Month);//for years
                int m2 = (ToDate.Year - FromDate.Year) * 12; //for months
                int months = m1 + m2 + 1;
                //lblPeriodRent.Text = (months * int.Parse(txtRentPerMonth.Text)).ToString();
                lblPeriodRent.Text = (months * decimal.Parse(txtRentPerMonth.Text)).ToString();

                //Label lblRentPeryear = (Label)currentRow.FindControl("lblRentPeryear");
                //lblRentPeryear.Text = (months * decimal.Parse(txtRentPerMonth.Text)).ToString();
                TextBox txtRentPeryear = (TextBox)currentRow.FindControl("txtRentPeryear");
                txtRentPeryear.Text = (months * decimal.Parse(txtRentPerMonth.Text)).ToString();

            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
        }
        /*------------IT_DeclarationHousing Ends------*/


        /*------------IT_IncomeOtherSources Starts------*/
        public void pageLoadEvents4()
        {
            try
            {
                bool? itlock = false;
                ITdalDataContext objDataContext = new ITdalDataContext();
                objDataContext.usp_IT_GetLockStatus(User.Identity.Name, ref itlock);

                if (itlock == false)
                {
                    ////MsgCls("", LblLockSts, Color.Transparent);
                    ////DivOthers.Visible = true;
                    ////GetFinancialDates4();
                    ////DIVCOMMENTS.Visible = false;
                    ////BTNSubmitHousingOthers.Visible = BTNSubmitHousingOthers.Enabled = false;
                    ////BtnEDIT4.Visible = BtnEDIT4.Enabled = false;
                    ////btnUpdate4.Visible = btnUpdate4.Enabled = false;
                    ////BtnCancel.Visible = BtnCancel.Enabled = false;
                    ////MV_IncomeSources.Visible = false;

                    MsgCls("", LblLockSts, Color.Transparent);
                    DivOthers.Visible = true;
                    GetFinancialDates4();
                    //DIVCOMMENTS.Visible = false;
                    // BTNSubmitHousingOthers.Visible = BTNSubmitHousingOthers.Enabled = false;
                    BtnEDIT4.Visible = BtnEDIT4.Enabled = false;
                    btnUpdate4.Visible = btnUpdate4.Enabled = false;
                    BtnCancel.Visible = BtnCancel.Enabled = false;
                    //MV_IncomeSources.Visible = false;



                    DDl_TYPE.SelectedValue = "1";
                    DIVINCOMETYP.Visible = true;
                    //MV_IncomeSources.SetActiveView(ViewHousing);
                    ClearControls4();
                    ClearControlsView24();

                    RB_PropTyp.SelectedValue = "1";
                    Div6.Visible = true;
                    DIVins.Visible = DIVins1.Visible = true;
                    Div7.Visible = false;
                    BTNSubmitHousingOthers.Visible = true;
                    BTNSubmitHousingOthers.Enabled = txtLendrName.Text != "" ? true : false;
                    //GetDeclarationHousingOthers();
                    DIVCOMMENTS.Visible = true;
                    MV_IncomeSources.SetActiveView(ViewHousing);
                    Load_grdSelfOccDetails();

                }
                else if (itlock == true)
                {
                    DivOthers.Visible = false;
                    MsgCls("IT Declaration has been locked. Please contact Payroll Admin.", LblLockSts, Color.Red);
                }

            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
        }

        private void GetFinancialDates4()
        {
            try
            {
                //DateTime dt = DateTime.Now;
                int month = int.Parse(DateTime.Today.Month.ToString());
                if (month > 3)
                {
                    LblFromDate4.Text = DateTime.Today.Year.ToString();
                    LblToDate4.Text = (DateTime.Today.Year + 1).ToString();
                    LblFrom.Text = DateTime.Today.Year.ToString();
                    LblTo.Text = (DateTime.Today.Year + 1).ToString();
                }
                else if (month <= 3)
                {
                    LblFromDate4.Text = (DateTime.Today.Year - 1).ToString();
                    LblToDate4.Text = DateTime.Today.Year.ToString();
                    LblFrom.Text = (DateTime.Today.Year - 1).ToString();
                    LblTo.Text = DateTime.Today.Year.ToString();
                }
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        }

        protected void DDl_TYPE_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                MV_IncomeSources.Visible = true;
                if (DDl_TYPE.SelectedValue.ToString().Trim() == "1")
                {
                    DIVINCOMETYP.Visible = true;
                    //MV_IncomeSources.SetActiveView(ViewHousing);
                    ClearControls4();
                    ClearControlsView24();
                    RB_PropTyp.SelectedValue = "1";
                    BTNSubmitHousingOthers.Visible = true;
                    //GetDeclarationHousingOthers();
                    DIVCOMMENTS.Visible = true;
                    Div6.Visible = true;
                    DIVins.Visible = DIVins1.Visible = true;
                    Div7.Visible = false;
                    MV_IncomeSources.SetActiveView(ViewHousing);
                    Load_grdSelfOccDetails();
                    grdLetout.Visible = false;
                    btnUpdate4.Visible = btnUpdate4.Enabled = false;
                    BtnEDIT4.Visible = BtnEDIT4.Enabled = false;
                    BtnCancel4.Visible = BtnCancel4.Enabled = true;
                    BTNSubmitHousingOthers.Visible = BTNSubmitHousingOthers.Enabled = false;
                }
                else if (DDl_TYPE.SelectedValue.ToString().Trim() == "2")
                {
                    DIVINCOMETYP.Visible = true;

                    ClearControls4();
                    ClearControlsView24();

                    BTNSubmitHousingOthers.Visible = true;
                    GetDeclarationHousingOthers();
                    MV_IncomeSources.SetActiveView(ViewOtherSources);
                    DIVCOMMENTS.Visible = true;
                }
                else if (DDl_TYPE.SelectedValue.ToString().Trim() == "0")
                {
                    DIVINCOMETYP.Visible = false;
                    ClearControls4();
                    ClearControlsView24();
                    DIVCOMMENTS.Visible = false;
                    BTNSubmitHousingOthers.Visible = false;
                }

            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
        }

        public void GetDeclarationHousingOthers()
        {
            try
            {
                ITbl ITblObj = new ITbl();
                List<ITbo> ITboObj = new List<ITbo>();
                List<ITbo> ITboObj1 = new List<ITbo>();
                // string ApproverId = User.Identity.Name;
                ITboObj1 = ITblObj.Load_IT_HousingOthersDetails(User.Identity.Name, DDl_TYPE.SelectedValue.ToString().Trim());

                if (ITboObj1 == null || ITboObj1.Count == 0)
                {
                    NewEntry4();

                    return;
                }
                else
                {
                    UpDateEntry4(ITboObj1);

                    // return;
                }

            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        }

        public void HousingProprtyType(string type)
        {
            try
            {
                if (type.ToString().Trim() == "1")
                {
                    Div6.Visible = true;
                    DIVins.Visible = DIVins1.Visible = true;
                    Div7.Visible = false;
                    grdLetout.Visible = false;
                    ClearControls4();
                }
                else if (type.ToString().Trim() == "2")
                {
                    Div6.Visible = false;
                    Div7.Visible = true;
                    // RFV_TXT_DedIntr.Enabled = false;
                    ClearControls4();
                }
                else if (type.ToString().Trim() == "3")
                {
                    Div6.Visible = true;
                    grdSelfOccDetails.Visible = false;
                    DIVins.Visible = DIVins1.Visible = false;
                    Div7.Visible = true;
                    //RFV_TXT_DedIntr.Enabled = false;
                    ClearControls4();
                }
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
        }

        protected void RB_PropTyp_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (RB_PropTyp.SelectedValue.ToString().Trim() == "1")
                {
                    Div6.Visible = true;
                    DIVins.Visible = DIVins1.Visible = true;
                    Div7.Visible = false;
                    grdSelfOccDetails.Visible = true;
                    grdLetout.Visible = false;
                    //RFV_TXT_DedIntr.Enabled = true;
                    ClearControls4();
                    btnUpdate4.Visible = btnUpdate4.Enabled = false;
                    BtnEDIT4.Visible = BtnEDIT4.Enabled = false;
                    BtnCancel4.Visible = BtnCancel4.Enabled = true;
                    BTNSubmitHousingOthers.Enabled = BTNSubmitHousingOthers.Enabled = false;

                }

                else if (RB_PropTyp.SelectedValue.ToString().Trim() == "2")
                {
                    Div6.Visible = false;
                    Div7.Visible = true;
                    //RFV_TXT_DedIntr.Enabled = false;
                    ClearControls4();
                }

                else if (RB_PropTyp.SelectedValue.ToString().Trim() == "3")
                {

                    //RFV_TXT_DedIntr.Enabled = false;
                    ClearControls4();
                    //GetDeclarationHousingOthers();
                    Load_letout();
                    grdLetout.Visible = true;
                    grdSelfOccDetails.Visible = false;
                    Div6.Visible = true;
                    DIVins.Visible = DIVins1.Visible = false;
                    Div7.Visible = true;
                }
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
        }

        public void ClearControls4()
        {
            try
            {
                //if (RB_PropTyp.SelectedValue.ToString().Trim() == "1")
                //{
                txtLendrName.Text = "";
                txtLendrAdd.Text = "";
                txtLendrPAN.Text = "";
                txtLoanSacDt.Text = "";
                txtLoanSacAmt.Text = "0.0";
                txtStampDuty.Text = "0.0";
                txtTotInsPaid.Text = "0.0";
                txtTotIns.Text = "0.0";
                txtTotPrincpl.Text = "0.0";
                chkOwnhsPropty.Checked = false;
                ddlSlfLoanType.SelectedIndex = -1;
                chkBenftsSec80EE.Checked = false;
                txtTotPrinPaid.Text = "0.0";
                txtStampChgrDt.Text = "";
                txtValPropty.Text = "0.0";
                txtCaptSqFt.Text = "0.0";
                txtPurofHsLoan.Text = "";
                txtITSlfCity.Text = "";
                txtITSlfState.Text = "";
                txtAddrPropty.Text = "";
                Session["BorwData"] = null;
                txtLendrName.Enabled = true;
                txtLendrAdd.Enabled = true;
                txtLendrPAN.Enabled = true;
                txtLoanSacDt.Enabled = true;
                txtLoanSacAmt.Enabled = true;
                txtStampDuty.Enabled = true;
                txtTotInsPaid.Enabled = true;
                //txtTotIns.Enabled = true;
                //txtTotPrincpl.Enabled = true;
                chkOwnhsPropty.Enabled = true;
                ddlSlfLoanType.Enabled = true;
                chkBenftsSec80EE.Enabled = true;
                txtTotPrinPaid.Enabled = true;
                txtStampChgrDt.Enabled = true;
                txtValPropty.Enabled = true;
                txtCaptSqFt.Enabled = true;
                txtPurofHsLoan.Enabled = true;
                txtITSlfCity.Enabled = true;
                txtITSlfState.Enabled = true;
                txtAddrPropty.Enabled = true;
                TXTCOMMENTS.Enabled = true;
                TXTCOMMENTS.Text = "";
                txtBorwName.Text = "";
                txtBorwPerct.Text = "0.0";
                grdBorwDetails.DataSource = Session["BorwData"] = null;
                grdBorwDetails.DataBind();
                grdBorwDetails.Columns[3].Visible = true;
                if (Session["BorwData"] == null)
                {
                    txtBorwName.Text = Session["EmployeeName"].ToString();
                    txtBorwName.Enabled = false;
                    txtBorwPerct.Text = "100";
                    txtBorwPerct.Enabled = true;
                }
                lnkborAdd.Enabled = true;
                //}
                //if (RB_PropTyp.SelectedValue.ToString().Trim() == "3")
                //{
                TXT_DEDINT.Text = "0.0";
                TXT_FNLLETVALUE.Text = "0.0";
                Lbl_DEDREPAIR.Text = "0";
                TXTDECOTHERS.Text = "0.0";
                TXTTDS.Text = "0.0";
                TXTCOMMENTS.Text = "";
                txtMunicipaltax.Text = "0.0";
                txtTotintPort.Text = "0.0";
                txtIncm_loss.Text = "0.0";


                TXT_FNLLETVALUE.Enabled = true;

                TXTTDS.Enabled = true;
                TXTCOMMENTS.Enabled = true;
                txtMunicipaltax.Enabled = true;
                txtTotintPort.Enabled = true;

                //  }

            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
        }
        private DataTable dtBorw()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("PERCNT", typeof(string));
            return dt;
        }
        public void ClearControlsView24()
        {
            try
            {
                //TXT_BusnProfits.Text = "";
                //TXT_LTCG.Text = "";
                //TXT_LTCGS.Text = "";
                //TXT_STCG.Text = "";
                //TXT_STCGLS.Text = "";
                //TXT_IFD.Text = "";
                //TXT_IFI.Text = "";
                //TXT_OI.Text = "";
                //TXT_TDSI.Text = "";
                //TXTCOMMENTS2.Text = "";

                //TXT_BusnProfits.Text = "0.0";
                //TXT_LTCG.Text = "0.0";
                //TXT_LTCGS.Text = "0.0";
                //TXT_STCG.Text = "0.0";
                //TXT_STCGLS.Text = "0.0";
                //TXT_IFD.Text = "0.0";
                //TXT_IFI.Text = "0.0";
                //TXT_OI.Text = "0.0";
                //TXT_TDSI.Text = "0.0";
                //TXTCOMMENTS2.Text = "";
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
        }

        protected void BTNSubmitHousingOthers_Click(object sender, EventArgs e)
        {
            try
            {
                bool? sts = false;
                int? ITOTHERSID = 0;

                if (DDl_TYPE.SelectedValue.ToString().Trim() == "1")
                {
                    if (txtLendrName.Text.Trim() != "" && txtLendrPAN.Text.Trim() == "")
                    {
                        lblMessageBoard.Text = "Please ENTER Lender PAN";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Please ENTER Lender PAN');", true);
                        return;
                    }
                    else
                    {
                        if (RB_PropTyp.SelectedValue.ToString().Trim() == "1")
                        {
                            //decimal amt = 0;//string.IsNullOrEmpty(TXT_DedIntr.Text) ? 0 : decimal.Parse(TXT_DedIntr.Text.ToString().Trim());
                            //// decimal amt = decimal.Parse(TXT_DedIntr.Text.ToString().Trim());

                            ITbl ITblObj = new ITbl();
                            ITbo ITboObj = new ITbo();
                            //------Housing property
                            ITboObj.PROPTYP = "1";
                            ITboObj.RENTO = RB_PropTyp.SelectedValue.ToString().Trim();
                            ITboObj.INT24 = string.IsNullOrEmpty(txtLoanSacAmt.Text) ? 0 : decimal.Parse(txtLoanSacAmt.Text.ToString().Trim());
                            ITboObj.LETVL = decimal.Parse(chkOwnhsPropty.Checked == true ? "1.00" : "0.00");
                            ITboObj.REP24 = string.IsNullOrEmpty(txtValPropty.Text) ? 0 : decimal.Parse(txtValPropty.Text.ToString().Trim());
                            ITboObj.OTH24 = string.IsNullOrEmpty(txtStampDuty.Text) ? 0 : decimal.Parse(txtStampDuty.Text.ToString().Trim());
                            ITboObj.TDSOT = 0;
                            //------Others
                            ITboObj.BSPFT = decimal.Parse(ddlSlfLoanType.SelectedValue);
                            ITboObj.CPGLN = string.IsNullOrEmpty(txtTotInsPaid.Text) ? 0 : decimal.Parse(txtTotInsPaid.Text.ToString().Trim());
                            ITboObj.CPGLS = string.IsNullOrEmpty(txtTotPrinPaid.Text) ? 0 : decimal.Parse(txtTotPrinPaid.Text.ToString().Trim());
                            ITboObj.CPGNS = 0;
                            ITboObj.CPGSS = string.IsNullOrEmpty(txtTotIns.Text) ? 0 : decimal.Parse(txtTotIns.Text.ToString().Trim());
                            ITboObj.DVDND = decimal.Parse(chkBenftsSec80EE.Checked == true ? "1.00" : "0.00");
                            ITboObj.INTRS = string.IsNullOrEmpty(txtTotPrincpl.Text) ? 0 : decimal.Parse(txtTotPrincpl.Text.ToString().Trim());
                            ITboObj.UNSPI = 0;
                            ITboObj.TDSAT = string.IsNullOrEmpty(txtCaptSqFt.Text) ? 0 : decimal.Parse(txtCaptSqFt.Text.ToString().Trim());
                            ITboObj.CREATED_BY = User.Identity.Name;
                            ITboObj.PERNR = User.Identity.Name;
                            ITboObj.STATUS = "Saved";//"Requested";
                            ITboObj.CREATED_ON = DateTime.Now;
                            ITboObj.BEGDA = DateTime.Now;
                            ITboObj.ENDDA = DateTime.Now;
                            ITboObj.MODIFIED_BY = "";
                            ITboObj.Flag = 1;
                            ITboObj.ID = 0;
                            ITboObj.EMPCOMMENTS = TXTCOMMENTS.Text.ToString().Trim();
                            ITblObj.Create_ITHousingOthers(ITboObj, ref ITOTHERSID, ref sts);
                            ITboObj.ID = ITOTHERSID;
                            ITboObj.LENDNAME = txtLendrName.Text.Trim();
                            ITboObj.LENDPAN = txtLendrPAN.Text.Trim();
                            ITboObj.LENDADD = txtLendrAdd.Text.Trim();
                            ITboObj.ADDPROPTY = txtAddrPropty.Text.Trim();
                            ITboObj.State = txtITSlfState.Text.Trim();
                            ITboObj.City = txtITSlfCity.Text.Trim();
                            ITboObj.PUPSHSLN = txtPurofHsLoan.Text.Trim();
                            ITboObj.LNSAC_DATE = DateTime.Parse(txtLoanSacDt.Text.Trim() == "" ? "1900-01-01" : txtLoanSacDt.Text.Trim());
                            ITboObj.STMPCHR_DATE = DateTime.Parse(txtStampChgrDt.Text.Trim() == "" ? "1900-01-01" : txtLoanSacDt.Text.Trim());
                            ITboObj.PERCNT = 0;
                            ITblObj.Create_ITHousingOthers_cont(ITboObj, 1, ref sts);
                            if (Session["BorwData"] != null)
                            {
                                DataTable dt = (DataTable)Session["BorwData"];
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    ITboObj.ID = ITOTHERSID;
                                    ITboObj.LENDNAME = dt.Rows[i]["NAME"].ToString().Trim();
                                    ITboObj.PERCNT = decimal.Parse(dt.Rows[i]["PERCNT"].ToString().Trim());
                                    ITblObj.Create_ITHousingOthers_cont(ITboObj, 3, ref sts);
                                }
                            }

                            if (sts == true)
                            {
                                SendMailHousingOthers(ITOTHERSID, "Submit", "Income from Other Sources - House Property");
                                MsgCls("IT House Property created successfully !", lblMessageBoard, Color.Green);
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('IT House Property created successfully !')", true);
                            }
                            CancelFunc4(); Load_grdSelfOccDetails();
                        }



                        else if ((RB_PropTyp.SelectedValue.ToString().Trim() == "2") || (RB_PropTyp.SelectedValue.ToString().Trim() == "3"))
                        {

                            //if (((string.IsNullOrEmpty(TXT_DEDINT.Text)) && ((string.IsNullOrEmpty(TXT_DEDINT.Text) ? 0 : decimal.Parse(TXT_DEDINT.Text.ToString().Trim())) <= 0)) &&
                            //    ((string.IsNullOrEmpty(TXT_FNLLETVALUE.Text)) && ((string.IsNullOrEmpty(TXT_FNLLETVALUE.Text) ? 0 : decimal.Parse(TXT_FNLLETVALUE.Text.ToString().Trim())) <= 0)) &&
                            //    ((string.IsNullOrEmpty(Lbl_DEDREPAIR.Text)) && ((string.IsNullOrEmpty(Lbl_DEDREPAIR.Text) ? 0 : decimal.Parse(Lbl_DEDREPAIR.Text.ToString().Trim())) <= 0)) &&
                            //    ((string.IsNullOrEmpty(TXTDECOTHERS.Text)) && ((string.IsNullOrEmpty(TXTDECOTHERS.Text) ? 0 : decimal.Parse(TXTDECOTHERS.Text.ToString().Trim())) <= 0)) &&
                            //    ((string.IsNullOrEmpty(TXTTDS.Text)) && ((string.IsNullOrEmpty(TXTTDS.Text) ? 0 : decimal.Parse(TXTTDS.Text.ToString().Trim())) <= 0)))
                            //{
                            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Please enter details before submiting');", true);
                            //    MsgCls("Please enter details before submiting", lblMessageBoard, System.Drawing.Color.Red);
                            //    return;
                            //}


                            ITbl ITblObj = new ITbl();
                            ITbo ITboObj = new ITbo();
                            //------Housing property
                            ITboObj.PROPTYP = "1";
                            ITboObj.RENTO = RB_PropTyp.SelectedValue.ToString().Trim();
                            ITboObj.INT24 = string.IsNullOrEmpty(TXT_DEDINT.Text) ? 0 : decimal.Parse(TXT_DEDINT.Text.ToString().Trim());
                            ITboObj.LETVL = string.IsNullOrEmpty(TXT_FNLLETVALUE.Text) ? 0 : decimal.Parse(TXT_FNLLETVALUE.Text.ToString().Trim());
                            ITboObj.REP24 = string.IsNullOrEmpty(txtMunicipaltax.Text) ? 0 : decimal.Parse(txtMunicipaltax.Text.ToString().Trim());
                            ITboObj.OTH24 = string.IsNullOrEmpty(TXTDECOTHERS.Text) ? 0 : decimal.Parse(TXTDECOTHERS.Text.ToString().Trim());
                            ITboObj.TDSOT = string.IsNullOrEmpty(TXTTDS.Text) ? 0 : decimal.Parse(TXTTDS.Text.ToString().Trim());
                            //------Others
                            ITboObj.BSPFT = decimal.Parse(ddlLoanType.SelectedValue);
                            ITboObj.CPGLN = string.IsNullOrEmpty(txtTotintPort.Text) ? 0 : decimal.Parse(txtTotintPort.Text.ToString().Trim());
                            ITboObj.CPGLS = string.IsNullOrEmpty(txtIncm_loss.Text) ? 0 : decimal.Parse(txtIncm_loss.Text.ToString().Trim());
                            ITboObj.CPGNS = 0;
                            ITboObj.CPGSS = 0;
                            ITboObj.DVDND = 0;
                            ITboObj.INTRS = 0;
                            ITboObj.UNSPI = 0;
                            ITboObj.TDSAT = 0;
                            ITboObj.CREATED_BY = User.Identity.Name;
                            ITboObj.PERNR = User.Identity.Name;
                            ITboObj.STATUS = "Saved";//"Requested";
                            ITboObj.CREATED_ON = DateTime.Now;
                            ITboObj.BEGDA = DateTime.Now;
                            ITboObj.ENDDA = DateTime.Now;
                            ITboObj.MODIFIED_BY = "";
                            ITboObj.Flag = 1;
                            ITboObj.ID = 0;
                            ITboObj.EMPCOMMENTS = TXTCOMMENTS.Text.ToString().Trim();
                            ITblObj.Create_ITHousingOthers(ITboObj, ref ITOTHERSID, ref sts);

                            ITboObj.RENTO = "3";
                            ITboObj.INT24 = string.IsNullOrEmpty(txtLoanSacAmt.Text) ? 0 : decimal.Parse(txtLoanSacAmt.Text.ToString().Trim());
                            ITboObj.LETVL = decimal.Parse(chkOwnhsPropty.Checked == true ? "1.00" : "0.00");
                            ITboObj.REP24 = string.IsNullOrEmpty(txtValPropty.Text) ? 0 : decimal.Parse(txtValPropty.Text.ToString().Trim());
                            ITboObj.OTH24 = string.IsNullOrEmpty(txtStampDuty.Text) ? 0 : decimal.Parse(txtStampDuty.Text.ToString().Trim());
                            ITboObj.TDSOT = 0;
                            //------Others
                            ITboObj.BSPFT = decimal.Parse(ddlSlfLoanType.SelectedValue);
                            ITboObj.CPGLN = string.IsNullOrEmpty(txtTotInsPaid.Text) ? 0 : decimal.Parse(txtTotInsPaid.Text.ToString().Trim());
                            ITboObj.CPGLS = string.IsNullOrEmpty(txtTotPrinPaid.Text) ? 0 : decimal.Parse(txtTotPrinPaid.Text.ToString().Trim());
                            ITboObj.CPGNS = 0;
                            ITboObj.CPGSS = string.IsNullOrEmpty(txtTotIns.Text) ? 0 : decimal.Parse(txtTotIns.Text.ToString().Trim());
                            ITboObj.DVDND = decimal.Parse(chkBenftsSec80EE.Checked == true ? "1.00" : "0.00");
                            ITboObj.INTRS = string.IsNullOrEmpty(txtTotPrincpl.Text) ? 0 : decimal.Parse(txtTotPrincpl.Text.ToString().Trim());
                            ITboObj.UNSPI = 0;
                            ITboObj.TDSAT = string.IsNullOrEmpty(txtCaptSqFt.Text) ? 0 : decimal.Parse(txtCaptSqFt.Text.ToString().Trim());
                            ITboObj.CREATED_BY = User.Identity.Name;
                            ITboObj.PERNR = User.Identity.Name;
                            ITboObj.STATUS = "Saved";//"Requested";
                            ITboObj.CREATED_ON = DateTime.Now;
                            ITboObj.BEGDA = DateTime.Now;
                            ITboObj.ENDDA = DateTime.Now;
                            ITboObj.MODIFIED_BY = "";
                            ITboObj.Flag = 1;
                            ITboObj.ID = ITOTHERSID;
                            ITboObj.EMPCOMMENTS = TXTCOMMENTS.Text.ToString().Trim();
                            ITblObj.Create_ITHousingOthers(ITboObj, ref ITOTHERSID, ref sts);

                            ITboObj.ID = ITOTHERSID;
                            ITboObj.LENDNAME = txtLendrName.Text.Trim();
                            ITboObj.LENDPAN = txtLendrPAN.Text.Trim();
                            ITboObj.LENDADD = txtLendrAdd.Text.Trim();
                            ITboObj.ADDPROPTY = txtAddrPropty.Text.Trim();
                            ITboObj.State = txtITSlfState.Text.Trim();
                            ITboObj.City = txtITSlfCity.Text.Trim();
                            ITboObj.PUPSHSLN = txtPurofHsLoan.Text.Trim();
                            ITboObj.LNSAC_DATE = DateTime.Parse(txtLoanSacDt.Text.Trim() == "" ? "1900-01-01" : txtLoanSacDt.Text.Trim());
                            ITboObj.STMPCHR_DATE = DateTime.Parse(txtStampChgrDt.Text.Trim() == "" ? "1900-01-01" : txtLoanSacDt.Text.Trim());
                            ITboObj.PERCNT = 0;
                            ITblObj.Create_ITHousingOthers_cont(ITboObj, 1, ref sts);
                            if (Session["BorwData"] != null)
                            {
                                DataTable dt = (DataTable)Session["BorwData"];
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    ITboObj.ID = ITOTHERSID;
                                    ITboObj.LENDNAME = dt.Rows[i]["NAME"].ToString().Trim();
                                    ITboObj.PERCNT = decimal.Parse(dt.Rows[i]["PERCNT"].ToString().Trim());
                                    ITblObj.Create_ITHousingOthers_cont(ITboObj, 3, ref sts);
                                }
                            }
                            if (sts == true)
                            {
                                SendMailHousingOthers(ITOTHERSID, "Submit", "Income from Other Sources - House Property");
                                //MsgCls("IT House Property created successfully !", lblMessageBoard, Color.Green);
                                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('IT House Property created successfully !')", true);


                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Income Tax House property Request saved successfully');", true);
                                lblMessageBoard.Text = "Income Tax House property Request saved successfully.";
                            }
                            CancelFunc4();
                        }
                    }
                }
                else if (DDl_TYPE.SelectedValue.ToString().Trim() == "2")
                {

                    if ((string.IsNullOrEmpty(TXT_IFI.Text)) && ((string.IsNullOrEmpty(TXT_IFI.Text) ? 0 : decimal.Parse(TXT_IFI.Text.ToString().Trim())) <= 0))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Please enter details before submiting');", true);
                        MsgCls("Please enter details before submiting", lblMessageBoard, System.Drawing.Color.Red);
                        return;
                    }


                    ITbl ITblObj = new ITbl();
                    ITbo ITboObj = new ITbo();
                    ITboObj.PROPTYP = "2";
                    ITboObj.RENTO = "";
                    ITboObj.INT24 = 0;
                    ITboObj.LETVL = 0;
                    ITboObj.REP24 = 0;
                    ITboObj.OTH24 = 0;
                    ITboObj.TDSOT = 0;
                    //-----others
                    ITboObj.BSPFT = string.IsNullOrEmpty(TXT_BusnProfits.Text) ? 0 : decimal.Parse(TXT_BusnProfits.Text.ToString().Trim());
                    ITboObj.CPGLN = string.IsNullOrEmpty(TXT_LTCG.Text) ? 0 : decimal.Parse(TXT_LTCG.Text.ToString().Trim());
                    ITboObj.CPGLS = string.IsNullOrEmpty(TXT_LTCGS.Text) ? 0 : decimal.Parse(TXT_LTCGS.Text.ToString().Trim());
                    ITboObj.CPGNS = string.IsNullOrEmpty(TXT_STCG.Text) ? 0 : decimal.Parse(TXT_STCG.Text.ToString().Trim());
                    ITboObj.CPGSS = string.IsNullOrEmpty(TXT_STCGLS.Text) ? 0 : decimal.Parse(TXT_STCGLS.Text.ToString().Trim());
                    ITboObj.DVDND = string.IsNullOrEmpty(TXT_IFD.Text) ? 0 : decimal.Parse(TXT_IFD.Text.ToString().Trim());
                    ITboObj.INTRS = string.IsNullOrEmpty(TXT_IFI.Text) ? 0 : decimal.Parse(TXT_IFI.Text.ToString().Trim());
                    ITboObj.UNSPI = string.IsNullOrEmpty(TXT_OI.Text) ? 0 : decimal.Parse(TXT_OI.Text.ToString().Trim());
                    ITboObj.TDSAT = string.IsNullOrEmpty(TXT_TDSI.Text) ? 0 : decimal.Parse(TXT_TDSI.Text.ToString().Trim());
                    ITboObj.CREATED_BY = User.Identity.Name;
                    ITboObj.PERNR = User.Identity.Name;
                    ITboObj.STATUS = "Saved";//"Requested";
                    ITboObj.CREATED_ON = DateTime.Now;
                    ITboObj.BEGDA = DateTime.Now;
                    ITboObj.ENDDA = DateTime.Now;
                    ITboObj.MODIFIED_BY = "";
                    ITboObj.Flag = 1;
                    ITboObj.ID = 0;
                    ITboObj.EMPCOMMENTS2 = TXTCOMMENTS2.Text.ToString().Trim();
                    ITblObj.Create_ITHousingOthers(ITboObj, ref ITOTHERSID, ref sts);

                    if (sts == true)
                    {
                        //SendMailHousingOthers(ITOTHERSID, "Submit", "Income from Other Sources");
                        //MsgCls("IT Other Sources created successfully !", LblMsg, Color.Green);
                        //ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('IT Other Sources created successfully !')", true);

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Income Tax Other Sources Request saved successfully');", true);
                        lblMessageBoard.Text = "Income Tax Other Sources Request saved successfully.";
                    }
                    CancelFunc4();
                }

            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
        }
        private void SendMailHousingOthers(int? ITHID, string type, string subtyp)
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

                ////strSubject = "Income Tax Request " + ITHID + " has been " + type + " by " + EMP_Name + "  |  " + User.Identity.Name + " and is pending for the Approval.";
                strSubject = "Income Tax Request " + ITHID + " has been " + type + " by " + EMP_Name + "  |  " + User.Identity.Name;// +" and is pending for the Approval.";

                RecipientsString = "karthik.k@itchamps.com";
                strPernr_Mail = "vaishnavi.k@itchamps.com";

                //    //Preparing the mail body--------------------------------------------------
                ////string body = "<b>Income Tax Request " + ITHID + " has been " + type + " by " + EMP_Name + "  |  " + User.Identity.Name + " and is pending for the Approval.<br/><br/></b>";
                string body = "<b>Income Tax Request " + ITHID + " has been " + type + " by " + EMP_Name + "  |  " + User.Identity.Name;// +" and is pending for the Approval.<br/><br/></b>";
                body += "<b>Income Tax ID  :  " + ITHID + "</b><br/><br/>";
                body += "<b>Income Tax Type :  " + subtyp + "</b><br/><br/>";

                //    //End of preparing the mail body-------------------------------------------
                iEmpPowerMaster_Load.masterbl.DispatchMail(RecipientsString, User.Identity.Name, strSubject, strPernr_Mail, body);
                lblMessageBoard.ForeColor = System.Drawing.Color.Green;
                lblMessageBoard.Text = "Income Tax Request submitted successfully and Mail sent successfully.";
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        }

        protected void TXT_FNLLETVALUE_TextChanged(object sender, EventArgs e)
        {
            try
            {


                // GetDEDREPAIR();

            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
        }

        public void GetDEDREPAIR()
        {
            try
            {
                if ((string.IsNullOrEmpty(TXT_FNLLETVALUE.Text) ? 0 : decimal.Parse(TXT_FNLLETVALUE.Text.ToString().Trim())) > 0)
                {
                    if ((string.IsNullOrEmpty(TXTDECOTHERS.Text) ? 0 : decimal.Parse(TXTDECOTHERS.Text.ToString().Trim())) == 0)
                    {
                        Lbl_DEDREPAIR.Text = (((decimal.Parse(TXT_FNLLETVALUE.Text.ToString().Trim())) * 30) / 100).ToString();
                    }

                    else if (((string.IsNullOrEmpty(TXTDECOTHERS.Text) ? 0 : decimal.Parse(TXTDECOTHERS.Text.ToString().Trim())) > 0)
                        && ((string.IsNullOrEmpty(TXTDECOTHERS.Text) ? 0 : decimal.Parse(TXTDECOTHERS.Text.ToString().Trim())) <= (string.IsNullOrEmpty(TXT_FNLLETVALUE.Text) ? 0 : decimal.Parse(TXT_FNLLETVALUE.Text.ToString().Trim()))))
                    {
                        Lbl_DEDREPAIR.Text = ((((decimal.Parse(TXT_FNLLETVALUE.Text.ToString().Trim())) * 30) / 100) - (((decimal.Parse(TXTDECOTHERS.Text.ToString().Trim())) * 30) / 100)).ToString();
                    }

                    else if (((string.IsNullOrEmpty(TXTDECOTHERS.Text) ? 0 : decimal.Parse(TXTDECOTHERS.Text.ToString().Trim())) > 0)
                        && ((string.IsNullOrEmpty(TXTDECOTHERS.Text) ? 0 : decimal.Parse(TXTDECOTHERS.Text.ToString().Trim())) > (string.IsNullOrEmpty(TXT_FNLLETVALUE.Text) ? 0 : decimal.Parse(TXT_FNLLETVALUE.Text.ToString().Trim()))))
                    {
                        Lbl_DEDREPAIR.Text = "0";
                    }
                }
                else
                {
                    Lbl_DEDREPAIR.Text = "0";

                }

            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
        }

        protected void TXTDECOTHERS_TextChanged(object sender, EventArgs e)
        {
            try
            {

                if ((string.IsNullOrEmpty(TXTDECOTHERS.Text) ? 0 : decimal.Parse(TXTDECOTHERS.Text.ToString().Trim())) > 0)
                {
                    if ((string.IsNullOrEmpty(TXT_FNLLETVALUE.Text) ? 0 : decimal.Parse(TXT_FNLLETVALUE.Text.ToString().Trim())) == 0)
                    {
                        Lbl_DEDREPAIR.Text = "0";
                    }
                    else if (((string.IsNullOrEmpty(TXT_FNLLETVALUE.Text) ? 0 : decimal.Parse(TXT_FNLLETVALUE.Text.ToString().Trim())) > 0)
                        && ((string.IsNullOrEmpty(TXT_FNLLETVALUE.Text) ? 0 : decimal.Parse(TXT_FNLLETVALUE.Text.ToString().Trim())) >=
                        ((string.IsNullOrEmpty(TXTDECOTHERS.Text) ? 0 : decimal.Parse(TXTDECOTHERS.Text.ToString().Trim())))))
                    {
                        Lbl_DEDREPAIR.Text = ((((decimal.Parse(TXT_FNLLETVALUE.Text.ToString().Trim())) * 30) / 100) - (((decimal.Parse(TXTDECOTHERS.Text.ToString().Trim())) * 30) / 100)).ToString();
                    }

                    else if (((string.IsNullOrEmpty(TXT_FNLLETVALUE.Text) ? 0 : decimal.Parse(TXT_FNLLETVALUE.Text.ToString().Trim())) > 0)
                        && ((string.IsNullOrEmpty(TXT_FNLLETVALUE.Text) ? 0 : decimal.Parse(TXT_FNLLETVALUE.Text.ToString().Trim())) <
                        ((string.IsNullOrEmpty(TXTDECOTHERS.Text) ? 0 : decimal.Parse(TXTDECOTHERS.Text.ToString().Trim())))))
                    {
                        Lbl_DEDREPAIR.Text = "0";
                    }

                }

                else if ((string.IsNullOrEmpty(TXTDECOTHERS.Text) ? 0 : decimal.Parse(TXTDECOTHERS.Text.ToString().Trim())) == 0)
                {

                    if ((string.IsNullOrEmpty(TXT_FNLLETVALUE.Text) ? 0 : decimal.Parse(TXT_FNLLETVALUE.Text.ToString().Trim())) == 0)
                    {
                        Lbl_DEDREPAIR.Text = "0";
                    }

                    else if ((string.IsNullOrEmpty(TXT_FNLLETVALUE.Text) ? 0 : decimal.Parse(TXT_FNLLETVALUE.Text.ToString().Trim())) > 0)
                    {
                        Lbl_DEDREPAIR.Text = (((decimal.Parse(TXT_FNLLETVALUE.Text.ToString().Trim())) * 30) / 100).ToString();
                    }

                }



                ////// if (decimal.Parse(TXTDECOTHERS.Text.ToString().Trim()) > 0)
                ////if ((string.IsNullOrEmpty(TXTDECOTHERS.Text) ? 0 : decimal.Parse(TXTDECOTHERS.Text.ToString().Trim())) <= (string.IsNullOrEmpty(TXT_FNLLETVALUE.Text) ? 0 : decimal.Parse(TXT_FNLLETVALUE.Text.ToString().Trim())))
                ////{

                ////    if ((string.IsNullOrEmpty(TXTDECOTHERS.Text) ? 0 : decimal.Parse(TXTDECOTHERS.Text.ToString().Trim())) > 0)
                ////    {
                ////        //TXT_FNLLETVALUE.Text = ((string.IsNullOrEmpty(TXT_FNLLETVALUE.Text) ? 0 : decimal.Parse(TXT_FNLLETVALUE.Text.ToString().Trim())) - (decimal.Parse(TXTDECOTHERS.Text.ToString().Trim()))).ToString();
                ////        //GetDEDREPAIR();
                ////        Lbl_DEDREPAIR.Text = ((((decimal.Parse(TXT_FNLLETVALUE.Text.ToString().Trim())) * 30) / 100) - (((decimal.Parse(TXTDECOTHERS.Text.ToString().Trim())) * 30) / 100)).ToString();

                ////    }
                ////}
                ////else
                ////{
                ////    TXT_FNLLETVALUE.Text = (string.IsNullOrEmpty(TXT_FNLLETVALUE.Text) ? 0 : decimal.Parse(TXT_FNLLETVALUE.Text.ToString().Trim())).ToString();
                ////}

            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
        }

        private void UpDateEntry4(List<ITbo> ITboObj1)
        {
            try
            {
                if (DDl_TYPE.SelectedValue.ToString().Trim() == "1")
                {
                    for (int i = 0; i < ITboObj1.Count; i++)
                    {
                        ViewState["Housing"] = ITboObj1[i].ID.ToString().Trim();
                        HousingProprtyType(ITboObj1[i].RENTO.ToString().Trim());
                        if ((ITboObj1[i].RENTO.ToString().Trim() == "2") || (ITboObj1[i].RENTO.ToString().Trim() == "3"))
                        {
                            TXT_DEDINT.Text = ITboObj1[i].INT24.ToString().Trim();
                            TXT_FNLLETVALUE.Text = ITboObj1[i].LETVL.ToString().Trim();
                            txtMunicipaltax.Text = ITboObj1[i].REP24.ToString().Trim();
                            TXTDECOTHERS.Text = ITboObj1[i].OTH24.ToString().Trim();
                            TXTTDS.Text = ITboObj1[i].TDSOT.ToString().Trim();
                            ddlLoanType.SelectedValue = ITboObj1[i].BSPFT.ToString().Trim();
                            txtTotintPort.Text = ITboObj1[i].CPGLN.ToString().Trim();
                            txtIncm_loss.Text = ITboObj1[i].CPGLS.ToString().Trim();
                            TXTCOMMENTS.Text = ITboObj1[i].EMPCOMMENTS.ToString().Trim();
                            TXTCOMMENTS.Enabled = false;
                            txtMunicipaltax.Enabled = false;
                            //  RB_PropTyp.Enabled = false;
                            //TXT_DedIntr.Enabled = false;
                            TXT_DEDINT.Enabled = false;
                            TXT_FNLLETVALUE.Enabled = false;
                            Lbl_DEDREPAIR.Enabled = false;
                            TXTDECOTHERS.Enabled = false;
                            TXTTDS.Enabled = false;
                            ddlLoanType.Enabled = false;
                            txtTotintPort.Enabled = false;
                            txtIncm_loss.Enabled = false;



                            ITbl ITblObj = new ITbl();
                            ITbo ITboObj = new ITbo();
                            List<ITbo> ITboList = new List<ITbo>();
                            List<ITbo> ITboList2 = new List<ITbo>();
                            bool? sts = false;
                            int ID = int.Parse(ITboObj1[i].EMPCOMMENTS2.ToString().Trim());

                            ITboList = ITblObj.Load_HousingOthersDetails(ID, "1", User.Identity.Name.Trim(), "1");
                            ITboList2 = ITblObj.Load_PreEpInCm_cont(ID, 1);

                            txtLendrName.Text = ITboList2[0].LENDNAME;
                            txtLendrAdd.Text = ITboList2[0].LENDADD;
                            txtLendrPAN.Text = ITboList2[0].LENDPAN;
                            txtLoanSacDt.Text = DateTime.Parse(ITboList2[0].LNSAC_DATE.ToString()).ToString("dd/MM/yyyy") == "1900-01-01" ? "" : DateTime.Parse(ITboList2[0].LNSAC_DATE.ToString()).ToString("dd/MM/yyyy");
                            txtLoanSacAmt.Text = ITboList[0].INT24.ToString();
                            txtStampDuty.Text = ITboList[0].OTH24.ToString();
                            txtTotInsPaid.Text = ITboList[0].CPGLN.ToString();
                            txtTotIns.Text = ITboList[0].CPGSS.ToString();
                            txtTotPrincpl.Text = ITboList[0].INTRS.ToString();
                            chkOwnhsPropty.Checked = ITboList[0].LETVL.ToString() == "0.00" ? false : true;
                            ddlSlfLoanType.SelectedValue = ITboList[0].BSPFT.ToString();
                            chkBenftsSec80EE.Checked = ITboList[0].DVDND.ToString() == "0.00" ? false : true;
                            txtTotPrinPaid.Text = ITboList[0].CPGLS.ToString();
                            txtStampChgrDt.Text = DateTime.Parse(ITboList2[0].STMPCHR_DATE.ToString()).ToString("dd/MM/yyyy") == "1900-01-01" ? "" : DateTime.Parse(ITboList2[0].STMPCHR_DATE.ToString()).ToString("dd/MM/yyyy");
                            txtValPropty.Text = ITboList[0].REP24.ToString();
                            txtCaptSqFt.Text = ITboList[0].TDSAT.ToString();
                            txtPurofHsLoan.Text = ITboList2[0].PUPSHSLN;
                            txtITSlfCity.Text = ITboList2[0].City;
                            txtITSlfState.Text = ITboList2[0].State;
                            txtAddrPropty.Text = ITboList2[0].ADDPROPTY;
                            TXTCOMMENTS.Text = ITboList[0].EMPCOMMENTS.ToString();

                            txtBorwName.Enabled = false;
                            txtBorwPerct.Enabled = false;
                            lnkborAdd.Enabled = false;
                            txtLendrName.Enabled = false;
                            txtLendrAdd.Enabled = false;
                            txtLendrPAN.Enabled = false;
                            txtLoanSacDt.Enabled = false;
                            txtLoanSacAmt.Enabled = false;
                            txtStampDuty.Enabled = false;
                            txtTotInsPaid.Enabled = false;
                            txtTotIns.Enabled = false;
                            txtTotPrincpl.Enabled = false;
                            chkOwnhsPropty.Enabled = false;
                            ddlSlfLoanType.Enabled = false;
                            chkBenftsSec80EE.Enabled = false;
                            txtTotPrinPaid.Enabled = false;
                            txtStampChgrDt.Enabled = false;
                            txtValPropty.Enabled = false;
                            txtCaptSqFt.Enabled = false;
                            txtPurofHsLoan.Enabled = false;
                            txtITSlfCity.Enabled = false;
                            txtITSlfState.Enabled = false;
                            txtAddrPropty.Enabled = false;
                            TXTCOMMENTS.Enabled = false;


                            ITboList = ITblObj.Load_PreEpInCm_cont(ID, 2);

                            DataTable dt = dtBorw();
                            for (int j = 0; j < ITboList.Count; j++)
                            {
                                dt.Rows.Add(ITboList[j].RID, ITboList[j].NAME, ITboList[j].PERCNT);
                            }
                            grdBorwDetails.DataSource = Session["BorwData"] = dt;
                            grdBorwDetails.DataBind();
                            grdBorwDetails.Columns[3].Visible = true;

                            btnUpdate4.Visible = btnUpdate4.Enabled = false;
                            BtnEDIT4.Visible = BtnEDIT4.Enabled = true;
                            BtnCancel4.Visible = BtnCancel4.Enabled = true;
                            BTNSubmitHousingOthers.Visible = BTNSubmitHousingOthers.Enabled = false;



                            return;
                        }
                    }
                }
                else if (DDl_TYPE.SelectedValue.ToString().Trim() == "2")
                {

                    //ITboObj.BSPFT = vRow.BSPFT;
                    //ITboObj.CPGLN = vRow.CPGLN;
                    //ITboObj.CPGLS = vRow.CPGLS;
                    //ITboObj.CPGNS = vRow.CPGNS;
                    //ITboObj.CPGSS = vRow.CPGSS;
                    //ITboObj.DVDND = vRow.DVDND;
                    //ITboObj.INTRS = vRow.INTRS;
                    //ITboObj.UNSPI = vRow.UNSPI;
                    //ITboObj.TDSAT = vRow.TDSAT;
                    ViewState["Others"] = ITboObj1[0].ID.ToString().Trim();
                    TXT_BusnProfits.Text = ITboObj1[0].BSPFT.ToString().Trim();
                    TXT_LTCG.Text = ITboObj1[0].CPGLN.ToString().Trim();
                    TXT_LTCGS.Text = ITboObj1[0].CPGLS.ToString().Trim();
                    TXT_STCG.Text = ITboObj1[0].CPGNS.ToString().Trim();
                    TXT_STCGLS.Text = ITboObj1[0].CPGSS.ToString().Trim();
                    TXT_IFD.Text = ITboObj1[0].DVDND.ToString().Trim();
                    TXT_IFI.Text = ITboObj1[0].INTRS.ToString().Trim();
                    TXT_OI.Text = ITboObj1[0].UNSPI.ToString().Trim();
                    TXT_TDSI.Text = ITboObj1[0].TDSAT.ToString().Trim();
                    TXTCOMMENTS2.Text = ITboObj1[0].EMPCOMMENTS2.ToString().Trim();

                    TXT_BusnProfits.Enabled = false;
                    TXT_LTCG.Enabled = false;
                    TXT_LTCGS.Enabled = false;
                    TXT_STCG.Enabled = false;
                    TXT_STCGLS.Enabled = false;
                    TXT_IFD.Enabled = false;
                    TXT_IFI.Enabled = false;
                    TXT_OI.Enabled = false;
                    TXT_TDSI.Enabled = false;
                    TXTCOMMENTS2.Enabled = false;

                    btnUpdate4.Visible = btnUpdate4.Enabled = false;
                    BtnEDIT4.Visible = BtnEDIT4.Enabled = true;
                    BtnCancel4.Visible = BtnCancel4.Enabled = true;
                    BTNSubmitHousingOthers.Visible = BTNSubmitHousingOthers.Enabled = false;
                }
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        }

        private void NewEntry4()
        {
            try
            {
                if (DDl_TYPE.SelectedValue.ToString().Trim() == "1")
                {
                    RB_PropTyp.SelectedValue = "3";
                    ClearControls4();
                    btnUpdate4.Visible = btnUpdate4.Enabled = false;
                    BtnEDIT4.Visible = BtnEDIT4.Enabled = false;
                    BtnCancel.Visible = BtnCancel.Enabled = true;
                    BTNSubmitHousingOthers.Visible = BTNSubmitHousingOthers.Enabled = true;
                }
                else if (DDl_TYPE.SelectedValue.ToString().Trim() == "2")
                {
                    ClearControlsView24();
                    btnUpdate4.Visible = btnUpdate4.Enabled = false;
                    BtnEDIT4.Visible = BtnEDIT4.Enabled = false;
                    BtnCancel.Visible = BtnCancel.Enabled = true;
                    BTNSubmitHousingOthers.Visible = BTNSubmitHousingOthers.Enabled = true;
                }
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        }

        protected void BtnEDIT4_Click(object sender, EventArgs e)
        {
            if (DDl_TYPE.SelectedValue.ToString().Trim() == "1")
            {
                //if (RB_PropTyp.SelectedValue.ToString().Trim() == "1")
                //{
                txtLendrName.Enabled = true;
                txtLendrAdd.Enabled = true;
                txtLendrPAN.Enabled = true;
                txtLoanSacDt.Enabled = true;
                txtLoanSacAmt.Enabled = true;
                txtStampDuty.Enabled = true;
                txtTotInsPaid.Enabled = true;
                //txtTotIns.Enabled = true;
                //txtTotPrincpl.Enabled = true;
                chkOwnhsPropty.Enabled = true;
                ddlSlfLoanType.Enabled = true;
                chkBenftsSec80EE.Enabled = true;
                txtTotPrinPaid.Enabled = true;
                txtStampChgrDt.Enabled = true;
                txtValPropty.Enabled = true;
                txtCaptSqFt.Enabled = true;
                txtPurofHsLoan.Enabled = true;
                txtITSlfCity.Enabled = true;
                txtITSlfState.Enabled = true;
                txtAddrPropty.Enabled = true;

                TXTCOMMENTS.Visible = TXTCOMMENTS.Enabled = true;
                btnUpdate4.Visible = btnUpdate4.Enabled = true;
                BtnEDIT4.Visible = BtnEDIT4.Enabled = false;
                BtnCancel.Visible = BtnCancel.Enabled = true;
                BTNSubmitHousingOthers.Visible = BTNSubmitHousingOthers.Enabled = false;
                grdBorwDetails.Columns[3].Visible = true;
                txtBorwName.Enabled = true;
                txtBorwPerct.Enabled = true;
                lnkborAdd.Enabled = true;
                //}

                //if (RB_PropTyp.SelectedValue.ToString().Trim() == "3")
                //{
                TXTCOMMENTS.Visible = TXTCOMMENTS.Enabled = true;
                //RB_PropTyp.Visible = RB_PropTyp.Enabled = true;
                // TXT_DedIntr.Visible = TXT_DedIntr.Enabled = true;
                TXT_DEDINT.Visible = true;
                TXT_FNLLETVALUE.Visible = TXT_FNLLETVALUE.Enabled = true;
                txtMunicipaltax.Visible = txtMunicipaltax.Enabled = true;
                TXTDECOTHERS.Visible = true;
                TXTTDS.Visible = TXTTDS.Enabled = true;
                ddlLoanType.Visible = ddlLoanType.Enabled = true;
                txtTotintPort.Visible = txtTotintPort.Enabled = true;
                txtIncm_loss.Visible = true;

                btnUpdate4.Visible = btnUpdate4.Enabled = true;
                BtnEDIT4.Visible = BtnEDIT4.Enabled = false;
                BtnCancel.Visible = BtnCancel.Enabled = true;
                BTNSubmitHousingOthers.Visible = BTNSubmitHousingOthers.Enabled = false;
                Div_Proptyp.Visible = false;
                //  }
            }
            else if (DDl_TYPE.SelectedValue.ToString().Trim() == "2")
            {


                TXT_BusnProfits.Visible = TXT_BusnProfits.Enabled = true;
                TXT_LTCG.Visible = TXT_LTCG.Enabled = true;
                TXT_LTCGS.Visible = TXT_LTCGS.Enabled = true;
                TXT_STCG.Visible = TXT_STCG.Enabled = true;
                TXT_STCGLS.Visible = TXT_STCGLS.Enabled = true;
                TXT_IFD.Visible = TXT_IFD.Enabled = true;
                TXT_IFI.Visible = TXT_IFI.Enabled = true;
                TXT_OI.Visible = TXT_OI.Enabled = true;
                TXT_TDSI.Visible = TXT_TDSI.Enabled = true;
                TXTCOMMENTS2.Enabled = true;

                btnUpdate4.Visible = btnUpdate4.Enabled = true;
                BtnEDIT4.Visible = BtnEDIT4.Enabled = false;
                BtnCancel.Visible = BtnCancel.Enabled = true;
                BTNSubmitHousingOthers.Visible = BTNSubmitHousingOthers.Enabled = false;
                Div_Proptyp.Visible = false;
            }

        }

        protected void btnUpdateITHousingOthers_Click(object sender, EventArgs e)
        {
            try
            {
                bool? stus = false;
                int? ITOTHERSIDU = 0;

                if (DDl_TYPE.SelectedValue.ToString().Trim() == "1")
                {
                    if (txtLendrName.Text.Trim() != "" && txtLendrPAN.Text.Trim() == "")
                    {
                        lblMessageBoard.Text = "Please ENTER Lender PAN";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Please ENTER Lender PAN');", true);
                        return;
                    }
                    else
                    {
                        if (RB_PropTyp.SelectedValue.ToString().Trim() == "1")
                        {

                            ITbl ITblObj = new ITbl();
                            ITbo ITboObj = new ITbo();
                            //------Housing property
                            ITboObj.PROPTYP = "1";
                            ITboObj.RENTO = RB_PropTyp.SelectedValue.ToString().Trim();
                            ITboObj.INT24 = string.IsNullOrEmpty(txtLoanSacAmt.Text) ? 0 : decimal.Parse(txtLoanSacAmt.Text.ToString().Trim());
                            ITboObj.LETVL = decimal.Parse(chkOwnhsPropty.Checked == true ? "1.00" : "0.00");
                            ITboObj.REP24 = string.IsNullOrEmpty(txtValPropty.Text) ? 0 : decimal.Parse(txtValPropty.Text.ToString().Trim());
                            ITboObj.OTH24 = string.IsNullOrEmpty(txtStampDuty.Text) ? 0 : decimal.Parse(txtStampDuty.Text.ToString().Trim());
                            ITboObj.TDSOT = 0;
                            //------Others
                            ITboObj.BSPFT = decimal.Parse(ddlSlfLoanType.SelectedValue);
                            ITboObj.CPGLN = string.IsNullOrEmpty(txtTotInsPaid.Text) ? 0 : decimal.Parse(txtTotInsPaid.Text.ToString().Trim());
                            ITboObj.CPGLS = string.IsNullOrEmpty(txtTotPrinPaid.Text) ? 0 : decimal.Parse(txtTotPrinPaid.Text.ToString().Trim());
                            ITboObj.CPGNS = 0;
                            ITboObj.CPGSS = string.IsNullOrEmpty(txtTotIns.Text) ? 0 : decimal.Parse(txtTotIns.Text.ToString().Trim());
                            ITboObj.DVDND = decimal.Parse(chkBenftsSec80EE.Checked == true ? "1.00" : "0.00");
                            ITboObj.INTRS = string.IsNullOrEmpty(txtTotPrincpl.Text) ? 0 : decimal.Parse(txtTotPrincpl.Text.ToString().Trim());
                            ITboObj.UNSPI = 0;
                            ITboObj.TDSAT = string.IsNullOrEmpty(txtCaptSqFt.Text) ? 0 : decimal.Parse(txtCaptSqFt.Text.ToString().Trim());
                            ITboObj.CREATED_BY = User.Identity.Name;
                            ITboObj.PERNR = User.Identity.Name;
                            ITboObj.STATUS = "Saved";//"Updated";
                            ITboObj.CREATED_ON = DateTime.Now;
                            ITboObj.BEGDA = DateTime.Now;
                            ITboObj.ENDDA = DateTime.Now;
                            ITboObj.MODIFIED_BY = User.Identity.Name;
                            ITboObj.Flag = 2;
                            ITboObj.ID = int.Parse(ViewState["SelfID"].ToString().Trim());
                            ITboObj.EMPCOMMENTS = TXTCOMMENTS.Text.ToString().Trim();
                            ITblObj.Create_ITHousingOthers(ITboObj, ref ITOTHERSIDU, ref stus);
                            ITboObj.LENDNAME = txtLendrName.Text.Trim();
                            ITboObj.LENDPAN = txtLendrPAN.Text.Trim();
                            ITboObj.LENDADD = txtLendrAdd.Text.Trim();
                            ITboObj.ADDPROPTY = txtAddrPropty.Text.Trim();
                            ITboObj.State = txtITSlfState.Text.Trim();
                            ITboObj.City = txtITSlfCity.Text.Trim();
                            ITboObj.PUPSHSLN = txtPurofHsLoan.Text.Trim();
                            ITboObj.LNSAC_DATE = DateTime.Parse(txtLoanSacDt.Text.Trim() == "" ? "1900-01-01" : txtStampChgrDt.Text.Trim());
                            ITboObj.STMPCHR_DATE = DateTime.Parse(txtStampChgrDt.Text.Trim() == "" ? "1900-01-01" : txtStampChgrDt.Text.Trim());
                            ITboObj.PERCNT = 0;
                            ITblObj.Create_ITHousingOthers_cont(ITboObj, 2, ref stus);
                            if (Session["BorwData"] != null)
                            {
                                DataTable dt = (DataTable)Session["BorwData"];
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    ITboObj.LENDNAME = dt.Rows[i]["NAME"].ToString().Trim();
                                    ITboObj.PERCNT = decimal.Parse(dt.Rows[i]["PERCNT"].ToString().Trim());
                                    ITblObj.Create_ITHousingOthers_cont(ITboObj, 4, ref stus);
                                }
                            }
                            if (stus == true)
                            {
                                SendMailHousingOthers(ITOTHERSIDU, "Updated", "Income from Other Sources - House Property");
                                //MsgCls("IT House Property updated successfully !", lblMessageBoard, Color.Green);
                                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('IT House Property updated successfully !')", true);
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Income Tax House Property Request saved successfully');", true);
                                lblMessageBoard.Text = "Income Tax House Property Request saved successfully.";
                            }
                            CancelFunc4(); Load_grdSelfOccDetails();

                        }
                        else if ((RB_PropTyp.SelectedValue.ToString().Trim() == "2") || (RB_PropTyp.SelectedValue.ToString().Trim() == "3"))
                        {

                            if (((string.IsNullOrEmpty(TXT_DEDINT.Text)) && ((string.IsNullOrEmpty(TXT_DEDINT.Text) ? 0 : decimal.Parse(TXT_DEDINT.Text.ToString().Trim())) <= 0)) ||
                                ((string.IsNullOrEmpty(TXT_FNLLETVALUE.Text)) && ((string.IsNullOrEmpty(TXT_FNLLETVALUE.Text) ? 0 : decimal.Parse(TXT_FNLLETVALUE.Text.ToString().Trim())) <= 0)) ||
                                ((string.IsNullOrEmpty(Lbl_DEDREPAIR.Text)) && ((string.IsNullOrEmpty(Lbl_DEDREPAIR.Text) ? 0 : decimal.Parse(Lbl_DEDREPAIR.Text.ToString().Trim())) <= 0)) ||
                                ((string.IsNullOrEmpty(TXTDECOTHERS.Text)) && ((string.IsNullOrEmpty(TXTDECOTHERS.Text) ? 0 : decimal.Parse(TXTDECOTHERS.Text.ToString().Trim())) <= 0)) ||
                                ((string.IsNullOrEmpty(TXTTDS.Text)) && ((string.IsNullOrEmpty(TXTTDS.Text) ? 0 : decimal.Parse(TXTTDS.Text.ToString().Trim())) <= 0)))
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Please enter details before submiting');", true);
                                MsgCls("Please enter details before submiting", lblMessageBoard, System.Drawing.Color.Red);
                                return;
                            }



                            ITbl ITblObj = new ITbl();
                            ITbo ITboObj = new ITbo();
                            //------Housing property
                            ITboObj.PROPTYP = "1";
                            ITboObj.RENTO = "3";
                            ITboObj.INT24 = string.IsNullOrEmpty(TXT_DEDINT.Text) ? 0 : decimal.Parse(TXT_DEDINT.Text.ToString().Trim());
                            ITboObj.LETVL = string.IsNullOrEmpty(TXT_FNLLETVALUE.Text) ? 0 : decimal.Parse(TXT_FNLLETVALUE.Text.ToString().Trim());
                            ITboObj.REP24 = string.IsNullOrEmpty(txtMunicipaltax.Text) ? 0 : decimal.Parse(txtMunicipaltax.Text.ToString().Trim());
                            ITboObj.OTH24 = string.IsNullOrEmpty(TXTDECOTHERS.Text) ? 0 : decimal.Parse(TXTDECOTHERS.Text.ToString().Trim());
                            ITboObj.TDSOT = string.IsNullOrEmpty(TXTTDS.Text) ? 0 : decimal.Parse(TXTTDS.Text.ToString().Trim());
                            //------Others
                            ITboObj.BSPFT = decimal.Parse(ddlLoanType.SelectedValue);
                            ITboObj.CPGLN = string.IsNullOrEmpty(txtTotintPort.Text) ? 0 : decimal.Parse(txtTotintPort.Text.ToString().Trim());
                            ITboObj.CPGLS = string.IsNullOrEmpty(txtIncm_loss.Text) ? 0 : decimal.Parse(txtIncm_loss.Text.ToString().Trim());
                            ITboObj.CPGNS = 0;
                            ITboObj.CPGSS = 0;
                            ITboObj.DVDND = 0;
                            ITboObj.INTRS = 0;
                            ITboObj.UNSPI = 0;
                            ITboObj.TDSAT = 0;
                            ITboObj.CREATED_BY = User.Identity.Name;
                            ITboObj.PERNR = User.Identity.Name;
                            ITboObj.STATUS = "Saved";//"Updated";
                            ITboObj.CREATED_ON = DateTime.Now;
                            ITboObj.BEGDA = DateTime.Now;
                            ITboObj.ENDDA = DateTime.Now;
                            ITboObj.MODIFIED_BY = User.Identity.Name;
                            ITboObj.Flag = 2;
                            ITboObj.ID = int.Parse(ViewState["LetID"].ToString().Trim());
                            ITboObj.EMPCOMMENTS = TXTCOMMENTS.Text.ToString().Trim();
                            ITblObj.Create_ITHousingOthers(ITboObj, ref ITOTHERSIDU, ref stus);

                            ITboObj.RENTO = "";
                            ITboObj.INT24 = string.IsNullOrEmpty(txtLoanSacAmt.Text) ? 0 : decimal.Parse(txtLoanSacAmt.Text.ToString().Trim());
                            ITboObj.LETVL = decimal.Parse(chkOwnhsPropty.Checked == true ? "1.00" : "0.00");
                            ITboObj.REP24 = string.IsNullOrEmpty(txtValPropty.Text) ? 0 : decimal.Parse(txtValPropty.Text.ToString().Trim());
                            ITboObj.OTH24 = string.IsNullOrEmpty(txtStampDuty.Text) ? 0 : decimal.Parse(txtStampDuty.Text.ToString().Trim());
                            ITboObj.TDSOT = 0;
                            //------Others
                            ITboObj.BSPFT = decimal.Parse(ddlSlfLoanType.SelectedValue);
                            ITboObj.CPGLN = string.IsNullOrEmpty(txtTotInsPaid.Text) ? 0 : decimal.Parse(txtTotInsPaid.Text.ToString().Trim());
                            ITboObj.CPGLS = string.IsNullOrEmpty(txtTotPrinPaid.Text) ? 0 : decimal.Parse(txtTotPrinPaid.Text.ToString().Trim());
                            ITboObj.CPGNS = 0;
                            ITboObj.CPGSS = string.IsNullOrEmpty(txtTotIns.Text) ? 0 : decimal.Parse(txtTotIns.Text.ToString().Trim());
                            ITboObj.DVDND = decimal.Parse(chkBenftsSec80EE.Checked == true ? "1.00" : "0.00");
                            ITboObj.INTRS = string.IsNullOrEmpty(txtTotPrincpl.Text) ? 0 : decimal.Parse(txtTotPrincpl.Text.ToString().Trim());
                            ITboObj.UNSPI = 0;
                            ITboObj.TDSAT = string.IsNullOrEmpty(txtCaptSqFt.Text) ? 0 : decimal.Parse(txtCaptSqFt.Text.ToString().Trim());
                            ITboObj.CREATED_BY = User.Identity.Name;
                            ITboObj.PERNR = User.Identity.Name;
                            ITboObj.STATUS = "Saved";//"Updated";
                            ITboObj.CREATED_ON = DateTime.Now;
                            ITboObj.BEGDA = DateTime.Now;
                            ITboObj.ENDDA = DateTime.Now;
                            ITboObj.MODIFIED_BY = User.Identity.Name;
                            ITboObj.Flag = 2;
                            ITboObj.ID = int.Parse(ViewState["LetIDNxt"].ToString().Trim());
                            ITboObj.EMPCOMMENTS = TXTCOMMENTS.Text.ToString().Trim();

                            ITblObj.Create_ITHousingOthers(ITboObj, ref ITOTHERSIDU, ref stus);

                            ITboObj.LENDNAME = txtLendrName.Text.Trim();
                            ITboObj.LENDPAN = txtLendrPAN.Text.Trim();
                            ITboObj.LENDADD = txtLendrAdd.Text.Trim();
                            ITboObj.ADDPROPTY = txtAddrPropty.Text.Trim();
                            ITboObj.State = txtITSlfState.Text.Trim();
                            ITboObj.City = txtITSlfCity.Text.Trim();
                            ITboObj.PUPSHSLN = txtPurofHsLoan.Text.Trim();
                            ITboObj.LNSAC_DATE = DateTime.Parse(txtLoanSacDt.Text.Trim() == "" ? "1900-01-01" : txtStampChgrDt.Text.Trim());
                            ITboObj.STMPCHR_DATE = DateTime.Parse(txtStampChgrDt.Text.Trim() == "" ? "1900-01-01" : txtStampChgrDt.Text.Trim());
                            ITboObj.PERCNT = 0;
                            ITblObj.Create_ITHousingOthers_cont(ITboObj, 2, ref stus);
                            if (Session["BorwData"] != null)
                            {
                                DataTable dt = (DataTable)Session["BorwData"];
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    ITboObj.LENDNAME = dt.Rows[i]["NAME"].ToString().Trim();
                                    ITboObj.PERCNT = decimal.Parse(dt.Rows[i]["PERCNT"].ToString().Trim());
                                    ITblObj.Create_ITHousingOthers_cont(ITboObj, 4, ref stus);
                                }
                            }


                            if (stus == true)
                            {
                                //SendMailHousingOthers(ITOTHERSIDU, "Updated", "Income from Other Sources - House Property");
                                //MsgCls("IT House Property updated successfully !", lblMessageBoard, Color.Green);
                                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('IT House Property updated successfully !')", true);
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Income Tax House Property Request saved successfully');", true);
                                lblMessageBoard.Text = "Income Tax House Property Request saved successfully.";
                            }
                            CancelFunc4();
                        }
                    }
                }
                else if (DDl_TYPE.SelectedValue.ToString().Trim() == "2")
                {



                    if (((string.IsNullOrEmpty(TXT_BusnProfits.Text)) && ((string.IsNullOrEmpty(TXT_BusnProfits.Text) ? 0 : decimal.Parse(TXT_BusnProfits.Text.ToString().Trim())) <= 0)) ||
                           ((string.IsNullOrEmpty(TXT_LTCG.Text)) && ((string.IsNullOrEmpty(TXT_LTCG.Text) ? 0 : decimal.Parse(TXT_LTCG.Text.ToString().Trim())) <= 0)) ||
                           ((string.IsNullOrEmpty(TXT_LTCGS.Text)) && ((string.IsNullOrEmpty(TXT_LTCGS.Text) ? 0 : decimal.Parse(TXT_LTCGS.Text.ToString().Trim())) <= 0)) ||
                           ((string.IsNullOrEmpty(TXT_STCG.Text)) && ((string.IsNullOrEmpty(TXT_STCG.Text) ? 0 : decimal.Parse(TXT_STCG.Text.ToString().Trim())) <= 0)) ||
                            ((string.IsNullOrEmpty(TXT_STCGLS.Text)) && ((string.IsNullOrEmpty(TXT_STCGLS.Text) ? 0 : decimal.Parse(TXT_STCGLS.Text.ToString().Trim())) <= 0)) ||
                             ((string.IsNullOrEmpty(TXT_IFD.Text)) && ((string.IsNullOrEmpty(TXT_IFD.Text) ? 0 : decimal.Parse(TXT_IFD.Text.ToString().Trim())) <= 0)) ||
                              ((string.IsNullOrEmpty(TXT_IFI.Text)) && ((string.IsNullOrEmpty(TXT_IFI.Text) ? 0 : decimal.Parse(TXT_IFI.Text.ToString().Trim())) <= 0)) ||
                               ((string.IsNullOrEmpty(TXT_OI.Text)) && ((string.IsNullOrEmpty(TXT_OI.Text) ? 0 : decimal.Parse(TXT_OI.Text.ToString().Trim())) <= 0)) ||
                           ((string.IsNullOrEmpty(TXT_TDSI.Text)) && ((string.IsNullOrEmpty(TXT_TDSI.Text) ? 0 : decimal.Parse(TXT_TDSI.Text.ToString().Trim())) <= 0)))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Please enter details before submiting');", true);
                        MsgCls("Please enter details before submiting", lblMessageBoard, System.Drawing.Color.Red);
                        return;
                    }


                    ITbl ITblObj = new ITbl();
                    ITbo ITboObj = new ITbo();
                    ITboObj.PROPTYP = "2";
                    ITboObj.RENTO = "";
                    ITboObj.INT24 = 0;
                    ITboObj.LETVL = 0;
                    ITboObj.REP24 = 0;
                    ITboObj.OTH24 = 0;
                    ITboObj.TDSOT = 0;
                    //-----others
                    ITboObj.BSPFT = string.IsNullOrEmpty(TXT_BusnProfits.Text) ? 0 : decimal.Parse(TXT_BusnProfits.Text.ToString().Trim());
                    ITboObj.CPGLN = string.IsNullOrEmpty(TXT_LTCG.Text) ? 0 : decimal.Parse(TXT_LTCG.Text.ToString().Trim());
                    ITboObj.CPGLS = string.IsNullOrEmpty(TXT_LTCGS.Text) ? 0 : decimal.Parse(TXT_LTCGS.Text.ToString().Trim());
                    ITboObj.CPGNS = string.IsNullOrEmpty(TXT_STCG.Text) ? 0 : decimal.Parse(TXT_STCG.Text.ToString().Trim());
                    ITboObj.CPGSS = string.IsNullOrEmpty(TXT_STCGLS.Text) ? 0 : decimal.Parse(TXT_STCGLS.Text.ToString().Trim());
                    ITboObj.DVDND = string.IsNullOrEmpty(TXT_IFD.Text) ? 0 : decimal.Parse(TXT_IFD.Text.ToString().Trim());
                    ITboObj.INTRS = string.IsNullOrEmpty(TXT_IFI.Text) ? 0 : decimal.Parse(TXT_IFI.Text.ToString().Trim());
                    ITboObj.UNSPI = string.IsNullOrEmpty(TXT_OI.Text) ? 0 : decimal.Parse(TXT_OI.Text.ToString().Trim());
                    ITboObj.TDSAT = string.IsNullOrEmpty(TXT_TDSI.Text) ? 0 : decimal.Parse(TXT_TDSI.Text.ToString().Trim());
                    ITboObj.CREATED_BY = User.Identity.Name;
                    ITboObj.PERNR = User.Identity.Name;
                    ITboObj.STATUS = "Saved";//"Updated";
                    ITboObj.CREATED_ON = DateTime.Now;
                    ITboObj.BEGDA = DateTime.Now;
                    ITboObj.ENDDA = DateTime.Now;
                    ITboObj.MODIFIED_BY = User.Identity.Name;
                    ITboObj.Flag = 2;
                    ITboObj.ID = int.Parse(ViewState["Others"].ToString().Trim());
                    ITboObj.EMPCOMMENTS2 = TXTCOMMENTS2.Text.ToString().Trim();
                    ITblObj.Create_ITHousingOthers(ITboObj, ref ITOTHERSIDU, ref stus);
                    if (stus == true)
                    {
                        //SendMailHousingOthers(ITOTHERSIDU, "Updated", "Income from Other Sources");
                        //MsgCls("IT Other Sources updated successfully !", LblMsg, Color.Green);
                        //ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('IT Other Sources updated successfully !')", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Income Tax Other Sources Request saved successfully');", true);
                        lblMessageBoard.Text = "Income Tax Other Sources Request saved successfully.";
                    }
                    CancelFunc4();
                }

            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }

        }

        protected void BtnCancel4_Click(object sender, EventArgs e)
        {
            try
            {

                //GetFinancialDates();
                //ClearControls();
                //ClearControlsView2();
                //DIVCOMMENTS.Visible = false;
                //BTNSubmitHousingOthers.Visible = BTNSubmitHousingOthers.Enabled = false;
                //BtnEDIT.Visible = BtnEDIT.Enabled = false;
                //btnUpdate.Visible = btnUpdate.Enabled = false;
                //BtnCancel.Visible = BtnCancel.Enabled = false;
                //MV_IncomeSources.Visible = false;
                //Div_Proptyp.Visible = true;
                //DDl_TYPE.SelectedValue = "0";
                CancelFunc4();
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }

        }

        public void CancelFunc4()
        {
            try
            {
                //Page.ClientScript.RegisterClientScriptBlock(

                GetFinancialDates();
                ClearControls4();
                // ClearControlsView24();
                DIVCOMMENTS.Visible = false;
                BTNSubmitHousingOthers.Visible = BTNSubmitHousingOthers.Enabled = false;
                BtnEDIT4.Visible = BtnEDIT4.Enabled = false;
                btnUpdate4.Visible = btnUpdate4.Enabled = false;
                BtnCancel.Visible = BtnCancel.Enabled = false;
                MV_IncomeSources.Visible = false;
                Div_Proptyp.Visible = true;
                DDl_TYPE.SelectedValue = "0";
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        }

        /*------------IT_IncomeOtherSources Ends------*/

        /*------------IT_History Starts------*/

        public void pageLoadEvents5()
        {
            Tab1H.CssClass = "Initial";
            Tab2H.CssClass = "Clicked";
            Tab3H.CssClass = "Initial";
            Tab4H.CssClass = "Initial";
            Tab5H.CssClass = "Initial";
            MultiViewH.ActiveViewIndex = 0;
            LoadSec80C();
            GridControls();
            HFTabID.Value = "2";
            ddlSeachSelect.SelectedValue = "0";
            txtsearch.Text = string.Empty;
            txtFromDateH.Text = string.Empty;
            txtTodateH.Text = string.Empty;
            MsgCls("", lblMessageBoard, Color.Transparent);
            MsgCls("", LblSec80c, Color.Transparent);
            MsgCls("", LblHousing, Color.Transparent);
            MsgCls("", LblOthers, Color.Transparent);
            viewcheckSec80.Value = "NO";



            //Newly added starts
            Tab1H.CssClass = "Initial";
            Tab2H.CssClass = "Clicked";
            Tab3H.CssClass = "Initial";
            Tab4H.CssClass = "Initial";
            Tab5H.CssClass = "Initial";
            MultiViewH.ActiveViewIndex = 1;
            LoadSec80C();
            GridControls();
            HFTabID.Value = "2";
            viewcheckSec80C.Value = "NO";
            //Newly added ends
        }

        public void GridControls()
        {
            try
            {
                GVITSec80H.Visible = false;
                GVITSec80CH.Visible = false;
                GVHousing.Visible = false;
                GVOthers1.Visible = false;
                GVOthers2.Visible = false;
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        }

        private void LoadSec80()
        {
            try
            {

                int flag = 1;
                ITbl ITblObj = new ITbl();
                List<ITbo> ITboObj = new List<ITbo>();
                List<ITbo> ITboObj1 = new List<ITbo>();
                string ApproverId = User.Identity.Name;
                ITboObj1 = ITblObj.Load_IT_HeaderHistory(User.Identity.Name, flag);
                Session.Add("ITSec80GrdInfo", ITboObj1);

                if (ITboObj1 == null || ITboObj1.Count == 0)
                {
                    MsgCls("No Records Found !", lblMessageBoard, Color.Red);
                    GVSec80Header.Visible = false;
                    GVSec80Header.DataSource = null;
                    GVSec80Header.DataBind();
                    ExportbtnSec80.Visible = false;
                    return;
                }
                else
                {
                    GVSec80Header.Visible = true;
                    GVSec80Header.DataSource = ITboObj1;
                    GVSec80Header.SelectedIndex = -1;
                    GVSec80Header.DataBind();
                    ExportbtnSec80.Visible = true;
                }
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        }

        //private void GetFinancialDates()
        //{
        //    try
        //    {
        //        //DateTime dt = DateTime.Now;
        //        int month = int.Parse(DateTime.Today.Month.ToString());
        //        if (month > 3)
        //        {
        //            LblFromDate.Text = DateTime.Today.Year.ToString();
        //            LblToDate.Text = (DateTime.Today.Year + 1).ToString();
        //        }
        //        else if (month <= 3)
        //        {
        //            LblFromDate.Text = (DateTime.Today.Year - 1).ToString();
        //            LblToDate.Text = DateTime.Today.Year.ToString();
        //        }
        //    }
        //    catch (Exception Ex)
        //    { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        //}

        protected void Tab1H_Click(object sender, EventArgs e)
        {
            ddlSeachSelect.SelectedValue = "0";
            txtsearch.Text = string.Empty;
            txtFromDateH.Text = string.Empty;
            txtTodateH.Text = string.Empty;
            MsgCls("", lblMessageBoard, Color.Transparent);
            MsgCls("", LblSec80c, Color.Transparent);
            MsgCls("", LblHousing, Color.Transparent);
            MsgCls("", LblOthers, Color.Transparent);
            Tab1H.CssClass = "Clicked";
            Tab2H.CssClass = "Initial";
            Tab3H.CssClass = "Initial";
            Tab4H.CssClass = "Initial";
            Tab5H.CssClass = "Initial";
            MultiViewH.ActiveViewIndex = 0;
            LoadSec80();
            GridControls();
            HFTabID.Value = "1";
            viewcheckSec80.Value = "NO";
        }

        protected void Tab2H_Click(object sender, EventArgs e)
        {
            ddlSeachSelect.SelectedValue = "0";
            txtsearch.Text = string.Empty;
            txtFromDateH.Text = string.Empty;
            txtTodateH.Text = string.Empty;
            MsgCls("", lblMessageBoard, Color.Transparent);
            MsgCls("", LblSec80c, Color.Transparent);
            MsgCls("", LblHousing, Color.Transparent);
            MsgCls("", LblOthers, Color.Transparent);
            Tab1H.CssClass = "Initial";
            Tab2H.CssClass = "Clicked";
            Tab3H.CssClass = "Initial";
            Tab4H.CssClass = "Initial";
            Tab5H.CssClass = "Initial";
            MultiViewH.ActiveViewIndex = 1;
            LoadSec80C();
            GridControls();
            HFTabID.Value = "2";
            viewcheckSec80C.Value = "NO";
        }

        private void LoadSec80C()
        {
            try
            {

                int flag = 2;
                ITbl ITblObj = new ITbl();
                List<ITbo> ITboObj = new List<ITbo>();
                List<ITbo> ITboObj1 = new List<ITbo>();
                string ApproverId = User.Identity.Name;
                ITboObj1 = ITblObj.Load_IT_HeaderHistory(User.Identity.Name, flag);
                Session.Add("ITSec80CGrdInfo", ITboObj1);

                if (ITboObj1 == null || ITboObj1.Count == 0)
                {
                    MsgCls("No Records Found !", LblSec80c, Color.Red);
                    GVSec80CHeader.Visible = false;
                    GVSec80CHeader.DataSource = null;
                    GVSec80CHeader.DataBind();
                    ExportbtnSec80C.Visible = false;
                    return;
                }
                else
                {
                    GVSec80CHeader.Visible = true;
                    GVSec80CHeader.DataSource = ITboObj1;
                    GVSec80CHeader.SelectedIndex = -1;
                    GVSec80CHeader.DataBind();
                    ExportbtnSec80C.Visible = true;
                }
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        }

        protected void Tab3H_Click(object sender, EventArgs e)
        {
            ddlSeachSelect.SelectedValue = "0";
            txtsearch.Text = string.Empty;
            txtFromDateH.Text = string.Empty;
            txtTodateH.Text = string.Empty;
            MsgCls("", lblMessageBoard, Color.Transparent);
            MsgCls("", LblSec80c, Color.Transparent);
            MsgCls("", LblHousing, Color.Transparent);
            MsgCls("", LblOthers, Color.Transparent);
            Tab1H.CssClass = "Initial";
            Tab2H.CssClass = "Initial";
            Tab3H.CssClass = "Clicked";
            Tab4H.CssClass = "Initial";
            Tab5H.CssClass = "Initial";
            MultiViewH.ActiveViewIndex = 2;
            LoadHousing();
            GridControls();
            HFTabID.Value = "3";
            viewcheckHousing.Value = "NO";
        }

        private void LoadHousing()
        {
            try
            {

                int flag = 3;
                ITbl ITblObj = new ITbl();
                List<ITbo> ITboObj = new List<ITbo>();
                List<ITbo> ITboObj1 = new List<ITbo>();
                string ApproverId = User.Identity.Name;
                ITboObj1 = ITblObj.Load_IT_HeaderHistory(User.Identity.Name, flag);
                Session.Add("ITHousingGrdInfo", ITboObj1);

                if (ITboObj1 == null || ITboObj1.Count == 0)
                {
                    MsgCls("No Records Found !", LblHousing, Color.Red);
                    GVHousingHeader.Visible = false;
                    GVHousingHeader.DataSource = null;
                    GVHousingHeader.DataBind();
                    ExportbtnHousing.Visible = false;
                    return;
                }
                else
                {
                    GVHousingHeader.Visible = true;
                    GVHousingHeader.DataSource = ITboObj1;
                    GVHousingHeader.SelectedIndex = -1;
                    GVHousingHeader.DataBind();
                    ExportbtnHousing.Visible = true;
                }
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        }

        protected void Tab4H_Click(object sender, EventArgs e)
        {
            ddlSeachSelect.SelectedValue = "0";
            txtsearch.Text = string.Empty;
            txtFromDateH.Text = string.Empty;
            txtTodateH.Text = string.Empty;
            MsgCls("", lblMessageBoard, Color.Transparent);
            MsgCls("", LblSec80c, Color.Transparent);
            MsgCls("", LblHousing, Color.Transparent);
            MsgCls("", LblOthers, Color.Transparent);
            Tab1H.CssClass = "Initial";
            Tab2H.CssClass = "Initial";
            Tab3H.CssClass = "Initial";
            Tab5H.CssClass = "Initial";
            Tab4H.CssClass = "Clicked";
            MultiViewH.ActiveViewIndex = 3;
            LoadOthers();
            GridControls();
            HFTabID.Value = "4";
            viewcheckOthers.Value = "NO";
        }

        private void LoadOthers()
        {
            try
            {

                int flag = 4;
                ITbl ITblObj = new ITbl();
                List<ITbo> ITboObj = new List<ITbo>();
                List<ITbo> ITboObj1 = new List<ITbo>();
                string ApproverId = User.Identity.Name;
                ITboObj1 = ITblObj.Load_IT_HeaderHistory(User.Identity.Name, flag);
                Session.Add("ITOthersGrdInfo", ITboObj1);

                if (ITboObj1 == null || ITboObj1.Count == 0)
                {
                    MsgCls("No Records Found !", LblOthers, Color.Red);
                    GVOthersHeader.Visible = false;
                    GVOthersHeader.DataSource = null;
                    GVOthersHeader.DataBind();
                    ExportbtnOthers.Visible = false;
                    return;
                }
                else
                {
                    GVOthersHeader.Visible = true;
                    GVOthersHeader.DataSource = ITboObj1;
                    GVOthersHeader.SelectedIndex = -1;
                    GVOthersHeader.DataBind();
                    ExportbtnOthers.Visible = true;
                }
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        }

        protected void GVSec80Header_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName.ToUpper())
                {
                    case "VIEW":

                        viewcheckSec80.Value = "YES";
                        int rowIndex = Convert.ToInt32(e.CommandArgument);

                        foreach (GridViewRow row in GVSec80Header.Rows)
                        {
                            row.BackColor = row.RowIndex.Equals(rowIndex) ?
                            System.Drawing.Color.LightGray :
                            System.Drawing.Color.White;
                        }

                        GVITSec80H.Visible = true;
                        int ID = int.Parse(GVSec80Header.DataKeys[int.Parse(e.CommandArgument.ToString())]["ID"].ToString());

                        //ID,CREATED_BY,ENAME,BEGDA,ENDDA,CREATED_ON,CONACTPROP,APPROVEDON,REMARKS,STATUS

                        ViewState["ITSEC80ID"] = int.Parse(GVSec80Header.DataKeys[int.Parse(e.CommandArgument.ToString())]["ID"].ToString());
                        ViewState["ITSEC80TYP"] = "Section 80";
                        ViewState["BEDGASEC80"] = GVSec80Header.DataKeys[int.Parse(e.CommandArgument.ToString())]["BEGDA"].ToString().Trim();
                        ViewState["ENDDASEC80"] = GVSec80Header.DataKeys[int.Parse(e.CommandArgument.ToString())]["ENDDA"].ToString().Trim();
                        ViewState["SEC80CA"] = GVSec80Header.DataKeys[int.Parse(e.CommandArgument.ToString())]["CONACTPROP"].ToString().Trim();
                        //ViewState["SEC80CREATEDBY"] = GVSec80Header.DataKeys[int.Parse(e.CommandArgument.ToString())]["CREATED_BY"].ToString().Trim();
                        //ViewState["SEC80ENAME"] = GVSec80Header.DataKeys[int.Parse(e.CommandArgument.ToString())]["ENAME"].ToString().Trim();
                        ViewState["SEC80CREATED_ON"] = GVSec80Header.DataKeys[int.Parse(e.CommandArgument.ToString())]["CREATED_ON"].ToString().Trim();
                        ViewState["SEC80APPROVEDON"] = GVSec80Header.DataKeys[int.Parse(e.CommandArgument.ToString())]["APPROVEDON"].ToString().Trim();
                        ViewState["SEC80REMARKS"] = GVSec80Header.DataKeys[int.Parse(e.CommandArgument.ToString())]["REMARKS"].ToString().Trim();
                        ViewState["SEC80STS"] = GVSec80Header.DataKeys[int.Parse(e.CommandArgument.ToString())]["STATUS"].ToString().Trim();

                        ITbl ITblObj = new ITbl();
                        List<ITbo> ITboObj = new List<ITbo>();
                        ITboObj = ITblObj.Load_Sec80Details(ID, User.Identity.Name);
                        GVITSec80H.DataSource = ITboObj;
                        GVITSec80H.DataBind();

                        break;
                    default:
                        break;
                }


            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        }

        protected void GVSec80CHeader_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName.ToUpper())
                {
                    case "VIEW":

                        viewcheckSec80C.Value = "YES";
                        int rowIndex = Convert.ToInt32(e.CommandArgument);

                        foreach (GridViewRow row in GVSec80CHeader.Rows)
                        {
                            row.BackColor = row.RowIndex.Equals(rowIndex) ?
                            System.Drawing.Color.LightGray :
                            System.Drawing.Color.White;
                        }

                        GVITSec80CH.Visible = true;
                        int ID = int.Parse(GVSec80CHeader.DataKeys[int.Parse(e.CommandArgument.ToString())]["ID"].ToString());

                        ViewState["ITSEC80CID"] = int.Parse(GVSec80CHeader.DataKeys[int.Parse(e.CommandArgument.ToString())]["ID"].ToString());
                        ViewState["ITSEC80CTYP"] = "Section 80 C";
                        ViewState["BEDGASEC80C"] = GVSec80CHeader.DataKeys[int.Parse(e.CommandArgument.ToString())]["BEGDA"].ToString().Trim();
                        ViewState["ENDDASEC80C"] = GVSec80CHeader.DataKeys[int.Parse(e.CommandArgument.ToString())]["ENDDA"].ToString().Trim();
                        ViewState["SEC80CCA"] = GVSec80CHeader.DataKeys[int.Parse(e.CommandArgument.ToString())]["CONACTPROP"].ToString().Trim();
                        //ViewState["SEC80CCREATEDBY"] = GVSec80Header.DataKeys[int.Parse(e.CommandArgument.ToString())]["CREATED_BY"].ToString().Trim();
                        //ViewState["SEC80CENAME"] = GVSec80Header.DataKeys[int.Parse(e.CommandArgument.ToString())]["ENAME"].ToString().Trim();
                        ViewState["SEC80CCREATED_ON"] = GVSec80CHeader.DataKeys[int.Parse(e.CommandArgument.ToString())]["CREATED_ON"].ToString().Trim();
                        ViewState["SEC80CAPPROVEDON"] = GVSec80CHeader.DataKeys[int.Parse(e.CommandArgument.ToString())]["APPROVEDON"].ToString().Trim();
                        ViewState["SEC80CREMARKS"] = GVSec80CHeader.DataKeys[int.Parse(e.CommandArgument.ToString())]["REMARKS"].ToString().Trim();
                        ViewState["SEC80CSTS"] = GVSec80CHeader.DataKeys[int.Parse(e.CommandArgument.ToString())]["STATUS"].ToString().Trim();


                        ITbl ITblObj = new ITbl();
                        List<ITbo> ITboObj = new List<ITbo>();
                        ITboObj = ITblObj.Load_Sec80CDetails(ID, User.Identity.Name);
                        GVITSec80CH.DataSource = ITboObj;
                        GVITSec80CH.DataBind();

                        break;
                    default:
                        break;
                }


            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        }

        protected void GVHousingHeader_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName.ToUpper())
                {
                    case "VIEW":

                        viewcheckHousing.Value = "YES";
                        int rowIndex = Convert.ToInt32(e.CommandArgument);

                        foreach (GridViewRow row in GVHousingHeader.Rows)
                        {
                            row.BackColor = row.RowIndex.Equals(rowIndex) ?
                            System.Drawing.Color.LightGray :
                            System.Drawing.Color.White;
                        }

                        GVHousing.Visible = true;
                        int ID = int.Parse(GVHousingHeader.DataKeys[int.Parse(e.CommandArgument.ToString())]["ID"].ToString());

                        ViewState["ITHOUSINGID"] = int.Parse(GVHousingHeader.DataKeys[int.Parse(e.CommandArgument.ToString())]["ID"].ToString());
                        ViewState["ITHOUSINGTYP"] = "Housing";
                        ViewState["BEDGAHOUSING"] = GVHousingHeader.DataKeys[int.Parse(e.CommandArgument.ToString())]["BEGDA"].ToString().Trim();
                        ViewState["ENDDAHOUSING"] = GVHousingHeader.DataKeys[int.Parse(e.CommandArgument.ToString())]["ENDDA"].ToString().Trim();
                        //ViewState["HOUSINGCREATEDBY"] = GVSec80Header.DataKeys[int.Parse(e.CommandArgument.ToString())]["CREATED_BY"].ToString().Trim();
                        //ViewState["HOUSINGENAME"] = GVSec80Header.DataKeys[int.Parse(e.CommandArgument.ToString())]["ENAME"].ToString().Trim();
                        ViewState["HOUSINGCREATED_ON"] = GVHousingHeader.DataKeys[int.Parse(e.CommandArgument.ToString())]["CREATED_ON"].ToString().Trim();
                        ViewState["HOUSINGAPPROVEDON"] = GVHousingHeader.DataKeys[int.Parse(e.CommandArgument.ToString())]["APPROVEDON"].ToString().Trim();
                        ViewState["HOUSINGREMARKS"] = GVHousingHeader.DataKeys[int.Parse(e.CommandArgument.ToString())]["REMARKS"].ToString().Trim();
                        ViewState["HOUSINGSTS"] = GVHousingHeader.DataKeys[int.Parse(e.CommandArgument.ToString())]["STATUS"].ToString().Trim();


                        ITbl ITblObj = new ITbl();
                        List<ITbo> ITboObj = new List<ITbo>();
                        ITboObj = ITblObj.Load_HousingDetails(ID, User.Identity.Name);
                        GVHousing.DataSource = ITboObj;
                        GVHousing.DataBind();

                        break;
                    default:
                        break;
                }


            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        }

        protected void GVOthersHeader_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            try
            {
                switch (e.CommandName.ToUpper())
                {
                    case "VIEW":

                        viewcheckOthers.Value = "YES";

                        int rowIndex = Convert.ToInt32(e.CommandArgument);

                        foreach (GridViewRow row in GVOthersHeader.Rows)
                        {
                            row.BackColor = row.RowIndex.Equals(rowIndex) ?
                            System.Drawing.Color.LightGray :
                            System.Drawing.Color.White;
                        }



                        int ID = int.Parse(GVOthersHeader.DataKeys[int.Parse(e.CommandArgument.ToString())]["ID"].ToString());
                        string ITtyp = GVOthersHeader.DataKeys[int.Parse(e.CommandArgument.ToString())]["ITTYP"].ToString();
                        int nxtid = GVOthersHeader.DataKeys[int.Parse(e.CommandArgument.ToString())]["CONACTPROP"].ToString() == "-" ? 0 : int.Parse(GVOthersHeader.DataKeys[int.Parse(e.CommandArgument.ToString())]["CONACTPROP"].ToString());
                        ViewState["ITOTHERSID"] = int.Parse(GVOthersHeader.DataKeys[int.Parse(e.CommandArgument.ToString())]["ID"].ToString());
                        ViewState["ITOTHERSTYP"] = ITtyp.ToString();
                        ViewState["BEDGAOTHERS"] = GVOthersHeader.DataKeys[int.Parse(e.CommandArgument.ToString())]["BEGDA"].ToString().Trim();
                        ViewState["ENDDAOTHERS"] = GVOthersHeader.DataKeys[int.Parse(e.CommandArgument.ToString())]["ENDDA"].ToString().Trim();
                        //ViewState["OTHERSCREATEDBY"] = GVSec80Header.DataKeys[int.Parse(e.CommandArgument.ToString())]["CREATED_BY"].ToString().Trim();
                        //ViewState["OTHERSENAME"] = GVSec80Header.DataKeys[int.Parse(e.CommandArgument.ToString())]["ENAME"].ToString().Trim();
                        ViewState["OTHERSCREATED_ON"] = GVOthersHeader.DataKeys[int.Parse(e.CommandArgument.ToString())]["CREATED_ON"].ToString().Trim();
                        ViewState["OTHERSAPPROVEDON"] = GVOthersHeader.DataKeys[int.Parse(e.CommandArgument.ToString())]["APPROVEDON"].ToString().Trim();
                        ViewState["OTHERSREMARKS"] = GVOthersHeader.DataKeys[int.Parse(e.CommandArgument.ToString())]["REMARKS"].ToString().Trim();
                        ViewState["OTHERSSTS"] = GVOthersHeader.DataKeys[int.Parse(e.CommandArgument.ToString())]["STATUS"].ToString().Trim();


                        ITbl ITblObj = new ITbl();
                        List<ITbo> ITboObj = new List<ITbo>();

                        if (ITtyp.Trim() == "1")
                        {

                            ITboObj = ITblObj.Load_HousingOthersDetails(ID, ITtyp, User.Identity.Name, "");
                            GVOthers1.DataSource = ITboObj;
                            GVOthers1.DataBind();
                            ViewState["TypeRENTO"] = ITboObj[0].RENTO;
                            ITboObj = ITblObj.Load_HousingOthersDetails(nxtid == 0 ? ID : nxtid, ITtyp, User.Identity.Name, "");

                            grdHistySelf.DataSource = ITboObj;
                            grdHistySelf.DataBind();
                            ITboObj = ITblObj.Load_PreEpInCm_cont(nxtid == 0 ? ID : nxtid, 2);

                            DataTable dt = dtBorw();
                            for (int i = 0; i < ITboObj.Count; i++)
                            {
                                dt.Rows.Add(ITboObj[i].RID, ITboObj[i].NAME, ITboObj[i].PERCNT);
                            }
                            grdHisBorw.DataSource = dt;
                            grdHisBorw.DataBind();
                            grdHisBorw.Visible = true;
                            grdHistySelf.Visible = true;

                            GVOthers1.Visible = ViewState["TypeRENTO"].ToString() == "3" ? true : false; ;

                            GVOthers2.Visible = false;
                        }
                        else if (ITtyp.Trim() == "2")
                        {
                            grdHistySelf.Visible = false;
                            grdHisBorw.Visible = false;
                            GVOthers2.Visible = true;
                            GVOthers1.Visible = false;
                            ITboObj = ITblObj.Load_HousingOthersDetails(ID, ITtyp, User.Identity.Name, "");
                            GVOthers2.DataSource = ITboObj;
                            GVOthers2.DataBind();
                        }


                        break;
                    default:
                        break;
                }


            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        }

        protected void btnsearch_Click(object sender, EventArgs e)
        {
            try
            {
                search();
                GridControls();
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        }

        public void search()
        {
            try
            {
                MsgCls(string.Empty, lblMessageBoard, Color.Transparent);
                string SelectedType = ddlSeachSelect.SelectedValue.ToString();
                string textSearch = txtsearch.Text;
                DateTime FromDate = DateTime.Parse(string.IsNullOrEmpty(txtFromDateH.Text) ? "31/12/1899" : txtFromDateH.Text);
                DateTime ToDate = DateTime.Parse(string.IsNullOrEmpty(txtTodateH.Text) ? "31/12/1899" : txtTodateH.Text);
                // 01/01/0001 1899-12-31
                //if (SelectedType != "0" && textSearch == "")
                //{
                //    MsgCls("Please Enter the Text", lblMessageBoard, Color.Red);
                //}

                //else if (SelectedType == "0" && textSearch != "")
                //{
                //    MsgCls("Please Select the Type", lblMessageBoard, Color.Red);
                //}

                if (SelectedType != "0" && textSearch == "")
                {
                    if (int.Parse(HFTabID.Value.ToString().Trim()) == 1)
                    {
                        MsgCls("Please Enter the Text", lblMessageBoard, Color.Red);
                    }
                    else if (int.Parse(HFTabID.Value.ToString().Trim()) == 2)
                    {
                        MsgCls("Please Enter the Text", LblSec80c, Color.Red);
                    }
                    else if (int.Parse(HFTabID.Value.ToString().Trim()) == 3)
                    {
                        MsgCls("Please Enter the Text", LblHousing, Color.Red);
                    }
                    else if (int.Parse(HFTabID.Value.ToString().Trim()) == 4)
                    {
                        MsgCls("Please Enter the Text", LblOthers, Color.Red);
                    }
                }

                else if (SelectedType == "0" && textSearch != "")
                {
                    if (int.Parse(HFTabID.Value.ToString().Trim()) == 1)
                    {
                        MsgCls("Please Select the Type", lblMessageBoard, Color.Red);
                    }
                    else if (int.Parse(HFTabID.Value.ToString().Trim()) == 2)
                    {
                        MsgCls("Please Select the Type", LblSec80c, Color.Red);
                    }
                    else if (int.Parse(HFTabID.Value.ToString().Trim()) == 3)
                    {
                        MsgCls("Please Select the Type", LblHousing, Color.Red);
                    }
                    else if (int.Parse(HFTabID.Value.ToString().Trim()) == 4)
                    {
                        MsgCls("Please Select the Type", LblOthers, Color.Red);
                    }

                }

                else
                {
                    ITbl ITblObj = new ITbl();
                    List<ITbo> ITboObj = new List<ITbo>();
                    List<ITbo> ITboObj1 = new List<ITbo>();
                    ITboObj1 = ITblObj.Load_ParticularITEmp(SelectedType, textSearch, FromDate, ToDate, int.Parse(HFTabID.Value.ToString().Trim()), User.Identity.Name);

                    if (int.Parse(HFTabID.Value.ToString().Trim()) == 1)
                    {

                        if (ITboObj1 == null || ITboObj1.Count == 0)
                        {
                            MsgCls("No Records found", lblMessageBoard, Color.Red);
                            //GVSec80Header.Visible = false;
                            GVSec80Header.DataSource = null;
                            GVSec80Header.DataBind();
                            ExportbtnSec80.Visible = false;
                            return;
                        }
                        else
                        {
                            MsgCls("", lblMessageBoard, Color.Transparent);
                            GVSec80Header.Visible = true;
                            GVSec80Header.DataSource = ITboObj1;
                            GVSec80Header.SelectedIndex = -1;
                            GVSec80Header.DataBind();
                            ExportbtnSec80.Visible = true;
                        }
                        viewcheckSec80.Value = "NO";
                    }

                    else if (int.Parse(HFTabID.Value.ToString().Trim()) == 2)
                    {

                        if (ITboObj1 == null || ITboObj1.Count == 0)
                        {
                            MsgCls("No Records found", LblSec80c, Color.Red);
                            //GVSec80Header.Visible = false;
                            GVSec80CHeader.DataSource = null;
                            GVSec80CHeader.DataBind();
                            ExportbtnSec80C.Visible = false;
                            return;
                        }
                        else
                        {
                            MsgCls("", LblSec80c, Color.Transparent);
                            GVSec80CHeader.Visible = true;
                            GVSec80CHeader.DataSource = ITboObj1;
                            GVSec80CHeader.SelectedIndex = -1;
                            GVSec80CHeader.DataBind();
                            ExportbtnSec80.Visible = true;

                        }

                        viewcheckSec80C.Value = "NO";
                    }


                    else if (int.Parse(HFTabID.Value.ToString().Trim()) == 3)
                    {

                        if (ITboObj1 == null || ITboObj1.Count == 0)
                        {
                            MsgCls("No Records found", LblHousing, Color.Red);
                            //GVSec80Header.Visible = false;
                            GVHousingHeader.DataSource = null;
                            GVHousingHeader.DataBind();
                            ExportbtnHousing.Visible = false;
                            return;
                        }
                        else
                        {
                            MsgCls("", LblHousing, Color.Transparent);
                            GVHousingHeader.Visible = true;
                            GVHousingHeader.DataSource = ITboObj1;
                            GVHousingHeader.SelectedIndex = -1;
                            GVHousingHeader.DataBind();
                            ExportbtnHousing.Visible = true;

                        }
                        viewcheckHousing.Value = "NO";
                    }

                    else if (int.Parse(HFTabID.Value.ToString().Trim()) == 4)
                    {

                        if (ITboObj1 == null || ITboObj1.Count == 0)
                        {
                            MsgCls("No Records found", LblOthers, Color.Red);
                            //GVSec80Header.Visible = false;
                            GVOthersHeader.DataSource = null;
                            GVOthersHeader.DataBind();
                            ExportbtnOthers.Visible = false;
                            return;
                        }
                        else
                        {
                            MsgCls("", LblOthers, Color.Transparent);
                            GVOthersHeader.Visible = true;
                            GVOthersHeader.DataSource = ITboObj1;
                            GVOthersHeader.SelectedIndex = -1;
                            GVOthersHeader.DataBind();
                            ExportbtnOthers.Visible = true;

                        }

                        viewcheckOthers.Value = "NO";
                    }

                    else if (int.Parse(HFTabID.Value.ToString().Trim()) == 5)
                    {

                        if (ITboObj1 == null || ITboObj1.Count == 0)
                        {
                            MsgCls("No Records found", lblPreE, Color.Red);
                            //GVSec80Header.Visible = false;
                            grdPreEmptIncHead.DataSource = null;
                            grdPreEmptIncHead.DataBind();
                            divPreEmptIncExprt.Visible = false;
                            grdPreEmptInc.Visible = false;
                            return;
                        }
                        else
                        {
                            MsgCls("", lblPreE, Color.Transparent);
                            grdPreEmptIncHead.Visible = true;
                            grdPreEmptIncHead.DataSource = ITboObj1;
                            grdPreEmptIncHead.SelectedIndex = -1;
                            grdPreEmptIncHead.DataBind();
                            divPreEmptIncExprt.Visible = true;
                            grdPreEmptInc.Visible = false;
                        }

                        viewcheckOthers.Value = "NO";
                    }

                }
            }
            catch (Exception Ex)
            {
                // ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true);
                //MsgCls(Ex.Message, lblMessageBoard, Color.Red);


                if (int.Parse(HFTabID.Value.ToString().Trim()) == 1)
                {
                    MsgCls(Ex.Message, lblMessageBoard, Color.Red);
                }
                else if (int.Parse(HFTabID.Value.ToString().Trim()) == 2)
                {
                    MsgCls(Ex.Message, LblSec80c, Color.Red);
                }
                else if (int.Parse(HFTabID.Value.ToString().Trim()) == 3)
                {
                    MsgCls(Ex.Message, LblHousing, Color.Red);
                }
                else if (int.Parse(HFTabID.Value.ToString().Trim()) == 4)
                {
                    MsgCls(Ex.Message, LblOthers, Color.Red);
                }
            }

        }

        protected void btnclear_Click(object sender, EventArgs e)
        {
            try
            {
                ddlSeachSelect.SelectedValue = "0";
                txtsearch.Text = string.Empty;
                txtFromDateH.Text = string.Empty;
                txtTodateH.Text = string.Empty;
                MsgCls("", lblMessageBoard, Color.Transparent);
                MsgCls("", LblSec80c, Color.Transparent);
                MsgCls("", LblHousing, Color.Transparent);
                MsgCls("", LblOthers, Color.Transparent);
                txtsearch.Focus();

                if (int.Parse(HFTabID.Value.ToString().Trim()) == 1)
                {
                    Tab1H.CssClass = "Clicked";
                    Tab2H.CssClass = "Initial";
                    Tab3H.CssClass = "Initial";
                    Tab4H.CssClass = "Initial";
                    MultiViewH.ActiveViewIndex = 0;
                    LoadSec80();
                    GridControls();
                    HFTabID.Value = "1";
                    viewcheckSec80.Value = "NO";
                    ExportbtnSec80.Visible = true;
                }

                else if (int.Parse(HFTabID.Value.ToString().Trim()) == 2)
                {
                    Tab1H.CssClass = "Initial";
                    Tab2H.CssClass = "Clicked";
                    Tab3H.CssClass = "Initial";
                    Tab4H.CssClass = "Initial";
                    MultiViewH.ActiveViewIndex = 1;
                    LoadSec80C();
                    GridControls();
                    HFTabID.Value = "2";
                    viewcheckSec80C.Value = "NO";
                    ExportbtnSec80C.Visible = true;
                }
                else if (int.Parse(HFTabID.Value.ToString().Trim()) == 3)
                {
                    Tab1H.CssClass = "Initial";
                    Tab2H.CssClass = "Initial";
                    Tab3H.CssClass = "Clicked";
                    Tab4H.CssClass = "Initial";
                    MainView.ActiveViewIndex = 2;
                    LoadHousing();
                    GridControls();
                    HFTabID.Value = "3";
                    viewcheckHousing.Value = "NO";
                    ExportbtnHousing.Visible = true;
                }
                else if (int.Parse(HFTabID.Value.ToString().Trim()) == 4)
                {
                    Tab1H.CssClass = "Initial";
                    Tab2H.CssClass = "Initial";
                    Tab3H.CssClass = "Initial";
                    Tab4H.CssClass = "Clicked";
                    MultiViewH.ActiveViewIndex = 3;
                    LoadOthers();
                    GridControls();
                    HFTabID.Value = "4";
                    viewcheckOthers.Value = "NO";
                    ExportbtnOthers.Visible = true;
                }
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        }

        protected void GVSec80Header_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                int pageindex = e.NewPageIndex;
                GVSec80Header.PageIndex = e.NewPageIndex;
                LoadSec80();
                search();
                GVSec80Header.SelectedIndex = -1;
                ddlSeachSelect.SelectedValue = "0";
                txtsearch.Text = string.Empty;
                txtFromDateH.Text = string.Empty;
                txtTodateH.Text = string.Empty;
                MsgCls("", lblMessageBoard, Color.Transparent);
                MsgCls("", LblSec80c, Color.Transparent);
                MsgCls("", LblHousing, Color.Transparent);
                MsgCls("", LblOthers, Color.Transparent);
                Tab1H.CssClass = "Clicked";
                Tab2H.CssClass = "Initial";
                Tab3H.CssClass = "Initial";
                Tab4H.CssClass = "Initial";
                MultiViewH.ActiveViewIndex = 0;
                GridControls();
                HFTabID.Value = "1";
                viewcheckSec80.Value = "NO";
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        }
        protected void GVSec80CHeader_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                int pageindex = e.NewPageIndex;
                GVSec80CHeader.PageIndex = e.NewPageIndex;
                LoadSec80C();
                search();
                GVSec80Header.SelectedIndex = -1;

                ddlSeachSelect.SelectedValue = "0";
                txtsearch.Text = string.Empty;
                txtFromDateH.Text = string.Empty;
                txtTodateH.Text = string.Empty;
                MsgCls("", lblMessageBoard, Color.Transparent);
                MsgCls("", LblSec80c, Color.Transparent);
                MsgCls("", LblHousing, Color.Transparent);
                MsgCls("", LblOthers, Color.Transparent);
                Tab1H.CssClass = "Initial";
                Tab2H.CssClass = "Clicked";
                Tab3H.CssClass = "Initial";
                Tab4H.CssClass = "Initial";
                MultiViewH.ActiveViewIndex = 1;
                GridControls();
                HFTabID.Value = "2";
                viewcheckSec80C.Value = "NO";
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        }
        protected void GVHousingHeader_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                int pageindex = e.NewPageIndex;
                GVHousingHeader.PageIndex = e.NewPageIndex;
                LoadHousing();
                search();
                GVSec80Header.SelectedIndex = -1;
                ddlSeachSelect.SelectedValue = "0";
                txtsearch.Text = string.Empty;
                txtFromDateH.Text = string.Empty;
                txtTodateH.Text = string.Empty;
                MsgCls("", lblMessageBoard, Color.Transparent);
                MsgCls("", LblSec80c, Color.Transparent);
                MsgCls("", LblHousing, Color.Transparent);
                MsgCls("", LblOthers, Color.Transparent);
                Tab1H.CssClass = "Initial";
                Tab2H.CssClass = "Initial";
                Tab3H.CssClass = "Clicked";
                Tab4H.CssClass = "Initial";
                MultiViewH.ActiveViewIndex = 2;
                GridControls();
                HFTabID.Value = "3";
                viewcheckHousing.Value = "NO";
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        }
        protected void GVOthersHeader_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                int pageindex = e.NewPageIndex;
                GVOthersHeader.PageIndex = e.NewPageIndex;
                LoadOthers();
                search();
                GVSec80Header.SelectedIndex = -1;

                ddlSeachSelect.SelectedValue = "0";
                txtsearch.Text = string.Empty;
                txtFromDateH.Text = string.Empty;
                txtTodateH.Text = string.Empty;
                MsgCls("", lblMessageBoard, Color.Transparent);
                MsgCls("", LblSec80c, Color.Transparent);
                MsgCls("", LblHousing, Color.Transparent);
                MsgCls("", LblOthers, Color.Transparent);
                Tab1H.CssClass = "Initial";
                Tab2H.CssClass = "Initial";
                Tab3H.CssClass = "Initial";
                Tab4H.CssClass = "Clicked";
                MultiViewH.ActiveViewIndex = 3;
                GridControls();
                HFTabID.Value = "4";
                viewcheckOthers.Value = "NO";
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        }

        protected void GVSec80Header_Sorting(object sender, GridViewSortEventArgs e)
        {
            try
            {
                HFTabID.Value = "1";
                LoadSec80();
                search();
                viewcheckSec80.Value = "NO";
                List<ITbo> ITboList = (List<ITbo>)Session["ITSec80GrdInfo"];
                bool objSortOrder = (bool)Session["bSortedOrder"];
                switch (e.SortExpression.ToString().Trim())
                {
                    case "CREATED_BY":
                        if (objSortOrder)
                        {
                            if (ITboList != null)
                            {
                                ITboList.Sort(delegate(ITbo objBo1, ITbo objBo2)
                                { return ((int.Parse(objBo1.CREATED_BY)).CompareTo(int.Parse(objBo2.CREATED_BY))); });
                                objSortOrder = false;
                                Session.Add("bSortedOrder", objSortOrder);
                            }
                        }
                        else
                        {
                            ITboList.Sort(delegate(ITbo objBo1, ITbo objBo2)
                            { return ((int.Parse(objBo2.CREATED_BY)).CompareTo(int.Parse(objBo1.CREATED_BY))); });
                            objSortOrder = true;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                        break;

                    case "ENAME":
                        if (objSortOrder)
                        {
                            if (ITboList != null)
                            {
                                ITboList.Sort(delegate(ITbo objBo1, ITbo objBo2)
                                { return (objBo1.ENAME.ToString().CompareTo(objBo2.ENAME.ToString())); });
                                objSortOrder = false;
                                Session.Add("bSortedOrder", objSortOrder);
                            }
                        }
                        else
                        {
                            ITboList.Sort(delegate(ITbo objBo1, ITbo objBo2)
                            { return (objBo2.ENAME.ToString().CompareTo(objBo1.ENAME.ToString())); });
                            objSortOrder = true;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                        break;
                    case "ID":
                        if (objSortOrder)
                        {
                            if (ITboList != null)
                            {
                                ITboList.Sort(delegate(ITbo objBo1, ITbo objBo2)
                                { return (objBo1.ID.Value.CompareTo(objBo2.ID.Value)); });
                                objSortOrder = false;
                                Session.Add("bSortedOrder", objSortOrder);
                            }
                        }
                        else
                        {
                            ITboList.Sort(delegate(ITbo objBo1, ITbo objBo2)
                            { return (objBo2.ID.Value.CompareTo(objBo1.ID.Value)); });
                            objSortOrder = true;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                        break;



                    case "ITTYP":
                        if (objSortOrder)
                        {
                            if (ITboList != null)
                            {
                                ITboList.Sort(delegate(ITbo objBo1, ITbo objBo2)
                                { return (int.Parse(objBo1.ITTYP.ToString()).CompareTo(int.Parse(objBo2.ITTYP.ToString()))); });
                                objSortOrder = false;
                                Session.Add("bSortedOrder", objSortOrder);
                            }
                        }
                        else
                        {
                            ITboList.Sort(delegate(ITbo objBo1, ITbo objBo2)
                            { return (int.Parse(objBo2.ITTYP.ToString()).CompareTo(int.Parse(objBo1.ITTYP.ToString()))); });
                            objSortOrder = true;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                        break;

                    case "STATUS":
                        if (objSortOrder)
                        {
                            if (ITboList != null)
                            {
                                ITboList.Sort(delegate(ITbo objBo1, ITbo objBo2)
                                { return (objBo1.STATUS.ToString().CompareTo(objBo2.STATUS.ToString())); });
                                objSortOrder = false;
                                Session.Add("bSortedOrder", objSortOrder);
                            }
                        }
                        else
                        {
                            ITboList.Sort(delegate(ITbo objBo1, ITbo objBo2)
                            { return (objBo2.STATUS.ToString().CompareTo(objBo1.STATUS.ToString())); });
                            objSortOrder = true;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                        break;


                    case "CREATED_ON":
                        if (objSortOrder)
                        {
                            if (ITboList != null)
                            {
                                ITboList.Sort(delegate(ITbo objBo1, ITbo objBo2)
                                { return (objBo1.CREATED_ON.Value.CompareTo(objBo2.CREATED_ON.Value)); });
                                objSortOrder = false;
                                Session.Add("bSortedOrder", objSortOrder);
                            }
                        }
                        else
                        {
                            ITboList.Sort(delegate(ITbo objBo1, ITbo objBo2)
                            { return (objBo2.CREATED_ON.Value.CompareTo(objBo1.CREATED_ON.Value)); });
                            objSortOrder = true;
                            Session.Add("bSortedOrder", objSortOrder);
                        }

                        break;

                    case "BEGDA":
                        if (objSortOrder)
                        {
                            if (ITboList != null)
                            {
                                ITboList.Sort(delegate(ITbo objBo1, ITbo objBo2)
                                { return (objBo1.BEGDA.Value.CompareTo(objBo2.BEGDA.Value)); });
                                objSortOrder = false;
                                Session.Add("bSortedOrder", objSortOrder);
                            }
                        }
                        else
                        {
                            ITboList.Sort(delegate(ITbo objBo1, ITbo objBo2)
                            { return (objBo2.BEGDA.Value.CompareTo(objBo1.BEGDA.Value)); });
                            objSortOrder = true;
                            Session.Add("bSortedOrder", objSortOrder);
                        }

                        break;

                    case "ENDDA":
                        if (objSortOrder)
                        {
                            if (ITboList != null)
                            {
                                ITboList.Sort(delegate(ITbo objBo1, ITbo objBo2)
                                { return (objBo1.ENDDA.Value.CompareTo(objBo2.ENDDA.Value)); });
                                objSortOrder = false;
                                Session.Add("bSortedOrder", objSortOrder);
                            }
                        }
                        else
                        {
                            ITboList.Sort(delegate(ITbo objBo1, ITbo objBo2)
                            { return (objBo2.ENDDA.Value.CompareTo(objBo1.ENDDA.Value)); });
                            objSortOrder = true;
                            Session.Add("bSortedOrder", objSortOrder);
                        }

                        break;


                    case "APPROVEDON":
                        if (objSortOrder)
                        {
                            if (ITboList != null)
                            {
                                ITboList.Sort(delegate(ITbo objBo1, ITbo objBo2)
                                { return (objBo1.APPROVEDON.Value.CompareTo(objBo2.APPROVEDON.Value)); });
                                objSortOrder = false;
                                Session.Add("bSortedOrder", objSortOrder);
                            }
                        }
                        else
                        {
                            ITboList.Sort(delegate(ITbo objBo1, ITbo objBo2)
                            { return (objBo2.APPROVEDON.Value.CompareTo(objBo1.APPROVEDON.Value)); });
                            objSortOrder = true;
                            Session.Add("bSortedOrder", objSortOrder);
                        }

                        break;

                }

                GVSec80Header.DataSource = ITboList;
                GVSec80Header.DataBind();

                Session.Add("ITSec80GrdInfo", ITboList);
                ddlSeachSelect.SelectedValue = "0";
                txtsearch.Text = string.Empty;
                txtFromDateH.Text = string.Empty;
                txtTodateH.Text = string.Empty;
                MsgCls("", lblMessageBoard, Color.Transparent);
                MsgCls("", LblSec80c, Color.Transparent);
                MsgCls("", LblHousing, Color.Transparent);
                MsgCls("", LblOthers, Color.Transparent);
                Tab1H.CssClass = "Clicked";
                Tab2H.CssClass = "Initial";
                Tab3H.CssClass = "Initial";
                Tab4H.CssClass = "Initial";
                MultiViewH.ActiveViewIndex = 0;
                GridControls();
                HFTabID.Value = "1";

            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        }


        protected void GVSec80CHeader_Sorting(object sender, GridViewSortEventArgs e)
        {
            try
            {
                HFTabID.Value = "2";
                LoadSec80C();
                search();
                viewcheckSec80C.Value = "NO";
                List<ITbo> ITboList = (List<ITbo>)Session["ITSec80CGrdInfo"];
                bool objSortOrder = (bool)Session["bSortedOrder"];
                switch (e.SortExpression.ToString().Trim())
                {
                    case "CREATED_BY":
                        if (objSortOrder)
                        {
                            if (ITboList != null)
                            {
                                ITboList.Sort(delegate(ITbo objBo1, ITbo objBo2)
                                { return ((int.Parse(objBo1.CREATED_BY)).CompareTo(int.Parse(objBo2.CREATED_BY))); });
                                objSortOrder = false;
                                Session.Add("bSortedOrder", objSortOrder);
                            }
                        }
                        else
                        {
                            ITboList.Sort(delegate(ITbo objBo1, ITbo objBo2)
                            { return ((int.Parse(objBo2.CREATED_BY)).CompareTo(int.Parse(objBo1.CREATED_BY))); });
                            objSortOrder = true;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                        break;

                    case "ENAME":
                        if (objSortOrder)
                        {
                            if (ITboList != null)
                            {
                                ITboList.Sort(delegate(ITbo objBo1, ITbo objBo2)
                                { return (objBo1.ENAME.ToString().CompareTo(objBo2.ENAME.ToString())); });
                                objSortOrder = false;
                                Session.Add("bSortedOrder", objSortOrder);
                            }
                        }
                        else
                        {
                            ITboList.Sort(delegate(ITbo objBo1, ITbo objBo2)
                            { return (objBo2.ENAME.ToString().CompareTo(objBo1.ENAME.ToString())); });
                            objSortOrder = true;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                        break;
                    case "ID":
                        if (objSortOrder)
                        {
                            if (ITboList != null)
                            {
                                ITboList.Sort(delegate(ITbo objBo1, ITbo objBo2)
                                { return (objBo1.ID.Value.CompareTo(objBo2.ID.Value)); });
                                objSortOrder = false;
                                Session.Add("bSortedOrder", objSortOrder);
                            }
                        }
                        else
                        {
                            ITboList.Sort(delegate(ITbo objBo1, ITbo objBo2)
                            { return (objBo2.ID.Value.CompareTo(objBo1.ID.Value)); });
                            objSortOrder = true;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                        break;



                    case "ITTYP":
                        if (objSortOrder)
                        {
                            if (ITboList != null)
                            {
                                ITboList.Sort(delegate(ITbo objBo1, ITbo objBo2)
                                { return (int.Parse(objBo1.ITTYP.ToString()).CompareTo(int.Parse(objBo2.ITTYP.ToString()))); });
                                objSortOrder = false;
                                Session.Add("bSortedOrder", objSortOrder);
                            }
                        }
                        else
                        {
                            ITboList.Sort(delegate(ITbo objBo1, ITbo objBo2)
                            { return (int.Parse(objBo2.ITTYP.ToString()).CompareTo(int.Parse(objBo1.ITTYP.ToString()))); });
                            objSortOrder = true;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                        break;

                    case "STATUS":
                        if (objSortOrder)
                        {
                            if (ITboList != null)
                            {
                                ITboList.Sort(delegate(ITbo objBo1, ITbo objBo2)
                                { return (objBo1.STATUS.ToString().CompareTo(objBo2.STATUS.ToString())); });
                                objSortOrder = false;
                                Session.Add("bSortedOrder", objSortOrder);
                            }
                        }
                        else
                        {
                            ITboList.Sort(delegate(ITbo objBo1, ITbo objBo2)
                            { return (objBo2.STATUS.ToString().CompareTo(objBo1.STATUS.ToString())); });
                            objSortOrder = true;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                        break;


                    case "CREATED_ON":
                        if (objSortOrder)
                        {
                            if (ITboList != null)
                            {
                                ITboList.Sort(delegate(ITbo objBo1, ITbo objBo2)
                                { return (objBo1.CREATED_ON.Value.CompareTo(objBo2.CREATED_ON.Value)); });
                                objSortOrder = false;
                                Session.Add("bSortedOrder", objSortOrder);
                            }
                        }
                        else
                        {
                            ITboList.Sort(delegate(ITbo objBo1, ITbo objBo2)
                            { return (objBo2.CREATED_ON.Value.CompareTo(objBo1.CREATED_ON.Value)); });
                            objSortOrder = true;
                            Session.Add("bSortedOrder", objSortOrder);
                        }

                        break;

                    case "BEGDA":
                        if (objSortOrder)
                        {
                            if (ITboList != null)
                            {
                                ITboList.Sort(delegate(ITbo objBo1, ITbo objBo2)
                                { return (objBo1.BEGDA.Value.CompareTo(objBo2.BEGDA.Value)); });
                                objSortOrder = false;
                                Session.Add("bSortedOrder", objSortOrder);
                            }
                        }
                        else
                        {
                            ITboList.Sort(delegate(ITbo objBo1, ITbo objBo2)
                            { return (objBo2.BEGDA.Value.CompareTo(objBo1.BEGDA.Value)); });
                            objSortOrder = true;
                            Session.Add("bSortedOrder", objSortOrder);
                        }

                        break;

                    case "ENDDA":
                        if (objSortOrder)
                        {
                            if (ITboList != null)
                            {
                                ITboList.Sort(delegate(ITbo objBo1, ITbo objBo2)
                                { return (objBo1.ENDDA.Value.CompareTo(objBo2.ENDDA.Value)); });
                                objSortOrder = false;
                                Session.Add("bSortedOrder", objSortOrder);
                            }
                        }
                        else
                        {
                            ITboList.Sort(delegate(ITbo objBo1, ITbo objBo2)
                            { return (objBo2.ENDDA.Value.CompareTo(objBo1.ENDDA.Value)); });
                            objSortOrder = true;
                            Session.Add("bSortedOrder", objSortOrder);
                        }

                        break;


                    case "APPROVEDON":
                        if (objSortOrder)
                        {
                            if (ITboList != null)
                            {
                                ITboList.Sort(delegate(ITbo objBo1, ITbo objBo2)
                                { return (objBo1.APPROVEDON.Value.CompareTo(objBo2.APPROVEDON.Value)); });
                                objSortOrder = false;
                                Session.Add("bSortedOrder", objSortOrder);
                            }
                        }
                        else
                        {
                            ITboList.Sort(delegate(ITbo objBo1, ITbo objBo2)
                            { return (objBo2.APPROVEDON.Value.CompareTo(objBo1.APPROVEDON.Value)); });
                            objSortOrder = true;
                            Session.Add("bSortedOrder", objSortOrder);
                        }

                        break;

                }

                GVSec80CHeader.DataSource = ITboList;
                GVSec80CHeader.DataBind();

                Session.Add("ITSec80CGrdInfo", ITboList);
                txtsearch.Text = string.Empty;
                txtFromDateH.Text = string.Empty;
                txtTodateH.Text = string.Empty;
                MsgCls("", lblMessageBoard, Color.Transparent);
                MsgCls("", LblSec80c, Color.Transparent);
                MsgCls("", LblHousing, Color.Transparent);
                MsgCls("", LblOthers, Color.Transparent);
                Tab1H.CssClass = "Initial";
                Tab2H.CssClass = "Clicked";
                Tab3H.CssClass = "Initial";
                Tab4H.CssClass = "Initial";
                MultiViewH.ActiveViewIndex = 1;
                GridControls();
                HFTabID.Value = "2";
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        }
        protected void GVHousingHeader_Sorting(object sender, GridViewSortEventArgs e)
        {
            try
            {
                HFTabID.Value = "3";
                LoadHousing();
                search();
                viewcheckHousing.Value = "NO";
                List<ITbo> ITboList = (List<ITbo>)Session["ITHousingGrdInfo"];
                bool objSortOrder = (bool)Session["bSortedOrder"];
                switch (e.SortExpression.ToString().Trim())
                {
                    case "CREATED_BY":
                        if (objSortOrder)
                        {
                            if (ITboList != null)
                            {
                                ITboList.Sort(delegate(ITbo objBo1, ITbo objBo2)
                                { return ((int.Parse(objBo1.CREATED_BY)).CompareTo(int.Parse(objBo2.CREATED_BY))); });
                                objSortOrder = false;
                                Session.Add("bSortedOrder", objSortOrder);
                            }
                        }
                        else
                        {
                            ITboList.Sort(delegate(ITbo objBo1, ITbo objBo2)
                            { return ((int.Parse(objBo2.CREATED_BY)).CompareTo(int.Parse(objBo1.CREATED_BY))); });
                            objSortOrder = true;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                        break;

                    case "ENAME":
                        if (objSortOrder)
                        {
                            if (ITboList != null)
                            {
                                ITboList.Sort(delegate(ITbo objBo1, ITbo objBo2)
                                { return (objBo1.ENAME.ToString().CompareTo(objBo2.ENAME.ToString())); });
                                objSortOrder = false;
                                Session.Add("bSortedOrder", objSortOrder);
                            }
                        }
                        else
                        {
                            ITboList.Sort(delegate(ITbo objBo1, ITbo objBo2)
                            { return (objBo2.ENAME.ToString().CompareTo(objBo1.ENAME.ToString())); });
                            objSortOrder = true;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                        break;
                    case "ID":
                        if (objSortOrder)
                        {
                            if (ITboList != null)
                            {
                                ITboList.Sort(delegate(ITbo objBo1, ITbo objBo2)
                                { return (objBo1.ID.Value.CompareTo(objBo2.ID.Value)); });
                                objSortOrder = false;
                                Session.Add("bSortedOrder", objSortOrder);
                            }
                        }
                        else
                        {
                            ITboList.Sort(delegate(ITbo objBo1, ITbo objBo2)
                            { return (objBo2.ID.Value.CompareTo(objBo1.ID.Value)); });
                            objSortOrder = true;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                        break;



                    case "ITTYP":
                        if (objSortOrder)
                        {
                            if (ITboList != null)
                            {
                                ITboList.Sort(delegate(ITbo objBo1, ITbo objBo2)
                                { return (int.Parse(objBo1.ITTYP.ToString()).CompareTo(int.Parse(objBo2.ITTYP.ToString()))); });
                                objSortOrder = false;
                                Session.Add("bSortedOrder", objSortOrder);
                            }
                        }
                        else
                        {
                            ITboList.Sort(delegate(ITbo objBo1, ITbo objBo2)
                            { return (int.Parse(objBo2.ITTYP.ToString()).CompareTo(int.Parse(objBo1.ITTYP.ToString()))); });
                            objSortOrder = true;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                        break;

                    case "STATUS":
                        if (objSortOrder)
                        {
                            if (ITboList != null)
                            {
                                ITboList.Sort(delegate(ITbo objBo1, ITbo objBo2)
                                { return (objBo1.STATUS.ToString().CompareTo(objBo2.STATUS.ToString())); });
                                objSortOrder = false;
                                Session.Add("bSortedOrder", objSortOrder);
                            }
                        }
                        else
                        {
                            ITboList.Sort(delegate(ITbo objBo1, ITbo objBo2)
                            { return (objBo2.STATUS.ToString().CompareTo(objBo1.STATUS.ToString())); });
                            objSortOrder = true;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                        break;


                    case "CREATED_ON":
                        if (objSortOrder)
                        {
                            if (ITboList != null)
                            {
                                ITboList.Sort(delegate(ITbo objBo1, ITbo objBo2)
                                { return (objBo1.CREATED_ON.Value.CompareTo(objBo2.CREATED_ON.Value)); });
                                objSortOrder = false;
                                Session.Add("bSortedOrder", objSortOrder);
                            }
                        }
                        else
                        {
                            ITboList.Sort(delegate(ITbo objBo1, ITbo objBo2)
                            { return (objBo2.CREATED_ON.Value.CompareTo(objBo1.CREATED_ON.Value)); });
                            objSortOrder = true;
                            Session.Add("bSortedOrder", objSortOrder);
                        }

                        break;

                    case "BEGDA":
                        if (objSortOrder)
                        {
                            if (ITboList != null)
                            {
                                ITboList.Sort(delegate(ITbo objBo1, ITbo objBo2)
                                { return (objBo1.BEGDA.Value.CompareTo(objBo2.BEGDA.Value)); });
                                objSortOrder = false;
                                Session.Add("bSortedOrder", objSortOrder);
                            }
                        }
                        else
                        {
                            ITboList.Sort(delegate(ITbo objBo1, ITbo objBo2)
                            { return (objBo2.BEGDA.Value.CompareTo(objBo1.BEGDA.Value)); });
                            objSortOrder = true;
                            Session.Add("bSortedOrder", objSortOrder);
                        }

                        break;

                    case "ENDDA":
                        if (objSortOrder)
                        {
                            if (ITboList != null)
                            {
                                ITboList.Sort(delegate(ITbo objBo1, ITbo objBo2)
                                { return (objBo1.ENDDA.Value.CompareTo(objBo2.ENDDA.Value)); });
                                objSortOrder = false;
                                Session.Add("bSortedOrder", objSortOrder);
                            }
                        }
                        else
                        {
                            ITboList.Sort(delegate(ITbo objBo1, ITbo objBo2)
                            { return (objBo2.ENDDA.Value.CompareTo(objBo1.ENDDA.Value)); });
                            objSortOrder = true;
                            Session.Add("bSortedOrder", objSortOrder);
                        }

                        break;


                    case "APPROVEDON":
                        if (objSortOrder)
                        {
                            if (ITboList != null)
                            {
                                ITboList.Sort(delegate(ITbo objBo1, ITbo objBo2)
                                { return (objBo1.APPROVEDON.Value.CompareTo(objBo2.APPROVEDON.Value)); });
                                objSortOrder = false;
                                Session.Add("bSortedOrder", objSortOrder);
                            }
                        }
                        else
                        {
                            ITboList.Sort(delegate(ITbo objBo1, ITbo objBo2)
                            { return (objBo2.APPROVEDON.Value.CompareTo(objBo1.APPROVEDON.Value)); });
                            objSortOrder = true;
                            Session.Add("bSortedOrder", objSortOrder);
                        }

                        break;

                }

                GVHousingHeader.DataSource = ITboList;
                GVHousingHeader.DataBind();

                Session.Add("ITHousingGrdInfo", ITboList);
                ddlSeachSelect.SelectedValue = "0";
                txtsearch.Text = string.Empty;
                txtFromDateH.Text = string.Empty;
                txtTodateH.Text = string.Empty;
                MsgCls("", lblMessageBoard, Color.Transparent);
                MsgCls("", LblSec80c, Color.Transparent);
                MsgCls("", LblHousing, Color.Transparent);
                MsgCls("", LblOthers, Color.Transparent);
                Tab1H.CssClass = "Initial";
                Tab2H.CssClass = "Initial";
                Tab3H.CssClass = "Clicked";
                Tab4H.CssClass = "Initial";
                MultiViewH.ActiveViewIndex = 2;
                GridControls();
                HFTabID.Value = "3";
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        }


        protected void GVOthersHeader_Sorting(object sender, GridViewSortEventArgs e)
        {
            try
            {

                HFTabID.Value = "4";
                LoadOthers();
                search();
                viewcheckOthers.Value = "NO";
                List<ITbo> ITboList = (List<ITbo>)Session["ITOthersGrdInfo"];
                bool objSortOrder = (bool)Session["bSortedOrder"];
                switch (e.SortExpression.ToString().Trim())
                {
                    case "CREATED_BY":
                        if (objSortOrder)
                        {
                            if (ITboList != null)
                            {
                                ITboList.Sort(delegate(ITbo objBo1, ITbo objBo2)
                                { return ((int.Parse(objBo1.CREATED_BY)).CompareTo(int.Parse(objBo2.CREATED_BY))); });
                                objSortOrder = false;
                                Session.Add("bSortedOrder", objSortOrder);
                            }
                        }
                        else
                        {
                            ITboList.Sort(delegate(ITbo objBo1, ITbo objBo2)
                            { return ((int.Parse(objBo2.CREATED_BY)).CompareTo(int.Parse(objBo1.CREATED_BY))); });
                            objSortOrder = true;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                        break;

                    case "ENAME":
                        if (objSortOrder)
                        {
                            if (ITboList != null)
                            {
                                ITboList.Sort(delegate(ITbo objBo1, ITbo objBo2)
                                { return (objBo1.ENAME.ToString().CompareTo(objBo2.ENAME.ToString())); });
                                objSortOrder = false;
                                Session.Add("bSortedOrder", objSortOrder);
                            }
                        }
                        else
                        {
                            ITboList.Sort(delegate(ITbo objBo1, ITbo objBo2)
                            { return (objBo2.ENAME.ToString().CompareTo(objBo1.ENAME.ToString())); });
                            objSortOrder = true;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                        break;
                    case "ID":
                        if (objSortOrder)
                        {
                            if (ITboList != null)
                            {
                                ITboList.Sort(delegate(ITbo objBo1, ITbo objBo2)
                                { return (objBo1.ID.Value.CompareTo(objBo2.ID.Value)); });
                                objSortOrder = false;
                                Session.Add("bSortedOrder", objSortOrder);
                            }
                        }
                        else
                        {
                            ITboList.Sort(delegate(ITbo objBo1, ITbo objBo2)
                            { return (objBo2.ID.Value.CompareTo(objBo1.ID.Value)); });
                            objSortOrder = true;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                        break;



                    case "ITTYP":
                        if (objSortOrder)
                        {
                            if (ITboList != null)
                            {
                                ITboList.Sort(delegate(ITbo objBo1, ITbo objBo2)
                                { return (int.Parse(objBo1.ITTYP.ToString()).CompareTo(int.Parse(objBo2.ITTYP.ToString()))); });
                                objSortOrder = false;
                                Session.Add("bSortedOrder", objSortOrder);
                            }
                        }
                        else
                        {
                            ITboList.Sort(delegate(ITbo objBo1, ITbo objBo2)
                            { return (int.Parse(objBo2.ITTYP.ToString()).CompareTo(int.Parse(objBo1.ITTYP.ToString()))); });
                            objSortOrder = true;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                        break;

                    case "STATUS":
                        if (objSortOrder)
                        {
                            if (ITboList != null)
                            {
                                ITboList.Sort(delegate(ITbo objBo1, ITbo objBo2)
                                { return (objBo1.STATUS.ToString().CompareTo(objBo2.STATUS.ToString())); });
                                objSortOrder = false;
                                Session.Add("bSortedOrder", objSortOrder);
                            }
                        }
                        else
                        {
                            ITboList.Sort(delegate(ITbo objBo1, ITbo objBo2)
                            { return (objBo2.STATUS.ToString().CompareTo(objBo1.STATUS.ToString())); });
                            objSortOrder = true;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                        break;


                    case "CREATED_ON":
                        if (objSortOrder)
                        {
                            if (ITboList != null)
                            {
                                ITboList.Sort(delegate(ITbo objBo1, ITbo objBo2)
                                { return (objBo1.CREATED_ON.Value.CompareTo(objBo2.CREATED_ON.Value)); });
                                objSortOrder = false;
                                Session.Add("bSortedOrder", objSortOrder);
                            }
                        }
                        else
                        {
                            ITboList.Sort(delegate(ITbo objBo1, ITbo objBo2)
                            { return (objBo2.CREATED_ON.Value.CompareTo(objBo1.CREATED_ON.Value)); });
                            objSortOrder = true;
                            Session.Add("bSortedOrder", objSortOrder);
                        }

                        break;

                    case "BEGDA":
                        if (objSortOrder)
                        {
                            if (ITboList != null)
                            {
                                ITboList.Sort(delegate(ITbo objBo1, ITbo objBo2)
                                { return (objBo1.BEGDA.Value.CompareTo(objBo2.BEGDA.Value)); });
                                objSortOrder = false;
                                Session.Add("bSortedOrder", objSortOrder);
                            }
                        }
                        else
                        {
                            ITboList.Sort(delegate(ITbo objBo1, ITbo objBo2)
                            { return (objBo2.BEGDA.Value.CompareTo(objBo1.BEGDA.Value)); });
                            objSortOrder = true;
                            Session.Add("bSortedOrder", objSortOrder);
                        }

                        break;

                    case "ENDDA":
                        if (objSortOrder)
                        {
                            if (ITboList != null)
                            {
                                ITboList.Sort(delegate(ITbo objBo1, ITbo objBo2)
                                { return (objBo1.ENDDA.Value.CompareTo(objBo2.ENDDA.Value)); });
                                objSortOrder = false;
                                Session.Add("bSortedOrder", objSortOrder);
                            }
                        }
                        else
                        {
                            ITboList.Sort(delegate(ITbo objBo1, ITbo objBo2)
                            { return (objBo2.ENDDA.Value.CompareTo(objBo1.ENDDA.Value)); });
                            objSortOrder = true;
                            Session.Add("bSortedOrder", objSortOrder);
                        }

                        break;


                    case "APPROVEDON":
                        if (objSortOrder)
                        {
                            if (ITboList != null)
                            {
                                ITboList.Sort(delegate(ITbo objBo1, ITbo objBo2)
                                { return (objBo1.APPROVEDON.Value.CompareTo(objBo2.APPROVEDON.Value)); });
                                objSortOrder = false;
                                Session.Add("bSortedOrder", objSortOrder);
                            }
                        }
                        else
                        {
                            ITboList.Sort(delegate(ITbo objBo1, ITbo objBo2)
                            { return (objBo2.APPROVEDON.Value.CompareTo(objBo1.APPROVEDON.Value)); });
                            objSortOrder = true;
                            Session.Add("bSortedOrder", objSortOrder);
                        }

                        break;

                }

                GVOthersHeader.DataSource = ITboList;
                GVOthersHeader.DataBind();

                Session.Add("ITOthersGrdInfo", ITboList);
                ddlSeachSelect.SelectedValue = "0";
                txtsearch.Text = string.Empty;
                txtFromDateH.Text = string.Empty;
                txtTodateH.Text = string.Empty;
                MsgCls("", lblMessageBoard, Color.Transparent);
                MsgCls("", LblSec80c, Color.Transparent);
                MsgCls("", LblHousing, Color.Transparent);
                MsgCls("", LblOthers, Color.Transparent);
                Tab1H.CssClass = "Initial";
                Tab2H.CssClass = "Initial";
                Tab3H.CssClass = "Initial";
                Tab4H.CssClass = "Clicked";
                MultiViewH.ActiveViewIndex = 3;
                GridControls();
                HFTabID.Value = "4";
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        }


        protected void BtnExporttoXlSec80_Click(object sender, EventArgs e)
        {
            try
            {
                ExportToExcelSec80();
                txtsearch.Focus();
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        }

        public void ExportToExcelSec80()
        {
            try
            {
                if (viewcheckSec80.Value == "YES")
                {
                    System.IO.StringWriter sw = new System.IO.StringWriter();
                    System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);

                    // Render grid view control.
                    htw.WriteBreak();

                    string colHeads = "IT Declaration Details";
                    htw.WriteEncodedText(colHeads);
                    GVITSec80H.RenderControl(htw);
                    htw.WriteBreak();

                    // Write the rendered content to a file.

                    // renderedGridView = "Summary_Report" + "<br>"; //+ sw.ToString();
                    string renderedGridView = "IT Declaration Details <br/>";
                    renderedGridView += "<table><tr><td align=left>IT ID</td><td align=left>:</td><td align=left>" + ViewState["ITSEC80ID"].ToString() + "</td></tr>";
                    renderedGridView += "<tr><td align=left>IT Type</td><td align=left>:</td><td align=left>" + ViewState["ITSEC80TYP"].ToString() + "</td></tr>";
                    renderedGridView += "<tr><td align=left>From Date</td><td align=left>:</td><td align=left>" + ViewState["BEDGASEC80"].ToString() + "</td></tr>";
                    renderedGridView += "<tr><td align=left>To Date</td><td align=left>:</td><td align=left>" + ViewState["ENDDASEC80"].ToString() + "</td></tr>";
                    renderedGridView += "<tr><td align=left>Consider Actuals</td><td align=left>:</td><td align=left>" + ViewState["SEC80CA"].ToString() + "</td></tr>";
                    //renderedGridView += "<tr><td align=left>Created By</td><td align=left>:</td><td align=left>" + ViewState["SEC80CREATEDBY"].ToString() + "</td></tr>";
                    //renderedGridView += "<tr><td align=left>Employee Name</td><td align=left>:</td><td align=left>" + ViewState["SEC80ENAME"].ToString() + "</td></tr>";
                    renderedGridView += "<tr><td align=left>Created On</td><td align=left>:</td><td align=left>" + ViewState["SEC80CREATED_ON"].ToString() + "</td></tr>";
                    renderedGridView += "<tr><td align=left>Approved On</td><td align=left>:</td><td align=left>" + ViewState["SEC80APPROVEDON"].ToString() + "</td></tr>";
                    renderedGridView += "<tr><td align=left>Remarks</td><td align=left>:</td><td align=left>" + ViewState["SEC80REMARKS"].ToString() + "</td></tr>";
                    renderedGridView += "<tr><td align=left>Status</td><td align=left>:</td><td align=left>" + ViewState["SEC80STS"].ToString() + "</td></tr></table>";
                    renderedGridView += sw.ToString() + "<br/>";
                    Response.AppendHeader("content-disposition", "attachment; filename=" + "Summary_Report" + "_IncomeTax.xls");
                    Response.ContentType = "Application/vnd.ms-excel";
                    Response.Write(renderedGridView);
                    Response.End();
                }
                else
                {

                    System.IO.StringWriter sw = new System.IO.StringWriter();
                    System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);

                    HFTabID.Value = "1";
                    // Render grid view control.
                    htw.WriteBreak();
                    GVSec80Header.AllowPaging = false;
                    search();
                    GVSec80Header.Columns[10].Visible = false;
                    GVSec80Header.HeaderRow.ForeColor = System.Drawing.Color.Blue;
                    GVSec80Header.HeaderRow.BackColor = System.Drawing.Color.LightBlue;
                    GVSec80Header.RenderControl(htw);
                    GVSec80Header.Columns[10].Visible = true;
                    GVSec80Header.AllowPaging = true;

                    htw.WriteBreak();


                    // Write the rendered content to a file.

                    // renderedGridView = "Summary_Report" + "<br>"; //+ sw.ToString();
                    string renderedGridView = "IT Declaration Details <br/>";
                    renderedGridView += sw.ToString() + "<br/>";
                    Response.AppendHeader("content-disposition", "attachment; filename=" + "Summary_Report" + "_IncomeTax.xls");
                    Response.ContentType = "Application/vnd.ms-excel";
                    Response.Write(renderedGridView);
                    Response.End();

                }
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        }

        protected void BtnExporttoPDFSec80_Click(object sender, EventArgs e)
        {
            try
            {
                if (viewcheckSec80.Value == "YES")
                {
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=" + "Summary_Report" + "_IncomeTax.pdf");
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);

                    StringWriter s_tw = new StringWriter();
                    HtmlTextWriter h_textw = new HtmlTextWriter(s_tw);
                    h_textw.AddStyleAttribute("font-size", "8pt");
                    h_textw.AddStyleAttribute("color", "Black");

                    string colHeads = "IT Declaration Details";
                    h_textw.WriteEncodedText(colHeads);
                    h_textw.WriteBreak();
                    colHeads = "IT ID :" + ViewState["ITSEC80ID"].ToString();
                    h_textw.WriteEncodedText(colHeads);
                    h_textw.WriteBreak();
                    colHeads = "IT Type :" + ViewState["ITSEC80TYP"].ToString();
                    h_textw.WriteEncodedText(colHeads);
                    h_textw.WriteBreak();
                    colHeads = "From Date :" + ViewState["BEDGASEC80"].ToString();
                    h_textw.WriteEncodedText(colHeads);
                    h_textw.WriteBreak();
                    colHeads = "To Date :" + ViewState["ENDDASEC80"].ToString();
                    h_textw.WriteEncodedText(colHeads);
                    h_textw.WriteBreak();
                    colHeads = "Consider Actuals :" + ViewState["SEC80CA"].ToString();
                    h_textw.WriteEncodedText(colHeads);
                    h_textw.WriteBreak();
                    //colHeads = "Employee Name :" + ViewState["SEC80CREATEDBY"].ToString() + " - " + ViewState["SEC80ENAME"].ToString();
                    //h_textw.WriteEncodedText(colHeads);
                    //h_textw.WriteBreak();
                    colHeads = "Created On :" + ViewState["SEC80CREATED_ON"].ToString();
                    h_textw.WriteEncodedText(colHeads);
                    h_textw.WriteBreak();
                    colHeads = "Approved On :" + ViewState["SEC80APPROVEDON"].ToString();
                    h_textw.WriteEncodedText(colHeads);
                    h_textw.WriteBreak();
                    colHeads = "Remarks :" + ViewState["SEC80REMARKS"].ToString();
                    h_textw.WriteEncodedText(colHeads);
                    h_textw.WriteBreak();
                    colHeads = "Status :" + ViewState["SEC80STS"].ToString();
                    h_textw.WriteEncodedText(colHeads);
                    h_textw.WriteBreak();


                    // h_textw.WriteEncodedText(colHeads);
                    h_textw.WriteBreak();
                    // colHeads = " Details";
                    // h_textw.WriteEncodedText(colHeads);
                    h_textw.WriteBreak();
                    GVITSec80H.RenderControl(h_textw);
                    h_textw.WriteBreak();

                    // Document doc = new Document(PageSize.A4.Rotate(), 0, 0, 5, 0); 

                    iTextSharp.text.Document doc = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4.Rotate(), 0, 0, 5, 0);

                    //  Document doc = new Document();
                    iTextSharp.text.pdf.PdfWriter.GetInstance(doc, Response.OutputStream);
                    doc.Open();
                    StringReader s_tr = new StringReader(s_tw.ToString());
                    iTextSharp.text.html.simpleparser.HTMLWorker html_worker = new iTextSharp.text.html.simpleparser.HTMLWorker(doc);
                    html_worker.Parse(s_tr);
                    doc.Close();
                    Response.Write(doc);
                }
                else
                {
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=" + "Summary_Report" + "_IncomeTax.pdf");
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);

                    StringWriter s_tw = new StringWriter();
                    HtmlTextWriter h_textw = new HtmlTextWriter(s_tw);
                    h_textw.AddStyleAttribute("font-size", "8pt");
                    h_textw.AddStyleAttribute("color", "Black");

                    HFTabID.Value = "1";

                    h_textw.WriteBreak();
                    string colHeads = "IT Declaration Details";
                    h_textw.WriteEncodedText(colHeads);
                    h_textw.WriteBreak();
                    GVSec80Header.AllowPaging = false;

                    search();
                    GVSec80Header.Columns[10].Visible = false;
                    GVSec80Header.HeaderRow.ForeColor = System.Drawing.Color.Blue;
                    GVSec80Header.HeaderRow.BackColor = System.Drawing.Color.LightBlue;
                    GVSec80Header.RenderControl(h_textw);
                    GVSec80Header.Columns[10].Visible = true;
                    GVSec80Header.AllowPaging = true;
                    h_textw.WriteBreak();

                    // Document doc = new Document(PageSize.A4.Rotate(), 0, 0, 5, 0); 

                    iTextSharp.text.Document doc = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4.Rotate(), 7f, 7f, 7f, 0f);

                    //  Document doc = new Document();
                    iTextSharp.text.pdf.PdfWriter.GetInstance(doc, Response.OutputStream);
                    doc.Open();
                    StringReader s_tr = new StringReader(s_tw.ToString());
                    iTextSharp.text.html.simpleparser.HTMLWorker html_worker = new iTextSharp.text.html.simpleparser.HTMLWorker(doc);
                    html_worker.Parse(s_tr);
                    doc.Close();
                    Response.Write(doc);

                }
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        }
        public override void VerifyRenderingInServerForm(Control control)
        {

        }

        protected void BtnExptoXLSEC80C_Click(object sender, EventArgs e)
        {
            try
            {
                ExportToExcelSec80C();
                txtsearch.Focus();
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        }

        protected void BtnExptoPdfSec80C_Click(object sender, EventArgs e)
        {
            try
            {
                ExportToPdfSec80C();
                txtsearch.Focus();
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        }

        public void ExportToPdfSec80C()
        {
            try
            {
                if (viewcheckSec80C.Value == "YES")
                {
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=" + "Summary_Report" + "_IncomeTax.pdf");
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);

                    StringWriter s_tw = new StringWriter();
                    HtmlTextWriter h_textw = new HtmlTextWriter(s_tw);
                    h_textw.AddStyleAttribute("font-size", "8pt");
                    h_textw.AddStyleAttribute("color", "Black");

                    string colHeads = "IT Declaration Details";
                    h_textw.WriteEncodedText(colHeads);
                    h_textw.WriteBreak();
                    colHeads = "IT ID :" + ViewState["ITSEC80CID"].ToString();
                    h_textw.WriteEncodedText(colHeads);
                    h_textw.WriteBreak();
                    colHeads = "IT Type :" + ViewState["ITSEC80CTYP"].ToString();
                    h_textw.WriteEncodedText(colHeads);
                    h_textw.WriteBreak();
                    colHeads = "From Date :" + ViewState["BEDGASEC80C"].ToString();
                    h_textw.WriteEncodedText(colHeads);
                    h_textw.WriteBreak();
                    colHeads = "To Date :" + ViewState["ENDDASEC80C"].ToString();
                    h_textw.WriteEncodedText(colHeads);
                    h_textw.WriteBreak();
                    colHeads = "Consider Actuals :" + ViewState["SEC80CCA"].ToString();
                    h_textw.WriteEncodedText(colHeads);
                    h_textw.WriteBreak();
                    //colHeads = "Employee Name :" + ViewState["SEC80CCREATEDBY"].ToString() + " - " + ViewState["SEC80CENAME"].ToString();
                    //h_textw.WriteEncodedText(colHeads);
                    //h_textw.WriteBreak();
                    colHeads = "Created On :" + ViewState["SEC80CCREATED_ON"].ToString();
                    h_textw.WriteEncodedText(colHeads);
                    h_textw.WriteBreak();
                    colHeads = "Approved On :" + ViewState["SEC80CAPPROVEDON"].ToString();
                    h_textw.WriteEncodedText(colHeads);
                    h_textw.WriteBreak();
                    colHeads = "Remarks :" + ViewState["SEC80CREMARKS"].ToString();
                    h_textw.WriteEncodedText(colHeads);
                    h_textw.WriteBreak();
                    colHeads = "Status :" + ViewState["SEC80CSTS"].ToString();
                    h_textw.WriteEncodedText(colHeads);
                    h_textw.WriteBreak();


                    // h_textw.WriteEncodedText(colHeads);
                    h_textw.WriteBreak();
                    // colHeads = " Details";
                    // h_textw.WriteEncodedText(colHeads);
                    h_textw.WriteBreak();
                    GVITSec80CH.RenderControl(h_textw);
                    h_textw.WriteBreak();

                    // Document doc = new Document(PageSize.A4.Rotate(), 0, 0, 5, 0); 

                    iTextSharp.text.Document doc = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4.Rotate(), 0, 0, 5, 0);

                    //  Document doc = new Document();
                    iTextSharp.text.pdf.PdfWriter.GetInstance(doc, Response.OutputStream);
                    doc.Open();
                    StringReader s_tr = new StringReader(s_tw.ToString());
                    iTextSharp.text.html.simpleparser.HTMLWorker html_worker = new iTextSharp.text.html.simpleparser.HTMLWorker(doc);
                    html_worker.Parse(s_tr);
                    doc.Close();
                    Response.Write(doc);
                }
                else
                {
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=" + "Summary_Report" + "_IncomeTax.pdf");
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);

                    StringWriter s_tw = new StringWriter();
                    HtmlTextWriter h_textw = new HtmlTextWriter(s_tw);
                    h_textw.AddStyleAttribute("font-size", "8pt");
                    h_textw.AddStyleAttribute("color", "Black");

                    HFTabID.Value = "2";

                    h_textw.WriteBreak();
                    string colHeads = "IT Declaration Details";
                    h_textw.WriteEncodedText(colHeads);
                    h_textw.WriteBreak();
                    GVSec80CHeader.AllowPaging = false;

                    search();
                    GVSec80CHeader.Columns[10].Visible = false;
                    GVSec80CHeader.HeaderRow.ForeColor = System.Drawing.Color.Blue;
                    GVSec80CHeader.HeaderRow.BackColor = System.Drawing.Color.LightBlue;
                    GVSec80CHeader.RenderControl(h_textw);
                    GVSec80CHeader.Columns[10].Visible = true;
                    GVSec80CHeader.AllowPaging = true;
                    h_textw.WriteBreak();

                    // Document doc = new Document(PageSize.A4.Rotate(), 0, 0, 5, 0); 

                    iTextSharp.text.Document doc = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4.Rotate(), 7f, 7f, 7f, 0f);

                    //  Document doc = new Document();
                    iTextSharp.text.pdf.PdfWriter.GetInstance(doc, Response.OutputStream);
                    doc.Open();
                    StringReader s_tr = new StringReader(s_tw.ToString());
                    iTextSharp.text.html.simpleparser.HTMLWorker html_worker = new iTextSharp.text.html.simpleparser.HTMLWorker(doc);
                    html_worker.Parse(s_tr);
                    doc.Close();
                    Response.Write(doc);

                }
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        }

        public void ExportToExcelSec80C()
        {
            try
            {
                if (viewcheckSec80C.Value == "YES")
                {
                    System.IO.StringWriter sw = new System.IO.StringWriter();
                    System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);

                    // Render grid view control.
                    htw.WriteBreak();

                    string colHeads = "IT Declaration Details";
                    htw.WriteEncodedText(colHeads);
                    GVITSec80CH.RenderControl(htw);
                    htw.WriteBreak();

                    // Write the rendered content to a file.

                    // renderedGridView = "Summary_Report" + "<br>"; //+ sw.ToString();
                    string renderedGridView = "IT Declaration Details <br/>";
                    renderedGridView += "<table><tr><td align=left>IT ID</td><td align=left>:</td><td align=left>" + ViewState["ITSEC80CID"].ToString() + "</td></tr>";
                    renderedGridView += "<tr><td align=left>IT Type</td><td align=left>:</td><td align=left>" + ViewState["ITSEC80CTYP"].ToString() + "</td></tr>";
                    renderedGridView += "<tr><td align=left>From Date</td><td align=left>:</td><td align=left>" + ViewState["BEDGASEC80C"].ToString() + "</td></tr>";
                    renderedGridView += "<tr><td align=left>To Date</td><td align=left>:</td><td align=left>" + ViewState["ENDDASEC80C"].ToString() + "</td></tr>";
                    renderedGridView += "<tr><td align=left>Consider Actuals</td><td align=left>:</td><td align=left>" + ViewState["SEC80CCA"].ToString() + "</td></tr>";
                    //renderedGridView += "<tr><td align=left>Created By</td><td align=left>:</td><td align=left>" + ViewState["SEC80CCREATEDBY"].ToString() + "</td></tr>";
                    //renderedGridView += "<tr><td align=left>Employee Name</td><td align=left>:</td><td align=left>" + ViewState["SEC80CENAME"].ToString() + "</td></tr>";
                    renderedGridView += "<tr><td align=left>Created On</td><td align=left>:</td><td align=left>" + ViewState["SEC80CCREATED_ON"].ToString() + "</td></tr>";
                    renderedGridView += "<tr><td align=left>Approved On</td><td align=left>:</td><td align=left>" + ViewState["SEC80CAPPROVEDON"].ToString() + "</td></tr>";
                    renderedGridView += "<tr><td align=left>Remarks</td><td align=left>:</td><td align=left>" + ViewState["SEC80CREMARKS"].ToString() + "</td></tr>";
                    renderedGridView += "<tr><td align=left>Status</td><td align=left>:</td><td align=left>" + ViewState["SEC80CSTS"].ToString() + "</td></tr></table>";
                    renderedGridView += sw.ToString() + "<br/>";
                    Response.AppendHeader("content-disposition", "attachment; filename=" + "Summary_Report" + "_IncomeTax.xls");
                    Response.ContentType = "Application/vnd.ms-excel";
                    Response.Write(renderedGridView);
                    Response.End();
                }
                else
                {

                    System.IO.StringWriter sw = new System.IO.StringWriter();
                    System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);

                    HFTabID.Value = "2";
                    // Render grid view control.
                    htw.WriteBreak();
                    GVSec80CHeader.AllowPaging = false;
                    search();
                    GVSec80CHeader.Columns[10].Visible = false;
                    GVSec80CHeader.HeaderRow.ForeColor = System.Drawing.Color.Blue;
                    GVSec80CHeader.HeaderRow.BackColor = System.Drawing.Color.LightBlue;
                    GVSec80CHeader.RenderControl(htw);
                    GVSec80CHeader.Columns[10].Visible = true;
                    GVSec80CHeader.AllowPaging = true;

                    htw.WriteBreak();


                    // Write the rendered content to a file.

                    // renderedGridView = "Summary_Report" + "<br>"; //+ sw.ToString();
                    string renderedGridView = "IT Declaration Details <br/>";
                    renderedGridView += sw.ToString() + "<br/>";
                    Response.AppendHeader("content-disposition", "attachment; filename=" + "Summary_Report" + "_IncomeTax.xls");
                    Response.ContentType = "Application/vnd.ms-excel";
                    Response.Write(renderedGridView);
                    Response.End();

                }
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        }

        protected void BtnExptoXLHousing_Click(object sender, EventArgs e)
        {
            try
            {
                if (viewcheckHousing.Value == "YES")
                {
                    System.IO.StringWriter sw = new System.IO.StringWriter();
                    System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);

                    // Render grid view control.
                    htw.WriteBreak();

                    string colHeads = "IT Declaration Details";
                    htw.WriteEncodedText(colHeads);
                    GVHousing.RenderControl(htw);
                    htw.WriteBreak();

                    // Write the rendered content to a file.

                    // renderedGridView = "Summary_Report" + "<br>"; //+ sw.ToString();
                    string renderedGridView = "IT Declaration Details <br/>";
                    renderedGridView += "<table><tr><td align=left>IT ID</td><td align=left>:</td><td align=left>" + ViewState["ITHOUSINGID"].ToString() + "</td></tr>";
                    renderedGridView += "<tr><td align=left>IT Type</td><td align=left>:</td><td align=left>" + ViewState["ITHOUSINGTYP"].ToString() + "</td></tr>";
                    renderedGridView += "<tr><td align=left>From Date</td><td align=left>:</td><td align=left>" + ViewState["BEDGAHOUSING"].ToString() + "</td></tr>";
                    renderedGridView += "<tr><td align=left>To Date</td><td align=left>:</td><td align=left>" + ViewState["ENDDAHOUSING"].ToString() + "</td></tr>";
                    //renderedGridView += "<tr><td align=left>Consider Actuals</td><td align=left>:</td><td align=left>" + ViewState["HOUSINGCA"].ToString() + "</td></tr>";
                    //renderedGridView += "<tr><td align=left>Created By</td><td align=left>:</td><td align=left>" + ViewState["HOUSINGCREATEDBY"].ToString() + "</td></tr>";
                    //renderedGridView += "<tr><td align=left>Employee Name</td><td align=left>:</td><td align=left>" + ViewState["HOUSINGENAME"].ToString() + "</td></tr>";
                    renderedGridView += "<tr><td align=left>Created On</td><td align=left>:</td><td align=left>" + ViewState["HOUSINGCREATED_ON"].ToString() + "</td></tr>";
                    renderedGridView += "<tr><td align=left>Approved On</td><td align=left>:</td><td align=left>" + ViewState["HOUSINGAPPROVEDON"].ToString() + "</td></tr>";
                    renderedGridView += "<tr><td align=left>Remarks</td><td align=left>:</td><td align=left>" + ViewState["HOUSINGREMARKS"].ToString() + "</td></tr>";
                    renderedGridView += "<tr><td align=left>Status</td><td align=left>:</td><td align=left>" + ViewState["HOUSINGSTS"].ToString() + "</td></tr></table>";
                    renderedGridView += sw.ToString() + "<br/>";
                    Response.AppendHeader("content-disposition", "attachment; filename=" + "Summary_Report" + "_IncomeTax.xls");
                    Response.ContentType = "Application/vnd.ms-excel";
                    Response.Write(renderedGridView);
                    Response.End();
                }
                else
                {

                    System.IO.StringWriter sw = new System.IO.StringWriter();
                    System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);

                    HFTabID.Value = "3";
                    // Render grid view control.
                    htw.WriteBreak();
                    GVHousingHeader.AllowPaging = false;
                    search();
                    GVHousingHeader.Columns[9].Visible = false;
                    GVHousingHeader.HeaderRow.ForeColor = System.Drawing.Color.Blue;
                    GVHousingHeader.HeaderRow.BackColor = System.Drawing.Color.LightBlue;
                    GVHousingHeader.RenderControl(htw);
                    GVHousingHeader.Columns[9].Visible = true;
                    GVHousingHeader.AllowPaging = true;

                    htw.WriteBreak();


                    // Write the rendered content to a file.

                    // renderedGridView = "Summary_Report" + "<br>"; //+ sw.ToString();
                    string renderedGridView = "IT Declaration Details <br/>";
                    renderedGridView += sw.ToString() + "<br/>";
                    Response.AppendHeader("content-disposition", "attachment; filename=" + "Summary_Report" + "_IncomeTax.xls");
                    Response.ContentType = "Application/vnd.ms-excel";
                    Response.Write(renderedGridView);
                    Response.End();

                }
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        }

        protected void BtnExptopdfHousing_Click(object sender, EventArgs e)
        {
            try
            {
                if (viewcheckHousing.Value == "YES")
                {
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=" + "Summary_Report" + "_IncomeTax.pdf");
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);

                    StringWriter s_tw = new StringWriter();
                    HtmlTextWriter h_textw = new HtmlTextWriter(s_tw);
                    h_textw.AddStyleAttribute("font-size", "8pt");
                    h_textw.AddStyleAttribute("color", "Black");

                    string colHeads = "IT Declaration Details";
                    h_textw.WriteEncodedText(colHeads);
                    h_textw.WriteBreak();
                    colHeads = "IT ID :" + ViewState["ITHOUSINGID"].ToString();
                    h_textw.WriteEncodedText(colHeads);
                    h_textw.WriteBreak();
                    colHeads = "IT Type :" + ViewState["ITHOUSINGTYP"].ToString();
                    h_textw.WriteEncodedText(colHeads);
                    h_textw.WriteBreak();
                    colHeads = "From Date :" + ViewState["BEDGAHOUSING"].ToString();
                    h_textw.WriteEncodedText(colHeads);
                    h_textw.WriteBreak();
                    colHeads = "To Date :" + ViewState["ENDDAHOUSING"].ToString();
                    h_textw.WriteEncodedText(colHeads);
                    h_textw.WriteBreak();
                    //colHeads = "Consider Actuals :" + ViewState["HOUSINGCA"].ToString();
                    //h_textw.WriteEncodedText(colHeads);
                    //h_textw.WriteBreak();
                    //colHeads = "Employee Name :" + ViewState["HOUSINGCREATEDBY"].ToString() + " - " + ViewState["HOUSINGENAME"].ToString();
                    //h_textw.WriteEncodedText(colHeads);
                    //h_textw.WriteBreak();
                    colHeads = "Created On :" + ViewState["HOUSINGCREATED_ON"].ToString();
                    h_textw.WriteEncodedText(colHeads);
                    h_textw.WriteBreak();
                    colHeads = "Approved On :" + ViewState["HOUSINGAPPROVEDON"].ToString();
                    h_textw.WriteEncodedText(colHeads);
                    h_textw.WriteBreak();
                    colHeads = "Remarks :" + ViewState["HOUSINGREMARKS"].ToString();
                    h_textw.WriteEncodedText(colHeads);
                    h_textw.WriteBreak();
                    colHeads = "Status :" + ViewState["HOUSINGSTS"].ToString();
                    h_textw.WriteEncodedText(colHeads);
                    h_textw.WriteBreak();


                    // h_textw.WriteEncodedText(colHeads);
                    h_textw.WriteBreak();
                    // colHeads = " Details";
                    // h_textw.WriteEncodedText(colHeads);
                    h_textw.WriteBreak();
                    GVHousing.RenderControl(h_textw);
                    h_textw.WriteBreak();

                    // Document doc = new Document(PageSize.A4.Rotate(), 0, 0, 5, 0); 

                    iTextSharp.text.Document doc = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4.Rotate(), 0, 0, 5, 0);

                    //  Document doc = new Document();
                    iTextSharp.text.pdf.PdfWriter.GetInstance(doc, Response.OutputStream);
                    doc.Open();
                    StringReader s_tr = new StringReader(s_tw.ToString());
                    iTextSharp.text.html.simpleparser.HTMLWorker html_worker = new iTextSharp.text.html.simpleparser.HTMLWorker(doc);
                    html_worker.Parse(s_tr);
                    doc.Close();
                    Response.Write(doc);
                }
                else
                {
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=" + "Summary_Report" + "_IncomeTax.pdf");
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);

                    StringWriter s_tw = new StringWriter();
                    HtmlTextWriter h_textw = new HtmlTextWriter(s_tw);
                    h_textw.AddStyleAttribute("font-size", "8pt");
                    h_textw.AddStyleAttribute("color", "Black");

                    HFTabID.Value = "3";

                    h_textw.WriteBreak();
                    string colHeads = "IT Declaration Details";
                    h_textw.WriteEncodedText(colHeads);
                    h_textw.WriteBreak();
                    GVHousingHeader.AllowPaging = false;

                    search();
                    GVHousingHeader.Columns[9].Visible = false;
                    GVHousingHeader.HeaderRow.ForeColor = System.Drawing.Color.Blue;
                    GVHousingHeader.HeaderRow.BackColor = System.Drawing.Color.LightBlue;
                    GVHousingHeader.RenderControl(h_textw);
                    GVHousingHeader.Columns[9].Visible = true;
                    GVHousingHeader.AllowPaging = true;
                    h_textw.WriteBreak();

                    // Document doc = new Document(PageSize.A4.Rotate(), 0, 0, 5, 0); 

                    iTextSharp.text.Document doc = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4.Rotate(), 7f, 7f, 7f, 0f);

                    //  Document doc = new Document();
                    iTextSharp.text.pdf.PdfWriter.GetInstance(doc, Response.OutputStream);
                    doc.Open();
                    StringReader s_tr = new StringReader(s_tw.ToString());
                    iTextSharp.text.html.simpleparser.HTMLWorker html_worker = new iTextSharp.text.html.simpleparser.HTMLWorker(doc);
                    html_worker.Parse(s_tr);
                    doc.Close();
                    Response.Write(doc);

                }
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        }

        protected void BtnExptoXLOthers_Click(object sender, EventArgs e)
        {
            try
            {
                if (viewcheckOthers.Value == "YES")
                {
                    System.IO.StringWriter sw = new System.IO.StringWriter();
                    System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);

                    // Render grid view control.
                    htw.WriteBreak();

                    string colHeads = "IT Declaration Details";
                    htw.WriteEncodedText(colHeads);

                    if (ViewState["ITOTHERSTYP"].ToString().Trim() == "1")
                    {
                        grdHistySelf.GridLines = GridLines.Both;
                        grdHisBorw.GridLines = GridLines.Both;
                        GVOthers1.GridLines = GridLines.Both;
                        grdHistySelf.RenderControl(htw);

                        if (ViewState["TypeRENTO"].ToString() == "3")
                            GVOthers1.RenderControl(htw);

                        grdHisBorw.RenderControl(htw);

                    }
                    else if (ViewState["ITOTHERSTYP"].ToString().Trim() == "2")
                    {
                        GVOthers2.GridLines = GridLines.Both;
                        GVOthers2.RenderControl(htw);
                    }
                    htw.WriteBreak();

                    // Write the rendered content to a file.

                    // renderedGridView = "Summary_Report" + "<br>"; //+ sw.ToString();
                    string renderedGridView = "IT Declaration Details <br/>";
                    renderedGridView += "<table><tr><td align=left>IT ID</td><td align=left>:</td><td align=left>" + ViewState["ITOTHERSID"].ToString() + "</td></tr>";
                    renderedGridView += "<tr><td align=left>IT Type</td><td align=left>:</td><td align=left>" + ViewState["ITOTHERSTYP"].ToString() + "</td></tr>";
                    renderedGridView += "<tr><td align=left>From Date</td><td align=left>:</td><td align=left>" + ViewState["BEDGAOTHERS"].ToString() + "</td></tr>";
                    renderedGridView += "<tr><td align=left>To Date</td><td align=left>:</td><td align=left>" + ViewState["ENDDAOTHERS"].ToString() + "</td></tr>";
                    //renderedGridView += "<tr><td align=left>Consider Actuals</td><td align=left>:</td><td align=left>" + ViewState["HOUSINGCA"].ToString() + "</td></tr>";
                    //renderedGridView += "<tr><td align=left>Created By</td><td align=left>:</td><td align=left>" + ViewState["OTHERSCREATEDBY"].ToString() + "</td></tr>";
                    //renderedGridView += "<tr><td align=left>Employee Name</td><td align=left>:</td><td align=left>" + ViewState["OTHERSENAME"].ToString() + "</td></tr>";
                    renderedGridView += "<tr><td align=left>Created On</td><td align=left>:</td><td align=left>" + ViewState["OTHERSCREATED_ON"].ToString() + "</td></tr>";
                    renderedGridView += "<tr><td align=left>Approved On</td><td align=left>:</td><td align=left>" + ViewState["OTHERSAPPROVEDON"].ToString() + "</td></tr>";
                    renderedGridView += "<tr><td align=left>Remarks</td><td align=left>:</td><td align=left>" + ViewState["OTHERSREMARKS"].ToString() + "</td></tr>";
                    renderedGridView += "<tr><td align=left>Status</td><td align=left>:</td><td align=left>" + ViewState["OTHERSSTS"].ToString() + "</td></tr></table>";
                    renderedGridView += sw.ToString() + "<br/>";
                    Response.AppendHeader("content-disposition", "attachment; filename=" + "Summary_Report" + "_IncomeTax.xls");
                    Response.ContentType = "Application/vnd.ms-excel";
                    Response.Write(renderedGridView);
                    Response.End();
                }
                else
                {

                    System.IO.StringWriter sw = new System.IO.StringWriter();
                    System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);

                    HFTabID.Value = "4";
                    // Render grid view control.
                    htw.WriteBreak();
                    GVOthersHeader.AllowPaging = false;
                    search();
                    GVOthersHeader.Columns[9].Visible = false;
                    GVOthersHeader.HeaderRow.ForeColor = System.Drawing.Color.Blue;
                    GVOthersHeader.HeaderRow.BackColor = System.Drawing.Color.LightBlue;
                    GVOthersHeader.RenderControl(htw);
                    GVOthersHeader.Columns[9].Visible = true;
                    GVOthersHeader.AllowPaging = true;

                    htw.WriteBreak();


                    // Write the rendered content to a file.

                    // renderedGridView = "Summary_Report" + "<br>"; //+ sw.ToString();
                    string renderedGridView = "IT Declaration Details <br/>";
                    renderedGridView += sw.ToString() + "<br/>";
                    Response.AppendHeader("content-disposition", "attachment; filename=" + "Summary_Report" + "_IncomeTax.xls");
                    Response.ContentType = "Application/vnd.ms-excel";
                    Response.Write(renderedGridView);
                    Response.End();

                }
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        }

        protected void BtnExptopdfOthers_Click(object sender, EventArgs e)
        {
            try
            {
                if (viewcheckOthers.Value == "YES")
                {
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=" + "Summary_Report" + "_IncomeTax.pdf");
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);

                    StringWriter s_tw = new StringWriter();
                    HtmlTextWriter h_textw = new HtmlTextWriter(s_tw);
                    h_textw.AddStyleAttribute("font-size", "8pt");
                    h_textw.AddStyleAttribute("color", "Black");

                    string colHeads = "IT Declaration Details";
                    h_textw.WriteEncodedText(colHeads);
                    h_textw.WriteBreak();
                    colHeads = "IT ID :" + ViewState["ITOTHERSID"].ToString();
                    h_textw.WriteEncodedText(colHeads);
                    h_textw.WriteBreak();
                    colHeads = "IT Type :" + ViewState["ITOTHERSTYP"].ToString();
                    h_textw.WriteEncodedText(colHeads);
                    h_textw.WriteBreak();
                    colHeads = "From Date :" + ViewState["BEDGAOTHERS"].ToString();
                    h_textw.WriteEncodedText(colHeads);
                    h_textw.WriteBreak();
                    colHeads = "To Date :" + ViewState["ENDDAOTHERS"].ToString();
                    h_textw.WriteEncodedText(colHeads);
                    h_textw.WriteBreak();
                    //colHeads = "Consider Actuals :" + ViewState["HOUSINGCA"].ToString();
                    //h_textw.WriteEncodedText(colHeads);
                    //h_textw.WriteBreak();
                    //colHeads = "Employee Name :" + ViewState["OTHERSCREATEDBY"].ToString() + " - " + ViewState["OTHERSENAME"].ToString();
                    //h_textw.WriteEncodedText(colHeads);
                    //h_textw.WriteBreak();
                    colHeads = "Created On :" + ViewState["OTHERSCREATED_ON"].ToString();
                    h_textw.WriteEncodedText(colHeads);
                    h_textw.WriteBreak();
                    colHeads = "Approved On :" + ViewState["OTHERSAPPROVEDON"].ToString();
                    h_textw.WriteEncodedText(colHeads);
                    h_textw.WriteBreak();
                    colHeads = "Remarks :" + ViewState["OTHERSREMARKS"].ToString();
                    h_textw.WriteEncodedText(colHeads);
                    h_textw.WriteBreak();
                    colHeads = "Status :" + ViewState["OTHERSSTS"].ToString();
                    h_textw.WriteEncodedText(colHeads);
                    h_textw.WriteBreak();


                    // h_textw.WriteEncodedText(colHeads);
                    h_textw.WriteBreak();
                    // colHeads = " Details";
                    //h_textw.WriteEncodedText(colHeads);
                    h_textw.WriteBreak();

                    if (ViewState["ITOTHERSTYP"].ToString().Trim() == "1")
                    {
                        grdHistySelf.GridLines = GridLines.Both;
                        grdHisBorw.GridLines = GridLines.Both;
                        GVOthers1.GridLines = GridLines.Both;
                        grdHistySelf.RenderControl(h_textw);
                        h_textw.WriteBreak();
                        if (ViewState["TypeRENTO"].ToString() == "3")
                            GVOthers1.RenderControl(h_textw);

                        h_textw.WriteBreak();
                        grdHisBorw.RenderControl(h_textw);
                        h_textw.WriteBreak();
                    }
                    else if (ViewState["ITOTHERSTYP"].ToString().Trim() == "2")
                    {
                        GVOthers2.GridLines = GridLines.Both;
                        GVOthers2.RenderControl(h_textw);
                    }

                    h_textw.WriteBreak();

                    // Document doc = new Document(PageSize.A4.Rotate(), 0, 0, 5, 0); 

                    iTextSharp.text.Document doc = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4.Rotate(), 0, 0, 5, 0);

                    //  Document doc = new Document();
                    iTextSharp.text.pdf.PdfWriter.GetInstance(doc, Response.OutputStream);
                    doc.Open();
                    StringReader s_tr = new StringReader(s_tw.ToString());
                    iTextSharp.text.html.simpleparser.HTMLWorker html_worker = new iTextSharp.text.html.simpleparser.HTMLWorker(doc);
                    html_worker.Parse(s_tr);
                    doc.Close();
                    Response.Write(doc);
                }
                else
                {
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=" + "Summary_Report" + "_IncomeTax.pdf");
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);

                    StringWriter s_tw = new StringWriter();
                    HtmlTextWriter h_textw = new HtmlTextWriter(s_tw);
                    h_textw.AddStyleAttribute("font-size", "8pt");
                    h_textw.AddStyleAttribute("color", "Black");

                    HFTabID.Value = "4";

                    h_textw.WriteBreak();
                    string colHeads = "IT Declaration Details";
                    h_textw.WriteEncodedText(colHeads);
                    h_textw.WriteBreak();
                    GVOthersHeader.AllowPaging = false;

                    search();
                    GVOthersHeader.Columns[9].Visible = false;
                    GVOthersHeader.HeaderRow.ForeColor = System.Drawing.Color.Blue;
                    GVOthersHeader.HeaderRow.BackColor = System.Drawing.Color.LightBlue;
                    GVOthersHeader.RenderControl(h_textw);
                    GVOthersHeader.Columns[9].Visible = true;
                    GVOthersHeader.AllowPaging = true;
                    h_textw.WriteBreak();

                    // Document doc = new Document(PageSize.A4.Rotate(), 0, 0, 5, 0); 

                    iTextSharp.text.Document doc = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4.Rotate(), 7f, 7f, 7f, 0f);

                    //  Document doc = new Document();
                    iTextSharp.text.pdf.PdfWriter.GetInstance(doc, Response.OutputStream);
                    doc.Open();
                    StringReader s_tr = new StringReader(s_tw.ToString());
                    iTextSharp.text.html.simpleparser.HTMLWorker html_worker = new iTextSharp.text.html.simpleparser.HTMLWorker(doc);
                    html_worker.Parse(s_tr);
                    doc.Close();
                    Response.Write(doc);

                }
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        }

        protected void Tab5H_Click(object sender, EventArgs e)
        {
            ddlSeachSelect.SelectedValue = "0";
            txtsearch.Text = string.Empty;
            txtFromDateH.Text = string.Empty;
            txtTodateH.Text = string.Empty;
            MsgCls("", lblMessageBoard, Color.Transparent);
            MsgCls("", LblSec80c, Color.Transparent);
            MsgCls("", LblHousing, Color.Transparent);
            MsgCls("", LblOthers, Color.Transparent);
            Tab1H.CssClass = "Initial";
            Tab2H.CssClass = "Initial";
            Tab3H.CssClass = "Initial";
            Tab4H.CssClass = "Initial";
            Tab5H.CssClass = "Clicked";
            MultiViewH.ActiveViewIndex = 4;
            Load_PreEmp_Income();
            HFTabID.Value = "5";
            HFPreEmptInc.Value = "NO";
            grdPreEmptInc.Visible = false;
        }
        /*------------IT_History Ends------*/


        protected void LoadTab6()
        {
            Tab1.CssClass = "nav-link p-2";
            Tab2.CssClass = "nav-link p-2";
            Tab3.CssClass = "nav-link p-2";
            Tab4.CssClass = "nav-link p-2";
            Tab5.CssClass = "nav-link p-2";
            Tab6.CssClass = "nav-link active p-2";
            Tab7.CssClass = "nav-link p-2";
            MainView.ActiveViewIndex = 5;
            Load_PreEmp_Income_main(); lblMessageBoard.Text = "";
        }

        protected void Tab6_Click(object sender, EventArgs e)
        {
            LoadTab6();

        }


        protected void Tab7_Click(object sender, EventArgs e)
        {
            loadTab7();
        }

        void loadTab7()
        {
            Tab6.CssClass = "nav-link p-2";
            Tab1.CssClass = "nav-link p-2";
            Tab2.CssClass = "nav-link p-2";
            Tab3.CssClass = "nav-link p-2";
            Tab4.CssClass = "nav-link p-2";
            Tab5.CssClass = "nav-link p-2";
            Tab7.CssClass = "nav-link active p-2";
            MainView.ActiveViewIndex = 6;

            ////pageLoadEvents5();
            lblMessageBoard.Text = "";

            lnkDwnlITFile.Visible = checkITFileExts();
        }



        protected void btnITPreEmprSub_Click(object sender, EventArgs e)
        {
            try
            {
                bool? stus = false;
                int? PEMPT = 0;
                ITbl ITblObj = new ITbl();
                ITbo ITboObj = new ITbo();
                ITboObj.PreEmpr = txtITPreEmprNm.Text.Trim();
                ITboObj.PreEmprPAN = txtITPreEmprPAN.Text.Trim();
                ITboObj.PreEmprTAN = txtITPreEmprTAN.Text.Trim();
                ITboObj.GRSAL = string.IsNullOrEmpty(txtITPreEmprSal171.Text) ? 0 : decimal.Parse(txtITPreEmprSal171.Text.ToString().Trim());
                ITboObj.VPRQS = string.IsNullOrEmpty(txtITPreEmprVal172.Text) ? 0 : decimal.Parse(txtITPreEmprVal172.Text.ToString().Trim());
                ITboObj.PRSAL = string.IsNullOrEmpty(txtITPreEmprPro173.Text) ? 0 : decimal.Parse(txtITPreEmprPro173.Text.ToString().Trim());
                ITboObj.EXS10 = string.IsNullOrEmpty(txtITPreEmprExpSec.Text) ? 0 : decimal.Parse(txtITPreEmprExpSec.Text.ToString().Trim());
                ITboObj.PRTAX = string.IsNullOrEmpty(txtITPreEmprProTax.Text) ? 0 : decimal.Parse(txtITPreEmprProTax.Text.ToString().Trim());
                ITboObj.PRFND = string.IsNullOrEmpty(txtITPreEmprPF.Text) ? 0 : decimal.Parse(txtITPreEmprPF.Text.ToString().Trim());
                ITboObj.TXDED = string.IsNullOrEmpty(txtITPreEmprIT.Text) ? 0 : decimal.Parse(txtITPreEmprIT.Text.ToString().Trim());
                ITboObj.SCDED = string.IsNullOrEmpty(txtITPreEmprSurD.Text) ? 0 : decimal.Parse(txtITPreEmprSurD.Text.ToString().Trim());
                ITboObj.ECDED = string.IsNullOrEmpty(txtITPreEmprCess.Text) ? 0 : decimal.Parse(txtITPreEmprCess.Text.ToString().Trim());
                ITboObj.PERNR = User.Identity.Name;
                ITboObj.BEGDA = DateTime.Parse(txtITPreEmprFrmDt.Text.Trim());
                ITboObj.ENDDA = DateTime.Parse(txtITPreEmprToDt.Text.Trim());
                ITboObj.MODIFIED_BY = User.Identity.Name;
                ITboObj.Flag = 1;
                ITboObj.ID = 0;
                ITblObj.Create_ITPreEmptIncome(ITboObj, ref PEMPT, ref stus);
                if (stus == true)
                {
                    //SendMailHousingOthers(ITOTHERSIDU, "Updated", "Income from Other Sources");
                    ////MsgCls("Previous Employment Income created successfully !", LblMsg, Color.Green);
                    ////ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Previous Employment Income created successfully !')", true);
                    Load_PreEmp_Income_main();
                    ITPreEmprClear();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Income Tax Previous Employment Income Request saved successfully');", true);
                    lblMessageBoard.Text = "Income Tax Previous Employment Income Request saved successfully.";
                }
                loadTab4();
            }
            catch (Exception ex)
            {
                switch (ex.Message.ToString())
                {
                    case "-10":
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Dates are same !')", true);
                        break;
                    default:
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + ex.Message + "')", true);
                        break;
                }
            }
        }

        protected void btnITPreEmprClear_Click(object sender, EventArgs e)
        {
            ITPreEmprClear();
        }


        protected void ITPreEmprClear()
        {
            txtITPreEmprNm.Text = "";
            txtITPreEmprPAN.Text = "";
            txtITPreEmprTAN.Text = "";
            txtITPreEmprSal171.Text = "";
            txtITPreEmprVal172.Text = "";
            txtITPreEmprPro173.Text = "";
            txtITPreEmprExpSec.Text = "";
            txtITPreEmprProTax.Text = "";
            txtITPreEmprPF.Text = "";
            txtITPreEmprIT.Text = "";
            txtITPreEmprSurD.Text = "";
            txtITPreEmprCess.Text = "";
            txtITPreEmprFrmDt.Text = "";
            txtITPreEmprToDt.Text = "";
        }
        protected void Load_PreEmp_Income()
        {
            try
            {
                int flag = 5;
                ITbl ITblObj = new ITbl();
                List<ITbo> ITboObj = new List<ITbo>();
                List<ITbo> ITboObj1 = new List<ITbo>();
                string ApproverId = User.Identity.Name;
                ITboObj1 = ITblObj.Load_IT_HeaderHistory(User.Identity.Name, flag);
                //Session.Add("ITOthersGrdInfo", ITboObj1);

                if (ITboObj1 == null || ITboObj1.Count == 0)
                {
                    //MsgCls("No Records Found !", LblOthers, Color.Red);
                    grdPreEmptIncHead.Visible = false;
                    grdPreEmptIncHead.DataSource = null;
                    grdPreEmptIncHead.DataBind();
                    divPreEmptIncExprt.Visible = false;
                    return;
                }
                else
                {
                    grdPreEmptIncHead.Visible = true;
                    grdPreEmptIncHead.DataSource = ITboObj1;
                    grdPreEmptIncHead.SelectedIndex = -1;
                    grdPreEmptIncHead.DataBind();
                    divPreEmptIncExprt.Visible = true;
                }
            }
            catch (Exception ex) { }
        }

        protected void Load_PreEmp_Income_main()
        {
            try
            {
                ITbl ITblObj = new ITbl();
                List<ITbo> ITboObj = new List<ITbo>();
                List<ITbo> ITboObj1 = new List<ITbo>();
                string ApproverId = User.Identity.Name;
                ITboObj1 = ITblObj.Load_PreEpInCm(User.Identity.Name, 2, 0);
                //Session.Add("ITOthersGrdInfo", ITboObj1);

                if (ITboObj1 == null || ITboObj1.Count == 0)
                {
                    //MsgCls("No Records Found !", LblOthers, Color.Red);
                    grdPreEmpDetails.Visible = false;
                    grdPreEmpDetails.DataSource = null;
                    grdPreEmpDetails.DataBind();
                    return;
                }
                else
                {
                    grdPreEmpDetails.Visible = true;
                    grdPreEmpDetails.DataSource = ITboObj1;
                    grdPreEmpDetails.SelectedIndex = -1;
                    grdPreEmpDetails.DataBind();
                }
                ViewState["PreEmptID"] = "";
            }
            catch (Exception ex) { }
        }

        protected void grdPreEmptIncHead_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName.ToUpper())
                {
                    case "VIEW":
                        HFPreEmptInc.Value = "YES";
                        int rowIndex = Convert.ToInt32(e.CommandArgument);
                        foreach (GridViewRow row in grdPreEmptIncHead.Rows)
                        {
                            row.BackColor = row.RowIndex.Equals(rowIndex) ?
                            System.Drawing.Color.LightGray :
                            System.Drawing.Color.White;
                        }

                        int ID = int.Parse(grdPreEmptIncHead.DataKeys[int.Parse(e.CommandArgument.ToString())]["ID"].ToString());
                        ViewState["PreemptIn_ID"] = ID;
                        ITbl ITblObj = new ITbl();
                        List<ITbo> ITboObj = new List<ITbo>();

                        ITboObj = ITblObj.Load_PreEpInCm(User.Identity.Name.Trim(), 1, ID);
                        grdPreEmptInc.DataSource = ITboObj;
                        grdPreEmptInc.DataBind();
                        grdPreEmptInc.Visible = true;
                        break;
                    default:
                        break;
                }


            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        }

        protected void grdPreEmptIncHead_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                grdPreEmptInc.Visible = false;
                int pageindex = e.NewPageIndex;
                grdPreEmptIncHead.PageIndex = e.NewPageIndex;
                Load_PreEmp_Income();
                search();
                ddlSeachSelect.SelectedValue = "0";
                txtsearch.Text = string.Empty;
                txtFromDateH.Text = string.Empty;
                txtTodateH.Text = string.Empty;
                MsgCls("", lblMessageBoard, Color.Transparent);
                MsgCls("", LblSec80c, Color.Transparent);
                MsgCls("", LblHousing, Color.Transparent);
                MsgCls("", LblOthers, Color.Transparent);
                Tab1H.CssClass = "Initial";
                Tab2H.CssClass = "Initial";
                Tab3H.CssClass = "Initial";
                Tab5H.CssClass = "Clicked";
                Tab4H.CssClass = "Initial";
                MultiViewH.ActiveViewIndex = 4;
                HFTabID.Value = "5";
                HFPreEmptInc.Value = "NO";
            }
            catch (Exception ex) { }
        }

        protected void btnPreEmptIncXL_Click(object sender, EventArgs e)
        {
            try
            {
                if (HFPreEmptInc.Value == "YES")
                {
                    System.IO.StringWriter sw = new System.IO.StringWriter();
                    System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);

                    // Render grid view control.
                    htw.WriteBreak();

                    string colHeads = "IT Declaration Details";
                    htw.WriteEncodedText(colHeads);
                    grdPreEmptInc.RenderControl(htw);

                    htw.WriteBreak();

                    // Write the rendered content to a file.

                    // renderedGridView = "Summary_Report" + "<br>"; //+ sw.ToString();
                    string renderedGridView = "IT Declaration Details <br/>";
                    renderedGridView += "<table><tr><td align=left>IT ID</td><td align=left>:</td><td align=left>" + ViewState["PreemptIn_ID"].ToString() + "</td></tr>";
                    renderedGridView += "<tr><td align=left>IT Type</td><td align=left>:</td><td align=left> Previous Employment Income </td></tr></table>";
                    //renderedGridView += "<tr><td align=left>From Date</td><td align=left>:</td><td align=left>" + ViewState["BEDGAOTHERS"].ToString() + "</td></tr>";
                    //renderedGridView += "<tr><td align=left>To Date</td><td align=left>:</td><td align=left>" + ViewState["ENDDAOTHERS"].ToString() + "</td></tr>";
                    //renderedGridView += "<tr><td align=left>Consider Actuals</td><td align=left>:</td><td align=left>" + ViewState["HOUSINGCA"].ToString() + "</td></tr>";
                    //renderedGridView += "<tr><td align=left>Created By</td><td align=left>:</td><td align=left>" + ViewState["OTHERSCREATEDBY"].ToString() + "</td></tr>";
                    //renderedGridView += "<tr><td align=left>Employee Name</td><td align=left>:</td><td align=left>" + ViewState["OTHERSENAME"].ToString() + "</td></tr>";
                    //renderedGridView += "<tr><td align=left>Created On</td><td align=left>:</td><td align=left>" + ViewState["OTHERSCREATED_ON"].ToString() + "</td></tr>";
                    //renderedGridView += "<tr><td align=left>Approved On</td><td align=left>:</td><td align=left>" + ViewState["OTHERSAPPROVEDON"].ToString() + "</td></tr>";
                    //renderedGridView += "<tr><td align=left>Remarks</td><td align=left>:</td><td align=left>" + ViewState["OTHERSREMARKS"].ToString() + "</td></tr>";
                    //renderedGridView += "<tr><td align=left>Status</td><td align=left>:</td><td align=left>" + ViewState["OTHERSSTS"].ToString() + "</td></tr></table>";
                    renderedGridView += sw.ToString() + "<br/>";
                    Response.AppendHeader("content-disposition", "attachment; filename=" + "Summary_Report" + "_IncomeTax.xls");
                    Response.ContentType = "Application/vnd.ms-excel";
                    Response.Write(renderedGridView);
                    Response.End();
                }
                else
                {

                    System.IO.StringWriter sw = new System.IO.StringWriter();
                    System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);

                    HFTabID.Value = "5";
                    // Render grid view control.
                    htw.WriteBreak();
                    grdPreEmptIncHead.AllowPaging = false;
                    search();
                    grdPreEmptIncHead.Columns[9].Visible = false;
                    grdPreEmptIncHead.HeaderRow.ForeColor = System.Drawing.Color.Blue;
                    grdPreEmptIncHead.HeaderRow.BackColor = System.Drawing.Color.LightBlue;
                    grdPreEmptIncHead.RenderControl(htw);
                    grdPreEmptIncHead.Columns[9].Visible = true;
                    grdPreEmptIncHead.AllowPaging = true;

                    htw.WriteBreak();


                    // Write the rendered content to a file.

                    // renderedGridView = "Summary_Report" + "<br>"; //+ sw.ToString();
                    string renderedGridView = "IT Declaration Details <br/>";
                    renderedGridView += sw.ToString() + "<br/>";
                    Response.AppendHeader("content-disposition", "attachment; filename=" + "Summary_Report" + "_IncomeTax.xls");
                    Response.ContentType = "Application/vnd.ms-excel";
                    Response.Write(renderedGridView);
                    Response.End();

                }
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        }

        protected void btnPreEmptIncPDF_Click(object sender, EventArgs e)
        {
            try
            {
                if (HFPreEmptInc.Value == "YES")
                {
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=" + "Summary_Report" + "_IncomeTax.pdf");
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);

                    StringWriter s_tw = new StringWriter();
                    HtmlTextWriter h_textw = new HtmlTextWriter(s_tw);
                    h_textw.AddStyleAttribute("font-size", "8pt");
                    h_textw.AddStyleAttribute("color", "Black");

                    string colHeads = "IT Declaration Details";
                    h_textw.WriteEncodedText(colHeads);
                    h_textw.WriteBreak();
                    colHeads = "IT ID :" + ViewState["PreemptIn_ID"].ToString();
                    h_textw.WriteEncodedText(colHeads);
                    h_textw.WriteBreak();
                    colHeads = "IT Type : Previous Employment Income"; //ViewState["ITOTHERSTYP"].ToString();
                    h_textw.WriteEncodedText(colHeads);
                    h_textw.WriteBreak();
                    //colHeads = "From Date :" + ViewState["BEDGAOTHERS"].ToString();
                    //h_textw.WriteEncodedText(colHeads);
                    //h_textw.WriteBreak();
                    //colHeads = "To Date :" + ViewState["ENDDAOTHERS"].ToString();
                    //h_textw.WriteEncodedText(colHeads);
                    //h_textw.WriteBreak();
                    //colHeads = "Consider Actuals :" + ViewState["HOUSINGCA"].ToString();
                    //h_textw.WriteEncodedText(colHeads);
                    //h_textw.WriteBreak();
                    //colHeads = "Employee Name :" + ViewState["OTHERSCREATEDBY"].ToString() + " - " + ViewState["OTHERSENAME"].ToString();
                    //h_textw.WriteEncodedText(colHeads);
                    //h_textw.WriteBreak();
                    //colHeads = "Created On :" + ViewState["OTHERSCREATED_ON"].ToString();
                    //h_textw.WriteEncodedText(colHeads);
                    //h_textw.WriteBreak();
                    //colHeads = "Approved On :" + ViewState["OTHERSAPPROVEDON"].ToString();
                    //h_textw.WriteEncodedText(colHeads);
                    //h_textw.WriteBreak();
                    //colHeads = "Remarks :" + ViewState["OTHERSREMARKS"].ToString();
                    //h_textw.WriteEncodedText(colHeads);
                    //h_textw.WriteBreak();
                    //colHeads = "Status :" + ViewState["OTHERSSTS"].ToString();
                    //h_textw.WriteEncodedText(colHeads);
                    //h_textw.WriteBreak();


                    // h_textw.WriteEncodedText(colHeads);
                    h_textw.WriteBreak();
                    // colHeads = " Details";
                    //h_textw.WriteEncodedText(colHeads);
                    h_textw.WriteBreak();


                    grdPreEmptInc.RenderControl(h_textw);


                    h_textw.WriteBreak();

                    // Document doc = new Document(PageSize.A4.Rotate(), 0, 0, 5, 0); 

                    iTextSharp.text.Document doc = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4.Rotate(), 0, 0, 5, 0);

                    //  Document doc = new Document();
                    iTextSharp.text.pdf.PdfWriter.GetInstance(doc, Response.OutputStream);
                    doc.Open();
                    StringReader s_tr = new StringReader(s_tw.ToString());
                    iTextSharp.text.html.simpleparser.HTMLWorker html_worker = new iTextSharp.text.html.simpleparser.HTMLWorker(doc);
                    html_worker.Parse(s_tr);
                    doc.Close();
                    Response.Write(doc);
                }
                else
                {
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=" + "Summary_Report" + "_IncomeTax.pdf");
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);

                    StringWriter s_tw = new StringWriter();
                    HtmlTextWriter h_textw = new HtmlTextWriter(s_tw);
                    h_textw.AddStyleAttribute("font-size", "8pt");
                    h_textw.AddStyleAttribute("color", "Black");

                    HFTabID.Value = "5";

                    h_textw.WriteBreak();
                    string colHeads = "IT Declaration Details";
                    h_textw.WriteEncodedText(colHeads);
                    h_textw.WriteBreak();
                    grdPreEmptIncHead.AllowPaging = false;

                    search();
                    grdPreEmptIncHead.Columns[9].Visible = false;
                    grdPreEmptIncHead.HeaderRow.ForeColor = System.Drawing.Color.Blue;
                    grdPreEmptIncHead.HeaderRow.BackColor = System.Drawing.Color.LightBlue;
                    grdPreEmptIncHead.RenderControl(h_textw);
                    grdPreEmptIncHead.Columns[9].Visible = true;
                    grdPreEmptIncHead.AllowPaging = true;
                    h_textw.WriteBreak();

                    // Document doc = new Document(PageSize.A4.Rotate(), 0, 0, 5, 0); 

                    iTextSharp.text.Document doc = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4.Rotate(), 7f, 7f, 7f, 0f);

                    //  Document doc = new Document();
                    iTextSharp.text.pdf.PdfWriter.GetInstance(doc, Response.OutputStream);
                    doc.Open();
                    StringReader s_tr = new StringReader(s_tw.ToString());
                    iTextSharp.text.html.simpleparser.HTMLWorker html_worker = new iTextSharp.text.html.simpleparser.HTMLWorker(doc);
                    html_worker.Parse(s_tr);
                    doc.Close();
                    Response.Write(doc);

                }
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        }

        protected void grdPreEmpDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                ITbl ITblObj = new ITbl();
                List<ITbo> ITboObj = new List<ITbo>();
                ITbo ITboObj1 = new ITbo();
                int? refID = 0;
                bool? st = false;
                int ID = int.Parse(grdPreEmpDetails.DataKeys[int.Parse(e.CommandArgument.ToString())]["ID"].ToString());
                ViewState["PreEmptID"] = ID;
                switch (e.CommandName.ToUpper())
                {
                    case "EDITR":
                        ITPreEmprClear();
                        btnITPreEmprSub.Visible = false;
                        btnITPreEmprClear.Visible = false;
                        btnITPreEmprUpd.Visible = true;
                        btnITPreEmprCancel.Visible = true;
                        ITboObj = ITblObj.Load_PreEpInCm(User.Identity.Name, 1, ID);

                        txtITPreEmprNm.Text = ITboObj[0].PreEmpr.ToString();
                        txtITPreEmprPAN.Text = ITboObj[0].PreEmprPAN.ToString();
                        txtITPreEmprTAN.Text = ITboObj[0].PreEmprTAN.ToString();
                        txtITPreEmprSal171.Text = ITboObj[0].GRSAL.ToString();
                        txtITPreEmprVal172.Text = ITboObj[0].VPRQS.ToString();
                        txtITPreEmprPro173.Text = ITboObj[0].PRSAL.ToString();
                        txtITPreEmprExpSec.Text = ITboObj[0].EXS10.ToString();
                        txtITPreEmprProTax.Text = ITboObj[0].PRTAX.ToString();
                        txtITPreEmprPF.Text = ITboObj[0].PRFND.ToString();
                        txtITPreEmprIT.Text = ITboObj[0].TXDED.ToString();
                        txtITPreEmprSurD.Text = ITboObj[0].SCDED.ToString();
                        txtITPreEmprCess.Text = ITboObj[0].ECDED.ToString();
                        txtITPreEmprFrmDt.Text = ITboObj[0].BEGDA.ToString();
                        txtITPreEmprToDt.Text = ITboObj[0].ENDDA.ToString();
                        lblPretot.Text = (decimal.Parse(txtITPreEmprIT.Text.ToString()) + decimal.Parse(txtITPreEmprSurD.Text.ToString()) + decimal.Parse(txtITPreEmprCess.Text.ToString())).ToString();
                        break;
                    case "DELETER":
                        ITboObj1.Flag = 3;
                        ITboObj1.ID = ID;
                        ITblObj.Create_ITPreEmptIncome(ITboObj1, ref refID, ref st);
                        if (st == true)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Previous Employment Income deleted successfully !')", true);
                            Load_PreEmp_Income_main();
                            ITPreEmprClear();
                            btnITPreEmprSub.Visible = true;
                            btnITPreEmprClear.Visible = true;
                            btnITPreEmprUpd.Visible = false;
                            btnITPreEmprCancel.Visible = false;
                            Load_PreEmp_Income_main();
                        }

                        break;
                }
            }
            catch (Exception ex) { }
        }

        protected void btnITPreEmprUpd_Click(object sender, EventArgs e)
        {
            try
            {
                bool? stus = false;
                int? PEMPT = 0;
                ITbl ITblObj = new ITbl();
                ITbo ITboObj = new ITbo();
                ITboObj.PreEmpr = txtITPreEmprNm.Text.Trim();
                ITboObj.PreEmprPAN = txtITPreEmprPAN.Text.Trim();
                ITboObj.PreEmprTAN = txtITPreEmprTAN.Text.Trim();
                ITboObj.GRSAL = string.IsNullOrEmpty(txtITPreEmprSal171.Text) ? 0 : decimal.Parse(txtITPreEmprSal171.Text.ToString().Trim());
                ITboObj.VPRQS = string.IsNullOrEmpty(txtITPreEmprVal172.Text) ? 0 : decimal.Parse(txtITPreEmprVal172.Text.ToString().Trim());
                ITboObj.PRSAL = string.IsNullOrEmpty(txtITPreEmprPro173.Text) ? 0 : decimal.Parse(txtITPreEmprPro173.Text.ToString().Trim());
                ITboObj.EXS10 = string.IsNullOrEmpty(txtITPreEmprExpSec.Text) ? 0 : decimal.Parse(txtITPreEmprExpSec.Text.ToString().Trim());
                ITboObj.PRTAX = string.IsNullOrEmpty(txtITPreEmprProTax.Text) ? 0 : decimal.Parse(txtITPreEmprProTax.Text.ToString().Trim());
                ITboObj.PRFND = string.IsNullOrEmpty(txtITPreEmprPF.Text) ? 0 : decimal.Parse(txtITPreEmprPF.Text.ToString().Trim());
                ITboObj.TXDED = string.IsNullOrEmpty(txtITPreEmprIT.Text) ? 0 : decimal.Parse(txtITPreEmprIT.Text.ToString().Trim());
                ITboObj.SCDED = string.IsNullOrEmpty(txtITPreEmprSurD.Text) ? 0 : decimal.Parse(txtITPreEmprSurD.Text.ToString().Trim());
                ITboObj.ECDED = string.IsNullOrEmpty(txtITPreEmprCess.Text) ? 0 : decimal.Parse(txtITPreEmprCess.Text.ToString().Trim());
                ITboObj.PERNR = User.Identity.Name;
                ITboObj.BEGDA = DateTime.Parse(txtITPreEmprFrmDt.Text.Trim());
                ITboObj.ENDDA = DateTime.Parse(txtITPreEmprToDt.Text.Trim());
                ITboObj.MODIFIED_BY = User.Identity.Name;
                ITboObj.Flag = 2;
                ITboObj.ID = int.Parse(ViewState["PreEmptID"].ToString());
                ITblObj.Create_ITPreEmptIncome(ITboObj, ref PEMPT, ref stus);
                if (stus == true)
                {
                    //SendMailHousingOthers(ITOTHERSIDU, "Updated", "Income from Other Sources");
                    ////MsgCls("Previous Employment Income created successfully !", LblMsg, Color.Green);
                    ////ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Previous Employment Income updated successfully !')", true);
                    ITPreEmprClear();
                    Load_PreEmp_Income_main();
                    btnITPreEmprSub.Visible = true;
                    btnITPreEmprClear.Visible = true;
                    btnITPreEmprUpd.Visible = false;
                    btnITPreEmprCancel.Visible = false;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Income Tax Previous Employment Income Request saved successfully');", true);
                    lblMessageBoard.Text = "Income Tax Previous Employment Income Request saved successfully.";
                }
                loadTab4();
            }
            catch (Exception ex)
            {
                switch (ex.Message.ToString())
                {
                    case "-10":
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Dates are same !')", true);
                        break;
                    default:
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + ex.Message + "')", true);
                        break;
                }
            }
        }

        protected void btnITPreEmprCancel_Click(object sender, EventArgs e)
        {
            ITPreEmprClear();
            btnITPreEmprSub.Visible = true;
            btnITPreEmprClear.Visible = true;
            btnITPreEmprUpd.Visible = false;
            btnITPreEmprCancel.Visible = false;
            Load_PreEmp_Income_main();
        }

        protected void lnkborAdd_Click(object sender, EventArgs e)
        {
            try
            {
                using (DataTable Dtu = Session["BorwData"] != null ? (DataTable)Session["BorwData"] : dtBorw())
                {
                    Dtu.Rows.Add(1, txtBorwName.Text.Trim(), decimal.Parse(txtBorwPerct.Text.Trim()));
                    Session["BorwData"] = Dtu;
                    txtBorwName.Enabled = true;
                    txtBorwName.Text = "";
                    txtBorwPerct.Text = "";
                }
                Load_grdBorwDetails();
                Calvl();
            }
            catch (Exception ex) { }
        }

        protected void grdBorwDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                ITbl ITblObj = new ITbl();
                ITbo ITboObj = new ITbo();
                bool? sts = false;
                switch (e.CommandName.ToUpper())
                {
                    case "DELETER":
                        int index = Convert.ToInt32(e.CommandArgument.ToString());
                        DataTable dt = (DataTable)Session["BorwData"];
                        dt.Rows[index].Delete();
                        Session["BorwData"] = dt;
                        Load_grdBorwDetails();
                        if (dt.Rows.Count == 0)
                        {
                            txtBorwName.Text = Session["EmployeeName"].ToString();
                            txtBorwPerct.Text = "100";
                            txtBorwName.Enabled = false;
                        }
                        Calvl();
                        break;
                }
            }
            catch (Exception ex) { }
        }

        protected void Load_grdBorwDetails()
        {
            try
            {
                DataTable dt = (DataTable)Session["BorwData"];
                grdBorwDetails.DataSource = dt;
                grdBorwDetails.DataBind();
            }
            catch (Exception ex) { }
        }

        protected void Load_grdSelfOccDetails()
        {
            try
            {
                ITbl itobjBL = new ITbl();
                List<ITbo> ITboList = new List<ITbo>();
                ITboList = itobjBL.Load_HousingOthersDetails(0, "1", User.Identity.Name.Trim(), RB_PropTyp.SelectedValue.Trim());
                grdSelfOccDetails.DataSource = ITboList;
                grdSelfOccDetails.DataBind();
            }
            catch (Exception ex) { }
        }

        protected void grdSelfOccDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                ITbl ITblObj = new ITbl();
                ITbo ITboObj = new ITbo();
                List<ITbo> ITboList = new List<ITbo>();
                List<ITbo> ITboList2 = new List<ITbo>();
                bool? sts = false;
                int ID = int.Parse(grdSelfOccDetails.DataKeys[int.Parse(e.CommandArgument.ToString())]["ID"].ToString());
                ViewState["SelfID"] = ID;
                ITboList = ITblObj.Load_HousingOthersDetails(ID, "1", User.Identity.Name.Trim(), "1");
                ITboList2 = ITblObj.Load_PreEpInCm_cont(ID, 1);
                //int index = Convert.ToInt32(e.CommandArgument.ToString());
                switch (e.CommandName.ToUpper())
                {
                    case "VIEWR":
                        txtLendrName.Text = ITboList2[0].LENDNAME;
                        txtLendrAdd.Text = ITboList2[0].LENDADD;
                        txtLendrPAN.Text = ITboList2[0].LENDPAN;
                        txtLoanSacDt.Text = DateTime.Parse(ITboList2[0].LNSAC_DATE.ToString()).ToString("dd/MM/yyyy") == "1900-01-01" ? "" : DateTime.Parse(ITboList2[0].LNSAC_DATE.ToString()).ToString("dd/MM/yyyy");
                        txtLoanSacAmt.Text = ITboList[0].INT24.ToString();
                        txtStampDuty.Text = ITboList[0].OTH24.ToString();
                        txtTotInsPaid.Text = ITboList[0].CPGLN.ToString();
                        txtTotIns.Text = ITboList[0].CPGSS.ToString();
                        txtTotPrincpl.Text = ITboList[0].INTRS.ToString();
                        chkOwnhsPropty.Checked = ITboList[0].LETVL.ToString() == "0.00" ? false : true;
                        ddlSlfLoanType.SelectedValue = ITboList[0].BSPFT.ToString();
                        chkBenftsSec80EE.Checked = ITboList[0].DVDND.ToString() == "0.00" ? false : true;
                        txtTotPrinPaid.Text = ITboList[0].CPGLS.ToString();
                        txtStampChgrDt.Text = DateTime.Parse(ITboList2[0].STMPCHR_DATE.ToString()).ToString("dd/MM/yyyy") == "1900-01-01" ? "" : DateTime.Parse(ITboList2[0].STMPCHR_DATE.ToString()).ToString("dd/MM/yyyy");
                        txtValPropty.Text = ITboList[0].REP24.ToString();
                        txtCaptSqFt.Text = ITboList[0].TDSAT.ToString();
                        txtPurofHsLoan.Text = ITboList2[0].PUPSHSLN;
                        txtITSlfCity.Text = ITboList2[0].City;
                        txtITSlfState.Text = ITboList2[0].State;
                        txtAddrPropty.Text = ITboList2[0].ADDPROPTY;
                        TXTCOMMENTS.Text = ITboList[0].EMPCOMMENTS.ToString();
                        txtBorwName.Enabled = false;
                        txtBorwPerct.Enabled = false;
                        lnkborAdd.Enabled = false;
                        txtLendrName.Enabled = false;
                        txtLendrAdd.Enabled = false;
                        txtLendrPAN.Enabled = false;
                        txtLoanSacDt.Enabled = false;
                        txtLoanSacAmt.Enabled = false;
                        txtStampDuty.Enabled = false;
                        txtTotInsPaid.Enabled = false;
                        txtTotIns.Enabled = false;
                        txtTotPrincpl.Enabled = false;
                        chkOwnhsPropty.Enabled = false;
                        ddlSlfLoanType.Enabled = false;
                        chkBenftsSec80EE.Enabled = false;
                        txtTotPrinPaid.Enabled = false;
                        txtStampChgrDt.Enabled = false;
                        txtValPropty.Enabled = false;
                        txtCaptSqFt.Enabled = false;
                        txtPurofHsLoan.Enabled = false;
                        txtITSlfCity.Enabled = false;
                        txtITSlfState.Enabled = false;
                        txtAddrPropty.Enabled = false;
                        TXTCOMMENTS.Enabled = false;


                        ITboList = ITblObj.Load_PreEpInCm_cont(ID, 2);

                        DataTable dt = dtBorw();
                        for (int i = 0; i < ITboList.Count; i++)
                        {
                            dt.Rows.Add(ITboList[i].RID, ITboList[i].NAME, ITboList[i].PERCNT);
                        }
                        grdBorwDetails.DataSource = Session["BorwData"] = dt;
                        grdBorwDetails.DataBind();
                        grdBorwDetails.Columns[3].Visible = false;

                        btnUpdate4.Visible = btnUpdate4.Enabled = false;
                        BtnEDIT4.Visible = BtnEDIT4.Enabled = true;
                        BtnCancel.Visible = BtnCancel.Enabled = true;
                        BTNSubmitHousingOthers.Visible = BTNSubmitHousingOthers.Enabled = false;
                        break;
                    case "EDITR":
                        txtLendrName.Text = ITboList2[0].LENDNAME;
                        txtLendrAdd.Text = ITboList2[0].LENDADD;
                        txtLendrPAN.Text = ITboList2[0].LENDPAN;
                        txtLoanSacDt.Text = DateTime.Parse(ITboList2[0].LNSAC_DATE.ToString()).ToString("dd/MM/yyyy");
                        txtLoanSacAmt.Text = ITboList[0].INT24.ToString();
                        txtStampDuty.Text = ITboList[0].OTH24.ToString();
                        txtTotInsPaid.Text = ITboList[0].CPGLN.ToString();
                        txtTotIns.Text = ITboList[0].CPGSS.ToString();
                        txtTotPrincpl.Text = ITboList[0].INTRS.ToString();
                        chkOwnhsPropty.Checked = ITboList[0].LETVL.ToString() == "0.00" ? false : true;
                        ddlSlfLoanType.SelectedValue = ITboList[0].BSPFT.ToString();
                        chkBenftsSec80EE.Checked = ITboList[0].DVDND.ToString() == "0.00" ? false : true;
                        txtTotPrinPaid.Text = ITboList[0].CPGLS.ToString();
                        txtStampChgrDt.Text = DateTime.Parse(ITboList2[0].STMPCHR_DATE.ToString()).ToString("dd/MM/yyyy");
                        txtValPropty.Text = ITboList[0].REP24.ToString();
                        txtCaptSqFt.Text = ITboList[0].TDSAT.ToString();
                        txtPurofHsLoan.Text = ITboList2[0].PUPSHSLN;
                        txtITSlfCity.Text = ITboList2[0].City;
                        txtITSlfState.Text = ITboList2[0].State;
                        txtAddrPropty.Text = ITboList2[0].ADDPROPTY;
                        TXTCOMMENTS.Text = ITboList[0].EMPCOMMENTS.ToString();
                        txtBorwName.Enabled = true;
                        txtBorwPerct.Enabled = true;
                        lnkborAdd.Enabled = true;
                        txtLendrName.Enabled = true;
                        txtLendrAdd.Enabled = true;
                        txtLendrPAN.Enabled = true;
                        txtLoanSacDt.Enabled = true;
                        txtLoanSacAmt.Enabled = true;
                        txtStampDuty.Enabled = true;
                        txtTotInsPaid.Enabled = true;
                        //txtTotIns.Enabled = true;
                        //txtTotPrincpl.Enabled = true;
                        chkOwnhsPropty.Enabled = true;
                        ddlSlfLoanType.Enabled = true;
                        chkBenftsSec80EE.Enabled = true;
                        txtTotPrinPaid.Enabled = true;
                        txtStampChgrDt.Enabled = true;
                        txtValPropty.Enabled = true;
                        txtCaptSqFt.Enabled = true;
                        txtPurofHsLoan.Enabled = true;
                        txtITSlfCity.Enabled = true;
                        txtITSlfState.Enabled = true;
                        txtAddrPropty.Enabled = true;
                        TXTCOMMENTS.Enabled = true;
                        btnUpdate4.Visible = btnUpdate4.Enabled = true;
                        BtnEDIT4.Visible = BtnEDIT4.Enabled = false;
                        BtnCancel.Visible = BtnCancel.Enabled = true;
                        BTNSubmitHousingOthers.Visible = BTNSubmitHousingOthers.Enabled = false;

                        ITboList = ITblObj.Load_PreEpInCm_cont(ID, 2);

                        DataTable dt1 = dtBorw();
                        for (int i = 0; i < ITboList.Count; i++)
                        {
                            dt1.Rows.Add(ITboList[i].RID, ITboList[i].NAME, ITboList[i].PERCNT);
                        }
                        grdBorwDetails.DataSource = Session["BorwData"] = dt1;
                        grdBorwDetails.DataBind();
                        grdBorwDetails.Columns[3].Visible = true;
                        break;
                    case "DELETER":
                        ITboObj.ID = ID;
                        ITblObj.Create_ITHousingOthers_cont(ITboObj, 6, ref sts);
                        Load_grdSelfOccDetails();
                        MsgCls("IT Other Sources created successfully !", LblMsg, Color.Green);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('IT Other Sources Deleted successfully !')", true);
                        break;
                }
            }
            catch (Exception ex) { }
        }

        protected void grdSelfOccDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    ITbl itobjBL = new ITbl();
                    int ID = int.Parse(grdSelfOccDetails.DataKeys[e.Row.RowIndex].Values[0].ToString().Trim());
                    List<ITbo> ITboList = new List<ITbo>();
                    ITboList = itobjBL.Load_PreEpInCm_cont(ID, 1);
                    e.Row.Cells[1].Text = ITboList[0].LENDNAME;
                    e.Row.Cells[3].Text = ITboList[0].LENDPAN;
                    e.Row.Cells[2].Text = ITboList[0].LENDADD;
                    e.Row.Cells[4].Text = ITboList[0].ADDPROPTY;
                    e.Row.Cells[5].Text = ITboList[0].State;
                    e.Row.Cells[6].Text = ITboList[0].City;
                    e.Row.Cells[8].Text = ITboList[0].PUPSHSLN;
                    e.Row.Cells[7].Text = DateTime.Parse(ITboList[0].LNSAC_DATE.ToString()).ToString("dd-MMM-yyyy");
                    e.Row.Cells[13].Text = DateTime.Parse(ITboList[0].STMPCHR_DATE.ToString()).ToString("dd-MMM-yyyy");

                    //grdSelfOccDetails.DataSource = ITboList;
                    //grdSelfOccDetails.DataBind();
                    //ITboList = itobjBL.Load_PreEpInCm_cont(ID, 2);
                    //grdBorwDetails.DataSource = Session["BorwData"] = ITboList;
                    //grdBorwDetails.DataBind();
                }
            }
            catch (Exception ex) { }
        }

        protected void txtITPreEmprNm_TextChanged(object sender, EventArgs e)
        {
            if (txtITPreEmprNm.Text != "")
            {
                RFVtxtITPreEmprPAN.Enabled = true;
                RFVtxtITPreEmprTAN.Enabled = true;
                btnITPreEmprSub.Enabled = true;
            }
        }

        protected void txtLendrName_TextChanged(object sender, EventArgs e)
        {

            BTNSubmitHousingOthers.Visible = BTNSubmitHousingOthers.Enabled = txtLendrName.Text != "" ? true : false;

        }

        protected void txtMunicipaltax_TextChanged(object sender, EventArgs e)
        {
            Callet();
        }

        protected void txtTotintPort_TextChanged(object sender, EventArgs e)
        {
            Callet();

        }

        protected void Calvl()
        {
            decimal PERCNT = 100;
            if (Session["BorwData"] != null)
            {
                DataTable dt = (DataTable)Session["BorwData"];
                if (dt.Rows.Count > 0)//newly added
                {
                    for (int i = 0; i < 1; i++)
                    {
                        PERCNT = decimal.Parse(dt.Rows[i]["PERCNT"].ToString().Trim());
                    }
                }
            }
            PERCNT = PERCNT / 100;
            txtTotPrincpl.Text = (decimal.Parse(txtTotPrinPaid.Text == "" ? "0.0" : txtTotPrinPaid.Text) * PERCNT).ToString();
            txtTotIns.Text = (decimal.Parse(txtTotInsPaid.Text == "" ? "0.0" : txtTotInsPaid.Text) * PERCNT).ToString();
        }

        protected void txtTotPrinPaid_TextChanged(object sender, EventArgs e)
        {
            Calvl();
        }

        protected void txtTotInsPaid_TextChanged(object sender, EventArgs e)
        {
            Calvl();
        }

        protected void grdLetout_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                ITbl ITblObj = new ITbl();
                ITbo ITboObj = new ITbo();
                List<ITbo> ITboList = new List<ITbo>();
                List<ITbo> ITboObj1 = new List<ITbo>();
                List<ITbo> ITboList2 = new List<ITbo>();
                bool? sts = false;
                int ID = int.Parse(grdLetout.DataKeys[int.Parse(e.CommandArgument.ToString())]["ID"].ToString());
                ViewState["LetID"] = ID;

                ITboObj1 = ITblObj.Load_HousingOthersDetails(ID, "1", User.Identity.Name.Trim(), "3");
                int nxtid = int.Parse(ITboObj1[0].EMPCOMMENTS2);
                ITboList = ITblObj.Load_HousingOthersDetails(nxtid, "1", User.Identity.Name.Trim(), "3");
                ViewState["LetIDNxt"] = nxtid;

                ITboList2 = ITblObj.Load_PreEpInCm_cont(nxtid, 1);
                //int index = Convert.ToInt32(e.CommandArgument.ToString());
                switch (e.CommandName.ToUpper())
                {
                    case "VIEWR":

                        TXT_DEDINT.Text = ITboObj1[0].INT24.ToString().Trim();
                        TXT_FNLLETVALUE.Text = ITboObj1[0].LETVL.ToString().Trim();
                        txtMunicipaltax.Text = ITboObj1[0].REP24.ToString().Trim();
                        TXTDECOTHERS.Text = ITboObj1[0].OTH24.ToString().Trim();
                        TXTTDS.Text = ITboObj1[0].TDSOT.ToString().Trim();
                        ddlLoanType.SelectedValue = ITboObj1[0].BSPFT.ToString().Trim();
                        txtTotintPort.Text = ITboObj1[0].CPGLN.ToString().Trim();
                        txtIncm_loss.Text = ITboObj1[0].CPGLS.ToString().Trim();
                        TXTCOMMENTS.Text = ITboObj1[0].EMPCOMMENTS.ToString().Trim();
                        TXTCOMMENTS.Enabled = false;
                        txtMunicipaltax.Enabled = false;
                        //  RB_PropTyp.Enabled = false;
                        //TXT_DedIntr.Enabled = false;
                        TXT_DEDINT.Enabled = false;
                        TXT_FNLLETVALUE.Enabled = false;
                        Lbl_DEDREPAIR.Enabled = false;
                        TXTDECOTHERS.Enabled = false;
                        TXTTDS.Enabled = false;
                        ddlLoanType.Enabled = false;
                        txtTotintPort.Enabled = false;
                        txtIncm_loss.Enabled = false;


                        txtLendrName.Text = ITboList2[0].LENDNAME;
                        txtLendrAdd.Text = ITboList2[0].LENDADD;
                        txtLendrPAN.Text = ITboList2[0].LENDPAN;
                        txtLoanSacDt.Text = DateTime.Parse(ITboList2[0].LNSAC_DATE.ToString()).ToString("dd/MM/yyyy") == "1900-01-01" ? "" : DateTime.Parse(ITboList2[0].LNSAC_DATE.ToString()).ToString("dd/MM/yyyy");
                        txtLoanSacAmt.Text = ITboList[0].INT24.ToString();
                        txtStampDuty.Text = ITboList[0].OTH24.ToString();
                        txtTotInsPaid.Text = ITboList[0].CPGLN.ToString();
                        txtTotIns.Text = ITboList[0].CPGSS.ToString();
                        txtTotPrincpl.Text = ITboList[0].INTRS.ToString();
                        chkOwnhsPropty.Checked = ITboList[0].LETVL.ToString() == "0.00" ? false : true;
                        ddlSlfLoanType.SelectedValue = ITboList[0].BSPFT.ToString();
                        chkBenftsSec80EE.Checked = ITboList[0].DVDND.ToString() == "0.00" ? false : true;
                        txtTotPrinPaid.Text = ITboList[0].CPGLS.ToString();
                        txtStampChgrDt.Text = DateTime.Parse(ITboList2[0].STMPCHR_DATE.ToString()).ToString("dd/MM/yyyy") == "1900-01-01" ? "" : DateTime.Parse(ITboList2[0].STMPCHR_DATE.ToString()).ToString("dd/MM/yyyy");
                        txtValPropty.Text = ITboList[0].REP24.ToString();
                        txtCaptSqFt.Text = ITboList[0].TDSAT.ToString();
                        txtPurofHsLoan.Text = ITboList2[0].PUPSHSLN;
                        txtITSlfCity.Text = ITboList2[0].City;
                        txtITSlfState.Text = ITboList2[0].State;
                        txtAddrPropty.Text = ITboList2[0].ADDPROPTY;
                        TXTCOMMENTS.Text = ITboList[0].EMPCOMMENTS.ToString();
                        txtBorwName.Enabled = false;
                        txtBorwPerct.Enabled = false;
                        lnkborAdd.Enabled = false;
                        txtLendrName.Enabled = false;
                        txtLendrAdd.Enabled = false;
                        txtLendrPAN.Enabled = false;
                        txtLoanSacDt.Enabled = false;
                        txtLoanSacAmt.Enabled = false;
                        txtStampDuty.Enabled = false;
                        txtTotInsPaid.Enabled = false;
                        txtTotIns.Enabled = false;
                        txtTotPrincpl.Enabled = false;
                        chkOwnhsPropty.Enabled = false;
                        ddlSlfLoanType.Enabled = false;
                        chkBenftsSec80EE.Enabled = false;
                        txtTotPrinPaid.Enabled = false;
                        txtStampChgrDt.Enabled = false;
                        txtValPropty.Enabled = false;
                        txtCaptSqFt.Enabled = false;
                        txtPurofHsLoan.Enabled = false;
                        txtITSlfCity.Enabled = false;
                        txtITSlfState.Enabled = false;
                        txtAddrPropty.Enabled = false;
                        TXTCOMMENTS.Enabled = false;


                        ITboList = ITblObj.Load_PreEpInCm_cont(nxtid, 2);

                        DataTable dt = dtBorw();
                        for (int i = 0; i < ITboList.Count; i++)
                        {
                            dt.Rows.Add(ITboList[i].RID, ITboList[i].NAME, ITboList[i].PERCNT);
                        }
                        grdBorwDetails.DataSource = Session["BorwData"] = dt;
                        grdBorwDetails.DataBind();
                        grdBorwDetails.Columns[3].Visible = false;

                        btnUpdate4.Visible = btnUpdate4.Enabled = false;
                        BtnEDIT4.Visible = BtnEDIT4.Enabled = true;
                        BtnCancel.Visible = BtnCancel.Enabled = true;
                        BTNSubmitHousingOthers.Visible = BTNSubmitHousingOthers.Enabled = false;
                        break;

                    case "EDITR":
                        TXT_DEDINT.Text = ITboObj1[0].INT24.ToString().Trim();
                        TXT_FNLLETVALUE.Text = ITboObj1[0].LETVL.ToString().Trim();
                        txtMunicipaltax.Text = ITboObj1[0].REP24.ToString().Trim();
                        TXTDECOTHERS.Text = ITboObj1[0].OTH24.ToString().Trim();
                        TXTTDS.Text = ITboObj1[0].TDSOT.ToString().Trim();
                        ddlLoanType.SelectedValue = ITboObj1[0].BSPFT.ToString().Trim();
                        txtTotintPort.Text = ITboObj1[0].CPGLN.ToString().Trim();
                        txtIncm_loss.Text = ITboObj1[0].CPGLS.ToString().Trim();
                        TXTCOMMENTS.Text = ITboObj1[0].EMPCOMMENTS.ToString().Trim();

                        //  RB_PropTyp.Enabled = false;
                        //TXT_DedIntr.Enabled = false;
                        TXT_DEDINT.Enabled = false;
                        TXTDECOTHERS.Enabled = false;
                        txtIncm_loss.Enabled = false;

                        TXT_FNLLETVALUE.Enabled = true;
                        Lbl_DEDREPAIR.Enabled = false;
                        TXTCOMMENTS.Enabled = true;
                        txtMunicipaltax.Enabled = true;
                        TXTTDS.Enabled = true;
                        ddlLoanType.Enabled = true;
                        txtTotintPort.Enabled = true;



                        txtLendrName.Text = ITboList2[0].LENDNAME;
                        txtLendrAdd.Text = ITboList2[0].LENDADD;
                        txtLendrPAN.Text = ITboList2[0].LENDPAN;
                        txtLoanSacDt.Text = DateTime.Parse(ITboList2[0].LNSAC_DATE.ToString()).ToString("dd/MM/yyyy");
                        txtLoanSacAmt.Text = ITboList[0].INT24.ToString();
                        txtStampDuty.Text = ITboList[0].OTH24.ToString();
                        txtTotInsPaid.Text = ITboList[0].CPGLN.ToString();
                        txtTotIns.Text = ITboList[0].CPGSS.ToString();
                        txtTotPrincpl.Text = ITboList[0].INTRS.ToString();
                        chkOwnhsPropty.Checked = ITboList[0].LETVL.ToString() == "0.00" ? false : true;
                        ddlSlfLoanType.SelectedValue = ITboList[0].BSPFT.ToString();
                        chkBenftsSec80EE.Checked = ITboList[0].DVDND.ToString() == "0.00" ? false : true;
                        txtTotPrinPaid.Text = ITboList[0].CPGLS.ToString();
                        txtStampChgrDt.Text = DateTime.Parse(ITboList2[0].STMPCHR_DATE.ToString()).ToString("dd/MM/yyyy");
                        txtValPropty.Text = ITboList[0].REP24.ToString();
                        txtCaptSqFt.Text = ITboList[0].TDSAT.ToString();
                        txtPurofHsLoan.Text = ITboList2[0].PUPSHSLN;
                        txtITSlfCity.Text = ITboList2[0].City;
                        txtITSlfState.Text = ITboList2[0].State;
                        txtAddrPropty.Text = ITboList2[0].ADDPROPTY;
                        TXTCOMMENTS.Text = ITboList[0].EMPCOMMENTS.ToString();
                        txtBorwName.Enabled = true;
                        txtBorwPerct.Enabled = true;
                        lnkborAdd.Enabled = true;
                        txtLendrName.Enabled = true;
                        txtLendrAdd.Enabled = true;
                        txtLendrPAN.Enabled = true;
                        txtLoanSacDt.Enabled = true;
                        txtLoanSacAmt.Enabled = true;
                        txtStampDuty.Enabled = true;
                        txtTotInsPaid.Enabled = true;
                        //txtTotIns.Enabled = true;
                        //txtTotPrincpl.Enabled = true;
                        chkOwnhsPropty.Enabled = true;
                        ddlSlfLoanType.Enabled = true;
                        chkBenftsSec80EE.Enabled = true;
                        txtTotPrinPaid.Enabled = true;
                        txtStampChgrDt.Enabled = true;
                        txtValPropty.Enabled = true;
                        txtCaptSqFt.Enabled = true;
                        txtPurofHsLoan.Enabled = true;
                        txtITSlfCity.Enabled = true;
                        txtITSlfState.Enabled = true;
                        txtAddrPropty.Enabled = true;
                        TXTCOMMENTS.Enabled = true;
                        btnUpdate4.Visible = btnUpdate4.Enabled = true;
                        BtnEDIT4.Visible = BtnEDIT4.Enabled = false;
                        BtnCancel.Visible = BtnCancel.Enabled = true;
                        BTNSubmitHousingOthers.Visible = BTNSubmitHousingOthers.Enabled = false;

                        ITboList = ITblObj.Load_PreEpInCm_cont(nxtid, 2);

                        DataTable dt1 = dtBorw();
                        for (int i = 0; i < ITboList.Count; i++)
                        {
                            dt1.Rows.Add(ITboList[i].RID, ITboList[i].NAME, ITboList[i].PERCNT);
                        }
                        grdBorwDetails.DataSource = Session["BorwData"] = dt1;
                        grdBorwDetails.DataBind();
                        grdBorwDetails.Columns[3].Visible = true;
                        break;

                    case "DELETER":
                        ITboObj.ID = ID;
                        ITblObj.Create_ITHousingOthers_cont(ITboObj, 6, ref sts);
                        Load_letout();
                        MsgCls("IT Other Sources created successfully !", LblMsg, Color.Green);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('IT Other Sources Deleted successfully !')", true);

                        break;
                }
            }
            catch (Exception ex) { }
        }

        protected void grdLetout_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    ITbl itobjBL = new ITbl();
                    int ID = int.Parse(grdLetout.DataKeys[e.Row.RowIndex].Values[1].ToString().Trim());
                    List<ITbo> ITboList = new List<ITbo>();
                    ITboList = itobjBL.Load_PreEpInCm_cont(ID, 1);
                    e.Row.Cells[1].Text = ITboList[0].LENDNAME;
                    e.Row.Cells[3].Text = ITboList[0].LENDPAN;
                    e.Row.Cells[2].Text = ITboList[0].LENDADD;
                    e.Row.Cells[4].Text = ITboList[0].ADDPROPTY;
                    e.Row.Cells[5].Text = ITboList[0].State;
                    e.Row.Cells[6].Text = ITboList[0].City;
                    e.Row.Cells[8].Text = ITboList[0].PUPSHSLN;
                    e.Row.Cells[7].Text = DateTime.Parse(ITboList[0].LNSAC_DATE.ToString()).ToString("dd-MMM-yyyy");
                    e.Row.Cells[13].Text = DateTime.Parse(ITboList[0].STMPCHR_DATE.ToString()).ToString("dd-MMM-yyyy");

                    //grdSelfOccDetails.DataSource = ITboList;
                    //grdSelfOccDetails.DataBind();
                    //ITboList = itobjBL.Load_PreEpInCm_cont(ID, 2);
                    //grdBorwDetails.DataSource = Session["BorwData"] = ITboList;
                    //grdBorwDetails.DataBind();
                }
            }
            catch (Exception ex) { }
        }

        protected void Load_letout()
        {
            try
            {
                ITbl itobjBL = new ITbl();
                List<ITbo> ITboList = new List<ITbo>();
                ITboList = itobjBL.Load_HousingOthersDetails(0, "1", User.Identity.Name.Trim(), RB_PropTyp.SelectedValue.Trim());
                grdLetout.DataSource = ITboList;
                grdLetout.DataBind();
            }
            catch (Exception ex) { }
        }

        protected void TXT_FNLLETVALUE_TextChanged1(object sender, EventArgs e)
        {
            Callet();
        }

        protected void Callet()
        {
            TXT_DEDINT.Text = (decimal.Parse(TXT_FNLLETVALUE.Text == "" ? "0.0" : TXT_FNLLETVALUE.Text) - decimal.Parse(txtMunicipaltax.Text == "" ? "0.0" : txtMunicipaltax.Text)).ToString();
            TXTDECOTHERS.Text = (decimal.Parse(TXT_DEDINT.Text) * decimal.Parse("0.3")).ToString();
            txtIncm_loss.Text = (decimal.Parse(txtTotintPort.Text) - (decimal.Parse(TXT_DEDINT.Text) - decimal.Parse(TXTDECOTHERS.Text))).ToString();
        }

        protected void grdHistySelf_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    ITbl itobjBL = new ITbl();
                    int ID = int.Parse(grdHistySelf.DataKeys[e.Row.RowIndex].Values[0].ToString().Trim());
                    List<ITbo> ITboList = new List<ITbo>();
                    ITboList = itobjBL.Load_PreEpInCm_cont(ID, 1);
                    e.Row.Cells[1].Text = ITboList[0].LENDNAME;
                    e.Row.Cells[3].Text = ITboList[0].LENDPAN;
                    e.Row.Cells[2].Text = ITboList[0].LENDADD;
                    e.Row.Cells[4].Text = ITboList[0].ADDPROPTY;
                    e.Row.Cells[5].Text = ITboList[0].State;
                    e.Row.Cells[6].Text = ITboList[0].City;
                    e.Row.Cells[8].Text = ITboList[0].PUPSHSLN;
                    e.Row.Cells[7].Text = DateTime.Parse(ITboList[0].LNSAC_DATE.ToString()).ToString("dd-MMM-yyyy");
                    e.Row.Cells[13].Text = DateTime.Parse(ITboList[0].STMPCHR_DATE.ToString()).ToString("dd-MMM-yyyy");

                    //grdSelfOccDetails.DataSource = ITboList;
                    //grdSelfOccDetails.DataBind();
                    //ITboList = itobjBL.Load_PreEpInCm_cont(ID, 2);
                    //grdBorwDetails.DataSource = Session["BorwData"] = ITboList;
                    //grdBorwDetails.DataBind();
                }
            }
            catch (Exception ex) { }
        }

        protected void btnSubmitAll_Click(object sender, EventArgs e)
        {
            try
            {
                bool? sts = false;
                string en = "", em = "";
                ITbl ITblObj = new ITbl();
                ITbo ITboObj = new ITbo();
                ITdalDataContext DAL = new ITdalDataContext();
                ITboObj.PERNR = User.Identity.Name;
                ITboObj.Flag = 1;
                ITboObj.ID = 0;
                ITblObj.SubmitAll(ITboObj, ref sts, ref en, ref em);
                if (sts == true)
                {
                    //bool? sts1 = false;
                    string fpout = "";
                    string fpath = "";
                    if (FU_ItAllFiles.HasFile)
                    {
                        fpath = FU_ItAllFiles.HasFile ? "~/ITDoc/" + User.Identity.Name + "-" + DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss") + Path.GetExtension(FU_ItAllFiles.FileName) : "";
                        FU_ItAllFiles.SaveAs(Server.MapPath("~/ITDoc/" + User.Identity.Name + "-" + DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss")) + Path.GetExtension(FU_ItAllFiles.FileName));
                    }
                    DAL.usp_IT_set_get_submitionFile(User.Identity.Name.Trim(), Convert.ToDateTime("1900-01-01"), Convert.ToDateTime("1900-01-01"), fpath, 1, ref sts, ref fpout);



                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('IT Details Submitted Successfully');", true);
                    MsgCls("IT Details Submitted Successfully", lblMessageBoard, System.Drawing.Color.Red);
                    SendMail_SubAll(em, en);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('You have not saved any Income Tax request yet!!');", true);
                    MsgCls("You have not saved any Income Tax request yet!!", lblMessageBoard, System.Drawing.Color.Red);
                }
            }
            catch (Exception ex) { }
        }


        private void SendMail_SubAll(string EMP_Email, string EMP_Name)
        {
            try
            {
                string strSubject = string.Empty;
                string RecipientsString = string.Empty;
                string strPernr_Mail = string.Empty;
                //string APPROVED_BY1 = "";
                //string Approver_Name = "";
                //string Approver_Email = "";



                ITdalDataContext objcontext = new ITdalDataContext();




                strSubject = "Income Tax Request details has been submitted by " + EMP_Name + "  |  " + User.Identity.Name; ////+ " and is pending for the Approval.";




                //    //Preparing the mail body--------------------------------------------------
                string body = "<b>Income Tax Request details has been submitted by " + EMP_Name + "  |  " + User.Identity.Name;//// +" and is pending for the Approval.<br/><br/></b>";
                //body += "<b>Income Tax ID  :  " + ITHID + "</b><br/><br/>";
                //body += "<b>Income Tax Type :  " + subtyp + "</b><br/><br/>";



                //    //End of preparing the mail body-------------------------------------------
                if (EMP_Email != "")
                {
                    iEmpPowerMaster_Load.masterbl.DispatchMail(EMP_Email, User.Identity.Name, strSubject, "", body);
                    lblMessageBoard.ForeColor = System.Drawing.Color.Green;
                    lblMessageBoard.Text = "Income Tax Request submitted successfully";
                }
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        }

        protected void btnHousingNext_Click(object sender, EventArgs e)
        {
            if (gvHRA.Rows.Count > 0)
            {
                if (DateTime.Parse(Session["DOJ"].ToString()) > DateTime.Parse(ViewState["mindate"].ToString()) &&
                        DateTime.Parse(Session["DOJ"].ToString()) < DateTime.Parse(ViewState["maxdate"].ToString()))
                    LoadTab6();
                else
                    loadTab4();
            }
            else
            {
                string msg = "Please Click on Add button to Save before proceeding to Save & Next";
                LblLockSts3.Text = "Please Click on Add button to Save before proceeding to Save & Next";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + msg + "')", true);
            }
        }

        protected void btnView12b_Click(object sender, EventArgs e)
        {
            try
            {
                ITdalDataContext DAL = new ITdalDataContext();




                lblEmpName.Text = spnEmpName.InnerText = lblEmpName2.Text = spnNAme.InnerText = Session["EmployeeName"].ToString();
                lblDOJ.Text = DateTime.Parse(Session["DOJ"].ToString()).ToString("yyyy-MMM-dd");//
                lblEmpID.Text = User.Identity.Name.Trim();



                string BDT = "", EDT = "";
                foreach (var data in DAL.usp_IT_get_Emp_details_IT_12b(User.Identity.Name.Trim()))
                {
                    BDT = data.BDT.ToString(); EDT = data.EDT.ToString();
                    lblEmpPAN.Text = lblEMPPAN2.Text = data.ICNUM;
                    spnEmpDesg.InnerHtml = data.PLSXT;
                    lblCurntYear.Text = data.BDT.ToString() + " - " + data.EDT.ToString();
                }
                DataTable dt = new DataTable();
                dt.Columns.Add("Text", typeof(string));
                dt.Columns.Add("rent", typeof(decimal));
                dt.Columns.Add("Apprent", typeof(decimal));
                foreach (var item in DAL.usp_IT_get_12b_HosingDtls(User.Identity.Name.Trim()))
                {
                    dt.Rows.Add(item.text1, item.rent, item.Apprent);
                }
                decimal sum = 0, Appsum = 0;
                ltMonths.Text = "<table id='tabl3' style='width:100%'>";
                //string[] months = { "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ltMonths.Text += "<tr>" + dt.Rows[i]["Text"].ToString() + "</tr>";
                    sum += decimal.Parse(dt.Rows[i]["rent"].ToString());
                    Appsum += decimal.Parse(dt.Rows[i]["Apprent"].ToString());
                }
                lblrntAmt.Text = sum.ToString();
                lblHRA.Text = sum.ToString();//Newly added
                lblAppAmt.Text = Appsum.ToString();
                lblSt.Text = Appsum > 0 ? "Approved" : "Pending";
                //string[] months1 = { "Jan", "Feb", "Mar" };
                //for (int ji = 0; ji < 3; ji++)
                //{
                //    ltMonths.Text += "<tr><td>" + (ji + 10) + "</td><td class='text-center'>" + months1[ji] + " " + EDT + "</td><td></td><td></td><td></td><td></td><td></td></tr>";
                //}
                ltMonths.Text += "</table >";
                dt.Rows.Clear();
                ////ltOtherDetls.Text = "<table id='tabl4' style='width:100%'>";
                foreach (var item in DAL.usp_IT_get_12b_80C(User.Identity.Name.Trim()))
                {
                    dt.Rows.Add(item.Text1, 0, 0);
                }
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ltOtherDetls.Text += "<tr>" + dt.Rows[i]["Text"].ToString() + "</tr>";



                }
                ////ltOtherDetls.Text += "</table >";
                dt.Rows.Clear();
                ltHsdtls.Text = "";
                foreach (var item in DAL.usp_IT_get_12b_get_all(User.Identity.Name.Trim(), 1))
                {
                    dt.Rows.Add(item.Column1, 0, 0);
                }
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ltHsdtls.Text += "<tr>" + dt.Rows[i]["Text"].ToString() + "</tr>";
                }
                ltHsdtls.Text += "";
                dt.Rows.Clear();
                lt80c.Text = "";
                foreach (var item in DAL.usp_IT_get_12b_get_all(User.Identity.Name.Trim(), 2))
                {
                    dt.Rows.Add(item.Column1, 0, 0);
                }
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    lt80c.Text += "<tr>" + dt.Rows[i]["Text"].ToString() + "</tr>";
                }

                //-------------------

                string connectioString = ConfigurationManager.ConnectionStrings["iEmpPowerConnectionString"].ToString();
                SqlConnection con = new SqlConnection(connectioString);
                DataTable ds = new DataTable();

                decimal LTA = 0, DedIntBor = 0, DedUCVIA = 0;
                decimal SelfIntpayable = 0, LOIntpayable = 0;
                decimal Sec80CCC = 0, Sec80CCD = 0, OtherSecs = 0;
                string SelfltLenderName = string.Empty, LOltLenderName = string.Empty, SelfLenderAddress = string.Empty, LOLenderAddress = string.Empty, SelfLenderPAN = string.Empty, LOLenderPAN = string.Empty;
                try
                {
                    SqlCommand cmd = new SqlCommand("usp_IT_get_12b_details", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PERNR", User.Identity.Name);
                    cmd.Parameters.AddWithValue("@LTA", LTA);
                    cmd.Parameters["@LTA"].Direction = ParameterDirection.Output;
                    cmd.Parameters.AddWithValue("@DedIntBor", DedIntBor);
                    cmd.Parameters["@DedIntBor"].Direction = ParameterDirection.Output;
                    cmd.Parameters.AddWithValue("@DedUCVIA", DedUCVIA);
                    cmd.Parameters["@DedUCVIA"].Direction = ParameterDirection.Output;


                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 0;
                    con.Open();
                    cmd.ExecuteNonQuery();

                    lblLTA.Text = cmd.Parameters["@LTA"].Value.ToString();  //LTA.ToString();
                    lblDedIOB.Text = cmd.Parameters["@DedIntBor"].Value.ToString();   //DedIntBor.ToString();
                    lblDedUC6A.Text = cmd.Parameters["@DedUCVIA"].Value.ToString();   //DedUCVIA.ToString();
                    con.Close();



                    //------------------------------------
                    SqlCommand cmd1 = new SqlCommand("usp_IT_get_12b_Dedofinterestonborrowing", con);
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.Parameters.AddWithValue("@PERNR", User.Identity.Name);
                    cmd1.Parameters.AddWithValue("@SelfIntpayable", SelfIntpayable);
                    cmd1.Parameters["@SelfIntpayable"].Direction = ParameterDirection.Output;
                    cmd1.Parameters.AddWithValue("@LOIntpayable", LOIntpayable);
                    cmd1.Parameters["@LOIntpayable"].Direction = ParameterDirection.Output;
                    //cmd1.Parameters.AddWithValue("@SelfltLenderName", SelfltLenderName);
                    cmd1.Parameters.Add("@SelfltLenderName", SqlDbType.VarChar, 100);
                    cmd1.Parameters["@SelfltLenderName"].Direction = ParameterDirection.Output;

                    //cmd1.Parameters.AddWithValue("@LOltLenderName", LOltLenderName);
                    cmd1.Parameters.Add("@LOltLenderName", SqlDbType.VarChar, 100);
                    cmd1.Parameters["@LOltLenderName"].Direction = ParameterDirection.Output;
                    //cmd1.Parameters.AddWithValue("@SelfLenderAddress", SelfLenderAddress);
                    cmd1.Parameters.Add("@SelfLenderAddress", SqlDbType.VarChar, 100);
                    cmd1.Parameters["@SelfLenderAddress"].Direction = ParameterDirection.Output;
                    //cmd1.Parameters.AddWithValue("@LOLenderAddress", LOLenderAddress);
                    cmd1.Parameters.Add("@LOLenderAddress", SqlDbType.VarChar, 100);
                    cmd1.Parameters["@LOLenderAddress"].Direction = ParameterDirection.Output;
                    //cmd1.Parameters.AddWithValue("@SelfLenderPAN", SelfLenderPAN);
                    cmd1.Parameters.Add("@SelfLenderPAN", SqlDbType.VarChar, 100);
                    cmd1.Parameters["@SelfLenderPAN"].Direction = ParameterDirection.Output;
                    //cmd1.Parameters.AddWithValue("@LOLenderPAN", LOLenderPAN);
                    cmd1.Parameters.Add("@LOLenderPAN", SqlDbType.VarChar, 100);
                    cmd1.Parameters["@LOLenderPAN"].Direction = ParameterDirection.Output;

                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.CommandTimeout = 0;
                    con.Open();
                    cmd1.ExecuteNonQuery();

                    ltSelfIntpayable.Text = "Self-Occupied Interest: Rs. -  "+cmd1.Parameters["@SelfIntpayable"].Value.ToString();  //LTA.ToString();
                    ltLOIntpayable.Text = "Let-Out Interest: Rs: "+cmd1.Parameters["@LOIntpayable"].Value.ToString();   //DedIntBor.ToString();
                    ltSelfltLenderName.Text = "Self-Occupied:" + Convert.ToString(cmd1.Parameters["@SelfltLenderName"].Value); //cmd1.Parameters["@SelfltLenderName"].Value.ToString();   //DedUCVIA.ToString();
                    ltLOltLenderName.Text = "Let-Out:" + Convert.ToString(cmd1.Parameters["@LOltLenderName"].Value); //cmd1.Parameters["@LOltLenderName"].ToString();
                    ltSelfLenderAddress.Text = "Self-Occupied:" + Convert.ToString(cmd1.Parameters["@SelfLenderAddress"].Value); //cmd1.Parameters["@SelfLenderAddress"].Value.ToString();
                    ltLOLenderAddress.Text = "Let-Out:" + Convert.ToString(cmd1.Parameters["@LOLenderAddress"].Value); //cmd1.Parameters["@LOLenderAddress"].Value.ToString();
                    ltSelfLenderPAN.Text = "Self-Occupied:" + Convert.ToString(cmd1.Parameters["@SelfLenderPAN"].Value); //cmd1.Parameters["@SelfLenderPAN"].Value.ToString();
                    ltLOfLenderPAN.Text = "Let-Out:" + Convert.ToString(cmd1.Parameters["@LOLenderPAN"].Value); //cmd1.Parameters["@LOLenderPAN"].Value.ToString(); 


                    ltDedIOBSelfIntpayable.Text = "Self-Occupied Interest: Rs. -  " + cmd1.Parameters["@SelfIntpayable"].Value.ToString();  //LTA.ToString();
                    ltDedIOBLOIntpayable.Text = "Let-Out Interest: Rs: " + cmd1.Parameters["@LOIntpayable"].Value.ToString();   //DedIntBor.ToString();
                    ltDedIOBSelfltLenderName.Text = "Self-Occupied:" + Convert.ToString(cmd1.Parameters["@SelfltLenderName"].Value); //cmd1.Parameters["@SelfltLenderName"].Value.ToString();   //DedUCVIA.ToString();
                    ltDedIOBLOltLenderName.Text = "Let-Out:" + Convert.ToString(cmd1.Parameters["@LOltLenderName"].Value); //cmd1.Parameters["@LOltLenderName"].ToString();
                    ltDedIOBSelfLenderAddress.Text = "Self-Occupied:" + Convert.ToString(cmd1.Parameters["@SelfLenderAddress"].Value); //cmd1.Parameters["@SelfLenderAddress"].Value.ToString();
                    ltDedIOBLOLenderAddress.Text = "Let-Out:" + Convert.ToString(cmd1.Parameters["@LOLenderAddress"].Value); //cmd1.Parameters["@LOLenderAddress"].Value.ToString();
                    ltDedIOBSelfLenderPAN.Text = "Self-Occupied:" + Convert.ToString(cmd1.Parameters["@SelfLenderPAN"].Value); //cmd1.Parameters["@SelfLenderPAN"].Value.ToString();
                    ltDedIOBLOfLenderPAN.Text = "Let-Out:" + Convert.ToString(cmd1.Parameters["@LOLenderPAN"].Value); //cmd1.Parameters["@LOLenderPAN"].Value.ToString(); 
                    con.Close();
                    //------------------------------------
                    SqlCommand cmd2 = new SqlCommand("usp_IT_GetSec80_12BB_ChapterVIA", con);
                    cmd2.CommandType = CommandType.StoredProcedure;
                    cmd2.Parameters.AddWithValue("@PERNR", User.Identity.Name);
                    cmd2.Parameters.AddWithValue("@Sec80CCC", Sec80CCC);
                    cmd2.Parameters["@Sec80CCC"].Direction = ParameterDirection.Output;
                    cmd2.Parameters.AddWithValue("@Sec80CCD", Sec80CCD);
                    cmd2.Parameters["@Sec80CCD"].Direction = ParameterDirection.Output;
                    cmd2.Parameters.AddWithValue("@OtherSecs", OtherSecs);
                    cmd2.Parameters["@OtherSecs"].Direction = ParameterDirection.Output;


                    cmd2.CommandType = CommandType.StoredProcedure;
                    cmd2.CommandTimeout = 0;
                    con.Open();
                    cmd2.ExecuteNonQuery();

                    lblSec80CCC.Text = cmd2.Parameters["@Sec80CCC"].Value.ToString();  //LTA.ToString();
                    lblSec80CCD.Text = cmd2.Parameters["@Sec80CCD"].Value.ToString();   //DedIntBor.ToString();
                    lblOtherSecs.Text = cmd2.Parameters["@OtherSecs"].Value.ToString();   //DedUCVIA.ToString();


                    decimal S80C = Convert.ToDecimal(string.IsNullOrEmpty(lblSection80C.Text) ? "0.0" : lblSection80C.Text);
                    decimal S80CCC = Convert.ToDecimal(string.IsNullOrEmpty(lblSec80CCC.Text)? "0.0" : lblSec80CCC.Text); 
                    decimal S80CCD = Convert.ToDecimal(string.IsNullOrEmpty(lblSec80CCD.Text)? "0.0" : lblSec80CCD.Text); 
                    decimal OSecs = Convert.ToDecimal(string.IsNullOrEmpty(lblOtherSecs.Text)? "0.0" : lblOtherSecs.Text);

                    lblSec80CCCCCCD.Text = (S80C + S80CCC + S80CCD).ToString();
                    lblDedUC6A.Text = (S80C + S80CCC + S80CCD + OSecs).ToString();

                    con.Close();
                    

                    //------------------------------------

                    decimal sumSec80C = 0;

                    DataTable dt2 = new DataTable();
                    dt2.Columns.Add("Text", typeof(string));
                    dt2.Columns.Add("ActAmount", typeof(decimal));


                    foreach (var item in DAL.usp_IT_GetSec80C_12BB(User.Identity.Name.Trim()))
                    {
                        dt2.Rows.Add(item.text1, item.rent);
                    }

                    ltSec80C12BB.Text = "<table id='tabl6'>";

                    for (int i = 0; i < dt2.Rows.Count; i++)
                    {
                        //ltSec8012BB.Text += "<tr>" + dt1.Rows[i]["Text"].ToString() + "</tr>";
                        ltSec80C12BB.Text += "<tr><td>" + dt2.Rows[i]["Text"].ToString() + "</td><td>" + decimal.Parse(dt2.Rows[i]["ActAmount"].ToString()) + "</td></tr>";
                        sumSec80C += decimal.Parse(dt2.Rows[i]["ActAmount"].ToString());
                    }
                    ltSec80C12BB.Text += "</table >";
                    lblSection80C.Text = sumSec80C.ToString();


                    //------------------------------------------------Sec80C_12BB-Ends

                    //------------------------------------------------Sec80_12BB-Starts
                    DataTable dt1 = new DataTable();
                    dt1.Columns.Add("Text", typeof(string));
                    dt1.Columns.Add("ActAmount", typeof(decimal));

                    foreach (var item in DAL.usp_IT_GetSec80_12BB(User.Identity.Name.Trim()))
                    {
                        dt1.Rows.Add(item.text1, item.rent);
                    }

                    ltSec8012BB.Text = "<table id='tabl5'>";

                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        //ltSec8012BB.Text += "<tr>" + dt1.Rows[i]["Text"].ToString() + "</tr>";
                        ltSec8012BB.Text += "<tr><td>" + dt1.Rows[i]["Text"].ToString() + "</td><td>" + decimal.Parse(dt1.Rows[i]["ActAmount"].ToString()) + "</td></tr>";
                        // sum += decimal.Parse(dt1.Rows[i]["ActAmount"].ToString());
                    }
                    ltSec8012BB.Text += "</table >";
                    //------------------------------------------------Sec80_12BB-Ends

                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", ex.ToString(), true);
                }





                //-------------------
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "createPDF();", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", ex.ToString(), true);
            }
        }



        protected void lnkDwnlITFile_Click(object sender, EventArgs e)
        {
            bool? sts = false;



            string a = "", fpth = "";
            ITdalDataContext DAL = new ITdalDataContext();
            DAL.usp_IT_set_get_submitionFile(User.Identity.Name.Trim(), txtFromDateH.Text == "" ? Convert.ToDateTime("1900-01-01") : Convert.ToDateTime(txtFromDateH.Text), txtTodateH.Text == "" ? Convert.ToDateTime("1900-01-01") : Convert.ToDateTime(txtTodateH.Text), a, 2, ref sts, ref fpth);
            if (sts == true)
            {
                string filePath1 = fpth.ToString();
                //Response.ContentType = ContentType;
                Response.ContentType = "application/octet-stream";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath1));
                Response.WriteFile(filePath1);
                Response.End();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "No file found!", true);
            }
        }



        protected bool checkITFileExts()
        {
            bool? sts = false;
            string a = "", fpth = "";
            ITdalDataContext DAL = new ITdalDataContext();
            DAL.usp_IT_set_get_submitionFile(User.Identity.Name.Trim(), txtFromDateH.Text == "" ? Convert.ToDateTime("1900-01-01") : Convert.ToDateTime(txtFromDateH.Text), txtTodateH.Text == "" ? Convert.ToDateTime("1900-01-01") : Convert.ToDateTime(txtTodateH.Text), a, 2, ref sts, ref fpth);
            return sts == true ? true : false;
        }


    }
}