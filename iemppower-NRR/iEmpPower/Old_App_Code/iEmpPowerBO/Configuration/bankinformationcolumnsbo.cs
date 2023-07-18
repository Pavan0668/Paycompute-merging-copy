using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for bankinformationcolumnsbo
/// </summary>
public class bankinformationcolumnsbo
{
	public bankinformationcolumnsbo()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    private string strPersonalNo = "PERNR"; //Employer ID Or Personal number
    private string strSubtype = "SUBTY";//Subtype
    private string strRecipientText = "EMFTX";//Recipient text
    private string strCity = "BKORT";//City
    private string strPostalCode = "BKPLZ";//Postal code
    private string strCountry = "BANKS";//Bank country key
    private string stBankKey = "BANKL";//Bank Key
    private string strBankAccountNo = "BANKN";//Bank account number
    private string strPaymentMethod = "ZLSCH";//Payment method
    private string strBankTransfers = "ZWECK";//Purpose of bank transfers
    private string strCurrencyKey = "WAERS";//Currency Key
    public string WAERS
    {
        get { return strCurrencyKey; }
    }
    public string ZWECK
    {
        get { return strBankTransfers; }
    }
    public string PERNR
    {
        get { return strPersonalNo; }
    }
    public string SUBTY
    {
        get { return strSubtype; }
    }
    public string EMFTX
    {
        get { return strRecipientText; }
    }
    public string BANKS
    {
        get { return strCountry; }
    }
    public string BANKL
    {
        get { return stBankKey; }
    }
    public string BANKN
    {
        get { return strBankAccountNo; }
    }
    public string ZLSCH
    {
        get { return strPaymentMethod; }
    }
    public string BKORT
    {
        get { return strCity; }
    }
    public string BKPLZ
    {
        get { return strPostalCode; }
    }
}