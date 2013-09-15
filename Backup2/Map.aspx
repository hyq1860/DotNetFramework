<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Map.aspx.cs" Inherits="DotNet.WebSite.Map" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="Scripts/jquery-1.4.1.min.js"></script>
    <script type="text/javascript" src="Scripts/Map.js"></script>
    <script>
        $(function () {
            $('.imgMap').imageMaps();
        })
    </script>
</head>
<body>
<!--模块展示 begin-->
<div class="modeShow">
    <div id="debug"></div>
    <div class="imgMap mapBox">
        <img src="http://res.mall.10010.com/mall/res/uploader/index/20120719210043-867086368.jpg" name="test"  border="0" usemap="#Map1" width="980" height="450" ref='imageMaps' />
        <map name="Map1">
          <area shape="rect" coords="300,80,500,150" href="mall.10010.com" />
        </map>
      </div>
</div>
<!--模块展示 end—>
</body>
</html>
