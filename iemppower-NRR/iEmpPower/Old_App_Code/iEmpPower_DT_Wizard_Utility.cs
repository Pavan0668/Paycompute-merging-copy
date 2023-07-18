using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
/// <summary>
/// Summary description for iEmpPower_DT_Wizard_Utility
/// </summary>
public class iEmpPower_DT_Wizard_Utility
{
    public iEmpPower_DT_Wizard_Utility()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public void LogError(string strFileName, string strErrMessage)
    {

        if (File.Exists(strFileName))
        {
            StreamWriter s = new StreamWriter(strFileName, true);
            s.WriteLine("<br>");
            s.WriteLine("<html>");
            s.WriteLine("<head>");
            s.WriteLine("<style type=\"text/css\">"
            + " body, html {font: normal normal normal 12px/22px 'Segoe UI','Helvetica Neue','Lucida Grande',Verdana,sans-serif; margin:0 30px;padding:0 10px;}"
            + ".R {color:red;} .G {color:green;}" 
            +"</style>");

            s.WriteLine("</head>");
            s.WriteLine(DateTime.Now.ToString() + "<br>");
            s.WriteLine(strErrMessage);
            s.WriteLine("</BODY>");
            s.WriteLine("</html>");
            s.Flush();
            s.Close();
        }
        else
        {
            using (StreamWriter s = File.CreateText(strFileName))
            {
                //StreamWriter s = new StreamWriter(strFileName, true);
                s.WriteLine("<br>");
                s.WriteLine("<html>");
                s.WriteLine("<head>");
                s.WriteLine("<style type=\"text/css\">"
                + " body, html {font: normal normal normal 11px/22px 'Segoe UI','Helvetica Neue','Lucida Grande',Verdana,sans-serif; margin:0;padding:0;}</style>");
                s.WriteLine("</head>");
                s.WriteLine(DateTime.Now.ToString() + "<br>");
                s.WriteLine("<br>");
                s.WriteLine(strErrMessage);
                s.WriteLine("</BODY>");
                s.WriteLine("</html>");
                s.Flush();
                s.Close();
            }
        }
    }
}