<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RoleSetPriviledgeList.aspx.cs" Inherits="EASYUITest.Sys.RoleSetPriviledgeList" %>

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

            gridHelper.controllerUrl = "/ashx/Sys/RoleController.ashx";
            gridHelper.singleSelect = true;
            gridHelper.title = "角色列表";
            gridHelper.onClickRow = onRoleClick;
            gridHelper.initGrid();


            gridHelper.controllerUrl = "/ashx/Sys/PriviledgeController.ashx";
            gridHelper.gridId = "tt1";
            gridHelper.singleSelect = false;
            gridHelper.pageSize = 500;
            gridHelper.title = "权限列表";
            gridHelper.isInit = false;
            gridHelper.initGrid();
        });

        function onRoleClick() {
            alert("haha");
            com.get("/ashx/Sys/PriviledgeController.ashx?action=getHasPriviledge", function(msg) {
                alert(msg);
            });
        }

    </script>
</head>
<body>
    <table>
        <tr>
            <td >
                <table id="tt" style="width: 100%;">
                    <thead>
                        <tr>
                            <th data-options="field:'Id',width:60,sortable:true">Id</th>
                            <th data-options="field:'Code',width:100,sortable:true">角色代码</th>
                            <th data-options="field:'Name',width:100,sortable:true">角色名称</th>
                        </tr>
                    </thead>
                </table>
            </td>
            <td >
                <table id="tt1" style="width: 100%;">
                    <thead>
                        <tr>
                            <th data-options="field:'ck',checkbox:true"></th>
                            <th data-options="field:'ModuleId',width:60,sortable:true">模块Id</th>
                            <th data-options="field:'ModuleName',width:100,sortable:true">模块名称</th>
                            <th data-options="field:'Code',width:100,sortable:true">权限代码</th>
                            <th data-options="field:'Name',width:100,sortable:true">权限名称</th>
                        </tr>
                    </thead>
                </table>
            </td>
        </tr>
    </table>
</body>
</html>
