using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.DataVisualization.Charting;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using iEmpPower.UI.Ticketing_Tool;
using System.Drawing;
using iEmpPower.Old_App_Code.iEmpPowerDAL.Personal_Information;
using System.Net;
using System.IO;
using iEmpPower.Old_App_Code.iEmpPowerDAL.Ticketing_Tool;

namespace iEmpPower.UI.Ticketing_Tool
{
    public partial class TicketingTDashBoard : System.Web.UI.Page
    {

        //public static String constr = ConfigurationManager.ConnectionStrings["iEmpPowerConnectionString"].ConnectionString;
        //public static SqlConnection con = new SqlConnection(constr);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblheadassgTeam.Text = "ASSIGNED TO MY TEAM";
                lblIntnlTck.Text = "Reportees Internal Tickets";
                txtTTFromDate.Text = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddMonths(0).ToString("dd/MM/yyyy");
                txtTTToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                //new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddDays(0).ToString("dd/MM/yyyy");
                pageInit();
                BindPendingCount();
                LoadMandateDocs();
            }
            Session["TTFRMDATE"] = txtTTFromDate.Text;
            Session["TTTODATE"] = txtTTToDate.Text;
            Session["PendingPageIndex"] = "0";
            Session["DDLStatusSearch"] = "0";
        }

        protected void pageInit()
        {
            try
            {
                pnlUSerManual.Visible = false;
                msassignedtomebo objPIDashBoardBo = new msassignedtomebo();
                msassignedtomebl objPIDashBoardBl = new msassignedtomebl();
                objPIDashBoardBo.PERNR = HttpContext.Current.User.Identity.Name;
                msassignedtomecollectionbo objPIDashBoardLst = objPIDashBoardBl.Get_Sub_Employees_Of_ManagerForMSS(objPIDashBoardBo);
                if (objPIDashBoardLst.Count > 0)
                {
                    MSSchartload_agenttcks();
                    MSSchartload_progress();
                    MSSchartloadstatus();
                    MSSchartload_priority();
                    MSSchartload_client();
                    MMSchartload__reqType();
                    MMSchartload__issueType();
                    pnlAsMSS.Visible = true;
                }
                else
                {
                    pnlAsMSS.Visible = false;
                }


                int parsedValue;
                if (int.TryParse(HttpContext.Current.User.Identity.Name, out parsedValue))
                {
                    chartload();
                    chartload_priority();
                    chartload_progress();
                    chartload_client();
                    chartload_agenttcks();
                    chartload__reqType();
                    chartload__issueType();
                }

                else if (!int.TryParse(HttpContext.Current.User.Identity.Name, out parsedValue))
                {
                    pnlUSerManual.Visible = true;
                    if (User.Identity.Name.ToString().ToLower() != "cssteam")
                    {
                        chartload();
                        chartload_priority();
                        chartload_progress();
                        TTMYQUEUETask.Visible = false;
                        pnlclient.Visible = false;
                        pnlAsMSS.Visible = false;
                        pnlTiManDocs.Visible = false;
                        pnlMandateDocs.Visible = false;
                        
                    }
                    else if (User.Identity.Name.ToString().ToLower() == "cssteam")
                    {
                        lblheadassgTeam.Text = "ALL TICKETS";
                        lblIntnlTck.Text = "Internal Tickets";
                        chartload();
                        chartload_priority();
                        chartload_progress();
                        chartload_client();
                        //chartload_agenttcks();
                        chartload__reqType();
                        chartload__issueType();
                        MMSchartload__issueType();
                        MSSchartload_agenttcks();
                        MSSchartload_progress();
                        MSSchartloadstatus();
                        MSSchartload_priority();
                        MSSchartload_client();
                        MMSchartload__reqType();
                        pnlAsMSS.Visible = true;
                        pnlIssue.Visible = false;
                        pnlclient.Visible = true;
                        pnlmytckts.Visible = false;
                        
                       
                    }
                }
            }
            catch (Exception Ex) {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true);
            }
        }

        protected static DataTable ChTGetData(string query, DateTime fd, DateTime td)
        {
            try
            {
                TicketingTDashBoard tt = new TicketingTDashBoard();
                DataTable dt = new DataTable();
                TicketingTooldalDataContext tDAL = new TicketingTooldalDataContext();
                dt.Columns.Add("Statustxt", typeof(string));
                dt.Columns.Add("Value", typeof(int));
                foreach (var i in tDAL.usp_tcikety_load_all_chart(HttpContext.Current.User.Identity.Name, Convert.ToDateTime(fd), Convert.ToDateTime(td), query))
                {
                    dt.Rows.Add(i.Statustxt, i.Value);
                }
                return dt;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        //private static DataTable GetData(string query, DateTime fd, DateTime td)
        //{
        //    DataTable dt = new DataTable();
        //    SqlCommand cmd = new SqlCommand(query);
        //    SqlDataAdapter sda = new SqlDataAdapter();
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Parameters.Add("@PERNR", SqlDbType.VarChar, 250).Value = HttpContext.Current.User.Identity.Name;
        //    cmd.Parameters.Add("@frmdt", SqlDbType.Date).Value = fd;
        //    cmd.Parameters.Add("@todt", SqlDbType.Date).Value = td;
        //    cmd.Connection = con;
        //    sda.SelectCommand = cmd;
        //    sda.Fill(dt);
        //    return dt;
        //}

        protected void chartload()
        {
            string query = string.Format("usp_chart_load_all_tickets");
            DataTable dt = ChTGetData(query, Convert.ToDateTime(txtTTFromDate.Text), Convert.ToDateTime(txtTTToDate.Text));
            string[] x = new string[dt.Rows.Count];
            int[] y = new int[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                x[i] = dt.Rows[i][0].ToString();
                y[i] = Convert.ToInt32(dt.Rows[i][1]);
            }
            //this.ctl00.Series[0].BorderWidth = 1;
            //this.ctl00.Series[0].BorderColor = System.Drawing.Color.FromArgb(26, 59, 105);
            ctl00.Series[0].Points.DataBindXY(x, y);
            ctl00.Series[0].ChartType = SeriesChartType.Pie;
            ctl00.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;

            ctl00.Series[0].Points[0].Color = ColorTranslator.FromHtml("#808000");
            ctl00.Series[0].Points[1].Color = ColorTranslator.FromHtml("#469990");
            ctl00.Series[0].Points[2].Color = ColorTranslator.FromHtml("#f58231");
            ctl00.Series[0].Points[3].Color = ColorTranslator.FromHtml("#ffe119");
            ctl00.Series[0].Points[4].Color = ColorTranslator.FromHtml("#3cb44b");
            ctl00.Series[0].Points[5].Color = ColorTranslator.FromHtml("#42d4f4");
            ctl00.Series[0].Points[6].Color = ColorTranslator.FromHtml("#f032e6");
            ctl00.Series[0].Points[7].Color = ColorTranslator.FromHtml("#a9a9a9");
            ctl00.Series[0].Points[8].Color = ColorTranslator.FromHtml("#3b9143");
            ctl00.Series[0].Points[9].Color = ColorTranslator.FromHtml("#fabebe");
            ctl00.Series[0].Points[10].Color = ColorTranslator.FromHtml("#C2A48E");
            ctl00.Series[0].Points[11].Color = ColorTranslator.FromHtml("#AE875C");
            ctl00.Series[0].Points[12].Color = ColorTranslator.FromHtml("#A0522D");
            ctl00.Series[0].ToolTip = "Status - #VALX \nNo. of Tickets - #VALY ";//\nPercentage - #PERCENT{P2}
            //ctl00.Legends[0].Enabled = true;
            // ctl00.Palette = ChartColorPalette.EarthTones;
            this.ctl00.Series[0]["Exploded"] = "true";
            this.ctl00.Series[0]["PieLabelStyle"] = "Outside";
            this.ctl00.Series[0]["PieLabelStyle"] = "Disabled";
            this.ctl00.Legends.Add("Legend1");
            this.ctl00.Legends[0].Enabled = true;
            this.ctl00.Legends[0].Docking = Docking.Bottom;
            this.ctl00.Legends[0].Alignment = System.Drawing.StringAlignment.Center;

            this.ctl00.Series[0].LegendText = "#VALX - #VALY";
            //ctl00.Series[0].LegendUrl = "Default.aspx";
            //ctl00.Series[0].LabelUrl = "Default.aspx";
            ctl00.Series[0].Url = "~/UI/Ticketing Tool/IssueTracker.aspx";

            //ctl00.Series[0].LegendPostBackValue = "#VALX";
            //ctl00.Series[0].LabelPostBackValue = "#VALX";
            ctl00.Series[0].PostBackValue = "#VALX";

            //ctl00.Series[0].LegendMapAreaAttributes = "target=\"_blank\"";
            //ctl00.Series[0].LabelMapAreaAttributes = "target=\"_blank\"";
            //ctl00.Series[0].MapAreaAttributes = "target=\"_blank\"";
        }

        protected void chartload_client()
        {
            string query = string.Format("usp_chart_load_basedon_client");
            DataTable dt = ChTGetData(query, Convert.ToDateTime(txtTTFromDate.Text), Convert.ToDateTime(txtTTToDate.Text));
            string[] x = new string[dt.Rows.Count];
            int[] y = new int[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                x[i] = dt.Rows[i][0].ToString();
                y[i] = Convert.ToInt32(dt.Rows[i][1]);

            }
            this.ctclients.Series[0].BorderWidth = 1;
            this.ctclients.Series[0].BorderColor = System.Drawing.Color.FromArgb(26, 59, 105);
            ctclients.Series[0].Points.DataBindXY(x, y);
            ctclients.Series[0].ChartType = SeriesChartType.Pie;
            ctclients.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = false;
            //ctl00.Legends[0].Enabled = true;
            ctclients.Palette = ChartColorPalette.Pastel;
            this.ctclients.Series[0]["Exploded"] = "true";
            this.ctclients.Series[0]["PieLabelStyle"] = "Outside";
            this.ctclients.Legends.Add("Legend1");
            this.ctclients.Legends[0].Enabled = true;
            this.ctclients.Legends[0].Docking = Docking.Bottom;
            this.ctclients.Legends[0].Alignment = System.Drawing.StringAlignment.Center;
            this.ctclients.Series[0].ToolTip = "Client - #VALX \nNo. of Tickets - #VALY ";
            this.ctclients.Series[0].LegendText = "#VALX - #VALY";
            //ctl00.Series[0].LegendUrl = "Default.aspx";
            ctclients.Series[0].LabelUrl = "~/UI/Ticketing Tool/IssueTracker.aspx";
            ctclients.Series[0].Url = "~/UI/Ticketing Tool/IssueTracker.aspx";
            ctclients.ChartAreas["ChartArea1"].AxisX = new Axis { LabelStyle = new LabelStyle() { Font = new Font("Roboto", 9.5f) } };
            //ctl00.Series[0].LegendPostBackValue = "#VALX";
            //ctl00.Series[0].LabelPostBackValue = "#VALX";
            ctclients.Series[0].PostBackValue = "#VALX";

            //ctl00.Series[0].LegendMapAreaAttributes = "target=\"_blank\"";
            //ctl00.Series[0].LabelMapAreaAttributes = "target=\"_blank\"";
            //ctl00.Series[0].MapAreaAttributes = "target=\"_blank\"";
        }

        protected void chartload_priority()
        {
            string query = string.Format("usp_chart_load_basedon_priority");
            DataTable dt = ChTGetData(query, Convert.ToDateTime(txtTTFromDate.Text), Convert.ToDateTime(txtTTToDate.Text));
            string[] x = new string[dt.Rows.Count];
            int[] y = new int[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                x[i] = dt.Rows[i][0].ToString();
                y[i] = Convert.ToInt32(dt.Rows[i][1]);

            }
            //this.ctpriority.Series[0].BorderWidth = 1;
            //this.ctpriority.Series[0].BorderColor = System.Drawing.Color.FromArgb(26, 59, 105);
            ctpriority.Series[0].Points.DataBindXY(x, y);
            ctpriority.Series[0].ChartType = SeriesChartType.Bar;
            ctpriority.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = false;
            ctpriority.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = false;
            ctpriority.ChartAreas["ChartArea1"].AxisY.LabelAutoFitStyle = LabelAutoFitStyles.None;
            //ctl00.Legends[0].Enabled = true;
            ctpriority.Series[0].Points[0].Color = ColorTranslator.FromHtml("#65499d");
            ctpriority.Series[0].Points[1].Color = ColorTranslator.FromHtml("#4554a4");
            ctpriority.Series[0].Points[2].Color = ColorTranslator.FromHtml("#ff6347");
            ctpriority.Series[0].Points[3].Color = ColorTranslator.FromHtml("#43a047");
            this.ctpriority.Series[0].IsValueShownAsLabel = true;
            this.ctpriority.Series[0].ToolTip = "Priority - #VALX \nNo. of Tickets - #VALY ";
            this.ctpriority.Series[0].LabelForeColor = Color.Black;
            this.ctpriority.Series[0].LabelBackColor = Color.White;
            ctpriority.ChartAreas["ChartArea1"].AxisX = new Axis { LabelStyle = new LabelStyle() { Font = new Font("Roboto", 9.5f) } };
            this.ctpriority.Series[0]["Exploded"] = "true";
            this.ctpriority.Series[0]["PieLabelStyle"] = "Outside";
            // this.ctpriority.Legends.Add("Legend1");
            //this.ctpriority.Legends[0].Enabled = true;
            //this.ctpriority.Legends[0].Docking = Docking.Bottom;
            //this.ctpriority.Legends[0].Alignment = System.Drawing.StringAlignment.Center;

            //this.ctpriority.Series[0].LegendText = "#VALX-#VALY (#PERCENT{P2})";
            //ctl00.Series[0].LegendUrl = "Default.aspx";
            ctpriority.Series[0].LabelUrl = "~/UI/Ticketing Tool/IssueTracker.aspx";
            ctpriority.Series[0].Url = "~/UI/Ticketing Tool/IssueTracker.aspx";

            //ctl00.Series[0].LegendPostBackValue = "#VALX";
            //ctl00.Series[0].LabelPostBackValue = "#VALX";
            ctpriority.Series[0].PostBackValue = "#VALX";

            //ctl00.Series[0].LegendMapAreaAttributes = "target=\"_blank\"";
            //ctl00.Series[0].LabelMapAreaAttributes = "target=\"_blank\"";
            //ctl00.Series[0].MapAreaAttributes = "target=\"_blank\"";
        }

        protected void chartload_agenttcks()
        {
            string query = string.Format("usp_chart_load_basedon_agent_tickets");
            DataTable dt = ChTGetData(query, Convert.ToDateTime(txtTTFromDate.Text), Convert.ToDateTime(txtTTToDate.Text));
            string[] x = new string[dt.Rows.Count];
            int[] y = new int[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                x[i] = dt.Rows[i][0].ToString();
                y[i] = Convert.ToInt32(dt.Rows[i][1]);

            }
            //this.ctagent.Series[0].BorderWidth = 1;
            //this.ctagent.Series[0].BorderColor = System.Drawing.Color.FromArgb(26, 59, 105);
            ctagent.Series[0].Points.DataBindXY(x, y);
            ctagent.Series[0].ChartType = SeriesChartType.Doughnut;
            ctagent.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;
            //ctl00.Legends[0].Enabled = true;
            ctagent.Palette = ChartColorPalette.SemiTransparent;
            this.ctagent.Series[0].IsValueShownAsLabel = true;
            // this.ctpriority.Series[0].LabelBackColo
            this.ctagent.Series[0]["Exploded"] = "true";
            //this.ctagent.Series[0]["PieLabelStyle"] = "Outside";
            this.ctagent.Legends.Add("Legend1");
            this.ctagent.Legends[0].Enabled = false;
            this.ctagent.Legends[0].Docking = Docking.Bottom;
            this.ctagent.Legends[0].Alignment = System.Drawing.StringAlignment.Center;
            this.ctagent.Series[0].ToolTip = "Agent ID - #VALX \nNo. of Tickets - #VALY ";
            //this.ctagent.Series[0].LegendText = "#VALX-#VALY (#PERCENT{P2})";
            //ctl00.Series[0].LegendUrl = "Default.aspx";
            ctagent.Series[0].LabelUrl = "~/UI/Ticketing Tool/IssueTracker.aspx";
            ctagent.Series[0].Url = "~/UI/Ticketing Tool/IssueTracker.aspx";

            //ctl00.Series[0].LegendPostBackValue = "#VALX";
            //ctl00.Series[0].LabelPostBackValue = "#VALX";
            ctagent.Series[0].PostBackValue = "#VALX";

            //ctl00.Series[0].LegendMapAreaAttributes = "target=\"_blank\"";
            //ctl00.Series[0].LabelMapAreaAttributes = "target=\"_blank\"";
            //ctl00.Series[0].MapAreaAttributes = "target=\"_blank\"";
        }

        protected void chartload__reqType()
        {
            string query = string.Format("usp_chart_load_basedon_reqtype");
            DataTable dt = ChTGetData(query, Convert.ToDateTime(txtTTFromDate.Text), Convert.ToDateTime(txtTTToDate.Text));
            string[] x = new string[dt.Rows.Count];
            int[] y = new int[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                x[i] = dt.Rows[i][0].ToString();
                y[i] = Convert.ToInt32(dt.Rows[i][1]);

            }
            //this.ctreqType.Series[0].BorderWidth = 1;
            //this.ctreqType.Series[0].BorderColor = System.Drawing.Color.FromArgb(26, 59, 105);
            ctreqType.Series[0].Points.DataBindXY(x, y);
            ctreqType.Series[0].Points[0].Color = ColorTranslator.FromHtml("#009688");
            ctreqType.Series[0].Points[1].Color = ColorTranslator.FromHtml("#43a047");
            ctreqType.Series[0].Points[2].Color = ColorTranslator.FromHtml("#8bc34a");
            ctreqType.Series[0].Points[3].Color = ColorTranslator.FromHtml("#f8971d");
            ctreqType.Series[0].Points[4].Color = ColorTranslator.FromHtml("#B03A2E");
            ctreqType.Series[0].Points[5].Color = ColorTranslator.FromHtml("#9A7D0A");
            ctreqType.Series[0].ChartType = SeriesChartType.Column;

            this.ctreqType.Series[0].LabelForeColor = Color.Black;
            this.ctreqType.Series[0].LabelBackColor = Color.White;
            ctreqType.ChartAreas["ChartArea1"].AxisX = new Axis { LabelStyle = new LabelStyle() { Font = new Font("Roboto", 8f) } };
            ctreqType.ChartAreas["ChartArea1"].AxisY.MinorGrid.Enabled = false;
            //ctreqType.ChartAreas["ChartArea1"].AxisY.MajorGrid.Enabled = false;
            ctreqType.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = false;
            //ctl00.Legends[0].Enabled = true;
            this.ctreqType.Series[0].IsValueShownAsLabel = true;
            ctreqType.ChartAreas[0].AxisX.LabelStyle.Angle = -30;
            // this.ctpriority.Series[0].LabelBackColo
            this.ctreqType.Series[0]["Exploded"] = "true";
            //this.ctagent.Series[0]["PieLabelStyle"] = "Outside";
            //this.ctreqType.Legends.Add("Legend1");
            //this.ctreqType.Legends[0].Enabled = true;
            //this.ctreqType.Legends[0].Docking = Docking.Bottom;
            //this.ctreqType.Legends[0].Alignment = System.Drawing.StringAlignment.Center;
            this.ctreqType.Series[0].ToolTip = "Category - #VALX \nNo. of Tickets - #VALY ";
            //this.ctreqType.Series[0].LegendText = "#VALX-#VALY (#PERCENT{P2})";
            //ctl00.Series[0].LegendUrl = "Default.aspx";
            ctreqType.Series[0].LabelUrl = "~/UI/Ticketing Tool/IssueTracker.aspx";
            ctreqType.Series[0].Url = "~/UI/Ticketing Tool/IssueTracker.aspx";

            //ctl00.Series[0].LegendPostBackValue = "#VALX";
            //ctl00.Series[0].LabelPostBackValue = "#VALX";
            ctreqType.Series[0].PostBackValue = "#VALX";

            //ctl00.Series[0].LegendMapAreaAttributes = "target=\"_blank\"";
            //ctl00.Series[0].LabelMapAreaAttributes = "target=\"_blank\"";
            //ctl00.Series[0].MapAreaAttributes = "target=\"_blank\"";
        }

        protected void chartload__issueType()
        {
            string query = string.Format("usp_chart_load_basedon_issueType");
            DataTable dt = ChTGetData(query, Convert.ToDateTime(txtTTFromDate.Text), Convert.ToDateTime(txtTTToDate.Text));
            string[] x = new string[dt.Rows.Count];
            int[] y = new int[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                x[i] = dt.Rows[i][0].ToString();
                y[i] = Convert.ToInt32(dt.Rows[i][1]);
            }
            //this.Cttckprogress.Series[0].BorderWidth = 1;
            //this.Cttckprogress.Series[0].BorderColor = System.Drawing.Color.FromArgb(26, 59, 105);
            ctcissueType.Series[0].Points.DataBindXY(x, y);
            this.ctcissueType.Series[0].LabelForeColor = Color.Black;
            this.ctcissueType.Series[0].LabelBackColor = Color.White;
            
            ctcissueType.Series[0].ChartType = SeriesChartType.Bubble;
            ctcissueType.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = false;

            ctcissueType.Series[0].Palette = ChartColorPalette.Fire;
            //ctcissueType.Series[0].Points[0].Color = ColorTranslator.FromHtml("#10cf00");
            //ctcissueType.Series[0].Points[1].Color = ColorTranslator.FromHtml("#febf00");
            //ctcissueType.Series[0].Points[2].Color = ColorTranslator.FromHtml("#f75f0b");
            //ctcissueType.Series[0].Points[3].Color = ColorTranslator.FromHtml("#ea0d14");
            //ctcissueType.ChartAreas["ChartArea1"].AxisX.LabelStyle.Enabled = false;
            //ctcissueType.Legends[0].Enabled = true;
            this.ctcissueType.Series[0].IsValueShownAsLabel = true;
            //this.ctcissueType.ChartAreas["ChartArea1"].AxisX.LabelStyle. = true;
            //this.ctcissueType.PrimaryXAxis.LabelRotateAngle = 135;
            ctcissueType.ChartAreas["ChartArea1"].AxisX = new Axis { LabelStyle = new LabelStyle() { Font = new Font("Roboto", 8f) } };
            ctcissueType.ChartAreas[0].AxisX.LabelStyle.Angle = -30;
            ////Alignment for axis labels
            //this.ctcissueType.PrimaryXAxis.LabelAlignment = StringAlignment.Near;
            // this.ctpriority.Series[0].LabelBackColo
            this.ctcissueType.Series[0]["Exploded"] = "true";
            //this.ctagent.Series[0]["PieLabelStyle"] = "Outside";
            this.ctcissueType.Legends.Add("Legend1");
            this.ctcissueType.Legends[0].Enabled = false;
            this.ctcissueType.Legends[0].Docking = Docking.Bottom;
            this.ctcissueType.Legends[0].Alignment = System.Drawing.StringAlignment.Center;
            this.ctcissueType.Series[0].ToolTip = "Type - #VALX \nNo. of Tickets - #VALY";
            //this.ctcissueType.Series[0].LegendText = "1";
            //this.ctcissueType.Series[1].LegendText = "2";
            //this.ctcissueType.Series[2].LegendText = "3";
            //this.ctcissueType.Series[3].LegendText = "4";
           
           // this.ctcissueType.Series[0].LegendText = " ";
                //ctl00.Series[0].LegendUrl = "Default.aspx";
            ctcissueType.Series[0].LabelUrl = "~/UI/Ticketing Tool/IssueTracker.aspx";
            ctcissueType.Series[0].Url = "~/UI/Ticketing Tool/IssueTracker.aspx";

            //ctl00.Series[0].LegendPostBackValue = "#VALX";
            ctcissueType.Series[0].LabelPostBackValue = "#VALX";
            ctcissueType.Series[0].PostBackValue = "#VALX";

            //ctl00.Series[0].LegendMapAreaAttributes = "target=\"_blank\"";
            //ctl00.Series[0].LabelMapAreaAttributes = "target=\"_blank\"";
            //ctl00.Series[0].MapAreaAttributes = "target=\"_blank\"";
        }

        protected void chartload_progress()
        {
            string query = string.Format("usp_chart_load_progess");
            DataTable dt = ChTGetData(query, Convert.ToDateTime(txtTTFromDate.Text), Convert.ToDateTime(txtTTToDate.Text));
            string[] x = new string[dt.Rows.Count];
            int[] y = new int[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                x[i] = dt.Rows[i][0].ToString();
                y[i] = Convert.ToInt32(dt.Rows[i][1]);

            }
            //this.Cttckprogress.Series[0].BorderWidth = 1;
            //this.Cttckprogress.Series[0].BorderColor = System.Drawing.Color.FromArgb(26, 59, 105);
            Cttckprogress.Series[0].Points.DataBindXY(x, y);

            Cttckprogress.Series[0].ChartType = SeriesChartType.Doughnut;
            Cttckprogress.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;

            Cttckprogress.Series[0].Points[0].Color = ColorTranslator.FromHtml("#3bb143");
            Cttckprogress.Series[0].Points[1].Color = ColorTranslator.FromHtml("#febf00");
            Cttckprogress.Series[0].Points[2].Color = ColorTranslator.FromHtml("#f75f0b");
            Cttckprogress.Series[0].Points[3].Color = ColorTranslator.FromHtml("#ea0d14");

            //ctl00.Legends[0].Enabled = true;
            this.Cttckprogress.Series[0].IsValueShownAsLabel = true;
            // this.ctpriority.Series[0].LabelBackColo
            this.Cttckprogress.Series[0]["Exploded"] = "true";
            //this.ctagent.Series[0]["PieLabelStyle"] = "Outside";
            this.Cttckprogress.Legends.Add("Legend1");
            this.Cttckprogress.Legends[0].Enabled = true;
            this.Cttckprogress.Legends[0].Docking = Docking.Bottom;
            this.Cttckprogress.Legends[0].Alignment = System.Drawing.StringAlignment.Center;
            this.Cttckprogress.Series[0].ToolTip = "SLA - #VALX \nNo. of Tickets - #VALY";
            this.Cttckprogress.Series[0].LegendText = "#VALX";
            //ctl00.Series[0].LegendUrl = "Default.aspx";
            //ctl00.Series[0].LabelUrl = "Default.aspx";
            //Cttckprogress.Series[0].Url = "~/UI/Ticketing Tool/IssueTracker.aspx";

            //ctl00.Series[0].LegendPostBackValue = "#VALX";
            //ctl00.Series[0].LabelPostBackValue = "#VALX";
            //Cttckprogress.Series[0].PostBackValue = "#VALX";

            //ctl00.Series[0].LegendMapAreaAttributes = "target=\"_blank\"";
            //ctl00.Series[0].LabelMapAreaAttributes = "target=\"_blank\"";
            //ctl00.Series[0].MapAreaAttributes = "target=\"_blank\"";
        }

        protected void MSSchartload_progress()
        {
            string query = string.Format("usp_chart_MSS_load_progess");
            DataTable dt = ChTGetData(query, Convert.ToDateTime(txtTTFromDate.Text), Convert.ToDateTime(txtTTToDate.Text));
            string[] x = new string[dt.Rows.Count];
            int[] y = new int[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                x[i] = dt.Rows[i][0].ToString();
                y[i] = Convert.ToInt32(dt.Rows[i][1]);

            }
            //this.ctMSStckProg.Series[0].BorderWidth = 1;
            //this.ctMSStckProg.Series[0].BorderColor = System.Drawing.Color.FromArgb(26, 59, 105);
            ctMSStckProg.Series[0].Points.DataBindXY(x, y);

            ctMSStckProg.Series[0].ChartType = SeriesChartType.Doughnut;
            ctMSStckProg.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;

            ctMSStckProg.Series[0].Points[0].Color = ColorTranslator.FromHtml("#3bb143");
            ctMSStckProg.Series[0].Points[1].Color = ColorTranslator.FromHtml("#febf00");
            ctMSStckProg.Series[0].Points[2].Color = ColorTranslator.FromHtml("#f75f0b");
            ctMSStckProg.Series[0].Points[3].Color = ColorTranslator.FromHtml("#ea0d14");

            //ctl00.Legends[0].Enabled = true;
            this.ctMSStckProg.Series[0].IsValueShownAsLabel = true;
            // this.ctpriority.Series[0].LabelBackColo
            this.ctMSStckProg.Series[0]["Exploded"] = "true";
            //this.ctagent.Series[0]["PieLabelStyle"] = "Outside";
            this.ctMSStckProg.Legends.Add("Legend1");
            this.ctMSStckProg.Legends[0].Enabled = true;
            this.ctMSStckProg.Legends[0].Docking = Docking.Bottom;
            this.ctMSStckProg.Legends[0].Alignment = System.Drawing.StringAlignment.Center;
            this.ctMSStckProg.Series[0].ToolTip = "SLA - #VALX \nNo. of Tickets - #VALY";
            this.ctMSStckProg.Series[0].LegendText = "#VALX";
            //ctl00.Series[0].LegendUrl = "Default.aspx";
            //ctl00.Series[0].LabelUrl = "Default.aspx";
            //ctMSStckProg.Series[0].Url = "~/UI/Ticketing Tool/IssueTracker.aspx";

            //ctl00.Series[0].LegendPostBackValue = "#VALX";
            //ctl00.Series[0].LabelPostBackValue = "#VALX";
            //ctMSStckProg.Series[0].PostBackValue = "#VALX";

            //ctl00.Series[0].LegendMapAreaAttributes = "target=\"_blank\"";
            //ctl00.Series[0].LabelMapAreaAttributes = "target=\"_blank\"";
            //ctl00.Series[0].MapAreaAttributes = "target=\"_blank\"";
        }

        protected void MSSchartloadstatus()
        {
            string query = string.Format("usp_MSS_chart_load_Status_tickets");
            DataTable dt = ChTGetData(query, Convert.ToDateTime(txtTTFromDate.Text), Convert.ToDateTime(txtTTToDate.Text));
            string[] x = new string[dt.Rows.Count];
            int[] y = new int[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                x[i] = dt.Rows[i][0].ToString();
                y[i] = Convert.ToInt32(dt.Rows[i][1]);

            }


            //this.ctMSSTckStats.Series[0].BorderWidth = 1;
            //this.ctMSSTckStats.Series[0].BorderColor = System.Drawing.Color.FromArgb(26, 59, 105);
            ctMSSTckStats.Series[0].Points.DataBindXY(x, y);
            ctMSSTckStats.Series[0].ChartType = SeriesChartType.Pie;
            ctMSSTckStats.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;

            ctMSSTckStats.Series[0].Points[0].Color = ColorTranslator.FromHtml("#808000");
            ctMSSTckStats.Series[0].Points[1].Color = ColorTranslator.FromHtml("#469990");
            ctMSSTckStats.Series[0].Points[2].Color = ColorTranslator.FromHtml("#f58231");
            ctMSSTckStats.Series[0].Points[3].Color = ColorTranslator.FromHtml("#ffe119");
            ctMSSTckStats.Series[0].Points[4].Color = ColorTranslator.FromHtml("#3cb44b");
            ctMSSTckStats.Series[0].Points[5].Color = ColorTranslator.FromHtml("#42d4f4");
            ctMSSTckStats.Series[0].Points[6].Color = ColorTranslator.FromHtml("#f032e6");
            ctMSSTckStats.Series[0].Points[7].Color = ColorTranslator.FromHtml("#a9a9a9");
            ctMSSTckStats.Series[0].Points[8].Color = ColorTranslator.FromHtml("#3b9143");
            ctMSSTckStats.Series[0].Points[9].Color = ColorTranslator.FromHtml("#fabebe");
            ctMSSTckStats.Series[0].Points[10].Color = ColorTranslator.FromHtml("#C2A48E");
            ctMSSTckStats.Series[0].Points[11].Color = ColorTranslator.FromHtml("#AE875C");
            ctMSSTckStats.Series[0].Points[12].Color = ColorTranslator.FromHtml("#A0522D");
            ctMSSTckStats.Series[0].ToolTip = "Status - #VALX \nNo. of Tickets - #VALY ";
            //ctl00.Legends[0].Enabled = true;
            // ctl00.Palette = ChartColorPalette.EarthTones;
            this.ctMSSTckStats.Series[0]["Exploded"] = "true";
            this.ctMSSTckStats.Series[0]["PieLabelStyle"] = "Outside";
            this.ctMSSTckStats.Series[0]["PieLabelStyle"] = "Disabled";
            this.ctMSSTckStats.Legends.Add("Legend1");
            this.ctMSSTckStats.Legends[0].Enabled = true;
            this.ctMSSTckStats.Legends[0].Docking = Docking.Bottom;
            this.ctMSSTckStats.Legends[0].Alignment = System.Drawing.StringAlignment.Center;

            this.ctMSSTckStats.Series[0].LegendText = "#VALX - #VALY";
            //ctl00.Series[0].LegendUrl = "Default.aspx";
            ctMSSTckStats.Series[0].LabelUrl = "~/UI/Ticketing Tool/IssueTracker.aspx";
            ctMSSTckStats.Series[0].Url = "~/UI/Ticketing Tool/IssueTracker.aspx";

            //ctl00.Series[0].LegendPostBackValue = "#VALX";
            //ctl00.Series[0].LabelPostBackValue = "#VALX";
            ctMSSTckStats.Series[0].PostBackValue = "#VALX";

            //ctl00.Series[0].LegendMapAreaAttributes = "target=\"_blank\"";
            //ctl00.Series[0].LabelMapAreaAttributes = "target=\"_blank\"";
            //ctl00.Series[0].MapAreaAttributes = "target=\"_blank\"";
        }

        protected void MSSchartload_priority()
        {
            string query = string.Format("usp_chart_MSS_load_basedon_priority");
            DataTable dt = ChTGetData(query, Convert.ToDateTime(txtTTFromDate.Text), Convert.ToDateTime(txtTTToDate.Text));
            string[] x = new string[dt.Rows.Count];
            int[] y = new int[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                x[i] = dt.Rows[i][0].ToString();
                y[i] = Convert.ToInt32(dt.Rows[i][1]);

            }
            //this.ctMSStckPrio.Series[0].BorderWidth = 1;
            //this.ctMSStckPrio.Series[0].BorderColor = System.Drawing.Color.FromArgb(26, 59, 105);
            ctMSStckPrio.Series[0].Points.DataBindXY(x, y);
            ctMSStckPrio.Series[0].ChartType = SeriesChartType.Bar;
            ctMSStckPrio.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = false;
            ctMSStckPrio.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = false;
            ctMSStckPrio.ChartAreas["ChartArea1"].AxisY.LabelAutoFitStyle = LabelAutoFitStyles.None;
            //ctl00.Legends[0].Enabled = true;
            ctMSStckPrio.Series[0].Points[0].Color = ColorTranslator.FromHtml("#65499d");
            ctMSStckPrio.Series[0].Points[1].Color = ColorTranslator.FromHtml("#4554a4");
            ctMSStckPrio.Series[0].Points[2].Color = ColorTranslator.FromHtml("#ff6347");
            ctMSStckPrio.Series[0].Points[3].Color = ColorTranslator.FromHtml("#43a047");
            this.ctMSStckPrio.Series[0].IsValueShownAsLabel = true;
            this.ctMSStckPrio.Series[0].ToolTip = "Priority - #VALX \nNo. of Tickets - #VALY";
            this.ctMSStckPrio.Series[0].LabelForeColor = Color.Black;
            this.ctMSStckPrio.Series[0].LabelBackColor = Color.White;
            ctMSStckPrio.ChartAreas["ChartArea1"].AxisX = new Axis { LabelStyle = new LabelStyle() { Font = new Font("Roboto", 9.5f) } };
            this.ctMSStckPrio.Series[0]["Exploded"] = "true";
            this.ctMSStckPrio.Series[0]["PieLabelStyle"] = "Outside";
            // this.ctpriority.Legends.Add("Legend1");
            //this.ctpriority.Legends[0].Enabled = true;
            //this.ctpriority.Legends[0].Docking = Docking.Bottom;
            //this.ctpriority.Legends[0].Alignment = System.Drawing.StringAlignment.Center;

            //this.ctpriority.Series[0].LegendText = "#VALX-#VALY (#PERCENT{P2})";
            //ctl00.Series[0].LegendUrl = "Default.aspx";
            ctMSStckPrio.Series[0].LabelUrl = "~/UI/Ticketing Tool/IssueTracker.aspx";
            ctMSStckPrio.Series[0].Url = "~/UI/Ticketing Tool/IssueTracker.aspx";

            //ctl00.Series[0].LegendPostBackValue = "#VALX";
            //ctl00.Series[0].LabelPostBackValue = "#VALX";
            ctMSStckPrio.Series[0].PostBackValue = "#VALX";

            //ctl00.Series[0].LegendMapAreaAttributes = "target=\"_blank\"";
            //ctl00.Series[0].LabelMapAreaAttributes = "target=\"_blank\"";
            //ctl00.Series[0].MapAreaAttributes = "target=\"_blank\"";
        }

        protected void MSSchartload_client()
        {
            string query = string.Format("usp_chart_MSS_load_basedon_client");
            DataTable dt = ChTGetData(query, Convert.ToDateTime(txtTTFromDate.Text), Convert.ToDateTime(txtTTToDate.Text));
            string[] x = new string[dt.Rows.Count];
            int[] y = new int[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                x[i] = dt.Rows[i][0].ToString();
                y[i] = Convert.ToInt32(dt.Rows[i][1]);

            }
            this.ctMSSClintRTcks.Series[0].BorderWidth = 1;
            this.ctMSSClintRTcks.Series[0].BorderColor = System.Drawing.Color.FromArgb(26, 59, 105);
            ctMSSClintRTcks.Series[0].Points.DataBindXY(x, y);
            ctMSSClintRTcks.Series[0].ChartType = SeriesChartType.Pie;
            ctMSSClintRTcks.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = false;
            //ctl00.Legends[0].Enabled = true;
            ctMSSClintRTcks.ChartAreas["ChartArea1"].AxisX = new Axis { LabelStyle = new LabelStyle() { Font = new Font("Roboto", 9.5f) } };
            ctMSSClintRTcks.Palette = ChartColorPalette.Pastel;
            this.ctMSSClintRTcks.Series[0]["Exploded"] = "true";
            this.ctMSSClintRTcks.Series[0]["PieLabelStyle"] = "Outside";
            this.ctMSSClintRTcks.Legends.Add("Legend1");
            this.ctMSSClintRTcks.Legends[0].Enabled = true;
            this.ctMSSClintRTcks.Legends[0].Docking = Docking.Bottom;

            this.ctMSSClintRTcks.Legends[0].Alignment = System.Drawing.StringAlignment.Center;
            this.ctMSSClintRTcks.Series[0].ToolTip = "Client - #VALX \nNo. of Tickets - #VALY ";
            this.ctMSSClintRTcks.Series[0].LegendText = "#VALX - #VALY";
            //ctl00.Series[0].LegendUrl = "Default.aspx";
            ctMSSClintRTcks.Series[0].LabelUrl = "~/UI/Ticketing Tool/IssueTracker.aspx";
            ctMSSClintRTcks.Series[0].Url = "~/UI/Ticketing Tool/IssueTracker.aspx";

            //ctl00.Series[0].LegendPostBackValue = "#VALX";
            //ctl00.Series[0].LabelPostBackValue = "#VALX";
            ctMSSClintRTcks.Series[0].PostBackValue = "#VALX";

            //ctl00.Series[0].LegendMapAreaAttributes = "target=\"_blank\"";
            //ctl00.Series[0].LabelMapAreaAttributes = "target=\"_blank\"";
            //ctl00.Series[0].MapAreaAttributes = "target=\"_blank\"";
        }

        protected void MMSchartload__reqType()
        {
            string query = string.Format("usp_chart_MSS_load_basedon_reqtype");
            DataTable dt = ChTGetData(query, Convert.ToDateTime(txtTTFromDate.Text), Convert.ToDateTime(txtTTToDate.Text));
            string[] x = new string[dt.Rows.Count];
            int[] y = new int[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                x[i] = dt.Rows[i][0].ToString();
                y[i] = Convert.ToInt32(dt.Rows[i][1]);

            }
            //this.ctMSSreqtytcks.Series[0].BorderWidth = 1;
            //this.ctMSSreqtytcks.Series[0].BorderColor = System.Drawing.Color.FromArgb(26, 59, 105);
            ctMSSreqtytcks.Series[0].Points.DataBindXY(x, y);
            ctMSSreqtytcks.Series[0].Points[0].Color = ColorTranslator.FromHtml("#009688");
            ctMSSreqtytcks.Series[0].Points[1].Color = ColorTranslator.FromHtml("#43a047");
            ctMSSreqtytcks.Series[0].Points[2].Color = ColorTranslator.FromHtml("#8bc34a");
            ctMSSreqtytcks.Series[0].Points[3].Color = ColorTranslator.FromHtml("#f8971d");
            ctMSSreqtytcks.Series[0].Points[4].Color = ColorTranslator.FromHtml("#B03A2E");
            ctMSSreqtytcks.Series[0].Points[5].Color = ColorTranslator.FromHtml("#9A7D0A");
            ctMSSreqtytcks.Series[0].ChartType = SeriesChartType.Column;

            this.ctMSSreqtytcks.Series[0].LabelForeColor = Color.Black;
            this.ctMSSreqtytcks.Series[0].LabelBackColor = Color.White;
            ctMSSreqtytcks.ChartAreas["ChartArea1"].AxisX = new Axis { LabelStyle = new LabelStyle() { Font = new Font("Roboto", 8f) } };
            ctMSSreqtytcks.ChartAreas["ChartArea1"].AxisY.MinorGrid.Enabled = false;
            //ctreqType.ChartAreas["ChartArea1"].AxisY.MajorGrid.Enabled = false;
            ctMSSreqtytcks.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = false;
            ctMSSreqtytcks.ChartAreas[0].AxisX.LabelStyle.Angle = -30;
            //ctl00.Legends[0].Enabled = true;
            this.ctMSSreqtytcks.Series[0].IsValueShownAsLabel = true;
            // this.ctpriority.Series[0].LabelBackColo
            this.ctMSSreqtytcks.Series[0]["Exploded"] = "true";
            //this.ctagent.Series[0]["PieLabelStyle"] = "Outside";
            //this.ctreqType.Legends.Add("Legend1");
            //this.ctreqType.Legends[0].Enabled = true;
            //this.ctreqType.Legends[0].Docking = Docking.Bottom;
            //this.ctreqType.Legends[0].Alignment = System.Drawing.StringAlignment.Center;
            this.ctMSSreqtytcks.Series[0].ToolTip = "Category - #VALX \nNo. of Tickets - #VALY";
            //this.ctreqType.Series[0].LegendText = "#VALX-#VALY (#PERCENT{P2})";
            //ctl00.Series[0].LegendUrl = "Default.aspx";
            ctMSSreqtytcks.Series[0].LabelUrl = "~/UI/Ticketing Tool/IssueTracker.aspx";
            ctMSSreqtytcks.Series[0].Url = "~/UI/Ticketing Tool/IssueTracker.aspx";

            //ctl00.Series[0].LegendPostBackValue = "#VALX";
            //ctl00.Series[0].LabelPostBackValue = "#VALX";
            ctMSSreqtytcks.Series[0].PostBackValue = "#VALX";

            //ctl00.Series[0].LegendMapAreaAttributes = "target=\"_blank\"";
            //ctl00.Series[0].LabelMapAreaAttributes = "target=\"_blank\"";
            //ctl00.Series[0].MapAreaAttributes = "target=\"_blank\"";
        }

        protected void MMSchartload__issueType()
        {
            string query = string.Format("usp_chart_MSS_load_basedon_issueType");
            DataTable dt = ChTGetData(query, Convert.ToDateTime(txtTTFromDate.Text), Convert.ToDateTime(txtTTToDate.Text));
            string[] x = new string[dt.Rows.Count];
            int[] y = new int[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                x[i] = dt.Rows[i][0].ToString();
                y[i] = Convert.ToInt32(dt.Rows[i][1]);
            }
            //this.Cttckprogress.Series[0].BorderWidth = 1;
            //this.Cttckprogress.Series[0].BorderColor = System.Drawing.Color.FromArgb(26, 59, 105);
            ctMssSubIssueType.Series[0].Points.DataBindXY(x, y);
            this.ctMssSubIssueType.Series[0].LabelForeColor = Color.Black;
            this.ctMssSubIssueType.Series[0].LabelBackColor = Color.White;
            ctMssSubIssueType.Series[0].ChartType = SeriesChartType.Bubble;
            ctMssSubIssueType.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = false;

            ctMssSubIssueType.Series[0].Palette = ChartColorPalette.Fire;
            //ctMssSubIssueType.Series[0].Points[0].Color = ColorTranslator.FromHtml("#10cf00");
            //ctMssSubIssueType.Series[0].Points[1].Color = ColorTranslator.FromHtml("#febf00");
            //ctMssSubIssueType.Series[0].Points[2].Color = ColorTranslator.FromHtml("#f75f0b");
            //ctMssSubIssueType.Series[0].Points[3].Color = ColorTranslator.FromHtml("#ea0d14");
            //ctcissueType.ChartAreas["ChartArea1"].AxisX.LabelStyle.Enabled = false;
            //ctcissueType.Legends[0].Enabled = true;
            this.ctMssSubIssueType.Series[0].IsValueShownAsLabel = true;
            ctMssSubIssueType.ChartAreas["ChartArea1"].AxisX = new Axis { LabelStyle = new LabelStyle() { Font = new Font("Roboto", 8f) } };
            // this.ctpriority.Series[0].LabelBackColo
            this.ctMssSubIssueType.Series[0]["Exploded"] = "true";
            //this.ctagent.Series[0]["PieLabelStyle"] = "Outside";
            this.ctMssSubIssueType.Legends.Add("Legend1");
            this.ctMssSubIssueType.Legends[0].Enabled = false;
            this.ctMssSubIssueType.Legends[0].Docking = Docking.Bottom;
            this.ctMssSubIssueType.Legends[0].Alignment = System.Drawing.StringAlignment.Center;
            this.ctMssSubIssueType.Series[0].ToolTip = "Type - #VALX \nNo. of Tickets - #VALY";
            //this.ctcissueType.Series[0].LegendText = "1";
            //this.ctcissueType.Series[1].LegendText = "2";
            //this.ctcissueType.Series[2].LegendText = "3";
            //this.ctcissueType.Series[3].LegendText = "4";
            ctMssSubIssueType.ChartAreas[0].AxisX.LabelStyle.Angle = -30;
           // this.ctMssSubIssueType.Series[0].LegendText = " ";
                //ctl00.Series[0].LegendUrl = "Default.aspx";
            ctMssSubIssueType.Series[0].LabelUrl = "~/UI/Ticketing Tool/IssueTracker.aspx";
            ctMssSubIssueType.Series[0].Url = "~/UI/Ticketing Tool/IssueTracker.aspx";

            //ctl00.Series[0].LegendPostBackValue = "#VALX";
            ctMssSubIssueType.Series[0].LabelPostBackValue = "#VALX";
            ctMssSubIssueType.Series[0].PostBackValue = "#VALX";

            //ctl00.Series[0].LegendMapAreaAttributes = "target=\"_blank\"";
            //ctl00.Series[0].LabelMapAreaAttributes = "target=\"_blank\"";
            //ctl00.Series[0].MapAreaAttributes = "target=\"_blank\"";
        }

        protected void MSSchartload_agenttcks()
        {
            string query = string.Format("usp_chart_MSS_load_agent_tickets");
            DataTable dt = ChTGetData(query, Convert.ToDateTime(txtTTFromDate.Text), Convert.ToDateTime(txtTTToDate.Text));
            string[] x = new string[dt.Rows.Count];
            //string[] z = new string[dt.Rows.Count];
            int[] y = new int[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                x[i] = dt.Rows[i][0].ToString();
                y[i] = Convert.ToInt32(dt.Rows[i][1]);

            }
            //this.ctagent.Series[0].BorderWidth = 1;
            //this.ctagent.Series[0].BorderColor = System.Drawing.Color.FromArgb(26, 59, 105);
            ctMSSSubAgent.Series[0].Points.DataBindXY(x, y);
            ctMSSSubAgent.Series[0].ChartType = SeriesChartType.Doughnut;
            ctMSSSubAgent.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;
            //ctl00.Legends[0].Enabled = true;
            ctMSSSubAgent.Palette = ChartColorPalette.SemiTransparent;
            this.ctMSSSubAgent.Series[0].IsValueShownAsLabel = true;
            // this.ctpriority.Series[0].LabelBackColo
            this.ctMSSSubAgent.Series[0]["Exploded"] = "true";
            //this.ctagent.Series[0]["PieLabelStyle"] = "Outside";
            this.ctMSSSubAgent.Legends.Add("Legend1");
            this.ctMSSSubAgent.Legends[0].Enabled = false;
            this.ctMSSSubAgent.Legends[0].Docking = Docking.Bottom;
            this.ctMSSSubAgent.Legends[0].Alignment = System.Drawing.StringAlignment.Center;
            this.ctMSSSubAgent.Series[0].ToolTip = "Agent ID | Name - #VALX \nNo. of Tickets - #VALY";
            //this.ctagent.Series[0].LegendText = "#VALX-#VALY (#PERCENT{P2})";
            //ctl00.Series[0].LegendUrl = "Default.aspx";
            ctMSSSubAgent.Series[0].LabelUrl = "~/UI/Ticketing Tool/IssueTracker.aspx";
            ctMSSSubAgent.Series[0].Url = "~/UI/Ticketing Tool/IssueTracker.aspx";

            //ctl00.Series[0].LegendPostBackValue = "#VALX";
            //ctl00.Series[0].LabelPostBackValue = "#VALX";
            ctMSSSubAgent.Series[0].PostBackValue = "#VALX";

            //ctl00.Series[0].LegendMapAreaAttributes = "target=\"_blank\"";
            //ctl00.Series[0].LabelMapAreaAttributes = "target=\"_blank\"";
            //ctl00.Series[0].MapAreaAttributes = "target=\"_blank\"";
        }

        protected void txtTTFromDate_TextChanged(object sender, EventArgs e)
        {
            DateTime fd = Convert.ToDateTime(txtTTFromDate.Text);
            DateTime td = Convert.ToDateTime(txtTTToDate.Text);

            int a = DateTime.Compare(fd, td);
            if (a > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('From date should be less than to date');", true);
                txtTTToDate.Text = txtTTFromDate.Text;
            }
            pageInit();
        }

        protected void txtTTToDate_TextChanged(object sender, EventArgs e)
        {
            DateTime fd = Convert.ToDateTime(txtTTFromDate.Text);
            DateTime td = Convert.ToDateTime(txtTTToDate.Text);

            int a = DateTime.Compare(fd, td);
            if (a > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('From date should be less than to date');", true);
                txtTTToDate.Text = txtTTFromDate.Text;
            }
            pageInit();
        }

        protected void Chart1_Click(object sender, ImageMapEventArgs e)
        {
            try
            {

                Session["chartNo"] = 1;
                TicketingToolbo obj = new TicketingToolbo();
                obj.POSTEDVALUE = e.PostBackValue;
                obj.USER = User.Identity.Name;
                obj.CHARTNO = 1;
                Session["TktStutsID"] = GetPostBkVal(obj);
                //Session["chartNo"] = 1;
                //string status = e.PostBackValue;

                //String constr = ConfigurationManager.ConnectionStrings["iEmpPowerConnectionString"].ConnectionString;
                //SqlConnection con = new SqlConnection(constr);
                //con.Open();
                //SqlCommand cmd = new SqlCommand("Select StatusID FROM dbo.Tickety_Status where StatusTxt='" + status + "' ", con);
                //SqlDataReader reader = cmd.ExecuteReader();

                //if (reader.Read())
                //{
                //    Session["TktStutsID"] = reader["StatusID"].ToString() == "" ? "0" : reader["StatusID"].ToString();
                //}
                //con.Close();
            }
            catch (Exception ex) { }
        }

        protected void ctpriority_Click(object sender, ImageMapEventArgs e)
        {
            try
            {

                Session["chartNo"] = 2;
                TicketingToolbo obj = new TicketingToolbo();
                obj.POSTEDVALUE = e.PostBackValue;
                obj.USER = User.Identity.Name;
                obj.CHARTNO = 2;
                Session["TktStutsID"] = GetPostBkVal(obj);
                //Session["chartNo"] = 2;
                //string status = e.PostBackValue;

                //String constr = ConfigurationManager.ConnectionStrings["iEmpPowerConnectionString"].ConnectionString;
                //SqlConnection con = new SqlConnection(constr);
                //con.Open();
                //SqlCommand cmd = new SqlCommand("Select PriorityID FROM dbo.Tickety_Priority where PriorityTxt='" + status + "' ", con);
                //SqlDataReader reader = cmd.ExecuteReader();

                //if (reader.Read())
                //{
                //    Session["TktStutsID"] = reader["PriorityID"].ToString() == "" ? "0" : reader["PriorityID"].ToString();
                //}
                //con.Close();
            }
            catch (Exception ex) { }
        }

        protected void Cttckprogress_Click(object sender, ImageMapEventArgs e)
        {
            Session["chartNo"] = 0;//3
        }

        protected void ctreqType_Click(object sender, ImageMapEventArgs e)
        {
            try
            {
                Session["chartNo"] = 4;
                TicketingToolbo obj = new TicketingToolbo();
                obj.POSTEDVALUE = e.PostBackValue;
                obj.USER = User.Identity.Name;
                obj.CHARTNO = 4;
                Session["TktStutsID"] = GetPostBkVal(obj);
                //Session["chartNo"] = 4;
                //string status = e.PostBackValue;

                //String constr = ConfigurationManager.ConnectionStrings["iEmpPowerConnectionString"].ConnectionString;
                //SqlConnection con = new SqlConnection(constr);
                //con.Open();
                //SqlCommand cmd = new SqlCommand("Select CategoryID FROM dbo.Tickety_Category where CategoryTxt='" + status + "' ", con);
                //SqlDataReader reader = cmd.ExecuteReader();

                //if (reader.Read())
                //{
                //    Session["TktStutsID"] = reader["CategoryID"].ToString() == "" ? "0" : reader["CategoryID"].ToString();
                //}
                //con.Close();
            }
            catch (Exception ex) { }
        }

        protected void ctclients_Click(object sender, ImageMapEventArgs e)
        {
            try
            {
                Session["chartNo"] = 5;
                string status = e.PostBackValue;
                Session["TktStutsID"] = e.PostBackValue;
                //String constr = ConfigurationManager.ConnectionStrings["iEmpPowerConnectionString"].ConnectionString;
                //SqlConnection con = new SqlConnection(constr);
                //con.Open();
                //SqlCommand cmd = new SqlCommand("Select CategoryID FROM dbo.Tickety_Category where CategoryTxt='" + status + "' ", con);
                //SqlDataReader reader = cmd.ExecuteReader();

                //if (reader.Read())
                //{
                //    Session["TktStutsID"] = reader["CategoryID"].ToString() == "" ? "0" : reader["CategoryID"].ToString();
                //}
                //con.Close();
            }
            catch (Exception ex) { }
        }

        protected void ctagent_Click(object sender, ImageMapEventArgs e)
        {
            try
            {
                Session["chartNo"] = 6;
                
                Session["TktStutsID"] = e.PostBackValue;
                
            }
            catch (Exception ex) { }
        }

        protected void ctMSSTckStats_Click(object sender, ImageMapEventArgs e)
        {
            try
            {
                Session["chartNo"] = 7;
                TicketingToolbo obj = new TicketingToolbo();
                obj.POSTEDVALUE = e.PostBackValue;
                obj.USER = User.Identity.Name;
                obj.CHARTNO = 7;
                Session["TktStutsID"] = GetPostBkVal(obj);
                //Session["chartNo"] = 7;
                //string status = e.PostBackValue;

                //String constr = ConfigurationManager.ConnectionStrings["iEmpPowerConnectionString"].ConnectionString;
                //SqlConnection con = new SqlConnection(constr);
                //con.Open();
                //SqlCommand cmd = new SqlCommand("Select StatusID FROM dbo.Tickety_Status where StatusTxt='" + status + "' ", con);
                //SqlDataReader reader = cmd.ExecuteReader();

                //if (reader.Read())
                //{
                //    Session["TktStutsID"] = reader["StatusID"].ToString() == "" ? "0" : reader["StatusID"].ToString();
                //}
                //con.Close();
            }
            catch (Exception ex) { }
        }

        protected void ctMSStckPrio_Click(object sender, ImageMapEventArgs e)
        {
            try
            {

                Session["chartNo"] = 8;
                TicketingToolbo obj = new TicketingToolbo();
                obj.POSTEDVALUE = e.PostBackValue;
                obj.USER = User.Identity.Name;
                obj.CHARTNO = 8;
                Session["TktStutsID"] = GetPostBkVal(obj);
                //Session["chartNo"] = 8;
                //string status = e.PostBackValue;

                //String constr = ConfigurationManager.ConnectionStrings["iEmpPowerConnectionString"].ConnectionString;
                //SqlConnection con = new SqlConnection(constr);
                //con.Open();
                //SqlCommand cmd = new SqlCommand("Select PriorityID FROM dbo.Tickety_Priority where PriorityTxt='" + status + "' ", con);
                //SqlDataReader reader = cmd.ExecuteReader();

                //if (reader.Read())
                //{
                //    Session["TktStutsID"] = reader["PriorityID"].ToString() == "" ? "0" : reader["PriorityID"].ToString();
                //}
                //con.Close();
            }
            catch (Exception ex) { }
        }

        protected void ctMSStckProg_Click(object sender, ImageMapEventArgs e)
        {

        }

        protected void ctMSSreqtytcks_Click(object sender, ImageMapEventArgs e)
        {
            try
            {
                Session["chartNo"] = 10;
                TicketingToolbo obj = new TicketingToolbo();
                obj.POSTEDVALUE = e.PostBackValue;
                obj.USER = User.Identity.Name;
                obj.CHARTNO = 10;
                Session["TktStutsID"] = GetPostBkVal(obj);
                //Session["chartNo"] = 10;
                //string status = e.PostBackValue;

                //String constr = ConfigurationManager.ConnectionStrings["iEmpPowerConnectionString"].ConnectionString;
                //SqlConnection con = new SqlConnection(constr);
                //con.Open();
                //SqlCommand cmd = new SqlCommand("Select CategoryID FROM dbo.Tickety_Category where CategoryTxt='" + status + "' ", con);
                //SqlDataReader reader = cmd.ExecuteReader();

                //if (reader.Read())
                //{
                //    Session["TktStutsID"] = reader["CategoryID"].ToString() == "" ? "0" : reader["CategoryID"].ToString();
                //}
                //con.Close();
            }
            catch (Exception ex) { }
        }

        protected void ctMSSClintRTcks_Click(object sender, ImageMapEventArgs e)
        {
            Session["chartNo"] = 11;
            //string status = e.PostBackValue;
            Session["TktStutsID"] = e.PostBackValue;
        }

        protected void ctMSSSubAgent_Click(object sender, ImageMapEventArgs e)
        {
            Session["chartNo"] = 12;
            string status = e.PostBackValue;
            string result = status.Substring(0, 8).TrimStart();
            Session["TktStutsID"] = result;
        }

        //private static DataTable GetData_Client(string query)
        //{
        //    DataTable dt = new DataTable();
        //    SqlCommand cmd = new SqlCommand(query);
        //    SqlDataAdapter sda = new SqlDataAdapter();
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Parameters.Add("@PERNR", SqlDbType.VarChar, 250).Value = HttpContext.Current.User.Identity.Name;
        //    cmd.Connection = con;
        //    sda.SelectCommand = cmd;
        //    sda.Fill(dt);
        //    return dt;
        //}

        //private static DataTable GetData_Client(string query)
        //{
        //    DataTable dt = new DataTable();
        //    SqlCommand cmd = new SqlCommand(query);                      
        //    SqlDataAdapter sda = new SqlDataAdapter();           
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Parameters.Add("@PERNR", SqlDbType.VarChar, 250).Value = HttpContext.Current.User.Identity.Name;
        //    cmd.Connection = con;
        //    sda.SelectCommand = cmd;
        //    sda.Fill(dt);
        //    return dt;
        //}

        protected void Button1_Click(object sender, EventArgs e)
        {
            Session["TransferdTicketId"] = "New";
            Session["TransferdValue"] = "0";
            Response.Redirect("~/UI/Ticketing Tool/CreateIssueTicket.aspx");
        }

        public void BindPendingCount()
        {
            string Leavecount = "", RWTcount = "";
            string TTMYQUEUECOUNT = "";
            string MyPendAllCnt = "";
            string MyPendLeavCnt = "";
            string MyPendTMSCnt = "";
            string MyPendingTask = "";
            string RRF = "";
            pidashboarddalDataContext objPIDashBoardDataContext = new pidashboarddalDataContext();


            //objPIDashBoardDataContext.Usp_Pending_for_Approval_Count(User.Identity.Name, ref  Leavecount, ref RWTcount, ref TTMYQUEUECOUNT, ref MyPendAllCnt, ref MyPendLeavCnt, ref MyPendTMSCnt, ref MyPendingTask, ref RRF);


            //TTMYQUEUE.InnerText = "Pending Tickets : " + TTMYQUEUECOUNT;
            //TTMYQUEUETask.InnerText = "Pending Tasks : " + MyPendingTask;

        }

        //protected void hypDocFS_Click(object sender, EventArgs e)
        //{
        //    download("~/UI/Document Management System/Documents/FS.docx");
        //}

        //protected void hypDocTS_Click(object sender, EventArgs e)
        //{
        //    download("~/UI/Document Management System/Documents/TS.doc");
        //}

        //protected void hypCodeRw_Click(object sender, EventArgs e)
        //{
        //    download("~/UI/Document Management System/Documents/Code Review Check List.xlsx");
        //}

        //protected void hypTstD_Click(object sender, EventArgs e)
        //{
        //    download("~/UI/Document Management System/Documents/Code_Review_Test_Defect_log.xlsx");
        //}

        //protected void download(string strURL)
        //{
        //    try
        //    {
        //        WebClient req = new WebClient();
        //        HttpResponse response = HttpContext.Current.Response;

        //        response.Clear();
        //        response.ClearContent();
        //        response.ClearHeaders();
        //        response.Buffer = true;
        //        response.ContentType = "application/" + Path.GetExtension(strURL);
        //        response.AddHeader("Content-Disposition", "attachment;filename=\"" + Path.GetFileName(strURL) + "\"");
        //        byte[] data = req.DownloadData(Server.MapPath(strURL));

        //        response.BinaryWrite(data);
        //        response.End();
        //    }
        //    catch (Exception e)
        //    {
        //    }
        //}

        //protected void hypTRF_Click(object sender, EventArgs e)
        //{
        //    download("~/UI/Document Management System/Documents/TR_Form.xlsx");
        //}

        public void LoadMandateDocs()
        {
            try
            {
                TicketingToolbo TicketingObjBo = new TicketingToolbo();
                TicketingToolbl TicketingObjBl = new TicketingToolbl();
                List<TicketingToolbo> TicketingboList = new List<TicketingToolbo>();

                TicketingboList = TicketingObjBl.LoadMandateDocs(User.Identity.Name.ToString().Trim());

                if (TicketingboList == null || TicketingboList.Count == 0)
                {
                   // MsgCls("No Records Found !", LblTicket, System.Drawing.Color.Red);
                    GrdMandteDocs.Visible = true;
                    GrdMandteDocs.DataSource = null;
                    GrdMandteDocs.DataBind();
                    return;
                }
                else
                {
                    //MsgCls("", LblTicket, System.Drawing.Color.Transparent);

                    GrdMandteDocs.DataSource = null;
                    GrdMandteDocs.DataBind();
                    GrdMandteDocs.DataSource = TicketingboList;
                    GrdMandteDocs.SelectedIndex = -1;
                    GrdMandteDocs.DataBind();

                }
            }
            catch (Exception Ex)
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + Ex.Message + "');", true); }
        }

        protected void GrdMandteDocs_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName.ToUpper())
            {
                case "DOWNLOAD":

                    string strURL = e.CommandArgument.ToString(); ;
                    WebClient req = new WebClient();
                    HttpResponse response = HttpContext.Current.Response;
                    response.Clear();
                    response.ClearContent();
                    response.ClearHeaders();
                    response.Buffer = true;
                    response.ContentType = "application/" + Path.GetExtension(strURL);
                    response.AddHeader("Content-Disposition", "attachment;filename=\"" + Path.GetFileName(strURL) + "\"");
                    byte[] data = req.DownloadData(Server.MapPath(strURL));
                    response.BinaryWrite(data);
                    response.End();
                    break;
            }
        }

        protected void ctcissueType_Click(object sender, ImageMapEventArgs e)
        {
            try
            {
                Session["chartNo"] = 13;
                TicketingToolbo obj = new TicketingToolbo();
                obj.POSTEDVALUE = e.PostBackValue;
                obj.USER = User.Identity.Name;
                obj.CHARTNO = 13;
                Session["TktStutsID"] = GetPostBkVal(obj);
                //Session["chartNo"] = 13;
                //string status = e.PostBackValue;

                //String constr = ConfigurationManager.ConnectionStrings["iEmpPowerConnectionString"].ConnectionString;
                //SqlConnection con = new SqlConnection(constr);
                //con.Open();
                //SqlCommand cmd = new SqlCommand("Select IssueTypeID FROM dbo.Tickety_IssueType where IssueTypeTxt='" + status + "' ", con);
                //SqlDataReader reader = cmd.ExecuteReader();

                //if (reader.Read())
                //{
                //    Session["TktStutsID"] = reader["IssueTypeID"].ToString() == "" ? "0" : reader["IssueTypeID"].ToString();
                //}
                //con.Close();
            }
            catch (Exception ex) { }
        }

        protected void ctMssSubIssueType_Click(object sender, ImageMapEventArgs e)
        {
            try
            {
                Session["chartNo"] = 14;
                TicketingToolbo obj = new TicketingToolbo();
                obj.POSTEDVALUE = e.PostBackValue;
                obj.USER = User.Identity.Name;
                obj.CHARTNO = 14;
                Session["TktStutsID"] = GetPostBkVal(obj);
                //Session["chartNo"] = 14;
                //string status = e.PostBackValue;

                //String constr = ConfigurationManager.ConnectionStrings["iEmpPowerConnectionString"].ConnectionString;
                //SqlConnection con = new SqlConnection(constr);
                //con.Open();
                //SqlCommand cmd = new SqlCommand("Select IssueTypeID FROM dbo.Tickety_IssueType where IssueTypeTxt='" + status + "' ", con);
                //SqlDataReader reader = cmd.ExecuteReader();

                //if (reader.Read())
                //{
                //    Session["TktStutsID"] = reader["IssueTypeID"].ToString() == "" ? "0" : reader["IssueTypeID"].ToString();
                //}
                //con.Close();
            }
            catch (Exception ex) { }
        }

        protected string GetPostBkVal(TicketingToolbo obj)
        {
            TicketingToolbl objBl = new TicketingToolbl();
            string outp = "";
            objBl.GetPostBackValue(obj, ref outp);
            return outp;
        }
    }
}