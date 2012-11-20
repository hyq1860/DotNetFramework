var fileview = $.extend({}, $.fn.datagrid.defaults.view, {
    render: function (target, container, frozen) {
        var opts = $.data(target, 'datagrid').options;
        var rows = $.data(target, 'datagrid').data.rows;
        var fields = $(target).datagrid('getColumnFields', frozen);
        var table = [];
        for (var i = 0; i < rows.length; i++) {
            table.push('<table cellspacing="0" cellpadding="0" border="0" style="float:left;margin:3px 2px;display:block;"><tbody>');
            var cls = (i % 2 && opts.striped) ? 'class="datagrid-row-alt"' : '';
            table.push('<tr datagrid-row-index="' + i + '" ' + cls + '>');
            table.push(this.renderRow.call(this, target, i, rows[i]));
            table.push('</tr>');
            table.push('</tbody></table>');
        }
        $(container).prev().remove();
        $(container).html(table.join(''));
    },
    renderRow: function (target, rowIndex, rowData) {
        var opts = $.data(target, 'datagrid').options;
        var col = $(target).datagrid('getColumnOption', opts.textField);
        var style = 'width:' + (col.boxWidth) + 'px;';
        var td = [];
        td.push('<td field="' + opts.textField + '" style="border:none;" title=' + rowData[opts.textField] + '>');
        td.push('<table cellspacing="0" cellpadding="0" border="0"><tbody>');
        td.push('<tr>');
        td.push('<td style="border:none;" align="center">');
        if (opts.imgFormatter) {
            td.push(opts.imgFormatter.call(target, rowIndex, rowData));
        } else {
            td.push(rowData[opts.textField]);
        }
        td.push('</td>');
        td.push('</tr>');
        td.push('<tr>');
        td.push('<td style="border:none;" align="center">');
        if (col.formatter) {
            td.push('<div style="' + style + '">' + col.formatter(rowData[opts.textField].replace(/\s/ig, ''), rowData, rowIndex) + "</div>");
        } else {
            td.push('<div style="' + style + '">' + rowData[opts.textField].replace(/\s/ig, '') + "</div>");
        }

        td.push('</td>');
        td.push('</tr>');
        td.push('</tbody></table>');
        td.push('</td>');
        td.push('</td>');
        return td.join('');
    }
});