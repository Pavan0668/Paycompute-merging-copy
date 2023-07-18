using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace iEmpPower.UI.IT
{
    public partial class ITDeclarations : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Roles.IsUserInRole("fbpadmin"))
            {
                EmpDiv.Visible = false;
                FinanceDiv.Visible = true;
                ITSec80.Visible = false;
                ITSec80C.Visible = false;
                ITHousing.Visible = false;
                ITOthers.Visible = false;
                ITEmpView.Visible = false;
                ITAdminLock.Visible = true;
                ITAdminView.Visible = true;
                ITAppRej.Visible = true;
               
            }
            else
            {
                EmpDiv.Visible = true;
                FinanceDiv.Visible = false;
                ITSec80.Visible = true;
                ITSec80C.Visible = true;
                ITHousing.Visible = true;
                ITOthers.Visible = true;
                ITEmpView.Visible = true;
                ITAdminLock.Visible = false;
                ITAdminView.Visible = false;
                ITAppRej.Visible = false;
               
            }
        }
    }
}