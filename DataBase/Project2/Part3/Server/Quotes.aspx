<%@ Page Language="C#" %>
<%@ Import Namespace="System.Xml.XPath" %>
<%@ Import Namespace="System.Xml.Xsl" %>

<%
  XPathDocument doc =
      new XPathDocument (Server.MapPath ("Quotes.xml"));
  XslTransform xsl = new XslTransform ();
  xsl.Load (Server.MapPath ("Quotes.xsl"));
  xsl.Transform (doc, null, Response.OutputStream);
%>