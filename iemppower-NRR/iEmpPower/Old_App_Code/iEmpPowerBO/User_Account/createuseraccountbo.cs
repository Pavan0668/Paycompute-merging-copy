using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for createuseraccountbo
/// </summary>
public class createuseraccountbo
{
	public createuseraccountbo()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    private Guid _guidUserId = Guid.Empty;
    private string _strUserName = string.Empty;
    public Guid USERID
    {
        get
        {
            return _guidUserId;
        }
        set
        {
            _guidUserId = value;
        }
    }
    public string USERNAME
    {
        get
        {
            return _strUserName;
        }
        set
        {
            _strUserName = value;
        }
    }
}