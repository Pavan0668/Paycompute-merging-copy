using iEmpPower.Old_App_Code.iEmpPowerBO.IT;
using iEmpPower.Old_App_Code.iEmpPowerDAL.IT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iEmpPower.Old_App_Code.iEmpPowerBL.IT
{
    public class ITbl
    {

        ITdalDataContext objITDataContext = new ITdalDataContext();

        public List<ITbo> Load_IT_Section80Details(string pernr, ref int? count)
        {
            ITdalDataContext objITDataContext = new ITdalDataContext();
            List<ITbo> ITboList = new List<ITbo>();
            foreach (var vRow in objITDataContext.usp_IT_GetSec80(pernr, ref count))
            {
                ITbo ITboObj = new ITbo();

                ITboObj.ID = vRow.ID;
                ITboObj.LID = vRow.LID;
                ITboObj.SBSEC = Decimal.Parse(vRow.SBSEC);
                ITboObj.SBDIV = Decimal.Parse(vRow.SBDIV);
                ITboObj.SBDDS = vRow.SBDDS;
                ITboObj.SDVLT = Decimal.Parse(vRow.SDVLT);
                ITboObj.TXEXM = Decimal.Parse(vRow.TXEXM);
                ITboObj.CURR = "INR";
                ITboObj.PROPCONTR = vRow.PROPCONTR;
                ITboObj.ACTCONTR = vRow.ACTCONTR;
                ITboObj.EMPCOMMENTS = vRow.EMPCOMMENTS;
                ITboObj.CONACTPROP = vRow.CONACTPROP;
                ITboObj.RECEIPT_FID = string.IsNullOrEmpty(vRow.RECEIPT_FID) ? "" : vRow.RECEIPT_FID;
                ITboObj.RECEIPT_FILE = string.IsNullOrEmpty(vRow.RECEIPT_FILE) ? "" : vRow.RECEIPT_FILE;
                ITboObj.RECEIPT_FPATH = string.IsNullOrEmpty(vRow.RECEIPT_FPATH) ? "" : vRow.RECEIPT_FPATH;

                ITboList.Add(ITboObj);
            }
            return ITboList;
        }



        public List<ITbo> Load_IT_Section80CDetails(string pernr, ref int? count)
        {
            try
            {

                ITdalDataContext objITDataContext = new ITdalDataContext();
                List<ITbo> ITboList = new List<ITbo>();
                foreach (var vRow in objITDataContext.usp_IT_GetSec80C(pernr, ref count))
                {
                    ITbo ITboObj = new ITbo();
                    //t2.ICODE, t2.ITTXT , t4.ITLMT, t4.CTGRY
                    ITboObj.ID = vRow.ID;
                    ITboObj.LID = vRow.LID;
                    ITboObj.ICODE = vRow.ICODE;
                    ITboObj.ITTXT = vRow.ITTXT;
                    ITboObj.ITLMT = Convert.ToDecimal("150000.0");////Convert.ToDecimal(vRow.ITLMT);////ITboObj.ITLMT = vRow.ITLMT;
                    ITboObj.CTGRY = vRow.CTGRY;
                    ITboObj.CURR = "INR";
                    ITboObj.PROPINVST = vRow.PROPINVST;
                    ITboObj.ACTINVST = vRow.ACTINVST;
                    ITboObj.EMPCOMMENTS = vRow.EMPCOMMENTS;
                    ITboObj.CONACTPROP = vRow.CONACTPROP;
                    ITboObj.RECEIPT_FID = string.IsNullOrEmpty(vRow.RECEIPT_FID) ? "" : vRow.RECEIPT_FID;
                    ITboObj.RECEIPT_FILE = string.IsNullOrEmpty(vRow.RECEIPT_FILE) ? "" : vRow.RECEIPT_FILE;
                    ITboObj.RECEIPT_FPATH = string.IsNullOrEmpty(vRow.RECEIPT_FPATH) ? "" : vRow.RECEIPT_FPATH;
                    ITboList.Add(ITboObj);
                }
                return ITboList;
            }
            catch (Exception Ex)
            { throw Ex; }
        }

        public void Create_ITHousing(ITbo ITObjbo, ref int? HID, ref bool? Sts)
        {
            try
            {
                ITdalDataContext objITDataContext = new ITdalDataContext();


                objITDataContext.usp_IT_Submit_Housing(ITObjbo.ID, ITObjbo.PERNR, ITObjbo.BEGDA, ITObjbo.ENDDA, ITObjbo.ACCOM, ITObjbo.METRO, ITObjbo.RTAMT,
                    ITObjbo.HRTXE, ITObjbo.LDAD1, ITObjbo.LDAID, ITObjbo.LDADE, ITObjbo.CREATED_BY, ITObjbo.CREATED_ON, ITObjbo.MODIFIED_BY, ITObjbo.STATUS, ITObjbo.Flag, ITObjbo.EMPCOMMENTS, ITObjbo.Address, ITObjbo.State, ITObjbo.City, ITObjbo.LDNAM,
                    ref HID, ref Sts);

                objITDataContext.Dispose();
            }
            catch (Exception Ex)
            { throw Ex; }

        }

        public List<ITbo> Load_IT_HousingDetails(string pernr)
        {
            ITdalDataContext objITDataContext = new ITdalDataContext();
            List<ITbo> ITboList = new List<ITbo>();
            foreach (var vRow in objITDataContext.usp_IT_GetHousing(pernr))
            {
                ITbo ITboObj = new ITbo();
                //ACCOM,METRO,RTAMT,HRTXE,LDAD1,LDAID,LDADE,EMPCOMMENTS,STATUS
                ITboObj.ID = vRow.ID;
                ITboObj.BEGDA = vRow.BEGDA;
                ITboObj.ENDDA = vRow.ENDDA;
                ITboObj.METRO = vRow.METRO.ToString();
                ITboObj.ACCOM = vRow.ACCOM.ToString();
                ITboObj.METRO = vRow.METRO.ToString();
                ITboObj.RTAMT = vRow.RTAMT;
                ITboObj.HRTXE = vRow.HRTXE;
                ITboObj.LDAD1 = vRow.LDAD1;
                ITboObj.LDAID = vRow.LDAID;
                ITboObj.LDADE = vRow.LDADE.ToString();
                ITboObj.EMPCOMMENTS = vRow.EMPCOMMENTS;
                ITboObj.STATUS = vRow.STATUS;
                ITboObj.Address = vRow.Address;
                ITboObj.State = vRow.State;
                ITboObj.City = vRow.City;
                ITboObj.LDNAM = vRow.LDNAM;
                ITboList.Add(ITboObj);
            }
            return ITboList;
        }


        public void Create_ITHousingOthers(ITbo ITObjbo, ref int? HID, ref bool? Sts)
        {
            try
            {
                ITdalDataContext objITDataContext = new ITdalDataContext();

                objITDataContext.usp_IT_Submit_HousingOthers(ITObjbo.ID, ITObjbo.PERNR, ITObjbo.BEGDA, ITObjbo.ENDDA, ITObjbo.PROPTYP, ITObjbo.RENTO, ITObjbo.INT24,
                   ITObjbo.LETVL, ITObjbo.REP24, ITObjbo.OTH24, ITObjbo.TDSOT, ITObjbo.BSPFT, ITObjbo.CPGLN, ITObjbo.CPGLS, ITObjbo.CPGNS,
                   ITObjbo.CPGSS, ITObjbo.DVDND, ITObjbo.INTRS, ITObjbo.UNSPI, ITObjbo.TDSAT,
                   ITObjbo.CREATED_BY, ITObjbo.CREATED_ON, ITObjbo.MODIFIED_BY, ITObjbo.STATUS, ITObjbo.Flag, ITObjbo.EMPCOMMENTS, ITObjbo.EMPCOMMENTS2, ref HID, ref Sts);

                objITDataContext.Dispose();
            }
            catch (Exception Ex)
            { throw Ex; }

        }

        public List<ITbo> Load_IT_HousingOthersDetails(string pernr, string PropTyp)
        {
            ITdalDataContext objITDataContext = new ITdalDataContext();
            List<ITbo> ITboList = new List<ITbo>();
            foreach (var vRow in objITDataContext.usp_IT_GetHousingOthers(pernr, PropTyp))
            {
                ITbo ITboObj = new ITbo();
                //ID,PROPTYP,RENTO,INT24,LETVL,REP24,OTH24,TDSOT,
                // BSPFT,CPGLN,CPGLS,CPGNS,CPGSS,DVDND,INTRS,UNSPI,TDSAT, EMPCOMMENTS,EMPCOMMENTS2,STATUS  
                ITboObj.ID = vRow.ID;
                ITboObj.PROPTYP = vRow.PROPTYP;
                ITboObj.RENTO = vRow.RENTO.ToString();
                ITboObj.INT24 = vRow.INT24;
                ITboObj.LETVL = vRow.LETVL;
                ITboObj.REP24 = vRow.REP24;
                ITboObj.OTH24 = vRow.OTH24;
                ITboObj.TDSOT = vRow.TDSOT;

                ITboObj.BSPFT = vRow.BSPFT;
                ITboObj.CPGLN = vRow.CPGLN;
                ITboObj.CPGLS = vRow.CPGLS;
                ITboObj.CPGNS = vRow.CPGNS;
                ITboObj.CPGSS = vRow.CPGSS;
                ITboObj.DVDND = vRow.DVDND;
                ITboObj.INTRS = vRow.INTRS;
                ITboObj.UNSPI = vRow.UNSPI;
                ITboObj.TDSAT = vRow.TDSAT;

                ITboObj.EMPCOMMENTS = vRow.EMPCOMMENTS;
                ITboObj.EMPCOMMENTS2 = vRow.EMPCOMMENTS2;
                ITboObj.STATUS = vRow.STATUS;

                ITboList.Add(ITboObj);
            }
            return ITboList;
        }



        public void Create_ITSECTION80HEADR(ITbo ITObjbo, ref int? ITHID)
        {
            try
            {
                ITdalDataContext objITDataContext = new ITdalDataContext();

                //objITDataContext.usp_IT_Submit_Section80(ITObjbo.ID, ITObjbo.PERNR, ITObjbo.BEGDA, ITObjbo.ENDDA, 
                //    ITObjbo.SBSEC,ITObjbo.SBDIV,ITObjbo.PROPCONTR,ITObjbo.ACTCONTR,ITObjbo.RECEIPT_FILE,ITObjbo.RECEIPT_FID,ITObjbo.RECEIPT_FPATH,ITObjbo.EMPCOMMENTS,
                //   ITObjbo.CREATED_BY, ITObjbo.CREATED_ON, ITObjbo.MODIFIED_BY, ITObjbo.STATUS, ITObjbo.Flag);

                objITDataContext.usp_IT_Submit_Section80Header(ITObjbo.ID, ITObjbo.PERNR, ITObjbo.BEGDA, ITObjbo.ENDDA,
                   ITObjbo.CREATED_BY, ITObjbo.CREATED_ON, ITObjbo.MODIFIED_BY, ITObjbo.STATUS, ITObjbo.Flag, ITObjbo.CONACTPROP, ref ITHID);


                objITDataContext.Dispose();
            }
            catch (Exception Ex)
            { throw Ex; }

        }


        //Create_ITSection80Trans
        //Create_ITSection80Transaction

        public void Create_ITSection80Transaction(ITbo ITObjbo, ref bool? sts)
        {
            try
            {
                ITdalDataContext objITDataContext = new ITdalDataContext();

                //objITDataContext.usp_IT_Submit_Section80(ITObjbo.ID, ITObjbo.PERNR, ITObjbo.BEGDA, ITObjbo.ENDDA, 
                //    ITObjbo.SBSEC,ITObjbo.SBDIV,ITObjbo.PROPCONTR,ITObjbo.ACTCONTR,ITObjbo.RECEIPT_FILE,ITObjbo.RECEIPT_FID,ITObjbo.RECEIPT_FPATH,ITObjbo.EMPCOMMENTS,
                //   ITObjbo.CREATED_BY, ITObjbo.CREATED_ON, ITObjbo.MODIFIED_BY, ITObjbo.STATUS, ITObjbo.Flag);

                objITDataContext.usp_IT_Submit_Section80Transaction(ITObjbo.ID, ITObjbo.LID, ITObjbo.SBSEC, ITObjbo.SBDIV,
                    ITObjbo.PROPCONTR, ITObjbo.ACTCONTR, ITObjbo.RECEIPT_FILE, ITObjbo.RECEIPT_FID, ITObjbo.RECEIPT_FPATH, ITObjbo.EMPCOMMENTS, ITObjbo.Flag, ref sts);


                objITDataContext.Dispose();
            }
            catch (Exception Ex)
            { throw Ex; }

        }


        public void ITSec80_fileUpdate(ITbo ITObjbo)
        {
            try
            {
                ITdalDataContext objITDataContext = new ITdalDataContext();
                objITDataContext.usp_IT_Sec80_fileUpdate(ITObjbo.ID, ITObjbo.LID, ITObjbo.RECEIPT_FILE, ITObjbo.RECEIPT_FID, ITObjbo.RECEIPT_FPATH, ITObjbo.CREATED_BY);
                objITDataContext.Dispose();
            }
            catch (Exception Ex)
            { throw Ex; }
        }

        public void ITSec80_fileDelete(ITbo ITObjbo)
        {
            try
            {
                ITdalDataContext objITDataContext = new ITdalDataContext();
                objITDataContext.usp_IT_Sec80_fileDelete(ITObjbo.ID, ITObjbo.LID, ITObjbo.CREATED_BY);
                objITDataContext.Dispose();
            }
            catch (Exception Ex)
            { throw Ex; }
        }

        public void Create_ITSECTION80CHEADR(ITbo ITObjbo, ref int? ITHID)
        {
            try
            {
                ITdalDataContext objITDataContext = new ITdalDataContext();

                //objITDataContext.usp_IT_Submit_Section80(ITObjbo.ID, ITObjbo.PERNR, ITObjbo.BEGDA, ITObjbo.ENDDA, 
                //    ITObjbo.SBSEC,ITObjbo.SBDIV,ITObjbo.PROPCONTR,ITObjbo.ACTCONTR,ITObjbo.RECEIPT_FILE,ITObjbo.RECEIPT_FID,ITObjbo.RECEIPT_FPATH,ITObjbo.EMPCOMMENTS,
                //   ITObjbo.CREATED_BY, ITObjbo.CREATED_ON, ITObjbo.MODIFIED_BY, ITObjbo.STATUS, ITObjbo.Flag);

                objITDataContext.usp_IT_Submit_Section80CHeader(ITObjbo.ID, ITObjbo.PERNR, ITObjbo.BEGDA, ITObjbo.ENDDA,
                   ITObjbo.CREATED_BY, ITObjbo.CREATED_ON, ITObjbo.MODIFIED_BY, ITObjbo.STATUS, ITObjbo.Flag, ITObjbo.CONACTPROP, ref ITHID);


                objITDataContext.Dispose();
            }
            catch (Exception Ex)
            { throw Ex; }

        }

        public void Create_ITSection80CTransaction(ITbo ITObjbo, ref bool? sts)
        {
            try
            {
                ITdalDataContext objITDataContext = new ITdalDataContext();

                //objITDataContext.usp_IT_Submit_Section80(ITObjbo.ID, ITObjbo.PERNR, ITObjbo.BEGDA, ITObjbo.ENDDA, 
                //    ITObjbo.SBSEC,ITObjbo.SBDIV,ITObjbo.PROPCONTR,ITObjbo.ACTCONTR,ITObjbo.RECEIPT_FILE,ITObjbo.RECEIPT_FID,ITObjbo.RECEIPT_FPATH,ITObjbo.EMPCOMMENTS,
                //   ITObjbo.CREATED_BY, ITObjbo.CREATED_ON, ITObjbo.MODIFIED_BY, ITObjbo.STATUS, ITObjbo.Flag);

                objITDataContext.usp_IT_Submit_Section80CTransaction(ITObjbo.ID, ITObjbo.LID, ITObjbo.ICODE,
                    ITObjbo.PROPINVST, ITObjbo.ACTINVST, ITObjbo.RECEIPT_FILE, ITObjbo.RECEIPT_FID, ITObjbo.RECEIPT_FPATH, ITObjbo.EMPCOMMENTS, ITObjbo.Flag, ref sts);


                objITDataContext.Dispose();
            }
            catch (Exception Ex)
            { throw Ex; }

        }


        public void ITSec80C_fileUpdate(ITbo ITObjbo)
        {
            try
            {
                ITdalDataContext objITDataContext = new ITdalDataContext();
                objITDataContext.usp_IT_Sec80C_fileUpdate(ITObjbo.ID, ITObjbo.LID, ITObjbo.RECEIPT_FILE, ITObjbo.RECEIPT_FID, ITObjbo.RECEIPT_FPATH, ITObjbo.CREATED_BY);
                objITDataContext.Dispose();
            }
            catch (Exception Ex)
            { throw Ex; }
        }


        public void ITSec80C_fileDelete(ITbo ITObjbo)
        {
            try
            {
                ITdalDataContext objITDataContext = new ITdalDataContext();
                objITDataContext.usp_IT_Sec80C_fileDelete(ITObjbo.ID, ITObjbo.LID, ITObjbo.CREATED_BY);
                objITDataContext.Dispose();
            }
            catch (Exception Ex)
            { throw Ex; }
        }

        public List<ITbo> Load_IT_HeaderHistory(string pernr, int flag)
        {
            ITdalDataContext objITDataContext = new ITdalDataContext();
            List<ITbo> ITboList = new List<ITbo>();
            foreach (var vRow in objITDataContext.usp_IT_EmpViewHeaderHistry(pernr, flag))
            {
                ITbo ITboObj = new ITbo();

                ITboObj.ID = vRow.ID;
                ITboObj.CREATED_ON = vRow.CREATED_ON;
                ITboObj.ENDDA = vRow.ENDDA;
                ITboObj.BEGDA = vRow.BEGDA;
                ITboObj.APPROVEDON = string.IsNullOrEmpty(vRow.APPROVED_ON.ToString()) ? DateTime.Parse("01-01-0001") : vRow.APPROVED_ON;
                ITboObj.REMARKS = string.IsNullOrEmpty(vRow.REMARKS) ? " - " : vRow.REMARKS;
                ITboObj.STATUS = vRow.STATUS;
                ITboObj.CONACTPROP = vRow.CONACTPROP;
                ITboObj.ITTYP = vRow.PROPTYP.Trim();

                ITboList.Add(ITboObj);
            }
            return ITboList;
        }





        public List<ITbo> Load_Sec80Details(int ID, string pernr)
        {
            ITdalDataContext objITDataContext = new ITdalDataContext();
            List<ITbo> ITboList = new List<ITbo>();
            foreach (var vRow in objITDataContext.usp_IT_GetSec80History(ID, pernr))
            {
                ITbo ITboObj = new ITbo();

                ITboObj.ID = vRow.ID;
                ITboObj.LID = vRow.LID;
                ITboObj.SBSEC = Decimal.Parse(vRow.SBSEC);
                ITboObj.SBDIV = Decimal.Parse(vRow.SBDIV);
                ITboObj.SBDDS = vRow.SBDDS;
                ITboObj.SDVLT = Decimal.Parse(vRow.SDVLT);
                ITboObj.TXEXM = Decimal.Parse(vRow.TXEXM);
                ITboObj.CURR = "INR";
                ITboObj.PROPCONTR = vRow.PROPCONTR;
                ITboObj.ACTCONTR = vRow.ACTCONTR;
                ITboObj.EMPCOMMENTS = vRow.EMPCOMMENTS;
                ITboObj.RECEIPT_FID = string.IsNullOrEmpty(vRow.RECEIPT_FID) ? "" : vRow.RECEIPT_FID;
                ITboObj.RECEIPT_FILE = string.IsNullOrEmpty(vRow.RECEIPT_FILE) ? "" : vRow.RECEIPT_FILE;
                ITboObj.RECEIPT_FPATH = string.IsNullOrEmpty(vRow.RECEIPT_FPATH) ? "" : vRow.RECEIPT_FPATH;

                ITboList.Add(ITboObj);
            }
            return ITboList;
        }


        public List<ITbo> Load_Sec80CDetails(int ID, string pernr)
        {
            ITdalDataContext objITDataContext = new ITdalDataContext();
            List<ITbo> ITboList = new List<ITbo>();
            foreach (var vRow in objITDataContext.usp_IT_GetSec80CHistory(ID, pernr))
            {
                ITbo ITboObj = new ITbo();
                //t2.ICODE, t2.ITTXT , t4.ITLMT, t4.CTGRY
                ITboObj.ID = vRow.ID;
                ITboObj.LID = vRow.LID;
                ITboObj.ICODE = vRow.ICODE;
                ITboObj.ITTXT = vRow.ITTXT;
                ITboObj.ITLMT = Convert.ToDecimal("150000.0");////  ITboObj.ITLMT = Convert.ToDecimal(vRow.ITLMT);// vRow.ITLMT;
                ITboObj.CTGRY = vRow.CTGRY;
                ITboObj.CURR = "INR";
                ITboObj.PROPINVST = vRow.PROPINVST;
                ITboObj.ACTINVST = vRow.ACTINVST;
                ITboObj.EMPCOMMENTS = vRow.EMPCOMMENTS;
                ITboObj.RECEIPT_FID = string.IsNullOrEmpty(vRow.RECEIPT_FID) ? "" : vRow.RECEIPT_FID;
                ITboObj.RECEIPT_FILE = string.IsNullOrEmpty(vRow.RECEIPT_FILE) ? "" : vRow.RECEIPT_FILE;
                ITboObj.RECEIPT_FPATH = string.IsNullOrEmpty(vRow.RECEIPT_FPATH) ? "" : vRow.RECEIPT_FPATH;
                ITboList.Add(ITboObj);
            }
            return ITboList;
        }



        public List<ITbo> Load_HousingDetails(int ID, string pernr)
        {
            ITdalDataContext objITDataContext = new ITdalDataContext();
            List<ITbo> ITboList = new List<ITbo>();
            foreach (var vRow in objITDataContext.usp_IT_GetHousingHistory(ID, pernr))
            {
                ITbo ITboObj = new ITbo();
                //ACCOM,METRO,RTAMT,HRTXE,LDAD1,LDAID,LDADE,EMPCOMMENTS,STATUS
                ITboObj.ID = vRow.ID;
                ITboObj.ACCOM = vRow.ACCOM.ToString();
                ITboObj.METRO = vRow.METRO.ToString();
                ITboObj.RTAMT = vRow.RTAMT;
                ITboObj.HRTXE = vRow.HRTXE;
                ITboObj.LDAD1 = vRow.LDAD1;
                ITboObj.LDAID = vRow.LDAID;
                ITboObj.LDADE = vRow.LDADE.ToString();
                ITboObj.EMPCOMMENTS = vRow.EMPCOMMENTS;

                ITboList.Add(ITboObj);
            }
            return ITboList;
        }

        public List<ITbo> Load_HousingOthersDetails(int ID, string PropTyp, string pernr, string rtype)
        {
            ITdalDataContext objITDataContext = new ITdalDataContext();
            List<ITbo> ITboList = new List<ITbo>();
            foreach (var vRow in objITDataContext.usp_IT_GetHousingOthersHistory(ID, PropTyp, pernr, rtype))
            {
                ITbo ITboObj = new ITbo();
                //ID,PROPTYP,RENTO,INT24,LETVL,REP24,OTH24,TDSOT,
                // BSPFT,CPGLN,CPGLS,CPGNS,CPGSS,DVDND,INTRS,UNSPI,TDSAT, EMPCOMMENTS,EMPCOMMENTS2,STATUS  
                ITboObj.ID = vRow.ID;
                ITboObj.PROPTYP = vRow.PROPTYP;
                ITboObj.RENTO = vRow.RENTO.ToString();
                ITboObj.INT24 = vRow.INT24;
                ITboObj.LETVL = vRow.LETVL;
                ITboObj.REP24 = vRow.REP24;
                ITboObj.OTH24 = vRow.OTH24;
                ITboObj.TDSOT = vRow.TDSOT;

                ITboObj.BSPFT = vRow.BSPFT;
                ITboObj.CPGLN = vRow.CPGLN;
                ITboObj.CPGLS = vRow.CPGLS;
                ITboObj.CPGNS = vRow.CPGNS;
                ITboObj.CPGSS = vRow.CPGSS;
                ITboObj.DVDND = vRow.DVDND;
                ITboObj.INTRS = vRow.INTRS;
                ITboObj.UNSPI = vRow.UNSPI;
                ITboObj.TDSAT = vRow.TDSAT;

                ITboObj.EMPCOMMENTS = vRow.EMPCOMMENTS;
                ITboObj.EMPCOMMENTS2 = vRow.EMPCOMMENTS2;
                ITboList.Add(ITboObj);
            }
            return ITboList;
        }


        public List<ITbo> Load_IT_Locking(string perner)
        {
            ITdalDataContext objDataContext = new ITdalDataContext();
            List<ITbo> ITbo = new List<ITbo>();
            foreach (var vRow in objDataContext.usp_IT_LoadLocking(perner))
            {
                ITbo ITboObj = new ITbo();
                ITboObj.PERNR = vRow.PERNR;
                ITboObj.ENAME = vRow.ENAME;
                ITboObj.ITLOCK = vRow.LOCK;
                ITboObj.CA80 = vRow.Sec80 == null ? false : vRow.Sec80;
                ITboObj.CA80C = vRow.Sec80C == null ? false : vRow.Sec80C;
                ITbo.Add(ITboObj);
            }
            return ITbo;
        }

        public List<ITbo> Load_IT_HistoryForAppRej(int flag)
        {
            ITdalDataContext objITDataContext = new ITdalDataContext();
            List<ITbo> ITboList = new List<ITbo>();
            foreach (var vRow in objITDataContext.usp_IT_HeaderForApproval(flag))
            {
                ITbo ITboObj = new ITbo();

                ITboObj.ID = vRow.ID;
                ITboObj.CREATED_ON = vRow.CREATED_ON;
                ITboObj.ENDDA = vRow.ENDDA;
                ITboObj.BEGDA = vRow.BEGDA;
                ITboObj.CREATED_BY = vRow.CREATED_BY;
                ITboObj.ENAME = vRow.ENAME;
                ITboObj.STATUS = vRow.STATUS;
                ITboObj.CONACTPROP = vRow.CONACTPROP;
                ITboObj.ITTYP = vRow.PROPTYP.Trim();

                ITboList.Add(ITboObj);
            }
            return ITboList;
        }



        public void AppRej_IT(ITbo ITObjbo, int flag, ref bool? Status)
        {
            try
            {
                ITdalDataContext objITDataContext = new ITdalDataContext();

                objITDataContext.usp_IT_AppRej(ITObjbo.ID, ITObjbo.REMARKS, ITObjbo.STATUS, flag, ref Status);
                objITDataContext.Dispose();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        public List<ITbo> Load_IT_HistoryForAdmin(int flag)
        {
            ITdalDataContext objITDataContext = new ITdalDataContext();
            List<ITbo> ITboList = new List<ITbo>();
            foreach (var vRow in objITDataContext.usp_IT_HeaderForAdmin(flag))
            {
                ITbo ITboObj = new ITbo();

                ITboObj.ID = vRow.ID;
                ITboObj.CREATED_ON = vRow.CREATED_ON;
                ITboObj.ENDDA = vRow.ENDDA;
                ITboObj.BEGDA = vRow.BEGDA;
                ITboObj.CREATED_BY = vRow.CREATED_BY;
                ITboObj.ENAME = vRow.ENAME;
                ITboObj.STATUS = vRow.STATUS;
                ITboObj.CONACTPROP = vRow.CONACTPROP;
                ITboObj.ITTYP = vRow.PROPTYP.Trim();
                ITboObj.APPROVEDON = string.IsNullOrEmpty(vRow.APPROVED_ON.ToString()) ? DateTime.Parse("01-01-0001") : vRow.APPROVED_ON;
                ITboObj.REMARKS = string.IsNullOrEmpty(vRow.REMARKS) ? " - " : vRow.REMARKS;

                ITboList.Add(ITboObj);
            }
            return ITboList;
        }


        public List<ITbo> Load_ParticularITForAdmin(string SelectedType, string textSearch, DateTime FromDate, DateTime ToDate, int flag)
        {
            ITdalDataContext objITDataContext = new ITdalDataContext();
            List<ITbo> ITboList = new List<ITbo>();
            foreach (var vRow in objITDataContext.usp_IT_ParticularITAdmin(SelectedType, textSearch, FromDate, ToDate, flag))
            {
                ITbo ITboObj = new ITbo();

                ITboObj.ID = vRow.ID;
                ITboObj.CREATED_ON = vRow.CREATED_ON;
                ITboObj.ENDDA = vRow.ENDDA;
                ITboObj.BEGDA = vRow.BEGDA;
                ITboObj.CREATED_BY = vRow.CREATED_BY;
                ITboObj.ENAME = vRow.ENAME;
                ITboObj.STATUS = vRow.STATUS;
                ITboObj.CONACTPROP = vRow.CONACTPROP;
                ITboObj.ITTYP = vRow.PROPTYP.Trim();
                ITboObj.APPROVEDON = string.IsNullOrEmpty(vRow.APPROVED_ON.ToString()) ? DateTime.Parse("01-01-0001") : vRow.APPROVED_ON;
                ITboObj.REMARKS = string.IsNullOrEmpty(vRow.REMARKS) ? " - " : vRow.REMARKS;

                ITboList.Add(ITboObj);

            }
            return ITboList;
        }

        public List<ITbo> Load_ParticularITEmp(string SelectedType, string textSearch, DateTime FromDate, DateTime ToDate, int flag, string Pernr)
        {
            ITdalDataContext objITDataContext = new ITdalDataContext();
            List<ITbo> ITboList = new List<ITbo>();
            foreach (var vRow in objITDataContext.usp_IT_ParticularITEmp(SelectedType, textSearch, FromDate, ToDate, flag, Pernr))
            {
                ITbo ITboObj = new ITbo();

                ITboObj.ID = vRow.ID;
                ITboObj.CREATED_ON = vRow.CREATED_ON;
                ITboObj.ENDDA = vRow.ENDDA;
                ITboObj.BEGDA = vRow.BEGDA;
                ITboObj.CREATED_BY = vRow.CREATED_BY;
                ITboObj.ENAME = vRow.ENAME;
                ITboObj.STATUS = vRow.STATUS;
                ITboObj.CONACTPROP = vRow.CONACTPROP;
                ITboObj.ITTYP = vRow.PROPTYP.Trim();
                ITboObj.APPROVEDON = string.IsNullOrEmpty(vRow.APPROVED_ON.ToString()) ? DateTime.Parse("01-01-0001") : vRow.APPROVED_ON;
                ITboObj.REMARKS = string.IsNullOrEmpty(vRow.REMARKS) ? " - " : vRow.REMARKS;

                ITboList.Add(ITboObj);

            }
            return ITboList;
        }


        public List<ITbo> Load_ParticularITForAdminAppRej(string SelectedType, string textSearch, int flag)
        {
            ITdalDataContext objITDataContext = new ITdalDataContext();
            List<ITbo> ITboList = new List<ITbo>();
            foreach (var vRow in objITDataContext.usp_IT_ParticularITAdminForAppRej(SelectedType, textSearch, flag))
            {
                ITbo ITboObj = new ITbo();

                ITboObj.ID = vRow.ID;
                ITboObj.CREATED_ON = vRow.CREATED_ON;
                ITboObj.ENDDA = vRow.ENDDA;
                ITboObj.BEGDA = vRow.BEGDA;
                ITboObj.CREATED_BY = vRow.CREATED_BY;
                ITboObj.ENAME = vRow.ENAME;
                ITboObj.STATUS = vRow.STATUS;
                ITboObj.CONACTPROP = vRow.CONACTPROP;
                ITboObj.ITTYP = vRow.PROPTYP.Trim();
                //ITboObj.APPROVEDON = string.IsNullOrEmpty(vRow.APPROVED_ON.ToString()) ? DateTime.Parse("01-01-0001") : vRow.APPROVED_ON;
                //ITboObj.REMARKS = string.IsNullOrEmpty(vRow.REMARKS) ? " - " : vRow.REMARKS;

                ITboList.Add(ITboObj);

            }
            return ITboList;
        }

        public List<ITbo> Get_IT_Slab_details(int? ID, string PERNR, int flag)
        {
            ITdalDataContext objITDataContext = new ITdalDataContext();
            List<ITbo> ITboList = new List<ITbo>();
            foreach (var vRow in objITDataContext.usp_get_IT_slab(ID, PERNR, flag))
            {
                ITbo ITboObj = new ITbo();



                ITboObj.ID = vRow.ID;
                ITboObj.CREATED_ON = vRow.UPDATED_ON;
                ITboObj.PERNR = vRow.PERNR;
                ITboObj.ITSLAB = vRow.IT_SLAB;
                ITboList.Add(ITboObj);



            }
            return ITboList;
        }



        public void IT_Set_Details(int? ID, string PERNR, int flag, int slab, ref bool? status)
        {
            try
            {
                ITdalDataContext objITDataContext = new ITdalDataContext();
                objITDataContext.usp_set_IT_slab(ID, PERNR, slab, 1, ref status);
                objITDataContext.Dispose();
            }
            catch (Exception Ex)
            { throw Ex; }
        }

        public void Create_ITHousingOthers_cont(ITbo ITObjbo, int flg, ref bool? Sts)
        {
            try
            {
                ITdalDataContext objITDataContext = new ITdalDataContext();



                objITDataContext.usp_IT_Submit_HousingOthers_CONT(ITObjbo.ID, ITObjbo.LENDNAME, ITObjbo.LENDPAN, ITObjbo.LENDADD, ITObjbo.ADDPROPTY, ITObjbo.State, ITObjbo.City,
                   ITObjbo.PUPSHSLN, ITObjbo.LNSAC_DATE, ITObjbo.STMPCHR_DATE, ITObjbo.PERCNT, flg, ref Sts);



                objITDataContext.Dispose();
            }
            catch (Exception Ex)
            { throw Ex; }



        }



        public List<ITbo> Load_PreEpInCm_cont(int ID, int flg)
        {
            ITdalDataContext objITDataContext = new ITdalDataContext();
            List<ITbo> ITboList = new List<ITbo>();
            foreach (var vRow in objITDataContext.usp_IT_get_HousingOthers_CONT(ID, flg))
            {
                ITbo ITboObj = new ITbo();
                ITboObj.ID = vRow.ID;
                ITboObj.LENDNAME = vRow.LEDNAME;
                ITboObj.LENDPAN = vRow.LEDPAN;
                ITboObj.LENDADD = vRow.LEDADD;
                ITboObj.ADDPROPTY = vRow.AddPRP;
                ITboObj.State = vRow.State;
                ITboObj.City = vRow.City;
                ITboObj.PUPSHSLN = vRow.PUPS;
                ITboObj.LNSAC_DATE = vRow.LNSAC_DT;
                ITboObj.STMPCHR_DATE = vRow.STMP_DT;
                ITboObj.RID = vRow.RID;
                ITboObj.NAME = vRow.BORNAME;
                ITboObj.PERCNT = vRow.PERCNT;
                ITboList.Add(ITboObj);
            }
            return ITboList;
        }

        public void Create_ITPreEmptIncome(ITbo ITObjbo, ref int? HID, ref bool? Sts)
        {
            try
            {
                ITdalDataContext objITDataContext = new ITdalDataContext();



                objITDataContext.usp_IT_PreEmplmnt_income(ITObjbo.ID,
                    ITObjbo.PERNR
                   , ITObjbo.BEGDA
                   , ITObjbo.ENDDA
                   , ITObjbo.PreEmpr
                   , ITObjbo.PreEmprPAN
                   , ITObjbo.PreEmprTAN
                   , ITObjbo.GRSAL
                   , ITObjbo.VPRQS
                   , ITObjbo.PRSAL
                   , ITObjbo.EXS10
                   , ITObjbo.PRTAX
                   , ITObjbo.PRFND
                   , ITObjbo.TXDED
                   , ITObjbo.SCDED
                   , ITObjbo.ECDED
                   , ITObjbo.MODIFIED_BY
                   , ITObjbo.Flag
                   , ref HID, ref Sts);



                objITDataContext.Dispose();
            }
            catch (Exception Ex)
            { throw Ex; }



        }



        public List<ITbo> Load_PreEpInCm(string PERNR, int flag, int ID)
        {
            ITdalDataContext objITDataContext = new ITdalDataContext();
            List<ITbo> ITboList = new List<ITbo>();
            foreach (var vRow in objITDataContext.usp_IT_Get_PreEmpHistry(PERNR, flag, ID))
            {
                ITbo ITboObj = new ITbo();
                ITboObj.ID = vRow.ID;
                ITboObj.PERNR = vRow.PERNR;
                ITboObj.PreEmpr = vRow.PrevEmployer;
                ITboObj.PreEmprPAN = vRow.CompanyPAN;
                ITboObj.PreEmprTAN = vRow.CompanyTAN;
                ITboObj.ENDDA = vRow.ENDDA;
                ITboObj.BEGDA = vRow.BEGDA;
                ITboObj.GRSAL = vRow.GRSAL;
                ITboObj.VPRQS = vRow.VPRQS;
                ITboObj.PRSAL = vRow.PRSAL;
                ITboObj.EXS10 = vRow.EXS10;
                ITboObj.PRTAX = vRow.PRTAX;
                ITboObj.PRFND = vRow.PRFND;
                ITboObj.TXDED = vRow.TXDED;
                ITboObj.SCDED = vRow.SCDED;
                ITboObj.ECDED = vRow.ECDED;
                ITboObj.CREATED_ON = vRow.CREATED_ON;
                ITboObj.CREATED_BY = vRow.CREATED_BY;
                ITboObj.MODIFIED_ON = vRow.MODIFIED_ON;
                ITboObj.MODIFIED_BY = vRow.MODIFIED_BY;
                ITboObj.APPROVEDON = string.IsNullOrEmpty(vRow.APPROVED_ON.ToString()) ? DateTime.Parse("01-01-0001") : vRow.APPROVED_ON;
                ITboObj.APPROVED_BY = vRow.APPROVED_BY;
                ITboObj.STATUS = vRow.STATUS;
                ITboObj.REMARKS = string.IsNullOrEmpty(vRow.REMARKS) ? " - " : vRow.REMARKS;
                ITboObj.ENAME = vRow.ENAME;
                ITboList.Add(ITboObj);
            }
            return ITboList;
        }

        public void SubmitAll(ITbo ITObjbo, ref bool? sts, ref string en, ref string em)
        {
            try
            {
                ITdalDataContext objITDataContext = new ITdalDataContext();



                objITDataContext.usp_IT_Submit_ALL(ITObjbo.ID, ITObjbo.PERNR, ITObjbo.Flag, ref sts, ref em, ref en);




                objITDataContext.Dispose();
            }
            catch (Exception Ex)
            { throw Ex; }



        }
    }
}