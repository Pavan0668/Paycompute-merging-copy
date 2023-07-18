using System;
using System.IO;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Globalization;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Drawing.Printing;
using System.Text;
using System.Drawing;
using iEmpPower.Dataset;

public partial class UI_Manager_Self_Service_employmentproof : System.Web.UI.Page
{
    private string exportPath;
    private DiskFileDestinationOptions diskFileDestinationOptions;
    private ExportOptions exportOptions;
    string strExportFileName = string.Empty;
    string sLetter = string.Empty;
    CrystalDecisions.CrystalReports.Engine.ReportDocument objEmploymentProofeReport = new
         CrystalDecisions.CrystalReports.Engine.ReportDocument();

    private addressproofdataset objAddresProofDataSet = new addressproofdataset();
    private iEmpPower.Dataset.addressproofdatasetTableAdapters.sp_ms_load_address_proofTableAdapter objAddresProofTA =
         new iEmpPower.Dataset.addressproofdatasetTableAdapters.sp_ms_load_address_proofTableAdapter();

    private addressproofdataset.sp_ms_load_address_proofDataTable objAddresProofTable =
        new addressproofdataset.sp_ms_load_address_proofDataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //LoadEmploymentProofReport();
            LoadEmployeeDetail(User.Identity.Name);
            btnSave.Focus();
        }
       // this.Page.Form.DefaultButton = btnEntryKey.UniqueID;
    }
    protected void LoadEmployeeDetail(string sEmployeeID)
    {
        msemployeedetailsbo objMSBo = new msemployeedetailsbo();
        objMSBo.PERNR = sEmployeeID;
        msemployeedetailsbl objMSBl = new msemployeedetailsbl();
        msemployeedetailscollectionbo objMSLst = objMSBl.Get_EmployeeDetails(objMSBo);
        foreach (msemployeedetailsbo obj in objMSLst)
        {
            sLetter = obj.BEGDA;
        }

        objAddresProofTA.ClearBeforeFill = true;
        string sEmployeeId = User.Identity.Name;
        objEmploymentProofeReport.Load(Server.MapPath("employmentproof.rpt"));

        objEmploymentProofeReport.SetDataSource(objMSLst);



        string strEmpName = (string)Session["EmployeeName"] + " , Emp-ID " + sEmployeeId;
        string strEmpId = " , Emp-ID " + sEmployeeId;

        objEmploymentProofeReport.SetParameterValue("employee_name", strEmpName);
        objEmploymentProofeReport.SetParameterValue("employee_id", strEmpId);
        objEmploymentProofeReport.SetParameterValue("sLetter", sLetter);


        CrystalReportViewer1.EnableViewState = true;
        CrystalReportViewer1.EnableDatabaseLogonPrompt = false;
        CrystalReportViewer1.ReportSource = objEmploymentProofeReport;
        CrystalReportViewer1.DataBind();

    }
    protected void LoadEmploymentProofReport()
    {
        try
        {
            objAddresProofTA.ClearBeforeFill = true;
            string sEmployeeId = User.Identity.Name;

            //LoadEmployeeDetail(sEmployeeId);
            objAddresProofTA.Fill(objAddresProofTable, sEmployeeId, "1");

            if (objAddresProofTable.Rows.Count <= 0)
            {
                CrystalReportViewer1.ReportSource = null;
                lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                lblMessageBoard.Text = GetLocalResourceObject("NoMatchingRecordFound").ToString();
                lblMessageBoard.Visible = true;
                return;
            }

            objEmploymentProofeReport.Load(Server.MapPath("employmentproof.rpt"));

            objEmploymentProofeReport.SetDataSource((DataSet)objAddresProofDataSet);
            objEmploymentProofeReport.SetDataSource((DataTable)objAddresProofTable);


            string strEmpName = (string)Session["EmployeeName"] + " , Emp-ID " + sEmployeeId;
            string strEmpId = " , Emp-ID " + sEmployeeId;

            objEmploymentProofeReport.SetParameterValue("employee_name", strEmpName);
            objEmploymentProofeReport.SetParameterValue("employee_id", strEmpId);
            objEmploymentProofeReport.SetParameterValue("sLetter", sLetter);


            CrystalReportViewer1.EnableViewState = true;
            CrystalReportViewer1.EnableDatabaseLogonPrompt = false;
            CrystalReportViewer1.ReportSource = objEmploymentProofeReport;
            CrystalReportViewer1.DataBind();
        }
        catch (Exception ex)
        {
            lblMessageBoard.Text = GetLocalResourceObject("UnkownError").ToString();
            lblMessageBoard.ForeColor = System.Drawing.Color.Red;
            return;
        }
    }
    protected string GenerateFileName(string strExtension)
    {
        strExportFileName = DateTime.Now.ToString("ddMMyyyy-hhmmss") + strExtension;
        Session.Add("strExportFileName", strExportFileName);
        return (strExportFileName);
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            exportPath = "D:/Exported/";

            if (!System.IO.Directory.Exists(exportPath))
            {
                System.IO.Directory.CreateDirectory(exportPath);
            }
            LoadEmployeeDetail(User.Identity.Name);
           diskFileDestinationOptions = new DiskFileDestinationOptions();
           exportOptions = objEmploymentProofeReport.ExportOptions;
            exportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
            exportOptions.FormatOptions = null;
            exportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
            diskFileDestinationOptions.DiskFileName = exportPath + GenerateFileName(".pdf");
            exportOptions.DestinationOptions = diskFileDestinationOptions;
            objEmploymentProofeReport.Export();
            Response.ContentType = "text/plain";


            Response.AppendHeader("Content-Disposition", "attachment; filename=" + Session["strExportFileName"].ToString());
            Response.TransmitFile(exportPath + Session["strExportFileName"].ToString());
            Response.End();
            exportPath = "";
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }
    }
    protected void btnExit_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Default.aspx");
    }
}