using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace iEmpPower.UI.PR
{
    public partial class PR : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            this.Page.Form.DefaultButton = btnEntryKey.UniqueID;
        }
    }
}