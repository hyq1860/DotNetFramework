<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ueditor_demo02.aspx.cs" Inherits="DotNet.EnterpriseWebSite.ueditor_demo.ueditor_demo02" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN"
        "http://www.w3.org/TR/html4/loose.dtd">
<html>
<head>

    <meta http-equiv="Content-Type" content="text/html;charset=utf-8"/>
        <title>http://www.cnblogs.com/greatverve/archive/2011/12/01/baidu-ueditor-plugin.html</title>

        <script type="text/javascript" charset="utf-8" src="../ueditor/editor_config.js"></script>
        <script type="text/javascript" src="../ckfinder/ckfinder.js"></script>
        	<script type="text/javascript">
        	    function SetFileField(fileUrl) {
        	        var html = "<img src=" + fileUrl + " />";
        	        editor.execCommand("inserthtml", html);
        	    }

	</script>
        <!--使用版-->
        <!--
        <script type="text/javascript" charset="utf-8" src="res/ueditor/editor_all.js"></script>
        -->

        <!--开发版-->
        <script type="text/javascript" charset="utf-8" src="../ueditor/editor_api.js">
            paths = [
                'editor.js',
                'core/browser.js',
                'core/utils.js',
                'core/EventBase.js',
                'core/dom/dom.js',
                'core/dom/dtd.js',
                'core/dom/domUtils.js',
                'core/dom/Range.js',
                'core/dom/Selection.js',
                'core/Editor.js',
                'commands/inserthtml.js',
                'commands/image.js',
                'commands/justify.js',
                'commands/font.js',
                'commands/link.js',
                'commands/map.js',
                'commands/iframe.js',
                'commands/removeformat.js',
                'commands/blockquote.js',
                'commands/indent.js',
                'commands/print.js',
                'commands/preview.js',
                'commands/spechars.js',
                'commands/emotion.js',
                'commands/selectall.js',
                'commands/paragraph.js',
                'commands/directionality.js',
                'commands/horizontal.js',
                'commands/time.js',
                'commands/rowspacing.js',
                'commands/cleardoc.js',
                'commands/anchor.js',
                'commands/delete.js',
                'commands/wordcount.js',
                'commands/image.js',
                'plugins/pagebreak/pagebreak.js',
                'plugins/checkimage/checkimage.js',
                'plugins/undo/undo.js',
                'plugins/paste/paste.js',           //粘贴时候的提示依赖了UI
                'plugins/list/list.js',
                'plugins/source/source.js',
                'plugins/shortcutkeys/shortcutkeys.js',
                'plugins/enterkey/enterkey.js',
                'plugins/keystrokes/keystrokes.js',
                'plugins/fiximgclick/fiximgclick.js',
                'plugins/autolink/autolink.js',
                'plugins/autoheight/autoheight.js',
                'plugins/autofloat/autofloat.js',  //依赖UEditor UI,在IE6中，会覆盖掉body的背景图属性
                'plugins/highlight/highlight.js',
                'plugins/serialize/serialize.js',
                'plugins/video/video.js',
                'plugins/table/table.js',
                'plugins/mycard/mycard.js', //自定义插件
                'plugins/contextmenu/contextmenu.js',
                'plugins/pagebreak/pagebreak.js',
                'plugins/basestyle/basestyle.js',
                'plugins/elementpath/elementpath.js',
                'plugins/formatmatch/formatmatch.js',
                'plugins/searchreplace/searchreplace.js',
                'ui/ui.js',
                'ui/uiutils.js',
                'ui/uibase.js',
                'ui/separator.js',
                'ui/mask.js',
                'ui/popup.js',
                'ui/colorpicker.js',
                'ui/tablepicker.js',
                'ui/stateful.js',
                'ui/button.js',
                'ui/splitbutton.js',
                'ui/colorbutton.js',
                'ui/tablebutton.js',
                'ui/toolbar.js',
                'ui/menu.js',
                'ui/combox.js',
                'ui/dialog.js',
                'ui/menubutton.js',
                'ui/datebutton.js',
                'ui/editorui.js',
                'ui/editor.js',
                'ui/multiMenu.js'
            ];
        </script>


        <script type="text/javascript">
            //实现插件的功能代码
            baidu.editor.commands['ckfinder'] = {
                execCommand: function () {
                    var finder = new CKFinder();
                    finder.basePath = '../'; // The path for the installation of CKFinder (default = "/ckfinder/").
                    finder.selectActionFunction = SetFileField;
                    finder.popup();
                    return true;
                }
            };
        </script>

        <link rel="stylesheet" type="text/css" href="../ueditor/themes/default/ueditor.css"/>
        <style>
            .myEditor { width: 800px;}

        </style>
    </head>
    <body>
        <h1>编写百度ueditor编辑器自定义插件</h1>
        <script type="text/plain" id="myEditor" class="myEditor"></script>

        <script type="text/javascript">
            var option = {
                initialContent: 'hello world', //初始化编辑器的内容
                minFrameHeight: 200,            //初始化编辑器最小高度
                iframeCssUrl: '../ueditor/themes/default/iframe.css'
            };
            var editor = new baidu.editor.ui.Editor(option);
            editor.render('myEditor');
        </script>
    </body>
</html>