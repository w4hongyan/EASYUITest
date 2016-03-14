<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="EASYUITest.Index" %>

<!DOCTYPE html>
<html>
<head>
	<meta charset="UTF-8">
	<title>后台管理系统</title>
	<link rel="stylesheet" type="text/css" href="jquery-easyui/themes/gray/easyui.css">
	<link rel="stylesheet" type="text/css" href="jquery-easyui/themes/icon.css">
	<script type="text/javascript" src="jquery-easyui/jquery.min.js"></script>
	<script type="text/javascript" src="jquery-easyui/jquery.easyui.min.js"></script>
</head>
<body class="easyui-layout">
	<div data-options="region:'north',border:false" style="height:60px;background:#3C8DBC;padding:10px;color:#ffffff;">后台管理系统</div>
	<div data-options="region:'west',split:true,title:'West'" style="width:200px;padding:10px;">
		<ul id="menuTree" class="easyui-tree" data-options="url:'/ashx/Sys/ModuleController.ashx?action=getModule',method:'get',animate:true"></ul>
	</div>
	<div data-options="region:'south',border:false" style="height:50px;background:#A9FACD;padding:10px;">版权所有</div>
	<div data-options="region:'center',title:'Center'">
        <div id="etab" class="easyui-tabs" style="width:100%;height:100%">
        <div title="我的桌面" style="padding:10px">
			<p>The tabs has a width of 100%.</p>
		</div>
        </div>
	</div>
    <script>
        $("#menuTree").tree({
            onClick: function (node){
                addTab(node.text, node.url)
            }
        });
        function addTab(tit, link) {
            if ($('#etab').tabs('exists', tit)) {
                $('#etab').tabs('select', tit);
            } else {
                $('#etab').tabs('add', {
                    title: tit,
                    content: '<iframe scrolling="auto" frameborder="0"  src="' + link + '" style="width:99%;height:98.5%;border-style:solid;  border-width:3px; border-color:#F2F2F2;"></iframe>',
                    fit: true,
                    closable: true
                });
            }
        }
    </script>
</body>
</html>
