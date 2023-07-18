using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iEmpPower.Old_App_Code.iEmpPowerBO.Benefits_Payment
{
    public class BasicInfo_Claimsbo
    {

        public BasicInfo_Claimsbo()
	    {
		    //
		    // TODO: Add constructor logic here
		    //
	    }

        private string _strSuperviosrRoleID = string.Empty; //S_PLANS
        private string _strSuperviosrID = string.Empty; //S_PERNR
        private string _strEmployeeId = string.Empty;  //PERNR
        private string _strEmployeeName = string.Empty; //ENAME
        private string _strEmployeeRoleID = string.Empty; //PLANS
        private string _strEmployeeState = string.Empty; //WERKS
        private string _strEmployeeHQ = string.Empty; //HQ
        private string _strEmployeeDesignation = string.Empty; //PLSXT
        private string _strEmployeeEmaild = string.Empty; //USERID
        private string _strSuperviosrName = string.Empty;//S_ENAME
        private string _strSuperviosrEmailId = string.Empty;//S_USERID
        private string _strSuperviosrDesgination = string.Empty;//S_PLSXT            

        public string PERNR
        {
            get { return _strEmployeeId; }
            set { _strEmployeeId = value; }
        }

        public string ENAME
        {
            get { return _strEmployeeName; }
            set { _strEmployeeName = value; }
        }

        public string WERKS
        {
            get { return _strEmployeeState; }
            set { _strEmployeeState = value; }
        }

        public string HQ
        {
            get { return _strEmployeeHQ; }
            set { _strEmployeeHQ = value; }
        }

        public string PLSXT
        {
            get { return _strEmployeeDesignation; }
            set { _strEmployeeDesignation = value; }
        }

        public string USRID
        {
            get { return _strEmployeeEmaild; }
            set { _strEmployeeEmaild = value; }
        }

        public string PLANS
        {
            get { return _strEmployeeRoleID; }
            set { _strEmployeeRoleID = value; }
        }

        public string S_PERNR
        {
            get { return _strSuperviosrID; }
            set { _strSuperviosrID = value; }
        }
     
        public string S_NAME
        {
            get { return _strSuperviosrName; }
            set { _strSuperviosrName = value; }
        }

        public string S_PLSXT
        {
            get { return _strSuperviosrDesgination; }
            set { _strSuperviosrDesgination = value; }
        }

        public string S_USRID
        {
            get { return _strSuperviosrEmailId; }
            set { _strSuperviosrEmailId = value; }
        }

        public string S_PLANS
        {
            get { return _strSuperviosrRoleID; }
            set { _strSuperviosrRoleID = value; }
        }

    }
}