using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Services.Protocols;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
namespace iEmpPower.Old_App_Code.iEmpPowerBL.Common
{
    /// <summary>
    /// Summary description for EmployeeNames
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class EmployeeNames : System.Web.Services.WebService
    {
        travelrequestbl travelrequestblObj = new travelrequestbl();
        [WebMethod]
        //for outstation, search section
        public string[] GetEmployeeNames(string prefixText)
        {
            DataTable EmplyeeDataTable = new DataTable();
            string[] EmplyeeNameItems = null;
            List<requisitionbo> requisitionboList = travelrequestblObj.Get_Employee_Names(prefixText);
            EmplyeeDataTable.Columns.Add("EmpName");
            EmplyeeDataTable.Columns.Add("EmpNo");
            foreach (var item in requisitionboList)
            {
                EmplyeeDataTable.Rows.Add(item.EMPLOYEE_NAME, item.EMPLOYEE_NO);
            }
            if (EmplyeeDataTable != null)
            {
                EmplyeeNameItems = new string[EmplyeeDataTable.Rows.Count];
            }
            if (EmplyeeDataTable != null)
            {
                int i = 0;
                foreach (DataRow dr in EmplyeeDataTable.Rows)
                {
                    EmplyeeNameItems.SetValue(dr["EmpName"].ToString(), i);
                    i++;
                }
            }
            return EmplyeeNameItems;
        }
        //for local requisition, member selection
        [WebMethod]
        public string[] GetEmployeeNamesAndId(string prefixText)
        {
            DataTable EmplyeeDataTable = new DataTable();
            string[] EmplyeeNameItems = null;
            List<requisitionbo> requisitionboList = travelrequestblObj.Get_Employee_Names(prefixText);
            EmplyeeDataTable.Columns.Add("EmpName");
            EmplyeeDataTable.Columns.Add("EmpNo");
            foreach (var item in requisitionboList)
            {
                EmplyeeDataTable.Rows.Add(item.EMPLOYEE_NAME, item.EMPLOYEE_NO);
            }
            if (EmplyeeDataTable != null)
            {
                EmplyeeNameItems = new string[EmplyeeDataTable.Rows.Count];
            }
            if (EmplyeeDataTable != null)
            {
                int i = 0;
                foreach (DataRow dr in EmplyeeDataTable.Rows)
                {
                    EmplyeeNameItems.SetValue(dr["EmpName"].ToString() + "-" + dr["EmpNo"].ToString(), i);
                    i++;
                }
            }
            return EmplyeeNameItems;
        }

        //Airlines
        [WebMethod]
        //for International -Airlines
        public string[] GetAirline(string prefixText)
        {
            DataTable EmplyeeDataTable = new DataTable();
            string[] EmplyeeNameItems = null;
             travelrequestbl objtravelrequestbl = new travelrequestbl();
            List<requisitionbo> requisitionboList = objtravelrequestbl.GetVehicleName("A");//travelrequestblObj.Get_Employee_Names(prefixText);
            EmplyeeDataTable.Columns.Add("VEHICLE_NAME_ZZVEHNAM");
            EmplyeeDataTable.Columns.Add("VEHICLE_NAME_VHNUM");
            foreach (var item in requisitionboList)
            {
                EmplyeeDataTable.Rows.Add(item.VEHICLE_NAME_ZZVEHNAM, item.VEHICLE_NAME_VHNUM);
            }
            if (EmplyeeDataTable != null)
            {
                EmplyeeNameItems = new string[EmplyeeDataTable.Rows.Count];
            }
            if (EmplyeeDataTable != null)
            {
                int i = 0;
                foreach (DataRow dr in EmplyeeDataTable.Rows)
                {
                    EmplyeeNameItems.SetValue(dr["VEHICLE_NAME_ZZVEHNAM"].ToString() , i);
                    i++;
                }
            }
            return EmplyeeNameItems;
        }







    }
}
