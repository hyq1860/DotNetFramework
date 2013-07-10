<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Knockout_Binding_01.aspx.cs" Inherits="DotNet.SpiderApplication.WebSite.KnockoutDemo.Knockout_Binding_01" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script type="text/javascript" src="../script/jquery-1.8.2.js"></script>
    <script type="text/javascript" src="../script/knockout-2.2.0.debug.js"></script>
    <script type="text/javascript">
        var viewModel;
        //var viewModel2;
        $(function () {
            //
            viewModel = {
                shouldShowMessage: ko.observable(true) // Message initially visible
            };
            ko.applyBindings(viewModel);

            //
//            viewModel2 = {
//                myValues: ko.observableArray([]) // Initially empty, so message hidden
//            };
//            ko.applyBindings(viewModel2);
        });

        function ModifyViewModel(flage) {
            viewModel.shouldShowMessage(flage); // ... now it's hidden
        }

    </script>
</head>
<body>
     1.visible 绑定
    <div data-bind="visible: shouldShowMessage">
    显示的内容1
    </div>

    <input type="button" value="显示" onclick="ModifyViewModel(true)"/>
    <input type="button" value="不显示" onclick="ModifyViewModel(false)"/>

    2.使用函数或者表达式来控制元素的可见性



    <input type="button" value="给数组push元素" onclick="javascript:viewModel2.myValues.push('some value'); "/>

    <div id="aaa" data-bind="visible: myValues().length > 0">
    You will see this message only when 'myValues' has at least one member.
   </div>
 
     <script type="text/javascript">
         var viewModel2 = {
             myValues: ko.observableArray([]) // Initially empty, so message hidden
         };
         ko.applyBindings(viewModel2,$("#aaa"));
         viewModel2.myValues.push("some value"); // Now visible
</script>
</body>
</html>
