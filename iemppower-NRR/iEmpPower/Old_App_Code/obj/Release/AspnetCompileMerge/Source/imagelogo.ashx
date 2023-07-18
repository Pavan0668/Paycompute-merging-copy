<%@ WebHandler Language="C#" Class="imagelogo" %>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.SessionState;
using iEmpPower.Old_App_Code.iEmpPowerDAL.Configuration;
using System.Data;

public class imagelogo : IHttpHandler,IRequiresSessionState {

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "image/jpg";
        Stream strm = ShowImageLogo();
        byte[] buffer = new byte[4096];

        int byteSeq = strm.Read(buffer, 0, 4096);

        while (byteSeq > 0)
        {

            context.Response.OutputStream.Write(buffer, 0, byteSeq);

            byteSeq = strm.Read(buffer, 0, 4096);

        }
    }
    public Stream ShowImageLogo()
    {
    
        configurationdalDataContext context1 = new configurationdalDataContext();

        var r = (from a in context1.sp_conf_get_logo() select a.img_logo).First();

        return new MemoryStream((byte[])(r.ToArray()));
    }
    public bool IsReusable {
        get {
            return false;
        }
    }

}