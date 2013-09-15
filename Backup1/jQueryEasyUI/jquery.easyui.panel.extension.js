var easyuiPanelOnMove = function (left, top) {/* 防止超出浏览器边界 */
    if (left < 0) {
        $(this).window('move', {
            left: 1
        });
    }
    if (top < 0) {
        $(this).window('move', {
            top: 1
        });
    }
    var width = $(this).panel('options').width;
    var height = $(this).panel('options').height;
    var right = left + width;
    var buttom = top + height;
    var browserWidth = $(document).width();
    var browserHeight = $(document).height();
    if (right > browserWidth) {
        $(this).window('move', {
            left: browserWidth - width
        });
    }
    if (buttom > browserHeight) {
        $(this).window('move', {
            top: browserHeight - height
        });
    }
};
$.fn.panel.defaults.onMove = easyuiPanelOnMove;
$.fn.window.defaults.onMove = easyuiPanelOnMove;
$.fn.dialog.defaults.onMove = easyuiPanelOnMove;