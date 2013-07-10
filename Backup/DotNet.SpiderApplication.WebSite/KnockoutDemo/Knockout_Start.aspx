<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Knockout_Start.aspx.cs" Inherits="DotNet.SpiderApplication.WebSite.KnockoutDemo.Knockout_Start" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="../script/jquery-1.8.2.js"></script>
    <script type="text/javascript" src="../script/knockout-2.2.0.debug.js"></script>
    <script type="text/javascript">
//        $(function () {
//            var viewModel = {
//                personName: '程序猿码农',
//                personAge: 123
//            };
//            ko.applyBindings(viewModel);
//        })
        var viewModel;
        $(function () {
            viewModel = {
                personName: ko.observable('程序猿码农'),
                personAge: ko.observable(123),
                姓: ko.observable("鸟"),
                名字: ko.observable("叔"),
                性别: ko.observable(1)
            };
            //依赖监控属性 
            //ko.dependentObservable 第二个参数 真是函数里面的this
            viewModel.fullName = ko.dependentObservable(function () {
                return this.姓() + this.名字();
            }, viewModel);

            viewModel.Sex = ko.dependentObservable({
                read: function () {
                    if (this.性别() == 1) {
                        return "男";
                    } else {
                        return "女";
                    }
                },
                owner: viewModel
            });

            ko.applyBindings(viewModel);

            //
            $("#container").text(viewModel.姓());
        });

        function ModifyViewModel() {
            viewModel.姓("搞笑");
            viewModel.名字("的大叔");
            viewModel.性别(0);
        }
    </script>
</head>
<body>
    我的名字叫：<span data-bind="text: personName"></span>
    江南style：我的名字叫 <span data-bind="text: fullName"></span>
    江南style：我的名字叫 <span data-bind="text: Sex"></span>
    <input type="button" value="修改值" onclick="ModifyViewModel()"/>
    
        传统的方式
    <div id="container"></div>
</body>
</html>
