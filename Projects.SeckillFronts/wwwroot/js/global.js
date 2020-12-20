
// 全局js
$(function () {
    // 1、获取AccessToken
    var user = getCache("user");
    // 2、设置jQuery Ajax全局的参数  
    $.ajaxSetup({
        headers: {
            "Authorization": "Bearer " + user.AccessToken,
            "UserId": user.UserId,
            "UserName": user.UserName
        },
        error: function (jqXHR, textStatus, errorThrown) {
            switch (jqXHR.status) {
                case (500):
                    alert("服务器系统内部错误");
                    break;
                case (401):
                    alert("未登录");
                    break;
                case (403):
                    alert("无权限执行此操作");
                    break;
                case (408):
                    alert("请求超时");
                    break;
                default:
                    console.log(jqXHR.statusText);
                    alert(jqXHR.statusText);
            }
        },
        success: function (data) {
            alert("操作成功");
        }
    });  


})