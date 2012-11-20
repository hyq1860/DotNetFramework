<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Snippet.aspx.cs" Inherits="DotNet.EnterpriseWebSite.Manage.Snippet" %>

<%--<div id="loading" style="position: absolute; z-index: 1000; top: 0px; left: 0px; width: 100%; height: 100%; background: #DDDDDB; text-align: center; padding-top: 20%;"></div>--%>

<form id="form1" runat="server">
<div>
    
</div>
</form>
<script type="text/javascript">

//    function show() {
//        $("#loading").fadeOut("normal", function () {
//            $(this).remove();
//        });
//    }
//    var delayTime;
    $.parser.onComplete = function() {
//        if (delayTime)
//            clearTimeout(delayTime);
//        delayTime = setTimeout(show, 500);
    };
    $(function () {
        alert("1");
    });
    function init() {
        alert("1");
    }
</script>

