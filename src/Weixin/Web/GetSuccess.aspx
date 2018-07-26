<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GetSuccess.aspx.cs" Inherits="Weixin.Web.GetSuccess" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">

<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" charset="utf-8" />
    <meta name="viewport" content="initial-scale=1, maximum-scale=1, minimum-scale=1, user-scalable=no" />
    <title>LUXLEAD洛诗琳双11红包大放送啦！</title>    
    <link href="http://wxstatic.luxlead.com/Css/style.css" rel="stylesheet" />
    <script src="http://wxstatic.luxlead.com/JS/zepto.js"></script>
    <script src="http://res.wx.qq.com/open/js/jweixin-1.0.0.js"></script>
    <script src="http://apps.bdimg.com/libs/jquery/2.1.1/jquery.min.js"></script>
    <script src="JS/jweixin-1.0.0.js"></script>
</head>

<body>
    <form id="form1" runat="server">
        <div style="text-align:center;">
            <div id="div_bg" style="display:block">
                <img src="http://wxstatic.luxlead.com/Images/RedPackage/领取后背景.jpg" style="height:100%;width:100%;"/>
            <%--<asp:Label ID="lbsSendName" runat="server" style="display:block;height:30px;margin-top:-100%;" Font-Size="XX-Large" ForeColor="White" >LUXLEAD洛诗琳</asp:Label>
                <asp:Label ID="lblSendPackage" runat="server" style="display:block;width:50%;margin-left:25%;margin-top:10px;text-align:center;" Font-Size="small" ForeColor="White">给你发了一个红包</asp:Label>
                <asp:Label ID="lblMsg1" runat="server" style="display:block;margin-left:11%;margin-right:8%;margin-top:50px;margin-bottom:10px;text-align:center;" Font-Size="Large" ForeColor="White">请关闭当前页面，到消息列表中领取！</asp:Label>
                <asp:Label ID="lblMsg2" runat="server" style="display:block;margin-left:10%;margin-right:8%;text-align:center;" Font-Size="small" ForeColor="White">（未关注"Luxlead洛诗琳"的在服务通知中点击领取，已关注的在公众号的消息列表中领取！）</asp:Label>--%>
            </div>            
        </div>
    </form>
</body>
</html>
