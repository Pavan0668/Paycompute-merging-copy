using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iEmpPower.Old_App_Code.iEmpPowerDAL.PR_Masterupload;
using iEmpPower.Old_App_Code.iEmpPowerBO.PR_Masterupload.PR_EXCELCollBo;
using iEmpPower.Old_App_Code.iEmpPowerBO.PR_Masterupload;

namespace iEmpPower.Old_App_Code.iEmpPowerBL.PR_MasteruploadBL
{
    public class PR_EXCELCollBL
    {
        PR_MasteruploadDALDataContext objDataContext = new PR_MasteruploadDALDataContext();

        public int Create_ZMM_MIS_PR(PR_ExcelUploadBO objempBo)
        {
            try
            {
                objDataContext = new PR_MasteruploadDALDataContext();
                int iResultCode = objDataContext.usp_PR_uploadExcell_master_ZMM_MIS(
                      objempBo.CID,
                      objempBo.C_DESC
                       , objempBo.A_DESC
                    , objempBo.B_DESC);

                return iResultCode;
            }
            catch (Exception Ex)
            { throw new Exception(Ex.Message); }
        }

        public int Create_master_T024_PR(PR_ExcelUploadBO objempBo)
        {
            try
            {
                objDataContext = new PR_MasteruploadDALDataContext();
                int iResultCode = objDataContext.usp_PR_uploadExcell_master_T024(
                      objempBo.EKGRP,
                      objempBo.EKNAM);

                return iResultCode;
            }
            catch (Exception Ex)
            { throw new Exception(Ex.Message); }
        }

        public int Create_master_T001W_PR(PR_ExcelUploadBO objempBo)
        {
            try
            {
                objDataContext = new PR_MasteruploadDALDataContext();
                int iResultCode = objDataContext.usp_PR_uploadExcell_master_T001W(
                      objempBo.WERKS,
                      objempBo.NAME1);
                return iResultCode;
            }
            catch (Exception Ex)
            { throw new Exception(Ex.Message); }
        }

        public int Create_master_TSPAT_PR(PR_ExcelUploadBO objempBo)
        {
            try
            {
                objDataContext = new PR_MasteruploadDALDataContext();
                int iResultCode = objDataContext.usp_PR_uploadExcell_master_TSPAT(
                      objempBo.SPART,
                      objempBo.VTEXT);
                return iResultCode;
            }
            catch (Exception Ex)
            { throw new Exception(Ex.Message); }
        }

        public int Create_master_ZMM_PR_REGION(PR_ExcelUploadBO objempBo)
        {
            try
            {
                objDataContext = new PR_MasteruploadDALDataContext();
                int iResultCode = objDataContext.usp_PR_uploadExcell_master_ZMM_PR_REGION(
                      objempBo.REGION_ID,
                      objempBo.REGION_TEXT);
                return iResultCode;
            }
            catch (Exception Ex)
            { throw new Exception(Ex.Message); }
        }

        public int Create_master_T006J_PR(PR_ExcelUploadBO objempBo)
        {
            try
            {
                objDataContext = new PR_MasteruploadDALDataContext();
                int iResultCode = objDataContext.usp_PR_uploadExcell_master_T006J(
                      objempBo.ISOCODE,
                      objempBo.ISOTXT);
                return iResultCode;
            }
            catch (Exception Ex)
            { throw new Exception(Ex.Message); }
        }

        public int Create_master_T512T_Iexpense(PR_ExcelUploadBO objempBo)
        {
            try
            {
                objDataContext = new PR_MasteruploadDALDataContext();
                int iResultCode = objDataContext.usp_iexpense_uploadExcell_master_T512T(
                      objempBo.LGART,
                      objempBo.LGTXT);
                return iResultCode;
            }
            catch (Exception Ex)
            { throw new Exception(Ex.Message); }
        }

        public int Create_ESS_FBPHOA(PR_ExcelUploadBO objempBo)
        {
            try
            {
                objDataContext = new PR_MasteruploadDALDataContext();
                int iResultCode = objDataContext.usp_master_uploadExcell_FBP_FBPHOA(
                      objempBo.LGART,
                      objempBo.LTEXT,
                      objempBo.Colid
                       );
                return iResultCode;
            }
            catch (Exception Ex)
            { throw new Exception(Ex.Message); }
        }

        public int Create_FBP_master_PA9001(PR_ExcelUploadBO objempBo)
        {
            try
            {
                objDataContext = new PR_MasteruploadDALDataContext();
                int iResultCode = objDataContext.usp_master_uploadExcell_FBP_PA9001(
                      objempBo.PERNR,
                      objempBo.AN_FBP);

                return iResultCode;
            }
            catch (Exception Ex)
            { throw new Exception(Ex.Message); }
        }

        public int Create_FBP_master_T7INA9(PR_ExcelUploadBO objempBo)
        {
            try
            {
                objDataContext = new PR_MasteruploadDALDataContext();
                int iResultCode = objDataContext.usp_master_uploadExcell_FBP_T7INA9(
                      objempBo.ALGRP,
                      objempBo.LGART,
                      objempBo.AMUNT);

                return iResultCode;
            }
            catch (Exception Ex)
            { throw new Exception(Ex.Message); }
        }

        public int Create_FBP_master_T7INA3(PR_ExcelUploadBO objempBo)
        {
            try
            {
                objDataContext = new PR_MasteruploadDALDataContext();
                int iResultCode = objDataContext.usp_master_uploadExcell_FBP_master_T7INA3(
                      objempBo.TRFAR,
                      objempBo.TRFGB,
                      objempBo.TRFGR,
                      objempBo.TRFST,
                      objempBo.ALGRP
                      );

                return iResultCode;
            }
            catch (Exception Ex)
            { throw new Exception(Ex.Message); }
        }

      


        public int Create_FBP_MSS_PA0008(PR_ExcelUploadBO objempBo)
        {
            try
            {
                objDataContext = new PR_MasteruploadDALDataContext();
                int iResultCode = objDataContext.usp_master_uploadExcell_FBP_MSS_PA0008(
                      objempBo.PERNR,
                      objempBo.TRFAR,
                      objempBo.TRFGB,
                      objempBo.TRFGR,
                      objempBo.TRFST,
                      objempBo.BEGDA,
                      objempBo.ENDDA


                      );

                return iResultCode;
            }
            catch (Exception Ex)
            { throw new Exception(Ex.Message); }
        }

        public int Create_IT_master_T7INI3(PR_ExcelUploadBO objempBo)
        {
            try
            {
                objDataContext = new PR_MasteruploadDALDataContext();
                int iResultCode = objDataContext.usp_master_uploadExcell_T7INI3_IT(
                      objempBo.ICODE,
                      objempBo.ITEXT);

                return iResultCode;
            }
            catch (Exception Ex)
            { throw new Exception(Ex.Message); }
        }

        public int Create_IT_master_T7INI9(PR_ExcelUploadBO objempBo)
        {
            try
            {
                objDataContext = new PR_MasteruploadDALDataContext();
                int iResultCode = objDataContext.usp_master_uploadExcell_T7INI9_IT(
                      objempBo.SBSEC,
                      objempBo.SBDIV,
                      objempBo.SDVLT,
                objempBo.TXEXM);

                return iResultCode;
            }
            catch (Exception Ex)
            { throw new Exception(Ex.Message); }
        }

        public int Create_IT_master_T7INI8(PR_ExcelUploadBO objempBo)
        {
            try
            {
                objDataContext = new PR_MasteruploadDALDataContext();
                int iResultCode = objDataContext.usp_master_uploadExcell_T7INI8_IT(
                      objempBo.SBSEC,
                      objempBo.SBDIV,
                      objempBo.SBDDS);

                return iResultCode;
            }
            catch (Exception Ex)
            { throw new Exception(Ex.Message); }
        }

        public int Create_IT_master_T7INI4(PR_ExcelUploadBO objempBo)
        {
            try
            {
                objDataContext = new PR_MasteruploadDALDataContext();
                int iResultCode = objDataContext.usp_master_uploadExcell_T7INI4_IT(
                      objempBo.ICODE,
                      objempBo.ENDDA,
                      objempBo.BEGDA,
                      objempBo.CTGRY,
                      objempBo.WAEHI,
                      objempBo.ITLMT);

                return iResultCode;
            }
            catch (Exception Ex)
            { throw new Exception(Ex.Message); }
        }

        public int Create_IT_master_T7INI5(PR_ExcelUploadBO objempBo)
        {
            try
            {
                objDataContext = new PR_MasteruploadDALDataContext();
                int iResultCode = objDataContext.usp_master_uploadExcell_T7INI5_IT(
                      objempBo.SBSEC,
                      objempBo.SBTDS);

                return iResultCode;
            }
            catch (Exception Ex)
            { throw new Exception(Ex.Message); }
        }

        public int Create_CM_master_TCURR(PR_ExcelUploadBO objempBo)
        {
            try
            {
                objDataContext = new PR_MasteruploadDALDataContext();
                int iResultCode = objDataContext.usp_master_uploadExcell_master_TCURR_CM(
                      objempBo.FCURR,
                      objempBo.TCURR,
                        objempBo.UKURS);

                return iResultCode;
            }
            catch (Exception Ex)
            { throw new Exception(Ex.Message); }
        }

        public int Create_CM_master_TCURT(PR_ExcelUploadBO objempBo)
        {
            try
            {
                objDataContext = new PR_MasteruploadDALDataContext();
                int iResultCode = objDataContext.usp_master_uploadExcell_master_TCURT_CM(
                      objempBo.WAERS,
                      objempBo.LTEXT);

                return iResultCode;
            }
            catch (Exception Ex)
            { throw new Exception(Ex.Message); }
        }

        public int Create_CM_master_PRPS(PR_ExcelUploadBO objempBo)
        {
            try
            {
                objDataContext = new PR_MasteruploadDALDataContext();
                int iResultCode = objDataContext.usp_master_uploadExcell_master_PRPS_CM(
                      objempBo.POSID,
                      objempBo.POST1,
                        objempBo.PSPHI,
                        objempBo.VERNR,
                        objempBo.VERNA,
                        objempBo.PBUKR,
                        objempBo.POSKI,
                        objempBo.PSPNR,
                        objempBo.Created_By,
                        objempBo.Created_On,
                        objempBo.Company_Code,
                        objempBo.Start_Date,
                        objempBo.End_Date,
                        objempBo.Updated_On,
                        objempBo.Updated_By,
                        objempBo.WBS_EXTNID





);

                return iResultCode;
            }
            catch (Exception Ex)
            { throw new Exception(Ex.Message); }
        }

        public int Create_Travel_master_T005T(PR_ExcelUploadBO objempBo)
        {
            try
            {
                objDataContext = new PR_MasteruploadDALDataContext();
                int iResultCode = objDataContext.usp_master_uploadExcell_master_T005T_Travel(
                      objempBo.LAND1,
                      objempBo.LANDX,
                       objempBo.NATIO

);

                return iResultCode;
            }
            catch (Exception Ex)
            { throw new Exception(Ex.Message); }
        }

        public int Create_Travel_master_T706O(PR_ExcelUploadBO objempBo)
        {
            try
            {
                objDataContext = new PR_MasteruploadDALDataContext();
                int iResultCode = objDataContext.usp_master_uploadExcell_master_T706O_Travel(
                      objempBo.LAND1,
                      objempBo.RGION,
                       objempBo.TEXT25

);

                return iResultCode;
            }
            catch (Exception Ex)
            { throw new Exception(Ex.Message); }
        }
                     
    }
}
                  

          