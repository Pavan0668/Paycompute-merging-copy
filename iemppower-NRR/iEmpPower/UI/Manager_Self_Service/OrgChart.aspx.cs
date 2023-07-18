using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace iEmpPower.UI.Manager_Self_Service
{
    public partial class OrgChart : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                getChartData();
            }
        }

        protected void getChartData()
        {
            try
            {
                msassignedtomebl objBl = new msassignedtomebl();
                List<msassignedtomebo> boList = new List<msassignedtomebo>();

                boList = objBl.Get_OrgChartData(User.Identity.Name.ToString().Trim(), Session["CompCode"].ToString().ToLower().Trim());

                if (boList == null || boList.Count == 0)
                {
                    gv_chartData.Visible = false;
                    gv_chartData.DataSource = null;
                    gv_chartData.DataBind();
                    return;
                }
                else
                {
                    gv_chartData.DataSource = null;
                    gv_chartData.DataBind();
                    gv_chartData.DataSource = boList;
                    gv_chartData.DataBind();
                }
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        }

        //protected void gv_chartData_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    try
        //    {
        //        TableCell imgCell = e.Row.Cells[4];
        //        if (imgCell.Text == "&nbsp;" || imgCell.Text == null)
        //        {
        //            imgCell.Text = "../../EmpImages/~itch1053~848.jpg";
        //        }
        //    }
        //    catch (Exception Ex)
        //    { }
        //}
    }
}