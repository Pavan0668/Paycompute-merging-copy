using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class UI_Performance_Management_System_pmsITCtemplate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            funLoadGrid();
        }
    }

    public void funLoadGrid()
    {
        try
        {
            List<string> objLst = new List<string>();
            objLst.Add("Q1");
            objLst.Add("Q2");
            objLst.Add("Q3");
            objLst.Add("Q4");
            
            //Label lbl1 = (Label)grdDevTraing.Rows[0].FindControl("lblQ1");
            //lbl1.Text = "Q1";

            //Label lbl2 = (Label)grdDevTraing.Rows[1].FindControl("lblQ2");
            //lbl2.Text = "Q2";

            //Label lbl3 = (Label)grdDevTraing.Rows[2].FindControl("lblQ3");
            //lbl3.Text = "Q3";

            //Label lbl4 = (Label)grdDevTraing.Rows[3].FindControl("lblQ4");
            //lbl4.Text = "Q4";


            DataTable dt = new DataTable();
            dt.Columns.Add("Sl No.");
            dt.Columns.Add("Development/Training Needs");
            dt.Columns.Add("Action Taken");
            dt.Columns.Add("Timeline Specified");
            dt.Columns.Add("Evaluation & Rating");

            if (objLst.Count > 0)
            {
                for (int i = 0; i <= objLst.Count - 1; i++)
                {
                    string strSlno = objLst[i].ToString();
                    string str = "";

                    DataRow dr = dt.NewRow();
                    dr["Sl No."] = strSlno;
                    dr["Development/Training Needs"] = str;
                    dr["Action Taken"] = str;
                    dr["Timeline Specified"] = str;
                    dr["Evaluation & Rating"] = str;
                    dt.Rows.Add(dr);
                }
            }

            grdDevTraing.DataSource = dt;
            grdDevTraing.DataBind();
        }
        catch (Exception ex)
        {

        }
    }
}