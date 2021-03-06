﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderList.aspx.cs" Inherits="EASYUITest.OrderList" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta charset="UTF-8">
    <title>后台管理系统</title>
    <link rel="stylesheet" type="text/css" href="jquery-easyui/themes/gray/easyui.css">
    <link rel="stylesheet" type="text/css" href="jquery-easyui/themes/icon.css">
    <link href="jquery-easyui/themes/skin-blue.css" rel="stylesheet" />
    <link href="font-awesome/css/font-awesome.css" rel="stylesheet" />
    <script type="text/javascript" src="jquery-easyui/jquery.min.js" ></script>
    <script type="text/javascript" src="jquery-easyui/jquery.easyui.min.js" charset="UTF-8"></script>
    <script src="jquery-easyui/locale/easyui-lang-zh_CN.js"></script>
    <script src="js/easyuiList.js"></script>
    <script src="js/common.js"></script>
    <script>
        $(function () {
            gridHelper.controllerUrl = "/ashx/OrderListController.ashx";
            gridHelper.initGrid();
        })

    </script>
</head>
<body>
    <table id="tt" style="width: 100%;">
        <thead>
            <tr>
                <th data-options="field:'ck',checkbox:true"></th>
                <th data-options="field:'Id',width:160">订单号</th>
                <th data-options="field:'Weid',width:100">weid</th>
                <th data-options="field:'ReceiverName',width:100">收货人</th>
                <th data-options="field:'Phone',width:100">电话</th>
                <th data-options="field:'Province',width:100,align:'right'">省</th>
                <th data-options="field:'City',width:100,align:'right'">市</th>
                <th data-options="field:'Area',width:100,align:'right'">县区</th>
                <th data-options="field:'Address',width:250,align:'left'">详细地址</th>
                <th data-options="field:'OrderState',width:100,align:'left'">订单状态</th>
            </tr>
        </thead>
    </table>
    <div id="tb" style="padding: 2px 5px;">
        <div id="searchdiv">
            电话：<input type="text" class="easyui-textbox" name="Phone" />
            成为创客时间:
            <input class="easyui-datebox" style="width: 110px">
            至<input class="easyui-datebox" style="width: 110px">
            订单状态: 
		<select class="easyui-combobox" name="OrderState" panelheight="auto" style="width: 100px">
            <option value="">全部</option>
            <option value="待发货">待发货</option>
            <option value="待收货">待收货</option>
            <option value="已退货">已退货</option>
            <option value="已完成">已完成</option>
        </select>
            <a href="#" class="easyui-linkbutton" iconcls="fa fa-search" onclick="dosearch()">搜索</a>
            <a href="#" class="easyui-linkbutton" iconcls="fa fa-file-excel-o" onclick="doexport()">导出</a>
            <a href="#" class="easyui-linkbutton" iconcls="fa fa-file-excel-o" onclick="opendlg('dlg')">批量发货</a>
        </div>
    </div>
    <div id="dlg" class="easyui-dialog" title="批量发货" data-options="iconCls:'icon-save',closed:true" style="width: 400px; height: 300px; padding: 10px">
        <form action="/ashx/OrderListController.ashx" enctype="multipart/form-data" method="post" id="fm">
            <div style="margin-bottom: 20px">
                <input id="uploadfile" class="easyui-filebox" name="uploadfile" data-options="prompt:'选择文件...'" style="width: 100%;">
            </div>
            <div style="margin-bottom: 20px">
                <a href="#" class="easyui-linkbutton" onclick="uploadFile()" style="width: 100%;">上传</a>
            </div>
            <div id="progress" class="easyui-progressbar" style="width: 100%; margin-bottom: 20px"></div>
        </form>
        <div style="margin-bottom: 20px">
            <a href="#" class="easyui-linkbutton" style="width: 100%;">确定</a>
        </div>
    </div>

</body>
</html>
