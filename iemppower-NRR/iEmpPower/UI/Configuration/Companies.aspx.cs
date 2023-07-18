using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iEmpPower.Old_App_Code.iEmpPowerBO.EmpData.EmpCollBo;

namespace iEmpPower.UI.Configuration
{
    public partial class Companies : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            loadGrid();
        }

        protected void loadGrid()
        {
            configurationbo ObjBo = new configurationbo();
            configurationbl ObjBl = new configurationbl();
            configurationcollectionbo objoList = new configurationcollectionbo();
            objoList = ObjBl.LoadAllCompnydeatils("", 1);
            if (objoList.Count > 0)
            {
                GV_Comp.DataSource = objoList;
                GV_Comp.DataBind();
                GV_Comp.Visible = true;
                //lblMessageBoard.Text = "";
                //lblMessageBoard.ForeColor = System.Drawing.Color.Transparent;
            }

            else
            {
                GV_Comp.DataSource = null;
                GV_Comp.DataBind();
            }

        }

        protected void GV_Comp_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow grdRow = GV_Comp.Rows[rowIndex];
                switch (e.CommandName.ToUpper())
                {
                    case "GENERATE":
                        ViewState["EMPID"] = GV_Comp.DataKeys[grdRow.RowIndex].Values["Company_Code"].ToString();
                        loadempdetls(ViewState["EMPID"].ToString());
                        pnlempdtls.Visible = true;
                        break;
                }
            }
            catch (Exception ex) { }
        }

        protected void loadempdetls(string cc)
        {
            try
            {
                EmoDataBo ObjBo = new EmoDataBo();
                EmpDataBL ObjBl = new EmpDataBL();
                EmpCollBo objoList = new EmpCollBo();
                objoList = ObjBl.Get_empInfosaral(cc, 1);
                if (objoList.Count > 0)
                {
                    gvEMpDtls.DataSource = objoList;
                    gvEMpDtls.DataBind();
                    gvEMpDtls.Visible = true;
                    //lblMessageBoard.Text = "";
                    //lblMessageBoard.ForeColor = System.Drawing.Color.Transparent;
                }

                else
                {
                    gvEMpDtls.DataSource = null;
                    gvEMpDtls.DataBind();
                }
            }
            catch (Exception ex) { }
        }

        protected void BtnExporttoXl_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Charset = "";
            string FileName = "Companies_Info" + DateTime.Now + ".xls";
            StringWriter strwritter = new StringWriter();
            HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
            gvEMpDtls.GridLines = GridLines.Both;
            gvEMpDtls.HeaderStyle.Font.Bold = true;
            gvEMpDtls.RenderControl(htmltextwrtter);
            Response.Write(strwritter.ToString());
            Response.End();



            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);
                gvEMpDtls.RenderControl(hw);

                //style to format numbers to string
                string style = @"<style> .textmode { } </style>";
                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }

        }

        public override void VerifyRenderingInServerForm(Control control) { }
    }
}