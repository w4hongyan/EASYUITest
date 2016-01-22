﻿var datagrid;
var gridHelper = {
    gridId: "tt",
    controllerUrl: "",
    isInit: false,
    initGrid: function () {
        if (!gridHelper.isInit) {
            datagrid = $("#" + gridHelper.gridId).datagrid({
                rownumbers: true,
                singleSelect: false,
                pagination: true,
                pageSize: 30,
                pageList: [10, 20,30, 40, 50, 100, 200,500], //可以设置每页记录条数的列表
                url: gridHelper.controllerUrl,
                method: 'get',
                toolbar: '#tb',
                onLoadSuccess: function () {
                    $(this).datagrid("fixRownumber");
                }
            });
            gridHelper.isInit = true;
        }
    }
}
//获取查询Json字符串
function GetQueryJson(actionId) {
    var scstr = "{";
    if (actionId != "")
    {
        scstr += "\"action\":\""+actionId+"\",";
    }
    $("#searchdiv").find("input").each(function () {
        if ($(this).attr("name") != undefined && $(this).attr("name") != "" && $(this).val()!="") {
            scstr += "\""+$(this).attr("name")+"\":\""+$(this).val() + "\",";
        }
    });

    $("#searchdiv").find("select").each(function () {
        var sname = $(this).attr("name");
        if (sname != undefined && sname != "" && $(this).val() != "") {
            scstr += "\"" + $(this).attr("name") + "\":\"" + $(this).val() + "\",";
            
        }
    });
    if (scstr.length > 1) {
        scstr = scstr.substr(0, scstr.length - 1);
    }
    scstr += "}";
    return scstr;
}
function dosearch() {
    var $grid = $("#" + gridHelper.gridId);
    var queryJson=GetQueryJson();
    //gridHelper.isRefresh = true;
    $grid.datagrid("load", $.parseJSON(queryJson));
}
//通用导出方法
function doexport()
{
    var frozencolumn = $("#" + gridHelper.gridId).datagrid('getColumnFields', true);
    var column = "";
    if (frozencolumn != "") {//如果有固定列
        column = frozencolumn + "," + $("#" + gridHelper.gridId).datagrid('getColumnFields');
    } else {
        column = $("#" + gridHelper.gridId).datagrid('getColumnFields')+"";
    }
    var columnArray = column.split(",");
    var relArray = new Array();
    var i = 0;
    $.each(columnArray, function () {
        if (this != "ck") {
            relArray[i] = this + "@" + $("tr.datagrid-header-row").find("td[field='" + this + "'] div span:eq(0)").html();
            i++;
        }
       
    });
    var columnStr = relArray.toString();//excel表头重命名数组
    var page = $("#" + gridHelper.gridId).datagrid('options').pageNumber//获取分页
    var rows = $("#" + gridHelper.gridId).datagrid('options').pageSize
    var queryJson = GetQueryJson("doexport");
    var parames = $.parseJSON(queryJson);
    var form = $("<form>");
    form.attr("style", "display:none");
    form.attr("target", "");
    form.attr("method", "get");
    form.attr("action", gridHelper.controllerUrl);
    for (var key in parames) {
        appdentToForm(form, key, parames[key]);
    }
    appdentToForm(form, "page", page);
    appdentToForm(form, "rows", rows);
    appdentToForm(form, "columnStr", columnStr);
    $("body").append(form);//将表单放置在web中
    form.submit();//表单提交 
}
function appdentToForm(form, name, value)
{
    var input = $("<input>");
    input.attr("type", "hidden");
    input.attr("name", name);
    input.attr("value", value);
    form.append(input);
}
//导入方法
function doimport() {
    var form = $("<form>");
    form.attr("style", "display:none");
    form.attr("enctype","multipart/form-data")
    form.attr("target", "");
    form.attr("method", "post");
    form.attr("action", gridHelper.controllerUrl);
    var input = $("<input>");
    input.attr("type", "file");
    input.attr("id", "_f");
    input.attr("name", "_f");
    form.append(input);
    $("body").append(form);//将表单放置在web中
    $("#_f").click();
    //form.submit();//表单提交 
}

//扩展方法解决rownumber显示不全
$.extend($.fn.datagrid.methods, {
    fixRownumber : function (jq) {
        return jq.each(function () {
            var panel = $(this).datagrid("getPanel");
            //获取最后一行的number容器,并拷贝一份
            var clone = $(".datagrid-cell-rownumber", panel).last().clone();
            //由于在某些浏览器里面,是不支持获取隐藏元素的宽度,所以取巧一下
            clone.css({
                "position" : "absolute",
                left : -1000
            }).appendTo("body");
            var width = clone.width("auto").width();
            //默认宽度是25,所以只有大于25的时候才进行fix
            if (width > 25) {
                //多加5个像素,保持一点边距
                $(".datagrid-header-rownumber,.datagrid-cell-rownumber", panel).width(width + 5);
                //修改了宽度之后,需要对容器进行重新计算,所以调用resize
                $(this).datagrid("resize");
                //一些清理工作
                clone.remove();
                clone = null;
            } else {
                //还原成默认状态
                $(".datagrid-header-rownumber,.datagrid-cell-rownumber", panel).removeAttr("style");
            }
        });
    }
});