using iEmpPower.Old_App_Code.iEmpPowerBL.IT;
using iEmpPower.Old_App_Code.iEmpPowerBO.IT;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace iEmpPower.UI.IT
{
    public partial class IT_EmpViewHistory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // GetFinancialDates();
                Tab1.CssClass = "Clicked";
                Tab2.CssClass = "Initial";
                Tab3.CssClass = "Initial";
                Tab4.CssClass = "Initial";
                MainView.ActiveViewIndex = 0;
                LoadSec80();
                GridControls();
                HFTabID.Value = "1";
                ddlSeachSelect.SelectedValue = "0";
                txtsearch.Text = string.Empty;
                txtFromDate.Text = string.Empty;
                txtTodate.Text = string.Empty;
                MsgCls("", lblMessageBoard, Color.Transparent);
                MsgCls("", LblSec80c, Color.Transparent);
                MsgCls("", LblHousing, Color.Transparent);
                MsgCls("", LblOthers, Color.Transparent);
                viewcheckSec80.Value = "NO";
            }
        }

        public void GridControls()
        {
            try
            {
                GVITSec80.Visible = false;
                GVITSec80C.Visible = false;
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
        protected void Tab1_Click(object sender, EventArgs e)
        {
            ddlSeachSelect.SelectedValue = "0";
            txtsearch.Text = string.Empty;
            txtFromDate.Text = string.Empty;
            txtTodate.Text = string.Empty;
            MsgCls("", lblMessageBoard, Color.Transparent);
            MsgCls("", LblSec80c, Color.Transparent);
            MsgCls("", LblHousing, Color.Transparent);
            MsgCls("", LblOthers, Color.Transparent);
            Tab1.CssClass = "Clicked";
            Tab2.CssClass = "Initial";
            Tab3.CssClass = "Initial";
            Tab4.CssClass = "Initial";
            MainView.ActiveViewIndex = 0;
            LoadSec80();
            GridControls();
            HFTabID.Value = "1";
            viewcheckSec80.Value = "NO";
        }

        protected void Tab2_Click(object sender, EventArgs e)
        {
            ddlSeachSelect.SelectedValue = "0";
            txtsearch.Text = string.Empty;
            txtFromDate.Text = string.Empty;
            txtTodate.Text = string.Empty;
            MsgCls("", lblMessageBoard, Color.Transparent);
            MsgCls("", LblSec80c, Color.Transparent);
            MsgCls("", LblHousing, Color.Transparent);
            MsgCls("", LblOthers, Color.Transparent);
            Tab1.CssClass = "Initial";
            Tab2.CssClass = "Clicked";
            Tab3.CssClass = "Initial";
            Tab4.CssClass = "Initial";
            MainView.ActiveViewIndex = 1;
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

        protected void Tab3_Click(object sender, EventArgs e)
        {
            ddlSeachSelect.SelectedValue = "0";
            txtsearch.Text = string.Empty;
            txtFromDate.Text = string.Empty;
            txtTodate.Text = string.Empty;
            MsgCls("", lblMessageBoard, Color.Transparent);
            MsgCls("", LblSec80c, Color.Transparent);
            MsgCls("", LblHousing, Color.Transparent);
            MsgCls("", LblOthers, Color.Transparent);
            Tab1.CssClass = "Initial";
            Tab2.CssClass = "Initial";
            Tab3.CssClass = "Clicked";
            Tab4.CssClass = "Initial";
            MainView.ActiveViewIndex = 2;
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
        protected void Tab4_Click(object sender, EventArgs e)
        {
            ddlSeachSelect.SelectedValue = "0";
            txtsearch.Text = string.Empty;
            txtFromDate.Text = string.Empty;
            txtTodate.Text = string.Empty;
            MsgCls("", lblMessageBoard, Color.Transparent);
            MsgCls("", LblSec80c, Color.Transparent);
            MsgCls("", LblHousing, Color.Transparent);
            MsgCls("", LblOthers, Color.Transparent);
            Tab1.CssClass = "Initial";
            Tab2.CssClass = "Initial";
            Tab3.CssClass = "Initial";
            Tab4.CssClass = "Clicked";
            MainView.ActiveViewIndex = 3;
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

                        GVITSec80.Visible = true;
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
                        GVITSec80.DataSource = ITboObj;
                        GVITSec80.DataBind();

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

                        GVITSec80C.Visible = true;
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
                        GVITSec80C.DataSource = ITboObj;
                        GVITSec80C.DataBind();

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
                            GVOthers1.Visible = true;
                            GVOthers2.Visible = false;
                            ITboObj = ITblObj.Load_HousingOthersDetails(ID, ITtyp, User.Identity.Name,"");
                            GVOthers1.DataSource = ITboObj;
                            GVOthers1.DataBind();

                        }
                        else if (ITtyp.Trim() == "2")
                        {
                            GVOthers2.Visible = true;
                            GVOthers1.Visible = false;
                            ITboObj = ITblObj.Load_HousingOthersDetails(ID, ITtyp, User.Identity.Name,"");
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
                DateTime FromDate = DateTime.Parse(string.IsNullOrEmpty(txtFromDate.Text) ? "31/12/1899" : txtFromDate.Text);
                DateTime ToDate = DateTime.Parse(string.IsNullOrEmpty(txtTodate.Text) ? "31/12/1899" : txtTodate.Text);
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
                txtFromDate.Text = string.Empty;
                txtTodate.Text = string.Empty;
                MsgCls("", lblMessageBoard, Color.Transparent);
                MsgCls("", LblSec80c, Color.Transparent);
                MsgCls("", LblHousing, Color.Transparent);
                MsgCls("", LblOthers, Color.Transparent);
                txtsearch.Focus();

                if (int.Parse(HFTabID.Value.ToString().Trim()) == 1)
                {
                    Tab1.CssClass = "Clicked";
                    Tab2.CssClass = "Initial";
                    Tab3.CssClass = "Initial";
                    Tab4.CssClass = "Initial";
                    MainView.ActiveViewIndex = 0;
                    LoadSec80();
                    GridControls();
                    HFTabID.Value = "1";
                    viewcheckSec80.Value = "NO";
                    ExportbtnSec80.Visible = true;
                }

                else if (int.Parse(HFTabID.Value.ToString().Trim()) == 2)
                {
                    Tab1.CssClass = "Initial";
                    Tab2.CssClass = "Clicked";
                    Tab3.CssClass = "Initial";
                    Tab4.CssClass = "Initial";
                    MainView.ActiveViewIndex = 1;
                    LoadSec80C();
                    GridControls();
                    HFTabID.Value = "2";
                    viewcheckSec80C.Value = "NO";
                    ExportbtnSec80C.Visible = true;
                }
                else if (int.Parse(HFTabID.Value.ToString().Trim()) == 3)
                {
                    Tab1.CssClass = "Initial";
                    Tab2.CssClass = "Initial";
                    Tab3.CssClass = "Clicked";
                    Tab4.CssClass = "Initial";
                    MainView.ActiveViewIndex = 2;
                    LoadHousing();
                    GridControls();
                    HFTabID.Value = "3";
                    viewcheckHousing.Value = "NO";
                    ExportbtnHousing.Visible = true;
                }
                else if (int.Parse(HFTabID.Value.ToString().Trim()) == 4)
                {
                    Tab1.CssClass = "Initial";
                    Tab2.CssClass = "Initial";
                    Tab3.CssClass = "Initial";
                    Tab4.CssClass = "Clicked";
                    MainView.ActiveViewIndex = 3;
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
                txtFromDate.Text = string.Empty;
                txtTodate.Text = string.Empty;
                MsgCls("", lblMessageBoard, Color.Transparent);
                MsgCls("", LblSec80c, Color.Transparent);
                MsgCls("", LblHousing, Color.Transparent);
                MsgCls("", LblOthers, Color.Transparent);
                Tab1.CssClass = "Clicked";
                Tab2.CssClass = "Initial";
                Tab3.CssClass = "Initial";
                Tab4.CssClass = "Initial";
                MainView.ActiveViewIndex = 0;
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
                txtFromDate.Text = string.Empty;
                txtTodate.Text = string.Empty;
                MsgCls("", lblMessageBoard, Color.Transparent);
                MsgCls("", LblSec80c, Color.Transparent);
                MsgCls("", LblHousing, Color.Transparent);
                MsgCls("", LblOthers, Color.Transparent);
                Tab1.CssClass = "Initial";
                Tab2.CssClass = "Clicked";
                Tab3.CssClass = "Initial";
                Tab4.CssClass = "Initial";
                MainView.ActiveViewIndex = 1;
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
                txtFromDate.Text = string.Empty;
                txtTodate.Text = string.Empty;
                MsgCls("", lblMessageBoard, Color.Transparent);
                MsgCls("", LblSec80c, Color.Transparent);
                MsgCls("", LblHousing, Color.Transparent);
                MsgCls("", LblOthers, Color.Transparent);
                Tab1.CssClass = "Initial";
                Tab2.CssClass = "Initial";
                Tab3.CssClass = "Clicked";
                Tab4.CssClass = "Initial";
                MainView.ActiveViewIndex = 2;
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
                txtFromDate.Text = string.Empty;
                txtTodate.Text = string.Empty;
                MsgCls("", lblMessageBoard, Color.Transparent);
                MsgCls("", LblSec80c, Color.Transparent);
                MsgCls("", LblHousing, Color.Transparent);
                MsgCls("", LblOthers, Color.Transparent);
                Tab1.CssClass = "Initial";
                Tab2.CssClass = "Initial";
                Tab3.CssClass = "Initial";
                Tab4.CssClass = "Clicked";
                MainView.ActiveViewIndex = 3;
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
                txtFromDate.Text = string.Empty;
                txtTodate.Text = string.Empty;
                MsgCls("", lblMessageBoard, Color.Transparent);
                MsgCls("", LblSec80c, Color.Transparent);
                MsgCls("", LblHousing, Color.Transparent);
                MsgCls("", LblOthers, Color.Transparent);
                Tab1.CssClass = "Clicked";
                Tab2.CssClass = "Initial";
                Tab3.CssClass = "Initial";
                Tab4.CssClass = "Initial";
                MainView.ActiveViewIndex = 0;
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
                txtFromDate.Text = string.Empty;
                txtTodate.Text = string.Empty;
                MsgCls("", lblMessageBoard, Color.Transparent);
                MsgCls("", LblSec80c, Color.Transparent);
                MsgCls("", LblHousing, Color.Transparent);
                MsgCls("", LblOthers, Color.Transparent);
                Tab1.CssClass = "Initial";
                Tab2.CssClass = "Clicked";
                Tab3.CssClass = "Initial";
                Tab4.CssClass = "Initial";
                MainView.ActiveViewIndex = 1;
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
                txtFromDate.Text = string.Empty;
                txtTodate.Text = string.Empty;
                MsgCls("", lblMessageBoard, Color.Transparent);
                MsgCls("", LblSec80c, Color.Transparent);
                MsgCls("", LblHousing, Color.Transparent);
                MsgCls("", LblOthers, Color.Transparent);
                Tab1.CssClass = "Initial";
                Tab2.CssClass = "Initial";
                Tab3.CssClass = "Clicked";
                Tab4.CssClass = "Initial";
                MainView.ActiveViewIndex = 2;
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
                txtFromDate.Text = string.Empty;
                txtTodate.Text = string.Empty;
                MsgCls("", lblMessageBoard, Color.Transparent);
                MsgCls("", LblSec80c, Color.Transparent);
                MsgCls("", LblHousing, Color.Transparent);
                MsgCls("", LblOthers, Color.Transparent);
                Tab1.CssClass = "Initial";
                Tab2.CssClass = "Initial";
                Tab3.CssClass = "Initial";
                Tab4.CssClass = "Clicked";
                MainView.ActiveViewIndex = 3;
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
                    GVITSec80.RenderControl(htw);
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
                    GVITSec80.RenderControl(h_textw);
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
                    GVITSec80C.RenderControl(h_textw);
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
                    GVITSec80C.RenderControl(htw);
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
                        GVOthers1.RenderControl(htw);
                    }
                    else if (ViewState["ITOTHERSTYP"].ToString().Trim() == "2")
                    {
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
                        // GVOthers1.RenderControl(htw);
                        GVOthers1.RenderControl(h_textw);
                    }
                    else if (ViewState["ITOTHERSTYP"].ToString().Trim() == "2")
                    {
                        //GVOthers2.RenderControl(htw);
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

    }
}