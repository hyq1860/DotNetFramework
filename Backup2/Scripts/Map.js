(function ($) {
    jQuery.fn.imageMaps = function (setting) {
        var $container = this;
        if ($container.length == 0) return false;
        $container.each(function () {
            var container = $(this);
            var $images = container.find('img[ref=imageMaps]');
            $images.wrap('<div class="image-maps-conrainer image-maps-conrainerEdit" style="position:relative;"></div>').css('border', '1px solid #ccc');
            $images.each(function () {
                var _img_conrainer = $(this).parent();
                _img_conrainer.append('<div class="button-conrainer"><a href="javascript:void(0)" class="addHot">添加热点</a><a href="javascript:void(0)" class="addImg">上传图片</a><a class="delSub delMode" href="javascript:void(0)">删除×</a></div>').append('<div class="link-conrainer"><ul></ul><div class="clr"></div></div><div class="clr"></div><span class="numFloor">模块-1</span>').append($.browser.msie ? $('<div class="position-conrainer" style="position:absolute"></div>').css({
                    background: '#fff',
                    opacity: 0
                }) : '<div class="position-conrainer" style="position:absolute"></div>');
                var _img_offset = $(this).offset();
                var _img_conrainer_offset = _img_conrainer.offset();
                _img_conrainer.find('.position-conrainer').css({
                    top: _img_offset.top - _img_conrainer_offset.top,
                    left: _img_offset.left - _img_conrainer_offset.left,
                    width: $(this).width(),
                    height: $(this).height(),
                    border: '1px solid transparent'
                });
                var map_name = $(this).attr('usemap').replace('#', '');
                if (map_name != '') {
                    var index = 1;
                    var _link_conrainer = _img_conrainer.find('.link-conrainer ul');
                    var _position_conrainer = _img_conrainer.find('.position-conrainer');
                    var image_param = $(this).attr('name') == '' ? '' : '[' + $(this).attr('name') + ']';
                    container.find('map[name=' + map_name + ']').find('area[shape=rect]').each(function () {
                        var coords = $(this).attr('coords');
                        $(this).attr('ref', "1");
                        _link_conrainer.append('<li ref="' + index + '" class="map-link"><span class="link-number-text">热点' + index + '</span>: <input type="text" size="60" name="link' + index + '" class="linkHref" value="' + $(this).attr('href') + '" /><input type="hidden" class="rect-value" name="rect' + index + '" value="' + coords + '" /></li>');
                        coords = coords.split(',');
                        _position_conrainer.append('<div ref="' + index + '" class="map-position" style="left:' + coords[0] + 'px;top:' + coords[1] + 'px;width:' + (coords[2] - coords[0]) + 'px;height:' + (coords[3] - coords[1]) + 'px;"><div class="map-position-bg"></div><span class="link-number-text">热点' + index + '</span><span class="delete">X</span><span class="resize"></span></div>');
                        index++;
                    });
                }
            });

        });
        $container.find('.button-conrainer .addHot').live("click", function () {
            var _link_conrainer = $(this).parent().parent().find('.link-conrainer ul');
            var _position_conrainer = $(this).parent().parent().find('.position-conrainer');
            var index = _link_conrainer.find('.map-link').length + 1;
            var _coordsMap = $(this).parent().parent().next('map');
            var image = $(this).parent().parent().find('img[ref=imageMaps]').attr('name');
            image = (image == '' ? '' : '[' + image + ']');
            _link_conrainer.append('<li ref="' + index + '" class="map-link"><span class="link-number-text">热点' + index + '</span>: <input type="text" size="60" name="link' + index + '" class="linkHref" value="" /><input type="hidden" class="rect-value" name="rect' + index + '" value="300,80,500,150" /></li>');
            _position_conrainer.append('<div ref="' + index + '" class="map-position" style="left:300px;top:80px;width:200px;height:70px;"><div class="map-position-bg"></div><span class="link-number-text">热点' + index + '</span><span class="delete">X</span><span class="resize"></span></div>');
            var coords = _link_conrainer.find('input[name=rect' + index + ']').val();
            _coordsMap.append('<area ref="' + index + '" href="" coords="' + coords + '" shape="rect">');
            $("input[name='link" + index + "']").val("请输入本热点对应的链接地址");
            bind_map_event();
            define_css();
            //添加map热区
        });
        //修改链接地址
        $(".linkHref").live("blur", function () {
            var valueHref = $(this).val();
            var thisRef = $(this).parent().attr('ref');
            var appArea = $(this).parents(".link-conrainer").parent().next('map');
            $(this).val(valueHref);
            appArea.find('area[ref=' + thisRef + ']').attr("href", valueHref);

        });
        //绑定map事件
        function bind_map_event() {
            $('.position-conrainer .map-position .map-position-bg').each(function () {
                var map_position_bg = $(this);
                var conrainer = $(this).parent().parent();
                map_position_bg.unbind('mousedown').mousedown(function (event) {
                    map_position_bg.data('mousedown', true);
                    map_position_bg.data('pageX', event.pageX);
                    map_position_bg.data('pageY', event.pageY);
                    map_position_bg.css('cursor', 'move');
                    return false;
                }).unbind('mouseup').mouseup(function (event) {
                    map_position_bg.data('mousedown', false);
                    map_position_bg.css('cursor', 'default');
                    return false;
                });
                conrainer.mousemove(function (event) {
                    if (!map_position_bg.data('mousedown')) return false;
                    var dx = event.pageX - map_position_bg.data('pageX');
                    var dy = event.pageY - map_position_bg.data('pageY');
                    if ((dx == 0) && (dy == 0)) {
                        return false;
                    }
                    var map_position = map_position_bg.parent();
                    var p = map_position.position();
                    var left = p.left + dx;
                    if (left < 0) left = 0;
                    var top = p.top + dy;
                    if (top < 0) top = 0;
                    var bottom = top + map_position.height();
                    if (bottom > conrainer.height()) {
                        top = top - (bottom - conrainer.height());
                    }
                    var right = left + map_position.width();
                    if (right > conrainer.width()) {
                        left = left - (right - conrainer.width());
                    }
                    map_position.css({
                        left: left,
                        top: top
                    });
                    map_position_bg.data('pageX', event.pageX);
                    map_position_bg.data('pageY', event.pageY);

                    bottom = top + map_position.height();
                    right = left + map_position.width();
                    var newArea = new Array(left, top, right, bottom).join(',');
                    var mapApp = conrainer.parent().next('map');
                    mapApp.find('area[ref=' + map_position.attr('ref') + ']').attr("coords", newArea);
                    $('.link-conrainer li[ref=' + map_position.attr('ref') + '] .rect-value').val(newArea);
                    return false;
                }).mouseup(function (event) {
                    map_position_bg.data('mousedown', false);
                    map_position_bg.css('cursor', 'default');
                    return false;
                });
            });
            $('.position-conrainer .map-position .resize').each(function () {
                var map_position_resize = $(this);
                var conrainer = $(this).parent().parent();
                map_position_resize.unbind('mousedown').mousedown(function (event) {
                    map_position_resize.data('mousedown', true);
                    map_position_resize.data('pageX', event.pageX);
                    map_position_resize.data('pageY', event.pageY);
                    return false;
                }).unbind('mouseup').mouseup(function (event) {
                    map_position_resize.data('mousedown', false);
                    return false;
                });
                //点击取消拖动
                conrainer.unbind('click').click(function (event) {
                    map_position_resize.data('mousedown', false);
                    return false;
                });
                conrainer.mousemove(function (event) {
                    if (!map_position_resize.data('mousedown')) return false;
                    var dx = event.pageX - map_position_resize.data('pageX');
                    var dy = event.pageY - map_position_resize.data('pageY');
                    if ((dx == 0) && (dy == 0)) {
                        return false;
                    }
                    var map_position = map_position_resize.parent();
                    var p = map_position.position();
                    var left = p.left;
                    var top = p.top;
                    var height = map_position.height() + dy;
                    if ((top + height) > conrainer.height()) {
                        height = height - ((top + height) - conrainer.height());
                    }
                    if (height < 20) height = 20;
                    var width = map_position.width() + dx;
                    if ((left + width) > conrainer.width()) {
                        width = width - ((left + width) - conrainer.width());
                    }
                    if (width < 50) width = 50;
                    map_position.css({
                        width: width,
                        height: height
                    });
                    map_position_resize.data('pageX', event.pageX);
                    map_position_resize.data('pageY', event.pageY);

                    bottom = top + map_position.height();
                    right = left + map_position.width();

                    var newArea = new Array(left, top, right, bottom).join(',');
                    var mapApp = conrainer.parent().next('map');
                    mapApp.find('area[ref=' + map_position.attr('ref') + ']').attr("coords", newArea);
                    $('.link-conrainer li[ref=' + map_position.attr('ref') + '] .rect-value').val(newArea);
                    return false;
                }).mouseup(function (event) {
                    map_position_resize.data('mousedown', false);
                    return false;
                });
            });
            $('.position-conrainer .map-position .delete').unbind('click').click(function () {
                var ref = $(this).parent().attr('ref');
                var _link_conrainer = $(this).parent().parent().parent().find('.link-conrainer ul');
                var _coordsMap = $(this).parent().parent().parent().next('map');
                var _position_conrainer = $(this).parent().parent().parent().find('.position-conrainer');
                _link_conrainer.find('.map-link[ref=' + ref + ']').remove();
                _position_conrainer.find('.map-position[ref=' + ref + ']').remove();
                _coordsMap.find('area[ref=' + ref + ']').remove();
                var index = 1;
                _link_conrainer.find('.map-link').each(function () {
                    $(this).attr('ref', index).find('.link-number-text').html('热点' + index);
                    index++;
                });
                index = 1;
                _position_conrainer.find('.map-position').each(function () {
                    $(this).attr('ref', index).find('.link-number-text').html('热点' + index);
                    index++;
                });
                index = 1;
                _coordsMap.find('area').each(function () {
                    $(this).attr('ref', index);
                    index++;
                });
            });
        }

        bind_map_event();

        function define_css() {
            //样式定义
            $container.find('.map-position .resize').css({
                display: 'block',
                position: 'absolute',
                right: 0,
                bottom: 0,
                width: 5,
                height: 5,
                cursor: 'nw-resize',
                background: '#000'
            });
        }
        define_css();
    };
})(jQuery);