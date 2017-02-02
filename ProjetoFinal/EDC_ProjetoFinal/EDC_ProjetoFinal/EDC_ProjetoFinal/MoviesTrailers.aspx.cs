﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace EDC_ProjetoFinal
{
    public partial class MoviesTrailers : System.Web.UI.Page
    {
        static readonly string script = "<script language = \"javascript\">\n " + "alert (\"Check your Internet Connection\");\n" + "</script>";

        // Read feed rss and link to the gridview
        protected void Page_Load(object sender, EventArgs e)
        {
            WebClient wc = new WebClient();
            wc.Encoding = System.Text.Encoding.UTF8;
            try
            {
                /* MovieWeb RSS Trailers Feed */
                String xmlData = wc.DownloadString("http://movieweb.com/rss/movie-trailers/");
                XmlDataSource1.Data = xmlData;
                XmlDataSource1.TransformFile = "~/xslt/movies_news.xslt";
                GridView1.DataSource = XmlDataSource1;
                XmlDataSource1.DataBind();
                GridView1.DataBind();
                /* Save to XML - xml Folder */
                saveToXML();
            }
            catch (Exception)
            {
                ClientScript.RegisterStartupScript(script.GetType(), "Error", script);
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Image img = (Image)e.Row.FindControl("image2");
            Label label = (Label)e.Row.FindControl("Label2");
            if (img == null)
            {
                return;
            }
            img.ImageUrl = label.Text;
        }

        protected void saveToXML()
        {
            try
            {
                XmlDocument document = new XmlDocument();
                document.Load("http://movieweb.com/rss/movie-trailers/");
                File.WriteAllText(Server.MapPath("xml/MoviesTrailers.xml"), document.InnerXml);
                System.Diagnostics.Debug.Write("Movies Trailers XML generated successfully!! ");
            }
            catch (Exception)
            {
                System.Diagnostics.Debug.Write("XML not generated!! ");
            }
        }

    }
}