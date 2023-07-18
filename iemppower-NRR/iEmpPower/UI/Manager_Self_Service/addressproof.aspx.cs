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

public partial class UI_Manager_Self_Service_addressproof : System.Web.UI.Page
{
    private string exportPath;
    private DiskFileDestinationOptions diskFileDestinationOptions;
    private ExportOptions exportOptions;
    string strExportFileName = string.Empty;
    string sLetter = string.Empty;
    CrystalDecisions.CrystalReports.Engine.ReportDocument objAddressProofeReport = new
         CrystalDecisions.CrystalReports.Engine.ReportDocument();
    private addressproofdataset objAddresProofDataSet = new addressproofdataset();
    private iEmpPower.Dataset.addressproofdatasetTableAdapters.sp_ms_load_address_proofTableAdapter objAddresProofTA =
         new iEmpPower.Dataset.addressproofdatasetTableAdapters.sp_ms_load_address_proofTableAdapter();

    private addressproofdataset.sp_ms_load_address_proofDataTable objAddresProofTable =
        new addressproofdataset.sp_ms_load_address_proofDataTable();

    private iEmpPower.Dataset.addressproofdatasetTableAdapters.sp_ms_load_company_detailsTableAdapter objCompanyDetailsTA =
        new iEmpPower.Dataset.addressproofdatasetTableAdapters.sp_ms_load_company_detailsTableAdapter();

    private addressproofdataset.sp_ms_load_company_detailsDataTable objCompanyDetailsTable =
        new addressproofdataset.sp_ms_load_company_detailsDataTable();
   
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMessageBoard.Text = "";
        if (!IsPostBack)
        { 
            LoadAddressType();
            btnSave.Enabled = false;
            drpdwnAddressType.Focus();
        }
      
    }
    protected void LoadEmployeeDetail(string sEmployeeID)
    {
        msemployeedetailsbo objMSBo = new msemployeedetailsbo();
        objMSBo.PERNR = sEmployeeID;
        msemployeedetailsbl objMSBl = new msemployeedetailsbl();
        msemployeedetailscollectionbo objMSLst = objMSBl.Get_EmployeeDetails(objMSBo);
        foreach (msemployeedetailsbo obj in objMSLst)
        {
            sLetter = obj.EMPLOYEE_DETAILS;
        }

    }

    protected void LoadAddressType()
    {
        mastercollectionbo objLst = iEmpPowerMaster_Load.masterbl.Load_Address_Type();
        drpdwnAddressType.DataSource = objLst;
        drpdwnAddressType.DataTextField = "STEXT";
        drpdwnAddressType.DataValueField = "SUBTY";
        drpdwnAddressType.DataBind();
    }
    protected void btnAddrssProof_Click(object sender, EventArgs e)
    {
        LoadAddressProofReport();
    }
    protected void LoadAddressProofReport()
    {
        try
        {
            objAddresProofTA.ClearBeforeFill = true;
            string strEmployeeId = User.Identity.Name; 
            //LoadEmployeeDetail(strEmployeeId);
            string strAddressType = drpdwnAddressType.SelectedValue.ToString();
            objAddresProofTA.Fill(objAddresProofTable, strEmployeeId, strAddressType);
            if (objAddresProofTable.Rows.Count <= 0)
            {
                CrystalReportViewer1.ReportSource = null;
                lblMessageBoard.ForeColor = System.Drawing.Color.Red;
                lblMessageBoard.Text = GetLocalResourceObject("NoMatchingRecordFound").ToString();
                lblMessageBoard.Visible = true;
                btnSave.Enabled = false;
                return;
            }
            objCompanyDetailsTA.ClearBeforeFill = true;
            objCompanyDetailsTA.Fill(objCompanyDetailsTable);

            DataRow dRow = objCompanyDetailsTable.Rows[0];

            string sCompanyName = dRow[0].ToString();
            string sContactPerson = dRow[1].ToString();
            string sDesignization = dRow[2].ToString();

            objAddressProofeReport.Load(Server.MapPath("addressproof.rpt"));

            objAddressProofeReport.SetDataSource((DataSet)objAddresProofDataSet);
            objAddressProofeReport.SetDataSource((DataTable)objAddresProofTable);
            string sAddressType = drpdwnAddressType.SelectedItem.Text;
            string[] arrysAddressType = sAddressType.Split(' ');

           // string strEmpName = (string)Session["EmployeeName"];
            string strEmpName = (string)Session["EmployeeName"] + " , Emp-ID " + strEmployeeId;
            string strEmpId = " , Emp-ID " + strEmployeeId;

            //string strCont = "This is to certify that Mr‎./‎Mrs‎." + strEmpName + " " + strEmpId + " is working in ‎our organization from " + +" till date‎";
                                 
            string sLetter4 = "This letter is issued only as address proof document and cannot be used for any other purpose.";

            objAddressProofeReport.SetParameterValue("sLetter", sLetter);
            objAddressProofeReport.SetParameterValue("sLetter4", sLetter4);
            objAddressProofeReport.SetParameterValue("sEmployee", strEmpName);
            objAddressProofeReport.SetParameterValue("sEmpID", "");
            objAddressProofeReport.SetParameterValue("companyname", sCompanyName);
            objAddressProofeReport.SetParameterValue("contactPerson", sContactPerson);
            objAddressProofeReport.SetParameterValue("designization", sDesignization);
            CrystalReportViewer1.EnableViewState = true;
            CrystalReportViewer1.EnableDatabaseLogonPrompt = false;
            CrystalReportViewer1.ReportSource = objAddressProofeReport;
            CrystalReportViewer1.DataBind();
            btnSave.Enabled = true;
        }
        catch (Exception ex)
        {
            lblMessageBoard.Text = GetLocalResourceObject("UnkownError").ToString();
            lblMessageBoard.ForeColor = System.Drawing.Color.Red;
            return;
        }
    }
    protected void LoadCompanyDetails()
    {
        objCompanyDetailsTA.ClearBeforeFill = true;
        objCompanyDetailsTA.Fill(objCompanyDetailsTable);
        string sCompanyName = string.Empty;
    }
    protected string GenerateFileName(string strExtension)
    {
        strExportFileName = DateTime.Now.ToString("ddMMyyyy-hhmmss") + strExtension;
        Session.Add("strExportFileName", strExportFileName);
        return (strExportFileName);
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        exportPath = "D:/Exported/";

        if (!System.IO.Directory.Exists(exportPath))
        {
            System.IO.Directory.CreateDirectory(exportPath);
        }
        LoadAddressProofReport();
        diskFileDestinationOptions = new DiskFileDestinationOptions();
        exportOptions = objAddressProofeReport.ExportOptions;
        exportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
        exportOptions.FormatOptions = null;
        exportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
        diskFileDestinationOptions.DiskFileName = exportPath + GenerateFileName(".pdf");
        exportOptions.DestinationOptions = diskFileDestinationOptions;
        objAddressProofeReport.Export();
        Response.ContentType = "text/plain";

        Response.AppendHeader("Content-Disposition", "attachment; filename=" + Session["strExportFileName"].ToString());
        Response.TransmitFile(exportPath + Session["strExportFileName"].ToString());
        Response.End();
        exportPath = "";
    }
    protected void btnExit_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Default.aspx");
    }
}