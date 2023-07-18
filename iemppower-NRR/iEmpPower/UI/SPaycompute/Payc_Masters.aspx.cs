using iEmpPower.Old_App_Code.iEmpPowerBL.SPaycompute;
using iEmpPower.Old_App_Code.iEmpPowerBO.SPaycompute;
using iEmpPower.Old_App_Code.iEmpPowerBO.SPaycompute.SPayc_Collection_BO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.IO;
using System.Configuration;
using iEmpPowerMaster_Load;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Threading.Tasks;
using System.Web.Security;








namespace iEmpPower.UI.SPaycompute

{
    public partial class Payc_Masters : System.Web.UI.Page
    {
        private string sCreateUserLogPath = ConfigurationManager.AppSettings["CreateUserLog"].ToString() + DateTime.Now.Date.ToShortDateString().Replace('/', '-') + ".html";
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                HideTabs();
                view1.Visible = true;
                View6.Visible = true;
                Tab1.CssClass = "nav-link active p-2";
                //LoadProjects();
                LoadProjects_WBS();
                //LoadAttdnceTypes();
                DDL_Compo();
                LoadProjects_Acty();
                LoadProjextdata();
                loadsrchddl();
            }
              
           
        }

        public void DDL_Compo()
        {
            try
            {

                SPayc_Collection_BO objspaylst1 = new SPayc_Collection_BO();
                SPaycompute_BO objSpayc1 = new SPaycompute_BO();
                SPayc_BL objPaycbl1 = new SPayc_BL();
                objspaylst1 = objPaycbl1.Operate_Masters("", "", "", 3);
                DDL_slrymap.DataSource = null;
                DDL_slrymap.DataBind();

                DDL_slrymap.DataSource = objspaylst1;
                DDL_slrymap.DataTextField = "CCD";
                DDL_slrymap.DataValueField = "SRTS";
                DDL_slrymap.DataBind();

                SPayc_Collection_BO lst = new SPayc_Collection_BO();
                SPayc_BL bl = new SPayc_BL();
                lst = bl.get_compomapping(Session["CompCode"].ToString(), 2);
                GV_slrymapg.DataSource = lst;
                GV_slrymapg.DataBind();
                mapexitmode();

                string frow = "", lrow = "";  ////Row count

                foreach (GridViewRow row in GV_slrymapg.Rows)
                {
                    for (int i = 0; i < GV_slrymapg.Rows.Count; i++)
                    {
                        Label lblsalmapRowNumber = (Label)GV_slrymapg.Rows[i].FindControl("lblsalmapRowNumber");
                        if (i == 0)
                        {
                            frow = lblsalmapRowNumber.Text;
                        }
                        if (i == GV_slrymapg.Rows.Count - 1)
                        {
                            lrow = lblsalmapRowNumber.Text;
                        }
                    }
                }
                dvcntsalmap.InnerHtml = "Showing " + frow + " to " + lrow + " of " + lst.Count + " entries";
                dvcntsalmap.Visible = GV_slrymapg.Rows.Count > 0 ? true : false;

                
            }
            catch (Exception ex)
            {

            }
        }

        protected void btn_save_masters_compo_Click(object sender, EventArgs e)
        {
            try
            {
                bool? status = false;
                SPaycompute_BO objSpayc = new SPaycompute_BO();
                SPayc_BL objPaycbl = new SPayc_BL();
                objSpayc.col1 = txt_nwcompo_mstrs.Text.ToString().Trim();
                objSpayc.col10 = "";
                objSpayc.col11 = "";
                objSpayc.col12 = User.Identity.Name;
                objSpayc.col13 = Session["CompCode"].ToString();
                objSpayc.ID = 1;
                objSpayc.col14 = "";
                objSpayc.begda = DateTime.Parse("1900-01-01");
                objSpayc.endda = DateTime.Parse("1900-01-01");
                objSpayc.begda1 = DateTime.Parse("1900-01-01");
                objSpayc.endda1 = DateTime.Parse("1900-01-01");
                objSpayc.col15 = "";
                objPaycbl.Check_Scompo_toadd(objSpayc, ref status);

                if (status == true)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Component already exists');", true);
                    txt_nwcompo_mstrs.Text = "";
                }

                else
                {
                    //bool? status1 = false;
                    //SPaycompute_BO objSpayc1 = new SPaycompute_BO();
                    //SPayc_BL objPaycbl1 = new SPayc_BL();

                    //objPaycbl.Check_Scompo_toadd(txt_nwcompo_mstrs.Text, User.Identity.Name, "", "abcd1234", ref status1, 1);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Salary Component has been added succesfully');", true);
                    DDL_Compo();
                    txt_nwcompo_mstrs.Text = "";
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void btn_Ccancel_Click(object sender, EventArgs e)
        {
            try
            {
                txt_nwcompo_mstrs.Text = "";
            }
            catch (Exception ex)
            {
                
            }
        }


        //public void LoadProjects()
        //{
        //    try
        //    {
        //        mastercollectionbo objLst = iEmpPowerMaster_Load.masterbl.Load_Order(Session["CompCode"].ToString());
        //        DDL_existing_projcts.DataSource = objLst;
        //        DDL_existing_projcts.DataTextField = "ORDER_TEXT";
        //        DDL_existing_projcts.DataValueField = "pspnr";
        //        DDL_existing_projcts.DataBind();
        //        DDL_existing_projcts.Items.Insert(0, new ListItem("Select", "0"));
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}

        public void LoadProjects_WBS()
        {
            try
            {
                mastercollectionbo objLst = iEmpPowerMaster_Load.masterbl.Load_Order(Session["CompCode"].ToString(),DateTime.Now,DateTime.Now);
                DDL_pojct_addwbs.DataSource = objLst;
                DDL_pojct_addwbs.DataTextField = "ORDER_TEXT";
                DDL_pojct_addwbs.DataValueField = "pspnr";
                DDL_pojct_addwbs.DataBind();
                DDL_pojct_addwbs.Items.Insert(0, new ListItem("Select", "0"));
            }
            catch (Exception ex)
            {

            }
        }

        //public void LoadWbs(string project)
        //{
        //    try
        //    {
        //        mastercollectionbo objLst = iEmpPowerMaster_Load.masterbl.Load_Wbs(Session["CompCode"].ToString(),DDL_pojct_addwbs.SelectedValue);
        //        DDL_WBSadd.DataSource = objLst;
        //        DDL_WBSadd.DataTextField = "RPROJ";
        //        DDL_WBSadd.DataValueField = "pspnr";
        //        DDL_WBSadd.DataBind();
        //        DDL_WBSadd.Items.Insert(0, new ListItem("Select", "0"));
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}


        //public void LoadAttdnceTypes()
        //{
        //    try
        //    {
        //        mastercollectionbo objLst = iEmpPowerMaster_Load.masterbl.Load_Attendence_abs_Types(Session["CompCode"].ToString());
        //        DDL_attctype.DataSource = objLst;
        //        DDL_attctype.DataTextField = "ATEXT";
        //        DDL_attctype.DataValueField = "pspnr";
        //        DDL_attctype.DataBind();
        //        DDL_attctype.Items.Insert(0, new ListItem("Select", "0"));
        //        Session.Add("objDropDownLst", objLst);
                               
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}

       

        protected void btn_addprojs_Click(object sender, EventArgs e)
        {
            try
            {
                string pfrmdate = txtprjfrmdate.Text.ToString().Trim();

                DateTime prjbegda = Convert.ToDateTime(pfrmdate.ToString());

                string spendate = txt_prjtodate.Text.Trim();

                DateTime pendate = Convert.ToDateTime(spendate.ToString());

                DateTime wbsbegda = Convert.ToDateTime(DateTime.Now.ToString());

                
                    string str1 = txt_proj_mstrs.Text.ToString().Trim();
                    string upperstr1 = str1.ToUpper();

                    bool? status = false;
                    SPaycompute_BO objSpayc = new SPaycompute_BO();
                    SPayc_BL objPaycbl = new SPayc_BL();
                    objSpayc.col1 = txt_proj_mstrs.Text.ToString().Trim();
                    objSpayc.col10 = upperstr1.ToString().Trim();
                    objSpayc.col11 = "";
                    objSpayc.col12 = User.Identity.Name;
                    objSpayc.col13 = Session["CompCode"].ToString();
                    objSpayc.ID = 2;
                    objSpayc.col14 = txt_projextnID.Text.ToString().Trim();
                    objSpayc.begda = DateTime.Parse(txtprjfrmdate.Text.ToString().Trim());
                    objSpayc.endda = DateTime.Parse(txt_prjtodate.Text.ToString().Trim());
                    objSpayc.begda1 = Convert.ToDateTime(wbsbegda.ToString());
                    objSpayc.endda1 = Convert.ToDateTime(wbsbegda.ToString());
                    objSpayc.col15 = "";
                    objPaycbl.Check_Scompo_toadd(objSpayc, ref status);
                    if (status == true)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Project already exists for selected dates');", true);
                        txt_nwcompo_mstrs.Text = "";
                    }

                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "alert('Project has been added succesfully');", true);
                        LoadProjextdata();
                        LoadProjects_WBS();
                        LoadProjects_Acty();
                        loadsrchddl();
                        cnclproj();

                    }
                
            }
            catch (Exception ex)
            {

            }
        }

        public void cnclproj()
        {
            //DDL_existing_projcts.SelectedIndex = -1;
            txt_projextnID.Text = "";
           // txt_WBS_mstrs.Text = "";
            txt_proj_mstrs.Text = "";
            txtprjfrmdate.Text = "";
            txt_prjtodate.Text = "";
           // txt_WBS_mstrs.Text = "";
           // txt_pwbssrtdate.Text = "";
           // txt_pwbsenddate.Text = "";
           // txt_pactivity.Text = "";
        }

        protected void btn_cancelsub_Click(object sender, EventArgs e)
        {
            try
            {
                cnclproj();
            }
            catch (Exception ex)
            {

            }
        }

        protected void btn_saveWBS_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime prostartdt = DateTime.Parse(Session["probegda"].ToString().Trim());

                DateTime proenddat = DateTime.Parse(Session["proendda"].ToString().Trim());

                string stdate = txt_wbssrtdate.Text.Trim();

                DateTime wbsbegda = DateTime.Parse(stdate.ToString().Trim());

                string edate = txt_wbsenddate.Text.Trim();

                DateTime wbsendda = DateTime.Parse(edate.ToString().Trim());

                if (wbsbegda < prostartdt || wbsbegda > proenddat && wbsendda > proenddat || wbsendda < prostartdt)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('WBS duration should be between project duration');", true);
                }

                else
                {

                    bool? status = false;
                    string fdate = txt_wbssrtdate.Text.Trim();
                    string todate = txt_wbsenddate.Text.Trim();

                    SPaycompute_BO objSpayc = new SPaycompute_BO();
                    SPayc_BL objPaycbl = new SPayc_BL();
                    objSpayc.col1 = txt_newWBSadd.Text.ToString().Trim();
                    objSpayc.col10 = DDL_pojct_addwbs.SelectedValue.ToString().Trim();
                    objSpayc.col11 = txt_wbsid.Text.Trim();
                    objSpayc.col12 = User.Identity.Name;
                    objSpayc.col13 = Session["CompCode"].ToString();
                    objSpayc.ID = 3;
                    objSpayc.col14 = "";
                    objSpayc.begda = DateTime.Parse("1900-01-01");
                    objSpayc.endda = DateTime.Parse("1900-01-01");
                    objSpayc.endda1 = Convert.ToDateTime(todate.ToString());
                    objSpayc.begda1 = DateTime.Parse(fdate.ToString());
                    objSpayc.col15 = "";
                    objPaycbl.Check_Scompo_toadd(objSpayc, ref status);

                    if (status == true)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('WBS already exists for selected dates');", true);
                        txt_nwcompo_mstrs.Text = "";
                    }

                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "alert('WBS has been added succesfully');", true);
                        LoadProjextdata();
                        LoadProjects_WBS();
                        LoadProjects_Acty();
                        loadsrchddl();
                        cnclwbs();

                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        public void cnclwbs()
        {
            DDL_pojct_addwbs.SelectedIndex = -1;
            txt_newWBSadd.Text = "";
            txt_wbssrtdate.Text = "";
            txt_wbsenddate.Text = "";
            txt_wbsid.Text = "";
            //txt_wbsactivity.Text = "";
            //LoadWbs(DDL_pojct_addwbs.SelectedValue);
        }

        protected void btn_WBScncl_Click(object sender, EventArgs e)
        {
            try
            {
                cnclwbs();
            }
            catch (Exception ex)
            {

            }
        }

        protected void btn_add_mstrattdcetyp_Click(object sender, EventArgs e)
        {
            try
            {
                bool? status = false;
                SPaycompute_BO objSpayc = new SPaycompute_BO();
                SPayc_BL objPaycbl = new SPayc_BL();
                objSpayc.col1 = "";
                objSpayc.col10 = txt_addattcytype.Text.ToString().Trim();
                objSpayc.col11 = "";
                objSpayc.col12 = User.Identity.Name;
                objSpayc.col13 = Session["CompCode"].ToString();
                objSpayc.ID = 4;
                objSpayc.col14 = "";
                objSpayc.begda = DateTime.Parse("1900-01-01");
                objSpayc.endda = DateTime.Parse("1900-01-01");
                objSpayc.begda1 = DateTime.Parse("1900-01-01");
                objSpayc.endda1 = DateTime.Parse("1900-01-01");
                objSpayc.col15 = "";
                objPaycbl.Check_Scompo_toadd(objSpayc, ref status);

                if (status == true)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Attendance Type already exists');", true);
                    txt_nwcompo_mstrs.Text = "";
                }

                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Attendance Type has been added succesfully');", true);
                    LoadProjextdata();
                    loadsrchddl();
                    txt_addattcytype.Text = "";
                    

                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void btn_cncl_mstrattdcetyp_Click(object sender, EventArgs e)
        {
            try
            {
                txt_addattcytype.Text = "";
                //LoadAttdnceTypes();
            }
            catch (Exception ex)
            {

            }
        }

        protected void HideTabs()
        {
            view1.Visible = false;
            view2.Visible = false;
            View6.Visible = false;

            Tab1.CssClass = "nav-link  p-2";
            Tab2.CssClass = "nav-link  p-2";
            Tab3.CssClass = "nav-link  p-2";

        }

        protected void Tab1_Click(object sender, EventArgs e)
        {
            HideTabs();
            view1.Visible = true;
            view2.Visible = false;
            info.Visible = false;
            Tab1.CssClass = "nav-link active p-2";
        }

        protected void Tab2_Click(object sender, EventArgs e)
        {
            HideTabs();
            view1.Visible = false;
            info.Visible = true;
            Tab2.CssClass = "nav-link active p-2";

            HideSubTabs();           
            view2.Visible = true;
            Infotab1.CssClass = "nav-link active p-2";
        }

        protected void btn_addactivity_Click(object sender, EventArgs e)
        {
            try
            {
                bool? status = false;
                SPaycompute_BO objSpayc = new SPaycompute_BO();
                SPayc_BL objPaycbl = new SPayc_BL();
                objSpayc.col1 = "";
                objSpayc.col10 = DDL_actwbs.SelectedValue.ToString().Trim();
                objSpayc.col11 = "";
                objSpayc.col12 = User.Identity.Name;
                objSpayc.col13 = Session["CompCode"].ToString();
                objSpayc.ID = 5;
                objSpayc.col14 = "";
                objSpayc.begda = DateTime.Parse("1900-01-01");
                objSpayc.endda = DateTime.Parse("1900-01-01");
                objSpayc.begda1 = DateTime.Parse("1900-01-01");
                objSpayc.endda1 = DateTime.Parse("1900-01-01");
                objSpayc.col15 = txt_activity.Text.ToString().Trim();
                objPaycbl.Check_Scompo_toadd(objSpayc, ref status);

                if (status == true)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Activity already exists for WBS');", true);                   
                }

                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Activity has been added succesfully');", true);
                    cnclactivity();
                    LoadProjextdata();
                    LoadProjects_WBS();
                    LoadProjects_Acty();
                    loadsrchddl();
                    loadacty();
                }
            }
            catch (Exception ex) { }
        }

        public void LoadProjects_Acty()
        {
            try
            {
                mastercollectionbo objLst = iEmpPowerMaster_Load.masterbl.Load_Order(Session["CompCode"].ToString(), DateTime.Now, DateTime.Now);
                DDL_actproj.DataSource = objLst;
                DDL_actproj.DataTextField = "ORDER_TEXT";
                DDL_actproj.DataValueField = "pspnr";
                DDL_actproj.DataBind();
                DDL_actproj.Items.Insert(0, new ListItem("Select", "0"));
            }
            catch (Exception ex)
            {

            }
        }

        public void cnclactivity()
        {
            DDL_actproj.SelectedIndex = -1;
            txt_activity.Text = "";
            //txt_actstrtdate.Text = "";
            //txt_actenddate.Text = "";
            LoadWbs_Acty(DDL_actproj.SelectedValue);
        }

        protected void btn_cnclactivity_Click(object sender, EventArgs e)
        {
            try
            {
                cnclactivity();
            }
            catch (Exception ex) { }
        }

        public void LoadWbs_Acty(string project)
        {
            try
            {
                mastercollectionbo objLst = iEmpPowerMaster_Load.masterbl.Load_Wbs(Session["CompCode"].ToString(), DDL_actproj.SelectedValue);
                DDL_actwbs.DataSource = objLst;
                DDL_actwbs.DataTextField = "RPROJ";
                DDL_actwbs.DataValueField = "pspnr";
                DDL_actwbs.DataBind();
                DDL_actwbs.Items.Insert(0, new ListItem("Select", "0"));
            }
            catch (Exception ex)
            {

            }
        }

        protected void DDL_actproj_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadWbs_Acty(DDL_actproj.SelectedValue);
        }

        public void LoadProjextdata()
        {
            try
            {
                SPayc_Collection_BO objspaylst = new SPayc_Collection_BO();
                SPayc_BL objPaycbl = new SPayc_BL();
                objspaylst = objPaycbl.get_projects(Session["CompCode"].ToString(), 1,"","","");
                GV_projects.DataSource = objspaylst;
                GV_projects.DataBind();
                exitedit();


                string frow = "", lrow = "";  ////Row count

                foreach (GridViewRow row in GV_projects.Rows)
                {
                    for (int i = 0; i < GV_projects.Rows.Count; i++)
                    {
                        Label lblprojRowNumber = (Label)GV_projects.Rows[i].FindControl("lblprojRowNumber");
                        if (i == 0)
                        {
                            frow = lblprojRowNumber.Text;
                        }
                        if (i == GV_projects.Rows.Count - 1)
                        {
                            lrow = lblprojRowNumber.Text;
                        }
                    }
                }
                dvdntproj.InnerHtml = "Showing " + frow + " to " + lrow + " of " + objspaylst.Count + " entries";
                dvdntproj.Visible = GV_projects.Rows.Count > 0 ? true : false;
                

                SPayc_Collection_BO objspaylst1 = new SPayc_Collection_BO();
                SPayc_BL objPaycbl1 = new SPayc_BL();
                objspaylst1 = objPaycbl1.get_projects(Session["CompCode"].ToString(), 2,"","","");
                GV_wbs.DataSource = objspaylst1;
                GV_wbs.DataBind();
                exiteditwbs();

                string frowbs = "", lrowbs = "";  ////Row count

                foreach (GridViewRow row in GV_wbs.Rows)
                {
                    for (int i = 0; i < GV_wbs.Rows.Count; i++)
                    {
                        Label lblwbsRowNumber = (Label)GV_wbs.Rows[i].FindControl("lblwbsRowNumber");
                        if (i == 0)
                        {
                            frowbs = lblwbsRowNumber.Text;
                        }
                        if (i == GV_wbs.Rows.Count - 1)
                        {
                            lrowbs = lblwbsRowNumber.Text;
                        }
                    }
                }
                dvcntwbs.InnerHtml = "Showing " + frowbs + " to " + lrowbs + " of " + objspaylst1.Count + " entries";
                dvcntwbs.Visible = GV_wbs.Rows.Count > 0 ? true : false;

                SPayc_Collection_BO objspaylst2 = new SPayc_Collection_BO();
                SPayc_BL objPaycbl2 = new SPayc_BL();
                objspaylst2 = objPaycbl2.get_projects(Session["CompCode"].ToString(), 3,"","","");
                GV_acty.DataSource = objspaylst2;
                GV_acty.DataBind();
                exitacty();

                string frowact = "", lrowact = "";  ////Row count

                foreach (GridViewRow row in GV_acty.Rows)
                {
                    for (int i = 0; i < GV_acty.Rows.Count; i++)
                    {
                        Label lblactyRowNumber = (Label)GV_acty.Rows[i].FindControl("lblactyRowNumber");
                        if (i == 0)
                        {
                            frowact = lblactyRowNumber.Text;
                        }
                        if (i == GV_acty.Rows.Count - 1)
                        {
                            lrowact = lblactyRowNumber.Text;
                        }
                    }
                }
                dvcntacty.InnerHtml = "Showing " + frowact + " to " + lrowact + " of " + objspaylst2.Count + " entries";
                dvcntacty.Visible = GV_acty.Rows.Count > 0 ? true : false;



                SPayc_Collection_BO objspaylst3 = new SPayc_Collection_BO();
                SPayc_BL objPaycbl3 = new SPayc_BL();
                objspaylst3 = objPaycbl3.get_projects(Session["CompCode"].ToString(), 4,"","","");
                GV_atttype.DataSource = objspaylst3;
                GV_atttype.DataBind();
                exitattdmod();

                string frowattd = "", lrowattd = "";  ////Row count

                foreach (GridViewRow row in GV_atttype.Rows)
                {
                    for (int i = 0; i < GV_atttype.Rows.Count; i++)
                    {
                        Label lblattdRowNumber = (Label)GV_atttype.Rows[i].FindControl("lblattdRowNumber");
                        if (i == 0)
                        {
                            frowattd = lblattdRowNumber.Text;
                        }
                        if (i == GV_atttype.Rows.Count - 1)
                        {
                            lrowattd = lblattdRowNumber.Text;
                        }
                    }
                }
                dvcntattd.InnerHtml = "Showing " + frowattd + " to " + lrowattd + " of " + objspaylst3.Count + " entries";
                dvcntattd.Visible = GV_atttype.Rows.Count > 0 ? true : false;
                
                
            }

            catch (Exception ex)
            {

            }

        }

        protected void HideSubTabs()
        {
           
            view2.Visible = false;
            view3.Visible = false;
            view4.Visible = false;
            view5.Visible = false;
            
            Infotab1.CssClass = "nav-link  p-2";
            Infotab2.CssClass = "nav-link  p-2";
            Infotab3.CssClass = "nav-link  p-2";
            Infotab4.CssClass = "nav-link  p-2";
            
        }

        protected void Infotab1_Click(object sender, EventArgs e)
        {
            HideSubTabs();
            view2.Visible = true;
            view3.Visible = false;
            view4.Visible = false;
            view5.Visible = false;   
            Infotab1.CssClass = "nav-link active p-2";
        }

        protected void Infotab2_Click(object sender, EventArgs e)
        {
            HideSubTabs();
            view2.Visible = false;
            view3.Visible = true;
            view4.Visible = false;
            view5.Visible = false;   
            Infotab2.CssClass = "nav-link active p-2";
        }

        protected void Infotab3_Click(object sender, EventArgs e)
        {
            HideSubTabs();
            view2.Visible = false;
            view3.Visible = false;
            view4.Visible = true;
            view5.Visible = false;   
            Infotab3.CssClass = "nav-link active p-2";
        }

       
        public void exitedit()
        {
            try
            {
                foreach (GridViewRow row in GV_projects.Rows)
                {
                    Label LBPROJidextmod = (Label)row.FindControl("lbl_prjctidedtmod");

                    Label LBPROJnameextmod = (Label)row.FindControl("lbl_projedtmod");

                    Label LBPROJsdate1 = (Label)row.FindControl("lbl_pbegda");

                    Label LBPROJenda1 = (Label)row.FindControl("lbl_pendda");


                    TextBox TXTPROJIDEXTMOD = (TextBox)row.FindControl("txt_pjctidedtmod");

                    TextBox TXTPROJNMEXTMOD = (TextBox)row.FindControl("txt_prohedtmod");

                    TextBox TXTFDATE1 = (TextBox)row.FindControl("txt_pbegda");

                    TextBox TXTEDATE1 = (TextBox)row.FindControl("txt_pendda");

                    LinkButton EDIT1 = (LinkButton)row.FindControl("LK_proedit");

                    LinkButton UPDATE1 = (LinkButton)row.FindControl("LK_proupdate");

                    LinkButton CNCL1 = (LinkButton)row.FindControl("LK_procncl");

                    TXTPROJIDEXTMOD.Visible = false;
                    TXTPROJNMEXTMOD.Visible = false;
                    TXTFDATE1.Visible = false;
                    TXTEDATE1.Visible = false;


                    LBPROJidextmod.Visible = true;
                    LBPROJnameextmod.Visible = true;
                    LBPROJsdate1.Visible = true;
                    LBPROJenda1.Visible = true;
                    

                    EDIT1.Visible = true;
                    UPDATE1.Visible = false;
                    CNCL1.Visible = false;


                }
            }

            catch (Exception ex)
            {
                { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + ex.Message + "');", true); }
            }

        }

        public void exiteditwbs()
        {
            try
            {
                foreach (GridViewRow row in GV_wbs.Rows)
                {
                    Label LBwbsidextmod = (Label)row.FindControl("lbl_wbsidedtmod");

                    Label LBwbssnmextmod = (Label)row.FindControl("lbl_wbsedtmod");

                    Label LBwbssdate = (Label)row.FindControl("lbl_wbssda");

                    Label LBwbsenda = (Label)row.FindControl("lbl_wbsendda");

                    TextBox TXTwbsidextmod = (TextBox)row.FindControl("txt_wbsidedtmod");

                    TextBox TXTwbsnmextdt = (TextBox)row.FindControl("txt_wbsedtmod");

                    TextBox TXTwbsFDATE = (TextBox)row.FindControl("txt_wbsbegda");

                    TextBox TXTwbsEDATE = (TextBox)row.FindControl("txt_wbsendda");

                    LinkButton EDITwbs = (LinkButton)row.FindControl("LKwbsroedit");

                    LinkButton UPDATEwbs = (LinkButton)row.FindControl("LKwbsroupdate");

                    LinkButton CNCLwbs = (LinkButton)row.FindControl("LKwbsrocncl");

                    TXTwbsidextmod.Visible = false;
                    TXTwbsnmextdt.Visible = false;
                    TXTwbsFDATE.Visible = false;
                    TXTwbsEDATE.Visible = false;

                    LBwbsidextmod.Visible = true;
                    LBwbssnmextmod.Visible = true;
                    LBwbssdate.Visible = true;
                    LBwbsenda.Visible = true;

                    EDITwbs.Visible = true;
                    UPDATEwbs.Visible = false;
                    CNCLwbs.Visible = false;


                }
            }

            catch (Exception ex)
            {
                { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + ex.Message + "');", true); }
            }

        }

        protected void GV_projects_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {

                switch (e.CommandName.ToUpper())
                {
                    case "EDT":
                        exitedit();
                        int rowIndx = Convert.ToInt32(e.CommandArgument);
                        GridViewRow gvRw = GV_projects.Rows[rowIndx];
                        GridViewRow grow = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);

                        Label LBLidedt = (Label)grow.FindControl("lbl_prjctidedtmod");

                        Label LBLpnameedt = (Label)grow.FindControl("lbl_projedtmod");

                        Label LBL1 = (Label)grow.FindControl("lbl_pbegda");

                        Label LBL2 = (Label)grow.FindControl("lbl_pendda");

                        TextBox TXTidedt = (TextBox)grow.FindControl("txt_pjctidedtmod");

                        TextBox TXTnmedt = (TextBox)grow.FindControl("txt_prohedtmod");

                        TextBox TXT1 = (TextBox)grow.FindControl("txt_pbegda");

                        TextBox TXT2 = (TextBox)grow.FindControl("txt_pendda");

                        LinkButton LK1 = (LinkButton)grow.FindControl("LK_proedit");

                        LinkButton LK2 = (LinkButton)grow.FindControl("LK_proupdate");

                        LinkButton LK3 = (LinkButton)grow.FindControl("LK_procncl");

                        LBLidedt.Visible = false;
                        LBLpnameedt.Visible = false;
                        LBL1.Visible = false;
                        LBL2.Visible = false;

                        TXTidedt.Visible = true;
                        TXTnmedt.Visible = true;
                        TXT1.Visible = true;
                        TXT2.Visible = true;

                        LK1.Visible = false;
                        LK2.Visible = true;
                        LK3.Visible = true;
                        break;

                    case "UPDTE":
                    int updateindex = Convert.ToInt32(e.CommandArgument);

                    GridViewRow gvupdate = GV_projects.Rows[updateindex];

                    Label LBLidupd = (Label)gvupdate.FindControl("lbl_prjctidedtmod");

                    Label LBLpnameupd = (Label)gvupdate.FindControl("lbl_projedtmod");

                    Label LBPRSDATE = (Label)gvupdate.FindControl("lbl_pbegda");

                    Label LBPRENDDA = (Label)gvupdate.FindControl("lbl_pendda");

                    TextBox TXTidupd = (TextBox)gvupdate.FindControl("txt_pjctidedtmod");

                    TextBox TXTnmupd = (TextBox)gvupdate.FindControl("txt_prohedtmod");

                    TextBox TXTPRSDATE = (TextBox)gvupdate.FindControl("txt_pbegda");

                    TextBox TXTPRENDDA = (TextBox)gvupdate.FindControl("txt_pendda");

                    int ID = Convert.ToInt32(GV_projects.DataKeys[gvupdate.RowIndex].Values["id1"].ToString());

                    string IDtxt = GV_projects.DataKeys[gvupdate.RowIndex].Values["col11"].ToString();
                    if (TXTnmupd.Text != "")
                    {
                        if (TXTPRSDATE.Text != "" && TXTPRENDDA.Text != "")
                        {
                            bool? status = false;
                            SPaycompute_BO objSpayc = new SPaycompute_BO();
                            SPayc_BL objPaycbl = new SPayc_BL();
                            objSpayc.CCD = Session["CompCode"].ToString().ToLower().Trim();
                            objSpayc.begda = DateTime.Parse(TXTPRSDATE.Text.Trim());
                            objSpayc.endda = DateTime.Parse(TXTPRENDDA.Text.Trim());
                            objSpayc.ID = ID;
                            objSpayc.col1 = IDtxt.ToString().Trim();
                            objSpayc.col2 = "";
                            objSpayc.id2 = 1;
                            objSpayc.col13 = TXTidupd.Text.Trim();
                            objSpayc.col14 = TXTnmupd.Text.Trim();
                            objPaycbl.update_projectdtls(objSpayc, ref status);

                            if (status == true)
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "alert('Project details updated successfully');", true);
                            }

                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Project details has been Updated,dates already exists for this Project');", true);


                            }
                            LoadProjextdata();
                            exitedit();
                            LoadProjextdata();
                            exiteditwbs();
                            loadacty();
                            LoadProjects_WBS();
                            LoadProjects_Acty();
                            loadsrchddl();
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please select valid dates.!');", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "alert('Please enter project title.!');", true);
                    }
                        break;
                    case "CANCL":
                         int canceleindex = Convert.ToInt32(e.CommandArgument);
                         GridViewRow gvcancel = GV_projects.Rows[canceleindex];
                         SPayc_Collection_BO objspaylst = new SPayc_Collection_BO();
                         GV_projects.DataSource = objspaylst;
                         GV_projects.DataBind();
                         LoadProjextdata();
                        exitedit();
                        break;
                }

            }
            catch (Exception ex)
            { }
        }

        protected void GV_wbs_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {

                switch (e.CommandName.ToUpper())
                {
                    case "EDITWBS":
                        exiteditwbs();
                        int rowIndex = Convert.ToInt32(e.CommandArgument);
                        GridViewRow gvRow = GV_wbs.Rows[rowIndex];
                        GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);

                        Label LBwbsidedt = (Label)row.FindControl("lbl_wbsidedtmod");

                        Label LBwbsnmedt = (Label)row.FindControl("lbl_wbsedtmod");

                        Label LBP1 = (Label)row.FindControl("lbl_wbssda");

                        Label LBP2 = (Label)row.FindControl("lbl_wbsendda");

                        TextBox TXTwbsidedt = (TextBox)row.FindControl("txt_wbsidedtmod");

                        TextBox TXTnmedt = (TextBox)row.FindControl("txt_wbsedtmod");

                        TextBox TXTP1 = (TextBox)row.FindControl("txt_wbsbegda");

                        TextBox TXTP2 = (TextBox)row.FindControl("txt_wbsendda");

                        LinkButton PEDT = (LinkButton)row.FindControl("LKwbsroedit");

                        LinkButton PUPDT = (LinkButton)row.FindControl("LKwbsroupdate");

                        LinkButton PCNCL = (LinkButton)row.FindControl("LKwbsrocncl");

                        LBwbsidedt.Visible = false;
                        LBwbsnmedt.Visible = false;
                        LBP1.Visible = false;
                        LBP2.Visible = false;

                        TXTwbsidedt.Visible = true;
                        TXTnmedt.Visible = true;
                        TXTP1.Visible = true;
                        TXTP2.Visible = true;

                        PEDT.Visible = false;
                        PUPDT.Visible = true;
                        PCNCL.Visible = true;

                        break;

                    case "UPWBS":
                        int updateindex = Convert.ToInt32(e.CommandArgument);

                        GridViewRow gvupdate = GV_wbs.Rows[updateindex];

                        Label LBupdwbsid = (Label)gvupdate.FindControl("lbl_wbsidedtmod");

                        Label LBupdwbsnm = (Label)gvupdate.FindControl("lbl_wbsedtmod");

                        Label LBWBSDATE = (Label)gvupdate.FindControl("lbl_wbssda");

                        Label LBWBSNDDA = (Label)gvupdate.FindControl("lbl_wbsendda");

                        TextBox TXTupdwbsid = (TextBox)gvupdate.FindControl("txt_wbsidedtmod");

                        TextBox TXTupdnm = (TextBox)gvupdate.FindControl("txt_wbsedtmod");

                        TextBox TXTWBSSDATE = (TextBox)gvupdate.FindControl("txt_wbsbegda");

                        TextBox TXTWBSENDDA = (TextBox)gvupdate.FindControl("txt_wbsendda");

                        int WID = Convert.ToInt32(GV_wbs.DataKeys[gvupdate.RowIndex].Values["id1"].ToString());

                    string PIDtxt = GV_wbs.DataKeys[gvupdate.RowIndex].Values["col11"].ToString();

                    string WIDtxt = GV_wbs.DataKeys[gvupdate.RowIndex].Values["TXT"].ToString();
                    if (TXTupdnm.Text != "")
                    {
                        if (TXTWBSSDATE.Text != "" && TXTWBSENDDA.Text != "")
                        {

                            int? ID2 = WID;
                            int? ID3 = 0;
                            DateTime? sd1 = Convert.ToDateTime(DateTime.Now.ToString());
                            DateTime? ed1 = Convert.ToDateTime(DateTime.Now.ToString());
                            SPayc_Collection_BO objspaylst = new SPayc_Collection_BO();
                            SPayc_BL objPaycbl1 = new SPayc_BL();
                            objPaycbl1.chk_projects(Session["CompCode"].ToString(), ID2, 2, ref ID3, ref sd1, ref ed1);

                            Session["srtdate"] = Convert.ToDateTime(sd1.ToString().Trim());
                            Session["endate"] = Convert.ToDateTime(ed1.ToString().Trim());

                            DateTime sdate1 = DateTime.Parse(Session["srtdate"].ToString().Trim());

                            DateTime edate1 = DateTime.Parse(Session["endate"].ToString().Trim());

                            DateTime strdt1 = DateTime.Parse(TXTWBSSDATE.Text.Trim());

                            DateTime endt1 = DateTime.Parse(TXTWBSENDDA.Text.Trim());

                            if (((strdt1 < sdate1) || (strdt1 > edate1)) && ((endt1 > edate1) || (endt1 < sdate1)))
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('WBS duration should be between project duration');", true);
                            }

                            else
                            {

                                bool? status = false;
                                SPaycompute_BO objSpayc = new SPaycompute_BO();
                                SPayc_BL objPaycbl = new SPayc_BL();
                                objSpayc.CCD = Session["CompCode"].ToString().ToLower().Trim();
                                objSpayc.begda = DateTime.Parse(TXTWBSSDATE.Text.Trim());
                                objSpayc.endda = DateTime.Parse(TXTWBSENDDA.Text.Trim());
                                objSpayc.ID = WID;
                                objSpayc.col1 = PIDtxt.ToString().Trim();
                                objSpayc.col2 = "";
                                objSpayc.id2 = 2;
                                objSpayc.col13 = TXTupdwbsid.Text.Trim();
                                objSpayc.col14 = TXTupdnm.Text.Trim();
                                objPaycbl.update_projectdtls(objSpayc, ref status);

                                if (status == true)
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Successfull", "alert('WBS details updated successfully');", true);
                                }

                                else
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('WBS details has been Updated,dates already exists for this WBS');", true);
                                }
                                LoadProjextdata();
                                exiteditwbs();
                                loadacty();
                                LoadProjects_Acty();
                                LoadWbs_Acty(DDL_actproj.SelectedValue);
                                loadsrchddl();
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please select valid dates.!');", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "alert('Please enter WBS title');", true);
                    }
                        break;
                    case "CANCELWBS":
                         int canceleindex = Convert.ToInt32(e.CommandArgument);
                         GridViewRow gvcancel = GV_wbs.Rows[canceleindex];
                         SPayc_Collection_BO objspaylst2 = new SPayc_Collection_BO();
                         GV_wbs.DataSource = objspaylst2;
                         GV_wbs.DataBind();
                        LoadProjextdata();
                        exiteditwbs();
                        break;
                }

            }
            catch (Exception ex)
            { }
        }

        protected void Infotab4_Click(object sender, EventArgs e)
        {
            HideSubTabs();
            view2.Visible = false;
            view3.Visible = false;
            view4.Visible = false;
            view5.Visible = true;
            Infotab4.CssClass = "nav-link active p-2";
        }

        protected void DDL_pojct_addwbs_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int? projid = Convert.ToInt32(DDL_pojct_addwbs.SelectedValue.Trim());
                int? pspnr = 0;
                DateTime? begda =Convert.ToDateTime(DateTime.Now.ToString());
                DateTime? engda = Convert.ToDateTime(DateTime.Now.ToString());
                SPayc_Collection_BO objspaylst = new SPayc_Collection_BO();
                SPayc_BL objPaycbl = new SPayc_BL();
                objPaycbl.chk_projects(Session["CompCode"].ToString(), projid, 1, ref pspnr, ref begda, ref engda);

                Session["probegda"]=Convert.ToDateTime(begda.ToString().Trim());
                Session["proendda"] = Convert.ToDateTime(engda.ToString().Trim());
            }
            catch (Exception ex)
            {

            }
        }

       
        protected void btn_viewexistingcompo_Click(object sender, EventArgs e)
        {
            try
            {
                exis_compo.Visible = true;
                btn_reset.Visible = true;
                SPayc_Collection_BO objspaylst = new SPayc_Collection_BO();
                SPaycompute_BO objSpayc = new SPaycompute_BO();
                SPayc_BL objPaycbl = new SPayc_BL();
                objspaylst = objPaycbl.get_compomapping(Session["CompCode"].ToString(), 1);
                GV_existngcompo.DataSource = null;
                GV_existngcompo.DataBind();

                GV_existngcompo.DataSource = objspaylst;
                GV_existngcompo.DataBind();
                btn_viewexistingcompo.Visible = false;


                string frow = "", lrow = "";  ////Row count

                foreach (GridViewRow row in GV_existngcompo.Rows)
                {
                    for (int i = 0; i < GV_existngcompo.Rows.Count; i++)
                    {
                        Label lblRowNumber = (Label)GV_existngcompo.Rows[i].FindControl("lblRowNumber");
                        if (i == 0)
                        {
                            frow = lblRowNumber.Text;
                        }
                        if (i == GV_existngcompo.Rows.Count - 1)
                        {
                            lrow = lblRowNumber.Text;
                        }
                    }
                }
                divcnt.InnerHtml = "Showing " + frow + " to " + lrow + " of " + objspaylst.Count + " entries";
                divcnt.Visible = GV_existngcompo.Rows.Count > 0 ? true : false;
            }
            catch (Exception ex)
            { }
        }

        protected void btn_reset_Click(object sender, EventArgs e)
        {
            try
            {
                exis_compo.Visible = false;
                GV_existngcompo.DataSource = null;
                GV_existngcompo.DataBind();
                btn_reset.Visible = false;
                btn_viewexistingcompo.Visible = true;
            }
            catch (Exception ex)
            { }
        }

        protected void GV_existngcompo_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                exis_compo.Visible = true;
                btn_reset.Visible = true;
                btn_viewexistingcompo.Visible = false;
                SPayc_Collection_BO objspaylst1 = new SPayc_Collection_BO();
                SPayc_BL objPaycbl1 = new SPayc_BL();
                objspaylst1 = objPaycbl1.get_compomapping(Session["CompCode"].ToString(), 1);                
                GV_existngcompo.DataSource = objspaylst1;
                GV_existngcompo.PageIndex = e.NewPageIndex;
                GV_existngcompo.DataBind();

                string frow = "", lrow = "";  ////Row count

                foreach (GridViewRow row in GV_existngcompo.Rows)
                {
                    for (int i = 0; i < GV_existngcompo.Rows.Count; i++)
                    {
                        Label lblRowNumber = (Label)GV_existngcompo.Rows[i].FindControl("lblRowNumber");
                        if (i == 0)
                        {
                            frow = lblRowNumber.Text;///grdFBP_claims.Rows[i].Cells[0].Text;
                        }
                        if (i == GV_existngcompo.Rows.Count - 1)
                        {
                            lrow = lblRowNumber.Text;
                        }
                    }
                }
                divcnt.InnerHtml = "Showing " + frow + " to " + lrow + " of " + objspaylst1.Count + " entries";
                divcnt.Visible = GV_existngcompo.Rows.Count > 0 ? true : false;

            }
            catch (Exception ex)
            { }
        }

        protected void btn_cnfimmap_Click(object sender, EventArgs e)
        {
            try
            {
                SPayc_BL objBl = new SPayc_BL();
                 SPaycompute_BO bo2 = new SPaycompute_BO();
                 DataTable dt2 = new DataTable();
                 bool? st1 = false;

                 using (dt2 = (DataTable)ViewState["tempsalmap"])
                {
                    for (int j = 0; j < dt2.Rows.Count; j++)
                    {
                        bo2.CCD = Session["CompCode"].ToString();
                        bo2.id1 = Convert.ToInt32(dt2.Rows[j]["CompoID"].ToString().Trim());
                        bo2.ID = Convert.ToInt32(dt2.Rows[j]["Compotypval"].ToString().Trim());
                        objBl.map_salry(bo2,2,ref st1);
                    }
                }
                 if (st1 == true)
                 {

                     ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Salary component mapping saved successfully')", true);
                     ViewState["tempsalmap"] = null;
                     Gv_tempmaping.DataSource = null;
                     Gv_tempmaping.DataBind();
                     Gv_tempmaping.Visible = false;
                     btn_cnfimmap.Visible = false;
                     btn_mapcncl.Visible = false;
                     loadmapgv();
                 }
                 else
                 {
                     ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "alert('Unable to save data')", true);
                 }
               
            }
            catch (Exception ex) { }          
           
        }

        protected void btn_mapcncl_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["tempsalmap"] = null;
                Gv_tempmaping.DataSource = null;
                Gv_tempmaping.DataBind();
                Gv_tempmaping.Visible = false;
                btn_cnfimmap.Visible = false;
                btn_mapcncl.Visible = false;
            }

            catch (Exception ex)
            { }
        }

        
        protected void GV_slrymapg_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName.ToUpper())
                {
                    case "EDITMP":
                        mapexitmode();
                        int rowedtmp = Convert.ToInt32(e.CommandArgument);
                        GridViewRow gvRowmp = GV_slrymapg.Rows[rowedtmp];
                        GridViewRow edtrowmp = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);

                        Label lblallowtypedt = (Label)edtrowmp.FindControl("lbl_mapedcompotyp");

                        CheckBox chkallotypedt = (CheckBox)edtrowmp.FindControl("chk_compotyp");

                        LinkButton edtedtmp = (LinkButton)edtrowmp.FindControl("LK_edtsmap");

                        LinkButton extupdtmp = (LinkButton)edtrowmp.FindControl("LK_updtsmap");

                        LinkButton cncledtmp = (LinkButton)edtrowmp.FindControl("LK_cnclsmap");

                        if (lblallowtypedt.Text == "Allowance")
                        {
                            chkallotypedt.Checked = true;
                        }
                        else
                        {
                            chkallotypedt.Checked = false;
                        } 


                        lblallowtypedt.Visible = false;

                        chkallotypedt.Visible = true;

                        edtedtmp.Visible = false;

                        extupdtmp.Visible = true;

                        cncledtmp.Visible = true;
                        
                        break;
                    case "UPDATEMP":
                         int updateindexmp= Convert.ToInt32(e.CommandArgument);

                         GridViewRow gvupdatmp = GV_slrymapg.Rows[updateindexmp];

                         Label lblallowtypupd = (Label)gvupdatmp.FindControl("lbl_mapedcompotyp");

                         CheckBox chkmpupd = (CheckBox)gvupdatmp.FindControl("chk_compotyp");

                         string comtyp = chkmpupd.Checked == true ? "1" : "0";

                         int rowID = Convert.ToInt32(GV_slrymapg.DataKeys[gvupdatmp.RowIndex].Values["ID"].ToString());

                         int CompoID = Convert.ToInt32(GV_slrymapg.DataKeys[gvupdatmp.RowIndex].Values["TXT"].ToString());

                         SPayc_BL objBl = new SPayc_BL();

                        SPaycompute_BO bo = new SPaycompute_BO();
                 
                        bool? stst = false;

                        bo.CCD = Session["CompCode"].ToString();
                        bo.id3 = rowID;
                        bo.id1 = CompoID;
                        bo.ID = Convert.ToInt32(comtyp.ToString().Trim());
                        objBl.map_salry(bo, 3, ref stst);
                        if (stst == true)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Salary component mapping already exists')", true);                            
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "alert('Salary component mapping updated successfully')", true);
                        }

                        loadmapgv();
                        break;
                    case "CANCELMP":
                         int mpcncl = Convert.ToInt32(e.CommandArgument);
                         GridViewRow gvbene = GV_slrymapg.Rows[mpcncl];
                        loadmapgv();
                        break;
                }
            }

            catch (Exception ex)
            { }
        }

        public void loadmapgv()
        {
            SPayc_Collection_BO lst = new SPayc_Collection_BO();
            SPayc_BL bl = new SPayc_BL();
            lst = bl.get_compomapping(Session["CompCode"].ToString(), 2);
            GV_slrymapg.DataSource = lst;
            GV_slrymapg.DataBind();
            mapexitmode();

            string frow = "", lrow = "";  ////Row count

            foreach (GridViewRow row in GV_slrymapg.Rows)
            {
                for (int i = 0; i < GV_slrymapg.Rows.Count; i++)
                {
                    Label lblsalmapRowNumber = (Label)GV_slrymapg.Rows[i].FindControl("lblsalmapRowNumber");
                    if (i == 0)
                    {
                        frow = lblsalmapRowNumber.Text;
                    }
                    if (i == GV_slrymapg.Rows.Count - 1)
                    {
                        lrow = lblsalmapRowNumber.Text;
                    }
                }
            }
            dvcntsalmap.InnerHtml = "Showing " + frow + " to " + lrow + " of " + lst.Count + " entries";
            dvcntsalmap.Visible = GV_slrymapg.Rows.Count > 0 ? true : false;
        }

        protected DataTable Salmap()
        {
            DataTable dtData = new DataTable();
            dtData.Columns.AddRange(new DataColumn[4]
                    { 
                     new DataColumn("CompoID",typeof(string)),
                     new DataColumn("Compname",typeof(string)),
                     new DataColumn("Compotyp",typeof(string)),
                     new DataColumn("Compotypval",typeof(string))
                     });

            return dtData;
        }

        protected void btn_tempmap_Click(object sender, EventArgs e)
        {
            try
            {
                SPaycompute_BO objSpayc = new SPaycompute_BO();
                SPayc_Collection_BO objspaylst1 = new SPayc_Collection_BO();
                SPayc_BL objPaycbl1 = new SPayc_BL();
                bool? status = false;
                objSpayc.CCD = Session["CompCode"].ToString();
                objSpayc.id3 = 0;
                objSpayc.id1 = Convert.ToInt32(DDL_slrymap.SelectedValue);
                objSpayc.ID = Convert.ToInt32(rbnPFApp.SelectedValue);

                objPaycbl1.map_salry(objSpayc, 1, ref status);
                if (status == false)
                {

                    using (DataTable Dt = ViewState["tempsalmap"] != null ? (DataTable)ViewState["tempsalmap"] : Salmap())
                    {
                        Dt.Rows.Add(DDL_slrymap.SelectedValue, DDL_slrymap.SelectedItem, rbnPFApp.SelectedItem.ToString(), rbnPFApp.SelectedValue);
                        ViewState["tempsalmap"] = Dt;
                    }
                    Gv_tempmaping.DataSource = (DataTable)ViewState["tempsalmap"];
                    Gv_tempmaping.DataBind();
                    btn_cnfimmap.Visible = Gv_tempmaping.Rows.Count > 0 ? true : false;
                    btn_mapcncl.Visible = Gv_tempmaping.Rows.Count > 0 ? true : false;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "alert('Salary Component mapping already exists')", true);

                }
            }

            catch (Exception ex)
            { }
        }

        protected void GV_slrymapg_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                SPayc_Collection_BO objspaylst1 = new SPayc_Collection_BO();
                SPayc_BL objPaycbl1 = new SPayc_BL();
                objspaylst1 = objPaycbl1.get_compomapping(Session["CompCode"].ToString(), 2);
                GV_slrymapg.DataSource = objspaylst1;
                GV_slrymapg.PageIndex = e.NewPageIndex;
                GV_slrymapg.DataBind();
                mapexitmode();

                string frow = "", lrow = "";  ////Row count

                foreach (GridViewRow row in GV_slrymapg.Rows)
                {
                    for (int i = 0; i < GV_slrymapg.Rows.Count; i++)
                    {
                        Label lblsalmapRowNumber = (Label)GV_slrymapg.Rows[i].FindControl("lblsalmapRowNumber");
                        if (i == 0)
                        {
                            frow = lblsalmapRowNumber.Text;
                        }
                        if (i == GV_slrymapg.Rows.Count - 1)
                        {
                            lrow = lblsalmapRowNumber.Text;
                        }
                    }
                }
                dvcntsalmap.InnerHtml = "Showing " + frow + " to " + lrow + " of " + objspaylst1.Count + " entries";
                dvcntsalmap.Visible = GV_slrymapg.Rows.Count > 0 ? true : false;
            }
            catch (Exception ex)
            { }
        }


        public void mapexitmode()
        {
            try
            {
                foreach (GridViewRow row in GV_slrymapg.Rows)
                {
                    Label lblextmap = (Label)row.FindControl("lbl_mapedcompotyp");

                    CheckBox chkallodeduext = (CheckBox)row.FindControl("chk_compotyp");

                    LinkButton EDITmap = (LinkButton)row.FindControl("LK_edtsmap");

                    LinkButton UPDATEmap = (LinkButton)row.FindControl("LK_updtsmap");

                    LinkButton CNCLmap = (LinkButton)row.FindControl("LK_cnclsmap");

                    chkallodeduext.Visible = false;
                    lblextmap.Visible = true;
                    EDITmap.Visible = true;
                    UPDATEmap.Visible = false;
                    CNCLmap.Visible = false;


                }
            }

            catch (Exception ex)
            { }

        }

        

        protected void GV_projects_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                SPayc_Collection_BO objspaylst = new SPayc_Collection_BO();
                SPayc_BL objPaycbl = new SPayc_BL();
                objspaylst = objPaycbl.get_projects(Session["CompCode"].ToString(), 1, "", "", "");
                GV_projects.DataSource = objspaylst;
                GV_projects.PageIndex = e.NewPageIndex;
                GV_projects.DataBind();
                exitedit();

                string frow = "", lrow = "";  ////Row count

                foreach (GridViewRow row in GV_projects.Rows)
                {
                    for (int i = 0; i < GV_projects.Rows.Count; i++)
                    {
                        Label lblprojRowNumber = (Label)GV_projects.Rows[i].FindControl("lblprojRowNumber");
                        if (i == 0)
                        {
                            frow = lblprojRowNumber.Text;
                        }
                        if (i == GV_projects.Rows.Count - 1)
                        {
                            lrow = lblprojRowNumber.Text;
                        }
                    }
                }
                dvdntproj.InnerHtml = "Showing " + frow + " to " + lrow + " of " + objspaylst.Count + " entries";
                dvdntproj.Visible = GV_projects.Rows.Count > 0 ? true : false;
            }
            catch (Exception ex)
            { }
        }

        

        protected void GV_wbs_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                SPayc_Collection_BO objspaylst1 = new SPayc_Collection_BO();
                SPayc_BL objPaycbl1 = new SPayc_BL();
                objspaylst1 = objPaycbl1.get_projects(Session["CompCode"].ToString(), 2, "", "", "");
                GV_wbs.DataSource = objspaylst1;
                GV_wbs.PageIndex = e.NewPageIndex;
                GV_wbs.DataBind();
                exiteditwbs();

                string frowbs = "", lrowbs = "";  ////Row count

                foreach (GridViewRow row in GV_wbs.Rows)
                {
                    for (int i = 0; i < GV_wbs.Rows.Count; i++)
                    {
                        Label lblwbsRowNumber = (Label)GV_wbs.Rows[i].FindControl("lblwbsRowNumber");
                        if (i == 0)
                        {
                            frowbs = lblwbsRowNumber.Text;
                        }
                        if (i == GV_wbs.Rows.Count - 1)
                        {
                            lrowbs = lblwbsRowNumber.Text;
                        }
                    }
                }
                dvcntwbs.InnerHtml = "Showing " + frowbs + " to " + lrowbs + " of " + objspaylst1.Count + " entries";
                dvcntwbs.Visible = GV_wbs.Rows.Count > 0 ? true : false;
            }
            catch (Exception ex)
            { }
        }

        protected void GV_acty_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                SPayc_Collection_BO objspaylst2 = new SPayc_Collection_BO();
                SPayc_BL objPaycbl2 = new SPayc_BL();
                objspaylst2 = objPaycbl2.get_projects(Session["CompCode"].ToString(), 3, "", "", "");
                GV_acty.DataSource = objspaylst2;
                GV_acty.PageIndex = e.NewPageIndex;
                GV_acty.DataBind();
                exitacty();

                string frowact = "", lrowact = "";  ////Row count

                foreach (GridViewRow row in GV_acty.Rows)
                {
                    for (int i = 0; i < GV_acty.Rows.Count; i++)
                    {
                        Label lblactyRowNumber = (Label)GV_acty.Rows[i].FindControl("lblactyRowNumber");
                        if (i == 0)
                        {
                            frowact = lblactyRowNumber.Text;
                        }
                        if (i == GV_acty.Rows.Count - 1)
                        {
                            lrowact = lblactyRowNumber.Text;
                        }
                    }
                }
                dvcntacty.InnerHtml = "Showing " + frowact + " to " + lrowact + " of " + objspaylst2.Count + " entries";
                dvcntacty.Visible = GV_acty.Rows.Count > 0 ? true : false;
            }
            catch (Exception ex)
            { }
        }

        protected void GV_atttype_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                SPayc_Collection_BO objspaylst3 = new SPayc_Collection_BO();
                SPayc_BL objPaycbl3 = new SPayc_BL();
                objspaylst3 = objPaycbl3.get_projects(Session["CompCode"].ToString(), 4, "", "", "");
                GV_atttype.DataSource = objspaylst3;
                GV_atttype.PageIndex = e.NewPageIndex;
                GV_atttype.DataBind();
                exitattdmod();

                string frowattd = "", lrowattd = "";  ////Row count

                foreach (GridViewRow row in GV_atttype.Rows)
                {
                    for (int i = 0; i < GV_atttype.Rows.Count; i++)
                    {
                        Label lblattdRowNumber = (Label)GV_atttype.Rows[i].FindControl("lblattdRowNumber");
                        if (i == 0)
                        {
                            frowattd = lblattdRowNumber.Text;
                        }
                        if (i == GV_atttype.Rows.Count - 1)
                        {
                            lrowattd = lblattdRowNumber.Text;
                        }
                    }
                }
                dvcntattd.InnerHtml = "Showing " + frowattd + " to " + lrowattd + " of " + objspaylst3.Count + " entries";
                dvcntattd.Visible = GV_atttype.Rows.Count > 0 ? true : false;
            }
            catch (Exception ex)
            { }
        }

        protected void DDL_srchprjcts_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                SPayc_Collection_BO objspaylst = new SPayc_Collection_BO();
                SPayc_BL objPaycbl = new SPayc_BL();
                objspaylst = objPaycbl.get_projects(Session["CompCode"].ToString(), 5, DDL_srchprjcts.SelectedValue.ToString().Trim(), "", "");
                GV_projects.DataSource = objspaylst;
                GV_projects.DataBind();
                exitedit();

                string frow = "", lrow = "";  ////Row count

                foreach (GridViewRow row in GV_projects.Rows)
                {
                    for (int i = 0; i < GV_projects.Rows.Count; i++)
                    {
                        Label lblprojRowNumber = (Label)GV_projects.Rows[i].FindControl("lblprojRowNumber");
                        if (i == 0)
                        {
                            frow = lblprojRowNumber.Text;
                        }
                        if (i == GV_projects.Rows.Count - 1)
                        {
                            lrow = lblprojRowNumber.Text;
                        }
                    }
                }
                dvdntproj.InnerHtml = "Showing " + frow + " to " + lrow + " of " + objspaylst.Count + " entries";
                dvdntproj.Visible = GV_projects.Rows.Count > 0 ? true : false;

            }

            catch (Exception ex)
            { }

        }

        protected void DDL_srchwbs_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                SPayc_Collection_BO objspaylst1 = new SPayc_Collection_BO();
                SPayc_BL objPaycbl1 = new SPayc_BL();
                objspaylst1 = objPaycbl1.get_projects(Session["CompCode"].ToString(), 6, DDL_srchwbs.SelectedValue.ToString().Trim(), "", "");
                GV_wbs.DataSource = objspaylst1;
                GV_wbs.DataBind();
                exiteditwbs();

                string frowbs = "", lrowbs = "";  ////Row count

                foreach (GridViewRow row in GV_wbs.Rows)
                {
                    for (int i = 0; i < GV_wbs.Rows.Count; i++)
                    {
                        Label lblwbsRowNumber = (Label)GV_wbs.Rows[i].FindControl("lblwbsRowNumber");
                        if (i == 0)
                        {
                            frowbs = lblwbsRowNumber.Text;
                        }
                        if (i == GV_wbs.Rows.Count - 1)
                        {
                            lrowbs = lblwbsRowNumber.Text;
                        }
                    }
                }
                dvcntwbs.InnerHtml = "Showing " + frowbs + " to " + lrowbs + " of " + objspaylst1.Count + " entries";
                dvcntwbs.Visible = GV_wbs.Rows.Count > 0 ? true : false;
            }
            catch (Exception ex)
            { }
        }

        protected void DDL_srchattd_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                btn_srchattdrest.Visible = true;
                SPayc_Collection_BO objspaylst2 = new SPayc_Collection_BO();
                SPayc_BL objPaycbl2 = new SPayc_BL();
                objspaylst2 = objPaycbl2.get_projects(Session["CompCode"].ToString(), 8, DDL_srchattd.SelectedValue.ToString().Trim(), "", "");
                GV_atttype.DataSource = objspaylst2;
                GV_atttype.DataBind();
                exitattdmod();

                string frowattd = "", lrowattd = "";  ////Row count

                foreach (GridViewRow row in GV_atttype.Rows)
                {
                    for (int i = 0; i < GV_atttype.Rows.Count; i++)
                    {
                        Label lblattdRowNumber = (Label)GV_atttype.Rows[i].FindControl("lblattdRowNumber");
                        if (i == 0)
                        {
                            frowattd = lblattdRowNumber.Text;
                        }
                        if (i == GV_atttype.Rows.Count - 1)
                        {
                            lrowattd = lblattdRowNumber.Text;
                        }
                    }
                }
                dvcntattd.InnerHtml = "Showing " + frowattd + " to " + lrowattd + " of " + objspaylst2.Count + " entries";
                dvcntattd.Visible = GV_atttype.Rows.Count > 0 ? true : false;
            }
            catch (Exception ex)
            { }
        }

        protected void DDL_srchacty_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                btn_srchact.Visible = true;
                SPayc_Collection_BO objspaylst3 = new SPayc_Collection_BO();
                SPayc_BL objPaycbl3 = new SPayc_BL();
                objspaylst3 = objPaycbl3.get_projects(Session["CompCode"].ToString(), 7, DDL_srchacty.SelectedValue.ToString(), "", "");
                GV_acty.DataSource = objspaylst3;
                GV_acty.DataBind();
                exitacty();

                string frowact = "", lrowact = "";  ////Row count

                foreach (GridViewRow row in GV_acty.Rows)
                {
                    for (int i = 0; i < GV_acty.Rows.Count; i++)
                    {
                        Label lblactyRowNumber = (Label)GV_acty.Rows[i].FindControl("lblactyRowNumber");
                        if (i == 0)
                        {
                            frowact = lblactyRowNumber.Text;
                        }
                        if (i == GV_acty.Rows.Count - 1)
                        {
                            lrowact = lblactyRowNumber.Text;
                        }
                    }
                }
                dvcntacty.InnerHtml = "Showing " + frowact + " to " + lrowact + " of " + objspaylst3.Count + " entries";
                dvcntacty.Visible = GV_acty.Rows.Count > 0 ? true : false;
                
            }
            catch (Exception ex)
            { }
        }


        public void loadsrchddl()
        {
            try
            {

                SPayc_Collection_BO objspaylst3 = new SPayc_Collection_BO();
                SPayc_BL objPaycbl3 = new SPayc_BL();
                objspaylst3 = objPaycbl3.load_prjctwbsact(Session["CompCode"].ToString(),1);
                DDL_srchprjcts.DataSource = objspaylst3;
                DDL_srchprjcts.DataTextField = "col11";
                DDL_srchprjcts.DataValueField = "id1";
                DDL_srchprjcts.DataBind();
                DDL_srchprjcts.Items.Insert(0, new ListItem("Select", "0"));


                SPayc_Collection_BO objlst = new SPayc_Collection_BO();
                SPayc_BL bl = new SPayc_BL();
                objlst = bl.load_prjctwbsact(Session["CompCode"].ToString(), 2);
                DDL_srchwbs.DataSource = objlst;
                DDL_srchwbs.DataTextField = "col11";
                DDL_srchwbs.DataValueField = "id1";
                DDL_srchwbs.DataBind();
                DDL_srchwbs.Items.Insert(0, new ListItem("Select", "0"));

                SPayc_Collection_BO objlst1 = new SPayc_Collection_BO();
                SPayc_BL bl1 = new SPayc_BL();
                objlst1 = bl1.load_prjctwbsact(Session["CompCode"].ToString(), 3);
                DDL_srchacty.DataSource = objlst1;
                DDL_srchacty.DataTextField = "col11";
                DDL_srchacty.DataValueField = "id1";
                DDL_srchacty.DataBind();
                DDL_srchacty.Items.Insert(0, new ListItem("Select", "0"));


                SPayc_Collection_BO objlst2 = new SPayc_Collection_BO();
                SPayc_BL bl2 = new SPayc_BL();
                objlst2 = bl2.load_prjctwbsact(Session["CompCode"].ToString(), 4);
                DDL_srchattd.DataSource = objlst2;
                DDL_srchattd.DataTextField = "col11";
                DDL_srchattd.DataValueField = "id1";
                DDL_srchattd.DataBind();
                DDL_srchattd.Items.Insert(0, new ListItem("Select", "0"));

            }
            catch (Exception ex)
            { }
        }

        protected void DDL_srchpjctby_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (DDL_srchpjctby.SelectedValue == "0")
                {
                    txt_srchprojbegda.Text = "";
                    txt_srchprojendda.Text = "";
                    btn_srchprojreset.Visible = false;
                    btn_srchproj.Visible = false;
                    DDL_srchprjcts.Visible = false;
                    txt_srchprojbegda.Visible = false;
                    txt_srchprojendda.Visible = false;
                    LoadProjextdata();
                }
                else if (DDL_srchpjctby.SelectedValue == "1")
                {
                    DDL_srchprjcts.Visible = true;
                    txt_srchprojbegda.Visible = false;
                    txt_srchprojendda.Visible = false;
                    btn_srchproj.Visible = false;
                    btn_srchprojreset.Visible = true;
                }
                else if (DDL_srchpjctby.SelectedValue == "2")
                {
                    DDL_srchprjcts.Visible = false;
                    txt_srchprojbegda.Visible = true;
                    txt_srchprojendda.Visible = true;
                    btn_srchproj.Visible = true;
                    btn_srchprojreset.Visible = true;
                }

            }
            catch (Exception ex)
            { }
        }

        protected void btn_srchprojreset_Click(object sender, EventArgs e)
        {
            try
            {
                txt_srchprojbegda.Text = "";
                txt_srchprojendda.Text = "";
                btn_srchproj.Visible = false;
                btn_srchprojreset.Visible = false;
                DDL_srchprjcts.Visible = false;
                txt_srchprojbegda.Visible = false;
                txt_srchprojendda.Visible = false;
                LoadProjextdata();
            }
            catch (Exception ex)
            { }
        }

        protected void btn_srchproj_Click(object sender, EventArgs e)
        {
            try
            {
                SPayc_Collection_BO objspaylst = new SPayc_Collection_BO();
                SPayc_BL objPaycbl = new SPayc_BL();
                objspaylst = objPaycbl.get_projects(Session["CompCode"].ToString(), 9, "", txt_srchprojbegda.Text.Trim(), txt_srchprojendda.Text.Trim());
                GV_projects.DataSource = objspaylst;
                GV_projects.DataBind();
                exitedit();

                string frow = "", lrow = "";  ////Row count

                foreach (GridViewRow row in GV_projects.Rows)
                {
                    for (int i = 0; i < GV_projects.Rows.Count; i++)
                    {
                        Label lblprojRowNumber = (Label)GV_projects.Rows[i].FindControl("lblprojRowNumber");
                        if (i == 0)
                        {
                            frow = lblprojRowNumber.Text;
                        }
                        if (i == GV_projects.Rows.Count - 1)
                        {
                            lrow = lblprojRowNumber.Text;
                        }
                    }
                }
                dvdntproj.InnerHtml = "Showing " + frow + " to " + lrow + " of " + objspaylst.Count + " entries";
                dvdntproj.Visible = GV_projects.Rows.Count > 0 ? true : false;
            }
            catch (Exception ex)
            { }
        }

        protected void DDL_srchwbsby_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (DDL_srchwbsby.SelectedValue == "0")
                {
                    txt_srchwbsbegda.Text = "";
                    txt_srchwbsendda.Text = "";
                    btn_srchwbsdates.Visible = false;
                    btn_srcwbsreset.Visible=false;
                    DDL_srchwbs.Visible = false;
                    txt_srchwbsbegda.Visible = false;
                    txt_srchwbsendda.Visible = false;
                    LoadProjextdata();
                }
                else if (DDL_srchwbsby.SelectedValue == "1")
                {
                    DDL_srchwbs.Visible = true;
                    btn_srchwbsdates.Visible = false;
                    txt_srchwbsbegda.Visible = false;
                    txt_srchwbsendda.Visible = false;
                    btn_srcwbsreset.Visible = true;
                }
                else if (DDL_srchwbsby.SelectedValue == "2")
                {
                    DDL_srchwbs.Visible = false;
                    btn_srchwbsdates.Visible = true;
                    txt_srchwbsbegda.Visible = true;
                    txt_srchwbsendda.Visible = true;
                    btn_srchproj.Visible = true;
                    btn_srcwbsreset.Visible = true;
                }

            
            }
            catch (Exception ex)
            { }
        }

        protected void btn_srchwbsdates_Click(object sender, EventArgs e)
        {
            try
            {
                SPayc_Collection_BO objspaylst1 = new SPayc_Collection_BO();
                SPayc_BL objPaycbl1 = new SPayc_BL();
                objspaylst1 = objPaycbl1.get_projects(Session["CompCode"].ToString(), 10, "", txt_srchwbsendda.Text.Trim(), txt_srchwbsendda.Text.Trim());
                GV_wbs.DataSource = objspaylst1;
                GV_wbs.DataBind();
                exiteditwbs();

                string frowbs = "", lrowbs = "";  ////Row count

                foreach (GridViewRow row in GV_wbs.Rows)
                {
                    for (int i = 0; i < GV_wbs.Rows.Count; i++)
                    {
                        Label lblwbsRowNumber = (Label)GV_wbs.Rows[i].FindControl("lblwbsRowNumber");
                        if (i == 0)
                        {
                            frowbs = lblwbsRowNumber.Text;
                        }
                        if (i == GV_wbs.Rows.Count - 1)
                        {
                            lrowbs = lblwbsRowNumber.Text;
                        }
                    }
                }
                dvcntwbs.InnerHtml = "Showing " + frowbs + " to " + lrowbs + " of " + objspaylst1.Count + " entries";
                dvcntwbs.Visible = GV_wbs.Rows.Count > 0 ? true : false;
            }
            catch (Exception ex)
            { }
        }

        protected void btn_srcwbsreset_Click(object sender, EventArgs e)
        {
            try
            {
                txt_srchwbsbegda.Text = "";
                txt_srchwbsendda.Text = "";
                btn_srchwbsdates.Visible = false;
                btn_srcwbsreset.Visible = false;
                DDL_srchwbs.Visible = false;
                txt_srchwbsbegda.Visible = false;
                txt_srchwbsendda.Visible = false;
                LoadProjextdata();
            }
            catch (Exception ex)
            { }
        }

        protected void btn_srchact_Click(object sender, EventArgs e)
        {
            try
            {
                btn_srchact.Visible = false;
                LoadProjextdata();
            }
            catch (Exception ex)
            { }
        }

        protected void btn_srchattdrest_Click(object sender, EventArgs e)
        {
            try
            {
                btn_srchattdrest.Visible = false;
                LoadProjextdata();
            }
            catch (Exception ex)
            { }
        }

        protected void GV_acty_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName.ToUpper())
                {
                    case "EDITACTY":
                        exitacty();
                    int rowIndexacty = Convert.ToInt32(e.CommandArgument);

                    GridViewRow gvRowacty = GV_acty.Rows[rowIndexacty];

                    GridViewRow rowacty = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);

                    Label lblactyedt = (Label)rowacty.FindControl("lbl_acty");

                    TextBox txtactyedt = (TextBox)rowacty.FindControl("txt_acty");

                    LinkButton editedtacty = (LinkButton)rowacty.FindControl("LK_edtacty");

                    LinkButton updteedtacty = (LinkButton)rowacty.FindControl("LK_updacty");

                    LinkButton cncledtacty = (LinkButton)rowacty.FindControl("LK_cnclacty");

                    lblactyedt.Visible = false;

                    txtactyedt.Visible = true;

                    editedtacty.Visible = false;
                    updteedtacty.Visible = true;
                    cncledtacty.Visible = true;
                        break;
                    case "UPDACTY":
                        int updateindexacty = Convert.ToInt32(e.CommandArgument);
                        GridViewRow gvupdateacty = GV_acty.Rows[updateindexacty];
                        GridViewRow rowupdtacty = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);

                        Label lblupdacty = (Label)rowupdtacty.FindControl("lbl_acty");

                        TextBox txtupdacty = (TextBox)rowupdtacty.FindControl("txt_acty");

                        int IDacty = Convert.ToInt32(GV_acty.DataKeys[rowupdtacty.RowIndex].Values["id1"].ToString());

                        string IDactywbs = GV_acty.DataKeys[rowupdtacty.RowIndex].Values["col16"].ToString();

                        if (txtupdacty.Text != "")
                        {
                            bool? status = false;
                            SPaycompute_BO objSpayc = new SPaycompute_BO();
                            SPayc_BL objPaycbl = new SPayc_BL();
                            objSpayc.CCD = Session["CompCode"].ToString().ToLower().Trim();
                            objSpayc.begda = DateTime.Now;
                            objSpayc.endda = DateTime.Now;
                            objSpayc.ID = IDacty;
                            objSpayc.col1 = "";
                            objSpayc.col2 = IDactywbs.ToString().Trim();
                            objSpayc.id2 = 3;
                            objSpayc.col13 = "";
                            objSpayc.col14 = txtupdacty.Text.Trim();
                            objPaycbl.update_projectdtls(objSpayc, ref status);

                            if (status == true)
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Successfull", "alert('Activity Type updated successfully');", true);
                            }

                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Activity Type already exists');", true);
                            }
                            loadacty();
                            loadsrchddl();
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please enter Activity Type.!');", true);
                        }

                        loadacty();
                        break;
                    case "CANACTY":
                         int actycncl = Convert.ToInt32(e.CommandArgument);
                         GridViewRow gvacty = GV_acty.Rows[actycncl];
                         loadacty();
                        break;
                }
            }
            catch (Exception ex)
            { }

        }

        public void loadacty()
        {
            try
            {
                SPayc_Collection_BO objspaylst2 = new SPayc_Collection_BO();
                SPayc_BL objPaycbl2 = new SPayc_BL();
                objspaylst2 = objPaycbl2.get_projects(Session["CompCode"].ToString(), 3, "", "", "");
                GV_acty.DataSource = objspaylst2;
                GV_acty.DataBind();
                exitacty();
            }
            catch (Exception ex)
            { }
        }



        public void exitacty()
        {
            try
            {
                foreach (GridViewRow row in GV_acty.Rows)
                {
                    Label LBLactyext= (Label)row.FindControl("lbl_acty");

                    TextBox txtactyext = (TextBox)row.FindControl("txt_acty");

                    LinkButton actyEDIT = (LinkButton)row.FindControl("LK_edtacty");

                    LinkButton actyUPDATE = (LinkButton)row.FindControl("LK_updacty");

                    LinkButton actyCNCL = (LinkButton)row.FindControl("LK_cnclacty");

                    txtactyext.Visible = false;

                    LBLactyext.Visible = true;

                    actyEDIT.Visible = true;
                    actyUPDATE.Visible = false;
                    actyCNCL.Visible = false;


                }
            }

            catch (Exception ex)
            {
               
            }

        }

        protected void GV_atttype_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName.ToUpper())
                {
                    case "ATTDEDIT":
                    exitattdmod();

                   int rowIndexattd = Convert.ToInt32(e.CommandArgument);

                    GridViewRow gvattdRow = GV_atttype.Rows[rowIndexattd];

                    GridViewRow rowattd = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);

                    Label lbledtattd = (Label)rowattd.FindControl("lbl_attdtyp");

                    TextBox txtedtattd = (TextBox)rowattd.FindControl("txt_attdtyp");

                    LinkButton editattdedt = (LinkButton)rowattd.FindControl("LK_attdedt");

                    LinkButton updteedtattd = (LinkButton)rowattd.FindControl("LK_attdupd");

                    LinkButton cncledtattd = (LinkButton)rowattd.FindControl("LK_attdcncl");

                    lbledtattd.Visible = false;

                    txtedtattd.Visible = true;

                    editattdedt.Visible = false;
                    updteedtattd.Visible = true;
                    cncledtattd.Visible = true;
                        break;

                    case "ATTDUPD":
                        int updateattdindex = Convert.ToInt32(e.CommandArgument);
                        GridViewRow gvupdateattd = GV_atttype.Rows[updateattdindex];
                        GridViewRow rowupdtattd = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);

                        Label lblattdupd = (Label)rowupdtattd.FindControl("lbl_attdtyp");

                        TextBox txtattdupd = (TextBox)rowupdtattd.FindControl("txt_attdtyp");

                        int IDattd = Convert.ToInt32(GV_atttype.DataKeys[rowupdtattd.RowIndex].Values["id1"].ToString());

                        if (txtattdupd.Text != "")
                        {
                            bool? status = false;
                            SPaycompute_BO objSpayc = new SPaycompute_BO();
                            SPayc_BL objPaycbl = new SPayc_BL();
                            objSpayc.CCD = Session["CompCode"].ToString().ToLower().Trim();
                            objSpayc.begda = DateTime.Now;
                            objSpayc.endda = DateTime.Now;
                            objSpayc.ID = IDattd;
                            objSpayc.col1 = "";
                            objSpayc.col2 = "";
                            objSpayc.id2 = 4;
                            objSpayc.col13 = "";
                            objSpayc.col14 = txtattdupd.Text.Trim();
                            objPaycbl.update_projectdtls(objSpayc, ref status);

                            if (status == true)
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Successfull", "alert('Attendance Type updated successfully');", true);
                            }

                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Attendance Type already exists');", true);
                            }
                            
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Please enter Attendance Type.!');", true);
                        }
                        loadattdgv();
                        loadsrchddl();
                       
                        break;

                    case "ATTDCAN":
                         int cnclattd = Convert.ToInt32(e.CommandArgument);
                         GridViewRow gvattd = GV_atttype.Rows[cnclattd];
                         loadattdgv();
                        break;
                }
            }
            catch (Exception ex)
            {

            }
        }


        public void exitattdmod()
        {
            try
            {
                foreach (GridViewRow row in GV_atttype.Rows)
                {
                    Label LBLextattd = (Label)row.FindControl("lbl_attdtyp");

                    TextBox txtextattd = (TextBox)row.FindControl("txt_attdtyp");

                    LinkButton EDITattd = (LinkButton)row.FindControl("LK_attdedt");

                    LinkButton UPDATEattd = (LinkButton)row.FindControl("LK_attdupd");

                    LinkButton CNCLattd = (LinkButton)row.FindControl("LK_attdcncl");

                    txtextattd.Visible = false;

                    LBLextattd.Visible = true;

                    EDITattd.Visible = true;
                    UPDATEattd.Visible = false;
                    CNCLattd.Visible = false;


                }
            }

            catch (Exception ex)
            {

            }

        }

        public void loadattdgv()
        {
            try
            {
                SPayc_Collection_BO objspaylst3 = new SPayc_Collection_BO();
                SPayc_BL objPaycbl3 = new SPayc_BL();
                objspaylst3 = objPaycbl3.get_projects(Session["CompCode"].ToString(), 4, "", "", "");
                GV_atttype.DataSource = objspaylst3;
                GV_atttype.DataBind();
                exitattdmod();
            }
            catch (Exception ex)
            {

            }
        }

        protected void Tab3_Click(object sender, EventArgs e)
        {
            HideTabs();
            View6.Visible = true;
            view1.Visible = false;
            info.Visible = false;
            Tab3.CssClass = "nav-link active p-2";

            HideSubTabs();
            view2.Visible = false;
            Infotab1.CssClass = "nav-link active p-2";
         


        }

        protected void btnUploadPRData_Click(object sender, EventArgs e)
        {
            View6.Visible = true;

            try
            {
                string path = Server.MapPath("~/MyFolder/" + uflPRData.FileName);
                uflPRData.SaveAs(Server.MapPath("~/PayCompute_Files/PR_Info/" + uflPRData.FileName));
                //string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(Server.MapPath("~/Uploads/" + uflPRData.FileName));
                string excelPath = Server.MapPath("~/PayCompute_Files/PR_Info/" + User.Identity.Name + "-" + (uflPRData.FileName) + "-" + DateTime.Now.ToString("yyyy_MM_dd") + Path.GetExtension(uflPRData.FileName));
                //Server.MapPath("~/PayCompute_Files/Emp_info/") + Path.GetFileName(uflEmpData.PostedFile.FileName);
                uflPRData.SaveAs(excelPath);


                string conString = string.Empty;
                string extension = Path.GetExtension(uflPRData.PostedFile.FileName);
                switch (extension)
                {
                    case ".xls": //Excel 97-03
                        conString = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                        break;
                    case ".xlsx": //Excel 07 or higher
                        conString = ConfigurationManager.ConnectionStrings["Excel07+ConString"].ConnectionString;
                        break;
                }

                conString = string.Format(conString, excelPath);
                using (OleDbConnection excel_con = new OleDbConnection(conString))
                {
                    excel_con.Open();
                    string PR = excel_con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null).Rows[0]["TABLE_NAME"].ToString().Trim();
                    DataTable dtExcelData = PRDt();

                    using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT CID,C_DESC,A_DESC,B_DESC,EKGRP,EKNAM,WERKS,NAME1,SPART,VTEXT,REGION_ID,REGION_TEXT,ISOCODE,ISOTXT,POSID,POST1,PSPHI,VERNR,VERNA,PBUKR,POSKI,ZZDEL_HEAD,ZZDEL_HEADNAME,ZZPERNR01,ZZENAME01,ZZROLE01,,ZZPERNR02,ZZENAME02,ZZROLE02,ZZPERNR03,ZZENAME03,ZZROLE03,ZZPERNR04,ZZENAME04,ZZROLE0,ZZPERNR05,ZZENAME05,ZZROLE05,ZZPERNR06,ZZENAME06,ZZROLE06,ZZPERNR07,ZZENAME07,ZZROLE07,STAT,Created_By,Created_on,Company_Code,Start_Date,End_Date,Updated_On,Updated_By,WBS_EXTNID,PSPNR FROM  [PR_Info] ", excel_con))
                    {
                        oda.Fill(dtExcelData);
                        for (int i = dtExcelData.Rows.Count - 1; i >= 0; i += -1)
                        {
                            DataRow row = dtExcelData.Rows[i];
                            if (row[0] == null)
                            {
                                dtExcelData.Rows.Remove(row);
                            }
                            else if (string.IsNullOrEmpty(row[0].ToString().Trim()))
                            {
                                dtExcelData.Rows.Remove(row);
                            }
                        }
                        dtExcelData.AcceptChanges();
                    }

                    //DataTable dtExcelData1 = ConTypesDt();

                    //using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT Employee_ID,Contact_Type,Contact_Type_ID FROM [Contact_Info$] where 'Employee_ID != '''", excel_con))
                    //{
                    //    oda.Fill(dtExcelData1);
                    //    for (int i = dtExcelData1.Rows.Count - 1; i >= 0; i += -1)
                    //    {
                    //        DataRow row = dtExcelData1.Rows[i];
                    //        if (row[0] == null)
                    //        {
                    //            dtExcelData1.Rows.Remove(row);
                    //        }
                    //        else if (string.IsNullOrEmpty(row[0].ToString().Trim()))
                    //        {
                    //            dtExcelData1.Rows.Remove(row);
                    ////        }
                    //    }
                    //    dtExcelData1.AcceptChanges();
                    //}


                    //DataTable dtExcelData2 = DocTypesDt();
                    //using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT Employee_ID,Document_Type,Document_Type_ID FROM [Document_Info$] where 'Employee_ID != '''", excel_con))
                    //{
                    //    oda.Fill(dtExcelData2);
                    //    for (int i = dtExcelData2.Rows.Count - 1; i >= 0; i += -1)
                    //    {
                    //        DataRow row = dtExcelData2.Rows[i];
                    //        if (row[0] == null)
                    //        {
                    //            dtExcelData2.Rows.Remove(row);
                    //        }
                    //        else if (string.IsNullOrEmpty(row[0].ToString().Trim()))
                    //        {
                    //            dtExcelData2.Rows.Remove(row);
                    //        }
                    //    }
                    //    dtExcelData2.AcceptChanges();
                    //}
                    excel_con.Close();


                    GV_EmpInfo.DataSource = dtExcelData;
                    GV_EmpInfo.DataBind();

                    gv_dept.DataSource = dtExcelData;
                    gv_dept.DataBind();

                    GV_BankInfo.DataSource = dtExcelData;
                    GV_BankInfo.DataBind();

                    GV_AddressInfo.DataSource = dtExcelData;
                    GV_AddressInfo.DataBind();

                    //GV_ContInfo.DataSource = dtExcelData1;
                    //GV_ContInfo.DataBind();

                    //GV_DocInfo.DataSource = dtExcelData2;
                    //GV_DocInfo.DataBind();

                    GV_Benefits.DataSource = dtExcelData;
                    GV_Benefits.DataBind();
                    divgrds.Visible = true;
                    btnSave.Visible = true;
                    btnClear.Visible = true;
                    ViewState["EmpDataDt"] = dtExcelData;
                    //ViewState["EmpContDt"] = dtExcelData1;
                    //ViewState["EmpDoctDt"] = dtExcelData2;
                }
            }
            catch (Exception ex)
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + ex.Message + "')", true);
            }
        }


        protected DataTable PRDt()
        {
            DataTable dtExcelData = new DataTable();

            dtExcelData.Columns.AddRange(new DataColumn [54]

                    { 
                        new DataColumn("CID", typeof(string)),
                        new DataColumn("C_DESC", typeof(string)),
                        new DataColumn("A_DESC", typeof(string)),
                        new DataColumn("B_DESC", typeof(string)),
                        new DataColumn("EKGRP", typeof(string)),
                        new DataColumn("EKNAM",typeof(string)),
                        new DataColumn("WERKS",typeof(string)),
                        new DataColumn("NAME1",typeof(string)),
                         new DataColumn("SPART",typeof(string)),
                         new DataColumn("VTEXT",typeof(string)), 
                         new DataColumn("REGION_ID",typeof(string)),
                         new DataColumn("REGION_TEXT",typeof(string)),
                         new DataColumn("ISOCODE",typeof(string)),
                         new DataColumn("ISOTXT",typeof(string)),
                         new DataColumn("POSID",typeof(string)),
                         new DataColumn("POST1",typeof(string)),
                         new DataColumn("PSPHI",typeof(string)),
                         new DataColumn("VERNR",typeof(string)),
                         new DataColumn("VERNA",typeof(string)),
                         new DataColumn("PBUKR",typeof(string)),
                         new DataColumn("POSKI",typeof(string)),
                         new DataColumn("ZZDEL_HEAD",typeof(string)),
                         new DataColumn("ZZDEL_HEADNAME",typeof(string)),
                         new DataColumn("ZZPERNR01",typeof(string)),
                         new DataColumn("ZZENAME01",typeof(string)),
                         new DataColumn("ZZROLE01",typeof(string)),
                         new DataColumn("ZZPERNR02",typeof(string)),
                         new DataColumn("ZZENAME02",typeof(string)),
                         new DataColumn("ZZROLE02",typeof(string)),
                         new DataColumn("ZZPERNR03",typeof(string)),
                         new DataColumn("ZZENAME03",typeof(string)),
                         new DataColumn("ZZROLE03",typeof(string)),
                         new DataColumn("ZZPERNR04",typeof(string)),
                         new DataColumn("ZZENAME04",typeof(string)),
                         new DataColumn("ZZROLE04",typeof(string)),
                         new DataColumn("ZZPERNR05",typeof(string)),
                         new DataColumn("ZZENAME05",typeof(string)),
                         new DataColumn("ZZROLE05",typeof(string)),
                         new DataColumn("ZZPERNR06",typeof(string)),
                         new DataColumn("ZZENAME06",typeof(string)),
                         new DataColumn("ZZROLE06",typeof(string)),
                         new DataColumn("ZZPERNR07",typeof(string)),
                         new DataColumn("ZZENAME07",typeof(string)),
                         new DataColumn("ZZROLE07",typeof(string)),
                         new DataColumn("STAT",typeof(string)),
                         new DataColumn("Created_By",typeof(string)),
                         new DataColumn("Created_on",typeof(string)),
                         new DataColumn("Company_Code",typeof(string)),
                         new DataColumn("Start_Date",typeof(string)),
                         new DataColumn("End_Date",typeof(string)),
                         new DataColumn("Updated_On",typeof(string)),
                         new DataColumn("Updated_By",typeof(string)),
                         new DataColumn("WBS_EXTNID",typeof(string)),
                         new DataColumn("PSPNR",typeof(string)),
                        


                        
                         
                     });

            return dtExcelData;
        }
    }
}