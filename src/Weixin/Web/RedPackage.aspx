<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RedPackage.aspx.cs" Inherits="Weixin.Web.RedPackage" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">

<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" charset="utf-8" />
    <meta name="viewport" content="initial-scale=1, maximum-scale=1, minimum-scale=1, user-scalable=no" />
    <title>洛诗琳双11红包大放送啦！</title>    
    <link href="http://wxstatic.luxlead.com/Css/style.css" rel="stylesheet" />
    <script src="http://wxstatic.luxlead.com/JS/zepto.js"></script>
    <script src="http://res.wx.qq.com/open/js/jweixin-1.0.0.js"></script>
    <script src="http://wxstatic.luxlead.com/JS/jquery.min.js"></script>
    <script src="http://wxstatic.luxlead.com/JS/jweixin-1.0.0.js"></script>

    <script type="text/javascript" >
        function CheckClick() {
            if (Page_ClientValidate()) {
                //防止重复点击
                $("#btnGetRedPackage").val("正在提交");
                $("#btnGetRedPackage").attr("disabled","disabled");
                return true;
            }
            else {
                return false;
            }                
        }
        function ChangePhoneNo() {
            $("#btnGetRedPackage").removeAttr("disabled");
        }
    </script>
</head>

<body>
    <form id="form1" runat="server" >
        <div style="text-align:center;">
            <div id="div_bg" style="display:block">
                <img src="http://wxstatic.luxlead.com/Images/RedPackage/领取前背景.jpg" style="height:100%;width:100%;"/>
                <asp:Label ID="lbsPhone" runat="server" style="display:block;height:50px;margin-top:-130%;" Font-Size="X-Large" ForeColor="White" >请输入下单时填写的手机号</asp:Label>
                <asp:TextBox ID="txtPhoneNo" runat="server" style="height:30px;width:40%;" onchange="ChangePhoneNo();" ></asp:TextBox>                               
                <asp:Button ID="btnGetRedPackage" runat="server" Text="领红包" style="height:33px;width:20%;" UseSubmitBehavior="false" OnClientClick="CheckClick();" OnClick="btnGetRedPackage_Click" BackColor="Black" ForeColor="White" BorderWidth="0px" />
                <asp:RequiredFieldValidator ID="rfvGetRedPackage" runat="server" ControlToValidate="txtPhoneNo" ErrorMessage="请输入手机号！" style="display:block;margin-top:10px;" ForeColor="White"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="revGetRedPackage" runat="server" ControlToValidate="txtPhoneNo" ValidationExpression="^1\d{10}$" ErrorMessage="请输入合法的手机号码!" ForeColor="White"></asp:RegularExpressionValidator>
                <asp:Label ID="lblMsg" runat="server" style="display:block;margin-left:5%;margin-right:5%;text-align:center;" Font-Size="Large" ForeColor="White" Text=""></asp:Label>
            </div>            
        </div>
    </form>
</body>
</html>
