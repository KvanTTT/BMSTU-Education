<%@ Page Language="C#" %>
<%@ Import Namespace="System.Xml.XPath" %>
<%@ Import Namespace="System.Xml.Xsl" %>

<%
  XPathDocument doc =
      new XPathDocument (Server.MapPath ("Russian users.xml"));
  XslTransform xsl = new XslTransform ();
  xsl.Load (Server.MapPath ("transform.xsl"));
  xsl.Transform (doc, null, Response.OutputStream);
%>