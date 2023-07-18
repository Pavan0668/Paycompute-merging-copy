using iEmpPower.Old_App_Code.iEmpPowerBL.FBP;
using iEmpPower.Old_App_Code.iEmpPowerBO.FBP;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace iEmpPower.UI.FBP
{
    public partial class FBP_Claims : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    pageLoadEvents();
                }
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
        }

        public void pageLoadEvents()
        {
            LoadasOnDate();
            Loadgrd_CalimsItems();
        }

        private void LoadasOnDate()
        {
            try
            {
             //   MsgCls(string.Empty, lblMessageBoard, Color.Transparent);
                DateTime now = DateTime.Now;
                DateTime startDate = new DateTime(now.Year, now.Month, 1);
                LblDate.Text = startDate.ToString("MMM yyyy");
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
        }
        private void Loadgrd_CalimsItems()
        {
            try
            {
                Fbp_Claimbl fbpblObj = new Fbp_Claimbl();
                List<FbpClaimbo> fbpboObj = new List<FbpClaimbo>();
                List<FbpClaimbo> fbpboObj1 = new List<FbpClaimbo>();
                string ApproverId = User.Identity.Name;
                fbpboObj1 = fbpblObj.Load_FbpClaim_Details(ApproverId.Trim(), "0");
                Session.Add("FbpGrdInfo", fbpboObj1);

                if (fbpboObj1 == null || fbpboObj1.Count == 0)
                {
                    MsgCls("Please declare the FBP amount before claiming !", lblmsg, Color.Red);
                    grd_CalimsItems.Visible = false;
                    grd_CalimsItems.DataSource = null;
                    grd_CalimsItems.DataBind();
                    btnApplyView.Visible = false;
                    return;
                }
                else
                {
                    grd_CalimsItems.Visible = true;
                    grd_CalimsItems.DataSource = fbpboObj1;
                    grd_CalimsItems.SelectedIndex = -1;
                    grd_CalimsItems.DataBind();
                    btnApplyView.Visible = true;
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


        protected void btnApplyView_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("FBP_Apply_ViewClaimsNew.aspx");
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('" + Ex.Message + "');", true); }
        }
    }
}