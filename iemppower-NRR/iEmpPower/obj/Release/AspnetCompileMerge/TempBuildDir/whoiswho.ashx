<%@ WebHandler Language="C#" Class="whoiswho" %>

using System;
using System.Web;
using System.IO;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

public class whoiswho : IHttpHandler
{
    public void ProcessRequest(HttpContext context)
    {
        try
        {
            context.Response.ContentType = "image/jpeg";
            context.Response.Cache.SetCacheability(HttpCacheability.Server);
            context.Response.BufferOutput = false;
            //  Setup the PhotoID Parameter 
            Stream stream = null;
            string userName = context.Request.QueryString["imagepath"];
            if (userName != "")
            {
                System.Drawing.Image imgPhoto = System.Drawing.Image.FromFile(userName);
                if (userName != null && userName != "")
                {
                    stream = new MemoryStream((byte[])GetUserPic(imgPhoto));
                    //context.Response.BinaryWrite((byte[])imgPhoto);
                    //if (stream == null)
                    //{
                    //    stream = ourUsersData.GetUserPic("defaultPicture");
                    //}

                }
                // Write image stream to the response stream 
                byte[] buffer = new byte[4096];

                int byteSeq = stream.Read(buffer, 0, 4096);

                while (byteSeq > 0)
                {
                    context.Response.OutputStream.Write(buffer, 0, byteSeq);
                    byteSeq = stream.Read(buffer, 0, 4096);
                }
            }
        }
        catch (Exception Ex)
        { string a = Ex.Message; }

    }
    public Byte[] GetUserPic(System.Drawing.Image uu)
    {
        MemoryStream ms = new MemoryStream();
        uu.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
        return ms.ToArray();

    }
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}