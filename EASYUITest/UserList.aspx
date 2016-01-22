<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserList.aspx.cs" Inherits="EASYUITest.UserList" %>

<!DOCTYPE html>

<html>
<head runat="server">
	<meta charset="UTF-8">
	<title>后台管理系统</title>
	<link rel="stylesheet" type="text/css" href="jquery-easyui/themes/gray/easyui.css">
	<link rel="stylesheet" type="text/css" href="jquery-easyui/themes/icon.css">
    <link href="font-awesome/css/font-awesome.css" rel="stylesheet" />
    <link href="jquery-easyui/themes/skin-blue.css" rel="stylesheet" />
	<script type="text/javascript" src="jquery-easyui/jquery.min.js"></script>
	<script type="text/javascript" src="jquery-easyui/jquery.easyui.min.js"></script>
    <script src="jquery-easyui/locale/easyui-lang-zh_CN.js"></script>
    <script src="js/easyuiList.js"></script>
    <script src="js/common.js"></script>
    <script>
        $(function () {
            gridHelper.controllerUrl = "/ashx/UserListController.ashx";
            gridHelper.initGrid();
        })
        
    </script>
</head>
<body>
    <table  id="tt" style="width:100%;">
		<thead>
			<tr>
                <th data-options="field:'ck',checkbox:true"></th>
                <th data-options="field:'weid',width:100">weid</th>
				<th data-options="field:'name',width:100">姓名</th>
				<th data-options="field:'nickname',width:100">昵称</th>
                <th data-options="field:'sex',width:60">性别</th>
				<th data-options="field:'bossid',width:100,align:'right'">bossid</th>
				<th data-options="field:'mobilephone',width:100,align:'right'">手机号</th>
                <th data-options="field:'XueHao',width:100,align:'right'">学号</th>
                 <th data-options="field:'mastertime',width:160,align:'right'">成为创客时间</th>
			</tr>
		</thead>
	</table>
	<div id="tb" style="padding:2px 5px;">
        <div id="searchdiv">
            姓名:<input name="name_like" class="easyui-textbox" style="width:110px" />
		成为创客时间: <input class="easyui-datebox" name="mastertime_mte"  style="width:110px">
		至<input class="easyui-datebox" name="mastertime_lt"  style="width:110px">
<%--		Language: 
		<select class="easyui-combobox" panelHeight="auto" style="width:100px">
			<option value="java">Java</option>
			<option value="c">C</option>
			<option value="basic">Basic</option>
			<option value="perl">Perl</option>
			<option value="python">Python</option>
		</select>--%>
        <a href="#" class="easyui-linkbutton" iconCls="fa fa-search" onclick="dosearch()" >搜索</a>
        <a href="#" class="easyui-linkbutton" iconCls="fa fa-file-excel-o" onclick="doexport()" >导出</a>
        </div>
		
           
	</div>

</body>
</html>
