<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ModuleList.aspx.cs" Inherits="EASYUITest.Sys.ModuleList" %>

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
            gridHelper.controllerUrl = "/ashx/Sys/ModuleController.ashx";
            gridHelper.initGrid();
            loadModules();
        })

        function loadModules() {
            $('#ParentId').combotree({
                url: gridHelper.controllerUrl + "?action=getModule"
            });
        }


    </script>
</head>
<body>
    <table id="tt" style="width: 100%;">
        <thead>
            <tr>
                <th data-options="field:'ck',checkbox:true"></th>
                <th data-options="field:'Code',width:100,sortable:true">模块代码</th>
                <th data-options="field:'Name',width:100,sortable:true">模块名称</th>
                <th data-options="field:'Url',width:200,sortable:true">模块Url</th>
                 <th data-options="field:'Ico',width:100,sortable:true">模块图标</th>
                <th data-options="field:'ParentId',width:100,sortable:true">上级模块Id</th>
                <th data-options="field:'isDelete',width:100,sortable:true" formatter="com.formatBool">是否删除</th>
            </tr>
        </thead>
    </table>
    <div id="tb" style="padding: 2px 5px;">
        <div><a href="#" class="easyui-linkbutton" iconcls="fa fa-plus-circle" onclick="$('#dlg').dialog('open')">新建</a></div>
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
		<h2>编辑模块</h2>
		<form id="ff" method="post">
			<table>
				<tr>
					<td>模块代码:</td>
					<td><input type="text" class="easyui-textbox" name="Code" style="width:350px;" data-options="required:true" /></td>
				</tr>
				<tr>
					<td>模块名称:</td>
					<td><input type="text" class="easyui-textbox" name="Name" style="width:350px;" data-options="required:true" /></td>
				</tr>
				<tr>
					<td>模块Url:</td>
					<td><input type="text" class="easyui-textbox" name="Url" style="width:350px;" data-options="required:true" /></td>
				</tr>
                <tr>
					<td>模块图标:</td>
					<td><input type="text" class="easyui-textbox" name="Ico" style="width:350px;" data-options="required:true" /></td>
				</tr>
                <tr>
					<td>上级模块:</td>
					<td><select id="ParentId" name="ParentId" style="width:200px;"></select></td>
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
