var com = {};
com.formatBool = function (value) {
    if (value=="True") {
        return "是";
    } else {
        return "否";
    }
}
com.get= function(url,callback) {
    $.get(url, callback);
}