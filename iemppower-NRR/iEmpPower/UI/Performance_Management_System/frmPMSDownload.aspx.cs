using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using System.Net;
using System.IO;
using Ionic.Zip;
using System.Reflection;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Net.Mail;
using System.Text;
using AjaxControlToolkit;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Net.Mail;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;


namespace iEmpPower.UI.Performance_Management_System
{
    public partial class frmPMSDownload : System.Web.UI.Page
    {
        //
        //string path = ConfigurationManager.AppSettings["PMS"];

        //string path = "F:/PRD05072019 - PMS/iemppower-NRR/iEmpPower/UI/Performance_Management_System";
        //SqlConnection con = new SqlConnection(");
        //string path = ConfigurationManager.AppSettings[""];

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["iEmpPowerConnectionString"].ConnectionString);
        //string con = ConfigurationSettings.AppSettings("iEmpPowerConnectionString");
        protected override void OnPreRender(EventArgs e)

{

      // add base.OnPreRender(e); at the beginning of the method.

                               base.OnPreRender(e);

 

       // codes to handle with your controls.

     

}
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (!this.IsPostBack)
                {
                    PageLoadEvents();

                    ddlselectreportees();

                    //loademployeeStatus();
                    //filtervalidate();
                    foreach (GridViewRow gvr in grdrate.Rows)
                    {
                        TextBox myTextBox = (TextBox)gvr.FindControl("txtself");
                        string example = (Convert.ToDouble(myTextBox.Text)).ToString();
                        // RequiredFieldValidator rfvtxtremark = row.FindControl("rfvtxtmremarks") as RequiredFieldValidator;
                        AjaxControlToolkit.FilteredTextBoxExtender myFTBE = new AjaxControlToolkit.FilteredTextBoxExtender();
                        FilteredTextBoxExtender ftbtext = gvr.FindControl("FilteredTextBoxExtender1") as FilteredTextBoxExtender;
                        ftbtext.Enabled = true;

                    }
                }
                //filtervalidate();

            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + " - Page_Load');", true); }

        }

        public void loaddata()
        {
            try
            {
                Submit.Visible = false;
                SqlCommand cmd = new SqlCommand("usp_emp_in", con);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PERNR", User.Identity.Name);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        name.Text = dr["Employee Name"].ToString();


                        doj.Text = DateTime.Parse((dr["Date of joining"].ToString())).ToShortDateString();
                        //dr["BEGDA"].ToString();
                        totalexperience.Text = dr["Total Experience"].ToString();
                        relevantexperience.Text = dr["Relevant Experience"].ToString();
                        nextreview.Text = DateTime.Parse((dr["Next Review"].ToString())).ToShortDateString();
                        grade.Text = dr["Grade"].ToString();
                        module.Text = dr["Module"].ToString();
                        certified.Text = dr["certified"].ToString();

                    }
                }
                con.Close();
               
                SqlCommand cmd1 = new SqlCommand("USP_EMPLOYEERATING", con);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@PERNR", User.Identity.Name);

                con.Open();



                SqlDataReader sdr = cmd1.ExecuteReader();
                if (sdr.HasRows)
                {
                    grdrate.DataSource = sdr;
                    grdrate.DataBind();
                    calculatetotal();
                    Submit.Visible = true;
                }
                else
                {
                    lblloadgrid.Text = "Grade is not maintained please contact HR Manager";
                }
                con.Close();





            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + " - Page_Load' )", true); }


        }










        private void PageLoadEvents()
        {
            try
            {
                EmpData();
                //Bind_data();
                //EmpDataMgr();
                //Bind_data_mgr();
                view1.Visible = true;
                view2.Visible = false;
                view3.Visible = false;
                Tab1.CssClass = "nav-link active p-2";
                Tab2.CssClass = "nav-link p-2";
                Tab3.CssClass = "nav-link p-2";


                loaddata();
                loadmngdata();
                //loadStatus();
                // calculatetotal();
                panellock();
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + " - PageLoadEvents');", true); }
        }


        protected DataTable EmpData()
        {

            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("SLNO", typeof(string)));//for sL no
            dt.Columns.Add(new DataColumn("PERNR", typeof(string)));//for Emp ID value 
            dt.Columns.Add(new DataColumn("ENAME", typeof(string)));//for Emp name value 
            dt.Columns.Add(new DataColumn("FileName", typeof(string)));//for filename value 
            return dt;
        }





        protected void Tab2_Click(object sender, EventArgs e)
        {
            view1.Visible = false;
            view2.Visible = true;
            view3.Visible = false;
            Tab1.CssClass = "nav-link  p-2";
            Tab2.CssClass = "nav-link active p-2";
            Tab3.CssClass = "nav-link  p-2";
            //msassignedtomebo objPIDashBoardBo = new msassignedtomebo();
            //msassignedtomebl objPIDashBoardBl = new msassignedtomebl();
            //objPIDashBoardBo.PERNR = HttpContext.Current.User.Identity.Name;
            //msassignedtomecollectionbo objPIDashBoardLst = objPIDashBoardBl.Get_Sub_Employees_Of_ManagerForMSS(objPIDashBoardBo);
            ////managerupdate.Visible = objPIDashBoardLst.Count > 0 ? true : false;
            //lblinfo.Visible = objPIDashBoardLst.Count > 0 ? true : false;

            // lblRatingValues.Visible = objPIDashBoardLst.Count > 0 ? true : false;

        }

        protected void Tab1_Click(object sender, EventArgs e)
        {
            view1.Visible = true;
            view2.Visible = false;
            view3.Visible = false;
            Tab1.CssClass = "nav-link active p-2";
            Tab2.CssClass = "nav-link p-2";
            Tab3.CssClass = "nav-link  p-2";

        }

        protected void Tab3_Click(object sender, EventArgs e)
        {
            view1.Visible = false;
            view2.Visible = false;
            view3.Visible = true;
            Tab1.CssClass = "nav-link  p-2";
            Tab2.CssClass = "nav-link p-2";
            Tab3.CssClass = "nav-link active p-2";
            //msassignedtomebo objPIDashBoardBo = new msassignedtomebo();
            //msassignedtomebl objPIDashBoardBl = new msassignedtomebl();
            //objPIDashBoardBo.PERNR = HttpContext.Current.User.Identity.Name;
            //msassignedtomecollectionbo objPIDashBoardLst = objPIDashBoardBl.Get_Sub_Employees_Of_ManagerForMSS(objPIDashBoardBo);
            //managerupdate.Visible = objPIDashBoardLst.Count > 0 ? true : false;
            //btnexportall.Visible = objPIDashBoardLst.Count > 0 ? true : false;

        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {

                try
                {
                    //AjaxControlToolkit.FilteredTextBoxExtender FilteredTextBoxExtender1 = new AjaxControlToolkit.FilteredTextBoxExtender();

                    //FilteredTextBoxExtender1.Enabled = true;

                    //filtervalidate();
                    con.Open();
                    SqlCommand cmd1 = new SqlCommand("usp_header", con);
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.Parameters.AddWithValue("@PERNR", User.Identity.Name);

                    cmd1.Parameters.AddWithValue("@id", SqlDbType.Int).Direction = ParameterDirection.Output;

                    int i = cmd1.ExecuteNonQuery();
                    string eres = cmd1.Parameters["@id"].Value.ToString();

                    //int eres1 = Convert.ToInt32(cmd1.Parameters["@id"].Value);



                    con.Close();

                    con.Open();
                    if (i>0)
                    {

                        foreach (GridViewRow g1 in grdrate.Rows)
                        {

                            SqlCommand cmdd = new SqlCommand("usp_lineitem", con);
                            cmdd.CommandType = CommandType.StoredProcedure;

                            String kpi_id = g1.Cells[0].Text;

                            TextBox txtself = (TextBox)g1.Cells[3].FindControl("txtself");
                            string nt = (Convert.ToDouble(txtself.Text)).ToString("0.00");

                            TextBox txtremark = (TextBox)g1.Cells[4].FindControl("txtremark");


                            String Weightage_Value = (g1.Cells[2].Text).ToString();

                            cmdd.Parameters.AddWithValue("@id", eres);
                            cmdd.Parameters.AddWithValue("@SINO", kpi_id);
                            cmdd.Parameters.AddWithValue("@SELF_RATING", txtself.Text);
                            cmdd.Parameters.AddWithValue("@SELF_REMARKS", txtremark.Text);
                            cmdd.Parameters.AddWithValue("@WEIGHTAGE_VALUE", Weightage_Value);

                            // cmdd.Parameters.AddWithValue("@WEIGHTED_RATING", sum);




                            cmdd.ExecuteNonQuery();


                            //return i;

                            //ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Submitted successfully')", true);





                        }
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record already found')", true);
                        lblsucess.Text = "Record already found";


                    }



                        if (i > 0)
                        {
                            SendMail();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Submitted successfully')", true);

                            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Inserted Successfully')", true);
                            lblsucess.Text = "Inserted Self Rating Sucessfully";



                        }

                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record already found')", true);
                            lblsucess.Text = "Record already found";


                        }


                        con.Close();
                    }
                   
                

                catch (Exception Ex)
                { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }




                
            }
            //SendMail();
            foreach (GridViewRow g1 in grdrate.Rows)
            {
                TextBox txtself = (TextBox)g1.Cells[3].FindControl("txtself");
                TextBox txtremark = (TextBox)g1.Cells[4].FindControl("txtremark");
                txtself.Text = "";
                txtremark.Text = "";
                Label lblrating = (Label)g1.Cells[5].FindControl("lblrating") as Label;
                lblrating.Text = "";
            }
            grdrate.FooterRow.Cells[5].Text = "";

        }

        public void SendMail()
        {

            // RequiredFieldValidator rfvtxtremark = row.FindControl("rfvtxtmremarks") as RequiredFieldValidator;

            //FilteredTextBoxExtender1.Enabled = false;

            // RequiredFieldValidator rfvtxtremark = row.FindControl("rfvtxtmremarks") as RequiredFieldValidator;

            try
            {

                //filtervalidate();
                //con.Open();
                SqlCommand cmdm = new SqlCommand("usp_PMS_Mail", con);
                cmdm.CommandType = CommandType.StoredProcedure;
                cmdm.Parameters.AddWithValue("PERNR", User.Identity.Name);



                cmdm.Parameters.Add(new SqlParameter("@SupervisorMail", SqlDbType.VarChar, 100)).Direction = ParameterDirection.Output;
                cmdm.Parameters.Add(new SqlParameter("@EmpMail", SqlDbType.VarChar, 100)).Direction = ParameterDirection.Output;
                cmdm.Parameters.Add(new SqlParameter("@SupervisiorName", SqlDbType.VarChar, 100)).Direction = ParameterDirection.Output;
                cmdm.Parameters.Add(new SqlParameter("@EmpId", SqlDbType.VarChar, 8)).Direction = ParameterDirection.Output;



                SqlDataReader dr = cmdm.ExecuteReader();



                string a1 = cmdm.Parameters["@SupervisorMail"].Value.ToString();
                string a2 = cmdm.Parameters["@EmpMail"].Value.ToString();
                string a3 = cmdm.Parameters["@SupervisiorName"].Value.ToString();
                string a4 = cmdm.Parameters["@EmpId"].Value.ToString();



                cmdm.Parameters.AddWithValue("@SupervisorMail", a1);
                cmdm.Parameters.AddWithValue("@EmpMail", a2);
                cmdm.Parameters.AddWithValue("@SupervisiorName", a3);
                cmdm.Parameters.AddWithValue("@EmpId", a4);

                // string style = @"<style> .textmode { mso-number-format:\@; } </style>";
                //Response.Write(style);

                //grdrate.HeaderRow.BackColor = System.Drawing.Color.LightBlue;

                //StringWriter sw1 = new StringWriter();
                //HtmlTextWriter hw1 = new HtmlTextWriter(sw1);
                //grdrate.RenderControl(hw1);



                string to = a1;
                string from = "iemppower.itchamps@gmail.com";
                MailMessage message = new MailMessage(from, to);
                string mailbody = "Self Appraisal  has been submitted by " + name.Text + " : " + User.Identity.Name;
                message.Subject = "Self Appraisal  has been submitted by " + name.Text + " : " + User.Identity.Name;
                message.Body += mailbody;
                message.IsBodyHtml = true;
                //message.Body += sw1.ToString() + "<br/>";
                MailAddress copy = new MailAddress(a2);
                message.CC.Add(copy);


                iEmpPowerMaster_Load.masterbl.DispatchMail(a1, User.Identity.Name, message.Subject, a2, message.Body);
                //SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                //client.UseDefaultCredentials = true;
                //System.Net.NetworkCredential basicCredential1 = new System.Net.NetworkCredential("iemppower.itchamps@gmail.com", "soqofarcegkcfcev");
                //client.EnableSsl = true;
                //client.UseDefaultCredentials = true;
                //client.Credentials = basicCredential1;



                //try
                //{
                //    DispatchMail();
                //    //client.Send(message);
                //}
                //catch (Exception ex)
                //{
                //    throw ex;
                ////}
                //con.Close();
                //FilteredTextBoxExtender1.Enabled = true;
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('PMS submitted successfully by you. Error in sending mail');", true);
                return;
            }


            //FilteredTextBoxExtender1.Enabled = true;
        }






        //StringWriter sw1 = new StringWriter();
        //HtmlTextWriter hw1 = new HtmlTextWriter(sw1);
        //GVSample.RenderControl(hw1);



        public string getgrdratedata(GridView grdrate)
        {



            StringBuilder strBuilder = new StringBuilder();



            StringWriter strWriter = new StringWriter(strBuilder);



            HtmlTextWriter htw = new HtmlTextWriter(strWriter);
            string style = @"<style> .textmode { mso-number-format:\@; } </style>";
            Response.Write(style);

            grdrate.HeaderRow.BackColor = System.Drawing.Color.LightBlue;

            grdrate.RenderControl(htw);



            return strBuilder.ToString();



        }







        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */



        }




        public void loadmngdata()
        {
            try
            {
                managerupdate.Visible = false;

                SqlCommand cmd2 = new SqlCommand("usp_load_employee_details", con);
                cmd2.CommandType = CommandType.StoredProcedure;
                cmd2.Parameters.AddWithValue("@PERNR", User.Identity.Name);


                SqlDataAdapter sda = new SqlDataAdapter(cmd2);
                DataSet ds = new DataSet();
                sda.Fill(ds);





                Grdmngrating.DataSource = ds;
                Grdmngrating.DataBind();






            }

            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + " - Page_Load');", true); }
        }

        protected void btnPendingView_Click(object sender, EventArgs e)
        {
            managerupdate.Visible = true;
            int rowIndex = ((sender as LinkButton).NamingContainer as GridViewRow).RowIndex;

            //Get the value of column from the DataKeys using the RowIndex.
            int id = Convert.ToInt32(Grdmngrating.DataKeys[rowIndex].Values[0]);



            SqlCommand cmdd3 = new SqlCommand("user_sp_load_manager", con);
            cmdd3.CommandType = CommandType.StoredProcedure;
            //cmdd3.Parameters.AddWithValue("@PERNR", pernr);
            cmdd3.Parameters.AddWithValue("@id", id);

            SqlDataAdapter sda = new SqlDataAdapter(cmdd3);

            DataSet ds = new DataSet();
            sda.Fill(ds);

            Grd_mng.DataSource = ds;
            Grd_mng.DataBind();




            calculatetotalweightedrating();

        }

        protected void managerupdate_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {

                    foreach (GridViewRow g2 in Grd_mng.Rows)
                    {






                        SqlCommand cmdd = new SqlCommand("USP_pms_lineitem_mngr", con);

                        cmdd.CommandType = CommandType.StoredProcedure;
                        con.Open();

                        //int rowIndex = ((sender as LinkButton).NamingContainer as GridViewRow).RowIndex;

                        ////Get the value of column from the DataKeys using the RowIndex.
                        //int PERNR = Convert.ToInt32(Grdmngrating.DataKeys[rowIndex].Values[0]);

                        String id = g2.Cells[0].Text;
                        Session["id"] = g2.Cells[0].Text;
                        String kpi_id = g2.Cells[1].Text;

                        TextBox txtManage = (TextBox)g2.Cells[6].FindControl("txtManage");

                        TextBox txtmremarks = (TextBox)g2.Cells[7].FindControl("txtmremarks");
                        String Weightage_Value = (g2.Cells[3].Text).ToString();


                        cmdd.Parameters.AddWithValue("@id", id.ToString());
                        cmdd.Parameters.AddWithValue("@SINO", kpi_id);
                        cmdd.Parameters.AddWithValue("@Manger_Rating", txtManage.Text);
                        cmdd.Parameters.AddWithValue("@Manger_remarks", txtmremarks.Text);
                        cmdd.Parameters.AddWithValue("@WEIGHTAGE_VALUE", Weightage_Value);

                        cmdd.ExecuteNonQuery();


                        con.Close();


                        con.Open();
                        SqlCommand cmd1 = new SqlCommand("usp_header_manager", con);
                        cmd1.CommandType = CommandType.StoredProcedure;
                        //cmd1.Parameters.AddWithValue("PERNR", User.Identity.Name);

                        cmd1.Parameters.AddWithValue("@id", id.ToString());

                        int i = cmd1.ExecuteNonQuery();

                        if (i > 0)
                        {
                            SendMailmanager();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Submitted successfully')", true);
                            lblupdate.Text = "Updated Manager Rating Sucessfully";
                            

                        }
                        else
                        {
                            lblupdate.Text = "Unable to Update Manager Rating";
                        
                        }

                      
                        con.Close();
                        

                    }

                    

                }

                catch (Exception Ex)
                { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
               
            }
            //Response.Redirect("frmPMSDownload.aspx");
            
            foreach (GridViewRow g2 in Grd_mng.Rows)
            {
                TextBox txtManage = (TextBox)g2.Cells[6].FindControl("txtManage");

                TextBox txtmremarks = (TextBox)g2.Cells[7].FindControl("txtmremarks");
                txtManage.Text = "";
                txtmremarks.Text = "";
                Label lblweighrating = (Label)g2.Cells[7].FindControl("lblweighrating") as Label;
                lblweighrating.Text = "";
               

            }
            Grd_mng.FooterRow.Cells[8].Text = "";
        }
        public void SendMailmanager()
        {

            try
            {

                //AjaxControlToolkit.FilteredTextBoxExtender rfvtxtself = new AjaxControlToolkit.FilteredTextBoxExtender();

                //rfvtxtself.Enabled = false;
                //filtervalidate();
                //con.Open();
                SqlCommand cmdm = new SqlCommand("usp_PMS_Mngr_Mail", con);
                cmdm.CommandType = CommandType.StoredProcedure;
                string s = (String)Session["id"];
                cmdm.Parameters.AddWithValue("@id", s);



                cmdm.Parameters.Add(new SqlParameter("@SupervisorMail", SqlDbType.VarChar, 100)).Direction = ParameterDirection.Output;
                cmdm.Parameters.Add(new SqlParameter("@EmpMail", SqlDbType.VarChar, 100)).Direction = ParameterDirection.Output;
                //cmdm.Parameters.Add(new SqlParameter("@SupervisiorName", SqlDbType.VarChar, 100)).Direction = ParameterDirection.Output;
                cmdm.Parameters.Add(new SqlParameter("@EmpId", SqlDbType.VarChar, 8)).Direction = ParameterDirection.Output;



                SqlDataReader dr = cmdm.ExecuteReader();



                string a1 = cmdm.Parameters["@SupervisorMail"].Value.ToString();
                string a2 = cmdm.Parameters["@EmpMail"].Value.ToString();
                //string a3 = cmdm.Parameters["@SupervisiorName"].Value.ToString();
                string a4 = cmdm.Parameters["@EmpId"].Value.ToString();



                cmdm.Parameters.AddWithValue("@SupervisorMail", a1);
                cmdm.Parameters.AddWithValue("@EmpMail", a2);
                //cmdm.Parameters.AddWithValue("@SupervisiorName", a3);
                cmdm.Parameters.AddWithValue("@EmpId", a4);



                //StringWriter sw1 = new StringWriter();
                //HtmlTextWriter hw1 = new HtmlTextWriter(sw1);
                //Grd_mng.RenderControl(hw1);



                string to = a2;
                string from = "iemppower.itchamps@gmail.com";
                MailMessage message = new MailMessage(from, to);
                string mailbody = "PMS Manager's Review has been updated  by " + name.Text + " : " + User.Identity.Name;
                message.Subject = "PMS Manager's Review has been updated  by " + name.Text + " : " + User.Identity.Name;
                message.Body += mailbody;
                message.IsBodyHtml = true;
                //message.Body += sw1.ToString() + "<br/>";
                MailAddress copy = new MailAddress(a1);
                message.CC.Add(copy);

                iEmpPowerMaster_Load.masterbl.DispatchMail(a1, User.Identity.Name, message.Subject, a2, message.Body);
              
                //SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                //client.UseDefaultCredentials = true;
                //System.Net.NetworkCredential basicCredential1 = new System.Net.NetworkCredential("iemppower.itchamps@gmail.com", "soqofarcegkcfcev");
                //client.EnableSsl = true;
                //client.UseDefaultCredentials = true;
                //client.Credentials = basicCredential1;



                //try
                //{
                //    client.Send(message);
                //}
                //catch (Exception ex)
                //{
                //    throw ex;
                //}
                //con.Close();
                //rfvtxtself.Enabled = false;
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Msg", "alert('PMS submitted successfully by you. Error in sending mail');", true);
                return;
            }



        }






        //StringWriter sw1 = new StringWriter();
        //HtmlTextWriter hw1 = new HtmlTextWriter(sw1);
        //GVSample.RenderControl(hw1);



        public string getGrd_mngdata(GridView Grd_mng)
        {



            StringBuilder strBuilder = new StringBuilder();



            StringWriter strWriter = new StringWriter(strBuilder);



            HtmlTextWriter htw = new HtmlTextWriter(strWriter);



            Grd_mng.RenderControl(htw);



            return strBuilder.ToString();



        }







        //public override void VerifyRenderingInServerForm(Control control)
        //{
        //    /* Verifies that the control is rendered */



        //}


        public void loadStatus()
        {
            try
            {

                SqlCommand cmd2 = new SqlCommand("usp_load_emp_manager_status", con);
                cmd2.CommandType = CommandType.StoredProcedure;
                cmd2.Parameters.AddWithValue("@PERNR", User.Identity.Name);
                con.Open();

                SqlDataAdapter sda = new SqlDataAdapter(cmd2);
                DataSet ds = new DataSet();
                sda.Fill(ds);

                GV_Report.DataSource = ds;
                GV_Report.DataBind();


                con.Close();
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + " - Page_Load');", true); }
        }

        protected void btnStatus_Click(object sender, EventArgs e)
        {
            try
            {
                int rowIndex = ((sender as LinkButton).NamingContainer as GridViewRow).RowIndex;

                //Get the value of column from the DataKeys using the RowIndex.
                int id1 = Convert.ToInt32(GV_Report.DataKeys[rowIndex].Values[0]);



                SqlCommand cmdd3 = new SqlCommand("usp_load_report", con);
                cmdd3.CommandType = CommandType.StoredProcedure;
                cmdd3.Parameters.AddWithValue("@PERNR", User.Identity.Name);
                cmdd3.Parameters.AddWithValue("@id", id1);


                SqlDataAdapter sda = new SqlDataAdapter(cmdd3);

                DataSet ds = new DataSet();
                sda.Fill(ds);

                Gridstatus.DataSource = ds;
                Gridstatus.DataBind();

                calculatetotalstatus();

            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }

        }

        protected void txtself_TextChanged(object sender, EventArgs e)
        {
            //string Weightage_Value = row.Cells[2].Text;

            //TextBox txtself = (TextBox)row.FindControl("txtself");
            //string nt = (Convert.ToDouble(txtself.Text)).ToString("0.00");
            ////string txtself = row.Cells[3].Text;
            //Label lblrating = (Label)row.FindControl(" lblrating");

            //decimal sum = 0;
            //sum = (Convert.ToDecimal(nt) * Convert.ToInt32(Weightage_Value)) / 100;


            //row.Cells[5].Text = sum.ToString();
            //String Wrating = row.Cells[5].Text; 
            ////Label lblrating = (Label)row.FindControl("lblrating");
            //lblrating.Text = "3";
            try
            {

                //AjaxControlToolkit.FilteredTextBoxExtender FilteredTextBoxExtender1 = new AjaxControlToolkit.FilteredTextBoxExtender();

                //FilteredTextBoxExtender1.Enabled = true;

                GridViewRow row = (sender as TextBox).NamingContainer as GridViewRow;
                double Weightage_Value = double.Parse(row.Cells[2].Text);
                TextBox txtself = row.FindControl("txtself") as TextBox;
                double Selfrating = double.Parse(txtself.Text);


                if (Selfrating > 5)
                {



                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert(' Number should be less than 5')", true);



                    txtself.Text = "";


                }
                else
                {
                    Label lblrating = row.FindControl("lblrating") as Label;
                    lblrating.Text = ((Selfrating * Weightage_Value) / 100).ToString();
                }

                //Label lblrating = row.FindControl("lblrating") as Label;
                //lblrating.Text = ((Selfrating * Weightage_Value) / 100).ToString();
                TextBox txtremark = row.FindControl("txtremark") as TextBox;

                RequiredFieldValidator rfvtxtremark = row.FindControl("rfvtxtmremarks") as RequiredFieldValidator;

                if (Selfrating == 1 || Selfrating == 5)
                {
                    rfvtxtremark.Enabled = true;
                    //txtremark.Text = "please add remarks";
                    rfvtxtremark.SetFocusOnError = true;
                    rfvtxtremark.Validate();
                    txtremark.Focus();
                    //txtremark.Text = "";
                }



                else
                {



                    rfvtxtremark.Enabled = false;

                }










                double total = 0;
                foreach (GridViewRow gvr in grdrate.Rows)
                {
                    Label tb = (Label)gvr.Cells[5].FindControl("lblrating");
                    double sum;
                    if (double.TryParse(tb.Text.Trim(), out sum))
                    {
                        total += sum;
                    }
                }
                //Display  the Totals in the Footer row  
                grdrate.FooterRow.Cells[5].Text = total.ToString();
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        }

        protected void txtManage_TextChanged(object sender, EventArgs e)
        {
            try
            {

                GridViewRow row = (sender as TextBox).NamingContainer as GridViewRow;
                double Weightage_Value = double.Parse(row.Cells[3].Text);
                TextBox txtManage = row.FindControl("txtManage") as TextBox;
                double Managerrating = double.Parse(txtManage.Text);

                if (Managerrating > 5)
                {



                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert(' Number should be less Than 5')", true);



                    txtManage.Text = "";


                }


                else
                {

                    Label lblweighrating = row.FindControl("lblweighrating") as Label;
                    lblweighrating.Text = ((Managerrating * Weightage_Value) / 100).ToString();

                }


                TextBox txtremark = row.FindControl("txtmremarks") as TextBox;

                RequiredFieldValidator rfvtxtremark = row.FindControl("rfvtxtmremarks") as RequiredFieldValidator;
                //    FilteredTextBoxExtender FTB_txtREMARKS = row.FindControl("FTB_txtREMARKS") as FilteredTextBoxExtender;
                if (Managerrating == 1 || Managerrating == 5)
                {
                    rfvtxtremark.Enabled = true;
                    //txtremark.Text = "please add remarks";
                    rfvtxtremark.SetFocusOnError = true;
                    rfvtxtremark.Validate();
                    txtremark.Focus();
                    txtremark.Text = "";
                   
                }



                else
                {



                    rfvtxtremark.Enabled = false;

                }














                double total = 0;
                foreach (GridViewRow gvr in Grd_mng.Rows)
                {
                    Label tb = (Label)gvr.Cells[5].FindControl("lblweighrating");
                    double sum;
                    if (double.TryParse(tb.Text.Trim(), out sum))
                    {
                        total += sum;
                    }
                }
                //Display  the Totals in the Footer row  
                Grd_mng.FooterRow.Cells[8].Text = total.ToString();
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        }
        protected void calculatetotal()
        {
            try
            {
                double total = 0;
                foreach (GridViewRow g1 in grdrate.Rows)
                {
                    total += Convert.ToDouble(g1.Cells[2].Text);
                }
                grdrate.FooterRow.Cells[2].Text = total.ToString();
                grdrate.FooterRow.Cells[1].Text = "Grand Total";
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + " - Page_Load');", true); }

        }

        protected void calculatetotalweightedrating()
        {
            try
            {
                double total = 0;
                foreach (GridViewRow g1 in Grd_mng.Rows)
                {
                    total += Convert.ToDouble(g1.Cells[3].Text);
                }
                Grd_mng.FooterRow.Cells[3].Text = total.ToString();
                Grd_mng.FooterRow.Cells[2].Text = "Grand Total";
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }

        }
        protected void calculatetotalstatus()
        {
            try
            {
                double total = 0;


                foreach (GridViewRow g1 in Gridstatus.Rows)
                {
                    total += Convert.ToDouble(g1.Cells[10].Text);


                    Gridstatus.FooterRow.Cells[10].Text = total.ToString();
                    Gridstatus.FooterRow.Cells[9].Text = "Grand Total";
                }
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }

        }
        protected void panellock()
        {


            SqlCommand cmdd3 = new SqlCommand("usp_panel_lock", con);
            cmdd3.CommandType = CommandType.StoredProcedure;
            cmdd3.Parameters.AddWithValue("@PERNR", User.Identity.Name);

            con.Open();
            SqlDataReader dr = cmdd3.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    int status = Convert.ToInt32(dr["Lock"]);
                    if (status == 1)
                    {
                        pnllock.Visible = false;
                        lblpanellock.Text = "PMS is locked please contact HR Manager";

                    }
                    else
                    {
                        pnllock.Visible = true;

                    }


                }
                con.Close();
            }
        }
        public void ddlselectreportees()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("usp_load_emp_manager_status_dropdown1", con);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PERNR", User.Identity.Name);
                //cmd.Parameters.AddWithValue("@Query", 3);
                con.Open();
                ddlstatus.DataSource = cmd.ExecuteReader();
                ddlstatus.DataTextField = "Employee_Name";
                ddlstatus.DataValueField = "PERNR";
                ddlstatus.DataBind();
                con.Close();
                //cmd.Parameters.AddWithValue("@Query", 1);
                //ddlstatus.Items.Insert(0, new ListItem("---Select---"));
                //ddlstatus.Items.Insert(1, new ListItem("All Employees"));
                //ddlstatus.Items.Insert(2, new ListItem("Team Member"));

            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        }

        protected void Display_Click(object sender, EventArgs e)
        {
            try
            {
                //ddlstatus.Items.Insert(1, new ListItem("All Employees"));
                if (ddlstatus.SelectedItem.Text == "All Employees")
                {

                    SqlCommand cmd = new SqlCommand("usp_load_report_excel", con);
                    //int id2 = Convert.ToInt32(GV_Report.DataKeys.Values[0]);



                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PERNR", User.Identity.Name);
                    cmd.Parameters.AddWithValue("@flag", 1);
                    con.Open();
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    sda.Fill(ds);



                    excelgrid.DataSource = ds;
                    excelgrid.DataBind();




                    con.Close();


                    //SqlCommand cmd = new SqlCommand("usp_load_emp_manager_status_dropdown", con);

                    //cmd.CommandType = CommandType.StoredProcedure;
                    //cmd.Parameters.AddWithValue("@PERNR", User.Identity.Name);
                    //cmd.Parameters.AddWithValue("@Query", 2);
                    //con.Open();
                    //SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    //DataSet ds = new DataSet();
                    //sda.Fill(ds);

                    //GV_Report.DataSource = ds;
                    //GV_Report.DataBind();


                    //con.Close();


                }
                else if (ddlstatus.SelectedValue == User.Identity.Name)
                {
                    SqlCommand cmd = new SqlCommand("usp_load_emp_manager_status_dropdown", con);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PERNR", User.Identity.Name);
                    cmd.Parameters.AddWithValue("@Query", 3);
                    con.Open();
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    sda.Fill(ds);

                    GV_Report.DataSource = ds;
                    GV_Report.DataBind();


                    con.Close();

                }

                else if (ddlstatus.SelectedItem.Text == "Team Members")
                {

                    SqlCommand cmd = new SqlCommand("usp_load_report_excel", con);
                    //int id2 = Convert.ToInt32(GV_Report.DataKeys.Values[0]);



                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PERNR", User.Identity.Name);
                    cmd.Parameters.AddWithValue("@flag", 2);
                    con.Open();
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    sda.Fill(ds);



                    excelgrid.DataSource = ds;
                    excelgrid.DataBind();




                    con.Close();


                    //SqlCommand cmd = new SqlCommand("usp_load_emp_manager_status_dropdown", con);

                    //cmd.CommandType = CommandType.StoredProcedure;
                    //cmd.Parameters.AddWithValue("@PERNR", User.Identity.Name);
                    ////cmd.Parameters.AddWithValue("@Query", 1);
                    //con.Open();
                    //SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    //DataSet ds = new DataSet();
                    //sda.Fill(ds);

                    //GV_Report.DataSource = ds;
                    //GV_Report.DataBind();


                    //con.Close();

                }
                else
                {
                    SqlCommand cmd = new SqlCommand("usp_load_emp_manager_status_dropdown", con);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PERNR", ddlstatus.SelectedItem.Value);
                    cmd.Parameters.AddWithValue("@Query", 4);
                    con.Open();
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    sda.Fill(ds);

                    GV_Report.DataSource = ds;
                    GV_Report.DataBind();


                    con.Close();

                }
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }

        }
        protected void btnExport_Click(object sender, EventArgs e)
        {
            try
            {

                funConvertToExcel();

            }

            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        }

        //Response.ClearContent();
        //Response.Buffer = true;
        //Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Status.xls"));
        //Response.ContentType = "application/ms-excel";
        //StringWriter sw = new StringWriter();
        //HtmlTextWriter htw = new HtmlTextWriter(sw);
        //Gridstatus.AllowPaging = false;
        ////Change the Header Row back to white color
        ////Gridstatus.HeaderRow.Style.Add("background-color", "#FFFFFF");
        ////Applying stlye to gridview header cells
        ////for (int i = 0; i < Gridstatus.HeaderRow.Cells.Count; i++)
        ////{
        ////    Gridstatus.HeaderRow.Cells[i].Style.Add("background-color", "#df5015");
        ////}
        //Gridstatus.RenderControl(htw);
        //Response.Write(sw.ToString());
        //Response.End();


        public void funConvertToExcel()
        {
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", string.Format("attachment; filename=" + "Summary_Report" + "_RWT.xls"));
            Response.ContentType = "application/ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            Gridstatus.AllowPaging = false;
            //Change the Header Row back to white color
            //Gridstatus.HeaderRow.Style.Add("background-color", "#FFFFFF");
            //Applying stlye to gridview header cells
            //for (int i = 0; i < Gridstatus.HeaderRow.Cells.Count; i++)
            //{
            //    Gridstatus.HeaderRow.Cells[i].Style.Add("background-color", "#df5015");
            //}
            Gridstatus.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }

        //try
        //{
        //    //string date1 = DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss");
        //    System.IO.StringWriter sw = new System.IO.StringWriter();
        //    System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);

        //    string colHeads = "Status";
        //    htw.WriteEncodedText(colHeads);
        //    htw.WriteBreak();
        //    Gridstatus.GridLines = GridLines.Both;
        //    //Gridstatus.HeaderRow.BackColor = System.Drawing.Color.LightBlue;
        //    Gridstatus.RenderControl(htw);
        //    htw.WriteBreak();

        //    // Write the rendered content to a file.
        //    // string renderedGridView = "Summary_Report" + "<br>"; //+ sw.ToString();
        //    string renderedGridView = sw.ToString() + "<br/>";
        //    Response.AppendHeader("content-disposition", "attachment; filename=" + "Summary_Report" + "_RWT.xls");
        //    Response.ContentType = "Application/vnd.ms-excel";
        //    Response.Write(renderedGridView);
        //    Response.End();

        //    //fuction();

        //}




        //catch (Exception ex)
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + ex.Message + "')", true);
        //}



        //public override void VerifyRenderingInServerForm(Control control)
        //{

        //}

        protected void btnexportall_Click(object sender, EventArgs e)
        {
            try
            {

                funConvertToExcel1();

            }

            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "')", true); }
        }



        public void funConvertToExcel1()
        {
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", string.Format("attachment; filename=" + "Summary_Report1" + "_RWT.xls"));
            Response.ContentType = "application/ms-excel";
            string colHeads = "Record Working Details";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            excelgrid.AllowPaging = false;

            excelgrid.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }

        //Gridstatus.HeaderRow.BackColor = System.Drawing.Color.LightBlue;
        //    Gridstatus.RenderControl(htw);
        //    htw.WriteBreak();

        //    // Write the rendered content to a file.
        //    // string renderedGridView = "Summary_Report" + "<br>"; //+ sw.ToString();
        //    string renderedGridView = sw.ToString() + "<br/>";
        //    Response.AppendHeader("content-disposition", "attachment; filename=" + "Summary_Report" + "_RWT.xls");
        //    Response.ContentType = "Application/vnd.ms-excel";
        //    Response.Write(renderedGridView);
        //    Response.End();

        //    //fuction();

        //}




        //catch (Exception ex)
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + ex.Message + "')", true);
        //}


        //public void filtervalidate()
        //{
        //    foreach (GridViewRow gvr in grdrate.Rows)
        //    {
        //        //  gvr.Cells[3].Controls.Add(myFTBE);
        //        //TextBox myTextBox = (TextBox)gvr.FindControl("txtself");
        //        //string example = (Convert.ToDouble(myTextBox.Text)).ToString();
        //        //// RequiredFieldValidator rfvtxtremark = row.FindControl("rfvtxtmremarks") as RequiredFieldValidator;
        //        //AjaxControlToolkit.FilteredTextBoxExtender myFTBE = new AjaxControlToolkit.FilteredTextBoxExtender();
        //        ////FilteredTextBoxExtender ftbtext = gvr.FindControl("FilteredTextBoxExtender1") as FilteredTextBoxExtender;
        //    //    ftbtext.Enabled = false;
                
        //        FilteredTextBoxExtender myFTBE1=row.FindControl("rfvtxtmremarks") as RequiredFieldValidator;
        //         //if (myFTBE==)
        //        myFTBE.TargetControlID = myTextBox.ID;
        //        myFTBE.Enabled = false;
        //        //myFTBE.FilterType = AjaxControlToolkit.FilterTypes.Numbers;
        //        //myFTBE.FilterType = AjaxControlToolkit.FilterTypes.Custom;
        //        //myFTBE.ValidChars = ".";


        //        gvr.Cells[3].Controls.Add(myFTBE);
        //        TextBox myTextBox1 = (TextBox)gvr.FindControl("txtremark");
        //        AjaxControlToolkit.FilteredTextBoxExtender myFTBE1 = new AjaxControlToolkit.FilteredTextBoxExtender();
        //        myFTBE1.TargetControlID = myTextBox1.ID;
        //        myFTBE1.FilterType = AjaxControlToolkit.FilterTypes.Numbers;
        //        myFTBE1.FilterType = AjaxControlToolkit.FilterTypes.Custom;
        //        myFTBE1.FilterType = AjaxControlToolkit.FilterTypes.UppercaseLetters;
        //        myFTBE1.FilterType = AjaxControlToolkit.FilterTypes.LowercaseLetters;
        //        myFTBE1.ValidChars = "./[]()&, - @:;+=%$*";
        //        gvr.Cells[4].Controls.Add(myFTBE);
        //    }
        //}
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (this.DesignMode == true)
            {
                this.EnsureChildControls();
            }
            this.Page.RegisterRequiresControlState(this);
        }

        protected void btnpdf_Click(object sender, EventArgs e)
        {
            try
            {
                using (StringWriter sw = new StringWriter())
                {
                    using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                    {
                        Gridstatus.RenderControl(hw);
                        StringReader sr = new StringReader(sw.ToString());
                        Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                        HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
                        using (MemoryStream memoryStream = new MemoryStream())
                        {
                            PdfWriter.GetInstance(pdfDoc, memoryStream);
                            pdfDoc.Open();
                            htmlparser.Parse(sr);
                            pdfDoc.Close();
                            byte[] bytes = memoryStream.ToArray();
                            memoryStream.Close();

                            MailMessage mm = new MailMessage("iemppower.itchamps@gmail.com", "bhanu.priya@itchamps.com");
                            mm.Subject = "GridView Exported PDF";
                            mm.Body = "GridView Exported PDF Attachment";
                            mm.Attachments.Add(new Attachment(new MemoryStream(bytes), "GridViewPDF.pdf"));
                            mm.IsBodyHtml = true;
                            SmtpClient smtp = new SmtpClient();
                            smtp.Host = "smtp.gmail.com";
                            smtp.EnableSsl = true;
                            System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
                            NetworkCred.UserName = "iemppower.itchamps@gmail.com";
                            NetworkCred.Password = "soqofarcegkcfcev";
                            smtp.UseDefaultCredentials = true;
                            smtp.Credentials = NetworkCred;
                            smtp.Port = 587;
                            smtp.Send(mm);
                        }
                    }
                }
            }

            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + " - Page_Load');", true); }
        } 
    }

}




    //    }

    //}


