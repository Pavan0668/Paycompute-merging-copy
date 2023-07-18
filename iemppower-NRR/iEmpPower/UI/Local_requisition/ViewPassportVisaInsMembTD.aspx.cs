using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Drawing;
using System.Configuration;



namespace iEmpPower.UI.Local_requisition
{
    public partial class ViewPassportVisaInsMembTD : System.Web.UI.Page
    {
        string dir = "";
        byte[] myByteArray = new byte[200];
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //LoadGridDetails();

                if (tcDefalut.ActiveTab == tabPPassport)
                {
                    //pipersonalidscollectionbo objPersonalIDsLst = (pipersonalidscollectionbo)Session["grdLst"];

                    //var sTypes = from col in objPersonalIDsLst

                    //             select col;//where col.ICTYPE == "9" || col.ICTYPE == "8"

                    //grdPassport.DataSource = sTypes;
                    //grdPassport.DataBind();
                    LoadPASSPORT();

                }
            }
        }

        //protected void LoadGridDetails()
        //{
        //    pipersonalidsbl objPersonalIDsBl = new pipersonalidsbl();
        //    pipersonalidsbo objPersonalIDsBo = new pipersonalidsbo();
        //    objPersonalIDsBo.PERNR = User.Identity.Name;
        //    pipersonalidscollectionbo objPersonalIDsLst = objPersonalIDsBl.Get_ALLPersonalIDSDetails_TD();

        //    Session.Add("grdLst", objPersonalIDsLst);
        //}

        protected void LoadFLYER_NUMBER()
        {
            pipersonalidsbl objPersonalIDsBl = new pipersonalidsbl();
            pipersonalidsbo objPersonalIDsBo = new pipersonalidsbo();
            objPersonalIDsBo.PERNR = "";// User.Identity.Name;

            pipersonalidscollectionbo objPersonalIDsLst = objPersonalIDsBl.Get_FLYER_NUMBER(objPersonalIDsBo);

            //Session.Add("grdLst", objPersonalIDsLst);
            grdFLYER_NUMBER.DataSource = objPersonalIDsLst;
            grdFLYER_NUMBER.DataBind();
        }

        protected void LoadPASSPORT()
        {
            pipersonalidsbl objPersonalIDsBl = new pipersonalidsbl();
            pipersonalidsbo objPersonalIDsBo = new pipersonalidsbo();
            objPersonalIDsBo.PERNR = "";// User.Identity.Name;
            pipersonalidscollectionbo objPersonalIDsLst = objPersonalIDsBl.Get_PASSPORT(objPersonalIDsBo);

            //Session.Add("grdLst", objPersonalIDsLst);
            grdPassport.DataSource = objPersonalIDsLst;
            grdPassport.DataBind();
        }

        protected void LoadTRAVEL_INS()
        {
            pipersonalidsbl objPersonalIDsBl = new pipersonalidsbl();
            pipersonalidsbo objPersonalIDsBo = new pipersonalidsbo();
            objPersonalIDsBo.PERNR = "";// User.Identity.Name;
            pipersonalidscollectionbo objPersonalIDsLst = objPersonalIDsBl.Get_TRAVEL_INS(objPersonalIDsBo);

            //Session.Add("grdLst", objPersonalIDsLst);
            grdTravelIns.DataSource = objPersonalIDsLst;
            grdTravelIns.DataBind();
        }


        protected void LoadVISA()
        {
            pipersonalidsbl objPersonalIDsBl = new pipersonalidsbl();
            pipersonalidsbo objPersonalIDsBo = new pipersonalidsbo();
            objPersonalIDsBo.PERNR = "";// User.Identity.Name;
            pipersonalidscollectionbo objPersonalIDsLst = objPersonalIDsBl.Get_VISA(objPersonalIDsBo);

            //Session.Add("grdLst", objPersonalIDsLst);
            grdVisa.DataSource = objPersonalIDsLst;
            grdVisa.DataBind();
        }

        protected void tcDefalut_ActiveTabChanged(object sender, EventArgs e)
        {
            if (tcDefalut.ActiveTab == tabPPassport)
            {
                //pipersonalidscollectionbo objPersonalIDsLst = (pipersonalidscollectionbo)Session["grdLst"];

                //var sTypes = from col in objPersonalIDsLst

                //             select col;//where col.ICTYPE == "9" || col.ICTYPE == "8"

                //grdPassport.DataSource = sTypes;
                //grdPassport.DataBind();

                LoadPASSPORT();

            }
            else if (tcDefalut.ActiveTab == tabPVisa)
            {
                // pipersonalidscollectionbo objPersonalIDsLst = (pipersonalidscollectionbo)Session["grdLst"];

                // var sTypes = from col in objPersonalIDsLst

                //              select col;//where col.ICTYPE == "10"

                //grdVisa.DataSource = sTypes;
                //grdVisa.DataBind();
                LoadVISA();
            }
            else if (tcDefalut.ActiveTab == tabPTravelIns)
            {
                //  pipersonalidscollectionbo objPersonalIDsLst = (pipersonalidscollectionbo)Session["grdLst"];

                //  var sTypes = from col in objPersonalIDsLst

                //               select col;// where col.ICTYPE == "13"

                //grdTravelIns.DataSource = sTypes;
                //grdTravelIns.DataBind();
                LoadTRAVEL_INS();
            }

            else if (tcDefalut.ActiveTab == tabPFLYER_NUMBER)
            {
                //pipersonalidscollectionbo objPersonalIDsLst = (pipersonalidscollectionbo)Session["grdLst"];

                //var sTypes = from col in objPersonalIDsLst

                //             select col;//where col.ICTYPE == "11"

                //grdFLYER_NUMBER.DataSource = sTypes;
                //grdFLYER_NUMBER.DataBind();

                LoadFLYER_NUMBER();
            }


            //else if (tcDefalut.ActiveTab == tabPMembershipNum)
            //{
            //    pipersonalidscollectionbo objPersonalIDsLst = (pipersonalidscollectionbo)Session["grdLst"];

            //    var sTypes = from col in objPersonalIDsLst
            //                 where col.ICTYPE == "11"
            //                 select col;

            //  grdMembership.DataSource = sTypes;
            //  grdMembership.DataBind();
            //}
        }

        protected void lnkExportAll_Click(object sender, EventArgs e)
        {
            ExportToExcel();
        }

        protected void ExportToExcel()
        {
            // DirectoryInfo dirPath = new DirectoryInfo(@"I:\iEmpPower_Tempfiles\");
            DirectoryInfo dirPath = new DirectoryInfo(@"D:\iEmpPower_ExcelFiles\");
            if (!Directory.Exists(dirPath.ToString()))
            {
                Directory.CreateDirectory(dirPath.ToString());
            }
            DirectoryInfo dirChildPath = new DirectoryInfo(dirPath + DateTime.Now.Date.ToString("dd-MMM-yyyy"));
            if (!Directory.Exists(dirChildPath.ToString()))
            {
                Directory.CreateDirectory(dirChildPath.ToString());
            }


            System.IO.StringWriter sw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);




            ////removepaging();

            

            // Render grid view control.
            grdPassport.RenderControl(htw);

            ////Get_ExpenseDetails();

            // Write the rendered content to a file.
            string renderedGridView = "Passport details"+"<br>"+sw.ToString();

            dir = dirChildPath.ToString();
            System.IO.File.WriteAllText(@dir + "\\" + "Passport details.xls", renderedGridView);

            System.Text.Encoding enc = System.Text.Encoding.ASCII;
            myByteArray = enc.GetBytes(sw.ToString());

            lblMessage.ForeColor = System.Drawing.Color.Green;
            lblMessage.Text = "Exported to " + dir;

             Response.AppendHeader( "content-disposition","attachment; filename=" + "Passport details.xls");
             Response.ContentType = "Application/vnd.ms-excel";
             Response.Write(renderedGridView);
             Response.End();
        }
        protected void lnkVisa_Click(object sender, EventArgs e)
        {
            ExportToVisaExcel();
        }

        protected void ExportToVisaExcel()
        {
            // DirectoryInfo dirPath = new DirectoryInfo(@"I:\iEmpPower_Tempfiles\");
            DirectoryInfo dirPath = new DirectoryInfo(@"D:\iEmpPower_ExcelFiles\");
            if (!Directory.Exists(dirPath.ToString()))
            {
                Directory.CreateDirectory(dirPath.ToString());
            }
            DirectoryInfo dirChildPath = new DirectoryInfo(dirPath + DateTime.Now.Date.ToString("dd-MMM-yyyy"));
            if (!Directory.Exists(dirChildPath.ToString()))
            {
                Directory.CreateDirectory(dirChildPath.ToString());
            }


            System.IO.StringWriter sw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);




            ////removepaging();



            // Render grid view control.
            grdVisa.RenderControl(htw);

            ////Get_ExpenseDetails();

            // Write the rendered content to a file.
            string renderedGridView = "Visa details" + "<br>" + sw.ToString();

            dir = dirChildPath.ToString();
            System.IO.File.WriteAllText(@dir + "\\" + "Visa details.xls", renderedGridView);

            System.Text.Encoding enc = System.Text.Encoding.ASCII;
            myByteArray = enc.GetBytes(sw.ToString());

            lblMessage.ForeColor = System.Drawing.Color.Green;
            lblMessage.Text = "Exported to " + dir;

            Response.AppendHeader("content-disposition", "attachment; filename=" + "Visa details.xls");
            Response.ContentType = "Application/vnd.ms-excel";
            Response.Write(renderedGridView);
            Response.End();
        }


        protected void lnkTravelIns_Click(object sender, EventArgs e)
        {
            ExportToTravelInsExcel();
        }

        protected void ExportToTravelInsExcel()
        {
            // DirectoryInfo dirPath = new DirectoryInfo(@"I:\iEmpPower_Tempfiles\");
            DirectoryInfo dirPath = new DirectoryInfo(@"D:\iEmpPower_ExcelFiles\");
            if (!Directory.Exists(dirPath.ToString()))
            {
                Directory.CreateDirectory(dirPath.ToString());
            }
            DirectoryInfo dirChildPath = new DirectoryInfo(dirPath + DateTime.Now.Date.ToString("dd-MMM-yyyy"));
            if (!Directory.Exists(dirChildPath.ToString()))
            {
                Directory.CreateDirectory(dirChildPath.ToString());
            }


            System.IO.StringWriter sw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);




            ////removepaging();



            // Render grid view control.
            grdTravelIns.RenderControl(htw);

            ////Get_ExpenseDetails();

            // Write the rendered content to a file.
            string renderedGridView = "Travel Insurance details" + "<br>" + sw.ToString();

            dir = dirChildPath.ToString();
            System.IO.File.WriteAllText(@dir + "\\" + "Travel Insurance details.xls", renderedGridView);

            System.Text.Encoding enc = System.Text.Encoding.ASCII;
            myByteArray = enc.GetBytes(sw.ToString());

            lblMessage.ForeColor = System.Drawing.Color.Green;
            lblMessage.Text = "Exported to " + dir;

            Response.AppendHeader("content-disposition", "attachment; filename=" + "Travel Insurance details.xls");
            Response.ContentType = "Application/vnd.ms-excel";
            Response.Write(renderedGridView);
            Response.End();
        }

        protected void lnkFLYER_Click(object sender, EventArgs e)
        {
            ExportTolnkFLYERExcel();
        }

        protected void ExportTolnkFLYERExcel()
        {
            // DirectoryInfo dirPath = new DirectoryInfo(@"I:\iEmpPower_Tempfiles\");
            DirectoryInfo dirPath = new DirectoryInfo(@"D:\iEmpPower_ExcelFiles\");
            if (!Directory.Exists(dirPath.ToString()))
            {
                Directory.CreateDirectory(dirPath.ToString());
            }
            DirectoryInfo dirChildPath = new DirectoryInfo(dirPath + DateTime.Now.Date.ToString("dd-MMM-yyyy"));
            if (!Directory.Exists(dirChildPath.ToString()))
            {
                Directory.CreateDirectory(dirChildPath.ToString());
            }


            System.IO.StringWriter sw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);




            ////removepaging();



            // Render grid view control.
            grdFLYER_NUMBER.RenderControl(htw);

            ////Get_ExpenseDetails();

            // Write the rendered content to a file.
            string renderedGridView = "FLYER number details" + "<br>" + sw.ToString();

            dir = dirChildPath.ToString();
            System.IO.File.WriteAllText(@dir + "\\" + "FLYER details.xls", renderedGridView);

            System.Text.Encoding enc = System.Text.Encoding.ASCII;
            myByteArray = enc.GetBytes(sw.ToString());

            lblMessage.ForeColor = System.Drawing.Color.Green;
            lblMessage.Text = "Exported to " + dir;

            Response.AppendHeader("content-disposition", "attachment; filename=" + "FLYER details.xls");
            Response.ContentType = "Application/vnd.ms-excel";
            Response.Write(renderedGridView);
            Response.End();
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
        }

        protected void grdPassport_Sorting(object sender, GridViewSortEventArgs e)
        {

        }
    }
}