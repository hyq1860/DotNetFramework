<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index4.aspx.cs" Inherits="DotNet.Web.BackstageManagement.Index4" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="js/jquery-1.7.2.min.js"></script>
    <script>

        $(document).ready(function () {
            initLayout();
            $(window).resize(function () {
                initLayout();
            });
        });


        function initLayout() {
            $('#right').width(document.documentElement.clientWidth - $("#left").width() - 2);
            var h = document.documentElement.clientHeight - $("#header").height() - 30;
            $('#left').height(h);
            $('#right').height(h - 20);
        }

    </script>
</head>


<body>
<div id="header">gg</div>  
<div id="left">gg</div>  
<div id="right">gg</div>  
</body>
</html>
