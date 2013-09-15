//初始化右侧导航菜单
function InitLeftMenu() {
    $("#nav").accordion({ animate: false });
    $.each(_menus, function(i, n) {
		var menulist ='';
		menulist +='<ul>';
    	$.each(n.menus, function(j, o) {
    	    menulist += '<li><div><a ref="' + o.menuid + '" isIframe="' + o.isIframe + '" href="#" rel="' + o.url + '" ><span class="icon ' + o.icon + '" >&nbsp;</span><span class="nav">' + o.menuname + '</span></a></div></li> ';
    	});
		menulist += '</ul>';

		$('#nav').accordion('add', {
            title: n.menuname,
            content: menulist,
            iconCls: 'icon ' + n.icon
        });

    });

    $('.easyui-accordion li a').click(function () {
        var tabTitle = $(this).children('.nav').text();

        var url = $(this).attr("rel");
        var menuid = $(this).attr("ref");
        var icon = getIcon(menuid, icon);
        var isIframe = $(this).attr("isIframe");
        addTab(tabTitle, url, icon, isIframe);
        $('.easyui-accordion li div').removeClass("selected");
        $(this).parent().addClass("selected");
    }).hover(function () {
        $(this).parent().addClass("hover");
    }, function () {
        $(this).parent().removeClass("hover");
    });

	//选中第一个
	var panels = $('#nav').accordion('panels');
	var t = panels[0].panel('options').title;
    $('#nav').accordion('select', t);
}
//获取左侧导航的图标
function getIcon(menuid){
	var icon = 'icon ';
	$.each(_menus, function(i, n) {
		$.each(n.menus, function(j, o) {
			if (o.menuid == menuid) {
				icon += o.icon;
			}
		});
	});

	return icon;
}

function addTab(subtitle, url, icon, isIframe) {
    var tabCount = $('#tabs').tabs('tabs').length; // 获取当前打开窗口总数量
    var hasTab = $('#tabs').tabs('exists', subtitle); //根据名称判断窗口是否已打开
    if (tabCount > 8 && !hasTab) {
        var msg = '您当前打开了太多的页面，如果继续打开，会造成程序运行缓慢，无法流畅操作！';
        $.messager.alert("系统提示", msg);
        return false;
    }
    if (!hasTab) {
        if (isIframe=="true") {
            $('#tabs').tabs('add', {
                title: subtitle,
                content: createFrame(url, subtitle),
                closable: true,
                cache: false,
                icon: icon
            });
        } else {
            $('#tabs').tabs('add', {
                title: subtitle,
                href: url,
                closable: true,
                cache:false,
                icon: icon
            });
        }
    }
    else{
		$('#tabs').tabs('select',subtitle);
        $('#mm-tabupdate').click();
}
    
	tabClose();
}

function createFrame(url, title)
{
    var s = '<iframe scrolling="auto" frameborder="0" id="' + title + '"  src="' + url + '" style="width:100%;height:100%;"></iframe>';
	return s;
}

function tabClose() {
    /*双击关闭TAB选项卡*/
    $(".tabs-inner").dblclick(function() {
        var subtitle = $(this).children(".tabs-closable").text();
        $('#tabs').tabs('close', subtitle);
    });
}


//弹出信息窗口 title:标题 msgString:提示信息 msgType:信息类型 [error,info,question,warning]
function msgShow(title, msgString, msgType) {
	$.messager.alert(title, msgString, msgType);
}

function handing(result) {
    if (result == "-100") {
        $.messager.alert("操作提示", "操作超时，请重新登陆！", "info", function() {
            parent.location.href = "../../login.aspx";
        });
    }
    else if(result=="-101") {
        $(function() {
            $.messager.alert("操作提示", "没有相关权限，请联系管理员！", "info");
        });
    }
}

//时间
function Clock() {
    var date = new Date();
    this.year = date.getFullYear();
    this.month = date.getMonth() + 1;
    this.date = date.getDate();
    this.day = new Array("星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六")[date.getDay()];
    this.hour = date.getHours() < 10 ? "0" + date.getHours() : date.getHours();
    this.minute = date.getMinutes() < 10 ? "0" + date.getMinutes() : date.getMinutes();
    this.second = date.getSeconds() < 10 ? "0" + date.getSeconds() : date.getSeconds();

    this.toString = function() {
        return  this.year + "年" + this.month + "月" + this.date + "日 " + this.hour + ":" + this.minute + ":" + this.second + " " + this.day;
    };

    this.toSimpleDate = function() {
        return this.year + "-" + this.month + "-" + this.date;
    };

    this.toDetailDate = function() {
        return this.year + "-" + this.month + "-" + this.date + " " + this.hour + ":" + this.minute + ":" + this.second;
    };

    this.display = function(ele) {
        var clock = new Clock();
        ele.innerHTML = clock.toString();
        window.setTimeout(function() { clock.display(ele); }, 1000);
    };
}



var sy = $.extend({}, sy); /* 全局对象 */

sy.changeTheme = function (themeName) {/* 更换主题 */
    var $easyuiTheme = $('#easyuiTheme');
    var url = $easyuiTheme.attr('href');
    var href = url.substring(0, url.indexOf('themes')) + 'themes/' + themeName + '/easyui.css';
    $easyuiTheme.attr('href', href);

    var $iframe = $('iframe');
    if ($iframe.length > 0) {
        for (var i = 0; i < $iframe.length; i++) {
            var ifr = $iframe[i];
            $(ifr).contents().find('#easyuiTheme').attr('href', href);
        }
    }

//    $.cookie('easyuiThemeName', themeName, {
//        expires: 7
//    });
    createCookie("easyuiThemeName", themeName,1000);
};
if (readCookie('easyuiThemeName')) {
    sy.changeTheme(readCookie('easyuiThemeName'));
}

function createCookie(name, value, days) {
    if (days) {
        var date = new Date();
        date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
        var expires = "; expires=" + date.toGMTString();
    }
    else var expires = "";
    document.cookie = name + "=" + value + expires + "; path=/";
}
function readCookie(name) {
    var nameEQ = name + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') c = c.substring(1, c.length);
        if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length, c.length);
    }
    return null;
}
function eraseCookie(name) {
    createCookie(name, "", -1);
}

function loadThemes() {
    if (readCookie('easyuiThemeName')) {
        sy.changeTheme(readCookie('easyuiThemeName'));
    }
}