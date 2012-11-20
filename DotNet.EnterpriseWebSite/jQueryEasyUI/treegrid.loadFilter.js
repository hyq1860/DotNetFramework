$.fn.treegrid.defaults.loadFilter = function (data, parent) {
    var opt = $(this).data().treegrid.options;
    var idFiled,
	textFiled,
	parentField;
    if (opt) {
        idFiled = opt.idFiled || 'Id';
        textFiled = opt.textFiled || 'Name';
        parentField = opt.parentField || 'ParentId';

        var i,
		l,
		treeData = [],
		tmpMap = [];

        for (i = 0, l = data.length; i < l; i++) {
            tmpMap[data[i][idFiled]] = data[i];
        }

        for (i = 0, l = data.length; i < l; i++) {
            if (tmpMap[data[i][parentField]] && data[i][idFiled] != data[i][parentField]) {
                if (!tmpMap[data[i][parentField]]['children'])
                    tmpMap[data[i][parentField]]['children'] = [];
                data[i]['text'] = data[i][textFiled];
                tmpMap[data[i][parentField]]['children'].push(data[i]);
            } else {
                data[i]['text'] = data[i][textFiled];
                treeData.push(data[i]);
            }
        }
        return treeData;
    }
    return data;
};