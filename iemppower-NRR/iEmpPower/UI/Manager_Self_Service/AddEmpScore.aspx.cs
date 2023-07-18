using iEmpPower.Old_App_Code.iEmpPowerBL.Skill_Matrix;
using iEmpPower.Old_App_Code.iEmpPowerBO.Personal_Information.CollectionBO;
using iEmpPower.Old_App_Code.iEmpPowerBO.Skill_Matrix;
using iEmpPower.Old_App_Code.iEmpPowerDAL.User_Account;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System;
using System.Configuration;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.Services;
using System.Web.UI.WebControls;



namespace iEmpPower.UI.Manager_Self_Service
{
    public partial class AddEmpScore : System.Web.UI.Page
    {
        DataTable table = new DataTable();



        protected void Page_Load(object sender, EventArgs e)
        {
               txtmonth_cldr.Enabled = false;
            txtmonth_cldr.Text = DateTime.Today.ToString("MM/yyyy");

            if (!IsPostBack)
            {


                Mult_Add_Scores.SetActiveView(View_Add_Scores);

                if (Session["GVBind"] != null)
                {
                    GV_TmpoScore.DataSource = (DataTable)Session["GVBind"];
                    GV_TmpoScore.DataBind();
                    btnsavescore.Visible = true;
                    
                }
                else
                {
                    btnsavescore.Visible = false;
                }
                DDL_load_allemp_tosrch();
                DDL_modulelst_tosrch();
                

                DDL_modulelst_View();
                DDL_Emplst_View();
                DDL_modulelst();
                DDL_Emplst();
               
                Tabledata();
                ddl_srch_Modl.Visible = false;
                ddl_srch_Enam.Visible = false;
                txt_Vw_month.Visible = false;

                DDL_module_searchby.Visible = false;
                DDL_subM_searchby.Visible = false;
                DDL_emp_searchby.Visible = false;

                Skill_Matrix_BO objskillbo = new Skill_Matrix_BO();
                Skill_Matrix_BL objskillmodBL = new Skill_Matrix_BL();
                objskillbo.PERNR = HttpContext.Current.User.Identity.Name;
                Skill_Matrix_Collectionbo objskilllst = objskillmodBL.get_subordinate_list(User.Identity.Name);
                if (objskilllst.Count > 0)
                {
                    Mult_Add_Scores.SetActiveView(View_Add_Scores);
                }
                else
                {
                    Mult_Add_Scores.SetActiveView(View_Score_Details);
                    GetHRPERNRS();
                    lnk_view_mthod();
                   

                    //btn_VExport.Visible = gv_Viewscores.Rows.Count > 0 ? true : false;
                    //btn_VEXl.Visible = gv_Viewscores.Rows.Count > 0 ? true : false;
                    Lnk_Back.Visible = false;


                }
                bool bSortedOrder = false;
                Session.Add("bSortedOrder", bSortedOrder);
            }
           
        }


        //-------------------------------------------SKILL MATRIX SUBORDINATE DROPDOWN--------------------------------------------
        protected void DDL_Skill_Ename_SelectedIndexChanged(object sender, EventArgs e)
        {
            //lbl_errmssg.Text = "";
        }

        public void DDL_Emplst()
        {
            Skill_Matrix_Collectionbo objskilllst = new Skill_Matrix_Collectionbo();
            Skill_Matrix_BL objskillmodBL = new Skill_Matrix_BL();
            objskilllst = objskillmodBL.Set_subordinate_DDL(User.Identity.Name, 1);
            DDL_Skill_Ename.DataSource = null;
            DDL_Skill_Ename.DataBind();

            DDL_Skill_Ename.DataSource = objskilllst;
            DDL_Skill_Ename.DataValueField = "EmpIDPA0001";
            DDL_Skill_Ename.DataTextField = "EmpNamePA0001";
            DDL_Skill_Ename.DataBind();
            DDL_Skill_Ename.Items.Insert(0, new System.Web.UI.WebControls.ListItem(" - SELECT - ", "0"));
            //lbl_errmssg.Text = "";
        }


        //-------------------------------------------- SKILL MATRIXMODULE DROPDOWN---------------------------------------------------
        protected void DDL_Skillmod_SelectedIndexChanged(object sender, EventArgs e)
        {
            DDL_submoduleLst();

        }
        public void DDL_modulelst()
        {
            Skill_Matrix_Collectionbo objskilllst = new Skill_Matrix_Collectionbo();
            Skill_Matrix_BL objskillmodBL = new Skill_Matrix_BL();
            objskilllst = objskillmodBL.Set_Skill_Details();
            DDL_Skillmod.DataSource = null;
            DDL_Skillmod.DataBind();

            DDL_Skillmod.DataSource = objskilllst;
            DDL_Skillmod.DataTextField = "MODULE";
            DDL_Skillmod.DataValueField = "MID";
            DDL_Skillmod.DataBind();
            DDL_Skillmod.Items.Insert(0, new System.Web.UI.WebControls.ListItem(" - SELECT - ", "0"));
        }


        //------------------------------------------ MATRIX SUB-MODULE DROPDOWN ----------------------------------------------------

        public void DDL_submoduleLst()
        {
            Skill_Matrix_Collectionbo objskilllst = new Skill_Matrix_Collectionbo();
            Skill_Matrix_BL objskillmodBL = new Skill_Matrix_BL();
            objskilllst = objskillmodBL.Set_submodule_DDL(DDL_Skillmod.SelectedValue.ToString());
            DDL_Submodule.DataSource = null;
            DDL_Submodule.DataBind();

            DDL_Submodule.DataSource = objskilllst;
            DDL_Submodule.DataTextField = "S_MODULE";
            DDL_Submodule.DataValueField = "SID";
            DDL_Submodule.DataBind();
            DDL_Submodule.Items.Insert(0, new System.Web.UI.WebControls.ListItem(" - SELECT - ", "0"));
            //lbl_errmssg.Text = "";
        }

        //-------------------------------------------- CLEARING DATA FROM CONTROLS ---------------------------------------------------
        public void DDL_sessionclear()
        {
            DDL_Emplst();
            DDL_Skill_Ename.SelectedIndex = -1;
            DDL_Skillmod.SelectedIndex = -1;
            DDL_Submodule.SelectedIndex = -1;
            //txtmonth_cldr.Text = "";
            txt_Score.Text = "";
            txt_Score_commnt.Text = "";

        }


        //-------------------------------------------- CLEAR  ---------------------------------------------------
        protected void Lk_Clear_Click(object sender, EventArgs e)
        {
            Lk_Clear.Text = "Clear";
            DDL_sessionclear();
            DDL_Skill_Ename.Enabled = true;
            DDL_Skillmod.Enabled = true;
            DDL_Submodule.Enabled = true;
            //txtmonth_cldr.Enabled = true;
            LK_update.Visible = false;
            LK_addtotmptbl.Visible = true;
            btnsavescore.Visible = GV_TmpoScore.Rows.Count > 0 ? true : false;
            txtmonth_cldr.Text = DateTime.Today.ToString("MM/yyyy");
            txtmonth_cldr.Enabled = false;

        }


        //-------------------------------------------- PAGE INDEX CHANGING ---------------------------------------------------
        protected void GV_TmpoScore_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DataTable dt = Session["GVBind"] as DataTable;
            GV_TmpoScore.DataSource = (DataTable)Session["GVBind"];
            GV_TmpoScore.PageIndex = e.NewPageIndex;
            GV_TmpoScore.DataBind();
        }

        //----------------------------------------------CREATE TEMPORARY TBL -----------------------------------------------------------

        public DataTable Tabledata()
        {
            // Here we create a DataTable with five columns.
            DataTable table = new DataTable();
            table.Columns.Add("LblID", typeof(int));


            //Set AutoIncrement True for the First Column.
            table.Columns["LblID"].AutoIncrement = true;
            //Set the Starting or Seed value.
            table.Columns["LblID"].AutoIncrementSeed = 1;
            //Set the Increment value.
            table.Columns["LblID"].AutoIncrementStep = 1;

            table.Columns.Add("DDL_Skill_Ename", typeof(string));
            table.Columns.Add("DDL_Skillmod", typeof(string));
            table.Columns.Add("DDL_Submodule", typeof(string));
            table.Columns.Add("txtmonth_cldr", typeof(string));
            table.Columns.Add("txt_Score", typeof(decimal));
            table.Columns.Add("txt_Score_commnt", typeof(string));


            table.Columns.Add("Ename_vfield", typeof(string));
            table.Columns.Add("Skillmod_vfield", typeof(int));
            table.Columns.Add("Submodule_vfield", typeof(int));


            return table;

        }


        //--------------------------------------- ADDING NEW DATA ROW TO GRID VIEW -------------------------------------------------------


        public void addnewrow()
        {
            try
            {
                // bool? IsDataPresent = false;
                bool? status = false;

                Skill_Matrix_BL objskillmodBL = new Skill_Matrix_BL();
                objskillmodBL.Check_saved_scores(DDL_Skill_Ename.SelectedValue.ToString(), Convert.ToInt32(DDL_Skillmod.SelectedValue), Convert.ToInt32(DDL_Submodule.SelectedValue), txtmonth_cldr.Text.Trim(), Convert.ToDecimal(txt_Score.Text), ref status);

                if (status == true)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Record already exists for current month');", true);
                    //lbl_errmssg.Text = "Record already exists for current date";
                }

                else if (IsDataPresent_toadd() == true)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Record already exists for current month');", true);

                }

                else
                {

                    txtmonth_cldr.Text = DateTime.Today.ToString("MM/yyyy");
                    txtmonth_cldr.Enabled = false;
                    using (DataTable Dt = Session["GVBind"] != null ? (DataTable)Session["GVBind"] : Tabledata())
                    {

                        Dt.Rows.Add(null, DDL_Skill_Ename.SelectedItem.ToString(), DDL_Skillmod.SelectedItem.ToString(), DDL_Submodule.SelectedItem.ToString().Trim(), 
                            txtmonth_cldr.Text, txt_Score.Text, txt_Score_commnt.Text, DDL_Skill_Ename.SelectedValue.ToString(), DDL_Skillmod.SelectedValue.ToString(), DDL_Submodule.SelectedValue.ToString().Trim());

                        Session["GVBind"] = Dt;

                    }
                    GV_TmpoScore.DataSource = (DataTable)Session["GVBind"];
                    GV_TmpoScore.DataBind();
                    GV_TmpoScore.Visible = true;
                    DDL_Submodule.SelectedValue = "0";
                    //txtmonth_cldr.Text = "";

                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + ex.Message + "');", true);
            }

        }


    //---------------------- Check if data is already present or not ------------------------
        public bool IsDataPresent_toadd()
        {
            if (Session["GVBind"] != null)
            {
                int count1 = 0;
                //bool? IsDataPresent = false;
                using (DataTable uptbl = (DataTable)Session["GVBind"])
                {
                    DataRow[] varrow = (from skill in uptbl.AsEnumerable().Cast<DataRow>()
                                        where skill.Field<string>("Ename_vfield") == (DDL_Skill_Ename.SelectedValue.ToString()) &&
                      skill.Field<int>("Skillmod_vfield") == Convert.ToInt32(DDL_Skillmod.SelectedValue.ToString()) && skill.Field<int>("Submodule_vfield") ==
                      Convert.ToInt32(DDL_Submodule.SelectedValue.ToString()) && skill.Field<string>("txtmonth_cldr") == txtmonth_cldr.Text
                                        select skill).ToArray();

                    foreach (DataRow skill in varrow)
                    {
                        count1 += 1;
                    }
                    if (count1 <= 0)
                    {

                        return false;

                    }
                    else
                        return true;
                }

            }
            else
                return false;


        }


        ////public bool IsDataPresent_rtmark()
        ////{
        ////    if (Session["GVBind"] != null)
        ////    {
        ////        int count1 = 0;
        ////        //bool? IsDataPresent = false;
        ////        using (DataTable uptbl = (DataTable)Session["GVBind"])
        ////        {
        ////            DataRow[] varrow = (from skill in uptbl.AsEnumerable().Cast<DataRow>()
        ////                                where skill.Field<decimal>("txt_Score") != Convert.ToDecimal(txt_Score.Text) || skill.Field<string>("txt_Score_commnt") != txt_Score_commnt.Text
        ////                                select skill).ToArray();

        ////            foreach (DataRow skill in varrow)
        ////            {
        ////                count1 += 1;
        ////            }
        ////            if (count1 <=0)
        ////            {

        ////                return false;

        ////            }
        ////            else
        ////                return true;
        ////        }

        ////    }
        ////    else
        ////        return false;


        ////}

//----------------------- Click link to add data to tempo tbl ----------------------
        protected void LK_addtotmptbl_Click(object sender, EventArgs e)
        {
           
            if (ValidateDuration() == true)
            {
                Lk_Clear.Text = "Clear";
                addnewrow();
                txtmonth_cldr.Text = DateTime.Today.ToString("MM/yyyy");
                txtmonth_cldr.Enabled = false;
                btnsavescore.Visible = GV_TmpoScore.Rows.Count > 0 ? true : false;
                //btnsavescore.Visible = true;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Records can be added upto current month');", true);
                //txtmonth_cldr.Text = "";
                Lk_Clear.Text = "Cancel";
                btnsavescore.Visible = GV_TmpoScore.Rows.Count > 0 ? true : false;
            }

        }


        //---------------------------------------------- GV ROW COMMAND BIND CONTROLS ----------------------------------------------------------

        protected void GV_TmpoScore_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName.ToUpper())
                {
                    case "BINDEDITDATA":
                        Lk_Clear.Text = "Cancel";
                        Session["LblID"] = GV_TmpoScore.DataKeys[int.Parse(e.CommandArgument.ToString())]["LblID"].ToString().Trim();
                        DDL_Skill_Ename.SelectedValue = GV_TmpoScore.DataKeys[int.Parse(e.CommandArgument.ToString())]["Ename_vfield"].ToString().Trim();
                        DDL_Skillmod.SelectedValue = GV_TmpoScore.DataKeys[int.Parse(e.CommandArgument.ToString())]["Skillmod_vfield"].ToString().Trim();
                        DDL_submoduleLst();
                        DDL_Submodule.SelectedValue = GV_TmpoScore.DataKeys[int.Parse(e.CommandArgument.ToString())]["Submodule_vfield"].ToString().Trim();
                        txtmonth_cldr.Text = GV_TmpoScore.DataKeys[int.Parse(e.CommandArgument.ToString())]["txtmonth_cldr"].ToString().Trim();
                       
                        txt_Score.Text = GV_TmpoScore.DataKeys[int.Parse(e.CommandArgument.ToString())]["txt_Score"].ToString().Trim();
                        txt_Score_commnt.Text = GV_TmpoScore.DataKeys[int.Parse(e.CommandArgument.ToString())]["txt_Score_commnt"].ToString().Trim();

                        LK_addtotmptbl.Visible = false;

                        DDL_Skill_Ename.Enabled = false;
                        DDL_Skillmod.Enabled = false;
                        DDL_Submodule.Enabled = false;
                        txtmonth_cldr.Enabled = false;


                        LK_update.Visible = true;
                        btnsavescore.Visible = false;
                        break;

                    case "DELETEDATA":


                        Lk_Clear.Text = "Clear";
                        Session["LblID"] = GV_TmpoScore.DataKeys[int.Parse(e.CommandArgument.ToString())]["LblID"].ToString().Trim();
                        using (DataTable uptbl = (DataTable)Session["GVBind"])
                        {
                            DataRow[] varrow = (from skill in uptbl.AsEnumerable().Cast<DataRow>() where skill.Field<int>("LblID") == Convert.ToInt32(Session["LblID"]) select skill).ToArray();
                            foreach (DataRow skill in varrow)
                            {
                                skill.Table.Rows.Remove(skill);
                            }

                            Session["GVBind"] = uptbl;
                            GV_TmpoScore.DataSource = (DataTable)Session["GVBind"];
                            GV_TmpoScore.DataBind();
                            btnsavescore.Visible = GV_TmpoScore.Rows.Count > 0 ? true : false;
                            LK_update.Visible = false;
                            LK_addtotmptbl.Visible = true;


                            DDL_Skill_Ename.Enabled = true;
                            DDL_Skillmod.Enabled = true;
                            DDL_Submodule.Enabled = true;
                            txtmonth_cldr.Enabled = false;
                            DDL_sessionclear();

                        }
                        break;


                }

            }

            catch (Exception ex)
            {
                { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + ex.Message + "');", true); }
            }

        }


        //------------------------------------------------------- UPDATE GRID DATA ---------------------------------------------------------------
        protected void LK_update_Click(object sender, EventArgs e)
        {
           
            if (ValidateDuration() == true)
            {
                if (IsDataPresent() == true)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Record already exists');", true);
                    
                    LK_update.Visible = true;
                    Lk_Clear.Text = "Cancel";
                    btnsavescore.Visible = false;

                    txtmonth_cldr.Text = DateTime.Today.ToString("MM/yyyy");
                    txtmonth_cldr.Enabled = false;
                }

                //else if (IsDataPresent_toadd() == true)
                //{
                //    if (IsDataPresent_rtmark() == true)
                //    {
                //        using (DataTable uptbl = (DataTable)Session["GVBind"])
                //        {
                //            DataRow[] varrow = (from skill in uptbl.AsEnumerable().Cast<DataRow>() where skill.Field<int>("LblID") == Convert.ToInt32(Session["LblID"]) select skill).ToArray();
                //            foreach (DataRow skill in varrow)
                //            {
                //                skill["DDL_Skill_Ename"] = DDL_Skill_Ename.SelectedItem.ToString();
                //                skill["DDL_Skillmod"] = DDL_Skillmod.SelectedItem.ToString();
                //                skill["DDL_Submodule"] = DDL_Submodule.SelectedItem.ToString();
                //                skill["txt_Score_commnt"] = txt_Score_commnt.Text.ToString();

                //                skill["txtmonth_cldr"] = txtmonth_cldr.Text.ToString();
                //                skill["Ename_vfield"] = DDL_Skill_Ename.SelectedValue.ToString();
                //                skill["Skillmod_vfield"] = DDL_Skillmod.SelectedValue.ToString();
                //                skill["Submodule_vfield"] = DDL_Submodule.SelectedValue.ToString();
                //                skill["txt_Score"] = txt_Score.Text.ToString();

                //            }
                //            Session["GVBind"] = uptbl;
                //            GV_TmpoScore.DataSource = (DataTable)Session["GVBind"];
                //            GV_TmpoScore.DataBind();
                //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Score details have been updated successfully');", true);
                //            LK_addtotmptbl.Visible = true;
                //            btnsavescore.Visible = true;
                            
                //            LK_update.Visible = false;
                //            DDL_sessionclear();
                //        }
                //    }

                //    else
                //    {


                //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Record already exists for current month');", true);
                //        LK_update.Visible = true;
                //        btnsavescore.Visible = false;
                //    }
                //}

                else
                {
                    //DataTable d = Tabledata();
                    using (DataTable uptbl = (DataTable)Session["GVBind"])
                    {
                        
                        DataRow[] varrow = (from skill in uptbl.AsEnumerable().Cast<DataRow>() where skill.Field<int>("LblID") == Convert.ToInt32(Session["LblID"]) select skill).ToArray();
                        foreach (DataRow skill in varrow)
                        {
                            skill["DDL_Skill_Ename"] = DDL_Skill_Ename.SelectedItem.ToString();
                            skill["DDL_Skillmod"] = DDL_Skillmod.SelectedItem.ToString();
                            skill["DDL_Submodule"] = DDL_Submodule.SelectedItem.ToString();
                            skill["txt_Score_commnt"] = txt_Score_commnt.Text.ToString();

                            skill["txtmonth_cldr"] = txtmonth_cldr.Text.ToString();
                            skill["Ename_vfield"] = DDL_Skill_Ename.SelectedValue.ToString();
                            skill["Skillmod_vfield"] = DDL_Skillmod.SelectedValue.ToString();
                            skill["Submodule_vfield"] = DDL_Submodule.SelectedValue.ToString();
                            skill["txt_Score"] = txt_Score.Text.ToString();

                        }
                        Lk_Clear.Text = "Clear";
                        Session["GVBind"]  = uptbl;
                        GV_TmpoScore.DataSource = (DataTable)Session["GVBind"];
                        GV_TmpoScore.DataBind();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Score details have been updated successfully');", true);
                        LK_addtotmptbl.Visible = true;
                        btnsavescore.Visible = true;
                        // btn_Submit.Visible = true;
                        LK_update.Visible = false;


                        DDL_Skill_Ename.Enabled = true;
                        DDL_Skillmod.Enabled = true;
                        DDL_Submodule.Enabled = true;
                        //txtmonth_cldr.Enabled = true;
                        DDL_sessionclear();
                        txtmonth_cldr.Text = DateTime.Today.ToString("MM/yyyy");
                        txtmonth_cldr.Enabled = false;

                    }
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Records can be update upto current month');", true);
                //txtmonth_cldr.Text = "";
            }

        }



        //---------------- Check any of data is changed to update or not ---------------

        public bool IsDataPresent()
        {
            if (Session["GVBind"] != null)
            {
                int count1 = 0;
                //bool? IsDataPresent = false;
                using (DataTable uptbl = (DataTable)Session["GVBind"])
                {
                    DataRow[] varrow = (from skill in uptbl.AsEnumerable().Cast<DataRow>()
                                    where  skill.Field<decimal>("txt_Score") == Convert.ToDecimal(txt_Score.Text) && skill.Field<string>("txt_Score_commnt") == txt_Score_commnt.Text
                                        select skill).ToArray();
                      //skill.Field<int>("Skillmod_vfield") == Convert.ToInt32(DDL_Skillmod.SelectedValue.ToString()) && skill.Field<int>("Submodule_vfield") ==
                      //Convert.ToInt32(DDL_Submodule.SelectedValue.ToString())  && 
                     

                    foreach (DataRow skill in varrow)
                    {
                        count1 += 1;
                    }
                    if (count1 <= 0)
                    {

                        return false;

                    }
                    else
                        return true;
                }

            }
            else
                return false;


        }
        //            foreach (DataRow skill in varrow)
        //            {
        //                count1 += 1;
        //            }
        //            if (count1 >= 0)
        //            {

        //                count1 = 0;
        //                DataRow[] varrow1 = (from skill in uptbl.AsEnumerable().Cast<DataRow>()
        //                                     where (skill.Field<decimal>("txt_Score") != Convert.ToDecimal(txt_Score.Text))
        //                                     select skill).ToArray();

        //                foreach (DataRow skill in varrow1)
        //                {
        //                    count1 += 1;
        //                }

        //                if (count1 >= 1)
        //                    return false;
        //                else
        //                {
        //                    count1 = 0;
        //                    DataRow[] varrow2 = (from skill in uptbl.AsEnumerable().Cast<DataRow>()
        //                                         where (skill.Field<string>("txt_Score_commnt") != txt_Score_commnt.Text)
        //                                         select skill).ToArray();

        //                    foreach (DataRow skill in varrow2)
        //                    {
        //                        count1 += 1;
        //                    }
        //                    if (count1 >= 1)
        //                        return false;
        //                    else
        //                        return true;
        //                }
        //            }
        //            else
        //                return false;
        //        }

        //         }
        //    else
        //        return false;

        //}

    

                   
        //----------------------------------------------------------------- MONTH VALIDATION ---------------------------------------------------------
        protected bool ValidateDuration()
        {
            string month = txtmonth_cldr.Text;
            string firstTwo = month.Substring(0, 2);
            if (txtmonth_cldr.Text.Substring(txtmonth_cldr.Text.Length - 4) == DateTime.Now.ToString("yyyy"))
            {
                if (Convert.ToInt32(firstTwo) > Convert.ToInt32(DateTime.Now.ToString("MM")))
                {

                    return false;
                }
                else
                    return true;

            }
            else if (Convert.ToInt32(txtmonth_cldr.Text.Substring(txtmonth_cldr.Text.Length - 4)) < (Convert.ToInt32(DateTime.Now.ToString("yyyy"))))
            {
                //if (Convert.ToInt32(firstTwo) > Convert.ToInt32(DateTime.Now.ToString("MM")))
                if (Convert.ToInt32(DateTime.Now.ToString("MM")) == 1 && (Convert.ToInt32(firstTwo)) == 12)

                    return true;

                else return false;
            }
            else return false;

        }


        //---------------------------------------------  SAVE TO DB TBL-------------------------------------------------------
        protected void btnsavescore_Click(object sender, EventArgs e)
        {

            try
            {
                Lk_Clear.Text = "Clear";
                Skill_Matrix_BO objskillbo = new Skill_Matrix_BO();
                Skill_Matrix_BL objskillmodBL = new Skill_Matrix_BL();
                objskillbo.LOGPERNR = User.Identity.Name.Trim();
                bool? status = false;

                if (Session["GVBind"] != null)
                {

                    using (DataTable Dt = (DataTable)Session["GVBind"])
                    {

                        for (int i = 0; i < Dt.Rows.Count; i++)
                        {

                            objskillbo.EmpIDPA0001 = Dt.Rows[i]["Ename_vfield"].ToString();

                            objskillbo.MID = int.Parse(Dt.Rows[i]["Skillmod_vfield"].ToString());

                            objskillbo.SID = int.Parse(Dt.Rows[i]["Submodule_vfield"].ToString());

                            objskillbo.For_month = Dt.Rows[i]["txtmonth_cldr"].ToString();

                            objskillbo.marks = decimal.Parse(Dt.Rows[i]["txt_Score"].ToString());

                            objskillbo.comments = Dt.Rows[i]["txt_Score_commnt"].ToString();

                            objskillbo.Modified_On = Convert.ToDateTime("1900-01-01");

                            objskillmodBL.Save_Score_Records(objskillbo, ref  status);


                        }

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Score details have been submitted successfully');", true);
                       
                        GV_TmpoScore.DataSource = null;
                        GV_TmpoScore.DataBind();

                        GV_TmpoScore.Visible = false;
                        btnsavescore.Visible = false;
                     
                        LK_addtotmptbl.Visible = true;
                        LK_update.Visible = false;
                        DDL_sessionclear();
                        Session["GVBind"] = null;
                        txtmonth_cldr.Text = DateTime.Today.ToString("MM/yyyy");
                        txtmonth_cldr.Enabled = false;

                    }
                }
            }


            catch (Exception ex)
            {
                { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + ex.Message + "');", true); }
            }
        }





        //----------------------------------------------------------- VIEW DDL EMP  ------------------------------------------------
        public void DDL_Emplst_View()
        {
            bool? status = false;
             //string sts1 = "";
            Skill_Matrix_Collectionbo objskilllst = new Skill_Matrix_Collectionbo();
            Skill_Matrix_BL objskillmodBL = new Skill_Matrix_BL();
            objskillmodBL.checkHR_mngt(User.Identity.Name, ref status);
            //objskilllst = objskillmodBL.checkHR_mngt(User.Identity.Name, ref status);
            //AnnouncementdalDataContext objAnnouncementDataContext = new AnnouncementdalDataContext();
            //objAnnouncementDataContext.usp_CheckHR(HttpContext.Current.User.Identity.Name, ref sts);
            if (status == true)
            {
                lst1.Enabled = true;
                //Skill_Matrix_Collectionbo objskilllst = new Skill_Matrix_Collectionbo();
                //Skill_Matrix_BL objskillmodBL = new Skill_Matrix_BL();
                objskilllst = objskillmodBL.Set_subordinate_DDL(User.Identity.Name, 3);
                ddl_srch_Enam.DataSource = null;
                ddl_srch_Enam.DataBind();

                ddl_srch_Enam.DataSource = objskilllst;
                ddl_srch_Enam.DataValueField = "EmpIDPA0001";
                ddl_srch_Enam.DataTextField = "EmpNamePA0001";
                ddl_srch_Enam.DataBind();
            }
            else
            {
                lst1.Enabled = false;
                //Skill_Matrix_Collectionbo objskilllst = new Skill_Matrix_Collectionbo();
                //Skill_Matrix_BL objskillmodBL = new Skill_Matrix_BL();
                objskilllst = objskillmodBL.Set_subordinate_DDL(User.Identity.Name, 2);
                ddl_srch_Enam.DataSource = null;
                ddl_srch_Enam.DataBind();

                ddl_srch_Enam.DataSource = objskilllst;
                ddl_srch_Enam.DataValueField = "EmpIDPA0001";
                ddl_srch_Enam.DataTextField = "EmpNamePA0001";
                ddl_srch_Enam.DataBind();
            }
            
            //ddl_srch_Enam.Items.Insert(0, new System.Web.UI.WebControls.ListItem(" - SELECT - ", "0"));
        }


        //----------------------------------------------------------- VIEW  DDL MODULE ------------------------------------------------
        public void DDL_modulelst_View()
        {
            Skill_Matrix_Collectionbo objskilllst = new Skill_Matrix_Collectionbo();
            Skill_Matrix_BL objskillmodBL = new Skill_Matrix_BL();
            objskilllst = objskillmodBL.Set_Skill_Details();
            ddl_srch_Modl.DataSource = null;
            ddl_srch_Modl.DataBind();

            ddl_srch_Modl.DataSource = objskilllst;
            ddl_srch_Modl.DataTextField = "MODULE";
            ddl_srch_Modl.DataValueField = "MID";
            ddl_srch_Modl.DataBind();
            //ddl_srch_Modl.Items.Insert(0, new System.Web.UI.WebControls.ListItem(" - SELECT - ", "0"));
        }

        //----------------------------------------------------------- GV PAGE INDEX ------------------------------------------------
        protected void gv_Viewscores_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            string prnr = ddl_srch_Enam.SelectedValue;
            int mod = Convert.ToInt32(ddl_srch_Modl.SelectedValue);
            string month = txt_Vw_month.Text.Trim();
            Skill_Matrix_Collectionbo objskilllst = new Skill_Matrix_Collectionbo();
            Skill_Matrix_BL objskillmodBL = new Skill_Matrix_BL();
            objskilllst = objskillmodBL.view_saved_scores(User.Identity.Name.Trim(), prnr, mod, month, Convert.ToInt32(ddl_Srchby.SelectedValue));

            gv_Viewscores.DataSource = Session["sortdata"];
            gv_Viewscores.PageIndex = e.NewPageIndex;

            gv_Viewscores.DataBind();
            //Session.Add("sortdata");
           // Session["sortdata"] = objskilllst;
        }


        //----------------------------------------------------------- VIEW RECORD DETAILS ------------------------------------------------
        public void lnk_view_mthod()
        {
            Mult_Add_Scores.SetActiveView(View_Score_Details);

            Skill_Matrix_Collectionbo objskilllst = new Skill_Matrix_Collectionbo();
            Skill_Matrix_BL objskillmodBL = new Skill_Matrix_BL();

            string prnr = ddl_srch_Enam.SelectedValue;
            int mod = Convert.ToInt32(ddl_srch_Modl.SelectedValue);
            string month = txt_Vw_month.Text.Trim() == null ? "" : txt_Vw_month.Text.Trim();


            ddl_Srchby.SelectedIndex = -1;
            //ddl_srch_Enam.SelectedIndex = 1;
            ddl_srch_Modl.SelectedIndex = -1;
            ddl_srch_Enam.Visible = true;
           // ddl_srch_Enam.SelectedValue = -1;
            ddl_srch_Enam.SelectedValue = User.Identity.Name.Trim();
            string prnr1 = ddl_srch_Enam.SelectedValue;
            ddl_srch_Modl.Visible = false;
            txt_Vw_month.Visible = false;

            objskilllst = objskillmodBL.view_saved_scores(User.Identity.Name.Trim(), prnr1, 1, "", Convert.ToInt32(1));

            
            gv_Viewscores.DataSource = objskilllst;
            gv_Viewscores.DataBind();
            Session["sortdata"] = objskilllst;
          
            btn_VExport.Visible = objskilllst.Count > 0 ? true : false;
            btn_VEXl.Visible = objskilllst.Count > 0 ? true : false;
        }

//--------------------------------------------------- SORT VIEW SCORES GRID ----------------------------------------------------------------
        protected void gv_Viewscores_Sorting(object sender, GridViewSortEventArgs e)
        {
            try
            {

                List<Skill_Matrix_BO> objPIDashBoardCmpltdLst = (List<Skill_Matrix_BO>)Session["sortdata"];

                bool objSortOrder = (bool)Session["bSortedOrder"];
                switch (e.SortExpression.ToString().Trim())
                {
                    case "sort_ename":
                        if (objSortOrder)
                        {
                            if (objPIDashBoardCmpltdLst != null)
                            {
                                objPIDashBoardCmpltdLst.Sort(delegate(Skill_Matrix_BO objBo1, Skill_Matrix_BO objBo2)
                                { return (objBo1.ENAME.CompareTo(objBo2.ENAME)); });
                                objSortOrder = false;
                                Session.Add("bSortedOrder", objSortOrder);
                            }
                        }
                        else
                        {
                            objPIDashBoardCmpltdLst.Sort(delegate(Skill_Matrix_BO objBo1, Skill_Matrix_BO objBo2)
                            { return (objBo2.ENAME.CompareTo(objBo1.ENAME)); });
                            objSortOrder = true;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                        break;

                    case "sort_mname":
                        if (objSortOrder)
                        {
                            if (objPIDashBoardCmpltdLst != null)
                            {
                                objPIDashBoardCmpltdLst.Sort(delegate(Skill_Matrix_BO objBo1, Skill_Matrix_BO objBo2)
                                { return (objBo1.MOD_name.CompareTo(objBo2.MOD_name)); });
                                objSortOrder = false;
                                Session.Add("bSortedOrder", objSortOrder);
                            }
                        }
                        else
                        {
                            objPIDashBoardCmpltdLst.Sort(delegate(Skill_Matrix_BO objBo1, Skill_Matrix_BO objBo2)
                            { return (objBo2.MOD_name.CompareTo(objBo1.MOD_name)); });
                            objSortOrder = true;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                        break;

                    case "sort_sname":
                        if (objSortOrder)
                        {
                            if (objPIDashBoardCmpltdLst != null)
                            {
                                objPIDashBoardCmpltdLst.Sort(delegate(Skill_Matrix_BO objBo1, Skill_Matrix_BO objBo2)
                                { return (objBo1.SUB__MOD_NAME.CompareTo(objBo2.SUB__MOD_NAME)); });
                                objSortOrder = false;
                                Session.Add("bSortedOrder", objSortOrder);
                            }
                        }
                        else
                        {
                            objPIDashBoardCmpltdLst.Sort(delegate(Skill_Matrix_BO objBo1, Skill_Matrix_BO objBo2)
                            { return (objBo2.SUB__MOD_NAME.CompareTo(objBo1.SUB__MOD_NAME)); });
                            objSortOrder = true;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                        break;

                    case "sort_scores":
                        if (objSortOrder)
                        {
                            if (objPIDashBoardCmpltdLst != null)
                            {
                                objPIDashBoardCmpltdLst.Sort(delegate(Skill_Matrix_BO objBo1, Skill_Matrix_BO objBo2)
                                { return (objBo1.SCORE.CompareTo(objBo2.SCORE)); });
                                objSortOrder = false;
                                Session.Add("bSortedOrder", objSortOrder);
                            }
                        }
                        else
                        {
                            objPIDashBoardCmpltdLst.Sort(delegate(Skill_Matrix_BO objBo1, Skill_Matrix_BO objBo2)
                            { return (objBo2.SCORE.CompareTo(objBo1.SCORE)); });
                            objSortOrder = true;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                        break;

                    case "sort_month":
                        if (objSortOrder)
                        {
                            if (objPIDashBoardCmpltdLst != null)
                            {
                                objPIDashBoardCmpltdLst.Sort(delegate(Skill_Matrix_BO objBo1, Skill_Matrix_BO objBo2)
                                { return (objBo1.For_month.CompareTo(objBo2.For_month)); });
                                objSortOrder = false;
                                Session.Add("bSortedOrder", objSortOrder);
                            }
                        }
                        else
                        {
                            objPIDashBoardCmpltdLst.Sort(delegate(Skill_Matrix_BO objBo1, Skill_Matrix_BO objBo2)
                            { return (objBo2.For_month.CompareTo(objBo1.For_month)); });
                            objSortOrder = true;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                        break;

                    case "sort_createdby":
                        if (objSortOrder)
                        {
                            if (objPIDashBoardCmpltdLst != null)
                            {
                                objPIDashBoardCmpltdLst.Sort(delegate(Skill_Matrix_BO objBo1, Skill_Matrix_BO objBo2)
                                { return (objBo1.Created_By.CompareTo(objBo2.Created_By)); });
                                objSortOrder = false;
                                Session.Add("bSortedOrder", objSortOrder);
                            }
                        }
                        else
                        {
                            objPIDashBoardCmpltdLst.Sort(delegate(Skill_Matrix_BO objBo1, Skill_Matrix_BO objBo2)
                            { return (objBo2.Created_By.CompareTo(objBo1.Created_By)); });
                            objSortOrder = true;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                        break;

                    case "sort_ondate":
                        if (objSortOrder)
                        {
                            if (objPIDashBoardCmpltdLst != null)
                            {
                                objPIDashBoardCmpltdLst.Sort(delegate(Skill_Matrix_BO objBo1, Skill_Matrix_BO objBo2)
                                { return (objBo1.Created_On.CompareTo(objBo2.Created_On)); });
                                objSortOrder = false;
                                Session.Add("bSortedOrder", objSortOrder);
                            }
                        }
                        else
                        {
                            objPIDashBoardCmpltdLst.Sort(delegate(Skill_Matrix_BO objBo1, Skill_Matrix_BO objBo2)
                            { return (objBo2.Created_On.CompareTo(objBo1.Created_On)); });
                            objSortOrder = true;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                        break;
                }


                gv_Viewscores.DataSource = objPIDashBoardCmpltdLst;
                gv_Viewscores.DataBind();
                Session.Add("sortdata", objPIDashBoardCmpltdLst);
            }

            catch (Exception ex)
            {
                { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + ex.Message + "');", true); }
            }
        }
        


        protected void lnk_view_Click(object sender, EventArgs e)
        {

            lnk_view_mthod();

            //GetHRPERNRS();

        }


        protected void Lnk_Back_Click(object sender, EventArgs e)
        {
            Mult_Add_Scores.SetActiveView(View_Add_Scores);
            btn_VExport.Visible = false;


        }

        //----------------------------------------------------------- SEARCH --------------------------------------------------------------
        protected void btn_Search_toview_Click(object sender, EventArgs e)
        {
            //if (Srch_Month() == true)
            //{
            Skill_Matrix_Collectionbo objskilllst = new Skill_Matrix_Collectionbo();
            Skill_Matrix_BL objskillmodBL = new Skill_Matrix_BL();

            string prnr = ddl_srch_Enam.SelectedValue;
            int mod = Convert.ToInt32(ddl_srch_Modl.SelectedValue);
            string month = txt_Vw_month.Text.Trim();

            objskilllst = objskillmodBL.view_saved_scores(User.Identity.Name.Trim(), prnr, mod, month, Convert.ToInt32(ddl_Srchby.SelectedValue));

            gv_Viewscores.DataSource = objskilllst;
            gv_Viewscores.DataBind();
            Session["sortdata"] = objskilllst;
            //btn_VExport.Visible = true;
            btn_VExport.Visible = objskilllst.Count > 0 ? true : false;
            btn_VEXl.Visible = objskilllst.Count > 0 ? true : false;
            //}
            //else
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Records can be view upto current month');", true);
            //}


        }


        //----------------------------------------------------------- DDL INDEX CHANGE --------------------------------------------------------------
        protected void ddl_Srchby_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (ddl_Srchby.SelectedIndex == 0)
            //{
            //    ddl_srch_Enam.Visible = false;
            //    ddl_srch_Modl.Visible = false;
            //    txt_Vw_month.Visible = false;
            //}
            //else 
            if (ddl_Srchby.SelectedValue == "1")
            {
                ddl_srch_Enam.Visible = true;
                ddl_srch_Modl.Visible = false;
                txt_Vw_month.Visible = false;

            }

            else if (ddl_Srchby.SelectedValue == "2")
            {
                ddl_srch_Modl.Visible = true;
                ddl_srch_Enam.Visible = false;
                txt_Vw_month.Visible = false;
            }
            else if (ddl_Srchby.SelectedValue == "3")
            {
                ddl_srch_Enam.Visible = false;
                ddl_srch_Modl.Visible = false;
                txt_Vw_month.Visible = false;
            }
            else if (ddl_Srchby.SelectedValue == "4")
            {
                ddl_srch_Enam.Visible = false;
                ddl_srch_Modl.Visible = false;
                txt_Vw_month.Text = "";
                txt_Vw_month.Visible = true;

            }

            else if (ddl_Srchby.SelectedValue == "5")
            {
                ddl_srch_Enam.Visible = false;
                ddl_srch_Modl.Visible = false;
                txt_Vw_month.Visible = false;

            }
            Mult_Add_Scores.SetActiveView(View_Score_Details);

        }


        protected void GetHRPERNRS()
        {
            string sts = "";
            AnnouncementdalDataContext objAnnouncementDataContext = new AnnouncementdalDataContext();
            objAnnouncementDataContext.usp_CheckHR(HttpContext.Current.User.Identity.Name, ref sts);
            if (sts.Trim().ToUpper() == "TRUE")
            {
                lst1.Enabled = true;
            }
            else
            {
                lst1.Enabled = false;
            }

        }


        //----------------------------------------------------------- SEARCH  CLEAR------------------------------------------------
        protected void btn_Clr_Srch_Click(object sender, EventArgs e)
        {
            Mult_Add_Scores.SetActiveView(View_Score_Details);
            lnk_view_mthod();
            //Skill_Matrix_Collectionbo objskilllst = new Skill_Matrix_Collectionbo();
            //Skill_Matrix_BL objskillmodBL = new Skill_Matrix_BL();

            //string prnr = ddl_srch_Enam.SelectedValue;
            //int mod = Convert.ToInt32(ddl_srch_Modl.SelectedValue);
            //string month = txt_Vw_month.Text.Trim();

            //objskilllst = objskillmodBL.view_saved_scores(User.Identity.Name.Trim(), "", 0, "", Convert.ToInt32(0));

           
            //ddl_srch_Enam.SelectedIndex = -1;
            //ddl_srch_Modl.SelectedIndex = -1;
            //txt_Vw_month.Text = "";
            //ddl_srch_Enam.Visible = false;
            //ddl_srch_Modl.Visible = false;
            //txt_Vw_month.Visible = false;
            //gv_Viewscores.DataSource = objskilllst;
            //gv_Viewscores.DataBind();
          
            //btn_VExport.Visible = objskilllst.Count > 0 ? true : false;
            //btn_VEXl.Visible = objskilllst.Count > 0 ? true : false;
        }


        //----------------------------------------------------- GV EXPORT TO PDF----------------------------------------------------------------------
        protected void btn_VExport_Click(object sender, EventArgs e)
        {
            try
            {

                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment;filename=Score_Records.pdf");


                gv_Viewscores.AllowPaging = false;
                string prnr = ddl_srch_Enam.SelectedValue;
                int mod = Convert.ToInt32(ddl_srch_Modl.SelectedValue);
                string month = txt_Vw_month.Text.Trim();
                Skill_Matrix_Collectionbo objskilllst = new Skill_Matrix_Collectionbo();
                Skill_Matrix_BL objskillmodBL = new Skill_Matrix_BL();
                objskilllst = objskillmodBL.view_saved_scores(User.Identity.Name.Trim(), prnr, mod, month, Convert.ToInt32(ddl_Srchby.SelectedValue));

                gv_Viewscores.DataSource = objskilllst;
                gv_Viewscores.DataBind();
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                StringWriter sw = new StringWriter();
                HtmlTextWriter hw = new HtmlTextWriter(sw);
                gv_Viewscores.RenderControl(hw);
                StringReader sr = new StringReader(sw.ToString());
                gv_Viewscores.HeaderRow.Style.Add("width", "12%");
                gv_Viewscores.HeaderRow.Style.Add("font-size", "9px");
               
                gv_Viewscores.Style.Add("text-decoration", "none");
                gv_Viewscores.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                gv_Viewscores.Style.Add("font-size", "8px");
                Document pdfDoc = new Document(PageSize.A4, 9f, 9f, 9f, 0f);
                HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
                PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                pdfDoc.Open();
                htmlparser.Parse(sr);
                pdfDoc.Close();
                Response.Write(pdfDoc);
                gv_Viewscores.AllowPaging = true;

                Response.End();
            }
            catch (Exception ex)
            {

            }
        }


        public override void VerifyRenderingInServerForm(System.Web.UI.Control control)
        {

        }

        //----------------------------------------------------- GV EXPORT TO EXCEL----------------------------------------------------------------------

        protected void btn_VEXl_Click(object sender, EventArgs e)
        {
            try
            {

                string date1 = DateTime.Now.ToString("ddMM_yyyy_hh_mm_ss");
                System.IO.StringWriter sw = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);


                gv_Viewscores.AllowPaging = false;
                Skill_Matrix_Collectionbo objskilllst = new Skill_Matrix_Collectionbo();
                Skill_Matrix_BL objskillmodBL = new Skill_Matrix_BL();

                string prnr = ddl_srch_Enam.SelectedValue;
                int mod = Convert.ToInt32(ddl_srch_Modl.SelectedValue);
                string month = txt_Vw_month.Text.Trim();

                objskilllst = objskillmodBL.view_saved_scores(User.Identity.Name.Trim(), prnr, mod, month, Convert.ToInt32(ddl_Srchby.SelectedValue));

                gv_Viewscores.DataSource = objskilllst;
                gv_Viewscores.DataBind();
                btn_VExport.Visible = true;

                gv_Viewscores.RenderControl(htw);

                gv_Viewscores.AllowPaging = true;
                htw.WriteBreak();

                string renderedGridView = "<h4>Employee Score Details Report</h4>" + "<br>"; //+ sw.ToString();
                renderedGridView += sw.ToString() + "<br/>";
                Response.AppendHeader("content-disposition", "attachment; filename=" + " Employee_Score_Details_" + date1 + ".xls");
                Response.ContentType = "Application/vnd.ms-excel";
                Response.Write(renderedGridView);
                Response.End();

            }
            catch (Exception ec)
            {
            }
        }
         protected void GV_TmpoScore_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
         }

        protected void ddl_srch_Modl_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        protected void lnk_add_modnsub_Click(object sender, EventArgs e)
        {
            Mult_Add_Scores.SetActiveView(VW_addmodnsub);
            //update_pnl_mod.Visible = true;
            div_viewall.Visible = false;
            divmodl.Visible =true ;
            //div_submod.Visible = false;
            DDL_modl1();
            DDL_modladd();
           
        }

        //protected void Lnk_goto_submodl_Click(object sender, EventArgs e)
        //{
        //    Mult_Add_Scores.SetActiveView(VW_addmodnsub);
        //    //div_submod.Visible = true;
        //    divmodl.Visible = false;
        //    DDL_modladd();
        //}

        protected void LK_backtomodlpg_Click(object sender, EventArgs e)
        {
            Mult_Add_Scores.SetActiveView(View_Add_Scores);
           
            DDL_modl1();
        }

        protected void btn_cancelsub_Click(object sender, EventArgs e)
        {
            DDL_newmodls.SelectedIndex=-1;
            txt_addsunmdl.Text = "";
        }

        protected void btn_Cancel_Click(object sender, EventArgs e)
        {
            txt_newsubmodule.Text = "";
            DDL_loadmodules.SelectedIndex = -1;
        }


        protected void LK_exit_Click(object sender, EventArgs e)
        {
            Mult_Add_Scores.SetActiveView(View_Add_Scores);
        }

        protected void Lnk_Backtomainpg_Click(object sender, EventArgs e)
        {
            Mult_Add_Scores.SetActiveView(View_Add_Scores);
        }

        public void DDL_modl1()
        {
            Skill_Matrix_Collectionbo objskilllst = new Skill_Matrix_Collectionbo();
            Skill_Matrix_BL objskillmodBL = new Skill_Matrix_BL();
            objskilllst = objskillmodBL.Set_Skill_Details();
            DDL_loadmodules.DataSource = null;
            DDL_loadmodules.DataBind();

            DDL_loadmodules.DataSource = objskilllst;
            DDL_loadmodules.DataTextField = "MODULE";
            DDL_loadmodules.DataValueField = "MID";
            DDL_loadmodules.DataBind();
            DDL_loadmodules.Items.Insert(0, new System.Web.UI.WebControls.ListItem(" - SELECT - ", "0"));
        }

        public void DDL_modladd()
        {
            Skill_Matrix_Collectionbo objskilllst = new Skill_Matrix_Collectionbo();
            Skill_Matrix_BL objskillmodBL = new Skill_Matrix_BL();
            objskilllst = objskillmodBL.Set_Skill_Details();
            DDL_newmodls.DataSource = null;
            DDL_newmodls.DataBind();

            DDL_newmodls.DataSource = objskilllst;
            DDL_newmodls.DataTextField = "MODULE";
            DDL_newmodls.DataValueField = "MID";
            DDL_newmodls.DataBind();
            DDL_newmodls.Items.Insert(0, new System.Web.UI.WebControls.ListItem(" - VIEW - ", "0"));
        }

        protected void btn_save_Click(object sender, EventArgs e)
        {
            try
            {
                // bool? IsDataPresent = false;
                bool? status = false;
                Skill_Matrix_BO objskillbo = new Skill_Matrix_BO(); 
                Skill_Matrix_BL objskillmodBL = new Skill_Matrix_BL();
                objskillmodBL.Check_exist_submodules(DDL_loadmodules.SelectedItem.ToString(), txt_newsubmodule.Text.Trim(), ref status);

                if (status == true)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Sub module already there for respective module in database');", true);

                }

                else
                {
                                      
                    objskillbo.Created_By = User.Identity.Name.Trim();

                    objskillbo.MID =Convert.ToInt32(DDL_loadmodules.SelectedValue);

                    objskillbo.SUB__MOD_NAME = txt_newsubmodule.Text;
                    objskillmodBL.add_submodules(objskillbo);

                    txt_newsubmodule.Text = "";
                    DDL_loadmodules.SelectedIndex = -1;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Sub module has been added successfully');", true);

                }
                
            }

            catch (Exception ex)
            {
                { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + ex.Message + "');", true); }
            }
        }

        protected void btn_addsubmodl_Click(object sender, EventArgs e)
        {
            try
            {
                int?   result=0;
                bool? status = false;
                Skill_Matrix_BO objskillbo = new Skill_Matrix_BO();
                
                Skill_Matrix_BL objskillmodBL = new Skill_Matrix_BL();
                objskillmodBL.Check_exist_modules(txt_addsunmdl.Text,User.Identity.Name.Trim(), ref status);

                if (status == true)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Module already there in database');", true);
                    DDL_modladd();
                }

                else
                {
                    objskillbo.Created_By = User.Identity.Name.Trim();

                    objskillbo.MODULE = txt_addsunmdl.Text;
                    //Session["modl"] = txt_addsunmdl.Text;
                    objskillmodBL.check_add_modls(objskillbo,ref result);
                    Session["modl"] =  result;

                    txt_addsunmdl.Text = "";
                    DDL_modladd();
                    DDL_modl1();
                    DDL_modulelst();
                    DDL_modulelst_tosrch();
                    DDL_loadmodules.SelectedValue = Session["modl"].ToString();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Module has been added successfully');", true);
                   
                    //div_submod.Visible = false;
                    divmodl.Visible = true;
                    //DDL_loadmodules.SelectedValue = Session["modl"].ToString();

                }
            }
            catch (Exception ex)
            {
                { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + ex.Message + "');", true); }
            }

        }

        //protected void LK_Viewmod_submodls_Click(object sender, EventArgs e)
        //{
        //    div_viewall.Visible = true;
        //    //div_submod.Visible = false;
        //    divmodl.Visible = false;

        //    Skill_Matrix_Collectionbo objskilllst = new Skill_Matrix_Collectionbo();
        //    Skill_Matrix_BL objskillmodBL = new Skill_Matrix_BL();

        //    objskilllst = objskillmodBL.viw_allmodls();

        //    GV_allmodules.DataSource = objskilllst;
        //    GV_allmodules.DataBind();
        //    GV_allmodules.Columns[1].Visible = false;
        //    GV_allmodules.Columns[2].Visible = false;
        //    exitedit();


        //}

        protected void GV_allmodules_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GV_allmodules.PageIndex = e.NewPageIndex;
            Skill_Matrix_Collectionbo objskilllst = new Skill_Matrix_Collectionbo();
            Skill_Matrix_BL objskillmodBL = new Skill_Matrix_BL();

            objskilllst = objskillmodBL.viw_allmodls();

            GV_allmodules.DataSource = Session["sortmodsubmod"];
            GV_allmodules.DataBind();
            //Session["sortmodsubmod"] = objskilllst;
            exitedit();
        }

        protected void LK_viewallmods_Click(object sender, EventArgs e)
        {
            div_viewall.Visible = true;
            //div_submod.Visible = false;
            divmodl.Visible = false;
            load_gv_allmodls();
            exitedit();
            DDL_module_searchby.Visible = false;
            DDL_subM_searchby.Visible = false;
            DDL_emp_searchby.Visible = false;
            DDL_modsub_searchby.SelectedValue = "4";
            DDL_submodulelst_tosrch();
           
        }

        public  void load_gv_allmodls()
        {
            Skill_Matrix_Collectionbo objskilllst = new Skill_Matrix_Collectionbo();
            Skill_Matrix_BL objskillmodBL = new Skill_Matrix_BL();

            objskilllst = objskillmodBL.viw_allmodls();

            GV_allmodules.DataSource = objskilllst;
            GV_allmodules.DataBind();
            Session["sortmodsubmod"] = objskilllst;
            exitedit();
        }

        protected void LK_backfrmvw_Click(object sender, EventArgs e)
        {
           // Mult_Add_Scores.SetActiveView(VW_addmodnsub);
            div_viewall.Visible = false;
            divmodl.Visible = true;
        }

        protected void GV_allmodules_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName.ToUpper())
                {
                    case "EDITMODLSUB":
                        exitedit();
                        int rowIndex = Convert.ToInt32(e.CommandArgument);
                        GridViewRow gvRow = GV_allmodules.Rows[rowIndex];

                        GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                        int MID = int.Parse(GV_allmodules.DataKeys[int.Parse(e.CommandArgument.ToString())]["MID"].ToString().Trim());
                        ViewState["ModID"] = MID;
                        int SID = int.Parse(GV_allmodules.DataKeys[int.Parse(e.CommandArgument.ToString())]["SID"].ToString().Trim());
                        ViewState["subID"] = SID;

                        Label modl_name = (Label)row.FindControl("LBL_modlname");

                         Label sub_modl_name = (Label)row.FindControl("LBL_submodlname");
                         
                         TextBox txtmodl = (TextBox)row.FindControl("txt_modlname");

                         TextBox txtsubmodl = (TextBox)row.FindControl("txt_submodlname");

                         LinkButton edit_vw = (LinkButton)row.FindControl("LK_Edit_modlsub");

                            LinkButton cancel_vw = (LinkButton)row.FindControl("LK_cancel_modlsub");

                            LinkButton update_vw = (LinkButton)row.FindControl("LK_update_modlsub");

                            modl_name.Visible = false;
                            sub_modl_name.Visible = false;

                        
                            txtmodl.Visible = true;
                            txtsubmodl.Visible = true;

                            edit_vw.Visible = false;
                            cancel_vw.Visible = true;
                            update_vw.Visible = true;
                        break;

                    case "UPDATEMODLSUB":


                        int updateindex = Convert.ToInt32(e.CommandArgument);

                        GridViewRow gvupdate = GV_allmodules.Rows[updateindex];
                        Label modl_name1 = (Label)gvupdate.FindControl("LBL_modlname");

                        Label sub_modl_name1 = (Label)gvupdate.FindControl("LBL_submodlname");

                        TextBox txtmodl1 = (TextBox)gvupdate.FindControl("txt_modlname");

                        TextBox txtsubmodl1 = (TextBox)gvupdate.FindControl("txt_submodlname");

                        Skill_Matrix_BO objskillbo = new Skill_Matrix_BO();
                        Skill_Matrix_BL objskillmodBL = new Skill_Matrix_BL();
                        Skill_Matrix_Collectionbo objskilllst = new Skill_Matrix_Collectionbo();
                bool? mstatus = false;
                bool? sstatus = false;
                objskillmodBL.check_modsub_toupdate(txtmodl1.Text, txtsubmodl1.Text, ref mstatus, ref sstatus);

                if (mstatus == true && sstatus == true)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Record already exists in database');", true);
                }
                else  if(mstatus == true && sstatus == false)
                {
                   
                    objskillbo.LOGPERNR = User.Identity.Name.Trim();

                    objskillbo.SUB__MOD_NAME = txtsubmodl1.Text;
                   
                      objskillbo.SID=Convert.ToInt32(ViewState["subID"]);
                  
                     objskillmodBL.update_modsub(objskillbo, 1);

                     ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Module already exists,Sub module has been updated successfully');", true);
                }
                else  if(mstatus == false  && sstatus == true)
                {
                   
                    objskillbo.PERNR = User.Identity.Name.Trim();

                    objskillbo.MOD_name = txtmodl1.Text;

                    objskillbo.MID = Convert.ToInt32(ViewState["ModID"]);

                    objskillmodBL.update_modsub(objskillbo, 0);

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Sub Module already exists,Module has been updated successfully');", true);
                }
                 else  if(mstatus == false  && sstatus == false)
                {
                    
                    objskillbo.LOGPERNR = User.Identity.Name.Trim();
                    objskillbo.PERNR = User.Identity.Name.Trim();

                    objskillbo.MID = Convert.ToInt32(ViewState["ModID"]);

                    objskillbo.SID = Convert.ToInt32(ViewState["subID"]);

                    objskillbo.MOD_name = txtmodl1.Text;

                    objskillbo.SUB__MOD_NAME = txtsubmodl1.Text;

                     objskillmodBL.update_modsub(objskillbo, 2);
                     ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Record has been updated successfully');", true);

                }


                GV_allmodules.DataSource = objskilllst;
                        GV_allmodules.DataBind();
                        load_gv_allmodls();
                        //Session["sortmodsubmod"]= objskilllst;
                        exitedit();
                        DDL_sessionclear();
                        DDL_modladd();
                    DDL_modl1();
                    DDL_modulelst();
                    DDL_modulelst_tosrch();
                    
                        break;

                    case "CANCELMODLSUB":
                        int canceleindex = Convert.ToInt32(e.CommandArgument);
                        GridViewRow gvcancel = GV_allmodules.Rows[canceleindex];
                        //Skill_Matrix_BL objskillmodBL1 = new Skill_Matrix_BL();
                        Skill_Matrix_Collectionbo objskilllst1 = new Skill_Matrix_Collectionbo();
                        //foreach (GridViewRow row1 in GV_allmodules.Rows)
                        //{
                        //    Label modl_name_ext1 = (Label)row1.FindControl("LBL_modlname");
                        //    //string aftr_cancel = modl_name_ext1.ToString();
                        //    Label sub_modl_name_ext1 = (Label)row1.FindControl("LBL_submodlname");
                        //    //string cancel_submod = sub_modl_name_ext1.ToString();

                        //    TextBox txtmodl_ext1 = (TextBox)row1.FindControl("txt_modlname");
                        //    txtmodl_ext1.Text = "";

                         
                        //    TextBox txtsubmodl_ext1 = (TextBox)row1.FindControl("txt_submodlname");
                        //    txtsubmodl_ext1.Text = "";
                            

                        //    LinkButton edit_vw_ext1 = (LinkButton)row1.FindControl("LK_Edit_modlsub");

                        //    LinkButton cancel_vw_ext1 = (LinkButton)row1.FindControl("LK_cancel_modlsub");

                        //    LinkButton update_vw_ext1 = (LinkButton)row1.FindControl("LK_update_modlsub");

                        //    modl_name_ext1.Visible = true;
                        //    sub_modl_name_ext1.Visible = true;

                        //    txtmodl_ext1.Visible = false;
                        //    txtsubmodl_ext1.Visible = false;
                           
                        //    edit_vw_ext1.Visible = true;
                        //    cancel_vw_ext1.Visible = false;
                        //    update_vw_ext1.Visible = false;
                           
                        //}
                        GV_allmodules.DataSource = objskilllst1;
                        GV_allmodules.DataBind();
                        load_gv_allmodls();
                        //Session["sortmodsubmod"] = objskilllst1;
                        exitedit();
                        DDL_sessionclear();
                        break;
                }
            }
            catch (Exception ex)
            {
                { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + ex.Message + "');", true); }
            }

        }


        public void exitedit()
        {
            foreach (GridViewRow row in GV_allmodules.Rows)
            {

                Label modl_name_ext = (Label)row.FindControl("LBL_modlname");
                Label sub_modl_name_ext = (Label)row.FindControl("LBL_submodlname");

                TextBox txtmodl_ext = (TextBox)row.FindControl("txt_modlname");

                TextBox txtsubmodl_ext = (TextBox)row.FindControl("txt_submodlname");

                
                LinkButton edit_vw_ext = (LinkButton)row.FindControl("LK_Edit_modlsub");

                LinkButton cancel_vw_ext = (LinkButton)row.FindControl("LK_cancel_modlsub");

                LinkButton update_vw_ext = (LinkButton)row.FindControl("LK_update_modlsub");

                modl_name_ext.Visible = true;
                sub_modl_name_ext.Visible = true;

                txtmodl_ext.Visible = false;
                txtsubmodl_ext.Visible = false;

                edit_vw_ext.Visible = true;
                cancel_vw_ext.Visible = false;
                update_vw_ext.Visible = false;


            }

         
        }

        protected void GV_allmodules_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    //Find the TextBox control.
                    Label lbl1 = (e.Row.FindControl("lbl_modcretedby") as Label);
                    if (lbl1.Text == "")
                    {
                        lbl1.Text = "Admin";
                    }

                    Label lbl2 = (e.Row.FindControl("lbl_subcreatedby") as Label);
                    if (lbl2.Text == "")
                    {
                        lbl2.Text = "Admin";
                    }

                    Label lbl3 = (e.Row.FindControl("lbl_modmodifdby") as Label);
                    if (lbl3.Text == "")
                    {
                        lbl3.Text = "-";
                    }

                    Label lbl4 = (e.Row.FindControl("lbl_submodifiedby") as Label);
                    if (lbl4.Text == "")
                    {
                        lbl4.Text = "-";
                    }
                }
              
            }
            catch (Exception ex)
            {
                { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + ex.Message + "');", true); }
            }
        }

        protected void DDL_modsub_searchby_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                if (DDL_modsub_searchby.SelectedValue == "1")
                {
                    DDL_module_searchby.Visible = true;
                    DDL_subM_searchby.Visible = false;
                    DDL_emp_searchby.Visible = false;
                }

                else if (DDL_modsub_searchby.SelectedValue == "2")
                {
                    
                    DDL_subM_searchby.Visible = true;
                    DDL_module_searchby.Visible = true;
                    DDL_emp_searchby.Visible = false;
                    DDL_submodulelst_tosrch();
                }
                else if (DDL_modsub_searchby.SelectedValue == "3")
                {
                    DDL_emp_searchby.Visible = true;
                    DDL_module_searchby.Visible = false;
                    DDL_subM_searchby.Visible = false;
                }
                else if (DDL_modsub_searchby.SelectedValue == "4")
                {
                    DDL_module_searchby.Visible = false;
                    DDL_subM_searchby.Visible = false;
                    DDL_emp_searchby.Visible = false;
                }

            }
            catch (Exception ex)
            {
                { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + ex.Message + "');", true); }

            }
        }


        public void DDL_load_allemp_tosrch()
        {
            Skill_Matrix_Collectionbo objskilllst = new Skill_Matrix_Collectionbo();
            Skill_Matrix_BL objskillmodBL = new Skill_Matrix_BL();
            objskilllst = objskillmodBL.irf_allemp_DDL();

            DDL_emp_searchby.DataSource = null;
            DDL_emp_searchby.DataBind();

            DDL_emp_searchby.DataSource = objskilllst;
            DDL_emp_searchby.DataTextField = "ENAME";
            DDL_emp_searchby.DataValueField = "PERNR";
            //ddl_srch_Enam.SelectedValue = ViewState["requestor"].ToString();
            DDL_emp_searchby.DataBind();
            //DDL_irfcandidviewby.Items.Insert(0, new System.Web.UI.WebControls.ListItem(" - SELECT - ", "0"));

        }


        public void DDL_modulelst_tosrch()
        {
            Skill_Matrix_Collectionbo objskilllst = new Skill_Matrix_Collectionbo();
            Skill_Matrix_BL objskillmodBL = new Skill_Matrix_BL();
            objskilllst = objskillmodBL.Set_Skill_Details();
            DDL_module_searchby.DataSource = null;
            DDL_module_searchby.DataBind();

            DDL_module_searchby.DataSource = objskilllst;
            DDL_module_searchby.DataTextField = "MODULE";
            DDL_module_searchby.DataValueField = "MID";
            DDL_module_searchby.DataBind();
            //DDL_module_searchby.Items.Insert(0, new System.Web.UI.WebControls.ListItem(" - SELECT - ", "0"));
        }


        public void DDL_submodulelst_tosrch()
        {

            Skill_Matrix_Collectionbo objskilllst = new Skill_Matrix_Collectionbo();
            Skill_Matrix_BL objskillmodBL = new Skill_Matrix_BL();
            objskilllst = objskillmodBL.Set_submodule_DDL(DDL_module_searchby.SelectedValue.ToString());
            DDL_subM_searchby.DataSource = null;
            DDL_subM_searchby.DataBind();

            DDL_subM_searchby.DataSource = objskilllst;
            DDL_subM_searchby.DataTextField = "S_MODULE";
            DDL_subM_searchby.DataValueField = "SID";
            DDL_subM_searchby.DataBind();
           // DDL_subM_searchby.Items.Insert(0, new System.Web.UI.WebControls.ListItem(" - SELECT - ", "0"));

        }

        protected void DDL_module_searchby_SelectedIndexChanged(object sender, EventArgs e)
        {
            DDL_submodulelst_tosrch();
        }

        protected void btn_ms_search_Click(object sender, EventArgs e)
        {
            search_allmodsub();
        }

        protected void btn_ms_resetsrch_Click(object sender, EventArgs e)
        {
            try
            {
                div_viewall.Visible = true;
                divmodl.Visible = false;
                load_gv_allmodls();
                exitedit();
                DDL_module_searchby.Visible = false;
                DDL_subM_searchby.Visible = false;
                DDL_emp_searchby.Visible = false;
                DDL_modsub_searchby.SelectedValue = "4";
            }
            catch (Exception ex)
            {
                { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + ex.Message + "');", true); }

            }
        }


        public void search_allmodsub()
        {
            try
            {
                Skill_Matrix_Collectionbo objskilllst = new Skill_Matrix_Collectionbo();
                Skill_Matrix_BL objskillmodBL = new Skill_Matrix_BL();

                string empid = DDL_emp_searchby.SelectedValue;
                int module = Convert.ToInt32(DDL_module_searchby.SelectedValue);
                int submodule = Convert.ToInt32(DDL_subM_searchby.SelectedValue);

                objskilllst = objskillmodBL.srch_mod_submod(User.Identity.Name.Trim(), empid, module, submodule, Convert.ToInt32(DDL_modsub_searchby.SelectedValue));

                GV_allmodules.DataSource = objskilllst;
                GV_allmodules.DataBind();
                Session["sortmodsubmod"] = objskilllst;
                exitedit();
            }
            catch (Exception ex)
            {
                { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + ex.Message + "');", true); }

            }
        }
//--------------------------------------------------- SORT GV VIEW ALL MODULE,SUB-MODULE --------------------------------------------------------------
        protected void GV_allmodules_Sorting(object sender, GridViewSortEventArgs e)
        {
            try
            {

                List<Skill_Matrix_BO> objPIDashBoardCmpltdLst = (List<Skill_Matrix_BO>)Session["sortmodsubmod"];

                bool objSortOrder = (bool)Session["bSortedOrder"];
                switch (e.SortExpression.ToString().Trim())
                {
                    case "sort_mods":
                        if (objSortOrder)
                        {
                            if (objPIDashBoardCmpltdLst != null)
                            {
                                objPIDashBoardCmpltdLst.Sort(delegate(Skill_Matrix_BO objBo1, Skill_Matrix_BO objBo2)
                                { return (objBo1.MOD_name.CompareTo(objBo2.MOD_name)); });
                                objSortOrder = false;
                                Session.Add("bSortedOrder", objSortOrder);
                            }
                        }
                        else
                        {
                            objPIDashBoardCmpltdLst.Sort(delegate(Skill_Matrix_BO objBo1, Skill_Matrix_BO objBo2)
                            { return (objBo2.MOD_name.CompareTo(objBo1.MOD_name)); });
                            objSortOrder = true;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                        break;

                    case "sort_submods":
                        if (objSortOrder)
                        {
                            if (objPIDashBoardCmpltdLst != null)
                            {
                                objPIDashBoardCmpltdLst.Sort(delegate(Skill_Matrix_BO objBo1, Skill_Matrix_BO objBo2)
                                { return (objBo1.SUB__MOD_NAME.CompareTo(objBo2.SUB__MOD_NAME)); });
                                objSortOrder = false;
                                Session.Add("bSortedOrder", objSortOrder);
                            }
                        }
                        else
                        {
                            objPIDashBoardCmpltdLst.Sort(delegate(Skill_Matrix_BO objBo1, Skill_Matrix_BO objBo2)
                            { return (objBo2.SUB__MOD_NAME.CompareTo(objBo1.SUB__MOD_NAME)); });
                            objSortOrder = true;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                        break;

                    case "sort_mcreatedby":
                        if (objSortOrder)
                        {
                            if (objPIDashBoardCmpltdLst != null)
                            {
                                objPIDashBoardCmpltdLst.Sort(delegate(Skill_Matrix_BO objBo1, Skill_Matrix_BO objBo2)
                                { return (objBo1.Created_By.ToString().CompareTo(objBo2.Created_By.ToString())); });
                                objSortOrder = false;
                                Session.Add("bSortedOrder", objSortOrder);
                            }
                        }
                        else
                        {
                            objPIDashBoardCmpltdLst.Sort(delegate(Skill_Matrix_BO objBo1, Skill_Matrix_BO objBo2)
                            { return (objBo2.Created_By.ToString().CompareTo(objBo1.Created_By.ToString())); });
                            objSortOrder = true;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                        break;

                    case "sort_smcreatedby":
                        if (objSortOrder)
                        {
                            if (objPIDashBoardCmpltdLst != null)
                            {
                                objPIDashBoardCmpltdLst.Sort(delegate(Skill_Matrix_BO objBo1, Skill_Matrix_BO objBo2)
                                { return (objBo1.SbCreated_By.ToString().CompareTo(objBo2.SbCreated_By.ToString())); });
                                objSortOrder = false;
                                Session.Add("bSortedOrder", objSortOrder);
                            }
                        }
                        else
                        {
                            objPIDashBoardCmpltdLst.Sort(delegate(Skill_Matrix_BO objBo1, Skill_Matrix_BO objBo2)
                            { return (objBo2.SbCreated_By.ToString().CompareTo(objBo1.SbCreated_By.ToString())); });
                            objSortOrder = true;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                        break;

                    case "sort_mmodifiedby":
                        if (objSortOrder)
                        {
                            if (objPIDashBoardCmpltdLst != null)
                            {
                                objPIDashBoardCmpltdLst.Sort(delegate(Skill_Matrix_BO objBo1, Skill_Matrix_BO objBo2)
                                { return (objBo1.mod_modifiedby.ToString().CompareTo(objBo2.mod_modifiedby.ToString())); });
                                objSortOrder = false;
                                Session.Add("bSortedOrder", objSortOrder);
                            }
                        }
                        else
                        {
                            objPIDashBoardCmpltdLst.Sort(delegate(Skill_Matrix_BO objBo1, Skill_Matrix_BO objBo2)
                            { return (objBo2.mod_modifiedby.ToString().CompareTo(objBo1.mod_modifiedby.ToString())); });
                            objSortOrder = true;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                        break;

                    case "sort_smmodifiedby":
                        if (objSortOrder)
                        {
                            if (objPIDashBoardCmpltdLst != null)
                            {
                                objPIDashBoardCmpltdLst.Sort(delegate(Skill_Matrix_BO objBo1, Skill_Matrix_BO objBo2)
                                { return (objBo1.submod_modifiedby.ToString().CompareTo(objBo2.submod_modifiedby.ToString())); });
                                objSortOrder = false;
                                Session.Add("bSortedOrder", objSortOrder);
                            }
                        }
                        else
                        {
                            objPIDashBoardCmpltdLst.Sort(delegate(Skill_Matrix_BO objBo1, Skill_Matrix_BO objBo2)
                            { return (objBo2.submod_modifiedby.ToString().CompareTo(objBo1.submod_modifiedby.ToString())); });
                            objSortOrder = true;
                            Session.Add("bSortedOrder", objSortOrder);
                        }
                        break;
                }


                GV_allmodules.DataSource = objPIDashBoardCmpltdLst;
                GV_allmodules.DataBind();
                Session.Add("sortmodsubmod", objPIDashBoardCmpltdLst);
                exitedit();
                
            }

            catch (Exception ex)
            {
                { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + ex.Message + "');", true); }
            }
        }

       

    }
}
































       //public void modsub_check_forexists()
       // {
       //     try
       //     {

       //         Skill_Matrix_Collectionbo objskilllst = new Skill_Matrix_Collectionbo();
       //         Skill_Matrix_BL objskillmodBL = new Skill_Matrix_BL();
       //         Skill_Matrix_BO objskillbo = new Skill_Matrix_BO();

       //         TextBox modl = (TextBox)GV_allmodules.FindControl("txt_modlname");

       //         TextBox submodl = (TextBox)GV_allmodules.FindControl("txt_submodlname");
       //         bool? mstatus = false;
       //         bool? sstatus = false;
       //         objskillmodBL.check_modsub_toupdate(modl.ToString(), submodl.ToString(), ref mstatus, ref sstatus);

       //         if (mstatus == true && sstatus == true)
       //         {
       //             ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Record already exists in database');", true);
       //         }
       //         else  if(mstatus == true && sstatus == false)
       //         {
       //             TextBox submodl1 = (TextBox)GV_allmodules.FindControl("txt_submodlname");
       //             objskillbo.LOGPERNR = User.Identity.Name.Trim();

       //             objskillbo.SUB__MOD_NAME = submodl1.ToString();
                   
       //               objskillbo.SID=Convert.ToInt32(ViewState["subID"]);
                  
       //              objskillmodBL.update_modsub(objskillbo, 2);

       //              ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Module already exists,Sub module has been updated successfully');", true);
       //         }
       //         else  if(mstatus == false  && sstatus == true)
       //         {
       //             TextBox modl1 = (TextBox)GV_allmodules.FindControl("txt_modlname");
       //             objskillbo.PERNR = User.Identity.Name.Trim();

       //             objskillbo.MOD_name = modl1.ToString();

       //             objskillbo.MID = Convert.ToInt32(ViewState["ModID"]);

       //             objskillmodBL.update_modsub(objskillbo, 1);

       //             ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Sub Module already exists,Module has been updated successfully');", true);
       //         }
       //          else  if(mstatus == false  && sstatus == false)
       //         {
       //             TextBox modl2 = (TextBox)GV_allmodules.FindControl("txt_modlname");

       //             TextBox submodl2 = (TextBox)GV_allmodules.FindControl("txt_submodlname");

       //             objskillbo.LOGPERNR = User.Identity.Name.Trim();
       //             objskillbo.PERNR = User.Identity.Name.Trim();

       //             objskillbo.MID = Convert.ToInt32(ViewState["ModID"]);

       //             objskillbo.SID = Convert.ToInt32(ViewState["subID"]);

       //             objskillbo.MOD_name = modl2.ToString();

       //             objskillbo.SUB__MOD_NAME = submodl2.ToString();

       //              objskillmodBL.update_modsub(objskillbo, 3);
       //              ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Record has been updated successfully');", true);

       //         }
                    
       //     }
       //     catch (Exception ex)
       //     {
       //         { ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('" + ex.Message + "');", true); }
       //     }
       // }
   