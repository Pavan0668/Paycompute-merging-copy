<%@ Application Language="C#" %>

<script RunAt="server">


    void Application_Start(object sender, EventArgs e)
    {
        // Code that runs on application startup

    }

    void Application_End(object sender, EventArgs e)
    {
        //  Code that runs on application shutdown

    }

    void Application_Error(object sender, EventArgs e)
    {
        // Code that runs when an unhandled error occurs
        try
        {
            Exception Ex = Server.GetLastError();
            HttpContext Con = HttpContext.Current;
            System.Xml.XmlDocument XmlDocObj = new System.Xml.XmlDocument();
            //Loading XML File in memory  
            XmlDocObj.Load(Server.MapPath("~/Temp/XML_ErrorFile.xml"));
            //Select root node which is already defined  
            System.Xml.XmlNode RootNode = XmlDocObj.SelectSingleNode("ErrorData");
            //Creating one child node with tag name Exception  
            System.Xml.XmlNode ErrorNode = RootNode.AppendChild(XmlDocObj.CreateNode(System.Xml.XmlNodeType.Element, "Exception", ""));
            //Adding node title to Exception node 
            ErrorNode.AppendChild(XmlDocObj.CreateNode(System.Xml.XmlNodeType.Element, "DateTime", "")).InnerText = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss");
            ErrorNode.AppendChild(XmlDocObj.CreateNode(System.Xml.XmlNodeType.Element, "ErrorCode", "")).InnerText = ((HttpException)(Ex)).GetHttpCode().ToString();
            ErrorNode.AppendChild(XmlDocObj.CreateNode(System.Xml.XmlNodeType.Element, "ExceptionType", "")).InnerText = Ex.GetType().ToString();
            ErrorNode.AppendChild(XmlDocObj.CreateNode(System.Xml.XmlNodeType.Element, "PageName", "")).InnerText = Con.Request.Url.ToString();
            ErrorNode.AppendChild(XmlDocObj.CreateNode(System.Xml.XmlNodeType.Element, "Description", "")).InnerText = Ex.Message;
            ErrorNode.AppendChild(XmlDocObj.CreateNode(System.Xml.XmlNodeType.Element, "InnerExp", "")).InnerText = Ex.InnerException.Message;
            ErrorNode.AppendChild(XmlDocObj.CreateNode(System.Xml.XmlNodeType.Element, "Source", "")).InnerText = Ex.TargetSite.ToString();
            //After adding node, saving XML_ErrorFile.xml back to the server  
            XmlDocObj.Save(Server.MapPath("~/Temp/XML_ErrorFile.xml"));
        }
        catch (Exception Ex)
        {
            string s = Ex.Message;
            //Response.Redirect("~/sessionout.aspx", false);
            Response.Redirect("~/Account/Login.aspx", false);
        }

    }

    void Session_Start(object sender, EventArgs e)
    {
        // Code that runs when a new session is started


    }

    void Session_End(object sender, EventArgs e)
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }
    private void Application_BeginRequest(Object source, EventArgs e)
    {
        string[] languages = HttpContext.Current.Request.UserLanguages;
        if (languages != null)
        {
            if (languages[0].ToLower() != null && languages[0].ToLower() != "")
            {
                System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(languages[0].ToLower());
                System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture(languages[0].ToLower());

            }
        }
    }


       
</script>
