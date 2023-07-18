using iEmpPower.Old_App_Code.iEmpPowerBL.IT;
using iEmpPower.Old_App_Code.iEmpPowerBO.IT;
using iEmpPower.Old_App_Code.iEmpPowerDAL.IT;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Ionic.Zip;
using System.Configuration;
using System.Data.SqlClient;

namespace iEmpPower.UI.IT
{
    public partial class IT_AppRej : System.Web.UI.Page
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
                //
                Tab1.CssClass = "Initial";
                Tab2.CssClass = "Clicked";
                Tab3.CssClass = "Initial";
                Tab4.CssClass = "Initial";
                MainView.ActiveViewIndex = 1;
                GridControls(); LoadSec80C();



                HFTabID.Value = "2";
                //
                ddlSeachSelect.SelectedValue = "0";
                txtsearch.Text = string.Empty;
                //txtFromDate.Text = string.Empty;
                //txtTodate.Text = string.Empty;
                MsgCls("", lblMessageBoard, Color.Transparent);
                MsgCls("", LblSec80c, Color.Transparent);
                MsgCls("", LblHousing, Color.Transparent);
                MsgCls("", LblOthers, Color.Transparent);
                tblsSec80.Visible = false;
                divbuttonSec80.Visible = false;
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
                grdBorwDetails.Visible = false;
                grdSelfOccDetails.Visible = false;
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
                ITboObj1 = ITblObj.Load_IT_HistoryForAppRej(flag);
                Session.Add("ITSec80GrdInfo", ITboObj1);

                if (ITboObj1 == null || ITboObj1.Count == 0)
                {
                    MsgCls("No Records Found !", lblMessageBoard, Color.Red);
                    GVSec80Header.Visible = false;
                    GVSec80Header.DataSource = null;
                    GVSec80Header.DataBind();
                    return;
                }
                else
                {
                    GVSec80Header.Visible = true;
                    GVSec80Header.DataSource = ITboObj1;
                    GVSec80Header.SelectedIndex = -1;
                    GVSec80Header.DataBind();
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
        protected void Tab1_Click(object sender, EventArgs e)
        {
            ddlSeachSelect.SelectedValue = "0";
            txtsearch.Text = string.Empty;
            MsgCls("", lblMessageBoard, Color.Transparent);
            MsgCls("", LblSec80c, Color.Transparent);
            MsgCls("", LblHousing, Color.Transparent);
            MsgCls("", LblOthers, Color.Transparent);
            Tab1.CssClass = "Clicked";
            Tab2.CssClass = "Initial";
            Tab3.CssClass = "Initial";
            Tab4.CssClass = "Initial";
            Tab5.CssClass = "Initial";
            MainView.ActiveViewIndex = 0;
            LoadSec80();
            GridControls();
            HFTabID.Value = "1";
            tblsSec80.Visible = false;
            divbuttonSec80.Visible = false;
        }

        protected void Tab2_Click(object sender, EventArgs e)
        {
            ddlSeachSelect.SelectedValue = "0";
            txtsearch.Text = string.Empty;
            //txtFromDate.Text = string.Empty;
            //txtTodate.Text = string.Empty;
            MsgCls("", lblMessageBoard, Color.Transparent);
            MsgCls("", LblSec80c, Color.Transparent);
            MsgCls("", LblHousing, Color.Transparent);
            MsgCls("", LblOthers, Color.Transparent);
            Tab1.CssClass = "Initial";
            Tab2.CssClass = "Clicked";
            Tab3.CssClass = "Initial";
            Tab4.CssClass = "Initial";
            Tab5.CssClass = "Initial";
            MainView.ActiveViewIndex = 1;
            LoadSec80C();
            GridControls();
            HFTabID.Value = "2";
            TblSec80C.Visible = false;
            DivBtnsSec80C.Visible = false;
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
                ITboObj1 = ITblObj.Load_IT_HistoryForAppRej(flag);
                Session.Add("ITSec80CGrdInfo", ITboObj1);

                if (ITboObj1 == null || ITboObj1.Count == 0)
                {
                    MsgCls("No Records Found !", LblSec80c, Color.Red);
                    GVSec80CHeader.Visible = false;
                    GVSec80CHeader.DataSource = null;
                    GVSec80CHeader.DataBind();
                    return;
                }
                else
                {
                    GVSec80CHeader.Visible = true;
                    GVSec80CHeader.DataSource = ITboObj1;
                    GVSec80CHeader.SelectedIndex = -1;
                    GVSec80CHeader.DataBind();
                }
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        }

        protected void Tab3_Click(object sender, EventArgs e)
        {
            ddlSeachSelect.SelectedValue = "0";
            txtsearch.Text = string.Empty;
            //txtFromDate.Text = string.Empty;
            //txtTodate.Text = string.Empty;
            MsgCls("", lblMessageBoard, Color.Transparent);
            MsgCls("", LblSec80c, Color.Transparent);
            MsgCls("", LblHousing, Color.Transparent);
            MsgCls("", LblOthers, Color.Transparent);
            Tab1.CssClass = "Initial";
            Tab2.CssClass = "Initial";
            Tab3.CssClass = "Clicked";
            Tab4.CssClass = "Initial";
            Tab5.CssClass = "Initial";
            MainView.ActiveViewIndex = 2;
            LoadHousing();
            GridControls();
            HFTabID.Value = "3";
            TblHousing.Visible = false;
            DivBtnsHousing.Visible = false;
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
                ITboObj1 = ITblObj.Load_IT_HistoryForAppRej(flag);
                Session.Add("ITHousingGrdInfo", ITboObj1);

                if (ITboObj1 == null || ITboObj1.Count == 0)
                {
                    MsgCls("No Records Found !", LblHousing, Color.Red);
                    GVHousingHeader.Visible = false;
                    GVHousingHeader.DataSource = null;
                    GVHousingHeader.DataBind();
                    return;
                }
                else
                {
                    GVHousingHeader.Visible = true;
                    GVHousingHeader.DataSource = ITboObj1;
                    GVHousingHeader.SelectedIndex = -1;
                    GVHousingHeader.DataBind();
                }
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        }
        protected void Tab4_Click(object sender, EventArgs e)
        {
            ddlSeachSelect.SelectedValue = "0";
            txtsearch.Text = string.Empty;
            //txtFromDate.Text = string.Empty;
            //txtTodate.Text = string.Empty;
            MsgCls("", lblMessageBoard, Color.Transparent);
            MsgCls("", LblSec80c, Color.Transparent);
            MsgCls("", LblHousing, Color.Transparent);
            MsgCls("", LblOthers, Color.Transparent);
            Tab1.CssClass = "Initial";
            Tab2.CssClass = "Initial";
            Tab3.CssClass = "Initial";
            Tab4.CssClass = "Clicked";
            Tab5.CssClass = "Initial";
            MainView.ActiveViewIndex = 3;
            LoadOthers();
            GridControls();
            HFTabID.Value = "4";
            TblOthers.Visible = false;
            DivBtnsOthers.Visible = false;
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
                ITboObj1 = ITblObj.Load_IT_HistoryForAppRej(flag);
                Session.Add("ITOthersGrdInfo", ITboObj1);

                if (ITboObj1 == null || ITboObj1.Count == 0)
                {
                    MsgCls("No Records Found !", LblOthers, Color.Red);
                    GVOthersHeader.Visible = false;
                    GVOthersHeader.DataSource = null;
                    GVOthersHeader.DataBind();
                    return;
                }
                else
                {
                    GVOthersHeader.Visible = true;
                    GVOthersHeader.DataSource = ITboObj1;
                    GVOthersHeader.SelectedIndex = -1;
                    GVOthersHeader.DataBind();
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

                        tblsSec80.Visible = true;
                        divbuttonSec80.Visible = true;

                        int rowIndex = Convert.ToInt32(e.CommandArgument);

                        foreach (GridViewRow row in GVSec80Header.Rows)
                        {
                            row.BackColor = row.RowIndex.Equals(rowIndex) ?
                            System.Drawing.Color.LightGray :
                            System.Drawing.Color.White;
                        }

                        GVITSec80.Visible = true;
                        int ID = int.Parse(GVSec80Header.DataKeys[int.Parse(e.CommandArgument.ToString())]["ID"].ToString());
                        HF_Sec80ID.Value = int.Parse(GVSec80Header.DataKeys[int.Parse(e.CommandArgument.ToString())]["ID"].ToString()).ToString();
                        HF_CreatedBySec80.Value = GVSec80Header.DataKeys[int.Parse(e.CommandArgument.ToString())]["CREATED_BY"].ToString();
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
                        TblSec80C.Visible = true;
                        DivBtnsSec80C.Visible = true;

                        int rowIndex = Convert.ToInt32(e.CommandArgument);

                        foreach (GridViewRow row in GVSec80CHeader.Rows)
                        {
                            row.BackColor = row.RowIndex.Equals(rowIndex) ?
                            System.Drawing.Color.LightGray :
                            System.Drawing.Color.White;
                        }

                        GVITSec80C.Visible = true;
                        int ID = int.Parse(GVSec80CHeader.DataKeys[int.Parse(e.CommandArgument.ToString())]["ID"].ToString());
                        HF_Sec80CID.Value = int.Parse(GVSec80CHeader.DataKeys[int.Parse(e.CommandArgument.ToString())]["ID"].ToString()).ToString();
                        HF_CreatedBySec80C.Value = GVSec80CHeader.DataKeys[int.Parse(e.CommandArgument.ToString())]["CREATED_BY"].ToString();
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
                        TblHousing.Visible = true;
                        DivBtnsHousing.Visible = true;


                        int rowIndex = Convert.ToInt32(e.CommandArgument);

                        foreach (GridViewRow row in GVHousingHeader.Rows)
                        {
                            row.BackColor = row.RowIndex.Equals(rowIndex) ?
                            System.Drawing.Color.LightGray :
                            System.Drawing.Color.White;
                        }

                        GVHousing.Visible = true;
                        int ID = int.Parse(GVHousingHeader.DataKeys[int.Parse(e.CommandArgument.ToString())]["ID"].ToString());
                        HFHousingID.Value = int.Parse(GVHousingHeader.DataKeys[int.Parse(e.CommandArgument.ToString())]["ID"].ToString()).ToString();
                        HFCreatedByHousing.Value = GVHousingHeader.DataKeys[int.Parse(e.CommandArgument.ToString())]["CREATED_BY"].ToString();
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



                        //HFOthersID = null;
                        //HFPROPTYP = null;
                        TblOthers.Visible = true;
                        DivBtnsOthers.Visible = true;
                        int rowIndex = Convert.ToInt32(e.CommandArgument);



                        foreach (GridViewRow row in GVOthersHeader.Rows)
                        {
                            row.BackColor = row.RowIndex.Equals(rowIndex) ?
                            System.Drawing.Color.LightGray :
                            System.Drawing.Color.White;
                        }





                        int ID = int.Parse(GVOthersHeader.DataKeys[int.Parse(e.CommandArgument.ToString())]["ID"].ToString());
                        int nxtid = GVOthersHeader.DataKeys[int.Parse(e.CommandArgument.ToString())]["CONACTPROP"].ToString() == "-" ? 0 : int.Parse(GVOthersHeader.DataKeys[int.Parse(e.CommandArgument.ToString())]["CONACTPROP"].ToString());
                        string ITtyp = GVOthersHeader.DataKeys[int.Parse(e.CommandArgument.ToString())]["ITTYP"].ToString();
                        HFOthersID.Value = GVOthersHeader.DataKeys[int.Parse(e.CommandArgument.ToString())]["ID"].ToString();
                        HFPROPTYP.Value = GVOthersHeader.DataKeys[int.Parse(e.CommandArgument.ToString())]["ITTYP"].ToString();
                        HFCreatedByOthers.Value = GVOthersHeader.DataKeys[int.Parse(e.CommandArgument.ToString())]["CREATED_BY"].ToString();
                        ITbl ITblObj = new ITbl();
                        List<ITbo> ITboObj = new List<ITbo>();



                        if (ITtyp.Trim() == "1")
                        {
                            GVOthers1.Visible = true;
                            GVOthers2.Visible = false;
                            ITboObj = ITblObj.Load_HousingOthersDetails(nxtid == 0 ? ID : nxtid, ITtyp, User.Identity.Name, "");
                            ViewState["TypeRENTO"] = ITboObj[0].RENTO;
                            grdSelfOccDetails.DataSource = ITboObj;
                            grdSelfOccDetails.DataBind();
                            frmSelfOccDetails.DataSource = ITboObj;
                            frmSelfOccDetails.DataBind();
                            ITboObj = ITblObj.Load_PreEpInCm_cont(nxtid == 0 ? ID : nxtid, 2);



                            DataTable dt = dtBorw();
                            for (int i = 0; i < ITboObj.Count; i++)
                            {
                                dt.Rows.Add(ITboObj[i].RID, ITboObj[i].NAME, ITboObj[i].PERCNT);
                            }
                            grdBorwDetails.DataSource = dt;
                            grdBorwDetails.DataBind();
                            grdBorwDetails.Visible = true;
                            grdSelfOccDetails.Visible = true;
                            if (ViewState["TypeRENTO"].ToString() == "1")
                            {
                                GVOthers1.Visible = false;
                            }
                            else
                            {
                                ITboObj = ITblObj.Load_HousingOthersDetails(ID, ITtyp, User.Identity.Name, "");
                                GVOthers1.DataSource = ITboObj;
                                GVOthers1.DataBind();
                            }



                        }
                        else if (ITtyp.Trim() == "2")
                        {
                            grdSelfOccDetails.Visible = false;
                            grdBorwDetails.Visible = false;
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

        protected void btnApproveSec80_Click(object sender, EventArgs e)
        {
            try
            {
                bool? Status = true;
                int flag = 1;
                ITbl ITblObj = new ITbl();
                ITbo ITboObj = new ITbo();
                ITboObj.ID = int.Parse(HF_Sec80ID.Value.ToString().Trim());
                ITboObj.REMARKS = txtRemarksSec80.Text.Trim();
                ITboObj.STATUS = "Approved";
                ITblObj.AppRej_IT(ITboObj, flag, ref Status);
                if (Status.Equals(false))
                {
                    SendMail(int.Parse(HF_Sec80ID.Value.ToString().Trim()), HF_CreatedBySec80.Value.ToString().Trim(), "Approved", "Section 80", txtRemarksSec80.Text.Trim());
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('IT Declaration Approved successfully !')", true);
                    Tab1.CssClass = "Clicked";
                    Tab2.CssClass = "Initial";
                    Tab3.CssClass = "Initial";
                    Tab4.CssClass = "Initial";
                    MainView.ActiveViewIndex = 0;
                    LoadSec80();
                    GridControls();
                    txtRemarksSec80.Text = string.Empty;
                    tblsSec80.Visible = false;
                    divbuttonSec80.Visible = false;



                }

                HF_Sec80ID = null;
                HF_CreatedBySec80 = null;
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        }

        protected void btnRejectSec80_Click(object sender, EventArgs e)
        {
            try
            {
                bool? Status = true;
                int flag = 1;
                ITbl ITblObj = new ITbl();
                ITbo ITboObj = new ITbo();
                ITboObj.ID = int.Parse(HF_Sec80ID.Value.ToString().Trim());
                ITboObj.REMARKS = txtRemarksSec80.Text.Trim();
                ITboObj.STATUS = "Rejected";
                ITblObj.AppRej_IT(ITboObj, flag, ref Status);
                if (Status.Equals(false))
                {
                    SendMail(int.Parse(HF_Sec80ID.Value.ToString().Trim()), HF_CreatedBySec80.Value.ToString().Trim(), "Rejected", "Section 80", txtRemarksSec80.Text.Trim());
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('IT Declaration Rejected successfully !')", true);
                    Tab1.CssClass = "Clicked";
                    Tab2.CssClass = "Initial";
                    Tab3.CssClass = "Initial";
                    Tab4.CssClass = "Initial";
                    MainView.ActiveViewIndex = 0;
                    LoadSec80();
                    GridControls();
                    txtRemarksSec80.Text = string.Empty;
                    tblsSec80.Visible = false;
                    divbuttonSec80.Visible = false;

                }

                HF_Sec80ID = null;
                HF_CreatedBySec80 = null;
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        }


        protected void BtnAppSec80C_Click(object sender, EventArgs e)
        {
            try
            {
                bool? Status = true;
                int flag = 2;
                ITbl ITblObj = new ITbl();
                ITbo ITboObj = new ITbo();
                ITboObj.ID = int.Parse(HF_Sec80CID.Value.ToString().Trim());
                ITboObj.REMARKS = TXT_RemarksSec80C.Text.Trim();
                ITboObj.STATUS = "Approved";
                ITblObj.AppRej_IT(ITboObj, flag, ref Status);
                if (Status.Equals(false))
                {
                    SendMail(int.Parse(HF_Sec80CID.Value.ToString().Trim()), HF_CreatedBySec80C.Value.ToString().Trim(), "Approved", "Section 80 C", TXT_RemarksSec80C.Text.Trim());
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('IT Declaration Approved successfully !')", true);
                    Tab1.CssClass = "Initial";
                    Tab2.CssClass = "Clicked";
                    Tab3.CssClass = "Initial";
                    Tab4.CssClass = "Initial";
                    MainView.ActiveViewIndex = 1;
                    LoadSec80C();
                    GridControls();
                    TXT_RemarksSec80C.Text = string.Empty;
                    TblSec80C.Visible = false;
                    DivBtnsSec80C.Visible = false;

                }

                HF_Sec80CID = null;
                HF_CreatedBySec80C = null;
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        }

        protected void BtnRejSec80C_Click(object sender, EventArgs e)
        {
            try
            {
                bool? Status = true;
                int flag = 2;
                ITbl ITblObj = new ITbl();
                ITbo ITboObj = new ITbo();
                ITboObj.ID = int.Parse(HF_Sec80CID.Value.ToString().Trim());
                ITboObj.REMARKS = TXT_RemarksSec80C.Text.Trim();
                ITboObj.STATUS = "Rejected";
                ITblObj.AppRej_IT(ITboObj, flag, ref Status);
                if (Status.Equals(false))
                {
                    SendMail(int.Parse(HF_Sec80CID.Value.ToString().Trim()), HF_CreatedBySec80C.Value.ToString().Trim(), "Rejected", "Section 80 C", TXT_RemarksSec80C.Text.Trim());

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('IT Declaration Rejected successfully !')", true);
                    Tab1.CssClass = "Initial";
                    Tab2.CssClass = "Clicked";
                    Tab3.CssClass = "Initial";
                    Tab4.CssClass = "Initial";
                    MainView.ActiveViewIndex = 1;
                    LoadSec80C();
                    GridControls();
                    TXT_RemarksSec80C.Text = string.Empty;
                    TblSec80C.Visible = false;
                    DivBtnsSec80C.Visible = false;
                }

                HF_Sec80CID = null;
                HF_CreatedBySec80C = null;

            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        }


        protected void BtnAppHousing_Click(object sender, EventArgs e)
        {
            try
            {

                bool? Status = true;
                int flag = 3;
                ITbl ITblObj = new ITbl();
                ITbo ITboObj = new ITbo();
                ITboObj.ID = int.Parse(HFHousingID.Value.ToString().Trim());
                ITboObj.REMARKS = TXT_RemarksHousing.Text.Trim();
                ITboObj.STATUS = "Approved";
                ITblObj.AppRej_IT(ITboObj, flag, ref Status);
                if (Status.Equals(false))
                {
                    SendMail(int.Parse(HFHousingID.Value.ToString().Trim()), HFCreatedByHousing.Value.ToString().Trim(), "Approved", "Housing", TXT_RemarksHousing.Text.Trim());
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('IT Declaration Approved successfully !')", true);
                    Tab1.CssClass = "Initial";
                    Tab2.CssClass = "Initial";
                    Tab3.CssClass = "Clicked";
                    Tab4.CssClass = "Initial";
                    MainView.ActiveViewIndex = 2;
                    LoadHousing();
                    GridControls();
                    TXT_RemarksHousing.Text = string.Empty;
                    TblHousing.Visible = false;
                    DivBtnsHousing.Visible = false;


                }

                HFHousingID = null;
                HFCreatedByHousing = null;
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        }

        protected void BtnRejHousing_Click(object sender, EventArgs e)
        {
            try
            {

                bool? Status = true;
                int flag = 3;
                ITbl ITblObj = new ITbl();
                ITbo ITboObj = new ITbo();
                ITboObj.ID = int.Parse(HFHousingID.Value.ToString().Trim());
                ITboObj.REMARKS = TXT_RemarksHousing.Text.Trim();
                ITboObj.STATUS = "Rejected";
                ITblObj.AppRej_IT(ITboObj, flag, ref Status);
                if (Status.Equals(false))
                {
                    SendMail(int.Parse(HFHousingID.Value.ToString().Trim()), HFCreatedByHousing.Value.ToString().Trim(), "Rejected", "Housing", TXT_RemarksHousing.Text.Trim());

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('IT Declaration Rejected successfully !')", true);
                    Tab1.CssClass = "Initial";
                    Tab2.CssClass = "Initial";
                    Tab3.CssClass = "Clicked";
                    Tab4.CssClass = "Initial";
                    MainView.ActiveViewIndex = 2;
                    LoadHousing();
                    GridControls();
                    TXT_RemarksHousing.Text = string.Empty;
                    TblHousing.Visible = false;
                    DivBtnsHousing.Visible = false;
                }

                HFHousingID = null;
                HFCreatedByHousing = null;
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        }

        protected void BtnAppOthers_Click(object sender, EventArgs e)
        {
            try
            {
                bool? Status = true;
                int flag = 4;
                ITbl ITblObj = new ITbl();
                ITbo ITboObj = new ITbo();
                ITboObj.ID = int.Parse(HFOthersID.Value.ToString().Trim());
                ITboObj.REMARKS = TXT_RemarksOthers.Text.Trim();
                ITboObj.STATUS = "Approved";
                ITblObj.AppRej_IT(ITboObj, flag, ref Status);
                if (Status.Equals(false))
                {
                    SendMail(int.Parse(HFOthersID.Value.ToString().Trim()), HFCreatedByOthers.Value.ToString().Trim(), "Approved", "Other Sources", TXT_RemarksOthers.Text.Trim());
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('IT Declaration Approved successfully !')", true);
                    Tab1.CssClass = "Initial";
                    Tab2.CssClass = "Initial";
                    Tab3.CssClass = "Initial";
                    Tab4.CssClass = "Clicked";
                    MainView.ActiveViewIndex = 3;
                    LoadOthers();
                    GridControls();
                    TXT_RemarksOthers.Text = string.Empty;
                    TblOthers.Visible = false;
                    DivBtnsOthers.Visible = false;

                }

                HFOthersID = null;
                HFCreatedByOthers = null;
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        }

        protected void BtnRejOthers_Click(object sender, EventArgs e)
        {
            try
            {
                bool? Status = true;
                int flag = 4;
                ITbl ITblObj = new ITbl();
                ITbo ITboObj = new ITbo();
                ITboObj.ID = int.Parse(HFOthersID.Value.ToString().Trim());
                ITboObj.REMARKS = TXT_RemarksOthers.Text.Trim();
                ITboObj.STATUS = "Rejected";
                ITblObj.AppRej_IT(ITboObj, flag, ref Status);
                if (Status.Equals(false))
                {
                    SendMail(int.Parse(HFOthersID.Value.ToString().Trim()), HFCreatedByOthers.Value.ToString().Trim(), "Rejected", "Other Sources", TXT_RemarksOthers.Text.Trim());

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('IT Declaration Rejected successfully !')", true);
                    Tab1.CssClass = "Initial";
                    Tab2.CssClass = "Initial";
                    Tab3.CssClass = "Initial";
                    Tab4.CssClass = "Clicked";
                    MainView.ActiveViewIndex = 3;
                    LoadOthers();
                    GridControls();
                    TXT_RemarksOthers.Text = string.Empty;
                    TblOthers.Visible = false;
                    DivBtnsOthers.Visible = false;

                }

                HFOthersID = null;
                HFCreatedByOthers = null;
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        }



        public void SendMail(int ID, string Pernr, string sts, string type, string Remarks)
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

                objcontext.usp_IT_Get_MailListSec80(ID, Pernr, ref EMP_Name, ref EMP_Email);

                strSubject = "Income Tax Request " + ID + " has been " + sts + " by Finance.";

                RecipientsString = "monica.ks@itchamps.com";
                strPernr_Mail = "latha.mg@itchamps.com";

                //    //Preparing the mail body--------------------------------------------------
                string body = "<b>Income Tax Request " + ID + " has been " + sts + " by Finance.<br/><br/></b>";
                body += "<b>Income Tax ID  :  " + ID + "</b><br/><br/>";
                body += "<b>Income Tax Type :  " + type + "</b><br/><br/>";
                body += "<b>Remarks :  " + Remarks + "</b><br/><br/>";

                //    //End of preparing the mail body-------------------------------------------
                iEmpPowerMaster_Load.masterbl.DispatchMail(RecipientsString, User.Identity.Name, strSubject, strPernr_Mail, body);
                lblMessageBoard.ForeColor = System.Drawing.Color.Green;
                lblMessageBoard.Text = "Income Tax " + sts + " successfully and Mail sent successfully.";
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
                //txtFromDate.Text = string.Empty;
                //txtTodate.Text = string.Empty;
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
                txtRemarksSec80.Text = string.Empty;
                tblsSec80.Visible = false;
                divbuttonSec80.Visible = false;

                HFTabID.Value = "1";
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
                //txtFromDate.Text = string.Empty;
                //txtTodate.Text = string.Empty;
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
                TXT_RemarksSec80C.Text = string.Empty;
                TblSec80C.Visible = false;
                DivBtnsSec80C.Visible = false;
                HFTabID.Value = "2";
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
                //txtFromDate.Text = string.Empty;
                //txtTodate.Text = string.Empty;
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
                TXT_RemarksHousing.Text = string.Empty;
                TblHousing.Visible = false;
                DivBtnsHousing.Visible = false;
                HFTabID.Value = "3";
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
                //txtFromDate.Text = string.Empty;
                //txtTodate.Text = string.Empty;
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
                TXT_RemarksOthers.Text = string.Empty;
                TblOthers.Visible = false;
                DivBtnsOthers.Visible = false;
                HFTabID.Value = "4";
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
                // viewcheck.Value = "NO";
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
                //txtFromDate.Text = string.Empty;
                //txtTodate.Text = string.Empty;
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
                txtRemarksSec80.Text = string.Empty;
                tblsSec80.Visible = false;
                divbuttonSec80.Visible = false;
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
                // viewcheck.Value = "NO";
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
                //txtFromDate.Text = string.Empty;
                //txtTodate.Text = string.Empty;
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
                TXT_RemarksSec80C.Text = string.Empty;
                TblSec80C.Visible = false;
                DivBtnsSec80C.Visible = false;
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
                // viewcheck.Value = "NO";
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
                //txtFromDate.Text = string.Empty;
                //txtTodate.Text = string.Empty;
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
                TXT_RemarksHousing.Text = string.Empty;
                TblHousing.Visible = false;
                DivBtnsHousing.Visible = false;
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
                // viewcheck.Value = "NO";
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
                //txtFromDate.Text = string.Empty;
                //txtTodate.Text = string.Empty;
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
                TXT_RemarksOthers.Text = string.Empty;
                TblOthers.Visible = false;
                DivBtnsOthers.Visible = false;
                HFTabID.Value = "4";
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
                //DateTime FromDate = DateTime.Parse(string.IsNullOrEmpty(txtFromDate.Text) ? "01/01/0001" : txtFromDate.Text);
                //DateTime ToDate = DateTime.Parse(string.IsNullOrEmpty(txtTodate.Text) ? "01/01/0001" : txtTodate.Text);

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
                    else if (int.Parse(HFTabID.Value.ToString().Trim()) == 5)
                    {
                        MsgCls("Please Enter the Text", lblPreE, Color.Red);
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
                    else if (int.Parse(HFTabID.Value.ToString().Trim()) == 5)
                    {
                        MsgCls("Please Select the Type", lblPreE, Color.Red);
                    }

                }

                else
                {
                    ITbl ITblObj = new ITbl();
                    List<ITbo> ITboObj = new List<ITbo>();
                    List<ITbo> ITboObj1 = new List<ITbo>();
                    ITboObj1 = ITblObj.Load_ParticularITForAdminAppRej(SelectedType, textSearch, int.Parse(HFTabID.Value.ToString().Trim()));

                    if (int.Parse(HFTabID.Value.ToString().Trim()) == 1)
                    {

                        if (ITboObj1 == null || ITboObj1.Count == 0)
                        {
                            MsgCls("No Records found", lblMessageBoard, Color.Red);
                            //GVSec80Header.Visible = false;
                            GVSec80Header.DataSource = null;
                            GVSec80Header.DataBind();
                            return;
                        }
                        else
                        {
                            MsgCls("", lblMessageBoard, Color.Transparent);
                            GVSec80Header.Visible = true;
                            GVSec80Header.DataSource = ITboObj1;
                            GVSec80Header.SelectedIndex = -1;
                            GVSec80Header.DataBind();

                        }

                        tblsSec80.Visible = false;
                        divbuttonSec80.Visible = false;
                    }

                    else if (int.Parse(HFTabID.Value.ToString().Trim()) == 2)
                    {

                        if (ITboObj1 == null || ITboObj1.Count == 0)
                        {
                            MsgCls("No Records found", LblSec80c, Color.Red);
                            //GVSec80Header.Visible = false;
                            GVSec80CHeader.DataSource = null;
                            GVSec80CHeader.DataBind();
                            return;
                        }
                        else
                        {
                            MsgCls("", LblSec80c, Color.Transparent);
                            GVSec80CHeader.Visible = true;
                            GVSec80CHeader.DataSource = ITboObj1;
                            GVSec80CHeader.SelectedIndex = -1;
                            GVSec80CHeader.DataBind();

                        }
                        TblSec80C.Visible = false;
                        DivBtnsSec80C.Visible = false;
                    }


                    else if (int.Parse(HFTabID.Value.ToString().Trim()) == 3)
                    {

                        if (ITboObj1 == null || ITboObj1.Count == 0)
                        {
                            MsgCls("No Records found", LblHousing, Color.Red);
                            //GVSec80Header.Visible = false;
                            GVHousingHeader.DataSource = null;
                            GVHousingHeader.DataBind();
                            return;
                        }
                        else
                        {
                            MsgCls("", LblHousing, Color.Transparent);
                            GVHousingHeader.Visible = true;
                            GVHousingHeader.DataSource = ITboObj1;
                            GVHousingHeader.SelectedIndex = -1;
                            GVHousingHeader.DataBind();

                        }
                        TblHousing.Visible = false;
                        DivBtnsHousing.Visible = false;
                    }

                    else if (int.Parse(HFTabID.Value.ToString().Trim()) == 4)
                    {

                        if (ITboObj1 == null || ITboObj1.Count == 0)
                        {
                            MsgCls("No Records found", LblOthers, Color.Red);
                            //GVSec80Header.Visible = false;
                            GVOthersHeader.DataSource = null;
                            GVOthersHeader.DataBind();
                            return;
                        }
                        else
                        {
                            MsgCls("", LblOthers, Color.Transparent);
                            GVOthersHeader.Visible = true;
                            GVOthersHeader.DataSource = ITboObj1;
                            GVOthersHeader.SelectedIndex = -1;
                            GVOthersHeader.DataBind();

                        }
                        TblOthers.Visible = false;
                        DivBtnsOthers.Visible = false;
                    }

                    else if (int.Parse(HFTabID.Value.ToString().Trim()) == 5)
                    {

                        if (ITboObj1 == null || ITboObj1.Count == 0)
                        {
                            MsgCls("No Records found", LblOthers, Color.Red);
                            //GVSec80Header.Visible = false;
                            grdPreEmptIncHead = null;
                            grdPreEmptIncHead.DataBind();
                            return;
                        }
                        else
                        {
                            MsgCls("", LblOthers, Color.Transparent);
                            grdPreEmptIncHead.Visible = true;
                            grdPreEmptIncHead.DataSource = ITboObj1;
                            grdPreEmptIncHead.SelectedIndex = -1;
                            grdPreEmptIncHead.DataBind();

                        }
                        TblOthers.Visible = false;
                        DivBtnsOthers.Visible = false;
                    }

                }
            }
            catch (Exception Ex)
            {
                //// ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true);
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
                else if (int.Parse(HFTabID.Value.ToString().Trim()) == 5)
                {
                    MsgCls(Ex.Message, lblPreE, Color.Red);
                }
            }

        }

        protected void btnclear_Click(object sender, EventArgs e)
        {
            try
            {
                ddlSeachSelect.SelectedValue = "0";
                txtsearch.Text = string.Empty;
                //txtFromDate.Text = string.Empty;
                //txtTodate.Text = string.Empty;
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
                    tblsSec80.Visible = false;
                    divbuttonSec80.Visible = false;
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
                    TblSec80C.Visible = false;
                    DivBtnsSec80C.Visible = false;
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
                    TblHousing.Visible = false;
                    DivBtnsHousing.Visible = false;
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
                    TblOthers.Visible = false;
                    DivBtnsOthers.Visible = false;
                }
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
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

        private DataTable dtBorw()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("PERCNT", typeof(string));
            return dt;
        }

        protected void Tab5_Click(object sender, EventArgs e)
        {
            ddlSeachSelect.SelectedValue = "0";
            txtsearch.Text = string.Empty;
            //txtFromDate.Text = string.Empty;
            //txtTodate.Text = string.Empty;
            MsgCls("", lblMessageBoard, Color.Transparent);
            MsgCls("", LblSec80c, Color.Transparent);
            MsgCls("", LblHousing, Color.Transparent);
            MsgCls("", LblOthers, Color.Transparent);
            MsgCls("", lblPreE, Color.Transparent);
            Tab1.CssClass = "Initial";
            Tab2.CssClass = "Initial";
            Tab3.CssClass = "Initial";
            Tab4.CssClass = "Initial";
            Tab5.CssClass = "Clicked";
            MainView.ActiveViewIndex = 4;
            HFTabID.Value = "5";
            divPreEmp.Visible = false;
            Load_PreEmp_Income();
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
                ITboObj1 = ITblObj.Load_IT_HistoryForAppRej(flag);
                //Session.Add("ITOthersGrdInfo", ITboObj1);

                if (ITboObj1 == null || ITboObj1.Count == 0)
                {
                    MsgCls("No Records Found !", LblOthers, Color.Red);
                    grdPreEmptIncHead.Visible = false;
                    grdPreEmptIncHead.DataSource = null;
                    grdPreEmptIncHead.DataBind();
                    return;
                }
                else
                {
                    grdPreEmptIncHead.Visible = true;
                    grdPreEmptIncHead.DataSource = ITboObj1;
                    grdPreEmptIncHead.SelectedIndex = -1;
                    grdPreEmptIncHead.DataBind();
                }
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        }

        protected void grdPreEmptIncHead_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName.ToUpper())
                {
                    case "VIEW":

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
                        divPreEmp.Visible = true;
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
                divPreEmp.Visible = false;
                int pageindex = e.NewPageIndex;
                grdPreEmptIncHead.PageIndex = e.NewPageIndex;
                Load_PreEmp_Income();
                search();
                ddlSeachSelect.SelectedValue = "0";
                txtsearch.Text = string.Empty;
                MsgCls("", lblMessageBoard, Color.Transparent);
                MsgCls("", LblSec80c, Color.Transparent);
                MsgCls("", LblHousing, Color.Transparent);
                MsgCls("", LblOthers, Color.Transparent);
                Tab1.CssClass = "Initial";
                Tab2.CssClass = "Initial";
                Tab3.CssClass = "Initial";
                Tab5.CssClass = "Clicked";
                Tab4.CssClass = "Initial";
                MainView.ActiveViewIndex = 4;
                HFTabID.Value = "5";

            }
            catch (Exception ex) { }
        }

        protected void btnPreEmApp_Click(object sender, EventArgs e)
        {
            try
            {
                bool? Status = true;
                int flag = 5;
                ITbl ITblObj = new ITbl();
                ITbo ITboObj = new ITbo();
                ITboObj.ID = int.Parse(ViewState["PreemptIn_ID"].ToString().Trim());
                ITboObj.REMARKS = txtPreEmpRemks.Text.Trim();
                ITboObj.STATUS = "Approved";
                ITblObj.AppRej_IT(ITboObj, flag, ref Status);
                if (Status.Equals(false))
                {
                    //SendMail(int.Parse(HF_Sec80CID.Value.ToString().Trim()), HF_CreatedBySec80C.Value.ToString().Trim(), "Approved", "Section 80 C", TXT_RemarksSec80C.Text.Trim());
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('IT Declaration Approved successfully !')", true);
                    Tab1.CssClass = "Initial";
                    Tab5.CssClass = "Clicked";
                    Tab3.CssClass = "Initial";
                    Tab4.CssClass = "Initial";
                    Tab2.CssClass = "Initial";
                    MainView.ActiveViewIndex = 4;
                    Load_PreEmp_Income();
                    txtPreEmpRemks.Text = string.Empty;
                    divPreEmp.Visible = false;
                }
                HF_Sec80CID = null;
                HF_CreatedBySec80C = null;
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        }

        protected void btnPreEmRej_Click(object sender, EventArgs e)
        {
            try
            {
                bool? Status = true;
                int flag = 5;
                ITbl ITblObj = new ITbl();
                ITbo ITboObj = new ITbo();
                ITboObj.ID = int.Parse(ViewState["PreemptIn_ID"].ToString().Trim());
                ITboObj.REMARKS = txtPreEmpRemks.Text.Trim();
                ITboObj.STATUS = "Rejected";
                ITblObj.AppRej_IT(ITboObj, flag, ref Status);
                if (Status.Equals(false))
                {
                    //SendMail(int.Parse(HF_Sec80CID.Value.ToString().Trim()), HF_CreatedBySec80C.Value.ToString().Trim(), "Approved", "Section 80 C", TXT_RemarksSec80C.Text.Trim());
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('IT Declaration Approved successfully !')", true);
                    Tab1.CssClass = "Initial";
                    Tab5.CssClass = "Clicked";
                    Tab3.CssClass = "Initial";
                    Tab4.CssClass = "Initial";
                    Tab2.CssClass = "Initial";
                    MainView.ActiveViewIndex = 4;
                    Load_PreEmp_Income();
                    txtPreEmpRemks.Text = string.Empty;
                    divPreEmp.Visible = false;
                }
                HF_Sec80CID = null;
                HF_CreatedBySec80C = null;
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        }

        protected void lbtnDownldall_Click(object sender, EventArgs e)
        {
            try
            {
                using (ZipFile zip = new ZipFile())
                {
                    zip.AlternateEncodingUsage = ZipOption.AsNecessary;
                    //zip.AddDirectoryByName("ITDoc");



                    if (HFTabID.Value.ToString().Trim() == "1" || HFTabID.Value.ToString().Trim() == "2")
                    {
                        ITbl ITblObj = new ITbl();
                        List<ITbo> ITboObj = new List<ITbo>();
                        for (int i = 0; i < (HFTabID.Value.ToString().Trim() == "1" ? GVSec80Header.Rows.Count : GVSec80CHeader.Rows.Count); i++)
                        {
                            int ID = int.Parse(HFTabID.Value.ToString().Trim() == "1" ? GVSec80Header.DataKeys[i].Values[0].ToString() : GVSec80CHeader.DataKeys[i].Values[0].ToString());
                            string CREATED_BY = HFTabID.Value.ToString().Trim() == "1" ? GVSec80Header.DataKeys[i].Values[1].ToString() : GVSec80CHeader.DataKeys[i].Values[1].ToString();
                            ITboObj = HFTabID.Value.ToString().Trim() == "1" ? ITblObj.Load_Sec80Details(ID, User.Identity.Name) : ITblObj.Load_Sec80CDetails(ID, User.Identity.Name);



                            bool? sts = false;
                            string a1 = "", fpth = "";
                            ITdalDataContext DAL = new ITdalDataContext();
                            DAL.usp_IT_set_get_submitionFile(CREATED_BY, Convert.ToDateTime("1900-01-01"), Convert.ToDateTime("1900-01-01"), a1, 2, ref sts, ref fpth);



                            //foreach (var a in ITboObj)
                            //{
                            string filePath = fpth;
                            if (filePath != "")
                            {
                                //string aw = a.RECEIPT_FPATH.Substring(8);
                                filePath = Server.MapPath("~/ITDoc/") + fpth.Substring(8);
                                zip.AddFile(filePath, "_ITDocs-" + CREATED_BY);
                            }
                            //}
                            //ITboObj = null;
                        }
                        Response.Clear();
                        Response.BufferOutput = false;
                        string zipName = String.Format("Income_Tax_{0}.zip", DateTime.Now.ToString("yyyy-MMM-dd"));
                        //Response.AddHeader("content-disposition", "attachment; filename=" + zipName);
                        //Response.ContentType = "application/zip";
                        //bool exists = System.IO.Directory.Exists(@"C:\\Income Tax");
                        //if (!exists)
                        //    System.IO.Directory.CreateDirectory(@"C:\\Income Tax");
                        // zip.Save(@"C:\\Income Tax\\" + zipName + ".zip");
                        Response.ContentType = "application/zip";
                        Response.AddHeader("content-disposition", "attachment; filename=" + zipName);
                        zip.Save(Response.OutputStream);
                        Response.End();
                    }
                }
            }
            catch (Exception ex) { }
        }

        //protected void UpdateITSec80()
        //{
        //    try
        //    {

        //        ////int RecordCount = 0;

        //        ////if (GVITSec80.Rows.Count > 0)
        //        ////{
        //        ////    for (int i = 0; i < GVITSec80.Rows.Count; i++)
        //        ////    {

        //        ////        using (TextBox txtActContr = (TextBox)GVITSec80.Rows[i].FindControl("txtActContr"))
        //        ////        {
        //        ////            if ((!string.IsNullOrEmpty(txtActContr.Text)) && (decimal.Parse(txtActContr.Text.ToString().Trim()) > 0))
        //        ////            {
        //        ////                RecordCount = RecordCount + 1;
        //        ////            }

        //        ////        }
        //        ////    }
        //        ////}

        //        ////if (RecordCount <= 0)
        //        ////{
        //        ////    ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Please enter atleast one record before submiting');", true);
        //        ////    MsgCls("Please enter atleast one record before submiting", lblMessageBoard, System.Drawing.Color.Red);
        //        ////    return;
        //        ////}


        //        ////bool? sts = false;
        //        ////int rowid141 = 0;
        //        ////int rowid142 = 0;
        //        ////int rowid31 = 0;
        //        ////int rowid32 = 0;
        //        ////if (GVITSec80.Rows.Count > 0)
        //        ////{
        //        ////    for (int i = 0; i < GVITSec80.Rows.Count; i++)
        //        ////    {
        //        ////        if ((decimal.Parse(GVITSec80.DataKeys[i]["SBSEC"].ToString()) == 14) && (decimal.Parse(GVITSec80.DataKeys[i]["SBDIV"].ToString()) == 1))
        //        ////        {
        //        ////            rowid141 = i;
        //        ////        }
        //        ////        else if ((decimal.Parse(GVITSec80.DataKeys[i]["SBSEC"].ToString()) == 14) && (decimal.Parse(GVITSec80.DataKeys[i]["SBDIV"].ToString()) == 2))
        //        ////        {
        //        ////            rowid142 = i;
        //        ////        }
        //        ////        else if ((decimal.Parse(GVITSec80.DataKeys[i]["SBSEC"].ToString()) == 3) && (decimal.Parse(GVITSec80.DataKeys[i]["SBDIV"].ToString()) == 1))
        //        ////        {
        //        ////            rowid31 = i;
        //        ////        }
        //        ////        else if ((decimal.Parse(GVITSec80.DataKeys[i]["SBSEC"].ToString()) == 3) && (decimal.Parse(GVITSec80.DataKeys[i]["SBDIV"].ToString()) == 2))
        //        ////        {
        //        ////            rowid32 = i;
        //        ////        }

        //        ////    }
        //        ////}

        //        ////using (TextBox txtPropContr141 = (TextBox)GVITSec80.Rows[rowid141].FindControl("txtPropContr"))
        //        ////using (TextBox txtActContr141 = (TextBox)GVITSec80.Rows[rowid141].FindControl("txtActContr"))

        //        ////using (TextBox txtPropContr142 = (TextBox)GVITSec80.Rows[rowid142].FindControl("txtPropContr"))
        //        ////using (TextBox txtActContr142 = (TextBox)GVITSec80.Rows[rowid142].FindControl("txtActContr"))

        //        ////using (TextBox txtPropContr31 = (TextBox)GVITSec80.Rows[rowid31].FindControl("txtPropContr"))
        //        ////using (TextBox txtActContr31 = (TextBox)GVITSec80.Rows[rowid31].FindControl("txtActContr"))

        //        ////using (TextBox txtPropContr32 = (TextBox)GVITSec80.Rows[rowid32].FindControl("txtPropContr"))
        //        ////using (TextBox txtActContr32 = (TextBox)GVITSec80.Rows[rowid32].FindControl("txtActContr"))
        //        ////{
        //        ////    if (((!string.IsNullOrEmpty(txtPropContr141.Text.ToString())) && (decimal.Parse(txtPropContr141.Text.ToString().Trim()) > 0)) && ((!string.IsNullOrEmpty(txtPropContr142.Text.ToString())) && (decimal.Parse(txtPropContr142.Text.ToString().Trim()) > 0)))
        //        ////    {
        //        ////        ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Cannot fil both Deduction for self disability and Deduction for self severe disability');", true);
        //        ////        MsgCls("Cannot fil both Deduction for self disability and Deduction for self severe disability", lblMessageBoard, System.Drawing.Color.Red);
        //        ////        return;
        //        ////    }

        //        ////    if (((!string.IsNullOrEmpty(txtPropContr31.Text.ToString())) && (decimal.Parse(txtPropContr31.Text.ToString().Trim()) > 0)) && ((!string.IsNullOrEmpty(txtPropContr32.Text.ToString())) && (decimal.Parse(txtPropContr32.Text.ToString().Trim()) > 0)))
        //        ////    {
        //        ////        ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Cannot fil both Deduction for dependent with disability and Deduction for dependent with severe disability');", true);
        //        ////        MsgCls("Cannot fil both Deduction for dependent with disability and Deduction for dependent with severe disability", lblMessageBoard, System.Drawing.Color.Red);
        //        ////        return;
        //        ////    }
        //        ////}
        //        int? ITHID = 0;
        //        ITbl ITblObj = new ITbl();
        //        ITbo ITboObj = new ITbo();

        //        ITboObj.CREATED_BY = User.Identity.Name;
        //        ITboObj.PERNR = User.Identity.Name;
        //        ITboObj.STATUS = "Saved";//"Requested";
        //        ITboObj.CREATED_ON = DateTime.Now;
        //        ITboObj.BEGDA = DateTime.Now;
        //        ITboObj.ENDDA = DateTime.Now;
        //        ITboObj.MODIFIED_BY = "";
        //        ITboObj.Flag = 1;
        //        ITboObj.ID = 0;
        //        //ITboObj.CONACTPROP = CB_ConsAct.Checked ? "1" : "0";
        //        ITblObj.Create_ITSECTION80HEADR(ITboObj, ref ITHID);

        //        if (ITHID != null)
        //        {
        //            string date1;
        //            DataTable Dt = GetSec80Dt();
        //            if (GVITSec80.Rows.Count > 0)
        //            {
        //                for (int i = 0; i < GVITSec80.Rows.Count; i++)
        //                {
        //                    //using (TextBox txtPropContr = (TextBox)GVITSec80.Rows[i].FindControl("txtPropContr"))
        //                    using (TextBox txtActContr = (TextBox)GVITSec80.Rows[i].FindControl("txtActContr"))
        //                    //using (FileUpload fu = (FileUpload)GVITSec80.Rows[i].FindControl("fuAttachments"))
        //                    //using (TextBox txtRemarks = (TextBox)GVITSec80.Rows[i].FindControl("txtRemarks"))
        //                    {
        //                        //if (txtPropContr != null)
        //                        //{
        //                            if (txtActContr != null)
        //                            {
        //                                //if (fu != null)
        //                                //{
        //                                    //if (txtRemarks != null)
        //                                    //{
        //                                        date1 = DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss");

        //                                        //  <%-- //select t8.SBSEC,t8.SBDIV,t8.SBDDS, t9.SDVLT,t9.TXEXM--%>
        //                                        ITboObj.SBSEC = decimal.Parse(GVITSec80.DataKeys[i]["SBSEC"].ToString());
        //                                        ITboObj.SBDIV = decimal.Parse(GVITSec80.DataKeys[i]["SBDIV"].ToString());
        //                                        //ITboObj.PROPCONTR = string.IsNullOrEmpty(txtPropContr.Text.ToString()) ? 0 : decimal.Parse(txtPropContr.Text.ToString());
        //                                        ITboObj.ACTCONTR = string.IsNullOrEmpty(txtActContr.Text.ToString()) ? 0 : decimal.Parse(txtActContr.Text.ToString());
        //                                        //ITboObj.EMPCOMMENTS = txtRemarks.Text;
        //                                        //ITboObj.RECEIPT_FID = fu.HasFile ? fu.PostedFile.FileName : "";
        //                                        //ITboObj.RECEIPT_FILE = fu.HasFile ? "YES" : "NO";
        //                                        //ITboObj.RECEIPT_FPATH = fu.HasFile ? "~/ITDoc/" + User.Identity.Name + "-" + date1 + Path.GetExtension(fu.FileName) : "";
        //                                        //if (fu.HasFile)
        //                                        //{ fu.SaveAs(Server.MapPath("~/ITDoc/" + User.Identity.Name + "-" + date1) + Path.GetExtension(fu.FileName)); }
        //                                        Dt.Rows.Add(ITHID, i + 1, ITboObj.SBSEC, ITboObj.SBDIV, ITboObj.PROPCONTR, ITboObj.ACTCONTR, ITboObj.RECEIPT_FILE, ITboObj.RECEIPT_FID, ITboObj.RECEIPT_FPATH, ITboObj.EMPCOMMENTS);


        //                                    //}

        //                                    //else
        //                                    //{
        //                                    //    throw new Exception("-1");
        //                                    //}
        //                                //}
        //                                //else
        //                                //{
        //                                //    throw new Exception("-1");
        //                                //}
        //                            }
        //                            else
        //                            {
        //                                throw new Exception("-1");
        //                            }
        //                       // }
        //                        //else
        //                        //{
        //                        //    throw new Exception("-1");
        //                        //}
        //                    }
        //                }
        //            }

        //            if (Dt != null)
        //            {
        //                if (Dt.Rows.Count > 0)
        //                {
        //                    for (int y = 0; y < Dt.Rows.Count; y++)
        //                    {
        //                        ITbl ITblObj1 = new ITbl();
        //                        ITbo ITboObj1 = new ITbo();


        //                        ITboObj1.ID = int.Parse(Dt.Rows[y]["ITHID"].ToString());
        //                        ITboObj1.LID = int.Parse(Dt.Rows[y]["ITLID"].ToString());
        //                        ITboObj1.SBSEC = decimal.Parse(Dt.Rows[y]["SBSEC"].ToString());
        //                        ITboObj1.SBDIV = decimal.Parse(Dt.Rows[y]["SBDIV"].ToString());
        //                        //ITboObj1.PROPCONTR = decimal.Parse(Dt.Rows[y]["PROPCONTR"].ToString());
        //                        ITboObj1.ACTCONTR = decimal.Parse(Dt.Rows[y]["ACTCONTR"].ToString());
        //                        //ITboObj1.RECEIPT_FILE = Dt.Rows[y]["RECEIPT_FILE"].ToString();
        //                        //ITboObj1.RECEIPT_FID = Dt.Rows[y]["RECEIPT_FID"].ToString();
        //                        //ITboObj1.RECEIPT_FPATH = Dt.Rows[y]["RECEIPT_FPATH"].ToString();
        //                        //ITboObj1.EMPCOMMENTS = Dt.Rows[y]["EMPCOMMENTS"].ToString();
        //                        ITboObj1.Flag = 1;



        //                        ITblObj.Create_ITSection80Transaction(ITboObj1, ref sts);
        //                        GetFinancialDates();
        //                        //LoadGrid();
        //                        //btnSubmitClaims.Visible = false;
        //                        //BtnUpdate.Visible = false;
        //                        //BtnCancel.Visible = false;
        //                        //BtnEdit.Visible = true;
        //                    }
        //                }
        //            }


        //            if (sts == true)
        //            {
        //                ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Income Tax Sec 80 Request saved successfully');", true);
        //                lblMessageBoard.Text = "Income Tax Sec 80 Request saved successfully.";
        //            }
        //        }
        //        //loadTab3();
        //    }

        //    catch (Exception Ex)
        //    {
        //        switch (Ex.Message)
        //        {
        //            case "-1":
        //                ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Invalid');", true);
        //                MsgCls("Invalid", lblMessageBoard, System.Drawing.Color.Red);
        //                return;
        //                break;

        //            case "-10":
        //                ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('Record Already Exists');", true);
        //                MsgCls("Record Already Exists", lblMessageBoard, System.Drawing.Color.Red);
        //                return;
        //                break;
        //            default:
        //                break;
        //        }
        //    }
        //}

        //private DataTable GetSec80Dt()
        //{
        //    try
        //    {
        //        DataTable Dt = new DataTable("ITSEC80");
        //        Dt.Columns.Add("ITHID", typeof(int));
        //        Dt.Columns.Add("ITLID", typeof(int));
        //        Dt.Columns.Add("SBSEC", typeof(decimal));
        //        Dt.Columns.Add("SBDIV", typeof(decimal));
        //        Dt.Columns.Add("PROPCONTR", typeof(decimal));
        //        Dt.Columns.Add("ACTCONTR", typeof(decimal));
        //        Dt.Columns.Add("RECEIPT_FILE", typeof(string));
        //        Dt.Columns.Add("RECEIPT_FID", typeof(string));
        //        Dt.Columns.Add("RECEIPT_FPATH", typeof(string));
        //        Dt.Columns.Add("EMPCOMMENTS", typeof(string));

        //        return Dt;
        //    }
        //    catch (Exception Ex)
        //    { throw Ex; return null; }
        //}

        protected void GVITSec80_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int id = Convert.ToInt32(GVITSec80.DataKeys[e.RowIndex].Value.ToString());
            GridViewRow row = (GridViewRow)GVITSec80.Rows[e.RowIndex];
            Label lblID = (Label)row.FindControl("lblID");




            int LID = Convert.ToInt32(GVITSec80.DataKeys[e.RowIndex].Values[1]);
            decimal SBSEC = Convert.ToInt32(GVITSec80.DataKeys[e.RowIndex].Values[2]);
            decimal SBDIV = Convert.ToInt32(GVITSec80.DataKeys[e.RowIndex].Values[3]);
            decimal ACTCONTR;



            TextBox txtActContr = (TextBox)GVITSec80.Rows[e.RowIndex].FindControl("txtActContr");//(TextBox)row.Cells[3].Controls[0];
            ACTCONTR = Convert.ToDecimal(txtActContr.Text);
            GVITSec80.EditIndex = -1;
            //conn.Open();
            ////SqlCommand cmd = new SqlCommand("SELECT * FROM detail", conn);  
            //SqlCommand cmd = new SqlCommand("update detail set name='" + textName.Text  + "'where id='" + rid + "'", conn);
            //cmd.ExecuteNonQuery();
            //conn.Close();
            ////gvbind();




            String strConnString = ConfigurationManager.ConnectionStrings["iEmpPowerConnectionString"].ConnectionString;

            SqlConnection con = new SqlConnection(strConnString);

            SqlCommand cmd = new SqlCommand();

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.CommandText = "USP_Update_PA0585_TRANS";

            cmd.Parameters.Add("@ID", SqlDbType.Int).Value = id;

            cmd.Parameters.Add("@LID", SqlDbType.Int).Value = LID;

            cmd.Parameters.Add("@SBSEC", SqlDbType.VarChar).Value = SBSEC;

            cmd.Parameters.Add("@SBDIV", SqlDbType.VarChar).Value = SBDIV;

            cmd.Parameters.Add("@ACTCONTR", SqlDbType.Decimal).Value = ACTCONTR;

            cmd.Connection = con;

            try
            {

                con.Open();

                cmd.ExecuteNonQuery();

                lblMessageBoard.Text = "Record updated successfully";

            }

            catch (Exception ex)
            {

                throw ex;

            }

            finally
            {

                con.Close();

                con.Dispose();

            }




        }

        protected void GVITSec80C_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int id = Convert.ToInt32(GVITSec80C.DataKeys[e.RowIndex].Value.ToString());
            GridViewRow row = (GridViewRow)GVITSec80C.Rows[e.RowIndex];
            Label lblID = (Label)row.FindControl("lblID");




            int LID = Convert.ToInt32(GVITSec80C.DataKeys[e.RowIndex].Values[1]);
            decimal ITCODE = Convert.ToInt32(GVITSec80C.DataKeys[e.RowIndex].Values[2]);
            decimal ACTINVST;



            TextBox txtACTINVST = (TextBox)GVITSec80C.Rows[e.RowIndex].FindControl("txtACTINVST");//(TextBox)row.Cells[3].Controls[0];
            ACTINVST = Convert.ToDecimal(txtACTINVST.Text);
            GVITSec80C.EditIndex = -1;
            //conn.Open();
            ////SqlCommand cmd = new SqlCommand("SELECT * FROM detail", conn);  
            //SqlCommand cmd = new SqlCommand("update detail set name='" + textName.Text  + "'where id='" + rid + "'", conn);
            //cmd.ExecuteNonQuery();
            //conn.Close();
            ////gvbind();




            String strConnString = ConfigurationManager.ConnectionStrings["iEmpPowerConnectionString"].ConnectionString;

            SqlConnection con = new SqlConnection(strConnString);

            SqlCommand cmd = new SqlCommand();

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.CommandText = "Usp_Update_PA0586_TRANS";

            cmd.Parameters.Add("@ID", SqlDbType.Int).Value = id;

            cmd.Parameters.Add("@LID", SqlDbType.Int).Value = LID;

            cmd.Parameters.Add("@ITCODE", SqlDbType.VarChar).Value = ITCODE;


            cmd.Parameters.Add("@ACTINVST", SqlDbType.Decimal).Value = ACTINVST;

            cmd.Connection = con;

            try
            {

                con.Open();

                cmd.ExecuteNonQuery();

                lblMessageBoard.Text = "Record updated successfully";

            }

            catch (Exception ex)
            {

                throw ex;

            }

            finally
            {

                con.Close();

                con.Dispose();

            }




        }
    }
}