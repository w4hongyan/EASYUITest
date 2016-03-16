<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PriviledgeList.aspx.cs" Inherits="EASYUITest.Sys.PreviledgeList" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta charset="UTF-8">
    <title>后台管理系统</title>
    <link rel="stylesheet" type="text/css" href="/jquery-easyui/themes/gray/easyui.css">
    <link rel="stylesheet" type="text/css" href="/jquery-easyui/themes/icon.css">
    <link href="/font-awesome/css/font-awesome.css" rel="stylesheet" />
    <link href="/jquery-easyui/themes/skin-blue.css" rel="stylesheet" />
    <script type="text/javascript" src="/jquery-easyui/jquery.min.js"></script>
    <script type="text/javascript" src="/jquery-easyui/jquery.easyui.min.js"></script>
    <script src="/jquery-easyui/locale/easyui-lang-zh_CN.js"></script>
    <script src="/js/easyuiList.js"></script>
    <script src="/js/common.js"></script>
    <script>
        $(function () {
            gridHelper.controllerUrl = "/ashx/Sys/PriviledgeController.ashx";
            gridHelper.initGrid();
            loadModules();
        })

        function loadModules() {
            $('#ModuleId').combotree({
                url: "/ashx/Sys/ModuleController.ashx?action=getModule"
            });
        }


    </script>
</head>
<body>
    <table id="tt" style="width: 100%;">
        <thead>
            <tr>
                <th data-options="field:'ck',checkbox:true"></th>
                <th data-options="field:'ModuleId',width:100,sortable:true">模块Id</th>
                <th data-options="field:'ModuleName',width:100,sortable:true">模块名称</th>
                <th data-options="field:'Code',width:100,sortable:true">权限代码</th>
                <th data-options="field:'Name',width:100,sortable:true">权限名称</th>
                <th data-options="field:'isDelete',width:100,sortable:true" formatter="com.formatBool">是否删除</th>
            </tr>
        </thead>
    </table>
    <div id="tb" style="padding: 2px 5px;">
        <div><a href="#" class="easyui-linkbutton" iconcls="fa fa-plus-circle" onclick="$('#dlg').dialog('open')">添加</a></div>
        <div id="searchdiv">
            姓名:<input name="Name_like" class="easyui-textbox" style="width: 110px" />
            创建时间:
            <input class="easyui-datebox" name="CreateTime_mte" style="width: 110px">
            至<input class="easyui-datebox" name="CreateTime_lt" style="width: 110px">
            <a href="#" class="easyui-linkbutton" iconcls="fa fa-search" onclick="dosearch()">搜索</a>
            <a href="#" class="easyui-linkbutton" iconcls="fa fa-file-excel-o" onclick="doexport()">导出</a>
        </div>
    </div>
	<div id="dlg" class="easyui-dialog" style="width:500px;height:400px;padding:10px 30px;"
			title="添加窗口" closed="true" buttons="#dlg-buttons">
		<h2>编辑权限</h2>
		<form id="ff" method="post">
			<table>
			    <tr>
					<td>模块名称:</td>
					<td><select id="ModuleId" name="ModuleId" style="width:200px;"></select></td>
				</tr>
				<tr>
					<td>权限代码:</td>
					<td><input type="text" class="easyui-textbox" name="Code" style="width:350px;" data-options="required:true" /></td>
				</tr>
				<tr>
					<td>权限名称:</td>
					<td><input type="text" class="easyui-textbox" name="Name" style="width:350px;" data-options="required:true" /></td>
				</tr>
			</table>
		</form>
	</div>
	<div id="dlg-buttons">
		<a href="#" class="easyui-linkbutton" iconCls="icon-ok" onclick="add()">添加</a>
		<a href="#" class="easyui-linkbutton" iconCls="icon-cancel" onclick="javascript:$('#dlg').dialog('close')">取消</a>
	</div>

</body>
</html>
